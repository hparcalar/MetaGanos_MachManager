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

namespace MachManager.Business{
    public class DefinitionListsBO : IBusinessObject {
        public DefinitionListsBO(MetaGanosSchema context): base(context){

        }

        public ItemCategoryModel[] GetItemCategories(int[] plants = null){
            ItemCategoryModel[] data = new ItemCategoryModel[0];

            try
            {
                data = _context.ItemCategory
                .Where(d => plants == null || plants.Length == 0 || (plants != null && plants.Contains((d.PlantId ?? 0))))
                    .Select(d => new ItemCategoryModel{
                        Id = d.Id,
                        ControlTimeType = d.ControlTimeType,
                        CreatedDate = d.CreatedDate,
                        IsActive = d.IsActive,
                        ItemCategoryCode = d.ItemCategoryCode,
                        ItemCategoryName = d.ItemCategoryName,
                        ItemChangeTime = d.ItemChangeTime,
                        ViewOrder = d.ViewOrder,
                        CreditRangeType = d.CreditRangeType,
                        CreditByRange = d.CreditByRange,
                        CreditRangeLength = d.CreditRangeLength,
                        PlantId = d.PlantId,
                        PlantCode = d.Plant != null ? d.Plant.PlantCode : "",
                        PlantName = d.Plant != null ? d.Plant.PlantName : "",
                    }).OrderBy(d => d.ItemCategoryCode).ToArray();

            }
            catch (System.Exception)
            {
                
            }

            return data;
        }

        public WarehouseModel[] GetWarehouses(int[] plants = null){
            WarehouseModel[] data = new WarehouseModel[0];

            try
            {
                data = _context.Warehouse
                .Where(d => plants == null || plants.Length == 0 || (plants != null && plants.Contains((d.PlantId ?? 0))))
                    .Select(d => new WarehouseModel{
                        Id = d.Id,
                        DealerId = d.DealerId,
                        PlantId = d.PlantId,
                        WarehouseCode = d.WarehouseCode,
                        WarehouseName = d.WarehouseName,
                        IsActive = d.IsActive,
                        PlantName = d.Plant != null ? d.Plant.PlantName : "",
                    }).OrderBy(d => d.WarehouseCode).ToArray();

            }
            catch (System.Exception)
            {
                
            }

            return data;
        }

        public PlantModel[] GetPlants(int? dealerId = null){
            PlantModel[] data = new PlantModel[0];

            try
            {
                data = _context.Plant
                    .Where(d => dealerId == null || (dealerId != null && d.DealerId == dealerId))
                    .Select(d => new PlantModel{
                        Id = d.Id,
                        CreatedDate = d.CreatedDate,
                        DealerCode = d.Dealer != null ? d.Dealer.DealerCode : "",
                        DealerId = d.DealerId,
                        DealerName = d.Dealer != null ? d.Dealer.DealerName : "",
                        Explanation = d.Explanation,
                        IsActive = d.IsActive,
                        PlantCode = d.PlantCode,
                        PlantName = d.PlantName,
                        Last4CharForCardRead = d.Last4CharForCardRead,
                    }).OrderBy(d => d.PlantCode).ToArray();
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }
    
        public OfficerModel[] GetOfficers(int[] plants = null){
            OfficerModel[] data = new OfficerModel[0];

            try
            {
                data = _context.Officer
                    .Where(d => plants == null || plants.Length == 0 || (plants != null && plants.Contains(d.PlantId)))
                    .Select(d => new OfficerModel{
                        Id = d.Id,
                        OfficerCode = d.OfficerCode,
                        IsActive = d.IsActive,
                        OfficerName = d.OfficerName,
                        OfficerPassword = "",
                        PlantId = d.PlantId,
                        DefaultLanguage = d.DefaultLanguage,
                        PlantCode = d.Plant != null ? d.Plant.PlantCode : "",
                        PlantName = d.Plant != null ? d.Plant.PlantName : "",
                    }).OrderBy(d => d.OfficerCode).ToArray();
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }
    
        public DepartmentModel[] GetDepartments(int[] plants = null){
            DepartmentModel[] data = new DepartmentModel[0];

            try
            {
                data = _context.Department
                    .Where(d => plants == null || plants.Length == 0 || (plants != null && plants.Contains((d.PlantId ?? 0))))
                    .Select(d => new DepartmentModel{
                        Id = d.Id,
                        DepartmentCode = d.DepartmentCode,
                        DepartmentName = d.DepartmentName,
                        IsActive = d.IsActive,
                        PlantCode = d.Plant != null ? d.Plant.PlantCode : "",
                        PlantName = d.Plant != null ? d.Plant.PlantName : "",
                        PlantId = d.PlantId,
                    }).OrderBy(d => d.DepartmentCode).ToArray();
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }
    
        public MachineModel[] GetMachines(int[] plants = null){
            MachineModel[] data = new MachineModel[0];

            try
            {
                data = _context.Machine
                    .Where(d => plants == null || plants.Length == 0 || (plants != null && plants.Contains(d.PlantId ?? 0)))
                    .Select(d => new MachineModel{
                        Id = d.Id,
                        Barcode = d.Barcode,
                        Brand = d.Brand,
                        BrandModel = d.BrandModel,
                        City = d.City,
                        Cols = d.Cols,
                        Country = d.Country,
                        CreatedDate = d.CreatedDate,
                        District = d.District,
                        InventoryCode = d.InventoryCode,
                        InventoryEntryDate = d.InventoryEntryDate,
                        IsActive = d.IsActive,
                        LocationData = d.LocationData,
                        MachineCode = d.MachineCode,
                        MachineName = d.MachineName,
                        PlantCode = d.Plant != null ? d.Plant.PlantCode : "",
                        PlantId = d.PlantId,
                        PlantName = d.Plant != null ? d.Plant.PlantName : "",
                        ProductionDate = d.ProductionDate,
                        Rows = d.Rows,
                        DefaultLanguage = d.DefaultLanguage,
                        SpecialCustomer = d.SpecialCustomer,
                        StartVideoPath = d.StartVideoPath,
                    }).OrderBy(d => d.MachineCode).ToArray();
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }

        public EmployeeModel[] GetEmployees(int[] plants = null, int[] departments = null) {
            EmployeeModel[] data = new EmployeeModel[0];

            try
            {
                data = _context.Employee
                    .Where(d => 
                        (plants == null || plants.Length == 0 || (plants != null && plants.Contains(d.PlantId ?? 0)))
                        &&
                        (d.EmployeeStatus ?? 0) == 0
                        &&
                        (departments == null || departments.Length == 0 || (departments != null && departments.Contains(d.DepartmentId ?? 0)))    
                    )
                    .Select(d => new EmployeeModel{
                        Id = d.Id,
                        ActiveCredit = d.ActiveCredit,
                        DepartmentCode = d.Department != null ? d.Department.DepartmentCode : "",
                        DepartmentId = d.DepartmentId,
                        DepartmentName = d.Department != null ? d.Department.DepartmentName : "",
                        Email = d.Email,
                        EmployeeCardCode = d.EmployeeCard != null ? d.EmployeeCard.CardCode : "",
                        EmployeeCardHex = d.EmployeeCard != null ? d.EmployeeCard.HexKey : "",
                        EmployeeCardId = d.EmployeeCardId,
                        EmployeeCode = d.EmployeeCode,
                        EmployeeName = d.EmployeeName,
                        EmployeePassword = "",
                        Gsm = d.Gsm,
                        IsActive = d.IsActive,
                        PlantCode = d.Plant != null ? d.Plant.PlantCode : "",
                        PlantId = d.PlantId,
                        PlantName = d.Plant != null ? d.Plant.PlantName : "",
                    }).OrderBy(d => d.EmployeeCode).ToArray();
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }

        public ItemModel[] GetItems(int[] plants = null, int[] groups = null, string search = ""){
            ItemModel[] data = new ItemModel[0];

            try
            {
                data = _context.Item
                    .Where(d => (plants == null || plants.Length == 0 || (plants != null && d.ItemCategory != null 
                        && plants.Contains(d.ItemCategory.PlantId ?? 0)))
                        &&
                        (search.Length == 0 || 
                            (
                                EF.Functions.ILike(d.ItemCode, $"%{search}%") || EF.Functions.ILike(d.ItemName, $"%{search}%")
                                // d.ItemCode.Contains(search) || d.ItemName.Contains(search)
                            )
                        )
                        &&
                        (groups == null || groups.Length == 0 || (groups != null && groups.Contains(d.ItemGroupId ?? 0)))
                        )
                    .Select(d => new ItemModel{
                        Id = d.Id,
                        AlternatingCode1 = d.AlternatingCode1,
                        AlternatingCode2 = d.AlternatingCode2,
                        Barcode1 = d.Barcode1,
                        Barcode2 = d.Barcode2,
                        CreatedDate = d.CreatedDate,
                        CriticalMax = d.CriticalMax,
                        CriticalMin = d.CriticalMin,
                        Explanation = d.Explanation,
                        IsActive = d.IsActive,
                        ItemCategoryCode = d.ItemCategory != null ? d.ItemCategory.ItemCategoryCode : "",
                        ItemCategoryId = d.ItemCategoryId,
                        ItemCategoryName = d.ItemCategory != null ? d.ItemCategory.ItemCategoryName : "",
                        ItemCode = d.ItemCode,
                        ItemGroupCode = d.ItemGroup != null ? d.ItemGroup.ItemGroupCode : "",
                        ItemGroupId = d.ItemGroupId,
                        ItemGroupName = d.ItemGroup != null ? d.ItemGroup.ItemGroupName : "",
                        ItemName = d.ItemName,
                        Price1 = d.Price1,
                        Price2 = d.Price2,
                        UnitTypeCode = d.UnitType != null ? d.UnitType.UnitTypeCode : "",
                        UnitTypeId = d.UnitTypeId,
                        UnitTypeName = d.UnitType != null ? d.UnitType.UnitTypeName : "",
                        ViewOrder = d.ViewOrder
                    }).OrderBy(d => d.ItemCode).ToArray();
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }

        public EmployeeCardModel[] GetCards(int[] plants = null, bool availableOnes = false){
            EmployeeCardModel[] data = new EmployeeCardModel[0];

            try
            {
                data = _context.EmployeeCard
                    .Where(d => 
                        (plants == null || plants.Length == 0 || (plants != null && plants.Contains(d.PlantId ?? 0)))
                        &&
                        (!availableOnes || (availableOnes && d.IsActive == true && !d.Employee.Any()))
                    )
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
            catch (System.Exception)
            {
                
            }

            return data;
        }

        public int GetMachineCount(int[] plants = null){
            int dataCount = 0;

            try
            {
                dataCount = _context.Machine
                    .Where(d => plants == null || plants.Length == 0 || (plants != null && plants.Contains(d.PlantId ?? 0)))
                    .Count();
            }
            catch (System.Exception)
            {
                
            }

            return dataCount;
        }
    
        public int GetDepartmentCount(int[] plants = null){
            int dataCount = 0;

            try
            {
                dataCount = _context.Department
                    .Where(d => plants == null || plants.Length == 0 || (plants != null && plants.Contains(d.PlantId ?? 0)))
                    .Count();
            }
            catch (System.Exception)
            {
                
            }

            return dataCount;
        }
    
        public int GetEmployeeCount(int[] plants = null){
            int dataCount = 0;

            try
            {
                dataCount = _context.Employee
                    .Where(d => 
                        (plants == null || plants.Length == 0 || (plants != null && plants.Contains(d.PlantId ?? 0)))
                        &&
                        (d.EmployeeStatus ?? 0) == 0
                        )
                    .Count();
            }
            catch (System.Exception)
            {
                
            }

            return dataCount;
        }

        public int GetOfficerCount(int[] plants = null){
            int dataCount = 0;

            try
            {
                dataCount = _context.Officer
                    .Where(d => plants == null || plants.Length == 0 || (plants != null && plants.Contains(d.PlantId)))
                    .Count();
            }
            catch (System.Exception)
            {
                
            }

            return dataCount;
        }
    
        public int GetItemCount(int[] plants = null){
            int dataCount = 0;

            try
            {
                dataCount = _context.Item
                    .Where(d => plants == null || plants.Length == 0 || (plants != null && d.ItemCategory != null 
                        && plants.Contains(d.ItemCategory.PlantId ?? 0)))
                    .Count();
            }
            catch (System.Exception)
            {
                
            }

            return dataCount;
        }

    }
}