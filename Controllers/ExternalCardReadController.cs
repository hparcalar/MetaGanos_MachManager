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
    public class ExternalCardReadController : MgControllerBase
    {
        public ExternalCardReadController(MetaGanosSchema context): base(context){ }


        [AllowAnonymous]
        [HttpGet("{machineId}")]
        public ExternalCardReadModel Get(int? machineId)
        {
            ExternalCardReadModel resData = new ExternalCardReadModel();

            try
            {
                var dbRead = _context.ExternalCardRead.Where(d => d.MachineId == machineId)
                    .OrderByDescending(d => d.Id)
                    .FirstOrDefault();

                if (dbRead != null){
                    dbRead.MapTo(resData);

                    _context.ExternalCardRead.Remove(dbRead);
                    _context.SaveChanges();
                }
            }
            catch
            {
                
            }
            
            return resData;
        }

        [AllowAnonymous]
        [HttpPost]
        public BusinessResult Post(ExternalCardReadModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbRead = new ExternalCardRead();
                _context.ExternalCardRead.Add(dbRead);

                model.MapTo(dbRead);

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

        
    }
}
