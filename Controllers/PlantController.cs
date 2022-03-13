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

        [HttpGet]
        [Authorize(Policy = "Dealer")]
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
        [Authorize(Policy = "Dealer")]
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
        [Authorize(Policy = "Dealer")]
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
        [Authorize(Policy = "Dealer")]
        [Route("{id}/PrintFiles")]
        public BusinessResult PostPrintFiles(int id, PlantPrintFileModel[] model){
            BusinessResult result = new BusinessResult();

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
        [Authorize(Policy = "Dealer")]
        [Route("{id}/PrintFile")]
        public BusinessResult PostPrintFiles(int id, PlantPrintFileModel model){
            BusinessResult result = new BusinessResult();

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
        [Route("{plantId}/PrintFile/{id}")]
        public BusinessResult DeletePrintFile(int plantId, int id){
            BusinessResult result = new BusinessResult();

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
