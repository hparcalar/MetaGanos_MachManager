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
    public class FirmController : MgControllerBase
    {
        public FirmController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        public IEnumerable<FirmModel> Get()
        {
            ResolveHeaders(Request);
            FirmModel[] data = new FirmModel[0];
            try
            {
                int[] plants = null;
                if (_isDealer)
                    plants = _context.Plant.Where(d => d.DealerId == _appUserId).Select(d => d.Id).ToArray();
                else if (_isFactoryOfficer)
                    plants = new int[]{ _context.Officer.Where(d => d.Id == _appUserId).Select(d => d.PlantId).First() };

                data = _context.Firm.Where(d => plants == null || (plants != null && plants.Contains(d.PlantId ?? 0)))
                    .Select(d => new FirmModel{
                        Id = d.Id,
                        ConnectionProtocol = d.ConnectionProtocol,
                        CreatedDate = d.CreatedDate,
                        DebitFormSamplePath = d.DebitFormSamplePath,
                        FirmCode = d.FirmCode,
                        FirmLogo = d.FirmLogo,
                        FirmName = d.FirmName,
                        IsActive = d.IsActive,
                        PlantId = d.PlantId,
                    }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public FirmModel Get(int id)
        {
            FirmModel data = new FirmModel();
            try
            {
                data = _context.Firm.Where(d => d.Id == id).Select(d => new FirmModel{
                        Id = d.Id,
                        ConnectionProtocol = d.ConnectionProtocol,
                        CreatedDate = d.CreatedDate,
                        DebitFormSamplePath = d.DebitFormSamplePath,
                        FirmCode = d.FirmCode,
                        FirmLogo = d.FirmLogo,
                        FirmName = d.FirmName,
                        IsActive = d.IsActive,
                        PlantId = d.PlantId,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "FactoryOfficer")]
        [HttpPost]
        public BusinessResult Post(FirmModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                if ((model.PlantId ?? 0) <= 0)
                    throw new Exception(_translator.Translate(Expressions.PlantDoesntExists, _userLanguage));

                var dbObj = _context.Firm.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Firm();
                    _context.Firm.Add(dbObj);
                }

                if (_context.Firm.Any(d => d.FirmCode == model.FirmCode 
                    && d.PlantId == model.PlantId && d.Id != model.Id))
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
        [Route("{id}")]
        [HttpDelete("{id}")]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.Firm.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                // if (_context.EmployeeCreditConsume.Any(d => d.ItemCategoryId == id))
                //     throw new Exception("Bu kategoriye ilişkin tüketim kayıtları olduğu için silinemez.");

                // if (_context.EmployeeCredit.Any(d => d.ItemCategoryId == id))
                //     throw new Exception("Bu kategoriye ilişkin verilen krediler olduğu için silinemez.");

                // if (_context.ItemGroup.Any(d => d.ItemCategoryId == id))
                //     throw new Exception("Bu kategoriye ait alt gruplar olduğu için silinemez.");

                _context.Firm.Remove(dbObj);

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
