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
using ClosedXML;
using ClosedXML.Excel;

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
                        IsInFault = d.IsInFault,
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
                        IsInFault = d.IsInFault,
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
                            IsInFault = d.IsInFault,
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
        [Route("{id}/MachineSpiralContents")]
        public IEnumerable<MachineSpiralModel> GetSpiralContentsById(int id){
            MachineSpiralModel[] data = new MachineSpiralModel[0];

            try
            {
                var dbMachine = _context.Machine.FirstOrDefault(d => d.Id == id);
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
                            IsInFault = d.IsInFault,
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

            if (filter.PlantId == null || filter.PlantId.Length == 0)
                return data;

            try
            {
                data = _context.MachineItemConsume
                    .Where(d =>
                        (filter == null || filter.MachineId == null || filter.MachineId.Length == 0 || filter.MachineId.Contains(d.MachineId ?? 0))
                        &&
                        (filter == null || filter.PlantId == null || filter.PlantId.Length == 0 || 
                            (d.Machine != null && filter.PlantId.Contains(d.Machine.PlantId ?? 0)) || (d.Warehouse != null && filter.PlantId.Contains(d.Warehouse.PlantId ?? 0)))
                        &&
                        (filter == null || filter.StartDate == null || (filter.StartDate <= d.ConsumedDate && filter.EndDate >= d.ConsumedDate))
                        &&
                        (filter == null || filter.CategoryId == null || filter.CategoryId.Length == 0 || filter.CategoryId.Contains(d.Item.ItemCategoryId ?? 0))
                        &&
                        (filter == null || filter.GroupId == null || filter.GroupId.Length == 0 || filter.GroupId.Contains(d.Item.ItemGroupId ?? 0))
                        &&
                        (filter == null || filter.ItemId == null || filter.ItemId.Length == 0 || filter.ItemId.Contains(d.Item.Id))
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
                        PlantId = d.Machine.PlantId,
                        ConsumedDate = d.ConsumedDate.Value,
                        SpiralNo = d.SpiralNo,
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
        [Route("ExcelConsumeReport")]
        public IActionResult ExcelConsumeReport(MachineConsumeFilter filter){
            try
            {
                

            #region PREPARE DATA
            MachineConsumeAbs[] data = new MachineConsumeAbs[0];
           
            try
            {
                data = _context.MachineItemConsume
                    .Where(d =>
                        (filter == null || filter.MachineId == null || filter.MachineId.Length == 0 || filter.MachineId.Contains(d.MachineId ?? 0))
                        &&
                        (filter == null || filter.PlantId == null || filter.PlantId.Length == 0 || 
                            (d.Machine != null && filter.PlantId.Contains(d.Machine.PlantId ?? 0)) || (d.Warehouse != null && filter.PlantId.Contains(d.Warehouse.PlantId ?? 0)))
                        &&
                        (filter == null || filter.StartDate == null || (filter.StartDate <= d.ConsumedDate && filter.EndDate >= d.ConsumedDate))
                        &&
                        (filter == null || filter.CategoryId == null || filter.CategoryId.Length == 0 || filter.CategoryId.Contains(d.Item.ItemCategoryId ?? 0))
                        &&
                        (filter == null || filter.GroupId == null || filter.GroupId.Length == 0 || filter.GroupId.Contains(d.Item.ItemGroupId ?? 0))
                        &&
                        (filter == null || filter.ItemId == null || filter.ItemId.Length == 0 || filter.ItemId.Contains(d.Item.Id))
                    )
                    .GroupBy(d => new {
                        MachineCode = d.Machine != null ? d.Machine.MachineCode : "",
                        MachineName = d.Machine != null ? d.Machine.MachineName : "",
                        ItemCode = d.Item.ItemCode,
                        ItemName = d.Item.ItemName,
                        ItemCategoryCode = d.Item.ItemCategory.ItemCategoryCode,
                        ItemCategoryName = d.Item.ItemCategory.ItemCategoryName,
                        WarehouseCode = d.Warehouse != null ? d.Warehouse.WarehouseCode : "",
                        WarehouseName = d.Warehouse != null ? d.Warehouse.WarehouseName : "",
                        EmployeeCode = d.Employee.EmployeeCode,
                        EmployeeName = d.Employee.EmployeeName,
                        MachineId = d.MachineId,
                        ItemId = d.ItemId,
                        EmployeeId = d.EmployeeId,
                        PlantId = d.Machine.PlantId,
                        ConsumedDate = d.ConsumedDate.Value,
                        SpiralNo = d.SpiralNo,
                    })
                    .Select(d => new MachineConsumeSummary{
                        MachineId = d.Key.MachineId,
                        EmployeeId = d.Key.EmployeeId,
                        PlantId = d.Key.PlantId,
                        ItemId = d.Key.ItemId,
                        MachineCode = d.Key.MachineCode,
                        MachineName = d.Key.MachineName,
                        WarehouseCode = d.Key.WarehouseCode,
                        WarehouseName = d.Key.WarehouseName,
                        EmployeeCode = d.Key.EmployeeCode,
                        EmployeeName = d.Key.EmployeeName,
                        ItemCode = d.Key.ItemCode,
                        ItemName = d.Key.ItemName,
                        ItemCategoryCode = d.Key.ItemCategoryCode,
                        ItemCategoryName = d.Key.ItemCategoryName,
                        ConsumedDate = d.Key.ConsumedDate,
                        SpiralNo = d.Key.SpiralNo,
                        TotalConsumed = d.Sum(m => m.ConsumedCount),
                    })
                    .OrderBy(d => d.ConsumedDate)
                    .ToList()
                    .Select(d => new MachineConsumeAbs {
                        ConsumedDate = string.Format("{0:dd.MM.yyyy}", d.ConsumedDate),
                        ConsumedTime = string.Format("{0:HH:mm}", d.ConsumedDate),
                        EmployeeName = d.EmployeeName,
                        MachineName = d.MachineName,
                        WarehouseName = d.WarehouseName,
                        ItemCategoryName = d.ItemCategoryName,
                        ItemName = d.ItemName,
                        SpiralNo = d.SpiralNo,
                        TotalConsumed = d.TotalConsumed,
                    }).ToArray();

                     if (filter.PlantId == null || filter.PlantId.Length == 0)
                        data = new MachineConsumeAbs[0];
            }
            catch
            {
                
            }
            #endregion

            #region PREPARE EXCEL FILE
            byte[] excelFile = new byte[0];

            using (var workbook = new XLWorkbook()) {
                var worksheet = workbook.Worksheets.Add("Tüketim Raporu");

                worksheet.Cell(1,1).Value = "Tarih";
                worksheet.Cell(1,2).Value = "Saat";
                worksheet.Cell(1,3).Value = "Personel";
                worksheet.Cell(1,4).Value = "Makine";
                worksheet.Cell(1,5).Value = "Kategori";
                worksheet.Cell(1,6).Value = "Stok";
                worksheet.Cell(1,7).Value = "Spiral No";
                worksheet.Cell(1,8).Value = "Miktar";

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
        [HttpPost]
        public BusinessResult Post(MachineModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                if ((model.PlantId ?? 0) <= 0){
                    throw new Exception(_translator.Translate(Expressions.PlantDoesntExists, _userLanguage));
                }

                var dbObj = _context.Machine.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Machine();
                    _context.Machine.Add(dbObj);
                }

                if (_context.Machine.Any(d => d.MachineCode == model.MachineCode 
                    && d.PlantId == model.PlantId && d.Id != model.Id))
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

                var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == dbObj.PlantId);
                dbPlant.LastUpdateDate = DateTime.Now;

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
                    
                    var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == dbMachine.PlantId);
                    dbPlant.LastUpdateDate = DateTime.Now;

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
        [HttpGet]
        [Route("{id}/EmptySpiral/{spiralId}")]
        public BusinessResult EmptySpiral(int id, int spiralId){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.Machine.FirstOrDefault(d => d.Id == id);
                if (dbObj == null){
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));
                }

                var dbSpiral = _context.MachineSpiral.FirstOrDefault(d => d.PosOrders == spiralId && d.MachineId == id);
                if (dbSpiral == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == dbObj.PlantId);
                dbPlant.LastUpdateDate = DateTime.Now;

                dbSpiral.ItemId = null;
                dbSpiral.ItemCategoryId = null;
                dbSpiral.ActiveQuantity = 0;

                _context.SaveChanges();
                result.Result=true;
                result.RecordId = dbSpiral.Id;
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
                if ((dbSpiral.ActiveQuantity ?? 0) + (model.Quantity ?? 0) > dbSpiral.Capacity)
                    throw new Exception(_translator.Translate(Expressions.SpiralCapacityOverflowed, _userLanguage));

                dbSpiral.ActiveQuantity = (dbSpiral.ActiveQuantity ?? 0) + (model.Quantity ?? 0);

                var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == dbObj.PlantId);
                dbPlant.LastUpdateDate = DateTime.Now;

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
        [Route("{id}/FullAllSpirals")]
        public BusinessResult FullAllSpirals(int id){
            ResolveHeaders(Request);
            BusinessResult result = new BusinessResult();

            try
            {
                var dbMachine = _context.Machine.FirstOrDefault(d => d.Id == id);
                if (dbMachine == null)
                    throw new Exception(_translator.Translate(Expressions.MachineNotFound, _userLanguage));

                var spirals = _context.MachineSpiral.Where(d => d.MachineId == id).ToArray();
                foreach (MachineSpiral item in spirals)
                {
                    // if capacity is infinite then go on with next spiral
                    if ((item.Capacity ?? 0) == 0)
                        continue;

                    // if spiral is disabled then go on with next spiral
                    if (!(item.IsEnabled ?? true))
                        continue;

                    // look for active item of current spiral
                    var properItemId = item.ItemId;

                    // search last consume history of current spiral
                    if (properItemId == null)
                    {
                        var lastConsumedItemId = _context.MachineItemConsume
                            .Where(d => d.MachineId == id && d.SpiralNo == item.PosOrders && d.ItemId != null)
                            .OrderByDescending(d => d.ConsumedDate)
                            .Select(d => d.ItemId)
                            .FirstOrDefault();
                        if (lastConsumedItemId != null)
                            properItemId = lastConsumedItemId;
                    }

                    // if proper item is found then load fully current spiral
                    if (properItemId != null){
                        var remainingQuantity = item.Capacity - item.ActiveQuantity;
                        if (remainingQuantity > 0){
                            var dbItem = _context.Item.FirstOrDefault(d => d.Id == properItemId);
                            if (dbItem != null){
                                item.ActiveQuantity = item.Capacity;
                                item.ItemId = properItemId;
                                item.ItemCategoryId = dbItem.ItemCategoryId;
                                item.ItemGroupId = dbItem.ItemGroupId;
                            }
                        }
                    }
                }

                var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == dbMachine.PlantId);
                dbPlant.LastUpdateDate = DateTime.Now;

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
                    (d.ItemId == null || d.ItemId == dbItem.Id) && (d.ItemGroupId == null || d.ItemGroupId == dbItem.ItemGroupId));
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

                var dbMachine = _context.Machine.FirstOrDefault(d => d.Id == id);
                var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == dbMachine.PlantId);
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
