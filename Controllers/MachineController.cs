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
using MachManager.Business;
using MachManager.Models.ReportContainers;

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

        [Authorize(Policy = "FactoryOfficer")]
        [HttpGet]
        public IEnumerable<MachineModel> Get()
        {
            ResolveHeaders(Request);
            MachineModel[] data = new MachineModel[0];
            try
            {
                int[] plants = null;
                if (_isDealer)
                    plants = _context.Plant.Where(d => d.DealerId == _appUserId).Select(d => d.Id).ToArray();
                else if (_isFactoryOfficer)
                    plants = new int[]{ _context.Officer.Where(d => d.Id == _appUserId).Select(d => d.PlantId).First() };

                using (DefinitionListsBO bObj = new DefinitionListsBO(this._context)){
                    data = bObj.GetMachines(plants);
                }
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "FactoryOfficer")]
        [HttpGet]
        [Route("Count")]
        public int GetMachineCount(){
            ResolveHeaders(Request);
            int dataCount = 0;

            try
            {
                int[] plants = null;
                if (_isDealer)
                    plants = _context.Plant.Where(d => d.DealerId == _appUserId).Select(d => d.Id).ToArray();
                else if (_isFactoryOfficer)
                    plants = new int[]{ _context.Officer.Where(d => d.Id == _appUserId).Select(d => d.PlantId).First() };

                using (DefinitionListsBO bObj = new DefinitionListsBO(this._context)){
                    dataCount = bObj.GetMachineCount(plants);
                }
            }
            catch (System.Exception)
            {
                
            }

            return dataCount;
        }


        [HttpGet]
        [Route("{id}")]
        public MachineModel Get(int id)
        {
            ResolveHeaders(Request);
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
                        SpiralStartIndex = d.SpiralStartIndex,
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
                        IsEnabled = d.IsEnabled,
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
            ResolveHeaders(Request);
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
                        IsEnabled = d.IsEnabled,
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
        [Route("{machineCode}/SpiralContents")]
        public IEnumerable<MachineSpiralModel> GetSpiralContents(string machineCode){
            MachineSpiralModel[] data = new MachineSpiralModel[0];

            try
            {
                var dbMachine = _context.Machine.FirstOrDefault(d => d.MachineCode == machineCode);
                if (dbMachine != null){
                    data = _context.MachineSpiral.Where(d => d.MachineId == dbMachine.Id)
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
                            IsEnabled = d.IsEnabled,
                            PosOrders = d.PosOrders,
                            Capacity = d.Capacity,
                            PosX = d.PosX,
                            PosY = d.PosY,
                        }).ToArray();
                }
            }
            catch (System.Exception)
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

        [AllowAnonymous]
        [HttpPost]
        [Route("ConsumeReport")]
        public IEnumerable<MachineConsumeSummary> GetConsumeReport(MachineConsumeFilter filter){
            MachineConsumeSummary[] data = new MachineConsumeSummary[0];

            try
            {
                data = _context.MachineItemConsume
                    .Where(d =>
                        (filter == null || filter.Machines == null || filter.Machines.Length == 0 || filter.Machines.Contains(d.MachineId ?? 0))
                        &&
                        (filter == null || filter.Plants == null || filter.Plants.Length == 0 || (d.Machine != null && filter.Plants.Contains(d.Machine.PlantId ?? 0)))
                        &&
                        (filter == null || filter.StartDate == null || (filter.StartDate <= d.ConsumedDate && filter.EndDate >= d.ConsumedDate))
                    )
                    .GroupBy(d => new {
                        MachineCode = d.Machine.MachineCode,
                        MachineName = d.Machine.MachineName,
                        ItemCode = d.Item.ItemCode,
                        ItemName = d.Item.ItemName,
                        ItemCategoryCode = d.Item.ItemCategory.ItemCategoryCode,
                        ItemCategoryName = d.Item.ItemCategory.ItemCategoryName,
                        EmployeeCode = d.Employee.EmployeeCode,
                        EmployeeName = d.Employee.EmployeeName,
                        MachineId = d.MachineId,
                        ItemId = d.ItemId,
                        EmployeeId = d.EmployeeId,
                        PlantId = d.Machine.PlantId,
                    })
                    .Select(d => new MachineConsumeSummary{
                        MachineId = d.Key.MachineId,
                        EmployeeId = d.Key.EmployeeId,
                        PlantId = d.Key.PlantId,
                        ItemId = d.Key.ItemId,
                        MachineCode = d.Key.MachineCode,
                        MachineName = d.Key.MachineName,
                        EmployeeCode = d.Key.EmployeeCode,
                        EmployeeName = d.Key.EmployeeName,
                        ItemCode = d.Key.ItemCode,
                        ItemName = d.Key.ItemName,
                        ItemCategoryCode = d.Key.ItemCategoryCode,
                        ItemCategoryName = d.Key.ItemCategoryName,
                        TotalConsumed = d.Sum(m => m.ConsumedCount),
                    }).ToArray();
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }

        [Authorize(Policy = "FactoryOfficer")]
        [HttpPost]
        public BusinessResult Post(MachineModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

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

        [Authorize(Policy = "FactoryOfficer")]
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

        [Authorize(Policy = "FactoryOfficer")]
        [HttpPost]
        [Route("{id}/LoadSpiral")]
        public BusinessResult LoadSpiral(int id, LoadSpiralModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

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
            ResolveHeaders(Request);

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
                
                var dbCreditList = _context.EmployeeCredit.Where(d => d.EmployeeId == model.EmployeeId
                    && d.ItemCategoryId == dbItem.ItemCategoryId).ToArray();
                var dbCredit = dbCreditList.FirstOrDefault(d => 
                    (d.ItemGroupId == null || d.ItemGroupId == dbItem.ItemGroupId));
                if (dbCredit == null || dbCredit.RangeCredit <= 0)
                    throw new Exception(_translator.Translate(Expressions.EmployeeIsOutOfCredit, _userLanguage));

                // start delivery operation
                //// create machine item consume
                _context.MachineItemConsume.Add(new MachineItemConsume{
                    ConsumedCount = 1,
                    ConsumedDate = model.DeliverDate,
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
                    ConsumedDate = model.DeliverDate,
                    EmployeeId = model.EmployeeId,
                    ItemId = model.ItemId,
                    ItemGroupId = dbItem.ItemGroupId,
                    ItemCategoryId = dbItem.ItemCategoryId,
                });

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
        [HttpDelete]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

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
