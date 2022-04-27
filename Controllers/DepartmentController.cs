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
            ResolveHeaders(Request);
            DepartmentModel[] data = new DepartmentModel[0];
            try
            {
                int[] plants = null;
                if (_isDealer)
                    plants = _context.Plant.Where(d => d.DealerId == _appUserId).Select(d => d.Id).ToArray();
                else if (_isFactoryOfficer)
                    plants = new int[]{ _context.Officer.Where(d => d.Id == _appUserId).Select(d => d.PlantId).First() };
                
                using (DefinitionListsBO bObj = new DefinitionListsBO(this._context)){
                    data = bObj.GetDepartments(plants);
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
        public int GetDepartmentCount(){
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
                    dataCount = bObj.GetDepartmentCount(plants);
                }
            }
            catch (System.Exception)
            {
                
            }

            return dataCount;
        }

        [HttpGet]
        [Route("{id}/Machines")]
        [Authorize(Policy = "FactoryOfficer")]
        public IEnumerable<DepartmentMachineModel> GetMachines(int id){
            DepartmentMachineModel[] data = new DepartmentMachineModel[0];

            try
            {
                data = _context.DepartmentMachine
                    .Where(d => d.DepartmentId == id)
                    .Select(d => new DepartmentMachineModel{
                        Id = d.Id,
                        DepartmentId = d.DepartmentId,
                        MachineId = d.MachineId,
                        MachineCode = d.Machine != null ? d.Machine.MachineCode : "",
                        MachineName = d.Machine != null ? d.Machine.MachineName : "",
                    }).ToArray();
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }

        [HttpPost]
        [Route("{id}/Machines")]
        [Authorize(Policy = "FactoryOfficer")]
        public BusinessResult SaveMachines(int id, DepartmentMachineModel[] model){
            ResolveHeaders(Request);
            BusinessResult result = new BusinessResult();

            try
            {
                var dbDepartment = _context.Department.FirstOrDefault(d => d.Id == id);
                if (dbDepartment == null)
                    throw new Exception(_translator.Translate(Expressions.DepartmentNotFound, _userLanguage));

                var exMatches = _context.DepartmentMachine.Where(d => d.DepartmentId == id).ToArray();
                foreach (var item in exMatches)
                {
                    _context.DepartmentMachine.Remove(item);
                }

                foreach (var item in model)
                {
                    _context.DepartmentMachine.Add(new DepartmentMachine{
                        DepartmentId = id,
                        MachineId = item.MachineId,
                    });
                }

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
                        CreditRangeType = d.CreditRangeType,
                        CreditByRange = d.CreditByRange,
                    }).ToArray();

                foreach (var item in data.ItemCategories)
                {
                    item.CreditRangeTypeText = _translator.Translate(
                        CreditRangeOption.GetExpression(item.CreditRangeType ?? CreditRangeOption.INDEFINITE),
                        _userLanguage
                    );
                }
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "FactoryOfficer")]
        [HttpPost]
        public BusinessResult Post(DepartmentModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.Department.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Department();
                    _context.Department.Add(dbObj);
                }

                if (_context.Department.Any(d => d.DepartmentCode == model.DepartmentCode 
                    && d.PlantId == model.PlantId && d.Id != model.Id))
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

        [Authorize(Policy = "FactoryOfficer")]
        [HttpDelete]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

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
