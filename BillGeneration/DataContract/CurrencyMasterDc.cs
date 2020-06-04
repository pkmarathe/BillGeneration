using System;

namespace BillGeneration.DataContract
{
    public class CurrencyMasterDc
    {
        public int CurrencyId { get; set; }
        public string Currency { get; set; } 
        public bool? IsActive { get; set; } 
    }
}