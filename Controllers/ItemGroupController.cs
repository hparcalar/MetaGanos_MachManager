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
    public class ItemGroupController : MgControllerBase
    {
        public ItemGroupController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        public IEnumerable<ItemGroupModel> Get()
        {
            ItemGroupModel[] data = new ItemGroupModel[0];
            try
            {
                data = _context.ItemGroup.Select(d => new ItemGroupModel{
                        Id = d.Id,
                        CreatedDate = d.CreatedDate,
                        IsActive = d.IsActive,
                        ItemCategoryCode = d.ItemCategory != null ? d.ItemCategory.ItemCategoryCode : "",
                        ItemCategoryName = d.ItemCategory != null ? d.ItemCategory.ItemCategoryName : "",
                        ItemCategoryId = d.ItemCategoryId,
                        ItemGroupCode = d.ItemGroupCode,
                        ItemGroupName = d.ItemGroupName,
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
        public BusinessResult Post(ItemGroupModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request.Headers);

            try
            {
                var dbObj = _context.ItemGroup.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new ItemGroup();
                    _context.ItemGroup.Add(dbObj);
                }

                if (_context.ItemGroup.Any(d => d.ItemGroupCode == model.ItemGroupCode && d.Id != model.Id))
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
                var dbObj = _context.ItemGroup.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

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
