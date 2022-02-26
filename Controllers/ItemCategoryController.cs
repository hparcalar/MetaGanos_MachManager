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
                    }).FirstOrDefault();
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
