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
using MachManager.Models.ReportContainers;

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
            ResolveHeaders(Request);
            PlantModel[] data = new PlantModel[0];
            try
            {
                using (DefinitionListsBO bObj = new DefinitionListsBO(this._context)){
                    data = bObj.GetPlants();
                }
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Find/{dealerCode}/{plantCode}")]
        public PlantModel Find(string dealerCode, string plantCode){
            PlantModel data = new PlantModel();
            try
            {
                var dbDealer = _context.Dealer.FirstOrDefault(d => d.DealerCode == dealerCode);
                if (dbDealer == null)
                    throw new Exception("");

                data = _context.Plant.Where(d => d.PlantCode == plantCode && d.DealerId == dbDealer.Id).Select(d => new PlantModel{
                        Id = d.Id,
                        CreatedDate = d.CreatedDate,
                        DealerCode = d.Dealer != null ? d.Dealer.DealerCode : "",
                        DealerId = d.DealerId,
                        DealerName = d.Dealer != null ? d.Dealer.DealerName : "",
                        Explanation = d.Explanation,
                        PlantLogo = d.PlantLogo,
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
        [AllowAnonymous]
        [Route("LastUpdate/{dealerCode}/{plantCode}")]
        public string GetLastUpdate(string dealerCode, string plantCode){
            string lastUpdateData = "";
            try
            {
                var dbDealer = _context.Dealer.FirstOrDefault(d => d.DealerCode == dealerCode);
                if (dbDealer == null)
                    throw new Exception("");

                var dbPlant = _context.Plant.Where(d => d.PlantCode == plantCode && d.DealerId == dbDealer.Id).Select(d => new PlantModel{
                        LastUpdateDate = d.LastUpdateDate
                    }).FirstOrDefault();

                if (dbPlant != null && dbPlant.LastUpdateDate != null){
                    lastUpdateData = string.Format("{0:yyyy-MM-dd HH:mm:ss}", dbPlant.LastUpdateDate);
                }
            }
            catch (System.Exception)
            {
                
            }
            return lastUpdateData;
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
                        PlantLogo = d.PlantLogo,
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

        [AllowAnonymous]
        [HttpGet]
        [Route("Count")]
        public int GetPlantCount(){
            ResolveHeaders(Request);
            int plantCount = 0;

            try
            {
                if (_isDealer)
                    plantCount = _context.Plant.Where(d => d.DealerId == _appUserId).Count();
            }
            catch (System.Exception)
            {
                
            }

            return plantCount;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("OverallStats")]
        public PlantOverallStats GetOverallStats(int[] plants){
            PlantOverallStats data = new PlantOverallStats();

            try
            {
                var mostConsumedItem = _context.MachineItemConsume
                    .Where(d => plants.Contains(d.Machine.PlantId ?? 0) && d.Item != null)
                    .GroupBy(d => new {
                        ItemName = d.Item.ItemName
                    })
                    .Select(d => new {
                        ItemName = d.Key.ItemName,
                        Count = d.Sum(m => m.ConsumedCount)
                    })
                    .OrderByDescending(d => d.Count)
                    .FirstOrDefault();
                if (mostConsumedItem != null){
                    data.MostConsumedItemName = mostConsumedItem.ItemName;
                    data.MostConsumedItemCount = mostConsumedItem.Count;
                }

                data.ActiveMachineCount = _context.Machine
                    .Where(d => plants.Contains(d.PlantId ?? 0) && d.IsActive == true).Count();
                data.TotalMachineCount = _context.Machine
                    .Where(d => plants.Contains(d.PlantId ?? 0)).Count();

                data.InFaultSpiralCount = _context.MachineSpiral
                    .Where(d => plants.Contains(d.Machine.PlantId ?? 0) && d.IsInFault == true).Count();

                var yearStart = DateTime.ParseExact("01.01." + string.Format("{0:yyyy}", DateTime.Now), "dd.MM.yyyy",
                    System.Globalization.CultureInfo.GetCultureInfo("tr"));
                var yearEnd = DateTime.ParseExact("31.12." + string.Format("{0:yyyy}", DateTime.Now), "dd.MM.yyyy",
                    System.Globalization.CultureInfo.GetCultureInfo("tr"));

                List<CategoryStatsYearly> yearlyStats = new List<CategoryStatsYearly>();
                var allCategories = _context.ItemCategory.Where(d => plants.Contains(d.PlantId ?? 0)).ToArray();
                foreach (var cat in allCategories)
                {
                    CategoryStatsYearly catStat = new CategoryStatsYearly{
                        CategoryName = cat.ItemCategoryName,
                    };

                    List<int> catConsumings = new List<int>();
                    for (int i = 0; i < 12; i++)
                    {
                        var monthStart = yearStart.AddMonths(i);
                        var monthEnd = yearStart.AddMonths(i + 1);

                        int monthConsumed = _context.MachineItemConsume.Where(d => d.Item != null 
                            && d.Item.ItemCategoryId == cat.Id
                            && d.ConsumedDate >= monthStart && d.ConsumedDate <= monthEnd)
                            .Sum(d => d.ConsumedCount);
                        catConsumings.Add(monthConsumed);
                    }

                    catStat.MonthlyStats = catConsumings.ToArray();
                    yearlyStats.Add(catStat);
                }

                data.CategoryStats = yearlyStats.ToArray();
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }

        [HttpGet]
        [Authorize(Policy = "FactoryOfficer")]
        [Route("{id}/Officers")]
        public IEnumerable<OfficerModel> GetOfficers(int id){
            OfficerModel[] data = new OfficerModel[0];

            try
            {
                using (DefinitionListsBO bObj = new DefinitionListsBO(this._context)){
                    data = bObj.GetOfficers(new int[]{ id });
                }
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }

        [HttpGet]
        [Authorize(Policy = "FactoryOfficer")]
        [Route("{id}/Machines")]
        public IEnumerable<MachineModel> GetMachines(int id){
            MachineModel[] data = new MachineModel[0];

            try
            {
                using (DefinitionListsBO bObj = new DefinitionListsBO(this._context)){
                    data = bObj.GetMachines(new int[]{ id });
                }
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }


        [HttpGet]
        [Authorize(Policy = "Machine")]
        [Route("{id}/ItemCategories")]
        public IEnumerable<ItemCategoryModel> GetItemCategories(int id){
            ItemCategoryModel[] data = new ItemCategoryModel[0];

            try
            {
                using (DefinitionListsBO bObj = new DefinitionListsBO(this._context)){
                    data = bObj.GetItemCategories(new int[]{ id });
                }
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }

        [HttpGet]
        [Authorize(Policy = "FactoryOfficer")]
        [Route("{id}/Departments")]
        public IEnumerable<DepartmentModel> GetDepartments(int id)
        {
            DepartmentModel[] data = new DepartmentModel[0];
            
            try
            {
                using (DefinitionListsBO bObj = new DefinitionListsBO(this._context)){
                    data = bObj.GetDepartments(new int[]{ id });
                }
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Authorize(Policy = "FactoryOfficer")]
        [Route("{id}/PrintFiles")]
        public IEnumerable<PlantPrintFileModel> GetPrintFiles(int id){
            PlantPrintFileModel[] data = new PlantPrintFileModel[0];
            
            try
            {
                data = _context.PlantPrintFile.Where(d => d.PlantId == id).Select(d => new PlantPrintFileModel{
                        Id = d.Id,
                        PrintFileCode = d.PrintFileCode,
                        PrintFileName = d.PrintFileName,
                        IsActive = d.IsActive,
                        PlantId = d.PlantId,
                        CreatedDate = d.CreatedDate,
                        Explanation = d.Explanation,
                        ImageData = d.ImageData,
                    }).OrderBy(d => d.PrintFileCode).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Authorize(Policy = "FactoryOfficer")]
        [Route("{plantId}/PrintFile/{id}")]
        public PlantPrintFileModel GetPrintFiles(int plantId, int id){
            PlantPrintFileModel data = new PlantPrintFileModel();
            
            try
            {
                data = _context.PlantPrintFile.Where(d => d.Id == id).Select(d => new PlantPrintFileModel{
                        Id = d.Id,
                        PrintFileCode = d.PrintFileCode,
                        PrintFileName = d.PrintFileName,
                        IsActive = d.IsActive,
                        PlantId = d.PlantId,
                        CreatedDate = d.CreatedDate,
                        Explanation = d.Explanation,
                        ImageData = d.ImageData,
                    }).OrderBy(d => d.PrintFileCode).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Authorize(Policy = "FactoryOfficer")]
        [Route("{id}/FileProcess")]
        public IEnumerable<PlantFileProcessModel> GetFileProcess(int id){
            PlantFileProcessModel[] data = new PlantFileProcessModel[0];

            try
            {
                data = _context.PlantFileProcess.Where(d => d.PlantPrintFile.PlantId == id)
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
                    }).ToArray();

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

        [HttpPost]
        [Authorize(Policy = "FactoryOfficer")]
        [Route("{id}/PrintFiles")]
        public BusinessResult PostPrintFiles(int id, PlantPrintFileModel[] model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == id);

                // remove extracted ones
                var currentList = _context.PlantPrintFile.Where(d => d.PlantId == id).ToArray();
                var extracteds = currentList.Where(d => !model.Any(m => m.Id == d.Id)).ToArray();
                foreach (var item in extracteds)
                {
                    if (_context.PlantFileProcess.Any(d => d.PlantPrintFileId == item.Id))
                        throw new Exception(_translator.Translate(Expressions.RecordWasUsedAndCannotBeDeleted, _userLanguage));

                    _context.PlantPrintFile.Remove(item);
                }

                // save new data
                foreach (var item in model)
                {
                    var dbFile = _context.PlantPrintFile.FirstOrDefault(d => d.Id == item.Id);
                    if (dbFile == null){
                        dbFile = new PlantPrintFile{
                            Plant = dbPlant,
                            CreatedDate = DateTime.Now,
                            Explanation = item.Explanation,
                            ImageData = item.ImageData,
                            IsActive = item.IsActive,
                            PrintFileCode = item.PrintFileCode,
                            PrintFileName = item.PrintFileName,
                        };

                        _context.PlantPrintFile.Add(dbFile);
                    }
                    else{
                        item.MapTo(dbFile);
                        dbFile.Plant = dbPlant;
                    }
                }

                // save changes
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

        [HttpPost]
        [Authorize(Policy = "FactoryOfficer")]
        [Route("{id}/PrintFile")]
        public BusinessResult PostPrintFiles(int id, PlantPrintFileModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == id);

                var dbFile = _context.PlantPrintFile.FirstOrDefault(d => d.Id == model.Id);
                if (dbFile == null){
                    dbFile = new PlantPrintFile{
                        Plant = dbPlant,
                        CreatedDate = DateTime.Now,
                        Explanation = model.Explanation,
                        ImageData = model.ImageData,
                        IsActive = model.IsActive,
                        PrintFileCode = model.PrintFileCode,
                        PrintFileName = model.PrintFileName,
                    };

                    _context.PlantPrintFile.Add(dbFile);
                }
                else{
                    model.MapTo(dbFile);
                    dbFile.Plant = dbPlant;
                }

                // save changes
                _context.SaveChanges();

                result.RecordId = dbFile.Id;
                result.Result = true;
            }
            catch (System.Exception ex)
            {
                result.Result = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        [Authorize(Policy = "Dealer")]
        [HttpPost]
        public BusinessResult Post(PlantModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

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

        [Authorize(Policy = "FactoryOfficer")]
        [HttpDelete]
        [Route("{plantId}/PrintFile/{id}")]
        public BusinessResult DeletePrintFile(int plantId, int id){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbFile = _context.PlantPrintFile.FirstOrDefault(d => d.Id == id);
                if (dbFile == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                if (_context.PlantFileProcess.Any(d => d.PlantPrintFileId == dbFile.Id))
                    throw new Exception(_translator.Translate(Expressions.RecordWasUsedAndCannotBeDeleted, _userLanguage));

                _context.PlantPrintFile.Remove(dbFile);
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
