using System;
using System.Collections.Generic;
using System.Linq;
using MachManager.Context;
using MachManager.Models.Constants;

namespace MachManager.Models{
    public class EmployeeCreditModel{
        public int Id { get; set; }
        public int ActiveCredit { get; set; } = 0;
        public Nullable<int> ItemCategoryId { get; set; }
        public Nullable<int> ItemGroupId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public int RangeCredit { get; set; } = 0;
        public int RangeIndex { get; set; } = 0;
        public int RangeType { get; set; } = 4;
        public int RangeLength { get; set; } = 1;
        public int CreditByRange { get; set; } = 0;
        public Nullable<DateTime> CreditLoadDate { get; set; }
        public Nullable<DateTime> CreditStartDate { get; set; }
        public Nullable<DateTime> CreditEndDate { get; set; }
        public Nullable<int> ProductIntervalType { get; set; }
        public Nullable<int> ProductIntervalTime { get; set; }
        public string SpecificRangeDates { get; set; }

        #region VISUAL ELEMENTS
        public string ItemCategoryCode { get; set; }
        public string ItemCategoryName { get; set; }
        public string ItemGroupCode { get; set; }
        public string ItemGroupName { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public bool CancelSubmit { get; set; } = false;
        public int[] BulkList { get; set; }
        #endregion

        #region BUSINESS LOGIC
        public void UpdateLiveRangeData(MetaGanosSchema context) {
            try
            {
                // updates live rangeIndex & remaining rangeCredit

                var activeStart = (DateTime?)this.CreditLoadDate.Value.Date;
                DateTime? activeEnd = GetRangedDate(this.CreditLoadDate);

                if (activeEnd != null){
                    var dtNow = DateTime.Now.Date;
                    this.CreditEndDate = activeEnd;

                    var rangeCount = Convert.ToInt32((this.ActiveCredit) / (this.CreditByRange));
                    int rangeIndex = 0;
                    int loopCredit = this.ActiveCredit;

                    while (!(dtNow >= activeStart && dtNow <= activeEnd)){
                        activeStart = GetRangedDate(activeStart);
                        activeEnd = GetRangedDate(activeEnd);
                        rangeIndex++;
                        loopCredit -= this.CreditByRange;

                        if (rangeCount < rangeIndex && !(dtNow >= activeStart && dtNow <= activeEnd)){
                            rangeIndex = -1;
                            break;
                        }
                    }

                    if (loopCredit > 0){
                        this.RangeIndex = rangeIndex;
                        this.CreditStartDate = activeStart;
                        this.CreditEndDate = activeEnd;
                    }
                    
                    if (rangeIndex > -1){
                        var consumedCreditAtCurrentRange = context
                            .EmployeeCreditConsume.Where(d => d.EmployeeId == this.EmployeeId
                                && d.ItemCategoryId == this.ItemCategoryId
                                && (d.ItemGroupId == this.ItemGroupId || this.ItemGroupId == null)
                                && (d.ItemId == this.ItemId || this.ItemId == null)
                                && d.ConsumedDate >= activeStart
                                && d.ConsumedDate <= activeEnd)
                                .Sum(d => d.ConsumedCredit);

                        this.RangeCredit = (loopCredit > this.CreditByRange ? this.CreditByRange : loopCredit) - consumedCreditAtCurrentRange;
                        if (this.RangeCredit < 0)
                            this.RangeCredit = 0;
                    }
                    else
                        this.RangeCredit = 0;

                    if (this.ActiveCredit == 0)
                        this.RangeCredit = 0;
                }
            }
            catch (System.Exception)
            {
                
            }
        }

        private DateTime? GetRangedDate(DateTime? startDate){
            try
            {
                DateTime? activeEnd = null;
                switch (this.RangeType)
                {
                    case CreditRangeOption.DAILY:
                        activeEnd = startDate.Value.AddDays(this.RangeLength).Date;
                        break;
                    case CreditRangeOption.WEEKLY:
                        activeEnd = startDate.Value.AddDays(this.RangeLength * 7).Date;
                        break;
                    case CreditRangeOption.MONTHLY:
                        activeEnd = startDate.Value.AddMonths(this.RangeLength).Date;
                        break;
                    case CreditRangeOption.INDEFINITE:
                        activeEnd = DateTime.MaxValue;
                        break;
                    default:
                        break;
                }

                return activeEnd;
            }
            catch (System.Exception)
            {
                
            }

            return null;
        }
        #endregion
    }
}