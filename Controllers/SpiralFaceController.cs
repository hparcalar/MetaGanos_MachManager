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
    public class SpiralFaceController : MgControllerBase
    {
        public SpiralFaceController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        public IEnumerable<SpiralFaceModel> Get()
        {
            SpiralFaceModel[] data = new SpiralFaceModel[0];
            try
            {
                data = _context.SpiralFace.Select(d => new SpiralFaceModel{
                        Id = d.Id,
                        Capacity = d.Capacity,
                        ItemCategoryId = d.ItemCategoryId,
                        ItemGroupId = d.ItemGroupId,
                        ItemCategoryCode = d.ItemCategory != null ? d.ItemCategory.ItemCategoryCode : "",
                        ItemCategoryName = d.ItemCategory != null ? d.ItemCategory.ItemCategoryName : "",
                        ItemGroupCode = d.ItemGroup != null ? d.ItemGroup.ItemGroupCode : "",
                        ItemGroupName = d.ItemGroup != null ? d.ItemGroup.ItemGroupName : "",
                    }).OrderBy(d => d.Id).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public SpiralFaceModel Get(int id)
        {
            SpiralFaceModel data = new SpiralFaceModel();
            try
            {
                data = _context.SpiralFace.Where(d => d.Id == id).Select(d => new SpiralFaceModel{
                        Id = d.Id,
                        Capacity = d.Capacity,
                        ItemCategoryId = d.ItemCategoryId,
                        ItemGroupId = d.ItemGroupId,
                        ItemCategoryCode = d.ItemCategory != null ? d.ItemCategory.ItemCategoryCode : "",
                        ItemCategoryName = d.ItemCategory != null ? d.ItemCategory.ItemCategoryName : "",
                        ItemGroupCode = d.ItemGroup != null ? d.ItemGroup.ItemGroupCode : "",
                        ItemGroupName = d.ItemGroup != null ? d.ItemGroup.ItemGroupName : "",
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "Dealer")]
        [HttpPost]
        public BusinessResult Post(SpiralFaceModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request.Headers);

            try
            {
                var dbObj = _context.SpiralFace.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new SpiralFace();
                    _context.SpiralFace.Add(dbObj);
                }

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
                var dbObj = _context.SpiralFace.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                _context.SpiralFace.Remove(dbObj);

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
