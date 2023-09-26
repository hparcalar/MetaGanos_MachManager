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
using MachManager.Models.Constants;
using MachManager.Business;
using MachManager.Models.Parameters;
using MachManager.Models.ReportContainers;
using ClosedXML;
using ClosedXML.Excel;

namespace MachManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class ReturnController : MgControllerBase
    {
        public ReturnController(MetaGanosSchema context): base(context){ }

        [AllowAnonymous]
        [HttpPost]
        [Route("ReturnReport")]
        public IEnumerable<ReturnModel> GetReturnReport(MachineConsumeFilter filter){
            ReturnModel[] data = new ReturnModel[0];
            var plantId = 0;

            if (filter.PlantId == null || filter.PlantId.Length == 0)
                return data;
            else{
                plantId = filter.PlantId[0];
            }
            try
            {
                data = _context.Return
                    .Where(d =>
                        //(filter == null || plantId == d.PlantId)
                        (filter == null || filter.PlantId == null || filter.PlantId.Length == 0 || 
                            (d.Warehouse != null && filter.PlantId.Contains(d.Warehouse.PlantId ?? 0)))
                        &&
                        (filter == null || filter.StartDate == null || (filter.StartDate <= d.ReturnDate && filter.EndDate >= d.ReturnDate))
                        &&
                        (filter == null || filter.CategoryId == null || filter.CategoryId.Length == 0 || filter.CategoryId.Contains(d.Item.ItemCategoryId ?? 0))
                        &&
                        (filter == null || filter.GroupId == null || filter.GroupId.Length == 0 || filter.GroupId.Contains(d.Item.ItemGroupId ?? 0))
                        &&
                        (filter == null || filter.ItemId == null || filter.ItemId.Length == 0 || filter.ItemId.Contains(d.Item.Id))
                        &&
                        (filter == null || filter.EmployeeId == null || filter.EmployeeId.Length == 0 || filter.EmployeeId.Contains(d.Employee.Id))
                        &&
                        (filter == null || filter.DepartmentId == null || filter.DepartmentId.Length == 0 || filter.DepartmentId.Contains(d.Employee.DepartmentId ?? 0))
                    )
                    .GroupBy(d => new {
                        Id = d.Id,
                        ItemCode = d.Item.ItemCode,
                        ItemName = d.Item.ItemName,
                        ItemGroupId = d.Item.ItemGroupId,
                        ItemCategoryId = d.Item.ItemCategoryId,
                        ItemId = d.Item.Id,
                        ItemCategoryCode = d.Item.ItemCategory.ItemCategoryCode,
                        ItemCategoryName = d.Item.ItemCategory.ItemCategoryName,
                        EmployeeCode = d.Employee.EmployeeCode,
                        EmployeeName = d.Employee.EmployeeName,
                        DepartmentCode = d.Employee.Department != null ? d.Employee.Department.DepartmentCode : "",
                        DepartmentName = d.Employee.Department != null ? d.Employee.Department.DepartmentName : "",
                        EmployeeCardCode = d.Employee.EmployeeCard != null ? d.Employee.EmployeeCard.CardCode : "",
                        WarehouseCode = d.Warehouse != null ? d.Warehouse.WarehouseCode : "",
                        WarehouseName = d.Warehouse != null ? d.Warehouse.WarehouseName : "",
                        EmployeeId = d.EmployeeId,
                        PlantId = d.Plant.Id,
                        ReturnDate = d.ReturnDate.Value,
                    })
                    .Select(d => new ReturnModel{
                        Id = d.Key.Id,
                        EmployeeId = d.Key.EmployeeId,
                        PlantId = d.Key.PlantId,
                        ItemId = d.Key.ItemId,
                        ItemCategoryId = d.Key.ItemCategoryId,
                        ItemGroupId = d.Key.ItemGroupId,
                        EmployeeCode = d.Key.EmployeeCode,
                        EmployeeName = d.Key.EmployeeName,
                        ItemCode = d.Key.ItemCode,
                        ItemName = d.Key.ItemName,
                        ItemCategoryName = d.Key.ItemCategoryName,
                        WarehouseCode = d.Key.WarehouseCode,
                        WarehouseName = d.Key.WarehouseName,
                        DepartmentCode = d.Key.DepartmentCode,
                        EmployeeCardCode = d.Key.EmployeeCardCode,
                        DepartmentName = d.Key.DepartmentName,
                        ReturnDate = d.Key.ReturnDate,
                        Quantity = d.Sum(m => m.Quantity),
                    })
                    .OrderByDescending(d => d.ReturnDate)
                    .ToArray();

                var employees = data.Select(d => d.EmployeeId).Distinct().ToArray();

                /* data = data.Select(d => new ReturnModel{
                    Id = d.Id,
                    EmployeeId = d.EmployeeId,
                    PlantId = d.PlantId,
                    ItemCategoryId = d.ItemCategoryId,
                    ItemGroupId = d.ItemGroupId,
                    ItemId = d.ItemId,
                    EmployeeName = d.EmployeeName,
                    EmployeeCode = d.EmployeeCode,
                    ItemCode = d.ItemCode,
                    ItemName = d.ItemName,
                    ItemCategoryName = d.ItemCategoryName,
                    DepartmentCode = d.DepartmentCode,
                    DepartmentName = d.DepartmentName,
                    ReturnDate = d.ReturnDate,
                    Quantity = d.Quantity,
                }).ToArray(); */
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("{plantId}/ReturnItem")]
        public BusinessResult Post(int plantId, ReturnModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbWarehouse = _context.Warehouse.FirstOrDefault(d => d.WarehouseCode == model.WarehouseCode && d.PlantId == plantId);

                _context.Return.Add(new Return{
                    Quantity = model.Quantity ?? 1,
                    ReturnDate = DateTime.Now,
                    EmployeeId = model.EmployeeId,
                    ItemId = model.ItemId,
                    WarehouseId = dbWarehouse.Id,
                    PlantId = plantId
                });
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

        [AllowAnonymous]
        [HttpPost]
        [Route("ReturnReport/Wr")]
        public IEnumerable<ReturnModel> GetReturn(MachineConsumeFilter filter){
            ReturnModel[] data = new ReturnModel[0];

            if (string.IsNullOrEmpty(filter.PlantCode))
                return data;
                
            var dbPlant = _context.Plant.FirstOrDefault(d => d.PlantCode == filter.PlantCode);
            var dbWr = _context.Warehouse.FirstOrDefault(d => d.WarehouseCode == filter.WarehouseCode && d.PlantId == dbPlant.Id);

            try
            {
                data = _context.Return
                    .Where(d =>
                        (dbWr.Id == d.WarehouseId)
                        &&
                        (filter == null || filter.StartDate == null || (filter.StartDate <= d.ReturnDate && filter.EndDate >= d.ReturnDate))
                        &&
                        (filter == null || filter.CategoryId == null || filter.CategoryId.Length == 0 || filter.CategoryId.Contains(d.Item.ItemCategoryId ?? 0))
                        &&
                        (filter == null || filter.GroupId == null || filter.GroupId.Length == 0 || filter.GroupId.Contains(d.Item.ItemGroupId ?? 0))
                        &&
                        (filter == null || filter.ItemId == null || filter.ItemId.Length == 0 || filter.ItemId.Contains(d.Item.Id))
                        &&
                        (filter == null || filter.EmployeeId == null || filter.EmployeeId.Length == 0 || filter.EmployeeId.Contains(d.EmployeeId ?? 0))
                    )
                    .GroupBy(d => new {
                        ItemCode = d.Item.ItemCode,
                        ItemName = d.Item.ItemName,
                        ItemCategoryCode = d.Item.ItemCategory.ItemCategoryCode,
                        ItemCategoryName = d.Item.ItemCategory.ItemCategoryName,
                        EmployeeCode = d.Employee.EmployeeCode,
                        EmployeeName = d.Employee.EmployeeName,
                        WarehouseCode = d.Warehouse != null ? d.Warehouse.WarehouseCode : "",
                        WarehouseName = d.Warehouse != null ? d.Warehouse.WarehouseName : "",
                        ItemId = d.ItemId,
                        EmployeeId = d.EmployeeId,
                        ReturnDate = d.ReturnDate.Value,
                    })
                    .Select(d => new ReturnModel{
                        EmployeeId = d.Key.EmployeeId,
                        ItemId = d.Key.ItemId,
                        EmployeeCode = d.Key.EmployeeCode,
                        EmployeeName = d.Key.EmployeeName,
                        ItemCode = d.Key.ItemCode,
                        ItemName = d.Key.ItemName,
                        ItemCategoryName = d.Key.ItemCategoryName,
                        WarehouseCode = d.Key.WarehouseCode,
                        WarehouseName = d.Key.WarehouseName,
                        ReturnDate = d.Key.ReturnDate,
                        Quantity = d.Sum(m => m.Quantity),
                    })
                    .OrderByDescending(d => d.ReturnDate)
                    .ToArray();
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ExcelReturnReport")]
        public IActionResult ExcelReturnReport(MachineConsumeFilter filter){
            try
            {
                
            #region PREPARE DATA
            ItemReturnAbsExcel[] data = new ItemReturnAbsExcel[0];
            
            try
            {
                var rawData = _context.Return
                    .Where(d =>
                        (filter == null || filter.PlantId == null || filter.PlantId.Length == 0 || 
                            (d.Warehouse != null && filter.PlantId.Contains(d.Warehouse.PlantId ?? 0)))
                        &&
                        (filter == null || filter.StartDate == null || (filter.StartDate <= d.ReturnDate && filter.EndDate >= d.ReturnDate))
                        &&
                        (filter == null || filter.CategoryId == null || filter.CategoryId.Length == 0 || filter.CategoryId.Contains(d.Item.ItemCategoryId ?? 0))
                        &&
                        (filter == null || filter.GroupId == null || filter.GroupId.Length == 0 || filter.GroupId.Contains(d.Item.ItemGroupId ?? 0))
                        &&
                        (filter == null || filter.ItemId == null || filter.ItemId.Length == 0 || filter.ItemId.Contains(d.Item.Id))
                        &&
                        (filter == null || filter.EmployeeId == null || filter.EmployeeId.Length == 0 || filter.EmployeeId.Contains(d.Employee.Id))
                        &&
                        (filter == null || filter.DepartmentId == null || filter.DepartmentId.Length == 0 || filter.DepartmentId.Contains(d.Employee.DepartmentId ?? 0))
                    )
                    .GroupBy(d => new {
                        ItemId = d.ItemId,
                        ItemGroupId = d.Item.ItemGroupId,
                        ItemCategoryId = d.Item.ItemCategoryId,
                        ItemCode = d.Item.ItemCode,
                        ItemName = d.Item.ItemName,
                        ItemCategoryCode = d.Item.ItemCategory.ItemCategoryCode,
                        ItemCategoryName = d.Item.ItemCategory.ItemCategoryName,
                        WarehouseCode = d.Warehouse != null ? d.Warehouse.WarehouseCode : "",
                        WarehouseName = d.Warehouse != null ? d.Warehouse.WarehouseName : "",
                        EmployeeCode = d.Employee.EmployeeCode,
                        EmployeeName = d.Employee.EmployeeName,
                        DepartmentCode = d.Employee.Department != null ? d.Employee.Department.DepartmentCode : "",
                        DepartmentName = d.Employee.Department != null ? d.Employee.Department.DepartmentName : "",
                        EmployeeId = d.EmployeeId,
                        PlantId = d.PlantId,
                        ReturnDate = d.ReturnDate.Value,
                    })
                    .Select(d => new ReturnModel{
                        EmployeeId = d.Key.EmployeeId,
                        PlantId = d.Key.PlantId,
                        ItemId = d.Key.ItemId,
                        ItemGroupId = d.Key.ItemGroupId,
                        ItemCategoryId = d.Key.ItemCategoryId,
                        WarehouseCode = d.Key.WarehouseCode,
                        WarehouseName = d.Key.WarehouseName,
                        EmployeeCode = d.Key.EmployeeCode,
                        EmployeeName = d.Key.EmployeeName,
                        DepartmentCode = d.Key.DepartmentCode,
                        DepartmentName = d.Key.DepartmentName,
                        ItemCode = d.Key.ItemCode,
                        ItemName = d.Key.ItemName,
                        ItemCategoryName = d.Key.ItemCategoryName,
                        ReturnDate = d.Key.ReturnDate,
                        Quantity = d.Sum(m => m.Quantity),
                    })
                    .OrderByDescending(d => d.ReturnDate)
                    .ToList();

                data = rawData
                    .Select(d => new ItemReturnAbsExcel {
                            ReturnDate = string.Format("{0:dd.MM.yyyy}", d.ReturnDate),
                            ReturnTime = string.Format("{0:HH:mm}", d.ReturnDate),
                            EmployeeName = d.EmployeeName,
                            WarehouseName = d.WarehouseName,
                            DepartmentName = d.DepartmentName,
                            ItemCategoryName = d.ItemCategoryName,
                            ItemName = d.ItemName,
                            Quantity = d.Quantity,
                        }).ToArray();

                if (filter.PlantId == null || filter.PlantId.Length == 0)
                data = new ItemReturnAbsExcel[0];
            }
            catch
            {
                
            }
            #endregion

            #region PREPARE EXCEL FILE
            byte[] excelFile = new byte[0];

            using (var workbook = new XLWorkbook()) {
                var worksheet = workbook.Worksheets.Add("İade Raporu");

                worksheet.Cell(1,1).Value = "Tarih";
                worksheet.Cell(1,2).Value = "Saat";
                worksheet.Cell(1,3).Value = "Personel";
                worksheet.Cell(1,4).Value = "Depo";
                worksheet.Cell(1,5).Value = "Departman";
                worksheet.Cell(1,6).Value = "Kategori";
                worksheet.Cell(1,7).Value = "Stok";
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
    }
}
