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
            ResolveHeaders(Request);
            EmployeeCardModel[] data = new EmployeeCardModel[0];
            try
            {
                int[] plants = null;
                if (_isDealer)
                    plants = _context.Plant.Where(d => d.DealerId == _appUserId).Select(d => d.Id).ToArray();
                else if (_isFactoryOfficer)
                    plants = new int[]{ _context.Officer.Where(d => d.Id == _appUserId).Select(d => d.PlantId).First() };

                using (DefinitionListsBO bObj = new DefinitionListsBO(this._context)){
                    data = bObj.GetCards(plants);
                }
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
            ResolveHeaders(Request);
            EmployeeCardModel[] data = new EmployeeCardModel[0];
            try
            {
                using (DefinitionListsBO bObj = new DefinitionListsBO(this._context)){
                    data = bObj.GetCards(new int[]{ id }, true);
                }
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

        [Authorize(Policy = "FactoryOfficer")]
        [HttpPost]
        public BusinessResult Post(EmployeeCardModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                if (model.PlantId == null)
                    throw new Exception("Bir hata oluştu. Lütfen tekrar deneyiniz.");

                var dbObj = _context.EmployeeCard.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new EmployeeCard();
                    _context.EmployeeCard.Add(dbObj);
                }

                var dbOther = _context.Employee.FirstOrDefault(d => d.EmployeeCard != null && d.EmployeeCard.CardCode == model.CardCode
                    && d.PlantId == model.PlantId);

                if (_context.EmployeeCard.Any(d => d.CardCode == model.CardCode
                    && d.PlantId == model.PlantId && d.Id != model.Id))
                    throw new Exception(_translator.Translate(Expressions.SameCodeExists, _userLanguage) + 
                    " Ait olduğu kişi: " + dbOther.EmployeeName);

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
        [HttpDelete]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

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