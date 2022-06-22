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
using MachManager.Business;
using MachManager.Models.Parameters;
using ClosedXML;
using ClosedXML.Excel;

namespace MachManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class ItemController : MgControllerBase
    {
        public ItemController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        public IEnumerable<ItemModel> Get()
        {
            ResolveHeaders(Request);
            ItemModel[] data = new ItemModel[0];
            try
            {
                int[] plants = null;
                if (_isDealer)
                    plants = _context.Plant.Where(d => d.DealerId == _appUserId).Select(d => d.Id).ToArray();
                else if (_isFactoryOfficer)
                    plants = new int[]{ _context.Officer.Where(d => d.Id == _appUserId).Select(d => d.PlantId).First() };

                using (DefinitionListsBO bObj = new DefinitionListsBO(this._context)){
                    data = bObj.GetItems(plants);
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
        public int GetItemCount(){
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
                    dataCount = bObj.GetItemCount(plants);
                }
            }
            catch (System.Exception)
            {
                
            }

            return dataCount;
        }

        [HttpGet]
        [Route("{id}")]
        public ItemModel Get(int id)
        {
            ItemModel data = new ItemModel();
            try
            {
                data = _context.Item.Where(d => d.Id == id).Select(d => new ItemModel{
                        Id = d.Id,
                        AlternatingCode1 = d.AlternatingCode1,
                        AlternatingCode2 = d.AlternatingCode2,
                        Barcode1 = d.Barcode1,
                        Barcode2 = d.Barcode2,
                        CreatedDate = d.CreatedDate,
                        CriticalMax = d.CriticalMax,
                        CriticalMin = d.CriticalMin,
                        Explanation = d.Explanation,
                        IsActive = d.IsActive,
                        ItemCategoryCode = d.ItemCategory != null ? d.ItemCategory.ItemCategoryCode : "",
                        ItemCategoryId = d.ItemCategoryId,
                        ItemCategoryName = d.ItemCategory != null ? d.ItemCategory.ItemCategoryName : "",
                        ItemCode = d.ItemCode,
                        ItemGroupCode = d.ItemGroup != null ? d.ItemGroup.ItemGroupCode : "",
                        ItemGroupId = d.ItemGroupId,
                        ItemGroupName = d.ItemGroup != null ? d.ItemGroup.ItemGroupName : "",
                        ItemName = d.ItemName,
                        Price1 = d.Price1,
                        Price2 = d.Price2,
                        UnitTypeCode = d.UnitType != null ? d.UnitType.UnitTypeCode : "",
                        UnitTypeId = d.UnitTypeId,
                        UnitTypeName = d.UnitType != null ? d.UnitType.UnitTypeName : "",
                        ViewOrder = d.ViewOrder
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("ForMachine/{machineCode}")]
        public IEnumerable<ItemModel> GetItemsForMachine(string machineCode){
            ItemModel[] data = new ItemModel[0];

            try
            {
                var dbMachine = _context.Machine.FirstOrDefault(d => d.MachineCode == machineCode);
                if (dbMachine != null){
                    data = _context.Item.Where(d => d.IsActive == true)
                        .Select(d => new ItemModel{
                            Id = d.Id,
                            AlternatingCode1 = d.AlternatingCode1,
                            AlternatingCode2 = d.AlternatingCode2,
                            Barcode1 = d.Barcode1,
                            Barcode2 = d.Barcode2,
                            CreatedDate = d.CreatedDate,
                            CriticalMax = d.CriticalMax,
                            CriticalMin = d.CriticalMin,
                            Explanation = d.Explanation,
                            IsActive = d.IsActive,
                            ItemCategoryCode = d.ItemCategory != null ? d.ItemCategory.ItemCategoryCode : "",
                            ItemCategoryId = d.ItemCategoryId,
                            ItemCategoryName = d.ItemCategory != null ? d.ItemCategory.ItemCategoryName : "",
                            ItemCode = d.ItemCode,
                            ItemGroupCode = d.ItemGroup != null ? d.ItemGroup.ItemGroupCode : "",
                            ItemGroupId = d.ItemGroupId,
                            ItemGroupName = d.ItemGroup != null ? d.ItemGroup.ItemGroupName : "",
                            ItemName = d.ItemName,
                            Price1 = d.Price1,
                            Price2 = d.Price2,
                            UnitTypeCode = d.UnitType != null ? d.UnitType.UnitTypeCode : "",
                            UnitTypeId = d.UnitTypeId,
                            UnitTypeName = d.UnitType != null ? d.UnitType.UnitTypeName : "",
                            ViewOrder = d.ViewOrder
                        }).ToArray();
                }
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }

        [Authorize(Policy = "FactoryOfficer")]
        [HttpPost]
        public BusinessResult Post(ItemModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.Item.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Item();
                    _context.Item.Add(dbObj);
                }

                var dbCategory = _context.ItemCategory.FirstOrDefault(d => d.Id == model.ItemCategoryId);
                if (dbCategory == null)
                    throw new Exception(_translator.Translate(Expressions.ItemCategoryNotFound, _userLanguage));

                if (_context.Item.Any(d => d.ItemCode == model.ItemCode 
                    && d.ItemCategory.PlantId == dbCategory.PlantId && d.Id != model.Id))
                    throw new Exception(_translator.Translate(Expressions.SameCodeExists, _userLanguage));

                model.MapTo(dbObj);

                if (dbCategory != null){
                    var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == dbCategory.PlantId);
                    dbPlant.LastUpdateDate = DateTime.Now;
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
        [Route("Upload")]
        public BusinessResult UploadData([FromForm]UploadItemModel model){
            ResolveHeaders(Request);
            BusinessResult result = new BusinessResult();

            try
            {
                var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == model.PlantId);
                if (dbPlant == null)
                    throw new Exception(_translator.Translate(Expressions.PlantDoesntExists, _userLanguage));

                if (model.File == null || model.File.Length == 0)
                    throw new Exception(_translator.Translate(Expressions.FileNotFound, _userLanguage));

                List<Item> newItemList = new List<Item>();
                List<ItemCategory> newCategoryList = new List<ItemCategory>();
                List<ItemGroup> newGroupList = new List<ItemGroup>();
                List<UnitType> newUnitList = new List<UnitType>();

                int insertedCount = 0;
                int updatedCount = 0;

                using (var wb = new XLWorkbook(model.File.OpenReadStream()))
                {
                    var ws = wb.Worksheet(1);
                    var sheetRows = ws.Rows();
                    var rowIndex = 0;
                    foreach (var row in sheetRows)
                    {
                        if (rowIndex == 0){
                            rowIndex++;
                            continue;
                        }

                        var clCode = row.Cell(1);
                        var clName = row.Cell(2);
                        var clCat = row.Cell(3);
                        var clGrp = row.Cell(4);
                        var clBar = row.Cell(5);
                        var clUnit = row.Cell(6);

                        string dtCode = clCode.GetValue<string>();
                        string dtName = clName.GetValue<string>();
                        string dtCat = clCat.GetValue<string>();
                        string dtGrp = clGrp.GetValue<string>();
                        string dtBar = clBar.GetValue<string>();
                        string dtUnit = clUnit.GetValue<string>();

                        if (!string.IsNullOrEmpty(dtCode)){
                            if (!newItemList.Any(d => d.ItemCode == dtCode)){
                                bool isValid = true;
                                if (string.IsNullOrEmpty(dtName))
                                    isValid = false;
                                if (string.IsNullOrEmpty(dtCat))
                                    isValid = false;
                                if (string.IsNullOrEmpty(dtGrp))
                                    isValid = false;

                                if (isValid){
                                    // check category
                                    var dbCat = _context.ItemCategory.FirstOrDefault(d => (d.ItemCategoryCode == dtCat
                                        || d.ItemCategoryName == dtCat) && d.PlantId == model.PlantId);
                                    if (dbCat == null)
                                        newCategoryList.FirstOrDefault(d => (d.ItemCategoryCode == dtCat 
                                            || d.ItemCategoryName == dtCat) && d.PlantId == model.PlantId);
                                    if (dbCat == null){
                                        dbCat = new ItemCategory{
                                            ItemCategoryCode = dtCat,
                                            ItemCategoryName = dtCat,
                                            CreatedDate = DateTime.Now,
                                            IsActive = true,
                                            PlantId = model.PlantId,
                                        };
                                        _context.ItemCategory.Add(dbCat);
                                        newCategoryList.Add(dbCat);
                                    }

                                    // check group
                                    var dbGrp = _context.ItemGroup.FirstOrDefault(d => (d.ItemGroupCode == dtGrp
                                        || d.ItemGroupName == dtGrp) && d.ItemCategory != null && d.ItemCategory.PlantId == model.PlantId);
                                    if (dbGrp == null)
                                        newGroupList.FirstOrDefault(d => (d.ItemGroupCode == dtGrp 
                                            || d.ItemGroupName == dtGrp) && d.ItemCategory.PlantId == model.PlantId);
                                    if (dbGrp == null){
                                        dbGrp = new ItemGroup{
                                            ItemGroupCode = dtGrp,
                                            ItemGroupName = dtGrp,
                                            CreatedDate = DateTime.Now,
                                            IsActive = true,
                                            ItemCategory = dbCat,
                                        };
                                        _context.ItemGroup.Add(dbGrp);
                                        newGroupList.Add(dbGrp);
                                    }

                                    // check unit type
                                    UnitType dbUnit = null;
                                    if (!string.IsNullOrEmpty(dtUnit)){
                                        dbUnit = _context.UnitType.FirstOrDefault(d => d.UnitTypeCode == dtUnit);
                                        if (dbUnit == null)
                                            newUnitList.FirstOrDefault(d => d.UnitTypeCode == dtUnit);
                                        if (dbUnit == null){
                                            dbUnit = new UnitType{
                                                UnitTypeCode = dtUnit,
                                                UnitTypeName = dtUnit,
                                                IsActive = true,
                                            };
                                            _context.UnitType.Add(dbUnit);
                                            newUnitList.Add(dbUnit);
                                        }
                                    }

                                    var dbItem = _context.Item.FirstOrDefault(d => d.ItemCode == dtCode
                                        && d.ItemCategory.PlantId == model.PlantId);
                                    if (dbItem == null)
                                        dbItem = newItemList.FirstOrDefault(d => d.ItemCode == dtCode
                                            && d.ItemCategory.PlantId == model.PlantId);
                                    if (dbItem == null){
                                        dbItem = new Item{
                                            Barcode1 = dtBar,
                                            CreatedDate = DateTime.Now,
                                            IsActive = true,
                                            ItemCategory = dbCat,
                                            ItemCode = dtCode,
                                            ItemGroup = dbGrp,
                                            ItemName = dtName,
                                            UnitType = dbUnit,
                                        };

                                        _context.Item.Add(dbItem);
                                        newItemList.Add(dbItem);
                                        insertedCount++;
                                    }
                                    else{
                                        dbItem.ItemName = dtName;
                                        dbItem.ItemCategory = dbCat;
                                        dbItem.ItemGroup = dbGrp;
                                        dbItem.UnitType = dbUnit;
                                        updatedCount++;
                                    }
                                }
                            }
                        }

                        rowIndex++;
                    }
                }

                _context.SaveChanges();
                result.Result = true;
                result.InfoMessage = insertedCount.ToString() +
                    " adet yeni kayıt sisteme transfer edildi ve " +
                    updatedCount.ToString() + " adet kayıt güncellendi.";
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
                var dbObj = _context.Item.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                if (dbObj.ItemCategoryId > 0){
                    var dbCat = _context.ItemCategory.FirstOrDefault(d => d.Id == dbObj.ItemCategoryId);
                    var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == dbCat.PlantId);
                    dbPlant.LastUpdateDate = DateTime.Now;
                }

                _context.Item.Remove(dbObj);

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
