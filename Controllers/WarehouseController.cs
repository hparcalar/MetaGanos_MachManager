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
using ClosedXML;
using ClosedXML.Excel;

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

                if (data != null && data.Id > 0){
                    data.HotSalesCategories = _context.WarehouseHotSalesCategory.Where(d => d.WarehouseId == id)
                        .Select(d => new WarehouseHotSalesCategoryModel{
                            Id = d.Id,
                            ItemCategoryId = d.ItemCategoryId,
                            ItemGroupId = d.ItemGroupId,
                            ItemId = d.ItemId,
                            ItemText = d.Item != null ? d.Item.ItemName :
                                d.ItemGroup != null ? d.ItemGroup.ItemGroupName :
                                d.ItemCategory != null ? d.ItemCategory.ItemCategoryName : "",
                        }).ToArray();
                }
                else
                {
                    data = new WarehouseModel();
                    data.HotSalesCategories = new WarehouseHotSalesCategoryModel[0];
                }
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

                 // save hot sales categories
                var oldCategories = _context.WarehouseHotSalesCategory.Where(d => d.WarehouseId == dbObj.Id).ToArray();
                foreach (var item in oldCategories)
                {
                    _context.WarehouseHotSalesCategory.Remove(item);
                }

                if (model.HotSalesCategories != null){
                    foreach (var item in model.HotSalesCategories)
                    {
                        var dbCategory = new WarehouseHotSalesCategory();
                        item.MapTo(dbCategory);
                        dbCategory.Id = 0;
                        dbCategory.Warehouse = dbObj;
                        _context.WarehouseHotSalesCategory.Add(dbCategory);
                    }
                }

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


        [Authorize(Policy = "FactoryOfficer")]
        [HttpPost]
        [Route("{warehouseId}/LoadWarehouse")]
        public BusinessResult LoadWarehouse(int warehouseId, LoadWarehouseModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.Warehouse.FirstOrDefault(d => d.Id == warehouseId);
                if (dbObj == null){
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));
                }

                // add load stamp
                var dbLoad = new WarehouseLoad{
                    ItemId = model.ItemId,
                    LoadDate = DateTime.Now,
                    WarehouseId = warehouseId,
                    LoadType = model.LoadType,
                    MachineId = model.MachineId,
                    Quantity = model.Quantity,
                    OfficerId = this._isFactoryOfficer ? this._appUserId : null,
                };
                _context.WarehouseLoad.Add(dbLoad);

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


        [Authorize(Policy = "FactoryOfficer")]
        [HttpDelete]
        [Route("DeleteLoadStamp/{id}")]
        public BusinessResult DeleteLoadStamp(int id){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.WarehouseLoad.FirstOrDefault(d => d.Id == id);
                if (dbObj == null){
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));
                }

                _context.WarehouseLoad.Remove(dbObj);

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


        [Authorize(Policy = "FactoryOfficer")]
        [HttpGet]
        [Route("{id}/LoadStampList")]
        public IEnumerable<WarehouseLoadModel> GetLoadStampList(int id){
            ResolveHeaders(Request);
            WarehouseLoadModel[] data = new WarehouseLoadModel[0];

            try
            {
                data = _context.WarehouseLoad.Where(d => d.WarehouseId == id)
                    .Select(d => new WarehouseLoadModel{
                        Id = d.Id,
                        ItemCategoryCode = d.Item.ItemCategory != null ? d.Item.ItemCategory.ItemCategoryCode : "",
                        ItemCategoryName = d.Item.ItemCategory != null ? d.Item.ItemCategory.ItemCategoryName : "",
                        ItemGroupCode = d.Item.ItemGroup != null ? d.Item.ItemGroup.ItemGroupCode : "",
                        ItemGroupName = d.Item.ItemGroup != null ? d.Item.ItemGroup.ItemGroupName : "",
                        ItemCode = d.Item.ItemCode,
                        ItemName = d.Item.ItemName,
                        ItemId = d.ItemId,
                        LoadDate = d.LoadDate,
                        LoadType = d.LoadType,
                        Quantity = d.Quantity,
                        WarehouseId = d.WarehouseId,
                        MachineCode = d.Machine != null ? d.Machine.MachineCode : "",
                        MachineId = d.MachineId,
                        MachineName = d.Machine != null ? d.Machine.MachineName : "",
                        LoadTypeText = d.LoadType == 1 ? "Giriş" : d.LoadType == 2 ? "Çıkış" : "",
                    })
                    .OrderByDescending(d => d.LoadDate)
                    .ToArray();
            }
            catch
            {
                
            }
            
            return data;
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


        [AllowAnonymous]
        [HttpPost]
        [Route("ItemStatusReport")]
        public IEnumerable<WarehouseItemStatusSummary> GetItemStatusReport(WarehouseItemStatusFilter filter){
            WarehouseItemStatusSummary[] data = new WarehouseItemStatusSummary[0];

            if (filter.PlantId == null || filter.PlantId.Length == 0)
                return data;
                
            var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == filter.PlantId[0]);

            try
            {
                data = _context.WarehouseLoad
                    .Where(d =>
                        (filter == null || filter.CategoryId == null || filter.CategoryId.Length == 0 || filter.CategoryId.Contains(d.Item.ItemCategoryId ?? 0))
                        &&
                        (filter == null || filter.GroupId == null || filter.GroupId.Length == 0 || filter.GroupId.Contains(d.Item.ItemGroupId ?? 0))
                        &&
                        (filter == null || filter.ItemId == null || filter.ItemId.Length == 0 || filter.ItemId.Contains(d.Item.Id))
                    )
                    .GroupBy(d => new {
                        ItemId = d.ItemId,
                        ItemCode = d.Item.ItemCode,
                        ItemName = d.Item.ItemName,
                        ItemCategoryCode = d.Item.ItemCategory != null ? d.Item.ItemCategory.ItemCategoryCode : "",
                        ItemCategoryName = d.Item.ItemCategory != null ? d.Item.ItemCategory.ItemCategoryName : "",
                        ItemGroupCode = d.Item.ItemGroup != null ? d.Item.ItemGroup.ItemGroupCode : "",
                        ItemGroupName = d.Item.ItemGroup != null ? d.Item.ItemGroup.ItemGroupName : "",
                    })
                    .Select(d => new WarehouseItemStatusSummary{
                        ItemId = d.Key.ItemId,
                        ItemCode = d.Key.ItemCode,
                        ItemName = d.Key.ItemName,
                        ItemCategoryCode = d.Key.ItemCategoryCode,
                        ItemCategoryName = d.Key.ItemCategoryName,
                        ItemGroupCode = d.Key.ItemGroupCode,
                        ItemGroupName = d.Key.ItemGroupName,
                        InQuantity = d.Where(m => m.LoadType == 1).Sum(m => m.Quantity) ?? 0,
                        OutQuantity = d.Where(m => m.LoadType == 2).Sum(m => m.Quantity) ?? 0,
                    }).ToArray();

                if (data != null){
                    foreach (var item in data)
                    {
                        int? consumeOuts = _context.MachineItemConsume.Where(d => d.ItemId == item.ItemId).Sum(d => d.ConsumedCount);
                        item.OutQuantity += (consumeOuts ?? 0);
                        item.TotalQuantity = item.InQuantity - item.OutQuantity;
                    }
                }
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("ExcelItemStatusReport")]
        public IActionResult ExcelItemStatusReport(WarehouseItemStatusFilter filter){
            try
            {
                
            #region PREPARE DATA
            ItemConsumeAbs[] data = new ItemConsumeAbs[0];
                
            var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == filter.PlantId[0]);

            try
            {
                data = _context.WarehouseLoad
                    .Where(d =>
                        (filter == null || filter.CategoryId == null || filter.CategoryId.Length == 0 || filter.CategoryId.Contains(d.Item.ItemCategoryId ?? 0))
                        &&
                        (filter == null || filter.GroupId == null || filter.GroupId.Length == 0 || filter.GroupId.Contains(d.Item.ItemGroupId ?? 0))
                        &&
                        (filter == null || filter.ItemId == null || filter.ItemId.Length == 0 || filter.ItemId.Contains(d.Item.Id))
                    )
                    .GroupBy(d => new {
                        ItemId = d.ItemId,
                        ItemCode = d.Item.ItemCode,
                        ItemName = d.Item.ItemName,
                        ItemCategoryCode = d.Item.ItemCategory != null ? d.Item.ItemCategory.ItemCategoryCode : "",
                        ItemCategoryName = d.Item.ItemCategory != null ? d.Item.ItemCategory.ItemCategoryName : "",
                        ItemGroupCode = d.Item.ItemGroup != null ? d.Item.ItemGroup.ItemGroupCode : "",
                        ItemGroupName = d.Item.ItemGroup != null ? d.Item.ItemGroup.ItemGroupName : "",
                    })
                    .Select(d => new ItemConsumeAbs{
                        ItemCode = d.Key.ItemCode,
                        ItemName = d.Key.ItemName,
                        Category = d.Key.ItemCategoryName,
                        Group = d.Key.ItemGroupName,
                        InQuantity = d.Where(m => m.LoadType == 1).Sum(m => m.Quantity) ?? 0,
                        OutQuantity = d.Where(m => m.LoadType == 2).Sum(m => m.Quantity) ?? 0,
                    }).ToArray();

                if (data != null){
                    foreach (var item in data)
                    {
                        int? consumeOuts = _context.MachineItemConsume.Where(d => d.Item.ItemCode == item.ItemCode && d.Item.ItemCategory.PlantId == filter.PlantId[0]).Sum(d => d.ConsumedCount);
                        item.OutQuantity += (consumeOuts ?? 0);
                        item.TotalQuantity = item.InQuantity - item.OutQuantity;
                    }
                }
            }
            catch (System.Exception)
            {
                
            }
            #endregion

            #region PREPARE EXCEL FILE
            byte[] excelFile = new byte[0];

            using (var workbook = new XLWorkbook()) {
                var worksheet = workbook.Worksheets.Add("Tüketim Raporu");

                worksheet.Cell(1,1).Value = "Stok Kodu";
                worksheet.Cell(1,2).Value = "Stok Adı";
                worksheet.Cell(1,3).Value = "Kategori";
                worksheet.Cell(1,4).Value = "Grup";
                worksheet.Cell(1,5).Value = "Giriş";
                worksheet.Cell(1,6).Value = "Çıkış";
                worksheet.Cell(1,7).Value = "Kalan";

                worksheet.Cell(2,1).InsertData(data);

                worksheet.Columns().AdjustToContents();

                var titlesStyle = workbook.Style;
                titlesStyle.Font.Bold = true;
                titlesStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Row(1).Style = titlesStyle;

                using (MemoryStream memoryStream = new MemoryStream()) {
                    workbook.SaveAs(memoryStream);
                    excelFile = memoryStream.ToArray();
                }

                return Ok(excelFile);
            }

            #endregion
            
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }


        [Authorize(Policy = "FactoryOfficer")]
        [HttpDelete("{id}")]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.Warehouse.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                if (_context.WarehouseLoadHeader.Any(d => d.WarehouseId == id))
                    throw new Exception("Bu depoya ait hareket kayıtları bulunduğu için silme işlemi yapılamaz.");

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
