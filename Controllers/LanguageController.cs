using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using MachManager.Context;
using MachManager.Models;
using MachManager.Models.Operational;
using MachManager.Controllers.Base;
using MachManager.i18n;
using Microsoft.AspNetCore.Cors;

namespace MachManager.Controllers
{
    [Authorize(Policy = "Dealer")]
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class LanguageController : MgControllerBase
    {
        public LanguageController(MetaGanosSchema context) : base(context){ }

        [HttpGet]
        public IEnumerable<SysLangModel> Get()
        {
            SysLangModel[] data = new SysLangModel[0];
            try
            {
                data = _context.SysLang.Select(d => new SysLangModel{
                        Id = d.Id,
                        LanguageCode = d.LanguageCode,
                        LanguageName = d.LanguageName,
                        IsDefault = d.IsDefault,
                        IsActive = d.IsActive,
                    }).OrderBy(d => d.LanguageCode).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public SysLangModel Get(int id)
        {
            SysLangModel data = new SysLangModel();
            try
            {
                data = _context.SysLang.Where(d => d.Id == id).Select(d => new SysLangModel{
                        Id = d.Id,
                        LanguageCode = d.LanguageCode,
                        LanguageName = d.LanguageName,
                        IsDefault = d.IsDefault,
                        IsActive = d.IsActive,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpPost]
        public BusinessResult Post(SysLangModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request.Headers);

            try
            {
                var dbObj = _context.SysLang.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new SysLang();
                    _context.SysLang.Add(dbObj);
                }

                if (_context.SysLang.Any(d => d.LanguageCode == model.LanguageCode && d.Id != model.Id))
                    throw new Exception(_translator.Translate(Expressions.SameCodeExists, _userLanguage));

                dbObj.LanguageCode = model.LanguageCode;
                dbObj.LanguageName = model.LanguageName;
                dbObj.IsDefault = model.IsDefault;
                dbObj.IsActive = model.IsActive;

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

        [HttpDelete]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request.Headers);

            try
            {
                var dbObj = _context.SysLang.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                // clear language dictionary before remove it
                if (_context.SysLangDict.Any(d => d.SysLangId == id)){
                    var langDict = _context.SysLangDict.Where(d => d.SysLangId == id).ToArray();
                    foreach (var item in langDict)
                    {
                        _context.SysLangDict.Remove(item);
                    }
                }

                // remove language
                _context.SysLang.Remove(dbObj);

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
