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
    public class PlantController : MgControllerBase
    {
        public PlantController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        public IEnumerable<PlantModel> Get()
        {
            PlantModel[] data = new PlantModel[0];
            try
            {
                data = _context.Plant.Select(d => new PlantModel{
                        Id = d.Id,
                        CreatedDate = d.CreatedDate,
                        DealerCode = d.Dealer != null ? d.Dealer.DealerCode : "",
                        DealerId = d.DealerId,
                        DealerName = d.Dealer != null ? d.Dealer.DealerName : "",
                        Explanation = d.Explanation,
                        IsActive = d.IsActive,
                        PlantCode = d.PlantCode,
                        PlantName = d.PlantName,
                    }).OrderBy(d => d.PlantCode).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public PlantModel Get(int id)
        {
            PlantModel data = new PlantModel();
            try
            {
                data = _context.Plant.Where(d => d.Id == id).Select(d => new PlantModel{
                        Id = d.Id,
                        CreatedDate = d.CreatedDate,
                        DealerCode = d.Dealer != null ? d.Dealer.DealerCode : "",
                        DealerId = d.DealerId,
                        DealerName = d.Dealer != null ? d.Dealer.DealerName : "",
                        Explanation = d.Explanation,
                        IsActive = d.IsActive,
                        PlantCode = d.PlantCode,
                        PlantName = d.PlantName,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("Count")]
        public int GetPlantCount(){
            int plantCount = 0;

            try
            {
                plantCount = _context.Plant.Count();
            }
            catch (System.Exception)
            {
                
            }

            return plantCount;
        }

        [HttpGet]
        [Route("{id}/Departments")]
        public IEnumerable<DepartmentModel> GetDepartments(int id)
        {
            DepartmentModel[] data = new DepartmentModel[0];
            
            try
            {
                data = _context.Department.Where(d => d.PlantId == id).Select(d => new DepartmentModel{
                        Id = d.Id,
                        DepartmentCode = d.DepartmentCode,
                        DepartmentName = d.DepartmentName,
                        IsActive = d.IsActive,
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


        [Authorize(Policy = "Dealer")]
        [HttpPost]
        public BusinessResult Post(PlantModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request.Headers);

            try
            {
                var dbObj = _context.Plant.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Plant();
                    _context.Plant.Add(dbObj);
                }

                if (_context.Plant.Any(d => d.PlantCode == model.PlantCode && d.Id != model.Id))
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
                var dbObj = _context.Plant.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                _context.Plant.Remove(dbObj);

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
