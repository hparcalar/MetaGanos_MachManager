using System;
using System.Collections.Generic;
using System.Collections;
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

namespace MachManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class ItemGroupController : MgControllerBase
    {
        public ItemGroupController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        public IEnumerable<ItemGroupModel> Get()
        {
            ResolveHeaders(Request);
            ItemGroupModel[] data = new ItemGroupModel[0];
            try
            {
                int[] plants = null;
                if (_isDealer)
                    plants = _context.Plant.Where(d => d.DealerId == _appUserId).Select(d => d.Id).ToArray();
                else if (_isFactoryOfficer)
                    plants = new int[]{ _context.Officer.Where(d => d.Id == _appUserId).Select(d => d.PlantId).First() };

                data = _context.ItemGroup
                    .Where(d => plants == null || plants.Length == 0 ||
                        (plants != null && d.ItemCategory != null && plants.Contains(d.ItemCategory.PlantId ?? 0)))
                    .Select(d => new ItemGroupModel{
                        Id = d.Id,
                        CreatedDate = d.CreatedDate,
                        IsActive = d.IsActive,
                        ItemCategoryCode = d.ItemCategory != null ? d.ItemCategory.ItemCategoryCode : "",
                        ItemCategoryName = d.ItemCategory != null ? d.ItemCategory.ItemCategoryName : "",
                        ItemCategoryId = d.ItemCategoryId,
                        ItemGroupCode = d.ItemGroupCode,
                        ItemGroupName = d.ItemGroupName,
                        PlantCode = d.ItemCategory != null && d.ItemCategory.Plant != null ?
                            d.ItemCategory.Plant.PlantCode : "",
                        PlantName = d.ItemCategory != null && d.ItemCategory.Plant != null ?
                            d.ItemCategory.Plant.PlantName : "",
                        ViewOrder = d.ViewOrder,
                    }).OrderBy(d => d.ItemCategoryCode).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public ItemGroupModel Get(int id)
        {
            ItemGroupModel data = new ItemGroupModel();
            try
            {
                data = _context.ItemGroup.Where(d => d.Id == id).Select(d => new ItemGroupModel{
                        Id = d.Id,
                        CreatedDate = d.CreatedDate,
                        IsActive = d.IsActive,
                        ItemCategoryCode = d.ItemCategory != null ? d.ItemCategory.ItemCategoryCode : "",
                        ItemCategoryName = d.ItemCategory != null ? d.ItemCategory.ItemCategoryName : "",
                        ItemCategoryId = d.ItemCategoryId,
                        ItemGroupCode = d.ItemGroupCode,
                        ItemGroupName = d.ItemGroupName,
                        GroupImage = d.GroupImage,
                        ViewOrder = d.ViewOrder,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}/Items")]
        [Authorize("Machine")]
        public ItemModel[] GetItems(int id){
            ItemModel[] data = new ItemModel[0];

            try
            {
                using (DefinitionListsBO bObj = new DefinitionListsBO(this._context)){
                    data = bObj.GetItems(groups: new int[]{ id });
                }
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }

        [Authorize(Policy = "FactoryOfficer")]
        [HttpPost]
        public BusinessResult Post(ItemGroupModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.ItemGroup.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new ItemGroup();
                    _context.ItemGroup.Add(dbObj);
                }

                if (_context.ItemGroup.Any(d => d.ItemGroupCode == model.ItemGroupCode
                    && d.ItemCategoryId == model.ItemCategoryId && d.Id != model.Id))
                    throw new Exception(_translator.Translate(Expressions.SameCodeExists, _userLanguage));

                model.MapTo(dbObj);

                if (model.ItemCategoryId > 0){
                    var dbCat = _context.ItemCategory.FirstOrDefault(d => d.Id == model.ItemCategoryId);
                    var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == dbCat.PlantId);
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

        [HttpGet]
        [Route("{id}/ItemsNonWr")]
        public IEnumerable<ItemModel> GetItemsNonWr(int id)
        {
            ItemModel[] data = new ItemModel[0];
            try
            {
                List<ItemModel> properData = new List<ItemModel>();

                var allData = _context.Item.Where(d => d.ItemGroupId == id).Select(d => new ItemModel{
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
                        ItemImage = d.ItemImage,
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
                    }).OrderBy(d => d.ItemCode).ToArray();

                foreach (var item in allData)
                {
                    if (!_context.WarehouseHotSalesCategory.Any(d => d.ItemId == item.Id))
                        properData.Add(item);
                }

                data = properData.ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("ItemsForWarehouse/{groupId}/{warehouseId}")]
        public IEnumerable<ItemModel> GetItemsForWarehouse(int groupId, int warehouseId)
        {
            ResolveHeaders(Request);
            ItemModel[] data = new ItemModel[0];
            try
            {
                var hotSalesItems = _context.WarehouseHotSalesCategory.Where(d => d.WarehouseId == warehouseId
                        && d.ItemGroupId == groupId
                        && d.ItemId != null
                    )
                    .Select(d => d.ItemId).Distinct().ToArray();

                data = _context.Item
                    .Where(d => d.ItemGroupId == groupId
                        &&
                        (
                            hotSalesItems.Length == 0 || hotSalesItems.Contains(d.Id)
                        )
                    )
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
                        ItemImage = d.ItemImage,
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
                    }).OrderBy(d => d.ItemCode).ToArray();
            }
            catch
            {

            }

            return data;
        }


        [Authorize(Policy = "FactoryOfficer")]
        [Route("{id}")]
        [HttpDelete("{id}")]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.ItemGroup.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                if (dbObj.ItemCategoryId > 0){
                    var dbCat = _context.ItemCategory.FirstOrDefault(d => d.Id == dbObj.ItemCategoryId);
                    var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == dbCat.PlantId);
                    dbPlant.LastUpdateDate = DateTime.Now;
                }

                if (_context.EmployeeCreditConsume.Any(d => d.ItemGroupId == id))
                    throw new Exception("Bu gruba ilişkin tüketim kayıtları olduğu için silinemez.");

                if (_context.EmployeeCredit.Any(d => d.ItemGroupId == id))
                    throw new Exception("Bu gruba ilişkin verilen krediler olduğu için silinemez.");

                _context.ItemGroup.Remove(dbObj);

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
