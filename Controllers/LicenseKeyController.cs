using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using MachManager.Context;
using MachManager.Models;
using MachManager.Helpers;
using MachManager.Models.Operational;
using MachManager.Models.Parameters;
using MachManager.Controllers.Base;
using MachManager.i18n;
using Microsoft.AspNetCore.Cors;
using MachManager.Business;

namespace MachManager.Controllers
{
    [Authorize(Policy = "Dealer")]
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class LicenseKeyController : MgControllerBase
    {
        public LicenseKeyController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        public IEnumerable<LicenseKeyModel> Get()
        {
            LicenseKeyModel[] data = new LicenseKeyModel[0];
            try
            {
                data = _context.LicenseKey.Select(d => new LicenseKeyModel{
                        Id = d.Id,
                        LicenseCode = d.LicenseCode,
                        DealerCode = d.DealerCode,
                        DealerName = d.DealerName,
                        Explanation = d.Explanation,
                        IsActive = d.IsActive,
                        LastValidation = d.LastValidation,
                    }).OrderBy(d => d.DealerCode).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("ValidateRemoteLicense")]
        public BusinessResult ValidateRemoteLicense(ValidateLicenseModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbLic = _context.LicenseKey.FirstOrDefault(d => d.LicenseCode == model.LicenseCode
                    && d.DealerCode == model.DealerCode);
                if (dbLic == null)
                    throw new Exception("Lisans bilgisi bulunamadı.");

                if (!dbLic.IsActive)
                    throw new Exception("Lisansınız aktif değil.");

                dbLic.LastValidation = DateTime.Now;
                _context.SaveChanges();

                result.Result=true;
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
        public LicenseKeyModel Get(int id)
        {
            LicenseKeyModel data = new LicenseKeyModel();
            try
            {
                data = _context.LicenseKey.Where(d => d.Id == id).Select(d => new LicenseKeyModel{
                        Id = d.Id,
                        LicenseCode = d.LicenseCode,
                        DealerCode = d.DealerCode,
                        DealerName = d.DealerName,
                        Explanation = d.Explanation,
                        IsActive = d.IsActive,
                        LastValidation = d.LastValidation,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpPost]
        public BusinessResult Post(LicenseKeyModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.LicenseKey.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new LicenseKey();
                    _context.LicenseKey.Add(dbObj);
                }

                if (_context.LicenseKey.Any(d => d.LicenseCode == model.LicenseCode))
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

        [HttpDelete("{id}")]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.LicenseKey.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                _context.LicenseKey.Remove(dbObj);

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