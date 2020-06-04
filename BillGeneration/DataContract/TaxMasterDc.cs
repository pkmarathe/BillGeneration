using System;

namespace BillGeneration.DataContract
{
    public class TaxMasterDc
    {
        public int TaxId { get; set; }
        public string Tax { get; set; } 
        public bool? IsActive { get; set; } 
    }
}