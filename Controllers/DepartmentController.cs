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


        [HttpGet]
        [Route("{id}/Credits/{creditId}")]
        public DepartmentCreditModel GetCreditInfo(int id, int creditId)
        {
            DepartmentCreditModel data = new DepartmentCreditModel();

            try
            {
                data = _context.DepartmentCredit.Where(d => d.Id == creditId)
                    .Select(d => new DepartmentCreditModel{
                        Id = d.Id,
                        ActiveCredit = d.ActiveCredit,
                        DepartmentId = d.DepartmentId,
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
                        ProductIntervalTime = d.ProductIntervalTime,
                        ProductIntervalType = d.ProductIntervalType,
                        SpecificRangeDates = d.SpecificRangeDates,
                    }).FirstOrDefault();
            }
            catch
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
        [Route("{id}/Employees")]
        [Authorize("FactoryOfficer")]
        public IEnumerable<EmployeeModel> GetEmployees(int id){
            ResolveHeaders(Request);
            EmployeeModel[] data = new EmployeeModel[0];

            try
            {
                using (DefinitionListsBO bObj = new DefinitionListsBO(this._context)){
                    data = bObj.GetEmployees(departments: new int[]{ id });
                }
            }
            catch (System.Exception)
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

                bool sampleCreditsUpdated = false;
                data.Credits = _context.DepartmentCredit.Where(d => d.DepartmentId == data.Id)
                    .Select(d => new DepartmentCreditModel{
                        Id = d.Id,
                        ActiveCredit = d.ActiveCredit,
                        CreditByRange = d.CreditByRange,
                        CreditEndDate = d.CreditEndDate,
                        CreditLoadDate = d.CreditLoadDate,
                        CreditStartDate = d.CreditStartDate,
                        DepartmentId = d.DepartmentId,
                        ItemCategoryId = d.ItemCategoryId,
                        ItemGroupId = d.ItemGroupId,
                        ItemId = d.ItemId,
                        ProductIntervalTime = d.ProductIntervalTime,
                        ProductIntervalType = d.ProductIntervalType,
                        RangeCredit = d.RangeCredit,
                        RangeIndex = d.RangeIndex,
                        RangeLength = d.RangeLength,
                        RangeType = d.RangeType,
                        SpecificRangeDates = d.SpecificRangeDates,
                        ItemName = d.Item != null ? d.Item.ItemName : "",
                        ItemGroupName = d.ItemGroup != null ? d.ItemGroup.ItemGroupName : "",
                        ItemCategoryName = d.ItemCategory != null ? d.ItemCategory.ItemCategoryName : "",
                    })
                    .ToArray();
                if (data.Credits == null || data.Credits.Length == 0){
                    var sampleEmployee = _context.Employee.Where(d => d.DepartmentId == data.Id).FirstOrDefault();
                    if (sampleEmployee != null){

                        List<DepartmentCreditModel> newCredits = new List<DepartmentCreditModel>();
                        var empCredits = _context.EmployeeCredit.Where(d => d.EmployeeId == sampleEmployee.Id).ToArray();
                        foreach (var crd in empCredits)
                        {
                            var newDepCrd = new DepartmentCredit{
                                DepartmentId = data.Id,
                                ItemId = crd.ItemId,
                                ItemCategoryId = crd.ItemCategoryId,
                                ItemGroupId = crd.ItemGroupId,
                                ActiveCredit = crd.ActiveCredit,
                                RangeCredit = crd.RangeCredit,
                                CreditByRange = crd.CreditByRange,
                                CreditEndDate = crd.CreditEndDate,
                                CreditLoadDate = crd.CreditLoadDate,
                                CreditStartDate = crd.CreditStartDate,
                                RangeIndex = crd.RangeIndex,
                                RangeLength = crd.RangeLength,
                                RangeType = crd.RangeType,
                                ProductIntervalTime = crd.ProductIntervalTime,
                                ProductIntervalType = crd.ProductIntervalType,
                            };
                            _context.DepartmentCredit.Add(newDepCrd);

                            DepartmentCreditModel crdContainer = new DepartmentCreditModel();
                            newDepCrd.MapTo(crdContainer);
                            newCredits.Add(crdContainer);

                            if (crd.ItemId != null){
                                var dbItem = _context.Item.FirstOrDefault(d => d.Id == crd.ItemId);
                                if (dbItem != null)
                                    crdContainer.ItemName = dbItem.ItemName;
                            }

                            if (crd.ItemGroupId != null){
                                var dbItemGr = _context.ItemGroup.FirstOrDefault(d => d.Id == crd.ItemGroupId);
                                if (dbItemGr != null)
                                    crdContainer.ItemGroupName = dbItemGr.ItemGroupName;
                            }

                            if (crd.ItemCategoryId != null){
                                var dbItemCt = _context.ItemCategory.FirstOrDefault(d => d.Id == crd.ItemCategoryId);
                                if (dbItemCt != null)
                                    crdContainer.ItemCategoryName = dbItemCt.ItemCategoryName;
                            }


                            sampleCreditsUpdated = true;
                        }

                        data.Credits = newCredits.ToArray();
                    }
                }

                if (sampleCreditsUpdated){
                    _context.SaveChanges();
                }
            }
            catch
            {
                
            }
            
            return data;
        }


        [HttpGet]
        [Route("ApplyCreditsForEveryone/{id}")]
        public BusinessResult ApplyCreditsForEveryone(int id)
        {
            BusinessResult result = new BusinessResult();

            try
            {
                var depCredits = _context.DepartmentCredit.Where(d => d.DepartmentId == id).ToArray();
                
                var employees = _context.Employee.Where(d => d.DepartmentId == id).ToArray();
                foreach (var dbEmployee in employees)
                {
                    // remove old credits of current employee
                    var oldCredits = _context.EmployeeCredit.Where(d => d.EmployeeId == dbEmployee.Id).ToArray();
                    foreach (var item in oldCredits)
                    {
                        _context.EmployeeCredit.Remove(item);
                    }

                    // loop into dep credits to give new permissions
                    foreach (var depCrd in depCredits)
                    {
                        DateTime rangeLoadDate = DateTime.Now.Date;
                        switch (depCrd.RangeType)
                        {
                            case 1:
                                rangeLoadDate = DateTime.Now.Date;

                                break;
                            case 2:
                                var dayIndex = rangeLoadDate.DayOfWeek;
                                while (dayIndex != DayOfWeek.Monday){
                                    rangeLoadDate = rangeLoadDate.AddDays(-1);
                                    dayIndex = rangeLoadDate.DayOfWeek;
                                }

                                break;
                            case 3:
                                rangeLoadDate = DateTime.ParseExact("01." 
                                    + string.Format("{0:MM}", rangeLoadDate) + "." + 
                                    string.Format("{0:yyyy}", rangeLoadDate), "dd.MM.yyyy",
                                        System.Globalization.CultureInfo.GetCultureInfo("tr"));

                                break;
                            default:
                                rangeLoadDate = DateTime.MinValue;
                                break;
                        }

                        if (rangeLoadDate == DateTime.MinValue)
                            continue;

                        var rangeCredit = depCrd.CreditByRange;

                        EmployeeCredit dbCredit = new EmployeeCredit{
                            ItemId = depCrd.ItemId,
                            ItemCategoryId = depCrd.ItemCategoryId,
                            ItemGroupId = depCrd.ItemGroupId,
                        };
                        dbCredit.Employee = dbEmployee;
                        _context.EmployeeCredit.Add(dbCredit);

                        // set credit attributes
                        dbCredit.RangeCredit = rangeCredit;
                        dbCredit.ActiveCredit = rangeCredit;
                        dbCredit.CreditByRange = rangeCredit;
                        dbCredit.RangeIndex = 0;
                        dbCredit.RangeType = depCrd.RangeType;
                        dbCredit.RangeLength = depCrd.RangeLength;
                        dbCredit.CreditLoadDate = rangeLoadDate;
                        dbCredit.CreditStartDate = rangeLoadDate;

                        EmployeeCreditModel creditModel = new EmployeeCreditModel();
                        dbCredit.MapTo(creditModel);
                        creditModel.UpdateLiveRangeData(_context);
                        creditModel.MapTo(dbCredit);

                        string newRanges = "";
                        DateTime dtCurrent = dbCredit.CreditStartDate.Value.Date;

                        while (dtCurrent <= dbCredit.CreditEndDate.Value.Date){
                            newRanges += "\""+ string.Format("{0:yyyy-MM-ddTHH:mm:ss}", dtCurrent) +".000Z\",";
                            dtCurrent = dtCurrent.AddDays(1);
                        }

                        newRanges = newRanges.Substring(0, newRanges.Length - 1);
                        newRanges = "[" + newRanges + "]";
                        dbCredit.SpecificRangeDates = newRanges;

                        if (dbCredit.ActiveCredit == 0)
                            dbCredit.RangeCredit = 0;
                    }
                }

                _context.SaveChanges();

                result.Result=true;
            }
            catch (Exception ex)
            {
                result.Result =false;
                result.ErrorMessage = ex.Message;
            }
            
            return result;
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
        [HttpPost]
        [Route("LoadCredit")]
        public BusinessResult LoadCredit(DepartmentCreditModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.Department.FirstOrDefault(d => d.Id == model.DepartmentId);
                if (dbObj == null){
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));
                }

                // model.UpdateLiveRangeData(_context);

                var dbCredit = _context.DepartmentCredit.FirstOrDefault(d => d.DepartmentId == model.DepartmentId
                    && d.ItemCategoryId == model.ItemCategoryId && d.ItemGroupId == model.ItemGroupId && d.ItemId == model.ItemId);
                if (dbCredit == null){
                    dbCredit = new DepartmentCredit{
                        Department = dbObj,
                        ItemCategoryId = model.ItemCategoryId,
                        ItemGroupId = model.ItemGroupId,
                        ItemId = model.ItemId,
                        ActiveCredit = 0,
                    };
                    model.MapTo(dbCredit);
                    dbCredit.ActiveCredit = 0;
                    dbCredit.Department = dbObj;

                    _context.DepartmentCredit.Add(dbCredit);
                }
                else
                {
                    int currentId = dbCredit.Id;
                    model.MapTo(dbCredit);
                    dbCredit.Id = currentId;
                    dbCredit.Department = dbObj;
                }

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

        [Authorize(Policy = "FactoryOfficer")]
        [HttpPost]
        [Route("EditCredit")]
        public BusinessResult EditCredit(DepartmentCreditModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.Department.FirstOrDefault(d => d.Id == model.DepartmentId);
                if (dbObj == null){
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));
                }

                var dbCredit = _context.DepartmentCredit.FirstOrDefault(d => d.Id == model.Id);
                if (dbCredit == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                // model.UpdateLiveRangeData(_context);
                model.MapTo(dbCredit);

                // if (dbCredit.ActiveCredit == 0)
                //     dbCredit.RangeCredit = 0;

                // if (!model.CancelSubmit)
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



        [Authorize(Policy = "FactoryOfficer")]
        [HttpDelete]
        [Route("DeleteCredit")]
        public BusinessResult DeleteCredit(int creditId){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.DepartmentCredit.FirstOrDefault(d => d.Id == creditId);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                _context.DepartmentCredit.Remove(dbObj);

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


        [Authorize(Policy = "FactoryOfficer")]
        [HttpDelete("{id}")]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.Department.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                if (_context.Employee.Any(d => d.DepartmentId == id))
                    throw new Exception("Bu departmana ait personeller bulunduğu için silinemez.");

                var categories = _context.DepartmentItemCategory.Where(d => d.DepartmentId == dbObj.Id).ToArray();
                foreach (var item in categories)
                {
                    _context.DepartmentItemCategory.Remove(item);
                }

                var machines = _context.DepartmentMachine.Where(d => d.DepartmentId == dbObj.Id).ToArray();
                foreach (var item in machines)
                {
                    _context.DepartmentMachine.Remove(item);
                }

                var credits = _context.DepartmentCredit.Where(d => d.DepartmentId == dbObj.Id).ToArray();
                foreach (var item in credits)
                {
                    _context.DepartmentCredit.Remove(item);
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
