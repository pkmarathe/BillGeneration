using System;

namespace BillGeneration.DataContract
{
    public class CategoryMasterDc
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class CategoryRecieptDc
    {
        public int CategoryRecieptId { get; set; }
        public int CategoryId { get; set; }
        public int FontId { get; set; }
        public string BillRecieptName { get; set; }
        public string BillRecieptDynamicHtml { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string BillRecieptSampleImage { get; set; }
        public string RecieptType { get; set; }
        public string BillRecieptPdfHtml { get; set; }
        public bool? IsRecieptLogo { get; set; }
        public string ReceiptWidth { get; set; }
        public string ReceiptHight { get; set; }
    }
    public class CategoryRecieptPDFDc
    {
        public string BillRecieptDynamicHtml { get; set; }
        public string BillRecieptSampleImage { get; set; }
        public string BillRecieptPdfHtml { get; set; }
        public string RecieptType { get; set; }
        public bool? IsRecieptLogo { get; set; }
        public string ReceiptWidth { get; set; }
        public string ReceiptHight { get; set; } 
    }

    public class CategorySearchDc
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string IsActive { get; set; }
    }
}