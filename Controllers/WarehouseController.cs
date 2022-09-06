using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using MachManager.Context;
using MachManager.Models;
using MachManager.Models.Parameters;
using MachManager.Models.Operational;
using MachManager.Controllers.Base;
using MachManager.i18n;
using Microsoft.AspNetCore.Cors;
using MachManager.Helpers;
using MachManager.Models.Constants;
using MachManager.Models.ReportContainers;
using MachManager.Business;

namespace MachManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class WarehouseController : MgControllerBase
    {
        public WarehouseController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        public IEnumerable<WarehouseModel> Get()
        {
            ResolveHeaders(Request);
            WarehouseModel[] data = new WarehouseModel[0];
            try
            {
                int[] plants = null;
                if (_isDealer)
                    plants = _context.Plant.Where(d => d.DealerId == _appUserId).Select(d => d.Id).ToArray();
                else if (_isFactoryOfficer)
                    plants = new int[]{ _context.Officer.Where(d => d.Id == _appUserId).Select(d => d.PlantId).First() };

                using (DefinitionListsBO bObj = new DefinitionListsBO(this._context)){
                    data = bObj.GetWarehouses(plants);
                }
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public WarehouseModel Get(int id)
        {
            WarehouseModel data = new WarehouseModel();
            try
            {
                data = _context.Warehouse.Where(d => d.Id == id).Select(d => new WarehouseModel{
                        Id = d.Id,
                        DealerId = d.DealerId,
                        IsActive = d.IsActive,
                        PlantId = d.PlantId,
                        WarehouseCode = d.WarehouseCode,
                        WarehouseName = d.WarehouseName,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "FactoryOfficer")]
        [HttpPost]
        public BusinessResult Post(WarehouseModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                if ((model.PlantId ?? 0) <= 0)
                    throw new Exception(_translator.Translate(Expressions.PlantDoesntExists, _userLanguage));

                var dbObj = _context.Warehouse.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Warehouse();
                    _context.Warehouse.Add(dbObj);
                }

                if (_context.Warehouse.Any(d => d.WarehouseCode == model.WarehouseCode 
                    && d.PlantId == model.PlantId && d.Id != model.Id))
                    throw new Exception(_translator.Translate(Expressions.SameCodeExists, _userLanguage));

                model.MapTo(dbObj);

                _context.SaveChanges();
                result.Result=true;
                result.RecordId = dbObj.Id;
            }
            catch (System.Exception ex)
            {
                result.Result=false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }


        [HttpPost]
        [Authorize(Policy = "FactoryOfficer")]
        [Route("{plantId}/DeliverProduct")]
        public BusinessResult DeliverProduct(int plantId, DeliverProductModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbWarehouse = _context.Warehouse.FirstOrDefault(d => d.WarehouseCode == model.WarehouseCode && d.PlantId == plantId);

                // check employee credit state
                var dbEmployee = _context.Employee.FirstOrDefault(d => d.Id == model.EmployeeId);
                if (dbEmployee == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));
                
                var dbItem = _context.Item.FirstOrDefault(d => d.Id == model.ItemId);
                if (dbItem == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));
                
                var dbCreditList = _context.EmployeeCredit.Where(d => d.EmployeeId == model.EmployeeId
                    && d.ItemCategoryId == dbItem.ItemCategoryId).ToArray();
                var dbCredit = dbCreditList.FirstOrDefault(d => 
                    (d.ItemId == null || d.ItemId == dbItem.Id) && (d.ItemGroupId == null || d.ItemGroupId == dbItem.ItemGroupId));
                if (dbCredit == null || (dbCredit.RangeCredit - (model.Quantity ?? 1)) <= 0)
                    throw new Exception(_translator.Translate(Expressions.EmployeeIsOutOfCredit, _userLanguage));

                // start delivery operation
                //// create machine item consume by warehouse
                _context.MachineItemConsume.Add(new MachineItemConsume{
                    ConsumedCount = model.Quantity ?? 1,
                    ConsumedDate = DateTime.Now,
                    EmployeeId = model.EmployeeId,
                    ItemId = model.ItemId,
                    ItemGroupId = dbItem.ItemGroupId,
                    WarehouseId = dbWarehouse.Id,
                });

                //// create employee credit consume
                _context.EmployeeCreditConsume.Add(new EmployeeCreditConsume{
                    ConsumedCredit = model.Quantity ?? 1,
                    ConsumedDate = DateTime.Now,
                    EmployeeId = model.EmployeeId,
                    ItemId = model.ItemId,
                    ItemGroupId = dbItem.ItemGroupId,
                    ItemCategoryId = dbItem.ItemCategoryId,
                });

                var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == dbWarehouse.PlantId);
                dbPlant.LastUpdateDate = DateTime.Now;

                _context.SaveChanges();
                result.Result = true;

                // update live credits data
                using (MetaGanosSchema checkContext = SchemaFactory.CreateContext()){
                    EmployeeCreditModel creditModelDomain = new EmployeeCreditModel();
                    var checkDbCredit = checkContext.EmployeeCredit.FirstOrDefault(d => d.Id == dbCredit.Id);
                    checkDbCredit.MapTo(creditModelDomain);
                    creditModelDomain.UpdateLiveRangeData(_context);
                    creditModelDomain.MapTo(checkDbCredit);
                    checkContext.SaveChanges();
                }
            }
            catch (System.Exception ex)
            {
                result.Result = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("ConsumeReport")]
        public IEnumerable<MachineConsumeSummary> GetConsumeReport(MachineConsumeFilter filter){
            MachineConsumeSummary[] data = new MachineConsumeSummary[0];

            if (string.IsNullOrEmpty(filter.PlantCode))
                return data;
                
            var dbPlant = _context.Plant.FirstOrDefault(d => d.PlantCode == filter.PlantCode);
            var dbWr = _context.Warehouse.FirstOrDefault(d => d.WarehouseCode == filter.WarehouseCode && d.PlantId == dbPlant.Id);

            try
            {
                data = _context.MachineItemConsume
                    .Where(d =>
                        (dbWr.Id == d.WarehouseId)
                        &&
                        (filter == null || filter.StartDate == null || (filter.StartDate <= d.ConsumedDate && filter.EndDate >= d.ConsumedDate))
                        &&
                        (filter == null || filter.CategoryId == null || filter.CategoryId.Length == 0 || filter.CategoryId.Contains(d.Item.ItemCategoryId ?? 0))
                        &&
                        (filter == null || filter.GroupId == null || filter.GroupId.Length == 0 || filter.GroupId.Contains(d.Item.ItemGroupId ?? 0))
                        &&
                        (filter == null || filter.ItemId == null || filter.ItemId.Length == 0 || filter.ItemId.Contains(d.Item.Id))
                        &&
                        (filter == null || filter.EmployeeId == null || filter.EmployeeId == 0 || filter.EmployeeId == d.EmployeeId)
                    )
                    .GroupBy(d => new {
                        MachineCode = d.Machine != null ? d.Machine.MachineCode : "",
                        MachineName = d.Machine != null ? d.Machine.MachineName : "",
                        ItemCode = d.Item.ItemCode,
                        ItemName = d.Item.ItemName,
                        ItemCategoryCode = d.Item.ItemCategory.ItemCategoryCode,
                        ItemCategoryName = d.Item.ItemCategory.ItemCategoryName,
                        EmployeeCode = d.Employee.EmployeeCode,
                        EmployeeName = d.Employee.EmployeeName,
                        MachineId = d.MachineId,
                        WarehouseCode = d.Warehouse != null ? d.Warehouse.WarehouseCode : "",
                        WarehouseName = d.Warehouse != null ? d.Warehouse.WarehouseName : "",
                        ItemId = d.ItemId,
                        EmployeeId = d.EmployeeId,
                        ConsumedDate = d.ConsumedDate.Value,
                        SpiralNo = d.SpiralNo,
                    })
                    .Select(d => new MachineConsumeSummary{
                        MachineId = d.Key.MachineId,
                        EmployeeId = d.Key.EmployeeId,
                        ItemId = d.Key.ItemId,
                        MachineCode = d.Key.MachineCode,
                        MachineName = d.Key.MachineName,
                        EmployeeCode = d.Key.EmployeeCode,
                        EmployeeName = d.Key.EmployeeName,
                        ItemCode = d.Key.ItemCode,
                        ItemName = d.Key.ItemName,
                        ItemCategoryCode = d.Key.ItemCategoryCode,
                        ItemCategoryName = d.Key.ItemCategoryName,
                        WarehouseCode = d.Key.WarehouseCode,
                        WarehouseName = d.Key.WarehouseName,
                        ConsumedDate = d.Key.ConsumedDate,
                        SpiralNo = d.Key.SpiralNo,
                        TotalConsumed = d.Sum(m => m.ConsumedCount),
                    })
                    .OrderBy(d => d.ConsumedDate)
                    .ToArray();
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }


        [Authorize(Policy = "FactoryOfficer")]
        [HttpDelete]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.Warehouse.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                _context.Warehouse.Remove(dbObj);

                _context.SaveChanges();
                result.Result=true;
            }
            catch (System.Exception ex)
            {
                result.Result=false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}
