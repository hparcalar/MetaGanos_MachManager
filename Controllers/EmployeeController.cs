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
    public class EmployeeController : MgControllerBase
    {
        public EmployeeController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        public IEnumerable<EmployeeModel> Get()
        {
            EmployeeModel[] data = new EmployeeModel[0];
            try
            {
                data = _context.Employee.Select(d => new EmployeeModel{
                        Id = d.Id,
                        ActiveCredit = d.ActiveCredit,
                        DepartmentCode = d.Department != null ? d.Department.DepartmentCode : "",
                        DepartmentId = d.DepartmentId,
                        DepartmentName = d.Department != null ? d.Department.DepartmentName : "",
                        Email = d.Email,
                        EmployeeCardCode = d.EmployeeCard != null ? d.EmployeeCard.CardCode : "",
                        EmployeeCardHex = d.EmployeeCard != null ? d.EmployeeCard.HexKey : "",
                        EmployeeCardId = d.EmployeeCardId,
                        EmployeeCode = d.EmployeeCode,
                        EmployeeName = d.EmployeeName,
                        EmployeePassword = "",
                        Gsm = d.Gsm,
                        IsActive = d.IsActive,
                        PlantCode = d.Plant != null ? d.Plant.PlantCode : "",
                        PlantId = d.PlantId,
                        PlantName = d.Plant != null ? d.Plant.PlantName : "",
                    }).OrderBy(d => d.EmployeeCode).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public EmployeeModel Get(int id)
        {
            EmployeeModel data = new EmployeeModel();
            try
            {
                data = _context.Employee.Where(d => d.Id == id).Select(d => new EmployeeModel{
                       Id = d.Id,
                        ActiveCredit = d.ActiveCredit,
                        DepartmentCode = d.Department != null ? d.Department.DepartmentCode : "",
                        DepartmentId = d.DepartmentId,
                        DepartmentName = d.Department != null ? d.Department.DepartmentName : "",
                        Email = d.Email,
                        EmployeeCardCode = d.EmployeeCard != null ? d.EmployeeCard.CardCode : "",
                        EmployeeCardHex = d.EmployeeCard != null ? d.EmployeeCard.HexKey : "",
                        EmployeeCardId = d.EmployeeCardId,
                        EmployeeCode = d.EmployeeCode,
                        EmployeeName = d.EmployeeName,
                        EmployeePassword = "",
                        Gsm = d.Gsm,
                        IsActive = d.IsActive,
                        PlantCode = d.Plant != null ? d.Plant.PlantCode : "",
                        PlantId = d.PlantId,
                        PlantName = d.Plant != null ? d.Plant.PlantName : "",
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "Dealer")]
        [HttpPost]
        public BusinessResult Post(EmployeeModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request.Headers);

            try
            {
                var dbObj = _context.Employee.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Employee();
                    _context.Employee.Add(dbObj);
                }

                if (_context.Employee.Any(d => d.EmployeeCode == model.EmployeeCode && d.Id != model.Id))
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
                var dbObj = _context.Employee.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                _context.Employee.Remove(dbObj);

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
