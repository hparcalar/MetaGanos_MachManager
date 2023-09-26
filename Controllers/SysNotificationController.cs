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
    public class SysNotificationController : MgControllerBase
    {
        public SysNotificationController(MetaGanosSchema context): base(context){ }

        #region SYSTEM NOTIFICATIONS

        [HttpGet]
        [Route("System/WaitingFor/{userId}")]
        public IEnumerable<SysNotificationModel> GetSystemWaitingFor(int? userId)
        {
            ResolveHeaders(Request);
            SysNotificationModel[] data = new SysNotificationModel[0];
            try
            {
                data = _context.SysNotification.Where(d => d.UserId == userId && (d.NotificationStatus ?? 0) == 0)
                    .Select(d => new SysNotificationModel{
                        Id = d.Id,
                        GotoLink = d.GotoLink,
                        NotificationDate = d.NotificationDate,
                        NotificationStatus = d.NotificationStatus,
                        NotificationType = d.NotificationType,
                        UserId = d.UserId,
                        WarningType = d.WarningType,
                        Content = d.Content,
                        PlantId = d.PlantId,
                        Title = d.Title,
                    }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpPost]
        [Route("System/SetAsSeen")]
        public BusinessResult PostSystemSetAsSeen(SysNotificationModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbNotif = _context.SysNotification.FirstOrDefault(d => d.Id == model.Id);
                if (dbNotif == null)
                    throw new Exception("Bildirim kaydı bulunamadı.");

                dbNotif.NotificationStatus = 1;
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

        #endregion
        
        #region EMPLOYEE NOTIFICATIONS
        [HttpGet]
        [Route("Employee/WaitingFor/{employeeId}")]
        public IEnumerable<EmployeeNotificationModel> GetEmployeeWaitingFor(int? employeeId)
        {
            ResolveHeaders(Request);
            EmployeeNotificationModel[] data = new EmployeeNotificationModel[0];
            try
            {
                DateTime upperLimitForTarget = DateTime.Now.Date;

                var rawData = _context.EmployeeNotification.Where(d => 
                    d.EmployeeId == employeeId 
                    && (d.NotificationStatus ?? 0) == 0)
                    .Select(d => new EmployeeNotificationModel{
                        Id = d.Id,
                        CreatedDate = d.CreatedDate,
                        EmployeeId = d.EmployeeId,
                        TargetDate = d.TargetDate,
                        NotificationStatus = d.NotificationStatus,
                        Content = d.Content,
                        PlantId = d.PlantId,
                        Title = d.Title,
                    }).ToArray();

                // clear out of range ones (setting as seen)
                bool updateRequired = false;
                var outOfRangeData = rawData.Where(d => d.TargetDate != upperLimitForTarget).ToArray();
                foreach (var item in outOfRangeData)
                {
                    var dbNotifOutOfRange = _context.EmployeeNotification.FirstOrDefault(d => d.Id == item.Id);
                    dbNotifOutOfRange.NotificationStatus = 1;
                    updateRequired = true;
                }

                // give updated notifications
                data = rawData.Where(d => !outOfRangeData.Any(m => m.Id == d.Id)).ToArray();

                if (updateRequired)
                    _context.SaveChanges();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpPost]
        [Route("Employee/SetAsSeen")]
        public BusinessResult PostEmployeeSetAsSeen(EmployeeNotificationModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbNotif = _context.EmployeeNotification.FirstOrDefault(d => d.Id == model.Id);
                if (dbNotif == null)
                    throw new Exception("Bildirim kaydı bulunamadı.");

                dbNotif.NotificationStatus = 1;
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

        [HttpPost]
        [Route("Employee")]
        public BusinessResult PostEmployee(EmployeeNotificationModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.EmployeeNotification.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new EmployeeNotification();
                    _context.EmployeeNotification.Add(dbObj);
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

        [HttpDelete("Employee/{id}")]
        public BusinessResult DeleteEmployee(int id){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbNotif = _context.EmployeeNotification.FirstOrDefault(d => d.Id == id);
                if (dbNotif == null)
                    throw new Exception("Bildirim kaydı bulunamadı.");

                _context.EmployeeNotification.Remove(dbNotif);
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
        
        #endregion
    
        #region SYSTEM PUBLICATIONS

        [HttpGet]
        [Route("Publication/WaitingFor/{plantId}")]
        public IEnumerable<SysPublicationModel> GetPublicationWaitingFor(int? plantId)
        {
            ResolveHeaders(Request);
            SysPublicationModel[] data = new SysPublicationModel[0];
            try
            {
                DateTime currentDate = DateTime.Now.Date;

                data = _context.SysPublication.Where(d => 
                    d.PlantId == plantId 
                    && (d.PublicationStatus ?? 0) == 0
                    && d.StartDate <= currentDate && d.EndDate >= currentDate)
                    .Select(d => new SysPublicationModel{
                        Id = d.Id,
                        PublicationStatus = d.PublicationStatus,
                        Attachment = d.Attachment,
                        AttachmentContentType = d.AttachmentContentType,
                        AttachmentFileName = d.AttachmentFileName,
                        EndDate = d.EndDate,
                        PublicationDate = d.PublicationDate,
                        ReplaceWithHomeVideo = d.ReplaceWithHomeVideo,
                        StartDate = d.StartDate,
                        WarningType = d.WarningType,
                        Content = d.Content,
                        PlantId = d.PlantId,
                        Title = d.Title,
                    }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpPost]
        [Route("Publication")]
        public BusinessResult PostPublication(SysPublicationModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.SysPublication.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new SysPublication();
                    _context.SysPublication.Add(dbObj);
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

        [HttpDelete("Publication/{id}")]
        public BusinessResult DeletePublication(int id){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbNotif = _context.SysPublication.FirstOrDefault(d => d.Id == id);
                if (dbNotif == null)
                    throw new Exception("Duyuru kaydı bulunamadı.");

                _context.SysPublication.Remove(dbNotif);
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
        


        #endregion

    }
}