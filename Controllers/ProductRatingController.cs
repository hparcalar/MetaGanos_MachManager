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
    public class ProductRatingController : MgControllerBase
    {
        public ProductRatingController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        public IEnumerable<ProductRatingModel> Get()
        {
            ResolveHeaders(Request);
            ProductRatingModel[] data = new ProductRatingModel[0];
            try
            {
                int[] plants = null;
                if (_isDealer)
                    plants = _context.Plant.Where(d => d.DealerId == _appUserId).Select(d => d.Id).ToArray();
                else if (_isFactoryOfficer)
                    plants = new int[]{ _context.Officer.Where(d => d.Id == _appUserId).Select(d => d.PlantId).First() };

                data = _context.ProductRating.Where(d => plants == null || (plants != null && d.Employee != null && plants.Contains(d.Employee.PlantId ?? 0)))
                    .Select(d => new ProductRatingModel{
                        Id = d.Id,
                        EmployeeId = d.EmployeeId,
                        Explanation = d.Explanation,
                        ItemId = d.ItemId,
                        Rate = d.Rate,
                        RatingDate = d.RatingDate,
                        UserId = d.UserId,
                        ItemCode = d.Item != null ? d.Item.ItemCode : "",
                        ItemName = d.Item != null ? d.Item.ItemName : "",
                        UserName = d.Employee != null ? d.Employee.EmployeeName : "",
                    }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public ProductRatingModel Get(int id)
        {
            ProductRatingModel data = new ProductRatingModel();
            try
            {
                data = _context.ProductRating.Where(d => d.Id == id).Select(d => new ProductRatingModel{
                        Id = d.Id,
                        EmployeeId = d.EmployeeId,
                        Explanation = d.Explanation,
                        ItemId = d.ItemId,
                        Rate = d.Rate,
                        RatingDate = d.RatingDate,
                        UserId = d.UserId,
                        ItemCode = d.Item != null ? d.Item.ItemCode : "",
                        ItemName = d.Item != null ? d.Item.ItemName : "",
                        UserName = d.Employee != null ? d.Employee.EmployeeName : "",
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpPost]
        public BusinessResult Post(ProductRatingModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.ProductRating.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new ProductRating();
                    _context.ProductRating.Add(dbObj);
                }

                model.MapTo(dbObj);

                _context.SaveChanges();

                // update item's averate rating after save
                using (MetaGanosSchema checkContext = SchemaFactory.CreateContext()){
                    using (ProductBO bObj = new ProductBO(checkContext)){
                        bObj.UpdateProductRate(model.ItemId ?? 0);
                    }
                }

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
                var dbObj = _context.ProductRating.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                var itemId = dbObj.ItemId;

                _context.ProductRating.Remove(dbObj);
                _context.SaveChanges();

                // update item's averate rating after delete
                using (MetaGanosSchema checkContext = SchemaFactory.CreateContext()){
                    using (ProductBO bObj = new ProductBO(checkContext)){
                        bObj.UpdateProductRate(itemId ?? 0);
                    }
                }

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