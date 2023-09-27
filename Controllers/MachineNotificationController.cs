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
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using MachManager.Helpers;
using MachManager.Business;
using MachManager.Models.Parameters;

namespace MachManager.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class MachineNotificationController : MgControllerBase
    {
        public MachineNotificationController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        public IEnumerable<NotificationModel> Get()
        {
            NotificationModel[] data = new NotificationModel[0];
            try
            {
                data = _context.Notification.Select(d => new NotificationModel{
                    Id = d.Id,
                    PlantName = d.Plant.PlantName,
                    NotificationTitle = d.NotificationTitle,
                    NotificationMessage = d.NotificationMessage,
                    IsSeen = d.IsSeen,
                    IsDeleted = d.IsDeleted,
                    DeleteDate = d.DeleteDate,
                    CreatedDate = d.CreatedDate
                }).OrderByDescending(d => d.Id).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{plantId}")]
        public IEnumerable<NotificationModel> GetByPlant(int plantId)
        {
            NotificationModel[] data = new NotificationModel[0];
            try
            {
                data = _context.Notification.Where(d => d.PlantId == plantId && d.IsDeleted == false).Select(d => new NotificationModel{
                    Id = d.Id,
                    PlantName = d.Plant.PlantName,
                    NotificationTitle = d.NotificationTitle,
                    NotificationMessage = d.NotificationMessage,
                    IsSeen = d.IsSeen,
                    IsDeleted = d.IsDeleted,
                    DeleteDate = d.DeleteDate,
                    CreatedDate = d.CreatedDate
                }).OrderByDescending(d => d.Id).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("NotificationCount/{plantId}")]
        public NotificationSeenModel GetNotificationCount(int plantId)
        {
            NotificationSeenModel data = new NotificationSeenModel();
            try
            {
                var dbObj = _context.Notification.Where(d => d.PlantId == plantId && d.IsDeleted == false && d.IsSeen != true).Select(d => new NotificationModel{
                    Id = d.Id,
                }).OrderByDescending(d => d.Id).ToArray();

                data.NotificationCount = dbObj.Length;
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "FactoryOfficer")]
        [HttpPost]
        public BusinessResult Post(NotificationModel model){
            BusinessResult result = new BusinessResult();
            
            try
            {
                var dbObj = _context.Notification.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Notification();
                    _context.Notification.Add(dbObj);
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
        [HttpPost]
        [Route("SetAsDeleted/{id}")]
        public BusinessResult SetAsDeleted(int id){
            BusinessResult result = new BusinessResult();
            
            try
            {
                var dbObj = _context.Notification.FirstOrDefault(d => d.Id == id);
                if (dbObj != null){
                    dbObj.IsDeleted = true;
                    dbObj.DeleteDate = DateTime.Now;
                    _context.SaveChanges();
                    result.Result=true;
                    result.RecordId = dbObj.Id;
                }
                else{
                    result.Result=false;
                }

            }
            catch (System.Exception ex)
            {
                result.Result=false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        [Authorize(Policy = "FactoryOfficer")]
        [HttpPost]
        [Route("SetAllDeleted/{id}")]
        public BusinessResult SetAllDeleted(int id){
            BusinessResult result = new BusinessResult();
            
            try
            {
                var data = _context.Notification.Where(d => d.IsDeleted != true && d.PlantId == id).Select(d => new NotificationModel{
                    Id = d.Id,
                }).OrderByDescending(d => d.Id).ToArray();

                foreach (var item in data)
                {
                    var dbObj = _context.Notification.FirstOrDefault(d => d.Id == item.Id);
                    dbObj.IsDeleted = true;
                    dbObj.DeleteDate = DateTime.Now;
                }

                _context.SaveChanges();
                result.Result=true;
                result.RecordId = 1;
            }
            catch (System.Exception ex)
            {
                result.Result=false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        [Authorize(Policy = "FactoryOfficer")]
        [HttpPost]
        [Route("SetAsSeen")]
        public BusinessResult SetAsSeen(NotificationSeenModel model){
            BusinessResult result = new BusinessResult();
            
            try
            {
                var data = _context.Notification.Where(d => d.IsSeen != true && d.PlantId == model.PlantId).Select(d => new NotificationModel{
                    Id = d.Id,
                }).OrderByDescending(d => d.Id).ToArray();

                foreach (var item in data)
                {
                    var dbObj = _context.Notification.FirstOrDefault(d => d.Id == item.Id);
                    dbObj.IsSeen = true;
                }

                _context.SaveChanges();
                result.Result=true;
                if(data.Length > 0){
                    result.RecordId = 1;
                }
                else{
                result.RecordId = 0;
                }

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
