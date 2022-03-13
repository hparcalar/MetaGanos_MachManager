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
    public class DepartmentController : MgControllerBase
    {
        public DepartmentController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        public IEnumerable<DepartmentModel> Get()
        {
            DepartmentModel[] data = new DepartmentModel[0];
            try
            {
                data = _context.Department.Select(d => new DepartmentModel{
                        Id = d.Id,
                        DepartmentCode = d.DepartmentCode,
                        DepartmentName = d.DepartmentName,
                        IsActive = d.IsActive,
                        PlantPrintFileId = d.PlantPrintFileId,
                        PlantCode = d.Plant != null ? d.Plant.PlantCode : "",
                        PlantName = d.Plant != null ? d.Plant.PlantName : "",
                        PlantId = d.PlantId,
                    }).OrderBy(d => d.DepartmentCode).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public DepartmentModel Get(int id)
        {
            DepartmentModel data = new DepartmentModel();
            try
            {
                data = _context.Department.Where(d => d.Id == id).Select(d => new DepartmentModel{
                        Id = d.Id,
                        DepartmentCode = d.DepartmentCode,
                        DepartmentName = d.DepartmentName,
                        IsActive = d.IsActive,
                        PlantPrintFileId = d.PlantPrintFileId,
                        PlantCode = d.Plant != null ? d.Plant.PlantCode : "",
                        PlantName = d.Plant != null ? d.Plant.PlantName : "",
                        PlantId = d.PlantId,
                    }).FirstOrDefault();

                data.ItemCategories = _context.DepartmentItemCategory.Where(d => d.DepartmentId == data.Id)
                    .Select(d => new DepartmentItemCategoryModel{
                        Id = d.Id,
                        DepartmentId = d.DepartmentId,
                        ItemCategoryCode = d.ItemCategory != null ? d.ItemCategory.ItemCategoryCode : "",
                        ItemCategoryId = d.ItemCategoryId,
                        ItemCategoryName = d.ItemCategory != null ? d.ItemCategory.ItemCategoryName : "",
                    }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "Dealer")]
        [HttpPost]
        public BusinessResult Post(DepartmentModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request.Headers);

            try
            {
                var dbObj = _context.Department.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Department();
                    _context.Department.Add(dbObj);
                }

                if (_context.Department.Any(d => d.DepartmentCode == model.DepartmentCode && d.Id != model.Id))
                    throw new Exception(_translator.Translate(Expressions.SameCodeExists, _userLanguage));

                model.MapTo(dbObj);

                // save item categories
                var oldCategories = _context.DepartmentItemCategory.Where(d => d.DepartmentId == dbObj.Id).ToArray();
                foreach (var item in oldCategories)
                {
                    _context.DepartmentItemCategory.Remove(item);
                }

                if (model.ItemCategories != null){
                    foreach (var item in model.ItemCategories)
                    {
                        var dbCategory = new DepartmentItemCategory();
                        item.MapTo(dbCategory);
                        dbCategory.Id = 0;
                        dbCategory.Department = dbObj;
                        _context.DepartmentItemCategory.Add(dbCategory);
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
        [HttpDelete]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request.Headers);

            try
            {
                var dbObj = _context.Department.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                var categories = _context.DepartmentItemCategory.Where(d => d.DepartmentId == dbObj.Id).ToArray();
                foreach (var item in categories)
                {
                    _context.DepartmentItemCategory.Remove(item);
                }

                _context.Department.Remove(dbObj);

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
