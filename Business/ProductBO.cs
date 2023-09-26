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
    public class ProductBO : IBusinessObject {
        public ProductBO(MetaGanosSchema context): base(context){

        }

        public BusinessResult UpdateProductRate(int itemId){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbItem = _context.Item.FirstOrDefault(d => d.Id == itemId);
                if (dbItem == null)
                    throw new Exception("Ürün tanımı bulunamadı.");

                double? avgRate = _context.ProductRating.Where(d => d.ItemId == itemId)
                    .Average(d => d.Rate);
                dbItem.AverageRating = Convert.ToDecimal(avgRate ?? 0);

                _context.SaveChanges();
                result.Result = true;
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