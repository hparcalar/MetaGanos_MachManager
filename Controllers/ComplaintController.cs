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
    public class ComplaintController : MgControllerBase
    {
        public ComplaintController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        public IEnumerable<ComplaintModel> Get()
        {
            ResolveHeaders(Request);
            ComplaintModel[] data = new ComplaintModel[0];
            try
            {
                int[] plants = null;
                if (_isDealer)
                    plants = _context.Plant.Where(d => d.DealerId == _appUserId).Select(d => d.Id).ToArray();
                else if (_isFactoryOfficer)
                    plants = new int[]{ _context.Officer.Where(d => d.Id == _appUserId).Select(d => d.PlantId).First() };

                data = _context.Complaint.Where(d => plants == null || (plants != null && plants.Contains(d.PlantId ?? 0)))
                    .Select(d => new ComplaintModel{
                        Id = d.Id,
                        ComplaintCode = d.ComplaintCode,
                        ComplaintDate = d.ComplaintDate,
                        ComplaintStatus = d.ComplaintStatus,
                        Content = d.Content,
                        ItemId = d.ItemId,
                        OwnerEmployeeId = d.OwnerEmployeeId,
                        OwnerUserId = d.OwnerUserId,
                        PlantId = d.PlantId,
                        Title = d.Title,
                        StatusText = (d.ComplaintStatus ?? 0) == 0 ? "Bekleniyor" : (d.ComplaintStatus ?? 0) == 1 ? "Çalışılıyor" : "Çözüldü",
                        UserName = d.Employee != null ? d.Employee.EmployeeName : d.Officer != null ? d.Officer.OfficerName : "",
                    }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public ComplaintModel Get(int id)
        {
            ComplaintModel data = new ComplaintModel();
            try
            {
                data = _context.Complaint.Where(d => d.Id == id).Select(d => new ComplaintModel{
                        Id = d.Id,
                        ComplaintCode = d.ComplaintCode,
                        ComplaintDate = d.ComplaintDate,
                        ComplaintStatus = d.ComplaintStatus,
                        Content = d.Content,
                        ItemId = d.ItemId,
                        OwnerEmployeeId = d.OwnerEmployeeId,
                        OwnerUserId = d.OwnerUserId,
                        PlantId = d.PlantId,
                        Title = d.Title,
                        StatusText = (d.ComplaintStatus ?? 0) == 0 ? "Bekleniyor" : (d.ComplaintStatus ?? 0) == 1 ? "Çalışılıyor" : "Çözüldü",
                        UserName = d.Employee != null ? d.Employee.EmployeeName : d.Officer != null ? d.Officer.OfficerName : "",
                    }).FirstOrDefault();

                if (data == null){
                    data = new ComplaintModel{
                        ComplaintCode = GenerateComplaintCode()
                    };
                }
            }
            catch
            {
                
            }
            
            return data;
        }

        private string GenerateComplaintCode(){
            try
            {
                int nextNumber = 1;
                var lastRecord = _context.Complaint
                    .OrderBy(d => d.ComplaintCode)
                    .FirstOrDefault();

                if (lastRecord != null){
                    nextNumber = Convert.ToInt32(lastRecord.ComplaintCode);
                    nextNumber++;
                }

                return string.Format("{0:000000}", nextNumber);
            }
            catch (System.Exception)
            {
                
            }
            
            return string.Empty;
        }

        [HttpPost]
        public BusinessResult Post(ComplaintModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                if ((model.PlantId ?? 0) <= 0)
                    throw new Exception(_translator.Translate(Expressions.PlantDoesntExists, _userLanguage));

                var dbObj = _context.Complaint.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Complaint();
                    _context.Complaint.Add(dbObj);
                }

                // generate new complaint code
                if (dbObj.Id == 0){
                    model.ComplaintCode = GenerateComplaintCode();
                }

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
        [Route("{id}")]
        [HttpDelete("{id}")]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                var dbObj = _context.Complaint.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                _context.Complaint.Remove(dbObj);

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