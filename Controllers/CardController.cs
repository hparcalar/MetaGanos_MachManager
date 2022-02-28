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
    public class CardController : MgControllerBase
    {
        public CardController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        public IEnumerable<EmployeeCardModel> Get()
        {
            EmployeeCardModel[] data = new EmployeeCardModel[0];
            try
            {
                data = _context.EmployeeCard.Select(d => new EmployeeCardModel{
                        Id = d.Id,
                        CardCode = d.CardCode,
                        HexKey = d.HexKey,
                        IsActive = d.IsActive,
                        PlantCode = d.Plant != null ? d.Plant.PlantCode : "",
                        PlantId = d.PlantId,
                        PlantName = d.Plant != null ? d.Plant.PlantName : "",
                    }).OrderBy(d => d.CardCode).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("Available/{id}")]
        public IEnumerable<EmployeeCardModel> GetAvailables(int id)
        {
            EmployeeCardModel[] data = new EmployeeCardModel[0];
            try
            {
                data = _context.EmployeeCard
                        .Where(d => d.IsActive == true && !d.Employee.Any() && d.PlantId == id)
                        .Select(d => new EmployeeCardModel{
                            Id = d.Id,
                            CardCode = d.CardCode,
                            HexKey = d.HexKey,
                            IsActive = d.IsActive,
                            PlantCode = d.Plant != null ? d.Plant.PlantCode : "",
                            PlantId = d.PlantId,
                            PlantName = d.Plant != null ? d.Plant.PlantName : "",
                    }).OrderBy(d => d.CardCode).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public EmployeeCardModel Get(int id)
        {
            EmployeeCardModel data = new EmployeeCardModel();
            try
            {
                data = _context.EmployeeCard.Where(d => d.Id == id).Select(d => new EmployeeCardModel{
                        Id = d.Id,
                        CardCode = d.CardCode,
                        HexKey = d.HexKey,
                        IsActive = d.IsActive,
                        PlantCode = d.Plant != null ? d.Plant.PlantCode : "",
                        PlantId = d.PlantId,
                        PlantName = d.Plant != null ? d.Plant.PlantName : "",
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "Dealer")]
        [HttpPost]
        public BusinessResult Post(EmployeeCardModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request.Headers);

            try
            {
                var dbObj = _context.EmployeeCard.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new EmployeeCard();
                    _context.EmployeeCard.Add(dbObj);
                }

                if (_context.EmployeeCard.Any(d => d.CardCode == model.CardCode && d.Id != model.Id))
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
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request.Headers);

            try
            {
                var dbObj = _context.EmployeeCard.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                if (_context.Employee.Any(d => d.EmployeeCardId == dbObj.Id))
                    throw new Exception(_translator.Translate(Expressions.CardIsBoundToEmployee, _userLanguage));

                var matchHistory = _context.EmployeeCardMatchHistory.Where(d => d.EmployeeCardId == dbObj.Id).ToArray();
                foreach (var item in matchHistory)
                {
                    _context.EmployeeCardMatchHistory.Remove(item);
                }

                _context.EmployeeCard.Remove(dbObj);

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
