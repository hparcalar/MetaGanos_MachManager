using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using MachManager.Context;
using MachManager.Models;
using MachManager.Models.Constants;
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
    public class EmployeeController : MgControllerBase
    {
        public EmployeeController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        public IEnumerable<EmployeeModel> Get()
        {
            ResolveHeaders(Request);

            EmployeeModel[] data = new EmployeeModel[0];
            try
            {
                int[] plants = null;
                if (_isDealer)
                    plants = _context.Plant.Where(d => d.DealerId == _appUserId).Select(d => d.Id).ToArray();
                else if (_isFactoryOfficer)
                    plants = new int[]{ _context.Officer.Where(d => d.Id == _appUserId).Select(d => d.PlantId).First() };

                using (DefinitionListsBO bObj = new DefinitionListsBO(this._context)){
                    data = bObj.GetEmployees(plants);
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
        public int GetEmployeeCount(){
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
                    dataCount = bObj.GetEmployeeCount(plants);
                }
            }
            catch (System.Exception)
            {
                
            }

            return dataCount;
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
                        ItemGroupId = d.ItemGroupId,
                        ItemGroupName = d.ItemGroup != null ? d.ItemGroup.ItemGroupName : "",
                        ItemName = d.Item != null ? d.Item.ItemName : "",
                        CreditEndDate = d.CreditEndDate,
                        CreditLoadDate = d.CreditLoadDate,
                        CreditStartDate = d.CreditStartDate,
                        CreditByRange = d.CreditByRange,
                        RangeCredit = d.RangeCredit,
                        RangeLength = d.RangeLength,
                        RangeIndex = d.RangeIndex,
                        RangeType = d.RangeType,
                    }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}/Credits")]
        public IEnumerable<EmployeeCreditModel> GetCredits(int id)
        {
            EmployeeCreditModel[] data = new EmployeeCreditModel[0];

            try
            {
                data = _context.EmployeeCredit.Where(d => d.EmployeeId == id)
                    .Select(d => new EmployeeCreditModel{
                        Id = d.Id,
                        ActiveCredit = d.ActiveCredit,
                        EmployeeId = d.EmployeeId,
                        ItemCategoryCode = d.ItemCategory != null ? d.ItemCategory.ItemCategoryCode : "",
                        ItemCategoryId = d.ItemCategoryId,
                        ItemGroupId = d.ItemGroupId,
                        ItemGroupName = d.ItemGroup != null ? d.ItemGroup.ItemGroupName : "",
                        ItemCategoryName = d.ItemCategory != null ? d.ItemCategory.ItemCategoryName : "",
                        ItemCode = d.Item != null ? d.Item.ItemCode : "",
                        ItemId = d.ItemId,
                        ItemName = d.Item != null ? d.Item.ItemName : "",
                        CreditEndDate = d.CreditEndDate,
                        CreditLoadDate = d.CreditLoadDate,
                        CreditStartDate = d.CreditStartDate,
                        CreditByRange = d.CreditByRange,
                        RangeLength = d.RangeLength,
                        RangeCredit = d.RangeCredit,
                        RangeIndex = d.RangeIndex,
                        RangeType = d.RangeType,
                    }).OrderBy(d => d.CreditLoadDate).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}/Credits/{creditId}")]
        public EmployeeCreditModel GetCreditInfo(int id, int creditId)
        {
            EmployeeCreditModel data = new EmployeeCreditModel();

            try
            {
                data = _context.EmployeeCredit.Where(d => d.Id == creditId)
                    .Select(d => new EmployeeCreditModel{
                        Id = d.Id,
                        ActiveCredit = d.ActiveCredit,
                        EmployeeId = d.EmployeeId,
                        ItemCategoryCode = d.ItemCategory != null ? d.ItemCategory.ItemCategoryCode : "",
                        ItemCategoryId = d.ItemCategoryId,
                        ItemGroupId = d.ItemGroupId,
                        ItemGroupName = d.ItemGroup != null ? d.ItemGroup.ItemGroupName : "",
                        ItemCategoryName = d.ItemCategory != null ? d.ItemCategory.ItemCategoryName : "",
                        ItemCode = d.Item != null ? d.Item.ItemCode : "",
                        ItemId = d.ItemId,
                        ItemName = d.Item != null ? d.Item.ItemName : "",
                        CreditEndDate = d.CreditEndDate,
                        CreditLoadDate = d.CreditLoadDate,
                        CreditStartDate = d.CreditStartDate,
                        RangeCredit = d.RangeCredit,
                        CreditByRange = d.CreditByRange,
                        RangeIndex = d.RangeIndex,
                        RangeLength = d.RangeLength,
                        RangeType = d.RangeType,
                    }).FirstOrDefault();
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
            ResolveHeaders(Request);

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
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.Employee.FirstOrDefault(d => d.Id == model.EmployeeId);
                if (dbObj == null){
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));
                }

                model.UpdateLiveRangeData(_context);

                var dbCredit = _context.EmployeeCredit.FirstOrDefault(d => d.EmployeeId == model.EmployeeId
                    && d.ItemCategoryId == model.ItemCategoryId && d.ItemGroupId == model.ItemGroupId);
                if (dbCredit == null){
                    dbCredit = new EmployeeCredit{
                        Employee = dbObj,
                        ItemCategoryId = model.ItemCategoryId,
                        ItemGroupId = model.ItemGroupId,
                        ItemId = model.ItemId,
                        ActiveCredit = 0,
                    };
                    model.MapTo(dbCredit);
                    dbCredit.ActiveCredit = 0;
                    dbCredit.Employee = dbObj;

                    _context.EmployeeCredit.Add(dbCredit);
                }
                else
                {
                    int currentId = dbCredit.Id;
                    model.MapTo(dbCredit);
                    dbCredit.Id = currentId;
                    dbCredit.Employee = dbObj;
                }

                // add to current credit
                dbCredit.ActiveCredit = model.ActiveCredit;

                // create load history
                var dbLoadHistory = new CreditLoadHistory{
                    //CreatedDate = DateTime.Now,
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
        [HttpPost]
        [Route("EditCredit")]
        public BusinessResult EditCredit(EmployeeCreditModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.Employee.FirstOrDefault(d => d.Id == model.EmployeeId);
                if (dbObj == null){
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));
                }

                var dbCredit = _context.EmployeeCredit.FirstOrDefault(d => d.EmployeeId == model.EmployeeId
                    && d.ItemCategoryId == model.ItemCategoryId && d.ItemGroupId == model.ItemGroupId);
                if (dbCredit == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                model.UpdateLiveRangeData(_context);
                model.MapTo(dbCredit);

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

        [HttpGet]
        [Authorize(Policy = "Dealer")]
        [Route("{id}/FileProcess")]
        public IEnumerable<PlantFileProcessModel> GetFileProcessList(int id){
            PlantFileProcessModel[] data = new PlantFileProcessModel[0];

            try
            {
                data = _context.PlantFileProcess.Where(d => d.EmployeeId == id)
                    .Select(d => new PlantFileProcessModel{
                        Id = d.Id,
                        ApprovedDate = d.ApprovedDate,
                        CreatedDate = d.CreatedDate,
                        DepartmentCode = d.Department != null ? d.Department.DepartmentCode : "",
                        DepartmentId = d.DepartmentId,
                        DepartmentName = d.Department != null ? d.Department.DepartmentName : "",
                        EmployeeCode = d.Employee != null ? d.Employee.EmployeeCode : "",
                        EmployeeId = d.EmployeeId,
                        EmployeeName = d.Employee != null ? d.Employee.EmployeeName : "",
                        EndDate = d.EndDate,
                        Explanation = d.Explanation,
                        PlantPrintFileId = d.PlantPrintFileId,
                        PrintFileCode = d.PlantPrintFile != null ? d.PlantPrintFile.PrintFileCode : "",
                        PrintFileName = d.PlantPrintFile != null ? d.PlantPrintFile.PrintFileName : "",
                        ProcessStatus = d.ProcessStatus,
                    })
                    .OrderBy(d => d.CreatedDate)
                    .ToArray();

                foreach (var item in data)
                {
                    item.ProcessStatusText = _translator
                        .Translate(PrintFileStatus.GetExpression(item.ProcessStatus), _userLanguage);
                }
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }

        [HttpGet]
        [Route("{id}/FileProcess/{processId}")]
        public PlantFileProcessModel GetFileProcess(int id, int processId)
        {
            PlantFileProcessModel data = new PlantFileProcessModel();

            try
            {
                data = _context.PlantFileProcess
                    .Where(d => d.EmployeeId == id && d.Id == processId)
                    .Select(d => new PlantFileProcessModel{
                        Id = d.Id,
                        Explanation = d.Explanation,
                        PlantPrintFileId = d.PlantPrintFileId,
                        EmployeeId = d.EmployeeId,
                        DepartmentId = d.DepartmentId,
                        ProcessStatus = d.ProcessStatus,
                        CreatedDate = d.CreatedDate,
                        ApprovedDate = d.ApprovedDate,
                        EndDate = d.EndDate,
                    }).FirstOrDefault();

                if (data != null){
                    data.ProcessStatusText = _translator
                        .Translate(PrintFileStatus.GetExpression(data.ProcessStatus), _userLanguage);
                }
            }
            catch
            {
                
            }
            
            return data;
        }


        [Authorize(Policy = "Dealer")]
        [HttpPost]
        [Route("SaveFileProcess")]
        public BusinessResult PostFileProcess(PlantFileProcessModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbProc = _context.PlantFileProcess.FirstOrDefault(d => d.Id == model.Id);
                if (dbProc == null){
                    dbProc = new PlantFileProcess();
                    model.MapTo(dbProc);
                    dbProc.CreatedDate = DateTime.Now;
                    _context.PlantFileProcess.Add(dbProc);
                }
                else
                    model.MapTo(dbProc);

                // auto assign the department
                if (dbProc.DepartmentId == null){
                    var dbEmployee = _context.Employee.FirstOrDefault(d => d.Id == model.EmployeeId);
                    dbProc.DepartmentId = dbEmployee.DepartmentId;
                }

                // overwrite status specific dates
                if (dbProc.ApprovedDate == null && dbProc.ProcessStatus == PrintFileStatus.APPROVED)
                    dbProc.ApprovedDate = DateTime.Now;
                else if (dbProc.ApprovedDate != null && dbProc.ProcessStatus == PrintFileStatus.CREATED){
                    dbProc.ApprovedDate = null;
                    dbProc.EndDate = null;
                }

                _context.SaveChanges();

                result.Result = true;
                result.RecordId = dbProc.Id;
            }
            catch (System.Exception ex)
            {
                result.Result = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        [Authorize(Policy = "Dealer")]
        [HttpDelete]
        [Route("UnloadCredit")]
        public BusinessResult UnloadCredit(int historyId){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

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
            ResolveHeaders(Request);

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
            ResolveHeaders(Request);

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
