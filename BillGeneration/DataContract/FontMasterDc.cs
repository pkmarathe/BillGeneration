using System;

namespace BillGeneration.DataContract
{
    public class FontMasterDc
    {
        public int FontId { get; set; }
        public string FontStyle { get; set; }
        public string FontLink { get; set; }
        public string FontClass { get; set; }
        public bool? IsActive { get; set; }
    }
    public class FontDropdownDc
    {
        public int FontId { get; set; }
        public string FontStyle { get; set; } 
    }
}