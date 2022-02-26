using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using MachManager.Context;
using MachManager.Models;
using MachManager.Models.Operational;
using MachManager.Controllers.Base;
using MachManager.i18n;
using Microsoft.AspNetCore.Cors;
using MachManager.Helpers;

namespace MachManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class MachineController : MgControllerBase
    {
        public MachineController(MetaGanosSchema context): base(context){ }

        [Authorize(Policy = "Dealer")]
        [HttpGet]
        public IEnumerable<MachineModel> Get()
        {
            MachineModel[] data = new MachineModel[0];
            try
            {
                data = _context.Machine.Select(d => new MachineModel{
                        Id = d.Id,
                        Barcode = d.Barcode,
                        Brand = d.Brand,
                        BrandModel = d.BrandModel,
                        City = d.City,
                        Cols = d.Cols,
                        Country = d.Country,
                        CreatedDate = d.CreatedDate,
                        District = d.District,
                        InventoryCode = d.InventoryCode,
                        InventoryEntryDate = d.InventoryEntryDate,
                        IsActive = d.IsActive,
                        LocationData = d.LocationData,
                        MachineCode = d.MachineCode,
                        MachineName = d.MachineName,
                        PlantCode = d.Plant != null ? d.Plant.PlantCode : "",
                        PlantId = d.PlantId,
                        PlantName = d.Plant != null ? d.Plant.PlantName : "",
                        ProductionDate = d.ProductionDate,
                        Rows = d.Rows,
                        SpecialCustomer = d.SpecialCustomer,
                        StartVideoPath = d.StartVideoPath,
                    }).OrderBy(d => d.MachineCode).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public MachineModel Get(int id)
        {
            MachineModel data = new MachineModel();
            try
            {
                data = _context.Machine.Where(d => d.Id == id).Select(d => new MachineModel{
                        Id = d.Id,
                        Barcode = d.Barcode,
                        Brand = d.Brand,
                        BrandModel = d.BrandModel,
                        City = d.City,
                        Cols = d.Cols,
                        Country = d.Country,
                        CreatedDate = d.CreatedDate,
                        District = d.District,
                        InventoryCode = d.InventoryCode,
                        InventoryEntryDate = d.InventoryEntryDate,
                        IsActive = d.IsActive,
                        LocationData = d.LocationData,
                        MachineCode = d.MachineCode,
                        MachineName = d.MachineName,
                        PlantCode = d.Plant != null ? d.Plant.PlantCode : "",
                        PlantId = d.PlantId,
                        PlantName = d.Plant != null ? d.Plant.PlantName : "",
                        ProductionDate = d.ProductionDate,
                        Rows = d.Rows,
                        SpecialCustomer = d.SpecialCustomer,
                        StartVideoPath = d.StartVideoPath,
                    }).FirstOrDefault();

                data.Spirals = _context.MachineSpiral.Where(d => d.MachineId == data.Id)
                    .Select(d => new MachineSpiralModel{
                        Id = d.Id,
                        ActiveQuantity = d.ActiveQuantity,
                        ItemCategoryCode = d.ItemCategory != null ? d.ItemCategory.ItemCategoryCode : "",
                        ItemCategoryId = d.ItemCategoryId,
                        ItemCategoryName = d.ItemCategory != null ? d.ItemCategory.ItemCategoryName : "",
                        ItemCode = d.Item != null ? d.Item.ItemCode : "",
                        ItemGroupCode = d.ItemGroup != null ? d.ItemGroup.ItemGroupCode : "",
                        ItemGroupId = d.ItemGroupId,
                        ItemGroupName = d.ItemGroup != null ? d.ItemGroup.ItemGroupName : "",
                        ItemId = d.ItemId,
                        ItemName = d.Item != null ? d.Item.ItemName : "",
                        MachineId = d.MachineId,
                        PosOrders = d.PosOrders,
                        PosX = d.PosX,
                        PosY = d.PosY,
                    }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "Dealer")]
        [HttpPost]
        public BusinessResult Post(MachineModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request.Headers);

            try
            {
                var dbObj = _context.Machine.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Machine();
                    _context.Machine.Add(dbObj);
                }

                if (_context.Machine.Any(d => d.MachineCode == model.MachineCode && d.Id != model.Id))
                    throw new Exception(_translator.Translate(Expressions.SameCodeExists, _userLanguage));

                model.MapTo(dbObj);

                // save spirals
                var oldSpirals = _context.MachineSpiral.Where(d => d.MachineId == dbObj.Id).ToArray();
                foreach (var item in oldSpirals)
                {
                    _context.MachineSpiral.Remove(item);
                }

                if (model.Spirals != null){
                    foreach (var item in model.Spirals)
                    {
                        var dbSpiral = new MachineSpiral();
                        item.MapTo(dbSpiral);
                        dbSpiral.Id = 0;
                        dbSpiral.Machine = dbObj;
                        _context.MachineSpiral.Add(dbSpiral);
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

        [Authorize(Policy = "Dealer")]
        [HttpDelete]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request.Headers);

            try
            {
                var dbObj = _context.Machine.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                var spirals = _context.MachineSpiral.Where(d => d.MachineId == dbObj.Id).ToArray();
                foreach (var item in spirals)
                {
                    _context.MachineSpiral.Remove(item);
                }

                _context.Machine.Remove(dbObj);

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
