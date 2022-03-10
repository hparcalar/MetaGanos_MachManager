using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using MachManager.Context;
using MachManager.Models;
using MachManager.Models.Operational;
using MachManager.Models.Parameters;
using MachManager.Controllers.Base;
using MachManager.i18n;
using Microsoft.AspNetCore.Cors;
using MachManager.Helpers;
using Microsoft.AspNetCore.StaticFiles;

namespace MachManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class MachineController : MgControllerBase
    {
        private IWebHostEnvironment Environment = null;
        
        public MachineController(MetaGanosSchema context, IWebHostEnvironment environment): base(context, environment){
            this.Environment = environment;
        }

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
                        Capacity = d.Capacity,
                        PosX = d.PosX,
                        PosY = d.PosY,
                    }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("Find/{code}")]
        public MachineModel Find(string code)
        {
            MachineModel data = new MachineModel();
            try
            {
                data = _context.Machine.Where(d => d.MachineCode == code).Select(d => new MachineModel{
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
                        Capacity = d.Capacity,
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

        [HttpGet]
        [Route("{id}/Spirals/{spiralNo}")]
        public MachineSpiralModel GetSpiral(int id, int spiralNo)
        {
            MachineSpiralModel data = new MachineSpiralModel();
            try
            {
                data = _context.MachineSpiral.Where(d => d.MachineId == id &&
                    d.PosOrders == spiralNo)
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
                        Capacity = d.Capacity,
                        PosX = d.PosX,
                        PosY = d.PosY,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}/Spirals/{spiralNo}/Consumings")]
        public MachineItemConsumeModel[] GetSpiralConsumings(int id, int spiralNo)
        {
            MachineItemConsumeModel[] data = new MachineItemConsumeModel[0];
            try
            {
                data = _context.MachineItemConsume.Where(d => d.MachineId == id &&
                    d.SpiralNo == spiralNo)
                    .Select(d => new MachineItemConsumeModel{
                        Id = d.Id,
                        ConsumedCount = d.ConsumedCount,
                        ConsumedDate = d.ConsumedDate,
                        EmployeeCode = d.Employee != null ? d.Employee.EmployeeCode : "",
                        EmployeeId = d.EmployeeId,
                        EmployeeName = d.Employee != null ? d.Employee.EmployeeName : "",
                        ItemCode = d.Item != null ? d.Item.ItemCode : "",
                        ItemGroupId = d.ItemGroupId,
                        ItemId = d.ItemId,
                        ItemName = d.Item != null ? d.Item.ItemName : "",
                        MachineId = d.MachineId,
                        SpiralNo = d.SpiralNo,
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
        [HttpPost]
        [Route("{id}/UploadVideo")]
        public BusinessResult UploadVideo(int id, IFormFile videoData){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbMachine = _context.Machine.FirstOrDefault(d => d.Id == id);
                if (dbMachine == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                string contentPath = this.Environment.ContentRootPath;
        
                string path = Path.Combine(contentPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (videoData != null){
                    string fileName = Path.GetFileName(dbMachine.Id.ToString() + "_" + videoData.FileName);
                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        videoData.CopyTo(stream);
                    }   

                    dbMachine.StartVideoPath = fileName;
                    _context.SaveChanges();
                }

                result.RecordId = dbMachine.Id;
                result.Result = true;
            }
            catch (System.Exception ex)
            {
                result.Result = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("{id}/Video")]
        public FileStreamResult GetVideo(int id){
            try
            {
                var dbMachine = _context.Machine.FirstOrDefault(d => d.Id == id);
                if (dbMachine != null && !string.IsNullOrEmpty(dbMachine.StartVideoPath)){
                    string contentPath = this.Environment.ContentRootPath;
                    string path = Path.Combine(contentPath, "Uploads");

                    string fileName = Path.GetFileName(dbMachine.StartVideoPath);
                    string contentType = "";
                        new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);

                    FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Open, FileAccess.Read);
                    return new FileStreamResult(stream, contentType);
                }    
            }
            catch (System.Exception)
            {
                
            }

            return new FileStreamResult(null, "");
        }

        [Authorize(Policy = "Dealer")]
        [HttpPost]
        [Route("{id}/LoadSpiral")]
        public BusinessResult LoadSpiral(int id, LoadSpiralModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request.Headers);

            try
            {
                var dbObj = _context.Machine.FirstOrDefault(d => d.Id == id);
                if (dbObj == null){
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));
                }

                var dbSpiral = _context.MachineSpiral.FirstOrDefault(d => d.MachineId == dbObj.Id
                    && d.PosOrders == model.SpiralNo);
                if (dbSpiral == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                dbSpiral.ItemId = model.ItemId;
                dbSpiral.ItemCategoryId = model.ItemCategoryId;

                // spiral capacity check
                if (dbSpiral.ActiveQuantity + model.Quantity > dbSpiral.Capacity)
                    throw new Exception(_translator.Translate(Expressions.SpiralCapacityOverflowed, _userLanguage));

                dbSpiral.ActiveQuantity = (dbSpiral.ActiveQuantity ?? 0) + (model.Quantity ?? 0);

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
        [Route("{id}/DeliverProduct")]
        public BusinessResult DeliverProduct(int id, DeliverProductModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request.Headers);

            try
            {
                // check machine spiral stock state
                var dbSpiral = _context.MachineSpiral.FirstOrDefault(d => d.MachineId == id 
                    && d.PosOrders == model.SpiralNo);
                if (dbSpiral == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                if (dbSpiral.ItemId != model.ItemId)
                    throw new Exception(_translator.Translate(Expressions.DemandedItemIsDifferentThanExistingOne, _userLanguage));

                if (dbSpiral.ActiveQuantity <= 0)
                    throw new Exception(_translator.Translate(Expressions.SpiralIsOutOfStock, _userLanguage));

                // check employee credit state
                var dbEmployee = _context.Employee.FirstOrDefault(d => d.Id == model.EmployeeId);
                if (dbEmployee == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));
                
                var dbItem = _context.Item.FirstOrDefault(d => d.Id == model.ItemId);
                if (dbItem == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));
                
                var dbCredit = _context.EmployeeCredit.FirstOrDefault(d => d.EmployeeId == model.EmployeeId
                    && d.ItemCategoryId == dbItem.ItemCategoryId);
                if (dbCredit == null || dbCredit.ActiveCredit <= 0)
                    throw new Exception(_translator.Translate(Expressions.EmployeeIsOutOfCredit, _userLanguage));

                // start delivery operation
                //// create machine item consume
                _context.MachineItemConsume.Add(new MachineItemConsume{
                    ConsumedCount = 1,
                    //ConsumedDate = DateTime.Now,
                    EmployeeId = model.EmployeeId,
                    ItemId = model.ItemId,
                    ItemGroupId = dbItem.ItemGroupId,
                    MachineId = id,
                    SpiralNo = model.SpiralNo,
                });
                dbSpiral.ActiveQuantity -= 1;

                //// create employee credit consume
                _context.EmployeeCreditConsume.Add(new EmployeeCreditConsume{
                    ConsumedCredit = 1,
                    //ConsumedDate = DateTime.Now,
                    EmployeeId = model.EmployeeId,
                    ItemId = model.ItemId,
                    ItemGroupId = dbItem.ItemGroupId,
                    ItemCategoryId = dbItem.ItemCategoryId,
                });
                dbCredit.ActiveCredit -= 1;

                _context.SaveChanges();
                result.Result = true;
            }
            catch (System.Exception ex)
            {
                result.Result = false;
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
