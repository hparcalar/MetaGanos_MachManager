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

namespace MachManager.Controllers
{
    [Authorize(Policy = "Dealer")]
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class DealerController : MgControllerBase
    {
        public DealerController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        public IEnumerable<DealerModel> Get()
        {
            DealerModel[] data = new DealerModel[0];
            try
            {
                data = _context.Dealer.Select(d => new DealerModel{
                        Id = d.Id,
                        DealerCode = d.DealerCode,
                        DealerName = d.DealerName,
                        Explanation = d.Explanation,
                        IsActive = d.IsActive,
                        ParentDealerId = d.ParentDealerId,
                    }).OrderBy(d => d.DealerCode).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public DealerModel Get(int id)
        {
            DealerModel data = new DealerModel();
            try
            {
                data = _context.Dealer.Where(d => d.Id == id).Select(d => new DealerModel{
                        Id = d.Id,
                        DealerCode = d.DealerCode,
                        DealerName = d.DealerName,
                        Explanation = d.Explanation,
                        IsActive = d.IsActive,
                        ParentDealerId = d.ParentDealerId,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpPost]
        public BusinessResult Post(DealerModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request.Headers);

            try
            {
                var dbObj = _context.Dealer.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Dealer();
                    _context.Dealer.Add(dbObj);
                }

                if (_context.Dealer.Any(d => d.DealerCode == model.DealerCode && d.Id != model.Id))
                    throw new Exception(_translator.Translate(Expressions.SameCodeExists, _userLanguage));

                dbObj.DealerCode = model.DealerCode;
                dbObj.DealerName = model.DealerName;
                dbObj.ParentDealerId = model.ParentDealerId;
                dbObj.IsActive = model.IsActive;
                dbObj.DealerPassword = model.DealerPassword;
                dbObj.Explanation = model.Explanation;

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
                var dbObj = _context.Dealer.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                _context.Dealer.Remove(dbObj);

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
