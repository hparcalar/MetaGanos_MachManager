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
using MachManager.Models.Parameters;
using MachManager.Models.PagedModels;
using ClosedXML;
using ClosedXML.Excel;
using System.Text.RegularExpressions;

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
                else if (_isMachine)
                    plants = new int[]{ _appUserId };

                using (DefinitionListsBO bObj = new DefinitionListsBO(this._context)){
                    data = bObj.GetEmployees(plants);
                }
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet("ListByPage/{page}")]
        public PagedEmployeeModel GetListByPage(int? page, string search)
        {
            PagedEmployeeModel resData = new PagedEmployeeModel();

            ResolveHeaders(Request);

            try
            {
                int[] plants = null;
                if (_isDealer)
                    plants = _context.Plant.Where(d => d.DealerId == _appUserId).Select(d => d.Id).ToArray();
                else if (_isFactoryOfficer)
                    plants = new int[]{ _context.Officer.Where(d => d.Id == _appUserId).Select(d => d.PlantId).First() };
                else if (_isMachine)
                    plants = new int[]{ _appUserId };

                using (DefinitionListsBO bObj = new DefinitionListsBO(this._context)){
                    resData = bObj.GetEmployeesByPage(plants, null, page, search);
                }
            }
            catch
            {
                
            }
            
            return resData;
        }

        [HttpGet]
        [Route("NonActive")]
        public IEnumerable<EmployeeModel> GetNonActive()
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
                else if (_isMachine)
                    plants = new int[]{ _appUserId };

                using (DefinitionListsBO bObj = new DefinitionListsBO(this._context)){
                    data = bObj.GetNonActiveEmployees(plants);
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
                        ProductIntervalTime = d.ProductIntervalTime,
                        ProductIntervalType = d.ProductIntervalType,
                        SpecificRangeDates = d.SpecificRangeDates,
                    }).ToArray();

                bool _creditUpdated = false;

                foreach (var crd in data.Credits)
                {
                    if (crd.CreditLoadDate < DateTime.Now.Date && crd.RangeType == 1){
                        while (crd.CreditLoadDate < DateTime.Now.Date){
                            var dbCredit = _context.EmployeeCredit.FirstOrDefault(d => d.Id == crd.Id);
                            if (dbCredit != null){
                            try
                            {
                                dbCredit.CreditLoadDate = DateTime.Now.Date;
                                dbCredit.RangeCredit = crd.CreditByRange;
                                dbCredit.ActiveCredit = crd.CreditByRange;
                                dbCredit.CreditStartDate = DateTime.Now.Date;
                                dbCredit.CreditEndDate = DateTime.Now.Date;
                                dbCredit.SpecificRangeDates = "";

                                dbCredit.MapTo(crd);

                                crd.UpdateLiveRangeData(_context);
                                crd.MapTo(dbCredit);

                                dbCredit.CreditLoadDate = DateTime.Now.Date;
                                dbCredit.CreditStartDate = DateTime.Now.Date;
                                // dbCredit.CreditEndDate = DateTime.Now.Date;

                                string newRanges = "";                            
                                DateTime dtCurrent = dbCredit.CreditStartDate.Value.Date;

                                while (dtCurrent <= dbCredit.CreditEndDate.Value.Date){
                                    newRanges += "\""+ string.Format("{0:yyyy-MM-ddTHH:mm:ss}", dtCurrent) +".000Z\",";
                                    dtCurrent = dtCurrent.AddDays(1);

                                    if (dtCurrent == dbCredit.CreditEndDate.Value.Date)
                                        break;
                                }

                                newRanges = newRanges.Substring(0, newRanges.Length - 1);
                                newRanges = "[" + newRanges + "]";
                                dbCredit.SpecificRangeDates = newRanges;
                            }
                            catch (System.Exception ex)
                            {
                                
                            }

                            _creditUpdated = true;
                        }
                        }
                    }
                    else if (crd.CreditEndDate <= DateTime.Now.Date && crd.RangeType != 1){
                        while (crd.CreditEndDate <= DateTime.Now.Date){
                            var dbCredit = _context.EmployeeCredit.FirstOrDefault(d => d.Id == crd.Id);
                            if (dbCredit != null){
                            try
                            {
                                var diffDays = Convert.ToInt32(Math.Abs((crd.CreditEndDate.Value - crd.CreditLoadDate.Value).TotalDays));
                                dbCredit.CreditLoadDate = crd.CreditEndDate.Value;
                                dbCredit.RangeCredit = crd.CreditByRange;
                                dbCredit.ActiveCredit = crd.CreditByRange;
                                dbCredit.CreditStartDate = dbCredit.CreditEndDate.Value;
                                dbCredit.CreditEndDate = dbCredit.CreditEndDate.Value.AddDays(diffDays);

                                dbCredit.MapTo(crd);
                                crd.UpdateLiveRangeData(_context);
                                crd.MapTo(dbCredit);

                                string newRanges = "";                            
                                DateTime dtCurrent = dbCredit.CreditStartDate.Value.Date;

                                while (dtCurrent <= dbCredit.CreditEndDate.Value.Date){
                                    newRanges += "\""+ string.Format("{0:yyyy-MM-ddTHH:mm:ss}", dtCurrent) +".000Z\",";
                                    dtCurrent = dtCurrent.AddDays(1);

                                    if (dtCurrent == dbCredit.CreditEndDate.Value.Date)
                                        break;
                                }

                                newRanges = newRanges.Substring(0, newRanges.Length - 1);
                                newRanges = "[" + newRanges + "]";
                                dbCredit.SpecificRangeDates = newRanges;

                                _creditUpdated = true;
                            }
                            catch (System.Exception)
                            {
                                
                            }
                        }
                        }
                    }
                }

                if (_creditUpdated){
                    var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == data.PlantId);
                    dbPlant.LastUpdateDate = DateTime.Now.AddMinutes(10);

                    _context.SaveChanges();

                    using (MetaGanosSchema nContext = SchemaFactory.CreateContext()){
                        data.Credits = nContext.EmployeeCredit.Where(d => d.EmployeeId == id)
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
                            ProductIntervalTime = d.ProductIntervalTime,
                            ProductIntervalType = d.ProductIntervalType,
                            SpecificRangeDates = d.SpecificRangeDates,
                        }).OrderBy(d => d.CreditLoadDate).ToArray();

                    }
                }
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
                var dbEmp = _context.Employee.FirstOrDefault(d => d.Id == id);

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
                        ProductIntervalTime = d.ProductIntervalTime,
                        ProductIntervalType = d.ProductIntervalType,
                        SpecificRangeDates = d.SpecificRangeDates,
                    }).OrderBy(d => d.CreditLoadDate).ToArray();

                bool _creditUpdated = false;

                foreach (var crd in data)
                {
                    if (crd.CreditLoadDate < DateTime.Now.Date && crd.RangeType == 1){
                        while (crd.CreditLoadDate < DateTime.Now.Date){
                            var dbCredit = _context.EmployeeCredit.FirstOrDefault(d => d.Id == crd.Id);
                            if (dbCredit != null){
                            try
                            {
                                dbCredit.CreditLoadDate = DateTime.Now.Date;
                                dbCredit.RangeCredit = crd.CreditByRange;
                                dbCredit.ActiveCredit = crd.CreditByRange;
                                dbCredit.CreditStartDate = DateTime.Now.Date;
                                dbCredit.CreditEndDate = DateTime.Now.Date;
                                dbCredit.SpecificRangeDates = "";

                                dbCredit.MapTo(crd);
                                crd.UpdateLiveRangeData(_context);
                                crd.MapTo(dbCredit);

                                dbCredit.CreditLoadDate = DateTime.Now.Date;
                                dbCredit.CreditStartDate = DateTime.Now.Date;
                                // dbCredit.CreditEndDate = DateTime.Now.Date;

                                string newRanges = "";                            
                                DateTime dtCurrent = dbCredit.CreditStartDate.Value.Date;

                                while (dtCurrent <= dbCredit.CreditEndDate.Value.Date){
                                    newRanges += "\""+ string.Format("{0:yyyy-MM-ddTHH:mm:ss}", dtCurrent) +".000Z\",";
                                    dtCurrent = dtCurrent.AddDays(1);

                                    if (dtCurrent == dbCredit.CreditEndDate.Value.Date)
                                        break;
                                }

                                newRanges = newRanges.Substring(0, newRanges.Length - 1);
                                newRanges = "[" + newRanges + "]";
                                dbCredit.SpecificRangeDates = newRanges;
                            }
                            catch (System.Exception)
                            {
                                
                            }

                            _creditUpdated = true;
                        }
                        }
                    }
                    else if (crd.CreditEndDate <= DateTime.Now.Date && crd.RangeType != 1){
                        while (crd.CreditEndDate <= DateTime.Now.Date){
                            var dbCredit = _context.EmployeeCredit.FirstOrDefault(d => d.Id == crd.Id);
                            if (dbCredit != null){
                                try
                                {
                                    var diffDays = Convert.ToInt32(Math.Abs((crd.CreditEndDate.Value - crd.CreditLoadDate.Value).TotalDays));
                                    dbCredit.CreditLoadDate = crd.CreditEndDate.Value;
                                    dbCredit.RangeCredit = crd.CreditByRange;
                                    dbCredit.ActiveCredit = crd.CreditByRange;
                                    dbCredit.CreditStartDate = dbCredit.CreditEndDate.Value;
                                    dbCredit.CreditEndDate = dbCredit.CreditEndDate.Value.AddDays(diffDays);

                                    dbCredit.MapTo(crd);
                                    crd.UpdateLiveRangeData(_context);
                                    crd.MapTo(dbCredit);

                                    string newRanges = "";                            
                                    DateTime dtCurrent = dbCredit.CreditStartDate.Value.Date;

                                    while (dtCurrent <= dbCredit.CreditEndDate.Value.Date){
                                        newRanges += "\""+ string.Format("{0:yyyy-MM-ddTHH:mm:ss}", dtCurrent) +".000Z\",";
                                        dtCurrent = dtCurrent.AddDays(1);
                                    }

                                    newRanges = newRanges.Substring(0, newRanges.Length - 1);
                                    newRanges = "[" + newRanges + "]";
                                    dbCredit.SpecificRangeDates = newRanges;

                                    _creditUpdated = true;
                                }
                                catch (System.Exception)
                                {
                                    
                                }
                            }
                        }
                    }
                }

                if (_creditUpdated){
                    var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == dbEmp.PlantId);
                    dbPlant.LastUpdateDate = DateTime.Now.AddMinutes(10);

                    _context.SaveChanges();

                    using (MetaGanosSchema nContext = SchemaFactory.CreateContext()){
                        data = nContext.EmployeeCredit.Where(d => d.EmployeeId == id)
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
                            ProductIntervalTime = d.ProductIntervalTime,
                            ProductIntervalType = d.ProductIntervalType,
                            SpecificRangeDates = d.SpecificRangeDates,
                        }).OrderBy(d => d.CreditLoadDate).ToArray();

                    }
                }
            }
            catch
            {
                
            }
            
            return data;
        }


        [HttpGet]
        [Route("{id}/FetchCredits")]
        public IEnumerable<EmployeeCreditModel> FetchCredits(int id)
        {
            EmployeeCreditModel[] data = new EmployeeCreditModel[0];

            try
            {
                var dbEmp = _context.Employee.FirstOrDefault(d => d.Id == id);

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
                        ProductIntervalTime = d.ProductIntervalTime,
                        ProductIntervalType = d.ProductIntervalType,
                        SpecificRangeDates = d.SpecificRangeDates,
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

        [Authorize(Policy = "FactoryOfficer")]
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

                if (_context.Employee.Any(d => d.EmployeeCode == model.EmployeeCode
                    && d.PlantId == model.PlantId && d.Id != model.Id))
                    throw new Exception(_translator.Translate(Expressions.SameCodeExists, _userLanguage));

                // detect change of department and update new credits template
                if (model.DepartmentId != dbObj.DepartmentId){
                    var currentCredits = _context.EmployeeCredit.Where(d => d.EmployeeId == dbObj.Id).ToArray();
                    foreach (var item in currentCredits)
                    {
                        _context.EmployeeCredit.Remove(item);
                    }

                    var sampleEmployee = _context.Employee.FirstOrDefault(d => d.DepartmentId == model.DepartmentId);
                    if (sampleEmployee != null){
                        var sampleCredits = _context.EmployeeCredit.Where(d => d.EmployeeId == sampleEmployee.Id).ToArray();
                        foreach (var item in sampleCredits)
                        {
                            var newCredit = new EmployeeCredit{
                                Employee = dbObj,
                                ActiveCredit = item.ActiveCredit,
                                CreditByRange = item.CreditByRange,
                                CreditEndDate = item.CreditEndDate,
                                CreditLoadDate = item.CreditLoadDate,
                                CreditStartDate = item.CreditStartDate,
                                ItemId = item.ItemId,
                                ItemCategoryId = item.ItemCategoryId,
                                ItemGroupId = item.ItemGroupId,
                                ProductIntervalTime = item.ProductIntervalTime,
                                ProductIntervalType = item.ProductIntervalType,
                                RangeCredit = item.ActiveCredit,
                                RangeIndex = item.RangeIndex,
                                RangeLength = item.RangeLength,
                                RangeType = item.RangeType,
                                SpecificRangeDates = item.SpecificRangeDates,
                            };
                            _context.EmployeeCredit.Add(newCredit);
                        }
                    }
                }

                model.MapTo(dbObj);

                var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == dbObj.PlantId);
                dbPlant.LastUpdateDate = DateTime.Now.AddMinutes(10);

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
        [Route("EditConsume")]
        public BusinessResult EditConsume(MachineItemConsumeModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbEmp = _context.Employee.FirstOrDefault(d => d.Id == model.EmployeeId);
                if (dbEmp == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));


                var dbObj = _context.MachineItemConsume.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));
                }

                // update or delete related employeecreditconsume
                var dbEmpConsume = _context.EmployeeCreditConsume.FirstOrDefault(d => d.EmployeeId == model.EmployeeId
                    && d.ConsumedDate == dbObj.ConsumedDate && d.ItemId == dbObj.ItemId);
                if (dbEmpConsume != null){
                    if (model.MakeDelete == 1){
                        _context.EmployeeCreditConsume.Remove(dbEmpConsume);
                    }
                    else{
                        var dbItem = _context.Item.FirstOrDefault(d => d.Id == model.ItemId);
                        if (dbItem != null){
                            dbEmpConsume.ItemCategoryId = dbItem.ItemCategoryId;
                            dbEmpConsume.ItemGroupId = dbItem.ItemGroupId;
                            dbEmpConsume.ItemId = dbItem.Id;
                        }
                    }
                }

                // update or delete machineitemconsume
                if (model.MakeDelete == 1){
                    _context.MachineItemConsume.Remove(dbObj);
                }
                else{
                    model.MapTo(dbObj);
                }

                // update plant data for machines shall be updated
                var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == dbEmp.PlantId);
                dbPlant.LastUpdateDate = DateTime.Now.AddMinutes(10);

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
                    && d.ItemCategoryId == model.ItemCategoryId && d.ItemGroupId == model.ItemGroupId && d.ItemId == model.ItemId);
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

                var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == dbObj.PlantId);
                dbPlant.LastUpdateDate = DateTime.Now.AddMinutes(10);

                if (!model.CancelSubmit)
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
        public BusinessResult EditCredit(EmployeeCreditModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.Employee.FirstOrDefault(d => d.Id == model.EmployeeId);
                if (dbObj == null){
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));
                }

                var dbCredit = _context.EmployeeCredit.FirstOrDefault(d => d.Id == model.Id);
                if (dbCredit == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                model.UpdateLiveRangeData(_context);
                model.MapTo(dbCredit);

                if (dbCredit.ActiveCredit == 0)
                    dbCredit.RangeCredit = 0;

                var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == dbObj.PlantId);
                dbPlant.LastUpdateDate = DateTime.Now.AddMinutes(10);

                if (!model.CancelSubmit)
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
        [Route("BulkLoadInfo")]
        public EmployeeCreditModel GetBulkLoadInfo(RequestBulkLoadModel model){
            EmployeeCreditModel data = null;

            try
            {
                var groupedInfo = _context.EmployeeCredit
                    .Where(d => model.Employees.Contains(d.EmployeeId ?? 0)
                        && (model.ItemCategoryId == null || d.ItemCategoryId == model.ItemCategoryId)
                        && (model.ItemGroupId == null || d.ItemGroupId == model.ItemGroupId)
                        && (model.ItemId == null || d.ItemId == model.ItemId)
                    )
                    .GroupBy(d => new {
                        d.RangeType,
                        d.RangeLength,
                        d.RangeCredit,
                        d.ProductIntervalType,
                        d.ProductIntervalTime,
                        d.CreditLoadDate,
                        d.CreditEndDate,
                    })
                    .Select(d => new {
                        d.Key.RangeType,
                        d.Key.RangeLength,
                        d.Key.RangeCredit,
                        d.Key.ProductIntervalType,
                        d.Key.ProductIntervalTime,
                        d.Key.CreditLoadDate,
                        d.Key.CreditEndDate,
                        MemberCount = d.Count(),
                        Scheduler = d.Select(m => m.SpecificRangeDates).FirstOrDefault(),
                    })
                    .ToArray();

                var topMember = groupedInfo
                    .Where(d => d.MemberCount > 0)
                    .OrderByDescending(d => d.MemberCount)
                    .FirstOrDefault();
                if (topMember != null){
                    data = new EmployeeCreditModel{
                        Id = 0,
                        ActiveCredit = topMember.RangeCredit,
                        CreditLoadDate = topMember.CreditLoadDate,
                        CreditEndDate = topMember.CreditEndDate,
                        CreditByRange = topMember.RangeCredit,
                        CreditStartDate = topMember.CreditLoadDate,
                        ItemCategoryId = model.ItemCategoryId,
                        ItemGroupId = model.ItemGroupId,
                        ItemId = model.ItemId,
                        ProductIntervalTime = topMember.ProductIntervalTime,
                        ProductIntervalType = topMember.ProductIntervalType,
                        RangeCredit = topMember.RangeCredit,
                        RangeIndex = 0,
                        RangeLength = topMember.RangeLength,
                        RangeType = topMember.RangeType,
                        SpecificRangeDates = topMember.Scheduler,
                    };
                }
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }

        [Authorize(Policy = "FactoryOfficer")]
        [HttpPost]
        [Route("BulkLoad")]
        public BusinessResult PostBulkLoad(EmployeeCreditModel model){
            ResolveHeaders(Request);
            BusinessResult result = new BusinessResult();

            try
            {
                if (model.BulkList == null || model.BulkList.Length == 0)
                    throw new Exception(_translator.Translate(Expressions.AnyError, _userLanguage));

                var prmModel = new EmployeeCreditModel();
                model.MapTo(prmModel);
                prmModel.BulkList = null;
                prmModel.CancelSubmit = true;

                int successCount=0, errorCount = 0;

                foreach (var item in model.BulkList)
                {
                    prmModel.EmployeeId = item;
                    BusinessResult postResult = null;
                    if (_context.EmployeeCredit.Any(d => d.EmployeeId == model.EmployeeId &&
                        d.ItemCategoryId == model.ItemCategoryId && d.ItemGroupId == model.ItemGroupId
                            && d.ItemId == model.ItemId))
                        postResult = this.EditCredit(prmModel);
                    else
                        postResult = this.LoadCredit(prmModel);
                    if (postResult.Result)
                        successCount++;
                    else
                        errorCount++;
                }

                _context.SaveChanges();
                result.Result = true;
                result.InfoMessage = successCount + " adet personele kredi başarıyla yüklendi.";
                if (errorCount > 0)
                    result.InfoMessage += " " + errorCount + " adet personele kredi yükleme işlemi başarısız oldu.";
            }
            catch (System.Exception ex)
            {
                result.Result = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        [HttpGet]
        [Authorize(Policy = "FactoryOfficer")]
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


        [Authorize(Policy = "FactoryOfficer")]
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

        [Authorize(Policy = "FactoryOfficer")]
        [HttpPost]
        [Route("Upload")]
        public BusinessResult UploadData([FromForm]UploadEmployeeModel model){
            ResolveHeaders(Request);
            BusinessResult result = new BusinessResult();

            try
            {
                var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == model.PlantId);
                if (dbPlant == null)
                    throw new Exception(_translator.Translate(Expressions.PlantDoesntExists, _userLanguage));

                if (model.File == null || model.File.Length == 0)
                    throw new Exception(_translator.Translate(Expressions.FileNotFound, _userLanguage));

                List<Employee> newEmployeeList = new List<Employee>();
                List<Department> newDepartmentList = new List<Department>();
                List<EmployeeCard> newCardList = new List<EmployeeCard>();

                Dictionary<string, int> columnItemList = new Dictionary<string, int>();

                int insertedCount = 0;
                int updatedCount = 0;

                using (var wb = new XLWorkbook(model.File.OpenReadStream()))
                {
                    var ws = wb.Worksheet(1);
                    var sheetRows = ws.Rows();
                    var rowIndex = 0;
                    foreach (var row in sheetRows)
                    {
                        if (rowIndex == 0){
                            // map item codes to column indexes
                            int colHeaderIndex = 5;
                            string readColHeader = string.Empty;

                            do {
                                readColHeader = row.Cell(colHeaderIndex).GetValue<string>();

                                if (!string.IsNullOrEmpty(readColHeader))
                                    columnItemList.Add(readColHeader, colHeaderIndex);

                                colHeaderIndex++;
                            } while(!string.IsNullOrEmpty(readColHeader));

                            rowIndex++;
                            continue;
                        }

                        var clCode = row.Cell(1);
                        var clName = row.Cell(2);
                        var clDpt = row.Cell(3);
                        var clCard = row.Cell(4);
                        // var clGsm = row.Cell(5);
                        // var clEmail = row.Cell(6);

                        string dtCode = clCode.GetValue<string>();
                        string dtName = clName.GetValue<string>();
                        string dtDpt = clDpt.GetValue<string>();
                        string dtCard = clCard.GetValue<string>();
                        // string dtGsm = clGsm.GetValue<string>();
                        // string dtEmail = clGsm.GetValue<string>();

                        if (!string.IsNullOrEmpty(dtCode)){
                            if (!newEmployeeList.Any(d => d.EmployeeCode == dtCode)){
                                bool isValid = true;
                                if (string.IsNullOrEmpty(dtName))
                                    isValid = false;
                                if (string.IsNullOrEmpty(dtDpt))
                                    isValid = false;

                                if (isValid){
                                    // check department
                                    var dbDpt = _context.Department.FirstOrDefault(d => (d.DepartmentCode == dtDpt
                                        || d.DepartmentName == dtDpt) && d.PlantId == model.PlantId);
                                    if (dbDpt == null)
                                        dbDpt = newDepartmentList.FirstOrDefault(d => (d.DepartmentCode == dtDpt 
                                            || d.DepartmentName == dtDpt) && d.PlantId == model.PlantId);
                                    if (dbDpt == null){
                                        dbDpt = new Department{
                                            DepartmentCode = dtDpt,
                                            DepartmentName = dtDpt,
                                            IsActive = true,
                                            PlantId = model.PlantId,
                                        };
                                        _context.Department.Add(dbDpt);
                                        newDepartmentList.Add(dbDpt);
                                    }

                                    // check card
                                    EmployeeCard dbCard = null;
                                    if (!string.IsNullOrEmpty(dtCard)){
                                        dbCard = _context.EmployeeCard.FirstOrDefault(d => d.CardCode == dtCard && d.PlantId == model.PlantId);
                                        if (dbCard == null)
                                            dbCard = newCardList.FirstOrDefault(d => d.CardCode == dtCard && d.PlantId == model.PlantId);
                                        if (dbCard == null){
                                            dbCard = new EmployeeCard{
                                                CardCode = dtCard,
                                                IsActive = true,
                                                PlantId = model.PlantId,
                                            };
                                            _context.EmployeeCard.Add(dbCard);
                                            newCardList.Add(dbCard);
                                        }
                                    }

                                    var dbEmployee = _context.Employee.FirstOrDefault(d => d.EmployeeCode == dtCode
                                        && d.PlantId == model.PlantId);
                                    if (dbEmployee == null)
                                        dbEmployee = newEmployeeList.FirstOrDefault(d => d.EmployeeCode == dtCode
                                            && d.PlantId == model.PlantId);
                                    if (dbEmployee == null){
                                        dbEmployee = new Employee{
                                            EmployeeCode = dtCode,
                                            EmployeeName = dtName,
                                            // Gsm = dtGsm,
                                            // Email = dtEmail,
                                            PlantId = model.PlantId,
                                            Department = dbDpt,
                                            EmployeeCard = dbCard,
                                        };

                                        _context.Employee.Add(dbEmployee);
                                        newEmployeeList.Add(dbEmployee);
                                        insertedCount++;
                                    }
                                    else{
                                        dbEmployee.EmployeeName = dtName;
                                        // dbEmployee.Gsm = dtGsm;
                                        // dbEmployee.Email = dtEmail;
                                        dbEmployee.Department = dbDpt;
                                        dbEmployee.EmployeeCard = dbCard;
                                        updatedCount++;
                                    }

                                    // remove old employeeCredits of current employee
                                    if (dbEmployee.Id > 0){
                                        var currentCredits = _context.EmployeeCredit.Where(d => d.EmployeeId == dbEmployee.Id).ToArray();
                                        foreach (var exCrItem in currentCredits)
                                        {
                                            _context.EmployeeCredit.Remove(exCrItem);
                                        }
                                    }

                                    // update credits
                                    foreach (var creditColumn in columnItemList)
                                    {
                                        string crCreditValue = row.Cell(creditColumn.Value).GetValue<string>();
                                        string crItemCode = creditColumn.Key;

                                        if (!string.IsNullOrEmpty(crItemCode) && !string.IsNullOrEmpty(crCreditValue)){
                                            var dbItem = _context.Item.FirstOrDefault(d => d.ItemCode == crItemCode && d.ItemCategory != null && d.ItemCategory.PlantId == model.PlantId);
                                            if (dbItem != null){
                                                EmployeeCreditModel creditModel = new EmployeeCreditModel();

                                                // resolve credit values from string data
                                                var matches = Regex.Matches(crCreditValue, "[0-9]+", RegexOptions.IgnoreCase);
                                                
                                                var rangeLength = 1;
                                                var rangeCredit = 0;
                                                var rangeType = 0;
                                                DateTime rangeLoadDate = DateTime.Now.Date;

                                                if (matches.Count > 1)
                                                {
                                                    rangeLength = Convert.ToInt32(matches[0].Value);
                                                    rangeCredit = Convert.ToInt32(matches[1].Value);
                                                }
                                                else if (matches.Count == 1){
                                                    rangeLength = 1;
                                                    rangeCredit = Convert.ToInt32(matches[0].Value);
                                                }

                                                var periodStr = Regex.Match(crCreditValue, "[A-Za-z]+", RegexOptions.IgnoreCase);
                                                if (periodStr != null && !string.IsNullOrEmpty(periodStr.Value)){
                                                    switch (periodStr.Value)
                                                    {
                                                        case "GUN":
                                                            rangeType = 1;
                                                            rangeLoadDate = DateTime.Now.Date;

                                                            break;
                                                        case "HAFTA":
                                                            rangeType = 2;
                                                            
                                                            var dayIndex = rangeLoadDate.DayOfWeek;
                                                            while (dayIndex != DayOfWeek.Monday){
                                                                rangeLoadDate = rangeLoadDate.AddDays(-1);
                                                                dayIndex = rangeLoadDate.DayOfWeek;
                                                            }

                                                            break;
                                                        case "AY":
                                                            rangeType = 3;
                                                            rangeLoadDate = DateTime.ParseExact("01." 
                                                                + string.Format("{0:MM}", rangeLoadDate) + "." + 
                                                                string.Format("{0:yyyy}", rangeLoadDate), "dd.MM.yyyy",
                                                                    System.Globalization.CultureInfo.GetCultureInfo("tr"));

                                                            break;
                                                        case "SINIRSIZ":
                                                            rangeType = 4;
                                                            break;
                                                        default:
                                                            rangeType = 0;
                                                            break;
                                                    }
                                                }

                                                if (rangeCredit > 0 && rangeType > 0){
                                                    EmployeeCredit dbCredit = null;//_context.EmployeeCredit.FirstOrDefault(d => d.ItemId == dbItem.Id);
                                                    if (dbCredit == null)
                                                    {
                                                        dbCredit = new EmployeeCredit{
                                                            ItemId = dbItem.Id,
                                                            ItemCategoryId = dbItem.ItemCategoryId,
                                                            ItemGroupId = dbItem.ItemGroupId,
                                                        };
                                                        dbCredit.Employee = dbEmployee;
                                                        _context.EmployeeCredit.Add(dbCredit);
                                                    }

                                                    // set credit attributes
                                                    dbCredit.RangeCredit = rangeCredit;
                                                    dbCredit.ActiveCredit = rangeCredit;
                                                    dbCredit.CreditByRange = rangeCredit;
                                                    dbCredit.RangeIndex = 0;
                                                    dbCredit.RangeType = rangeType;
                                                    dbCredit.RangeLength = rangeLength;
                                                    dbCredit.CreditLoadDate = rangeLoadDate;
                                                    dbCredit.CreditStartDate = rangeLoadDate;

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
                                        }
                                    }
                                }
                            }
                        }
                        else
                            break;

                        rowIndex++;
                    }
                }

                _context.SaveChanges();
                result.Result = true;
                result.InfoMessage = insertedCount.ToString() +
                    " adet yeni kayıt sisteme transfer edildi ve " +
                    updatedCount.ToString() + " adet kayıt güncellendi.";
            }
            catch (System.Exception ex)
            {
                result.Result = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Export/{plantId}")]
        public IActionResult ExportData(int plantId){
            try
            {
                

            #region PREPARE DATA
            EmployeeModel[] data = new EmployeeModel[0];
            string[] itemCodes = new string[0];
           
            try
            {
                data = _context.Employee
                    .Where(d =>
                       d.PlantId == plantId && (d.EmployeeStatus ?? 0) == 0
                    )
                    .Select(d => new EmployeeModel{
                        Id = d.Id,
                        EmployeeCode = d.EmployeeCode,
                        EmployeeName = d.EmployeeName,
                        EmployeeCardCode = d.EmployeeCard != null ? d.EmployeeCard.CardCode : "",
                        DepartmentName = d.Department != null ? d.Department.DepartmentName : "",
                    })
                    .ToArray();
                
                var empIds = data.Select(d => d.Id).ToArray();

                itemCodes = _context.EmployeeCredit.Where(d => empIds.Contains(d.EmployeeId ?? 0) && d.Item != null)
                    .Select(d => d.Item.ItemCode).Distinct().ToArray();
            }
            catch
            {
                
            }
            #endregion

            #region PREPARE EXCEL FILE
            byte[] excelFile = new byte[0];

            using (var workbook = new XLWorkbook()) {
                var worksheet = workbook.Worksheets.Add("Personel ve İstihkaklar");

                worksheet.Cell(1,1).Value = "SICIL";
                worksheet.Cell(1,2).Value = "ADSOYAD";
                worksheet.Cell(1,3).Value = "KISIM";
                worksheet.Cell(1,4).Value = "KART NO";

                int itemColIndex = 5;
                foreach (var iCode in itemCodes)
                {
                    worksheet.Cell(1,itemColIndex).Value = iCode;
                    itemColIndex++;
                }

                int rowIndex = 2;
                foreach (var emp in data)
                {
                    // worksheet.Cell(rowIndex, 1).DataType = XLDataType.Blank;
                    
                    worksheet.Cell(rowIndex, 1).Style.NumberFormat.Format = "@";
                    worksheet.Cell(rowIndex, 1).SetValue(emp.EmployeeCode).SetDataType(XLDataType.Text);
                    worksheet.Cell(rowIndex, 2).Value = emp.EmployeeName;
                    worksheet.Cell(rowIndex, 3).Value = emp.DepartmentName;

                    worksheet.Cell(rowIndex, 4).Style.NumberFormat.Format = "@";
                    worksheet.Cell(rowIndex, 4).SetValue(emp.EmployeeCardCode).SetDataType(XLDataType.Text);

                    int itemIndex = 5;
                    foreach (var iCode in itemCodes)
                    {
                        string creditText = "";
                        var itemCredit = _context.EmployeeCredit.FirstOrDefault(d => d.Item != null && d.Item.ItemCode == iCode && d.EmployeeId == emp.Id);
                        if (itemCredit != null){
                            if (itemCredit.RangeLength > 1)
                                creditText += itemCredit.RangeLength.ToString();

                            switch (itemCredit.RangeType)
                            {
                                case 1:
                                    creditText += "GUN";
                                    break;
                                case 2:
                                    creditText += "HAFTA";
                                    break;
                                case 3:
                                    creditText += "AY";
                                    break;
                                case 4:
                                    creditText += "SINIRSIZ";
                                    break;
                                default:
                                    break;
                            }

                            creditText += (itemCredit.CreditByRange).ToString();
                        }

                        worksheet.Cell(rowIndex, itemIndex).Value = creditText;
                        itemIndex++;
                    }

                    rowIndex++;
                }

                // worksheet.Cell(2,1).InsertData(data);

                worksheet.Columns().AdjustToContents();

                var titlesStyle = workbook.Style;
                titlesStyle.Font.Bold = true;
                titlesStyle.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Row(1).Style = titlesStyle;

                using (MemoryStream memoryStream = new MemoryStream()) {
                    workbook.SaveAs(memoryStream);
                    excelFile = memoryStream.ToArray();
                }

                return Ok(excelFile);
            }

            #endregion
            
            }
            catch (System.Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Policy = "FactoryOfficer")]
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

                var dbEmp = _context.Employee.FirstOrDefault(d => d.Id == dbObj.EmployeeId);
                var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == dbEmp.PlantId);
                dbPlant.LastUpdateDate = DateTime.Now.AddMinutes(10);

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

                var dbEmp = _context.Employee.FirstOrDefault(d => d.Id == dbObj.EmployeeId);
                var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == dbEmp.PlantId);
                dbPlant.LastUpdateDate = DateTime.Now.AddMinutes(10);

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
                var dbObj = _context.Employee.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                if (_context.EmployeeCreditConsume.Any(d => d.EmployeeId == id) ||
                    _context.MachineItemConsume.Any(d => d.EmployeeId == id)){
                        throw new Exception("Bu personele ait tüketim kayıtları bulunduğu için silme işlemi yapılamaz.");
                    }

                var credits = _context.EmployeeCredit.Where(d => d.EmployeeId == id).ToArray();
                foreach (var item in credits)
                {
                    _context.EmployeeCredit.Remove(item);
                }

                _context.Employee.Remove(dbObj);

                var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == dbObj.PlantId);
                dbPlant.LastUpdateDate = DateTime.Now.AddMinutes(10);

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