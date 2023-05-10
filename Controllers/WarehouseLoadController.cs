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
    public class WarehouseLoadController : MgControllerBase
    {
        public WarehouseLoadController(MetaGanosSchema context): base(context){ }

        [HttpGet]
        [Route("List/{loadType}")]
        public IEnumerable<WarehouseLoadHeaderModel> Get(int loadType)
        {
            ResolveHeaders(Request);
            WarehouseLoadHeaderModel[] data = new WarehouseLoadHeaderModel[0];
            try
            {
                int[] plants = null;
                if (_isDealer)
                    plants = _context.Plant.Where(d => d.DealerId == _appUserId).Select(d => d.Id).ToArray();
                else if (_isFactoryOfficer)
                    plants = new int[]{ _context.Officer.Where(d => d.Id == _appUserId).Select(d => d.PlantId).First() };

                data = _context.WarehouseLoadHeader.Where(d => 
                    (plants == null || (plants != null && plants.Contains(d.PlantId ?? 0)))
                        && d.LoadType == loadType
                    )
                    .Select(d => new WarehouseLoadHeaderModel{
                        Id = d.Id,
                        DocumentNo = d.DocumentNo,
                        Explanation = d.Explanation,
                        FirmId = d.FirmId,
                        LoadDate = d.LoadDate,
                        LoadOfficerId = d.LoadOfficerId,
                        LoadType = d.LoadType,
                        PlantCode = d.Plant !=null ? d.Plant.PlantCode : "",
                        PlantName = d.Plant != null ? d.Plant.PlantName : "",
                        PlantId = d.PlantId,
                        ReceiptNo = d.ReceiptNo,
                        WarehouseId = d.WarehouseId,
                        FirmCode = d.Firm != null ? d.Firm.FirmCode : "",
                        FirmName = d.Firm != null ? d.Firm.FirmName : "",
                        OfficerCode = d.Officer != null ? d.Officer.OfficerCode : "",
                        OfficerName = d.Officer != null ? d.Officer.OfficerName : "",
                        WarehouseCode = d.Warehouse != null ? d.Warehouse.WarehouseCode : "",
                        WarehouseName = d.Warehouse != null ? d.Warehouse.WarehouseName : "",
                        InLoadHeaderId = d.InLoadHeaderId,
                        IsGenerated = d.IsGenerated,
                        ItemOrderId = d.ItemOrderId,
                        OutLoadHeaderId = d.OutLoadHeaderId,
                        OutWarehouseId = d.OutWarehouseId,
                        LoadTypeText = d.LoadType == 1 ? "Depo Giriş Fişi" : d.LoadType == 2 ? "Depo Çıkış Fişi" : d.LoadType == 3 ? "Depo Transfer Fişi" 
                            : d.LoadType == 4 ? "Depo Transfer Girişi" : d.LoadType == 5 ? "Depo Transfer Çıkışı" : "",
                    }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("ListAll")]
        public IEnumerable<WarehouseLoadHeaderModel> GetAll()
        {
            ResolveHeaders(Request);
            WarehouseLoadHeaderModel[] data = new WarehouseLoadHeaderModel[0];
            try
            {
                int[] plants = null;
                if (_isDealer)
                    plants = _context.Plant.Where(d => d.DealerId == _appUserId).Select(d => d.Id).ToArray();
                else if (_isFactoryOfficer)
                    plants = new int[]{ _context.Officer.Where(d => d.Id == _appUserId).Select(d => d.PlantId).First() };

                data = _context.WarehouseLoadHeader.Where(d => 
                    (d.LoadType != 4 && d.LoadType != 5) &&
                    (plants == null || (plants != null && plants.Contains(d.PlantId ?? 0)))
                    )
                    .Select(d => new WarehouseLoadHeaderModel{
                        Id = d.Id,
                        DocumentNo = d.DocumentNo,
                        Explanation = d.Explanation,
                        FirmId = d.FirmId,
                        LoadDate = d.LoadDate,
                        LoadOfficerId = d.LoadOfficerId,
                        LoadType = d.LoadType,
                        PlantCode = d.Plant !=null ? d.Plant.PlantCode : "",
                        PlantName = d.Plant != null ? d.Plant.PlantName : "",
                        PlantId = d.PlantId,
                        ReceiptNo = d.ReceiptNo,
                        WarehouseId = d.WarehouseId,
                        FirmCode = d.Firm != null ? d.Firm.FirmCode : "",
                        FirmName = d.Firm != null ? d.Firm.FirmName : "",
                        OfficerCode = d.Officer != null ? d.Officer.OfficerCode : "",
                        OfficerName = d.Officer != null ? d.Officer.OfficerName : "",
                        WarehouseCode = d.Warehouse != null ? d.Warehouse.WarehouseCode : "",
                        WarehouseName = d.Warehouse != null ? d.Warehouse.WarehouseName : "",
                        InLoadHeaderId = d.InLoadHeaderId,
                        IsGenerated = d.IsGenerated,
                        ItemOrderId = d.ItemOrderId,
                        OutLoadHeaderId = d.OutLoadHeaderId,
                        OutWarehouseId = d.OutWarehouseId,
                        LoadTypeText = d.LoadType == 1 ? "Depo Giriş Fişi" : d.LoadType == 2 ? "Depo Çıkış Fişi" : d.LoadType == 3 ? "Depo Transfer Fişi" 
                            : d.LoadType == 4 ? "Depo Transfer Girişi" : d.LoadType == 5 ? "Depo Transfer Çıkışı" : "",
                    })
                    .OrderByDescending(d => d.ReceiptNo)
                    .ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public WarehouseLoadHeaderModel GetById(int id)
        {
            ResolveHeaders(Request);
            WarehouseLoadHeaderModel data = new WarehouseLoadHeaderModel();
            try
            {
                data = _context.WarehouseLoadHeader.Where(d => d.Id == id).Select(d => new WarehouseLoadHeaderModel{
                        Id = d.Id,
                        DocumentNo = d.DocumentNo,
                        Explanation = d.Explanation,
                        FirmId = d.FirmId,
                        LoadDate = d.LoadDate,
                        LoadOfficerId = d.LoadOfficerId,
                        LoadType = d.LoadType,
                        PlantId = d.PlantId,
                        ReceiptNo = d.ReceiptNo,
                        WarehouseId = d.WarehouseId,
                        FirmCode = d.Firm != null ? d.Firm.FirmCode : "",
                        FirmName = d.Firm != null ? d.Firm.FirmName : "",
                        OfficerCode = d.Officer != null ? d.Officer.OfficerCode : "",
                        OfficerName = d.Officer != null ? d.Officer.OfficerName : "",
                        WarehouseCode = d.Warehouse != null ? d.Warehouse.WarehouseCode : "",
                        WarehouseName = d.Warehouse != null ? d.Warehouse.WarehouseName : "",
                        InLoadHeaderId = d.InLoadHeaderId,
                        IsGenerated = d.IsGenerated,
                        ItemOrderId = d.ItemOrderId,
                        OutLoadHeaderId = d.OutLoadHeaderId,
                        OutWarehouseId = d.OutWarehouseId,
                        LoadTypeText = d.LoadType == 1 ? "Depo Giriş Fişi" : d.LoadType == 2 ? "Depo Çıkış Fişi" : d.LoadType == 3 ? "Depo Transfer Fişi" 
                            : d.LoadType == 4 ? "Depo Transfer Girişi" : d.LoadType == 5 ? "Depo Transfer Çıkışı" : "",
                    }).FirstOrDefault();

                if (data != null && data.Id > 0){
                    data.Details = _context.WarehouseLoad.Where(d => d.WarehouseLoadHeaderId == id)
                        .Select(d => new WarehouseLoadModel{
                            Id = d.Id,
                            WarehouseLoadHeaderId = d.WarehouseLoadHeaderId,
                            ItemCategoryCode = d.Item.ItemCategory != null ? d.Item.ItemCategory.ItemCategoryCode : "",
                            ItemCategoryName = d.Item.ItemCategory != null ? d.Item.ItemCategory.ItemCategoryName : "",
                            ItemGroupCode = d.Item.ItemGroup != null ? d.Item.ItemGroup.ItemGroupCode : "",
                            ItemGroupName = d.Item.ItemGroup != null ? d.Item.ItemGroup.ItemGroupName : "",
                            ItemCode = d.Item.ItemCode,
                            ItemName = d.Item.ItemName,
                            ItemId = d.ItemId,
                            LoadDate = d.LoadDate,
                            LoadType = d.LoadType,
                            Quantity = d.Quantity,
                            WarehouseId = d.WarehouseId,
                            MachineCode = d.Machine != null ? d.Machine.MachineCode : "",
                            MachineId = d.MachineId,
                            MachineName = d.Machine != null ? d.Machine.MachineName : "",
                            LoadTypeText = d.LoadType == 1 ? "Giriş" : d.LoadType == 2 ? "Çıkış" : "",
                        }).ToArray();
                }
                else{
                    if (data == null)
                        data = new WarehouseLoadHeaderModel();

                    data.ReceiptNo = GetNextReceiptNo();
                    data.Details = new WarehouseLoadModel[0];
                }
            }
            catch
            {
                
            }
            
            return data;
        }

        int __lastProducedReceiptNo = 0;
        private string GetNextReceiptNo(){
            try
            {
                int[] plants = null;
                if (_isDealer)
                    plants = _context.Plant.Where(d => d.DealerId == _appUserId).Select(d => d.Id).ToArray();
                else if (_isFactoryOfficer)
                    plants = new int[]{ _context.Officer.Where(d => d.Id == _appUserId).Select(d => d.PlantId).First() };

                int nextNumber = 1;
                var lastRecord = _context.WarehouseLoadHeader.Where(d => plants == null || (plants != null && plants.Contains(d.PlantId ?? 0)))
                    .OrderByDescending(d => d.ReceiptNo).Select(d => d.ReceiptNo).FirstOrDefault();
                if (lastRecord != null && !string.IsNullOrEmpty(lastRecord))
                    nextNumber = Convert.ToInt32(lastRecord) + 1;

                // offline incrementing on multiple calls in a request
                if (__lastProducedReceiptNo == 0)
                    __lastProducedReceiptNo = nextNumber;
                else{
                    __lastProducedReceiptNo++;
                    nextNumber = __lastProducedReceiptNo;
                }

                return string.Format("{0:000000}", nextNumber);
            }
            catch (System.Exception)
            {
                
            }

            return string.Empty;
        }

        [Authorize(Policy = "FactoryOfficer")]
        [HttpPost]
        public BusinessResult Post(WarehouseLoadHeaderModel model){
            BusinessResult result = new BusinessResult();
            ResolveHeaders(Request);

            try
            {
                if ((model.PlantId ?? 0) <= 0)
                    throw new Exception(_translator.Translate(Expressions.PlantDoesntExists, _userLanguage));

                var dbObj = _context.WarehouseLoadHeader.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new WarehouseLoadHeader();
                    _context.WarehouseLoadHeader.Add(dbObj);
                    model.ReceiptNo = GetNextReceiptNo();
                    dbObj.ReceiptNo = model.ReceiptNo;
                }

                // keep constants
                var currentRcNo = dbObj.ReceiptNo;
                model.MapTo(dbObj);

                // replace constants after auto mapping
                dbObj.ReceiptNo = currentRcNo;

                #region SAVE DETAILS
                var currentDetails = _context.WarehouseLoad.Where(d => d.WarehouseLoadHeaderId == dbObj.Id).ToArray();

                var removedDetails = currentDetails.Where(d => !model.Details.Any(m => m.Id == d.Id)).ToArray();
                foreach (var item in removedDetails)
                {
                    _context.WarehouseLoad.Remove(item);
                }

                foreach (var item in model.Details)
                {
                    var dbDetail = _context.WarehouseLoad.FirstOrDefault(d => d.Id == item.Id);
                    if (dbDetail == null){
                        dbDetail = new WarehouseLoad();
                        _context.WarehouseLoad.Add(dbDetail);
                    }

                    item.MapTo(dbDetail);
                    dbDetail.LoadType = dbObj.LoadType;
                    dbDetail.LoadDate = dbObj.LoadDate;
                    dbDetail.WarehouseId = dbObj.WarehouseId;
                    dbDetail.OfficerId = dbObj.LoadOfficerId;
                    dbDetail.WarehouseLoadHeader = dbObj;
                }
                #endregion

                #region PROCESS IF TRANSFER RECEIPT TYPE

                if (dbObj.LoadType == 3){
                    // in receipt process
                    var inReceipt = dbObj.InLoadHeader;
                    if (inReceipt == null){
                        inReceipt = new WarehouseLoadHeader();
                        inReceipt.ReceiptNo = GetNextReceiptNo();
                        inReceipt.WarehouseId = dbObj.WarehouseId;
                        inReceipt.LoadDate = dbObj.LoadDate;
                        inReceipt.PlantId = dbObj.PlantId;
                        inReceipt.LoadOfficerId = dbObj.LoadOfficerId;
                        inReceipt.LoadType = 4;
                        inReceipt.IsGenerated = true;
                        
                        _context.WarehouseLoadHeader.Add(inReceipt);
                        dbObj.InLoadHeader = inReceipt;
                    }
                    inReceipt.WarehouseId = dbObj.WarehouseId;

                    // in receipt details
                    var _inCurrentDetails = _context.WarehouseLoad.Where(d => d.WarehouseLoadHeaderId == inReceipt.Id).ToArray();
                    foreach (var item in _inCurrentDetails)
                    {
                        _context.WarehouseLoad.Remove(item);
                    }

                    foreach (var item in model.Details)
                    {
                        var dbDetail = new WarehouseLoad();
                        _context.WarehouseLoad.Add(dbDetail);

                        item.MapTo(dbDetail);
                        dbDetail.LoadType = inReceipt.LoadType;
                        dbDetail.LoadDate = inReceipt.LoadDate;
                        dbDetail.WarehouseId = inReceipt.WarehouseId;
                        dbDetail.OfficerId = inReceipt.LoadOfficerId;
                        dbDetail.WarehouseLoadHeader = inReceipt;
                    }
                    
                    // out receipt process
                    var outReceipt = dbObj.OutLoadHeader;
                    if (outReceipt == null){
                        outReceipt = new WarehouseLoadHeader();
                        outReceipt.ReceiptNo = GetNextReceiptNo();
                        outReceipt.WarehouseId = dbObj.OutWarehouseId;
                        outReceipt.LoadDate = dbObj.LoadDate;
                        outReceipt.PlantId = dbObj.PlantId;
                        outReceipt.LoadOfficerId = dbObj.LoadOfficerId;
                        outReceipt.LoadType = 5;
                        outReceipt.IsGenerated = true;
                        
                        _context.WarehouseLoadHeader.Add(outReceipt);
                        dbObj.OutLoadHeader = outReceipt;
                    }
                    outReceipt.WarehouseId = dbObj.OutWarehouseId;

                    // out receipt details
                    var _outCurrentDetails = _context.WarehouseLoad.Where(d => d.WarehouseLoadHeaderId == outReceipt.Id).ToArray();
                    foreach (var item in _outCurrentDetails)
                    {
                        _context.WarehouseLoad.Remove(item);
                    }

                    foreach (var item in model.Details)
                    {
                        var dbDetail = new WarehouseLoad();
                        _context.WarehouseLoad.Add(dbDetail);

                        item.MapTo(dbDetail);
                        dbDetail.LoadType = outReceipt.LoadType;
                        dbDetail.LoadDate = outReceipt.LoadDate;
                        dbDetail.WarehouseId = outReceipt.WarehouseId;
                        dbDetail.OfficerId = outReceipt.LoadOfficerId;
                        dbDetail.WarehouseLoadHeader = outReceipt;
                    }
                }

                #endregion

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
                var dbObj = _context.WarehouseLoadHeader.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception(_translator.Translate(Expressions.RecordNotFound, _userLanguage));

                // delete if its a warehouse transfer receipt
                if (dbObj.LoadType == 3){
                    var inReceipt = _context.WarehouseLoadHeader.FirstOrDefault(d => d.Id == dbObj.InLoadHeaderId);
                    var inDetails = _context.WarehouseLoad.Where(d => d.WarehouseLoadHeaderId == inReceipt.Id).ToArray();
                    foreach (var item in inDetails)
                    {
                        _context.WarehouseLoad.Remove(item);
                    }
                    _context.WarehouseLoadHeader.Remove(inReceipt);

                    var outReceipt = _context.WarehouseLoadHeader.FirstOrDefault(d => d.Id == dbObj.OutLoadHeaderId);
                    var outDetails = _context.WarehouseLoad.Where(d => d.WarehouseLoadHeaderId == outReceipt.Id).ToArray();
                    foreach (var item in outDetails)
                    {
                        _context.WarehouseLoad.Remove(item);
                    }
                    _context.WarehouseLoadHeader.Remove(outReceipt);
                }

                var details = _context.WarehouseLoad.Where(d => d.WarehouseLoadHeaderId == id).ToArray();
                foreach (var item in details)
                {
                    _context.WarehouseLoad.Remove(item);
                }

                _context.WarehouseLoadHeader.Remove(dbObj);

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
