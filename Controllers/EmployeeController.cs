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

                data.Credits = _context.EmployeeCredit.Where(d => d.EmployeeId == id)
                    .Select(d => new EmployeeCreditModel{
                        Id = d.Id,
                        ActiveCredit = d.ActiveCredit,
                        EmployeeId = d.EmployeeId,
                        ItemCategoryCode = d.ItemCategory != null ? d.ItemCategory.ItemCategoryCode : "",
                        ItemCategoryId = d.ItemCategoryId,
                        ItemCategoryName = d.ItemCategory != null ? d.ItemCategory.ItemCategoryName : "",
                        ItemCode = d.Item != null ? d.Item.ItemCode : "",
                        ItemId = d.ItemId,
                        ItemName = d.Item != null ? d.Item.ItemName : "",
                    }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}/CreditLoadHistory")]
        public IEnumerable<CreditLoadHistoryModel> GetCreditLoadHistory(int id)
        {
            CreditLoadHistoryModel[] data = new CreditLoadHistoryModel[0];

            try
            {
                data = _context.CreditLoadHistory
                    .Where(d => d.EmployeeId == id)
                    .Select(d => new CreditLoadHistoryModel{
                        Id = d.Id,
                        CreatedDate = d.CreatedDate,
                        DealerId = d.DealerId,
                        EmployeeCardId = d.EmployeeCardId,
                        EmployeeId = d.EmployeeId,
                        ItemCategoryCode = d.ItemCategory != null ? d.ItemCategory.ItemCategoryCode : "",
                        ItemCategoryId = d.ItemCategoryId,
                        ItemCategoryName = d.ItemCategory != null ? d.ItemCategory.ItemCategoryName : "",
                        LoadedCredits = d.LoadedCredits,
                    }).OrderBy(d => d.CreatedDate).ToArray();
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
        [HttpPost]
        [Route("LoadCredit")]
        public BusinessResult LoadCredit(EmployeeCreditModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request.Headers);

            try
            {
                var dbObj = _context.Employee.FirstOrDefault(d => d.Id == model.EmployeeId);
                if (dbObj == null){
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));
                }

                var dbCredit = _context.EmployeeCredit.FirstOrDefault(d => d.EmployeeId == model.EmployeeId
                    && d.ItemCategoryId == model.ItemCategoryId);
                if (dbCredit == null){
                    dbCredit = new EmployeeCredit{
                        Employee = dbObj,
                        ItemCategoryId = model.ItemCategoryId,
                        ItemGroupId = model.ItemGroupId,
                        ItemId = model.ItemId,
                        ActiveCredit = 0,
                    };
                    _context.EmployeeCredit.Add(dbCredit);
                }

                // add to current credit
                dbCredit.ActiveCredit += model.ActiveCredit;

                // create load history
                var dbLoadHistory = new CreditLoadHistory{
                    CreatedDate = DateTime.Now,
                    DealerId = null, // will be got from jwt token
                    EmployeeId = dbObj.Id,
                    ItemCategoryId = model.ItemCategoryId,
                    LoadedCredits = model.ActiveCredit,
                };
                _context.CreditLoadHistory.Add(dbLoadHistory);

                _context.SaveChanges();
                result.Result=true;
                result.RecordId = dbCredit.Id;
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
        [Route("UnloadCredit")]
        public BusinessResult UnloadCredit(int historyId){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request.Headers);

            try
            {
                var dbObj = _context.CreditLoadHistory.FirstOrDefault(d => d.Id == historyId);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                var dbEmployeeCredit = _context.EmployeeCredit.FirstOrDefault(d => d.EmployeeId == dbObj.EmployeeId
                    && d.ItemCategoryId == dbObj.ItemCategoryId);
                if (dbEmployeeCredit != null){
                    dbEmployeeCredit.ActiveCredit -= dbObj.LoadedCredits;

                    if (dbEmployeeCredit.ActiveCredit < 0)
                        dbEmployeeCredit.ActiveCredit = 0;
                }

                _context.CreditLoadHistory.Remove(dbObj);

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

        [Authorize(Policy = "Dealer")]
        [HttpDelete]
        [Route("DeleteCredit")]
        public BusinessResult DeleteCredit(int creditId){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request.Headers);

            try
            {
                var dbObj = _context.EmployeeCredit.FirstOrDefault(d => d.Id == creditId);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                _context.EmployeeCredit.Remove(dbObj);

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
