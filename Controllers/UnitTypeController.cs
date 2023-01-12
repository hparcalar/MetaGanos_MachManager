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
    public class UnitTypeController : MgControllerBase
    {
        public UnitTypeController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        public IEnumerable<UnitTypeModel> Get()
        {
            UnitTypeModel[] data = new UnitTypeModel[0];
            try
            {
                data = _context.UnitType.Select(d => new UnitTypeModel{
                        Id = d.Id,
                        UnitTypeCode = d.UnitTypeCode,
                        IsActive = d.IsActive,
                        UnitTypeName = d.UnitTypeName,
                    }).OrderBy(d => d.UnitTypeCode).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public UnitTypeModel Get(int id)
        {
            UnitTypeModel data = new UnitTypeModel();
            try
            {
                data = _context.UnitType.Where(d => d.Id == id).Select(d => new UnitTypeModel{
                        Id = d.Id,
                        UnitTypeCode = d.UnitTypeCode,
                        IsActive = d.IsActive,
                        UnitTypeName = d.UnitTypeName,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "Dealer")]
        [HttpPost]
        public BusinessResult Post(UnitTypeModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.UnitType.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new UnitType();
                    _context.UnitType.Add(dbObj);
                }

                if (_context.UnitType.Any(d => d.UnitTypeCode == model.UnitTypeCode && d.Id != model.Id))
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
        [HttpDelete("{id}")]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.UnitType.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                _context.UnitType.Remove(dbObj);

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
