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
    public class ItemCategoryController : MgControllerBase
    {
        public ItemCategoryController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        public IEnumerable<ItemCategoryModel> Get()
        {
            ItemCategoryModel[] data = new ItemCategoryModel[0];
            try
            {
                data = _context.ItemCategory.Select(d => new ItemCategoryModel{
                        Id = d.Id,
                        ControlTimeType = d.ControlTimeType,
                        CreatedDate = d.CreatedDate,
                        IsActive = d.IsActive,
                        ItemCategoryCode = d.ItemCategoryCode,
                        ItemCategoryName = d.ItemCategoryName,
                        ItemChangeTime = d.ItemChangeTime,
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
        public ItemCategoryModel Get(int id)
        {
            ItemCategoryModel data = new ItemCategoryModel();
            try
            {
                data = _context.ItemCategory.Where(d => d.Id == id).Select(d => new ItemCategoryModel{
                        Id = d.Id,
                        ControlTimeType = d.ControlTimeType,
                        CreatedDate = d.CreatedDate,
                        IsActive = d.IsActive,
                        ItemCategoryCode = d.ItemCategoryCode,
                        ItemCategoryName = d.ItemCategoryName,
                        ItemChangeTime = d.ItemChangeTime,
                        ViewOrder = d.ViewOrder,
                        CategoryImage = d.CategoryImage
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}/Groups")]
        public IEnumerable<ItemGroupModel> GetGroups(int id)
        {
            ItemGroupModel[] data = new ItemGroupModel[0];
            
            try
            {
                data = _context.ItemGroup.Where(d => d.ItemCategoryId == id).Select(d => new ItemGroupModel{
                        Id = d.Id,
                        ItemGroupCode = d.ItemGroupCode,
                        ItemGroupName = d.ItemGroupName,
                    }).OrderBy(d => d.ItemGroupCode).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}/Items")]
        public IEnumerable<ItemModel> GetItems(int id)
        {
            ItemModel[] data = new ItemModel[0];
            try
            {
                data = _context.Item.Where(d => d.ItemCategoryId == id).Select(d => new ItemModel{
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
                    }).OrderBy(d => d.ItemCode).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "Dealer")]
        [HttpPost]
        public BusinessResult Post(ItemCategoryModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request.Headers);

            try
            {
                var dbObj = _context.ItemCategory.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new ItemCategory();
                    _context.ItemCategory.Add(dbObj);
                }

                if (_context.ItemCategory.Any(d => d.ItemCategoryCode == model.ItemCategoryCode && d.Id != model.Id))
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

        [Authorize(Policy = "Dealer")]
        [HttpDelete]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request.Headers);

            try
            {
                var dbObj = _context.ItemCategory.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                _context.ItemCategory.Remove(dbObj);

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
