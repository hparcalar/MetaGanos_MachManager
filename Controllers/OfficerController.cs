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
using MachManager.Business;
using MachManager.Authentication;

namespace MachManager.Controllers
{
    [Authorize(Policy = "FactoryOfficer")]
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class OfficerController : MgControllerBase
    {
        public OfficerController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        public IEnumerable<OfficerModel> Get()
        {
            OfficerModel[] data = new OfficerModel[0];
            try
            {
                using (DefinitionListsBO bObj = new DefinitionListsBO(this._context)){
                    data = bObj.GetOfficers();
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
        public int GetOfficerCount(){
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
                    dataCount = bObj.GetOfficerCount(plants);
                }
            }
            catch (System.Exception)
            {
                
            }


            return dataCount;
        }

        [HttpGet]
        [Route("{id}")]
        public OfficerModel Get(int id)
        {
            OfficerModel data = new OfficerModel();
            try
            {
                data = _context.Officer.Where(d => d.Id == id).Select(d => new OfficerModel{
                        Id = d.Id,
                        OfficerCode = d.OfficerCode,
                        IsActive = d.IsActive,
                        OfficerName = d.OfficerName,
                        OfficerPassword = d.OfficerPassword,
                        PlantId = d.PlantId,
                        PlantCode = d.Plant != null ? d.Plant.PlantCode : "",
                        PlantName = d.Plant != null ? d.Plant.PlantName : "",
                    }).FirstOrDefault();

                if (data == null)
                    data = new OfficerModel{
                        AuthUnits = new AuthUnitModel[0],
                    };

                if (data != null){
                    data.AuthUnits = _context.AuthUnit
                        .Where(d => d.OfficerId == data.Id)
                        .Select(d => new AuthUnitModel{
                            Id = d.Id,
                            CanDelete = d.CanDelete,
                            CanRead = d.CanRead,
                            CanWrite = d.CanWrite,
                            OfficerId = d.OfficerId,
                            Section = d.Section,
                        }).ToArray();

                    string[] allAuthUnits = Enum.GetNames(typeof(MgAuthUnit));
                    var nonExistingUnits = allAuthUnits.Where(d => !data.AuthUnits.Any(m => m.Section == d)).ToArray();
                    if (nonExistingUnits != null && nonExistingUnits.Length > 0){
                        data.AuthUnits = data.AuthUnits.Concat(nonExistingUnits.Select(d => new AuthUnitModel{
                            Id = 0,
                            CanDelete = true,
                            CanRead = true,
                            CanWrite = true,
                            OfficerId = data.Id,
                            Section = d,
                        })).ToArray();
                    }

                    foreach (var item in data.AuthUnits)
                    {
                        item.SectionText = _translator.Translate(item.Section, _userLanguage);
                    }
                }
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpPost]
        public BusinessResult Post(OfficerModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                if (model.PlantId <= 0)
                    throw new Exception(_translator.Translate(Expressions.PlantDoesntExists, _userLanguage));

                var dbObj = _context.Officer.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Officer();
                    _context.Officer.Add(dbObj);
                }

                if (_context.Officer.Any(d => d.OfficerCode == model.OfficerCode 
                    && d.PlantId == model.PlantId && d.Id != model.Id))
                    throw new Exception(_translator.Translate(Expressions.SameCodeExists, _userLanguage));

                model.MapTo(dbObj);

                // save auth units
                var oldUnits = _context.AuthUnit.Where(d => d.OfficerId == dbObj.Id).ToArray();
                foreach (var item in oldUnits)
                {
                    _context.AuthUnit.Remove(item);
                }

                if (model.AuthUnits != null){
                    foreach (var item in model.AuthUnits)
                    {
                        var dbAuthUnit = new AuthUnit();
                        item.MapTo(dbAuthUnit);
                        dbAuthUnit.Id = 0;
                        dbAuthUnit.Officer = dbObj;
                        _context.AuthUnit.Add(dbAuthUnit);
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

        [HttpDelete]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.Officer.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                // clear auth units
                var authUnits = _context.AuthUnit.Where(d => d.OfficerId == dbObj.Id).ToArray();
                foreach (var item in authUnits)
                {
                    _context.AuthUnit.Remove(item);
                }

                _context.Officer.Remove(dbObj);

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
