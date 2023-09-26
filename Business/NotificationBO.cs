using System;
using System.Collections.Generic;
using System.Collections;
using MachManager.Context;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using MachManager.Models;
using MachManager.Models.Operational;
using MachManager.Helpers;
using MachManager.Business.Base;
using MachManager.Models.PagedModels;

namespace MachManager.Business{
    public class NotificationBO : IBusinessObject{
        public NotificationBO(MetaGanosSchema context): base(context){

        }

        public BusinessResult CreateNotification(SysNotificationModel model, bool commit = true){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.SysNotification.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new SysNotification();
                    _context.SysNotification.Add(dbObj);
                }

                model.MapTo(dbObj);

                if (commit)
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
    }
}