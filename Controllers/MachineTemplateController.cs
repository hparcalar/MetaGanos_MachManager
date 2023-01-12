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
    public class MachineTemplateController : MgControllerBase
    {
        public MachineTemplateController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        public IEnumerable<MachineTemplateModel> Get()
        {
            MachineTemplateModel[] data = new MachineTemplateModel[0];
            try
            {
                data = _context.MachineTemplate.Select(d => new MachineTemplateModel{
                        Id = d.Id,
                        BrandModel = d.BrandModel,
                        Cols = d.Cols,
                        DealerId = d.DealerId,
                        Rows = d.Rows,
                        SpiralConf = d.SpiralConf,
                        TemplateName = d.TemplateName,
                    }).OrderBy(d => d.Id).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public MachineTemplateModel Get(int id)
        {
            MachineTemplateModel data = new MachineTemplateModel();
            try
            {
                data = _context.MachineTemplate.Where(d => d.Id == id).Select(d => new MachineTemplateModel{
                        Id = d.Id,
                        BrandModel = d.BrandModel,
                        Cols = d.Cols,
                        DealerId = d.DealerId,
                        Rows = d.Rows,
                        SpiralConf = d.SpiralConf,
                        TemplateName = d.TemplateName,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "Dealer")]
        [HttpPost]
        public BusinessResult Post(MachineTemplateModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.MachineTemplate.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new MachineTemplate();
                    _context.MachineTemplate.Add(dbObj);
                }

                if (_context.MachineTemplate.Any(d => d.TemplateName == model.TemplateName && d.DealerId == model.DealerId && d.Id != model.Id))
                    throw new Exception(_translator.Translate(Expressions.SameCodeExists, _userLanguage));

                model.MapTo(dbObj);

                // auto assign dealerId
                if (_isDealer && _appUserId != null && _context.Dealer.Any(d => d.Id == _appUserId)){
                    dbObj.DealerId = _appUserId;
                }
                else if (_isFactoryOfficer && _appUserId != null){
                    var dbOfficer = _context.Officer.FirstOrDefault(d => d.Id == _appUserId);
                    if (dbOfficer != null && dbOfficer.PlantId != null){
                        var dbPlant = _context.Plant.FirstOrDefault(d => d.Id == dbOfficer.PlantId);
                        if (dbPlant != null && dbPlant.DealerId != null){
                            dbObj.DealerId = dbPlant.DealerId;
                        }
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

        [Authorize(Policy = "Dealer")]
        [HttpDelete("{id}")]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.MachineTemplate.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                _context.MachineTemplate.Remove(dbObj);

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
