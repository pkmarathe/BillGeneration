using BillGeneration.BusinessLayer;
using BillGeneration.DataContract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.Http;
using System.Web;
using System.IO;
using System.Data;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Net.Http;
using Newtonsoft.Json;
using System.Configuration;
using System.Linq;

namespace BillGeneration.Controllers
{
    //[RoutePrefix("api/billreciept")]
    public class BillRecieptController : ApiController
    {
        [AllowAnonymous]

        #region Bill Reciept

        #region Add Update Bill Reciept
        [HttpPost]
        public int AddUpdateBillReciept()
        { 
            BillRecieptManager BillRecieptManager = new BillRecieptManager();
            MasterManager MasterManager = new MasterManager();
            string strResult = string.Empty;
            BillRecieptDc BillRecieptDc = null;
            HttpResponseMessage response = new HttpResponseMessage();
            var httpRequest = HttpContext.Current.Request;
            BillRecieptDc BRDc = null;
            FontMasterDc FontDc = null;
            try
            {
                string requestString = httpRequest.Headers["RequestModel"];
                BillRecieptDc = JsonConvert.DeserializeObject<BillRecieptDc>(requestString);

                if (httpRequest.Files.Count > 0)
                {
                    HttpFileCollection files = httpRequest.Files;
                    foreach (string key in files)
                    {
                        HttpPostedFile file = files[key];
                        string postedFile = file.FileName;
                        string filePath = "";
                        filePath = HttpContext.Current.Server.MapPath("~/RecieptLogo/" + postedFile.Replace(" ", ""));

                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                        file.SaveAs(filePath);
                    }
                }
                else
                {
                    if (BillRecieptDc.BillRecieptId > 0)
                        BillRecieptDc.RecieptLogo = BillRecieptManager.GetBillRecieptById(BillRecieptDc.BillRecieptId).RecieptLogo;
                    else
                        BillRecieptDc.RecieptLogo = "";
                }

                #region Check zero or null validation
                BillRecieptDc.BaseFare = (BillRecieptDc.BaseFare <= 0 || BillRecieptDc.BaseFare == null) ? 0 : BillRecieptDc.BaseFare;
                BillRecieptDc.DistanceFare = (BillRecieptDc.DistanceFare <= 0 || BillRecieptDc.DistanceFare == null) ? 0 : BillRecieptDc.DistanceFare;
                BillRecieptDc.TimeFare = (BillRecieptDc.TimeFare <= 0 || BillRecieptDc.TimeFare == null) ? 0 : BillRecieptDc.TimeFare;
                BillRecieptDc.SubCharges = (BillRecieptDc.SubCharges <= 0 || BillRecieptDc.SubCharges == null) ? 0 : BillRecieptDc.SubCharges;
                BillRecieptDc.SafeRideFee = (BillRecieptDc.SafeRideFee <= 0 || BillRecieptDc.SafeRideFee == null) ? 0 : BillRecieptDc.SafeRideFee;
                BillRecieptDc.LiftLineDiscountPrice = (BillRecieptDc.LiftLineDiscountPrice <= 0 || BillRecieptDc.LiftLineDiscountPrice == null) ? 0 : BillRecieptDc.LiftLineDiscountPrice;
                BillRecieptDc.PromotionalPricing = (BillRecieptDc.PromotionalPricing <= 0 || BillRecieptDc.PromotionalPricing == null) ? 0 : BillRecieptDc.PromotionalPricing;
                BillRecieptDc.OverloadedVehicleFee = (BillRecieptDc.OverloadedVehicleFee <= 0 || BillRecieptDc.OverloadedVehicleFee == null) ? 0 : BillRecieptDc.OverloadedVehicleFee;
                BillRecieptDc.CreditLimit = (BillRecieptDc.CreditLimit <= 0 || BillRecieptDc.CreditLimit == null) ? 0 : BillRecieptDc.CreditLimit;
                BillRecieptDc.SecurityDeposit = (BillRecieptDc.SecurityDeposit <= 0 || BillRecieptDc.SecurityDeposit == null) ? 0 : BillRecieptDc.SecurityDeposit;
                BillRecieptDc.PreviousBalance = (BillRecieptDc.PreviousBalance <= 0 || BillRecieptDc.PreviousBalance == null) ? 0 : BillRecieptDc.PreviousBalance;
                BillRecieptDc.Payments = (BillRecieptDc.Payments <= 0 || BillRecieptDc.Payments == null) ? 0 : BillRecieptDc.Payments;
                BillRecieptDc.PayAmount = (BillRecieptDc.PayAmount <= 0 || BillRecieptDc.PayAmount == null) ? 0 : BillRecieptDc.PayAmount;
                BillRecieptDc.Adjustments = (BillRecieptDc.Adjustments <= 0 || BillRecieptDc.Adjustments == null) ? 0 : BillRecieptDc.Adjustments;
                BillRecieptDc.LatePaymentFee = (BillRecieptDc.LatePaymentFee <= 0 || BillRecieptDc.LatePaymentFee == null) ? 0 : BillRecieptDc.LatePaymentFee;
                BillRecieptDc.MonthlyRentals = (BillRecieptDc.MonthlyRentals <= 0 || BillRecieptDc.MonthlyRentals == null) ? 0 : BillRecieptDc.MonthlyRentals;
                BillRecieptDc.UsageCharges = (BillRecieptDc.UsageCharges <= 0 || BillRecieptDc.UsageCharges == null) ? 0 : BillRecieptDc.UsageCharges;
                BillRecieptDc.OneTimeCharges = (BillRecieptDc.OneTimeCharges <= 0 || BillRecieptDc.OneTimeCharges == null) ? 0 : BillRecieptDc.OneTimeCharges;
                BillRecieptDc.Taxes = (BillRecieptDc.Taxes <= 0 || BillRecieptDc.Taxes == null) ? 0 : BillRecieptDc.Taxes;
                BillRecieptDc.UsageP_ISD_IRCalling = (BillRecieptDc.UsageP_ISD_IRCalling <= 0 || BillRecieptDc.UsageP_ISD_IRCalling == null) ? 0 : BillRecieptDc.UsageP_ISD_IRCalling;
                BillRecieptDc.UsageDataCharges = (BillRecieptDc.UsageDataCharges <= 0 || BillRecieptDc.UsageDataCharges == null) ? 0 : BillRecieptDc.UsageDataCharges;
                BillRecieptDc.UsageSMSCharges = (BillRecieptDc.UsageSMSCharges <= 0 || BillRecieptDc.UsageSMSCharges == null) ? 0 : BillRecieptDc.UsageSMSCharges;
                BillRecieptDc.UsageVASCharges = (BillRecieptDc.UsageVASCharges <= 0 || BillRecieptDc.UsageVASCharges == null) ? 0 : BillRecieptDc.UsageVASCharges;
                BillRecieptDc.CurruntMonthDiscount = (BillRecieptDc.CurruntMonthDiscount <= 0 || BillRecieptDc.CurruntMonthDiscount == null) ? 0 : BillRecieptDc.CurruntMonthDiscount;
                BillRecieptDc.BillDiscountWithTax = (BillRecieptDc.BillDiscountWithTax <= 0 || BillRecieptDc.BillDiscountWithTax == null) ? 0 : BillRecieptDc.BillDiscountWithTax;
                BillRecieptDc.Rating = (BillRecieptDc.Rating <= 0 || BillRecieptDc.Rating == null) ? 0 : BillRecieptDc.Rating;
                #endregion

                BillRecieptDc.BillRecieptId = BillRecieptManager.InsertUpdate_BillReciept(BillRecieptDc);
                if (BillRecieptDc.BillRecieptId > 0)
                {
                    try
                    {
                        BRDc = BillRecieptManager.GetBillRecieptById(BillRecieptDc.BillRecieptId);
                        if (BRDc != null)
                        {
                            CategoryManager CategoryManager = new CategoryManager();
                            CategoryRecieptDc CategoryRecieptDc = CategoryManager.GetCategoryRecieptById(BillRecieptDc.CategoryRecieptId);
                            if (CategoryRecieptDc != null)
                            {
                                BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = CategoryRecieptDc.BillRecieptDynamicHtml;
                                BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = CategoryRecieptDc.BillRecieptPdfHtml;
                            }
                            string TotalWithItemTax = "";
                            double SubTotal = 0, TaxTotal = 0;
                            if (!string.IsNullOrEmpty(BRDc.RecieptLogo))
                            {
                                BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + BRDc.RecieptLogo);
                                BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + BRDc.RecieptLogo);
                            }                            
                            if (!string.IsNullOrEmpty(BRDc.ProfileImage))
                            {
                                BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ProfileImage]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + BRDc.ProfileImage);
                                BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ProfileImage]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + BRDc.ProfileImage);
                            }
                            else
                            {
                                BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ProfileImage]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/avatar.png");
                                BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ProfileImage]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/avatar.png");
                            }
                            if (!string.IsNullOrEmpty(BRDc.BarCode))
                            {
                                BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BarCode]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + BRDc.BarCode);
                                BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BarCode]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + BRDc.BarCode);
                            }
                            else
                            {
                                BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BarCode]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/barcode.jpg");
                                BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BarCode]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/barcode.jpg");
                            }
                            FontDc = MasterManager.GetFontById(BillRecieptDc.FontId);
                            if (FontDc != null)
                            {
                                BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[FontStyle]", FontDc.FontStyle);
                                BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[FontStyle]", FontDc.FontStyle);
                                BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[FontClass]", FontDc.FontClass);
                                BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[FontClass]", FontDc.FontClass);
                                BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[FontLink]", FontDc.FontLink);
                                BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[FontLink]", FontDc.FontLink);
                            }
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Business]", BRDc.Business);
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Business]", BRDc.Business);
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Address]", BRDc.Address);
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Address]", BRDc.Address);
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[CityState]", BRDc.CityState);
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[CityState]", BRDc.CityState);
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[MobileNo]", BRDc.MobileNo);
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[MobileNo]", BRDc.MobileNo);
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Date]", Convert.ToDateTime(BRDc.RecieptDate).ToString("dd/MM/yyyy"));
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Date]", Convert.ToDateTime(BRDc.RecieptDate).ToString("dd/MM/yyyy"));
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Time]", Convert.ToDateTime(BRDc.RecieptDate).ToString("hh:mm tt"));
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Time]", Convert.ToDateTime(BRDc.RecieptDate).ToString("hh:mm tt"));
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PaymentType]", BRDc.PaymentType);
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PaymentType]", BRDc.PaymentType);
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Auth]", BRDc.Auth);
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Auth]", BRDc.Auth);
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Mcc]", BRDc.Mcc);
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Mcc]", BRDc.Mcc);
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Trans]", BRDc.Trans);
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Trans]", BRDc.Trans);
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ApprCode]", BRDc.ApprCode);
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ApprCode]", BRDc.ApprCode);
                            try
                            {
                                #region Dynamic Reciept Wise Calculation 
                                if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "GeneralReceipt2")
                                {
                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0) { foreach (var item in BRDc.BillRecieptItemInfoDc) { SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2); } }
                                    TotalWithItemTax = "<table width='100%'><tr><td width='40%'>SubTotal:</td><td>" + BRDc.Currency + " " + SubTotal + "</td></tr>";
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0) { foreach (var tax in BRDc.BillRecieptTaxInfoDc) { double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100); TaxTotal += taxtot; TotalWithItemTax += "<tr><td>" + tax.Tax + ":</td><td>" + BRDc.Currency + " " + Math.Round(taxtot, 2) + "</td></tr>"; } } else { TotalWithItemTax += "<tr><td>NO TAX:</td><td>" + BRDc.Currency + " 0.00</td></tr>"; }
                                    TotalWithItemTax += "<tr><td>Total:</td><td>" + BRDc.Currency + " " + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</td></tr></table>";
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "GeneralReceipt3")
                                {
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/walmart-logo.jpg");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/walmart-logo.jpg");
                                    }
                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0) { foreach (var item in BRDc.BillRecieptItemInfoDc) { SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2); } }
                                    TotalWithItemTax = "<table width='100%' class='custom-table aovel-font'><tr><td width='40%'>AMT:</td><td>" + BRDc.Currency + " " + SubTotal + "</td></tr>";
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0) { foreach (var tax in BRDc.BillRecieptTaxInfoDc) { double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100); TaxTotal += taxtot; TotalWithItemTax += "<tr><td style='text-transform:uppercase;'>" + tax.Tax + ":</td><td>" + BRDc.Currency + " " + Math.Round(taxtot, 2) + "</td></tr>"; } } else { TotalWithItemTax += "<tr><td>NO TAX:</td><td>" + BRDc.Currency + " 0.00</td></tr>"; }
                                    TotalWithItemTax += "<tr><td>TOTAL:</td><td>" + BRDc.Currency + " " + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</td></tr></table>";
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "GeneralReceipt4")
                                {
                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0) { foreach (var item in BRDc.BillRecieptItemInfoDc) { SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2); } }
                                    TotalWithItemTax = "<table width='100%' class='custom-table udin-font'><tr><td width='40%'>SubTotal:</td><td>" + BRDc.Currency + " " + SubTotal + "</td></tr>";
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0) { foreach (var tax in BRDc.BillRecieptTaxInfoDc) { double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100); TaxTotal += taxtot; TotalWithItemTax += "<tr><td>" + tax.Tax + ":</td><td>" + BRDc.Currency + " " + Math.Round(taxtot, 2) + "</td></tr>"; } } else { TotalWithItemTax += "<tr><td>NO TAX:</td><td>" + BRDc.Currency + " 0.00</td></tr>"; }
                                    TotalWithItemTax += "<tr><td colspan='2'></td></tr><tr><td>Total:</td><td>" + BRDc.Currency + " " + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</td></tr></table>";
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "GeneralReceipt5")
                                {
                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0) { foreach (var item in BRDc.BillRecieptItemInfoDc) { SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2); } }
                                    TotalWithItemTax = "<table width='100%' class='custom-table bungee-font2' style='font-size:18px;font-weight: 900;letter-spacing: -3px;word-spacing: 7px;color:#000000;'><tr><td width='50%'>SubTotal:</td><td>" + BRDc.Currency + " " + SubTotal + "</td></tr><tr><td width='40%'>AMT:</td><td>" + BRDc.Currency + " " + SubTotal + "</td></tr>";
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0) { foreach (var tax in BRDc.BillRecieptTaxInfoDc) { double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100); TaxTotal += taxtot; TotalWithItemTax += "<tr><td>" + tax.Tax + ":</td><td>" + BRDc.Currency + " " + Math.Round(taxtot, 2) + "</td></tr>"; } } else { TotalWithItemTax += "<tr><td>NO TAX:</td><td>" + BRDc.Currency + " 0.00</td></tr>"; }
                                    TotalWithItemTax += "<tr><td>Total:</td><td>" + BRDc.Currency + " " + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</td></tr><tr><td>BALANCE:</td><td>$0.00</td></tr></table>";
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "GeneralReceipt6")
                                {
                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0) { foreach (var item in BRDc.BillRecieptItemInfoDc) { SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2); } }
                                    TotalWithItemTax = "<table align='right' class='ptmono-font text-right' style='color:#686868;'><tr><td width='40%'>SUBTOTAL:</td><td>" + BRDc.Currency + "" + SubTotal + "</td></tr>";
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0) { foreach (var tax in BRDc.BillRecieptTaxInfoDc) { double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100); TaxTotal += taxtot; TotalWithItemTax += "<tr><td>" + tax.Tax + ":</td><td>" + BRDc.Currency + "" + Math.Round(taxtot, 2) + "</td></tr>"; } } else { TotalWithItemTax += "<tr><td>NO TAX:</td><td>" + BRDc.Currency + "0.00</td></tr>"; }
                                    TotalWithItemTax += "<tr><td>TOTAL:</td><td>" + BRDc.Currency + "" + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</td></tr></table>";
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "GasFuelReceipt1")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[WebSiteName]", BRDc.WebSiteName);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[WebSiteName]", BRDc.WebSiteName);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DocNo]", BRDc.DocNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DocNo]", BRDc.DocNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Pump]", BRDc.Pump);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Pump]", BRDc.Pump);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptNo]", BRDc.RecieptNo);

                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0)
                                    {
                                        foreach (var item in BRDc.BillRecieptItemInfoDc)
                                        {
                                            SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2);
                                            TotalWithItemTax = "<tr><td width='30%'>PRODUCT:</td> <td> " + item.ItemName + " </td> </tr> <tr> <td>APPROVAL#</td><td> " + BRDc.ApprCode + " </td> </tr> <tr> <td>GALLONS:</td> <td> " + item.Quantity + " </td> </tr> <tr> <td>PRICE/G:</td> <td> " + BRDc.Currency + "" + item.Price + " </td> </tr> <tr><td>FUEL  SALE</td> <td> " + BRDc.Currency + "" + SubTotal + " </td> </tr>";
                                        }
                                    }

                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "GasFuelReceipt2")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[LandlineNo]", BRDc.LandlineNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[LandlineNo]", BRDc.LandlineNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[GSTNo]", BRDc.GSTNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[GSTNo]", BRDc.GSTNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillNo]", BRDc.BillNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillNo]", BRDc.BillNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[AtndId]", BRDc.AtndId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[AtndId]", BRDc.AtndId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[VehicleNo]", BRDc.VehicleNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[VehicleNo]", BRDc.VehicleNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[FPId]", BRDc.FPId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[FPId]", BRDc.FPId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[NozlNo]", BRDc.NozlNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[NozlNo]", BRDc.NozlNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Fuel]", BRDc.Fuel);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Fuel]", BRDc.Fuel);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Density]", BRDc.Density);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Density]", BRDc.Density);

                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0)
                                    {
                                        foreach (var item in BRDc.BillRecieptItemInfoDc)
                                        {
                                            SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2);
                                            TotalWithItemTax = "<tr><td>Rate</td><td>: " + BRDc.Currency + "" + item.Price + "</td></tr><tr> <td>Sale</td><td>: " + BRDc.Currency + "" + SubTotal + "</td></tr><tr> <td>Volume</td><td>: " + item.Quantity + "</td></tr>";
                                        }
                                    }
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/hindustan-petrolium-logo.jpg");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/hindustan-petrolium-logo.jpg");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "GasFuelReceipt3")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TinNo]", BRDc.TinNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TinNo]", BRDc.TinNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[LandlineNo]", BRDc.LandlineNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[LandlineNo]", BRDc.LandlineNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TRXId]", BRDc.TRXId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TRXId]", BRDc.TRXId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Pump]", BRDc.Pump);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Pump]", BRDc.Pump);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[NozlNo]", BRDc.NozlNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[NozlNo]", BRDc.NozlNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[VehicleNo]", BRDc.VehicleNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[VehicleNo]", BRDc.VehicleNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[VehicleType]", BRDc.VehicleType);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[VehicleType]", BRDc.VehicleType);

                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0)
                                    {
                                        foreach (var item in BRDc.BillRecieptItemInfoDc)
                                        {
                                            SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2);
                                            TotalWithItemTax = "<tr><td class='uppercase'>PRODUCT</td><td>: " + item.ItemName + "</td></tr><tr><td class='uppercase'>QUANTITY</td><td>: " + item.Quantity + "</td></tr><tr><td class='uppercase'>UNIT RATE</td><td>: " + item.Price + "</td></tr><tr><td>SALE</td><td>: " + SubTotal + "</td></tr><tr><td>TOTAL SALE </td><td>: " + SubTotal + "</td></tr>";
                                        }
                                    }
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/Indian_Oil.png");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/Indian_Oil.png");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "GasFuelReceipt4")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[AccountNo]", BRDc.AccountNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[AccountNo]", BRDc.AccountNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Pump]", BRDc.Pump);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Pump]", BRDc.Pump);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DropAddress]", BRDc.DropAddress);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DropAddress]", BRDc.DropAddress);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PickupAddress]", BRDc.PickupAddress);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PickupAddress]", BRDc.PickupAddress);

                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0)
                                    {
                                        foreach (var item in BRDc.BillRecieptItemInfoDc)
                                        {
                                            SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2);
                                            TotalWithItemTax = "<tr><td>PUMP</td><td>PRODUCT</td><td>S/G</td></tr><tr><td>" + BRDc.Pump + "</td><td><b>" + item.ItemName + "</b></td><td>" + item.Price + "</td></tr><tr><td colspan='2'>&nbsp;</td></tr><tr><td>GALLONS</td><td>FUEL</td><td>TOTAL</td></tr><tr><td>" + item.Quantity + "</td><td>" + BRDc.Currency + "</td><td>" + SubTotal + "</td></tr>";
                                        }
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "GasFuelReceipt5")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PompaSalans]", BRDc.PompaSalans);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PompaSalans]", BRDc.PompaSalans);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[NomarNota]", BRDc.NomarNota);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[NomarNota]", BRDc.NomarNota);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[JenisBBM]", BRDc.JenisBBM);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[JenisBBM]", BRDc.JenisBBM);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Operator]", BRDc.Operator);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Operator]", BRDc.Operator);

                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0)
                                    {
                                        foreach (var item in BRDc.BillRecieptItemInfoDc)
                                        {
                                            SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2);
                                            TotalWithItemTax = "<tr><td>Liter</td><td>:  " + item.Quantity + "</td></tr><tr><td>Harsa/liter</td><td>:  " + BRDc.Currency + "    " + item.Price + "</td></tr><tr><td>Total</td><td>:  " + BRDc.Currency + "    " + SubTotal + "</td></tr>";
                                        }
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "GasFuelReceipt6")
                                {
                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0)
                                    {
                                        foreach (var item in BRDc.BillRecieptItemInfoDc)
                                        {
                                            SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2);
                                            TotalWithItemTax = "<tr><td colspan='2'>" + item.Quantity + " REG @ " + item.Price + " /GAL<br><br></td></tr><tr><td>Sub Total</td><td>" + BRDc.Currency + " " + SubTotal + "</td></tr><tr><td>Total</td><td>" + BRDc.Currency + " " + SubTotal + "</td></tr>";
                                        }
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "GasFuelReceipt7")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[StationNo]", BRDc.StationNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[StationNo]", BRDc.StationNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Pump]", BRDc.Pump);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Pump]", BRDc.Pump);
                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0)
                                    {
                                        foreach (var item in BRDc.BillRecieptItemInfoDc)
                                        {
                                            SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2);
                                            TotalWithItemTax = "<tr><td>PUMP:" + BRDc.Pump + " </td><td>QTY:" + item.Quantity + "</td></tr><tr><td>REG @:</td><td>" + item.Price + "/GAL</td></tr><tr><td>Subtotal:</td><td>" + BRDc.Currency + " " + SubTotal + "</td></tr>";
                                        }
                                    }
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0) { foreach (var tax in BRDc.BillRecieptTaxInfoDc) { double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100); TaxTotal += taxtot; TotalWithItemTax += "<tr><td>" + tax.Tax + ":</td><td>" + BRDc.Currency + " " + Math.Round(taxtot, 2) + "</td></tr>"; } } else { TotalWithItemTax += "<tr><td>TAX:</td><td>" + BRDc.Currency + " 0.00</td></tr>"; }
                                    TotalWithItemTax += "<tr><td>Total:</td><td>" + BRDc.Currency + " " + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</td></tr>";
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "GasFuelReceipt8")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[WebSiteName]", BRDc.WebSiteName);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[WebSiteName]", BRDc.WebSiteName);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Pump]", BRDc.Pump);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Pump]", BRDc.Pump);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[AccountNo]", BRDc.AccountNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[AccountNo]", BRDc.AccountNo);

                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0)
                                    {
                                        foreach (var item in BRDc.BillRecieptItemInfoDc)
                                        {
                                            SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2);
                                            TotalWithItemTax = "<tr><td colspan='2'>QTY " + item.Quantity + " @ " + item.Price + "/GAL</td></tr><tr><td colspan='2'>&nbsp;</td></tr><tr><td>SUBTOTAL</td><td>" + BRDc.Currency + " " + SubTotal + "</td></tr><tr><td colspan='2'>&nbsp;</td></tr><tr><td>FUEL TOTAL: </td><td>" + BRDc.Currency + " " + SubTotal + "</td></tr>";
                                        }
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "GroceryReceipt1")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[LandlineNo]", BRDc.LandlineNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[LandlineNo]", BRDc.LandlineNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[HelplineNo]", BRDc.HelplineNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[HelplineNo]", BRDc.HelplineNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[MobileNo]", BRDc.MobileNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[MobileNo]", BRDc.MobileNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TinNo]", BRDc.TinNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TinNo]", BRDc.TinNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[CINNo]", BRDc.CINNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[CINNo]", BRDc.CINNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PayAmount]", BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PayAmount]", BRDc.PayAmount.ToString());
                                    string belowtax = "";
                                    TotalWithItemTax = "<tr><td class='no-padding uppercase'><table width='100%' class='custom-table'><tr><td colspan='5'><div class='dash-divider'></div></td></tr><tr><td>ITEM DESC</td><td>UOM</td><td>QTY</td><td>DISC AMT</td><td>NET AMT</td></tr><tr><td colspan='5'><div class='dash-divider'></div></td></tr>";
                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0)
                                    {
                                        foreach (var item in BRDc.BillRecieptItemInfoDc)
                                        {
                                            SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2);
                                            TotalWithItemTax += "<tr><td>" + item.ItemName + "</td><td>" + item.UOM + "</td><td>" + item.Quantity + "</td><td>" + item.Price + "</td><td>" + BRDc.Currency + " " + Convert.ToDouble(item.Quantity * item.Price) + "</td></tr>";
                                        }
                                    }
                                    TotalWithItemTax += "<tr><td colspan = '3'> &nbsp; &nbsp; SUB TOTAL</td><td colspan = '2' class='text-right'>" + BRDc.Currency + " " + SubTotal + "</td></tr>";
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0)
                                    {
                                        foreach (var tax in BRDc.BillRecieptTaxInfoDc)
                                        {
                                            double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100);
                                            TaxTotal += taxtot;
                                            TotalWithItemTax += "<tr><td colspan = '3'> &nbsp; &nbsp; " + tax.Tax + "</td><td colspan = '2' class='text-right'>" + BRDc.Currency + " " + Math.Round(taxtot, 2) + "</td></tr>";
                                            belowtax += "<tr><td>" + tax.Tax + "</td><td>" + SubTotal + "</td><td>" + Math.Round(taxtot, 2) + "</td></tr> ";
                                        }
                                    }
                                    TotalWithItemTax += "<tr> <td colspan = '4'></td><td><div class='dash-divider'></div><div class='dash-divider' style='margin-top:2px;'></div></td></tr><tr><td colspan = '3'> &nbsp; &nbsp; TOTAL</td><td colspan = '2' class='text-right'>" + BRDc.Currency + " " + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</td></tr><tr><td colspan = '3'> &nbsp; &nbsp; CASH</td><td colspan = '2' class='text-right'>" + BRDc.PayAmount + "</td></tr><tr><td colspan = '4'> &nbsp; &nbsp; &nbsp; &nbsp; CHANGE DUE</td><td class='text-right'>" + ((BRDc.PayAmount > 0) ? (Convert.ToDouble(BRDc.PayAmount) - Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2)).ToString() : "0.00") + "</td></tr></table></td></tr><tr><td><div class='dash-divider'></div></td></tr><tr><td class='text-center'><h3>Your mobile number " + BRDc.MobileNo + " has been registered with " + BRDc.Business + "</h3></td></tr><tr><td><div class='dash-divider'></div></td></tr><tr><td>PIECES PURCHASED: " + BRDc.BillRecieptItemInfoDc.Sum(x => !string.IsNullOrEmpty(x.Quantity.ToString()) ? Math.Round(double.Parse(x.Quantity.ToString()), 0) : 0).ToString() + "</td></tr><tr><td class='uppercase'><table class='custom-table' style='width:50%'><tr><td>GST</td><td>BASE AMT</td><td>TAX AMT</td></tr>";
                                    TotalWithItemTax += belowtax;
                                    TotalWithItemTax += "</table></td></tr>";
                                    //if (string.IsNullOrEmpty(BRDc.BarCode))
                                    //{
                                    //    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BarCode]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/Grocery-big-bazaarbarcode.png");
                                    //    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BarCode]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/Grocery-big-bazaarbarcode.png");
                                    //}
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "GroceryReceipt2")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[STNo]", BRDc.STNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[STNo]", BRDc.STNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[OPNo]", BRDc.OPNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[OPNo]", BRDc.OPNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TENo]", BRDc.TENo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TENo]", BRDc.TENo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TRNNo]", BRDc.TRNNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TRNNo]", BRDc.TRNNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[AccountNo]", BRDc.AccountNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[AccountNo]", BRDc.AccountNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[REFNo]", BRDc.REFNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[REFNo]", BRDc.REFNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Validation]", BRDc.Validation);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Validation]", BRDc.Validation);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PaymentService]", BRDc.PaymentService);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PaymentService]", BRDc.PaymentService);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TerminalId]", BRDc.TerminalId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TerminalId]", BRDc.TerminalId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TCId]", BRDc.TCId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TCId]", BRDc.TCId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PayAmount]", BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PayAmount]", BRDc.PayAmount.ToString());

                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0)
                                    {
                                        foreach (var item in BRDc.BillRecieptItemInfoDc)
                                        {
                                            SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2);
                                            TotalWithItemTax += "<tr><td>" + item.ItemName + "</td><td>" + BRDc.Currency + " " + Convert.ToDouble(item.Quantity * item.Price) + "</td></tr>";
                                        }
                                        string itemcount = BRDc.BillRecieptItemInfoDc.Sum(x => !string.IsNullOrEmpty(x.Quantity.ToString()) ? Math.Round(double.Parse(x.Quantity.ToString()), 0) : 0).ToString();
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ItemCount]", itemcount);
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ItemCount]", itemcount);
                                    }
                                    TotalWithItemTax += "</table></td></tr><tr><td>&nbsp;</td></tr><tr><td><table align='right' width='70%'><tr><td>SUBTOTAL </td><td>" + BRDc.Currency + " " + SubTotal + "</td></tr>";
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0) { foreach (var tax in BRDc.BillRecieptTaxInfoDc) { double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100); TaxTotal += taxtot; TotalWithItemTax += "<tr><td>" + tax.Tax + ":</td><td>" + BRDc.Currency + " " + Math.Round(taxtot, 2) + "</td></tr>"; } } else { TotalWithItemTax += "<tr><td>NO TAX:</td><td>" + BRDc.Currency + " 0.00</td></tr>"; }
                                    TotalWithItemTax += "<tr><td>TOTAL </td><td>" + BRDc.Currency + " " + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</td></tr><tr><td>TEND</td><td>" + BRDc.Currency + " " + BRDc.PayAmount + "</td></tr></table></td></tr>";

                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DueAmount]", (BRDc.PayAmount > 0) ? (Convert.ToDouble(BRDc.PayAmount) - Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2)).ToString() : "0.00");
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DueAmount]", (BRDc.PayAmount > 0) ? (Convert.ToDouble(BRDc.PayAmount) - Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2)).ToString() : "0.00");
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/bigbox-logo.jpg");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/bigbox-logo.jpg");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "GroceryReceipt3")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ExitDate]", Convert.ToDateTime(BRDc.ExitDate.ToString()).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ExitDate]", Convert.ToDateTime(BRDc.ExitDate.ToString()).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptNo]", BRDc.RecieptNo);

                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0) { foreach (var item in BRDc.BillRecieptItemInfoDc) { SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2); TotalWithItemTax += "<tr><td width='15%'>" + Math.Round(double.Parse(item.Quantity.ToString()), 0) + "</td> <td>" + item.ItemName + "</td> <td class='text-right'>" + BRDc.Currency + "" + Convert.ToDouble(item.Quantity * item.Price) + "</td> </tr>"; } }
                                    TotalWithItemTax += "<tr><td></td></tr><tr><td></td><td class='text-center'>SUBTOTAL </td> <td class=text-right>" + BRDc.Currency + "" + SubTotal + "</td> </tr>";
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0) { foreach (var tax in BRDc.BillRecieptTaxInfoDc) { double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100); TaxTotal += taxtot; TotalWithItemTax += "<tr><td></td><td class='text-center'>T = TAX " + BRDc.Currency + "" + tax.TaxInPercentage + "% on " + BRDc.Currency + "" + SubTotal + "</td> <td class='text-right'>" + BRDc.Currency + "" + Math.Round(taxtot, 2) + "</td></tr>"; } }
                                    TotalWithItemTax += "<tr><td></td><td class='text-center'>TOTAL </td> <td class='text-right'>" + BRDc.Currency + "" + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</td> </tr> <tr><td></td><td class='text-center'>" + BRDc.PaymentType + " CHARGE </td> <td class='text-right'>" + BRDc.Currency + "" + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</td> </tr>";
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/Redstore_Logo.jpg");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/Redstore_Logo.jpg");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "GroceryReceipt4")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PayAmount]", BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PayAmount]", BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Cashier]", BRDc.Cashier);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Cashier]", BRDc.Cashier);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Lane]", BRDc.Lane);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Lane]", BRDc.Lane);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Clerk]", BRDc.Clerk);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Clerk]", BRDc.Clerk);

                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0)
                                    {
                                        foreach (var item in BRDc.BillRecieptItemInfoDc)
                                        {
                                            SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2);
                                            TotalWithItemTax += "<tr> <td>" + item.ItemName + "</td> <td>" + BRDc.Currency + "" + Convert.ToDouble(item.Quantity * item.Price) + "</td> </tr>";
                                            string itemcount = BRDc.BillRecieptItemInfoDc.Sum(x => !string.IsNullOrEmpty(x.Quantity.ToString()) ? Math.Round(double.Parse(x.Quantity.ToString()), 0) : 0).ToString();
                                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ItemCount]", itemcount);
                                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ItemCount]", itemcount);
                                        }
                                    }
                                    TotalWithItemTax += "</table> </td> </tr> <tr> <td> </td> </tr> <tr> <td> <table class='custom-table' width='100%'> <tr> <td width='50%'>SUBTOTAL</td> <td>" + BRDc.Currency + "" + SubTotal + "</td> </tr>";
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0) { foreach (var tax in BRDc.BillRecieptTaxInfoDc) { double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100); TaxTotal += taxtot; TotalWithItemTax += "<tr><td>" + tax.Tax + ":</td><td>" + BRDc.Currency + "" + Math.Round(taxtot, 2) + "</td></tr>"; } } else { TotalWithItemTax += "<tr><td>NO TAX:</td><td>" + BRDc.Currency + "0.00</td></tr>"; }
                                    TotalWithItemTax += "<tr> <td>TOTAL</td> <td>" + BRDc.Currency + "" + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</td> </tr> <tr> <td>TEND </td> <td>" + BRDc.Currency + "" + BRDc.PayAmount + "</td> </tr> <tr> <td>CHANGE DUE </td> <td>" + BRDc.Currency + "" + ((BRDc.PayAmount > 0) ? (Convert.ToDouble(BRDc.PayAmount) - Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2)).ToString() : "0.00") + "</td> </tr>";

                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "GroceryReceipt5")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ExitDate]", Convert.ToDateTime(BRDc.ExitDate.ToString()).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ExitDate]", Convert.ToDateTime(BRDc.ExitDate.ToString()).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Cashier]", BRDc.Cashier);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Cashier]", BRDc.Cashier);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Lane]", BRDc.Lane);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Lane]", BRDc.Lane);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[EPSSequence]", BRDc.EPSSequence);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[EPSSequence]", BRDc.EPSSequence);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[SNNo]", BRDc.SNNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[SNNo]", BRDc.SNNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[MRCH]", BRDc.MRCH);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[MRCH]", BRDc.MRCH);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TerminalId]", BRDc.TerminalId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TerminalId]", BRDc.TerminalId);
                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0)
                                    {
                                        foreach (var item in BRDc.BillRecieptItemInfoDc)
                                        {
                                            SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2);
                                            TotalWithItemTax += "<tr> <td>" + Math.Round(double.Parse(item.Quantity.ToString()), 0) + "</td> <td>" + item.ItemName + "</td> <td>" + BRDc.Currency + " " + Convert.ToDouble(item.Quantity * item.Price) + "</td> </tr>";
                                        }
                                        string itemcount = BRDc.BillRecieptItemInfoDc.Sum(x => !string.IsNullOrEmpty(x.Quantity.ToString()) ? Math.Round(double.Parse(x.Quantity.ToString()), 0) : 0).ToString();
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ItemCount]", itemcount);
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ItemCount]", itemcount);
                                    }
                                    TotalWithItemTax += "</table> </td> </tr> <tr> <td> <table class='dynamic-font' width='100%'> <tr> <td width='50%'>SUBTOTAL</td> <td>" + BRDc.Currency + " " + SubTotal + "</td> </tr> <tr> <td>TOTAL </td> <td>" + BRDc.Currency + " " + SubTotal + "</td> </tr><tr> <td>PURCHASE </td> <td>" + BRDc.Currency + " " + SubTotal + "</td> </tr>";
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/supermarket-logo.png");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/supermarket-logo.png");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "GroceryReceipt6")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[STNo]", BRDc.STNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[STNo]", BRDc.STNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[OPNo]", BRDc.OPNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[OPNo]", BRDc.OPNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TENo]", BRDc.TENo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TENo]", BRDc.TENo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TRNNo]", BRDc.TRNNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TRNNo]", BRDc.TRNNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PayAmount]", BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PayAmount]", BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TCId]", BRDc.TCId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TCId]", BRDc.TCId);

                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0)
                                    {
                                        foreach (var item in BRDc.BillRecieptItemInfoDc)
                                        {
                                            SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2); TotalWithItemTax += "<tr> <td>" + item.ItemName + "</td><td class='text-right'>" + item.ItemNo + "</td> <td class='text-right'>" + Convert.ToDouble(item.Quantity * item.Price) + "</td> </tr>";
                                            string itemcount = BRDc.BillRecieptItemInfoDc.Sum(x => !string.IsNullOrEmpty(x.Quantity.ToString()) ? Math.Round(double.Parse(x.Quantity.ToString()), 0) : 0).ToString();
                                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ItemCount]", itemcount);
                                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ItemCount]", itemcount);
                                        }
                                    }
                                    TotalWithItemTax += "<tr> <td></td> <td class='text-right'>SUBTOTAL</td> <td class='text-right'>" + SubTotal + "</td> </tr>";
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0) { foreach (var tax in BRDc.BillRecieptTaxInfoDc) { double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100); TaxTotal += taxtot; TotalWithItemTax += "<tr><td></td><td class='text-right'>" + tax.Tax + "     " + tax.TaxInPercentage + " % </td><td class='text-right'>" + Math.Round(taxtot, 2) + "</td></tr>"; } }
                                    TotalWithItemTax += "<tr> <td width='30%' class='text-right'></td> <td width='30%' class='text-right'>TOTAL</td> <td class='text-right'>" + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</td></tr> <tr> <td class='text-right'></td> <td class='text-right'>CASH TEND </td> <td class='text-right'>" + BRDc.PayAmount + "</td></tr> <tr> <td class='text-right'></td> <td class='text-right'>CHANGE DUE</td> <td class='text-right'>" + ((BRDc.PayAmount > 0) ? (Convert.ToDouble(BRDc.PayAmount) - Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2)).ToString() : "0.00") + "</td></tr>";
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/walmart-logo.jpg");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/walmart-logo.jpg");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "GroceryReceipt7")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[STNo]", BRDc.STNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[STNo]", BRDc.STNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[OPNo]", BRDc.OPNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[OPNo]", BRDc.OPNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TENo]", BRDc.TENo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TENo]", BRDc.TENo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TRNNo]", BRDc.TRNNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TRNNo]", BRDc.TRNNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PayAmount]", BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PayAmount]", BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[AccountNo]", BRDc.AccountNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[AccountNo]", BRDc.AccountNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[SNNo]", BRDc.SNNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[SNNo]", BRDc.SNNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[REFNo]", BRDc.REFNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[REFNo]", BRDc.REFNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[NetworkId]", BRDc.NetworkId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[NetworkId]", BRDc.NetworkId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TerminalId]", BRDc.TerminalId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TerminalId]", BRDc.TerminalId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TCNo]", BRDc.TCNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TCNo]", BRDc.TCNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Payments]", BRDc.Payments.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Payments]", BRDc.Payments.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PaymentService]", BRDc.PaymentService);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PaymentService]", BRDc.PaymentService);

                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0)
                                    {
                                        foreach (var item in BRDc.BillRecieptItemInfoDc)
                                        {
                                            SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2);
                                            TotalWithItemTax += "<tr> <td>PRODUCT SERIAL # " + item.ItemNo + "</td> <td class='text-right'></td> <td class='text-right'></td> </tr> <tr> <td>" + item.ItemName + "</td> <td class='text-right'>" + Convert.ToDouble(item.Quantity * item.Price) + "</td> <td class='text-right' style='width: 30px;'> T</td> </tr> ";
                                            string itemcount = BRDc.BillRecieptItemInfoDc.Sum(x => !string.IsNullOrEmpty(x.Quantity.ToString()) ? Math.Round(double.Parse(x.Quantity.ToString()), 0) : 0).ToString();
                                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ItemCount]", itemcount);
                                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ItemCount]", itemcount);
                                        }
                                    }
                                    TotalWithItemTax += "<tr> <td class='text-right'>SUBTOTAL</td> <td class='text-right'>" + SubTotal + "</td> <td class='text-right'></td> </tr>";
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0) { foreach (var tax in BRDc.BillRecieptTaxInfoDc) { double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100); TaxTotal += taxtot; TotalWithItemTax += "<tr> <td class='text-right'>" + tax.Tax + "     " + tax.TaxInPercentage + " %</td> <td class='text-right'>" + Math.Round(taxtot, 2) + "</td> <td class='text-right'></td> </tr>"; } }
                                    TotalWithItemTax += "<tr> <td class='text-right'>TOTAL</td> <td class='text-right'>" + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</td> <td class='text-right'></td> </tr> <tr> <td class='text-right'>CASH TEND</td> <td class='text-right'>" + BRDc.PayAmount + "</td> <td class='text-right'></td> </tr> <tr> <td class='text-right'>DEBIT TEND</td> <td class='text-right'>" + BRDc.Payments + "</td> <td class='text-right'></td> </tr> <tr> <td class='text-right'>CHANGE DUE</td> <td class='text-right'>" + ((BRDc.PayAmount > 0) ? ((Convert.ToDouble(BRDc.PayAmount) + Convert.ToDouble(BRDc.Payments)) - Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2)).ToString() : "0.00") + "</td> <td class='text-right'></td> </tr> </table> </td> </tr> <tr> <td> <table class='dynamic-font uppercase' width='100%'> <tr> <td>EFT DEBIT </td> <td> PAY FROM PRIMARY</td> </tr> <tr> <td>   " + BRDc.Payments + "&nbsp;&nbsp;&nbsp;&nbsp;TOTAL </td> <td> PURCHASE</td> </tr>";
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/walmart-logo.jpg");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/walmart-logo.jpg");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "ParkingReceipt1")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BaseFare]", BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BaseFare]", BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Taxes]", BRDc.Taxes.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Taxes]", BRDc.Taxes.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[SubTotal]", (BRDc.BaseFare + BRDc.Taxes).ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[SubTotal]", (BRDc.BaseFare + BRDc.Taxes).ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Currency]", BRDc.Currency);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Currency]", BRDc.Currency);
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "ParkingReceipt2")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[SpaceNo]", BRDc.SpaceNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[SpaceNo]", BRDc.SpaceNo);
                                    string edate = Convert.ToDateTime(BRDc.ExitDate).ToString("MMM dd, yyyy");
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ExitDate]", edate);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ExitDate]", edate);
                                    string rdate = Convert.ToDateTime(BRDc.RecieptDate).ToString("hh:mmtt MMM dd, yyyy");
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PurchaseDate]", rdate);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PurchaseDate]", rdate);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ExitTime]", Convert.ToDateTime(BRDc.ExitDate).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ExitTime]", Convert.ToDateTime(BRDc.ExitDate).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TicketNo]", BRDc.TicketNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TicketNo]", BRDc.TicketNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[SNNo]", BRDc.SNNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[SNNo]", BRDc.SNNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Lot_Location]", BRDc.Lot_Location);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Lot_Location]", BRDc.Lot_Location);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PayedAt]", BRDc.PayedAt);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PayedAt]", BRDc.PayedAt);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PayAmount]", BRDc.Currency + "" + BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PayAmount]", BRDc.Currency + "" + BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Payments]", BRDc.Currency + "" + BRDc.Payments.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Payments]", BRDc.Currency + "" + BRDc.Payments.ToString());
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "ParkingReceipt3")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[EntryDate]", Convert.ToDateTime(BRDc.EntryDate).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[EntryDate]", Convert.ToDateTime(BRDc.EntryDate).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[EntryTime]", Convert.ToDateTime(BRDc.EntryDate).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[EntryTime]", Convert.ToDateTime(BRDc.EntryDate).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ExitDate]", Convert.ToDateTime(BRDc.ExitDate).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ExitDate]", Convert.ToDateTime(BRDc.ExitDate).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ExitTime]", Convert.ToDateTime(BRDc.ExitDate).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ExitTime]", Convert.ToDateTime(BRDc.ExitDate).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PayAmount]", BRDc.Currency + "" + BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PayAmount]", BRDc.Currency + "" + BRDc.PayAmount.ToString());
                                    TimeSpan span = (Convert.ToDateTime(BRDc.ExitDate) - Convert.ToDateTime(BRDc.EntryDate));
                                    string totalhours = String.Format("{0} days {1} hours {2} minutes", span.Days, span.Hours, span.Minutes);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TotalHours]", totalhours);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TotalHours]", totalhours);
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "ParkingReceipt4")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[EntryDate]", Convert.ToDateTime(BRDc.EntryDate).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[EntryDate]", Convert.ToDateTime(BRDc.EntryDate).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[EntryTime]", Convert.ToDateTime(BRDc.EntryDate).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[EntryTime]", Convert.ToDateTime(BRDc.EntryDate).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ExitDate]", Convert.ToDateTime(BRDc.ExitDate).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ExitDate]", Convert.ToDateTime(BRDc.ExitDate).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ExitTime]", Convert.ToDateTime(BRDc.ExitDate).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ExitTime]", Convert.ToDateTime(BRDc.ExitDate).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[StationNo]", BRDc.StationNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[StationNo]", BRDc.StationNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TicketNo]", BRDc.TicketNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TicketNo]", BRDc.TicketNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PayAmount]", BRDc.Currency + "" + BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PayAmount]", BRDc.Currency + "" + BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Payments]", BRDc.Currency + "" + BRDc.Payments.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Payments]", BRDc.Currency + "" + BRDc.Payments.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Taxes]", BRDc.Currency + "" + BRDc.Taxes.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Taxes]", BRDc.Currency + "" + BRDc.Taxes.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TotalFee]", BRDc.Currency + "" + (BRDc.Payments + BRDc.Taxes).ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TotalFee]", BRDc.Currency + "" + (BRDc.Payments + BRDc.Taxes).ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Trans]", BRDc.Trans);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Trans]", BRDc.Trans);

                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "ParkingReceipt5")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Lot_Location]", BRDc.Lot_Location);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Lot_Location]", BRDc.Lot_Location);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[MachName]", BRDc.MachName);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[MachName]", BRDc.MachName);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[SNNo]", BRDc.SNNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[SNNo]", BRDc.SNNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TicketNo]", BRDc.TicketNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TicketNo]", BRDc.TicketNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Payments]", BRDc.Currency + "" + BRDc.Payments.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Payments]", BRDc.Currency + "" + BRDc.Payments.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PayAmount]", BRDc.Currency + "" + BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PayAmount]", BRDc.Currency + "" + BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[AccountNo]", BRDc.AccountNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[AccountNo]", BRDc.AccountNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptDate]", Convert.ToDateTime(BRDc.RecieptDate).ToString("MMMM dd,yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptDate]", Convert.ToDateTime(BRDc.RecieptDate).ToString("MMMM dd,yyyy"));
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "ParkingReceipt7")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[EntryDate]", Convert.ToDateTime(BRDc.EntryDate).ToString("dd-MMM-yyyy HH:mm:ss"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[EntryDate]", Convert.ToDateTime(BRDc.EntryDate).ToString("dd-MMM-yyyy HH:mm:ss"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ExitDate]", Convert.ToDateTime(BRDc.ExitDate).ToString("dd-MMM-yyyy HH:mm:ss"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ExitDate]", Convert.ToDateTime(BRDc.ExitDate).ToString("dd-MMM-yyyy HH:mm:ss"));
                                    string totalhours = "";
                                    try
                                    {
                                        TimeSpan span = (Convert.ToDateTime(BRDc.ExitDate) - Convert.ToDateTime(BRDc.EntryDate));
                                        if (span.Days > 0)
                                            totalhours = String.Format("{0} : {1} : {2}", span.Hours + (span.Days * 24), span.Minutes, span.Seconds);
                                        else
                                            totalhours = String.Format("{0} : {1} : {2}", span.Hours, span.Minutes, span.Seconds);
                                    }
                                    catch { }
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TotalHours]", totalhours);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TotalHours]", totalhours);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BaseFare]", BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BaseFare]", BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[VehicleNo]", BRDc.VehicleNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[VehicleNo]", BRDc.VehicleNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[VehicleType]", BRDc.VehicleType);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[VehicleType]", BRDc.VehicleType);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Currency]", BRDc.Currency);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Currency]", BRDc.Currency);
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "ParkingReceipt8")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[EntryDate]", BRDc.EntryDate.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[EntryDate]", BRDc.EntryDate.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ExitDate]", BRDc.ExitDate.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ExitDate]", BRDc.ExitDate.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PayAmount]", BRDc.Currency + " " + BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PayAmount]", BRDc.Currency + " " + BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Taxes]", BRDc.Currency + " " + BRDc.Taxes.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Taxes]", BRDc.Currency + " " + BRDc.Taxes.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DropAddress]", BRDc.DropAddress);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DropAddress]", BRDc.DropAddress);
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "PharmacyReceipt1")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[EntryDate]", BRDc.EntryDate.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[EntryDate]", BRDc.EntryDate.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ExitDate]", BRDc.ExitDate.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ExitDate]", BRDc.ExitDate.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RegNo]", BRDc.RegNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RegNo]", BRDc.RegNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TRNNo]", BRDc.TRNNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TRNNo]", BRDc.TRNNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[CSHRNo]", BRDc.CSHRNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[CSHRNo]", BRDc.CSHRNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[STRNo]", BRDc.STRNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[STRNo]", BRDc.STRNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[HelpedBy]", BRDc.HelpedBy);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[HelpedBy]", BRDc.HelpedBy);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[REFNo]", BRDc.REFNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[REFNo]", BRDc.REFNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[AID]", BRDc.AID);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[AID]", BRDc.AID);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TCNo]", BRDc.TCNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TCNo]", BRDc.TCNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TerminalId]", BRDc.TerminalId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TerminalId]", BRDc.TerminalId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[CVM]", BRDc.CVM);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[CVM]", BRDc.CVM);
                                    TotalWithItemTax = "<tr> <td> <table class='custom-table' width='100%'>";
                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0) { foreach (var item in BRDc.BillRecieptItemInfoDc) { SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2); TotalWithItemTax += "<tr> <td>" + Math.Round(double.Parse(item.Quantity.ToString()), 0) + "</td> <td>" + item.ItemName + "</td> <td>" + BRDc.Currency + "" + item.Price + "</td> </tr>"; } }
                                    string itemcount = BRDc.BillRecieptItemInfoDc.Sum(x => !string.IsNullOrEmpty(x.Quantity.ToString()) ? Math.Round(double.Parse(x.Quantity.ToString()), 0) : 0).ToString();
                                    TotalWithItemTax += "</table> </td> </tr><tr><td>&nbsp;</td></tr> <tr> <td style='padding: 2%;' class='uppercase'> <table class='custom-table' width='100%'> <tr> <td colspan='3'> " + itemcount + " ITEM</td> </tr> <tr> <td> SUBTOTAL: </td> <td class='text-right'>" + BRDc.Currency + "" + SubTotal + "</td> </tr> <tr> <td>TOTAL </td> <td class='text-right'>" + BRDc.Currency + "" + SubTotal + "</td> </tr> <tr> <td>CHARGE </td> <td class='text-right'>" + BRDc.Currency + "" + SubTotal + "</td> </tr> </table> </td> </tr>";
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/fast-pharmacy-logo.jpg");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/fast-pharmacy-logo.jpg");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "PharmacyReceipt2")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RegNo]", BRDc.RegNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RegNo]", BRDc.RegNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[StoreNo]", BRDc.StoreNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[StoreNo]", BRDc.StoreNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Cashier]", BRDc.Cashier);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Cashier]", BRDc.Cashier);

                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0) { foreach (var item in BRDc.BillRecieptItemInfoDc) { SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2); TotalWithItemTax += "<tr> <td>" + Math.Round(double.Parse(item.Quantity.ToString()), 0) + "</td> <td>" + item.ItemName + "</td> <td width='25%'>" + BRDc.Currency + "" + item.Price + "</td> </tr>"; } }
                                    string itemcount = BRDc.BillRecieptItemInfoDc.Sum(x => !string.IsNullOrEmpty(x.Quantity.ToString()) ? Math.Round(double.Parse(x.Quantity.ToString()), 0) : 0).ToString();
                                    TotalWithItemTax += "<tr> <td>" + itemcount + " Items </td> <td class='text-right'>Subtotal </td> <td>" + BRDc.Currency + "" + SubTotal + "</td> </tr>";
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0) { foreach (var tax in BRDc.BillRecieptTaxInfoDc) { double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100); TaxTotal += taxtot; TotalWithItemTax += "<tr> <td> </td> <td class='text-right'>" + tax.Tax + "</td> <td>" + BRDc.Currency + "" + Math.Round(taxtot, 2) + "</td> </tr>"; } } else { TotalWithItemTax += "<tr><td>NO TAX:</td><td>" + BRDc.Currency + " 0.00</td></tr>"; }
                                    TotalWithItemTax += "<tr> <td> </td> <td class='text-right'> Total </td> <td>" + BRDc.Currency + "" + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</td> </tr> <tr> <td></td> <td colspan='2'>" + BRDc.PaymentType + "</td> </tr> <tr> <td> </td> <td>App # " + BRDc.AID + " </td> <td></td> </tr> <tr> <td> </td> <td colspan='2'> Ref # " + BRDc.REFNo + "</td> </tr> <tr> <td> </td> <td colspan='2'>Entry Method Contactless</td> </tr> <tr> <td> </td> <td class='text-right'>Tendered </td> <td>" + BRDc.Currency + "" + BRDc.PayAmount + "</td> </tr> <tr> <td> </td> <td class='text-right'>Cash Changes </td> <td>" + BRDc.Currency + "" + ((BRDc.PayAmount > Convert.ToDecimal(SubTotal + TaxTotal)) ? (Convert.ToDouble(BRDc.PayAmount) - Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2)).ToString() : "0.00") + "</td> </tr>";
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/local-drug-logo.jpg");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/local-drug-logo.jpg");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "PharmacyReceipt3")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PharmacyNo]", BRDc.PharmacyNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PharmacyNo]", BRDc.PharmacyNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[StoreNo]", BRDc.StoreNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[StoreNo]", BRDc.StoreNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RegNo]", BRDc.RegNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RegNo]", BRDc.RegNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TRNNo]", BRDc.TRNNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TRNNo]", BRDc.TRNNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[CSHRNo]", BRDc.CSHRNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[CSHRNo]", BRDc.CSHRNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[STRNo]", BRDc.STRNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[STRNo]", BRDc.STRNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TCNo]", BRDc.TCNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TCNo]", BRDc.TCNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ExitDate]", Convert.ToDateTime(BRDc.ExitDate.ToString()).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ExitDate]", Convert.ToDateTime(BRDc.ExitDate.ToString()).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[WebSiteName]", BRDc.WebSiteName);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[WebSiteName]", BRDc.WebSiteName);

                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0) { foreach (var item in BRDc.BillRecieptItemInfoDc) { SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2); TotalWithItemTax += "<tr> <td>" + Math.Round(double.Parse(item.Quantity.ToString()), 0) + "</td> <td>" + item.ItemName + "</td> <td class='text-right'>" + item.Price + "</td> </tr>"; } }
                                    string itemcount = BRDc.BillRecieptItemInfoDc.Sum(x => !string.IsNullOrEmpty(x.Quantity.ToString()) ? Math.Round(double.Parse(x.Quantity.ToString()), 0) : 0).ToString();
                                    TotalWithItemTax += "<tr><td>&nbsp;</td></tr><tr> <td colspan='3'> " + itemcount + " ITEMS</td> </tr> <tr> <td></td> <td>SUBTOTAL </td> <td class='text-right'>" + SubTotal + "</td> </tr> ";
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0) { foreach (var tax in BRDc.BillRecieptTaxInfoDc) { double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100); TaxTotal += taxtot; TotalWithItemTax += "<tr> <td> </td> <td>MA " + tax.TaxInPercentage + "% " + tax.Tax + "</td> <td class='text-right'>" + Math.Round(taxtot, 2) + "</td> </tr>"; } }
                                    TotalWithItemTax += "<tr> <td> </td> <td > <b>Total</b> </td> <td class='text-right'><b>" + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</b></td> </tr> <tr> <td> </td> <td>CASH </td> <td  class='text-right'>" + BRDc.PayAmount + "</td> </tr> <tr> <td> </td> <td >CHANGE</td> <td class='text-right'>" + ((BRDc.PayAmount > Convert.ToDecimal(SubTotal + TaxTotal)) ? (Convert.ToDouble(BRDc.PayAmount) - Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2)).ToString() : "0.00") + "</td> </tr>";
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/cvs-pharmacy-logo.png");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/cvs-pharmacy-logo.png");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "PharmacyReceipt4")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillTo]", BRDc.BillTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillTo]", BRDc.BillTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[REFNo]", BRDc.REFNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[REFNo]", BRDc.REFNo);

                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0) { foreach (var item in BRDc.BillRecieptItemInfoDc) { SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2); TotalWithItemTax += "<tr> <td> </td> <td>" + item.ItemName + "</td> <td>" + Math.Round(double.Parse(item.Quantity.ToString())) + "</td> <td class='text-right'>" + item.Price + "</td> <td width='15%'> SALE </td> </tr>"; } }
                                    TotalWithItemTax += "<tr> <td> </td> <td> TOTAL </td> <td> </td> <td class='text-right'>" + SubTotal + "</td> <td> </td> </tr><tr><td colspan='5'>&nbsp;</td></tr> <tr> <td> </td> <td> </td> <td> CASH </td> <td class='text-right'>" + BRDc.PayAmount + "</td> <td> </td> </tr> <tr> <td> </td> <td> CHANGE </td> <td> </td> <td class='text-right'>" + ((BRDc.PayAmount > Convert.ToDecimal(SubTotal)) ? Math.Round(Convert.ToDouble((Convert.ToDouble(BRDc.PayAmount) - Convert.ToDouble(SubTotal + TaxTotal))), 2).ToString() : "0.00") + "</td> <td> </td> </tr> <tr><td colspan='5'>&nbsp;</td></tr> <tr> <td colspan='4'> WAG ADVERTISED SAVINGS: </td> <td>" + SubTotal + "</td> </tr>";
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/walgreens-logo.png");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/walgreens-logo.png");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "PharmacyReceipt5")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillTo]", BRDc.BillTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillTo]", BRDc.BillTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillNo]", BRDc.BillNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillNo]", BRDc.BillNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PharmacyNo]", BRDc.PharmacyNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PharmacyNo]", BRDc.PharmacyNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[HelpedBy]", BRDc.HelpedBy);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[HelpedBy]", BRDc.HelpedBy);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[LandlineNo]", BRDc.LandlineNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[LandlineNo]", BRDc.LandlineNo);

                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0) { foreach (var item in BRDc.BillRecieptItemInfoDc) { SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2); } }
                                    TotalWithItemTax = "<table align='right' class='ptmono-font text-right' style='color:#686868;'><tr><td width='40%'>SubTotal:</td><td>" + BRDc.Currency + " " + SubTotal + "</td></tr>";
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0) { foreach (var tax in BRDc.BillRecieptTaxInfoDc) { double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100); TaxTotal += taxtot; TotalWithItemTax += "<tr><td>" + tax.Tax + ":</td><td>" + BRDc.Currency + " " + Math.Round(taxtot, 2) + "</td></tr>"; } } else { TotalWithItemTax += "<tr><td>NO TAX:</td><td>" + BRDc.Currency + " 0.00</td></tr>"; }
                                    TotalWithItemTax += "<tr><td>Total:</td><td>" + BRDc.Currency + " " + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</td></tr></table>";
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "PhoneReceipt1")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillTo]", BRDc.BillTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillTo]", BRDc.BillTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ShipTo]", BRDc.ShipTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ShipTo]", BRDc.ShipTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PickupAddress]", BRDc.PickupAddress);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PickupAddress]", BRDc.PickupAddress);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DropAddress]", BRDc.DropAddress);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DropAddress]", BRDc.DropAddress);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PANNo]", BRDc.PANNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PANNo]", BRDc.PANNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[GSTNo]", BRDc.GSTNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[GSTNo]", BRDc.GSTNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[OrderNo]", BRDc.OrderNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[OrderNo]", BRDc.OrderNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[OrderDate]", Convert.ToDateTime(BRDc.OrderDate.ToString()).ToString("dd.MM.yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[OrderDate]", Convert.ToDateTime(BRDc.OrderDate.ToString()).ToString("dd.MM.yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[InvoiceDetails]", BRDc.InvoiceDetails);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[InvoiceDetails]", BRDc.InvoiceDetails);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillDate]", Convert.ToDateTime(BRDc.BillDate.ToString()).ToString("dd.MM.yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillDate]", Convert.ToDateTime(BRDc.BillDate.ToString()).ToString("dd.MM.yyyy"));
                                    string itemjoin = "", taxjoin = "", taxjoin1 = "", taxjoin2 = "", taxjoin3 = "";
                                    int aindex = 0, bindex = 0;
                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0)
                                    {
                                        foreach (var item in BRDc.BillRecieptItemInfoDc)
                                        {
                                            SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2);
                                            aindex += 1;
                                            if (aindex == 1)
                                                TotalWithItemTax = "<tr> <td>" + item.ItemNo + "</td> <td>" + item.ItemName + " </td> <td>" + BRDc.Currency + "" + item.Price + "</td> <td>" + Math.Round(double.Parse(item.Quantity.ToString()), 0) + "</td> <td>" + BRDc.Currency + "" + SubTotal + "</td>";
                                            else
                                                itemjoin += "<tr> <td>" + item.ItemNo + "</td> <td>" + item.ItemName + " </td> <td>" + BRDc.Currency + "" + item.Price + "</td> <td>" + Math.Round(double.Parse(item.Quantity.ToString()), 0) + "</td> <td>" + BRDc.Currency + "" + SubTotal + "</td>";
                                        }
                                    }
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0)
                                    {
                                        foreach (var tax in BRDc.BillRecieptTaxInfoDc)
                                        {
                                            double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100);
                                            TaxTotal += taxtot;
                                            bindex += 1;
                                            if (bindex == 1)
                                            {
                                                taxjoin1 += "<td rowspan='" + BRDc.BillRecieptItemInfoDc.Count + "'>" + tax.TaxInPercentage + "%";
                                                taxjoin2 += "<td rowspan='" + BRDc.BillRecieptItemInfoDc.Count + "'>" + tax.Tax + "";
                                                taxjoin3 += "<td rowspan='" + BRDc.BillRecieptItemInfoDc.Count + "'>" + BRDc.Currency + "" + Math.Round(taxtot, 2) + "";
                                            }
                                            else
                                            {
                                                taxjoin1 += "<br>" + tax.TaxInPercentage + "%";
                                                taxjoin2 += "<br>" + tax.Tax;
                                                taxjoin3 += "<br>" + BRDc.Currency + "" + Math.Round(taxtot, 2);
                                            }
                                        }
                                        taxjoin1 += "</td>";
                                        taxjoin2 += "</td>";
                                        taxjoin3 += "</td> ";
                                        taxjoin = taxjoin1 + taxjoin2 + taxjoin3;
                                    }

                                    taxjoin += "<td rowspan='" + BRDc.BillRecieptItemInfoDc.Count + "'>" + BRDc.Currency + "" + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 0) + "</td> </tr> <tr> <th colspan='7'>Total</th> <th style='background: #999999;'>" + BRDc.Currency + "" + Math.Round(Convert.ToDouble(TaxTotal), 2) + "</th> <th style='background: #999999;'>" + BRDc.Currency + "" + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</th> </tr> <tr> <td colspan='9'> <b> Amount in Words:<br> " + ConvertNumbertoWords(int.Parse(Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 0).ToString())) + "</b> </td> </tr>";
                                    TotalWithItemTax += taxjoin;
                                    TotalWithItemTax += itemjoin;
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/amazon-logo.png");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/amazon-logo.png");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "PhoneReceipt2")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillTo]", BRDc.BillTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillTo]", BRDc.BillTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillToEmailId]", BRDc.BillToEmailId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillToEmailId]", BRDc.BillToEmailId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[LandMark]", BRDc.LandMark);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[LandMark]", BRDc.LandMark);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[FixedlineNo]", BRDc.FixedlineNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[FixedlineNo]", BRDc.FixedlineNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BroadbandId]", BRDc.BroadbandId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BroadbandId]", BRDc.BroadbandId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RelationShipNo]", BRDc.RelationShipNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RelationShipNo]", BRDc.RelationShipNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillNo]", BRDc.BillNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillNo]", BRDc.BillNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillDate]", Convert.ToDateTime(BRDc.BillDate.ToString()).ToString("dd-MM-yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillDate]", Convert.ToDateTime(BRDc.BillDate.ToString()).ToString("dd-MM-yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillPeriod]", BRDc.BillPeriod);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillPeriod]", BRDc.BillPeriod);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillPayDate]", Convert.ToDateTime(BRDc.BillPayDate.ToString()).ToString("dd-MM-yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillPayDate]", Convert.ToDateTime(BRDc.BillPayDate.ToString()).ToString("dd-MM-yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[SecurityDeposit]", BRDc.SecurityDeposit.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[SecurityDeposit]", BRDc.SecurityDeposit.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[AlternateMobileNo]", BRDc.AlternateMobileNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[AlternateMobileNo]", BRDc.AlternateMobileNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PreviousBalance]", BRDc.PreviousBalance.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PreviousBalance]", BRDc.PreviousBalance.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Payments]", BRDc.Payments.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Payments]", BRDc.Payments.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[MonthlyRentals]", BRDc.MonthlyRentals.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[MonthlyRentals]", BRDc.MonthlyRentals.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[UsageCharges]", BRDc.UsageCharges.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[UsageCharges]", BRDc.UsageCharges.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[OneTimeCharges]", BRDc.OneTimeCharges.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[OneTimeCharges]", BRDc.OneTimeCharges.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Taxes]", BRDc.Taxes.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Taxes]", BRDc.Taxes.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillGenerateBy]", BRDc.BillGenerateBy);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillGenerateBy]", BRDc.BillGenerateBy);
                                    double TMC = Math.Round(double.Parse((BRDc.MonthlyRentals + BRDc.UsageCharges + BRDc.OneTimeCharges + BRDc.Taxes).ToString()), 2);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ThisMonthCharges]", TMC.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ThisMonthCharges]", TMC.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[AmountInWords]", ConvertNumbertoWords(int.Parse(TMC.ToString())));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[AmountInWords]", ConvertNumbertoWords(int.Parse(TMC.ToString())));
                                    double ADTD = Math.Round(double.Parse(((double.Parse(BRDc.PreviousBalance.ToString()) + TMC) - double.Parse(BRDc.Payments.ToString())).ToString()), 2);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[AmountDueTill]", ADTD.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[AmountDueTill]", ADTD.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Currency]", BRDc.Currency);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Currency]", BRDc.Currency);
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/airtel-logo.jpg");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/airtel-logo.jpg");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "PhoneReceipt3")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillTo]", BRDc.BillTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillTo]", BRDc.BillTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[LandMark]", BRDc.LandMark);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[LandMark]", BRDc.LandMark);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RelationShipNo]", BRDc.RelationShipNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RelationShipNo]", BRDc.RelationShipNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillNo]", BRDc.BillNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillNo]", BRDc.BillNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillDate]", Convert.ToDateTime(BRDc.BillDate.ToString()).ToString("dd-MM-yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillDate]", Convert.ToDateTime(BRDc.BillDate.ToString()).ToString("dd-MM-yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillPeriod]", BRDc.BillPeriod);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillPeriod]", BRDc.BillPeriod);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillPayDate]", Convert.ToDateTime(BRDc.BillPayDate.ToString()).ToString("dd-MM-yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillPayDate]", Convert.ToDateTime(BRDc.BillPayDate.ToString()).ToString("dd-MM-yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[SecurityDeposit]", BRDc.SecurityDeposit.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[SecurityDeposit]", BRDc.SecurityDeposit.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[CreditLimit]", BRDc.CreditLimit.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[CreditLimit]", BRDc.CreditLimit.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PreviousBalance]", BRDc.PreviousBalance.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PreviousBalance]", BRDc.PreviousBalance.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Payments]", BRDc.Payments.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Payments]", BRDc.Payments.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[MonthlyRentals]", BRDc.MonthlyRentals.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[MonthlyRentals]", BRDc.MonthlyRentals.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[UsageCharges]", BRDc.UsageCharges.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[UsageCharges]", BRDc.UsageCharges.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[OneTimeCharges]", BRDc.OneTimeCharges.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[OneTimeCharges]", BRDc.OneTimeCharges.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Taxes]", BRDc.Taxes.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Taxes]", BRDc.Taxes.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Adjustments]", BRDc.Adjustments.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Adjustments]", BRDc.Adjustments.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[LatePaymentFee]", BRDc.LatePaymentFee.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[LatePaymentFee]", BRDc.LatePaymentFee.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[WebSiteName]", BRDc.WebSiteName);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[WebSiteName]", BRDc.WebSiteName);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RegNo]", BRDc.RegNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RegNo]", BRDc.RegNo);
                                    double TMC = Math.Round(double.Parse((BRDc.MonthlyRentals + BRDc.UsageCharges + BRDc.OneTimeCharges + BRDc.Taxes).ToString()), 2);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ThisMonthCharges]", TMC.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ThisMonthCharges]", TMC.ToString());
                                    double ADTD = Math.Round(double.Parse(((double.Parse(BRDc.PreviousBalance.ToString()) + TMC + double.Parse(BRDc.LatePaymentFee.ToString())) - (double.Parse(BRDc.Payments.ToString()) + double.Parse(BRDc.Adjustments.ToString()))).ToString()), 2);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[AmountDueTill]", ADTD.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[AmountDueTill]", ADTD.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[AmountDueAfter]", (ADTD + 100).ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[AmountDueAfter]", (ADTD + 100).ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Currency]", BRDc.Currency);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Currency]", BRDc.Currency);
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/airtel-logo.jpg");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/airtel-logo.jpg");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "PhoneReceipt4")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillTo]", BRDc.BillTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillTo]", BRDc.BillTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DropAddress]", BRDc.DropAddress);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DropAddress]", BRDc.DropAddress);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[GSTNo]", BRDc.GSTNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[GSTNo]", BRDc.GSTNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[AadharNo]", BRDc.AadharNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[AadharNo]", BRDc.AadharNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillToEmailId]", BRDc.BillToEmailId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillToEmailId]", BRDc.BillToEmailId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[AccountNo]", BRDc.AccountNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[AccountNo]", BRDc.AccountNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillNo]", BRDc.BillNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillNo]", BRDc.BillNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillDate]", Convert.ToDateTime(BRDc.BillDate.ToString()).ToString("dd-MM-yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillDate]", Convert.ToDateTime(BRDc.BillDate.ToString()).ToString("dd-MM-yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DueDate]", Convert.ToDateTime(BRDc.DueDate.ToString()).ToString("dd-MM-yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DueDate]", Convert.ToDateTime(BRDc.DueDate.ToString()).ToString("dd-MM-yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[CreditLimit]", BRDc.CreditLimit.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[CreditLimit]", BRDc.CreditLimit.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[MemberShip]", BRDc.MemberShip);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[MemberShip]", BRDc.MemberShip);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PreviousBalance]", BRDc.PreviousBalance.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PreviousBalance]", BRDc.PreviousBalance.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Payments]", BRDc.Payments.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Payments]", BRDc.Payments.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[MonthlyRentals]", BRDc.MonthlyRentals.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[MonthlyRentals]", BRDc.MonthlyRentals.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[UsageP_ISD_IRCalling]", BRDc.UsageP_ISD_IRCalling.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[UsageP_ISD_IRCalling]", BRDc.UsageP_ISD_IRCalling.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[UsageDataCharges]", BRDc.UsageDataCharges.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[UsageDataCharges]", BRDc.UsageDataCharges.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[UsageSMSCharges]", BRDc.UsageSMSCharges.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[UsageSMSCharges]", BRDc.UsageSMSCharges.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[UsageVASCharges]", BRDc.UsageVASCharges.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[UsageVASCharges]", BRDc.UsageVASCharges.ToString());
                                    string usagecharges = (BRDc.UsageP_ISD_IRCalling + BRDc.UsageDataCharges + BRDc.UsageSMSCharges + BRDc.UsageVASCharges).ToString();
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[UsageCharges]", usagecharges);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[UsageCharges]", usagecharges);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[OneTimeCharges]", BRDc.OneTimeCharges.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[OneTimeCharges]", BRDc.OneTimeCharges.ToString());
                                    string totalvaluecharges = (decimal.Parse(usagecharges) + BRDc.OneTimeCharges + BRDc.MonthlyRentals).ToString();
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TotalValueCharges]", totalvaluecharges);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TotalValueCharges]", totalvaluecharges);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[CurruntMonthDiscount]", BRDc.CurruntMonthDiscount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[CurruntMonthDiscount]", BRDc.CurruntMonthDiscount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[SecurityDeposit]", BRDc.SecurityDeposit.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[SecurityDeposit]", BRDc.SecurityDeposit.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Adjustments]", BRDc.Adjustments.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Adjustments]", BRDc.Adjustments.ToString());
                                    string creditadjustments = (BRDc.CurruntMonthDiscount + BRDc.Adjustments).ToString();
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[CreditAdjustments]", creditadjustments);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[CreditAdjustments]", creditadjustments);
                                    string subcharges = (decimal.Parse(creditadjustments) + decimal.Parse(totalvaluecharges)).ToString();
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[SubCharges]", subcharges);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[SubCharges]", subcharges);

                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0)
                                    {
                                        int inccount = 0;
                                        foreach (var tax in BRDc.BillRecieptTaxInfoDc)
                                        {
                                            inccount += 1;
                                            double taxtot = ((double.Parse(subcharges) * double.Parse(tax.TaxInPercentage.ToString())) / 100);
                                            TaxTotal += taxtot;
                                            if (inccount == BRDc.BillRecieptTaxInfoDc.Count)
                                                TotalWithItemTax += "<tr><td></td><td> &nbsp; &nbsp; " + tax.Tax + " (" + tax.TaxInPercentage + "%)</td><td class='text-center'></td><td class='text-right'><u>" + Math.Round(taxtot, 2) + "</u></td><td class='text-right'><u>" + Math.Round(TaxTotal, 2) + "</u></td></tr>";
                                            else
                                                TotalWithItemTax += "<tr><td></td><td> &nbsp; &nbsp; " + tax.Tax + " (" + tax.TaxInPercentage + "%)</td><td class='text-center'></td><td class='text-right'>" + Math.Round(taxtot, 2) + "</td><td class='text-right'></td></tr>";
                                        }
                                    }

                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Taxes]", TaxTotal.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Taxes]", TaxTotal.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillDiscountWithTax]", BRDc.BillDiscountWithTax.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillDiscountWithTax]", BRDc.BillDiscountWithTax.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PANNo]", BRDc.PANNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PANNo]", BRDc.PANNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Currency]", BRDc.Currency);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Currency]", BRDc.Currency);
                                    string AmountDueTill = (decimal.Parse(subcharges) + decimal.Parse(TaxTotal.ToString()) + BRDc.CurruntMonthDiscount).ToString();
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[AmountDueTill]", AmountDueTill);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[AmountDueTill]", AmountDueTill);
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/jio.jpg");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/jio.jpg");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "RestaurantReceipt1")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[LandlineNo]", BRDc.LandlineNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[LandlineNo]", BRDc.LandlineNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[FaxNo]", BRDc.FaxNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[FaxNo]", BRDc.FaxNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[WebSiteName]", BRDc.WebSiteName);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[WebSiteName]", BRDc.WebSiteName);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillTo]", BRDc.BillTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillTo]", BRDc.BillTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ShipTo]", BRDc.ShipTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ShipTo]", BRDc.ShipTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PickupAddress]", BRDc.PickupAddress);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PickupAddress]", BRDc.PickupAddress);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DropAddress]", BRDc.DropAddress);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DropAddress]", BRDc.DropAddress);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[CustomerNo]", BRDc.CustomerNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[CustomerNo]", BRDc.CustomerNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[OrderNo]", BRDc.OrderNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[OrderNo]", BRDc.OrderNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[OrderDate]", Convert.ToDateTime(BRDc.OrderDate.ToString()).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[OrderDate]", Convert.ToDateTime(BRDc.OrderDate.ToString()).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[CustomerPONo]", BRDc.CustomerPONo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[CustomerPONo]", BRDc.CustomerPONo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DeliveryDate]", Convert.ToDateTime(BRDc.DeliveryDate.ToString()).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DeliveryDate]", Convert.ToDateTime(BRDc.DeliveryDate.ToString()).ToString("dd/MM/yyyy"));

                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0)
                                    {
                                        int i = 0;
                                        foreach (var item in BRDc.BillRecieptItemInfoDc)
                                        {
                                            i += 1;
                                            SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2);
                                            if (i == BRDc.BillRecieptItemInfoDc.Count)
                                                TotalWithItemTax += "<tr class='uppercase'><td>" + item.ItemNo + "</td><td>" + item.Quantity + "</td><td>" + item.UOM + "</td><td>" + item.ItemName + "</td><td class='text-right'>" + Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2) + "</td></tr>";
                                            else
                                                TotalWithItemTax += "<tr class='uppercase'><td>" + item.ItemNo + "</td><td>" + item.Quantity + "</td><td>" + item.UOM + "</td><td>" + item.ItemName + "</td><td class='text-right'>" + Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2) + "</td></tr><tr><td colspan='5'><div class='dash-divider'></div></td></tr>";
                                        }
                                    }
                                    TotalWithItemTax += "<tr><td colspan='5' class='line-divider'></td></tr><tr><td colspan='3'>Estimated Total: <br> <br>Pricing is estimated and subject to change.</td><td class='text-right' colspan='2'><h3>" + BRDc.Currency + "" + SubTotal + "</h3></td></tr>";
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/necs.jpg");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/necs.jpg");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "RestaurantReceipt2")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[MRCH]", BRDc.MRCH);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[MRCH]", BRDc.MRCH);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TerminalId]", BRDc.TerminalId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TerminalId]", BRDc.TerminalId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[SNNo]", BRDc.SNNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[SNNo]", BRDc.SNNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Host]", BRDc.Host);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Host]", BRDc.Host);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ExitDate]", Convert.ToDateTime(BRDc.ExitDate.ToString()).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ExitDate]", Convert.ToDateTime(BRDc.ExitDate.ToString()).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PayAmount]", BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PayAmount]", BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Taxes]", BRDc.Taxes.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Taxes]", BRDc.Taxes.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Currency]", BRDc.Currency);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Currency]", BRDc.Currency);
                                    decimal? subtot = BRDc.PayAmount + BRDc.Taxes;
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[SubTotal]", subtot.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[SubTotal]", subtot.ToString());
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/super-rest-logo.jpg");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/super-rest-logo.jpg");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "RestaurantReceipt3")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Host]", BRDc.Host);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Host]", BRDc.Host);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[OrderNo]", BRDc.OrderNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[OrderNo]", BRDc.OrderNo);

                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0)
                                    {
                                        foreach (var item in BRDc.BillRecieptItemInfoDc)
                                        {
                                            SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2);
                                            TotalWithItemTax += "<tr><td colspan='2'>" + item.ItemName + "</td><td class='text-right'>" + BRDc.Currency + "" + Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2) + "</td></tr> ";
                                        }
                                    }
                                    TotalWithItemTax += "<tr><td colspan='3'>&nbsp;</td></tr><tr><td> SUBTOTAL: </td><td>" + BRDc.Currency + "" + SubTotal + "</td><td></td></tr>";
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0) { foreach (var tax in BRDc.BillRecieptTaxInfoDc) { double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100); TaxTotal += taxtot; TotalWithItemTax += "<tr><td> " + tax.Tax + ": </td><td>" + BRDc.Currency + "" + Math.Round(taxtot, 2) + "</td><td></td></tr>"; } }
                                    TotalWithItemTax += "<tr><td colspan='3'>" + BRDc.PaymentType + "</td></tr><tr><td colspan='3'>AUTHORIZE...</td></tr><tr><td>BALANCE DUE </td><td>" + BRDc.Currency + "" + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</td><td></td></tr>";
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/barrito-logo.jpg");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/barrito-logo.jpg");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "RestaurantReceipt4")
                                {
                                    string taxamt = "";
                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0)
                                    {
                                        foreach (var item in BRDc.BillRecieptItemInfoDc)
                                        {
                                            SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2);
                                            TotalWithItemTax += "<tr><td>" + Math.Round(Convert.ToDouble(item.Quantity), 0) + "</td><td>" + item.ItemName + "</td><td class='text-right'>" + BRDc.Currency + "" + Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2) + "</td></tr>";
                                        }
                                    }
                                    TotalWithItemTax += "<tr><td colspan='3'>&nbsp;</td></tr><tr><td></td><td colspan='2'><table style='width:70%;' align='left' cellpadding='8' class='p-10 w-50 uppercase dynamic-font [FontClass]'>";
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0)
                                    {
                                        foreach (var tax in BRDc.BillRecieptTaxInfoDc)
                                        {
                                            double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100); TaxTotal += taxtot;
                                            taxamt += "<tr><td>" + tax.Tax + "</td><td>" + BRDc.Currency + "" + Math.Round(taxtot, 2) + "</td></tr>";
                                        }
                                    }
                                    TotalWithItemTax += "<tr><td>AMT</td> <td>" + BRDc.Currency + "" + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</td></tr>";
                                    TotalWithItemTax += "<tr><td>SUBTOTAL </td><td>" + BRDc.Currency + "" + SubTotal + "</td></tr>";
                                    TotalWithItemTax += taxamt;
                                    TotalWithItemTax += "<tr><td>Total:</td><td>" + BRDc.Currency + " " + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</td></tr></table>";
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/Restaurant-ItemizedLogo.png");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/Restaurant-ItemizedLogo.png");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "RestaurantReceipt5")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Clerk]", BRDc.Clerk);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Clerk]", BRDc.Clerk);

                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0) { foreach (var item in BRDc.BillRecieptItemInfoDc) { SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2); TotalWithItemTax += "<tr><td>" + Math.Round(Convert.ToDouble(item.Quantity), 0) + "</td><td>" + item.ItemName + "</td><td class='text-right'>" + BRDc.Currency + "" + Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2) + "</td></tr>"; } }
                                    string itemcount = BRDc.BillRecieptItemInfoDc.Sum(x => !string.IsNullOrEmpty(x.Quantity.ToString()) ? Math.Round(double.Parse(x.Quantity.ToString()), 0) : 0).ToString();

                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ItemCount]", itemcount);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ItemCount]", itemcount);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[SubTotal]", SubTotal.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[SubTotal]", SubTotal.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Currency]", BRDc.Currency);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Currency]", BRDc.Currency);
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "RestaurantReceipt6")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[WebSiteName]", BRDc.WebSiteName);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[WebSiteName]", BRDc.WebSiteName);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[GSTNo]", BRDc.GSTNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[GSTNo]", BRDc.GSTNo);

                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0)
                                    {
                                        foreach (var item in BRDc.BillRecieptItemInfoDc)
                                        {
                                            SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2);
                                            TotalWithItemTax += "<tr><td></td><td>" + Math.Round(Convert.ToDouble(item.Quantity), 0) + "</td><td>" + item.ItemName + "</td><td class='text-right'>" + Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2) + "</td><td></td></tr>";
                                        }
                                    }
                                    TotalWithItemTax += "<tr><td colspan='5'>&nbsp;</td></tr><tr><td></td><td></td><td>Food</td><td class='text-right'>" + SubTotal + "</td><td></td></tr>";
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0) { foreach (var tax in BRDc.BillRecieptTaxInfoDc) { double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100); TaxTotal += taxtot; TotalWithItemTax += "<tr><td></td><td></td><td>" + tax.Tax + "</td><td class='text-right'>" + Math.Round(taxtot, 2) + "</td><td></td></tr>"; } }
                                    TotalWithItemTax += "<tr><td></td><td></td><td>TOTAL DUE </td><td class='text-right'><h3>" + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</h3></td><td></td></tr>";
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/franks-logo.jpg");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/franks-logo.jpg");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "RestaurantReceipt7")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[AccountNo]", BRDc.AccountNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[AccountNo]", BRDc.AccountNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[CardEntry]", BRDc.CardEntry);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[CardEntry]", BRDc.CardEntry);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TransType]", BRDc.TransType);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TransType]", BRDc.TransType);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[CheckNo]", BRDc.CheckNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[CheckNo]", BRDc.CheckNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[CheckId]", BRDc.CheckId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[CheckId]", BRDc.CheckId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Tip]", BRDc.Tip);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Tip]", BRDc.Tip);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[SoldBy]", BRDc.SoldBy);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[SoldBy]", BRDc.SoldBy);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PayAmount]", BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PayAmount]", BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Currency]", BRDc.Currency);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Currency]", BRDc.Currency);
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "TaxiReceipt1")
                                {

                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptDate]", Convert.ToDateTime(BRDc.RecieptDate.ToString()).ToString("dd.MM.yy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptDate]", Convert.ToDateTime(BRDc.RecieptDate.ToString()).ToString("dd.MM.yy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PickupTime]", Convert.ToDateTime(BRDc.PickupTime.ToString()).ToString("hh:mm:ss"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PickupTime]", Convert.ToDateTime(BRDc.PickupTime.ToString()).ToString("hh:mm:ss"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DropTime]", Convert.ToDateTime(BRDc.DropTime.ToString()).ToString("hh:mm:ss"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DropTime]", Convert.ToDateTime(BRDc.DropTime.ToString()).ToString("hh:mm:ss"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TaxiKM]", BRDc.TaxiKM);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TaxiKM]", BRDc.TaxiKM);
                                    string totaltime = "";
                                    try
                                    {
                                        TimeSpan span = (Convert.ToDateTime(BRDc.DropTime) - Convert.ToDateTime(BRDc.PickupTime));
                                        if (span.Days > 0)
                                            totaltime = String.Format("{0}:{1}", span.Hours + (span.Days * 24), span.Minutes);
                                        else
                                            totaltime = String.Format("{0}:{1}", span.Hours, span.Minutes);
                                    }
                                    catch { }
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TaxiWTTime]", totaltime);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TaxiWTTime]", totaltime);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BaseFare]", BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BaseFare]", BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TaxiCallNo]", BRDc.TaxiCallNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TaxiCallNo]", BRDc.TaxiCallNo);
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "TaxiReceipt2")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RideBy]", BRDc.RideBy);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RideBy]", BRDc.RideBy);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptDate]", Convert.ToDateTime(BRDc.RecieptDate.ToString()).ToString("MMMM dd, yyy hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptDate]", Convert.ToDateTime(BRDc.RecieptDate.ToString()).ToString("MMMM dd, yyyy hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BaseFare]", BRDc.Currency + "" + BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BaseFare]", BRDc.Currency + "" + BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Taxes]", BRDc.Taxes.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Taxes]", BRDc.Taxes.ToString());
                                    string taxamount = Math.Round(double.Parse(((BRDc.BaseFare * BRDc.Taxes) / 100).ToString()), 2).ToString();
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TaxAmount]", BRDc.Currency + "" + taxamount);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TaxAmount]", BRDc.Currency + "" + taxamount);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[LiftLineDiscountPrice]", BRDc.Currency + "" + BRDc.LiftLineDiscountPrice.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[LiftLineDiscountPrice]", BRDc.Currency + "" + BRDc.LiftLineDiscountPrice.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PromotionalPricing]", BRDc.Currency + "" + BRDc.PromotionalPricing.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PromotionalPricing]", BRDc.Currency + "" + BRDc.PromotionalPricing.ToString());
                                    string totalamount = ((BRDc.BaseFare + decimal.Parse(taxamount)) - (BRDc.LiftLineDiscountPrice + BRDc.PromotionalPricing)).ToString();
                                    totalamount = Math.Round(double.Parse(totalamount), 2).ToString();
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TotalAmount]", BRDc.Currency + "" + totalamount);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TotalAmount]", BRDc.Currency + "" + totalamount);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PickupTime]", Convert.ToDateTime(BRDc.PickupTime.ToString()).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PickupTime]", Convert.ToDateTime(BRDc.PickupTime.ToString()).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DropTime]", Convert.ToDateTime(BRDc.DropTime.ToString()).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DropTime]", Convert.ToDateTime(BRDc.DropTime.ToString()).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PickupAddress]", BRDc.PickupAddress.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PickupAddress]", BRDc.PickupAddress.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DropAddress]", BRDc.DropAddress.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DropAddress]", BRDc.DropAddress.ToString());
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/lyft-logo.png");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/lyft-logo.png");
                                    }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "TaxiReceipt3")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BaseFare]", BRDc.Currency + "" + BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BaseFare]", BRDc.Currency + "" + BRDc.BaseFare.ToString());
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "TaxiReceipt4")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptNo]", BRDc.RecieptNo);

                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0) { foreach (var item in BRDc.BillRecieptItemInfoDc) { SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2); TotalWithItemTax += "<tr class='uppercase'><td colspan='2'>" + item.ItemName + "</td><td class='text-right'>" + BRDc.Currency + "" + item.Price + "</td></tr>"; } }
                                    TotalWithItemTax += "<tr class='uppercase'><td colspan='3'>&nbsp;</td></tr><tr><td class='text-right'> SUBTOTAL: </td><td  style='width:10%;'></td><td class='text-right'>" + BRDc.Currency + "" + SubTotal + "</td></tr>";
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0) { foreach (var tax in BRDc.BillRecieptTaxInfoDc) { double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100); TaxTotal += taxtot; TotalWithItemTax += "<tr class='uppercase'><td class='text-right'>" + tax.Tax + ":</td><td></td><td class='text-right'>" + BRDc.Currency + " " + Math.Round(taxtot, 2) + "</td></tr>"; } }
                                    TotalWithItemTax += "<tr class='uppercase'><td class='text-right'>TOTAL: </td><td></td><td class='text-right'>" + BRDc.Currency + "" + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</td></tr>";

                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TotalAmount]", BRDc.Currency + "" + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TotalAmount]", BRDc.Currency + "" + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2));
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "TaxiReceipt5")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BaseFare]", BRDc.Currency + "" + BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BaseFare]", BRDc.Currency + "" + BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[SubCharges]", BRDc.Currency + "" + BRDc.SubCharges.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[SubCharges]", BRDc.Currency + "" + BRDc.SubCharges.ToString());
                                    string totalamount = (BRDc.BaseFare + BRDc.SubCharges).ToString();
                                    totalamount = Math.Round(double.Parse(totalamount), 2).ToString();
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TotalAmount]", BRDc.Currency + "" + totalamount);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TotalAmount]", BRDc.Currency + "" + totalamount);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[CarName]", BRDc.CarName);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[CarName]", BRDc.CarName);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TaxiKM]", BRDc.TaxiKM);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TaxiKM]", BRDc.TaxiKM);
                                    string totaltime = "";
                                    try
                                    {
                                        TimeSpan span = (Convert.ToDateTime(BRDc.DropTime) - Convert.ToDateTime(BRDc.PickupTime));
                                        if (span.Days > 0)
                                            totaltime = String.Format("{0}:{1}:{2}", span.Hours + (span.Days * 24), span.Minutes, span.Seconds);
                                        else
                                            totaltime = String.Format("{0}:{1}:{2}", span.Hours, span.Minutes, span.Seconds);
                                    }
                                    catch { }
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TaxiWTTime]", totaltime);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TaxiWTTime]", totaltime);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptDate]", Convert.ToDateTime(BRDc.RecieptDate.ToString()).ToString("dd MMMM yyyy, hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptDate]", Convert.ToDateTime(BRDc.RecieptDate.ToString()).ToString("dd MMMM yyyy, hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PickupTime]", Convert.ToDateTime(BRDc.PickupTime.ToString()).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PickupTime]", Convert.ToDateTime(BRDc.PickupTime.ToString()).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DropTime]", Convert.ToDateTime(BRDc.DropTime.ToString()).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DropTime]", Convert.ToDateTime(BRDc.DropTime.ToString()).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PickupAddress]", BRDc.PickupAddress.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PickupAddress]", BRDc.PickupAddress.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DropAddress]", BRDc.DropAddress.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DropAddress]", BRDc.DropAddress.ToString());
                                    try
                                    {
                                        string startrating = "";
                                        if (BRDc.Rating > 0)
                                        {
                                            if (BRDc.Rating == 1)
                                                startrating = "&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/yellowstar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/graystar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/graystar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/graystar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/graystar.png' width='15px;'>";
                                            else if (BRDc.Rating == 2)
                                                startrating = "&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/yellowstar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/yellowstar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/graystar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/graystar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/graystar.png' width='15px;'>";
                                            else if (BRDc.Rating == 3)
                                                startrating = "&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/yellowstar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/yellowstar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/yellowstar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/graystar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/graystar.png' width='15px;'>";
                                            else if (BRDc.Rating == 4)
                                                startrating = "&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/yellowstar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/yellowstar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/yellowstar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/yellowstar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/graystar.png' width='15px;'>";
                                            else if (BRDc.Rating == 5)
                                                startrating = "&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/yellowstar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/yellowstar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/yellowstar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/yellowstar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/yellowstar.png' width='15px;'>";
                                        }
                                        else
                                        {
                                            startrating = "&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/graystar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/graystar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/graystar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/graystar.png' width='15px;'>&nbsp;&nbsp;&nbsp;<img src='http://app.bills99.com/assets/images/graystar.png' width='15px;'>";
                                        }

                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[StarRating]", startrating);
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[StarRating]", startrating);
                                    }
                                    catch { }
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "TaxiReceipt6")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BaseFare]", BRDc.Currency + "" + BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BaseFare]", BRDc.Currency + "" + BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PromotionalPricing]", BRDc.Currency + "" + BRDc.PromotionalPricing.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PromotionalPricing]", BRDc.Currency + "" + BRDc.PromotionalPricing.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillTo]", BRDc.BillTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillTo]", BRDc.BillTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[LicensePlate]", BRDc.LicensePlate);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[LicensePlate]", BRDc.LicensePlate);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TaxiKM]", BRDc.TaxiKM);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TaxiKM]", BRDc.TaxiKM);
                                    string totaltime = "";
                                    try
                                    {
                                        TimeSpan span = (Convert.ToDateTime(BRDc.DropTime) - Convert.ToDateTime(BRDc.PickupTime));
                                        if (span.Days > 0)
                                            totaltime = String.Format("{0} h {1} min", span.Hours + (span.Days * 24), span.Minutes);
                                        else
                                            totaltime = String.Format("{0} h {1} min", span.Hours, span.Minutes);
                                    }
                                    catch { }
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TaxiWTTime]", totaltime);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TaxiWTTime]", totaltime);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptDate]", Convert.ToDateTime(BRDc.RecieptDate.ToString()).ToString("ddd, MMM dd, yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptDate]", Convert.ToDateTime(BRDc.RecieptDate.ToString()).ToString("ddd, MMM dd, yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PickupTime]", Convert.ToDateTime(BRDc.PickupTime.ToString()).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PickupTime]", Convert.ToDateTime(BRDc.PickupTime.ToString()).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DropTime]", Convert.ToDateTime(BRDc.DropTime.ToString()).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DropTime]", Convert.ToDateTime(BRDc.DropTime.ToString()).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PickupAddress]", BRDc.PickupAddress.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PickupAddress]", BRDc.PickupAddress.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DropAddress]", BRDc.DropAddress.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DropAddress]", BRDc.DropAddress.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RideBy]", BRDc.RideBy);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RideBy]", BRDc.RideBy);
                                    decimal? befotetaxes = BRDc.BaseFare - BRDc.PromotionalPricing;
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0) { foreach (var tax in BRDc.BillRecieptTaxInfoDc) { double taxtot = ((double.Parse(befotetaxes.ToString()) * double.Parse(tax.TaxInPercentage.ToString())) / 100); TaxTotal += taxtot; TotalWithItemTax += "<tr><td>" + tax.Tax + " (" + tax.TaxInPercentage + "%):</td><td class='text-right'>" + BRDc.Currency + "" + Math.Round(taxtot, 2) + "</td></tr>"; } }
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BeforeTaxes]", BRDc.Currency + "" + befotetaxes);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BeforeTaxes]", BRDc.Currency + "" + befotetaxes);
                                    string totalamount = Math.Round((double.Parse(befotetaxes.ToString()) + TaxTotal), 2).ToString();
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TotalAmount]", BRDc.Currency + "" + totalamount);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TotalAmount]", BRDc.Currency + "" + totalamount);

                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "TaxiReceipt7")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BaseFare]", BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BaseFare]", BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DistanceFare]", BRDc.DistanceFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DistanceFare]", BRDc.DistanceFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TimeFare]", BRDc.TimeFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TimeFare]", BRDc.TimeFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[SafeRideFee]", BRDc.SafeRideFee.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[SafeRideFee]", BRDc.SafeRideFee.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillTo]", BRDc.BillTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillTo]", BRDc.BillTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RideBy]", BRDc.RideBy);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RideBy]", BRDc.RideBy);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[SoldBy]", BRDc.SoldBy);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[SoldBy]", BRDc.SoldBy);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[CarName]", BRDc.CarName);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[CarName]", BRDc.CarName);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TaxiKM]", BRDc.TaxiKM);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TaxiKM]", BRDc.TaxiKM);
                                    string totaltime = "";
                                    try
                                    {
                                        TimeSpan span = (Convert.ToDateTime(BRDc.DropTime) - Convert.ToDateTime(BRDc.PickupTime));
                                        if (span.Days > 0)
                                            totaltime = String.Format("{0}:{1}:{2}", span.Hours + (span.Days * 24), span.Minutes, span.Seconds);
                                        else
                                            totaltime = String.Format("{0}:{1}:{2}", span.Hours, span.Minutes, span.Seconds);
                                    }
                                    catch { }
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TaxiWTTime]", totaltime);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TaxiWTTime]", totaltime);
                                    decimal? befotefee = decimal.Parse(Math.Round(double.Parse((BRDc.BaseFare + BRDc.DistanceFare + BRDc.TimeFare).ToString()), 2).ToString());
                                    string totalamount = Math.Round(double.Parse((befotefee + BRDc.SafeRideFee).ToString()), 2).ToString();
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TotalAmount]", BRDc.Currency + "" + totalamount);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TotalAmount]", BRDc.Currency + "" + totalamount);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BeforeFee]", BRDc.Currency + "" + befotefee);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BeforeFee]", BRDc.Currency + "" + befotefee);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptDate]", Convert.ToDateTime(BRDc.RecieptDate.ToString()).ToString("MMMM dd, yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptDate]", Convert.ToDateTime(BRDc.RecieptDate.ToString()).ToString("MMMM dd, yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PickupTime]", Convert.ToDateTime(BRDc.PickupTime.ToString()).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PickupTime]", Convert.ToDateTime(BRDc.PickupTime.ToString()).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DropTime]", Convert.ToDateTime(BRDc.DropTime.ToString()).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DropTime]", Convert.ToDateTime(BRDc.DropTime.ToString()).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PickupAddress]", BRDc.PickupAddress.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PickupAddress]", BRDc.PickupAddress.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DropAddress]", BRDc.DropAddress.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DropAddress]", BRDc.DropAddress.ToString());
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "TaxiReceipt8")
                                {
                                    if (string.IsNullOrEmpty(BRDc.RecieptLogo))
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/olalogo.png");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptLogo]", ConfigurationManager.AppSettings["BaseUrl"].ToString() + "RecieptLogo/olalogo.png");
                                    }

                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BaseFare]", BRDc.Currency + "" + BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BaseFare]", BRDc.Currency + "" + BRDc.BaseFare.ToString());
                                    decimal? TotalROAmount = decimal.Parse(Math.Round(double.Parse(BRDc.BaseFare.ToString()), 0).ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TotalROAmount]", BRDc.Currency + "" + TotalROAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TotalROAmount]", BRDc.Currency + "" + TotalROAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[SafeRideFee]", BRDc.Currency + "" + BRDc.SafeRideFee.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[SafeRideFee]", BRDc.Currency + "" + BRDc.SafeRideFee.ToString());
                                     
                                    string totalamount = Math.Round(double.Parse((TotalROAmount + BRDc.SafeRideFee).ToString()), 2).ToString();

                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TotalAmount]", BRDc.Currency + "" + totalamount);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TotalAmount]", BRDc.Currency + "" + totalamount);

                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BillTo]", BRDc.BillTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BillTo]", BRDc.BillTo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RideBy]", BRDc.RideBy);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RideBy]", BRDc.RideBy); 
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[CarName]", BRDc.CarName);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[CarName]", BRDc.CarName);

                                    
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptDate]", Convert.ToDateTime(BRDc.RecieptDate.ToString()).ToString("dd MMM, yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptDate]", Convert.ToDateTime(BRDc.RecieptDate.ToString()).ToString("dd MMM, yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptAvailDate]", Convert.ToDateTime(BRDc.RecieptDate.ToString()).AddMonths(+1).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptAvailDate]", Convert.ToDateTime(BRDc.RecieptDate.ToString()).AddMonths(+1).ToString("dd/MM/yyyy"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PickupTime]", Convert.ToDateTime(BRDc.PickupTime.ToString()).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PickupTime]", Convert.ToDateTime(BRDc.PickupTime.ToString()).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DropTime]", Convert.ToDateTime(BRDc.DropTime.ToString()).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DropTime]", Convert.ToDateTime(BRDc.DropTime.ToString()).ToString("hh:mm tt"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PickupAddress]", BRDc.PickupAddress.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PickupAddress]", BRDc.PickupAddress.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[DropAddress]", BRDc.DropAddress.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[DropAddress]", BRDc.DropAddress.ToString());
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "TollReceipt1")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptDate]", Convert.ToDateTime(BRDc.RecieptDate.ToString()).ToString("dd/MMM/yyyy hh:mm:ss"));
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptDate]", Convert.ToDateTime(BRDc.RecieptDate.ToString()).ToString("dd/MMM/yyyy hh:mm:ss"));
                                    if (BRDc.ExitDate.ToString() != null && BRDc.ExitDate.ToString() != "" && BRDc.ExitDate.ToString() != "0")
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ExitDate]", Convert.ToDateTime(BRDc.ExitDate.ToString()).ToString("dd/MMM/yyyy hh:mm:ss"));
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ExitDate]", Convert.ToDateTime(BRDc.ExitDate.ToString()).ToString("dd/MMM/yyyy hh:mm:ss"));
                                    }
                                    else
                                    {
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ExitDate]", "");
                                        BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ExitDate]", "");
                                    }
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TollPlaza]", BRDc.TollPlaza);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TollPlaza]", BRDc.TollPlaza);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Lane]", BRDc.Lane);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Lane]", BRDc.Lane);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Operator]", BRDc.Operator);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Operator]", BRDc.Operator);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[VehicleType]", BRDc.VehicleType);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[VehicleType]", BRDc.VehicleType);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BaseFare]", BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BaseFare]", BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PaymentService]", BRDc.PaymentService);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PaymentService]", BRDc.PaymentService);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[VehicleNo]", BRDc.VehicleNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[VehicleNo]", BRDc.VehicleNo);
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "TollReceipt2")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TollPlaza]", BRDc.TollPlaza);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TollPlaza]", BRDc.TollPlaza);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Section]", BRDc.Section);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Section]", BRDc.Section);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Operator]", BRDc.Operator);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Operator]", BRDc.Operator);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TicketNo]", BRDc.TicketNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TicketNo]", BRDc.TicketNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[VehicleNo]", BRDc.VehicleNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[VehicleNo]", BRDc.VehicleNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[VehicleType]", BRDc.VehicleType);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[VehicleType]", BRDc.VehicleType);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Journey]", BRDc.Journey);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Journey]", BRDc.Journey);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BaseFare]", BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BaseFare]", BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[VehicleStanderedWeight]", BRDc.VehicleStanderedWeight);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[VehicleStanderedWeight]", BRDc.VehicleStanderedWeight);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[VehicleActualWeight]", BRDc.VehicleActualWeight);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[VehicleActualWeight]", BRDc.VehicleActualWeight);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[OverloadedVehicleFee]", BRDc.OverloadedVehicleFee.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[OverloadedVehicleFee]", BRDc.OverloadedVehicleFee.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Currency]", BRDc.Currency);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Currency]", BRDc.Currency);
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "TollReceipt3")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RelationShipNo]", BRDc.RelationShipNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RelationShipNo]", BRDc.RelationShipNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[EntryDate]", BRDc.EntryDate.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[EntryDate]", BRDc.EntryDate.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[ExitDate]", BRDc.ExitDate.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[ExitDate]", BRDc.ExitDate.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Lane]", BRDc.Lane);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Lane]", BRDc.Lane);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Shift]", BRDc.Shift);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Shift]", BRDc.Shift);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[VehicleType]", BRDc.VehicleType);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[VehicleType]", BRDc.VehicleType);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Journey]", BRDc.Journey);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Journey]", BRDc.Journey);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[VehicleNo]", BRDc.VehicleNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[VehicleNo]", BRDc.VehicleNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TCId]", BRDc.TCId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TCId]", BRDc.TCId);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[VehicleActualWeight]", BRDc.VehicleActualWeight);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[VehicleActualWeight]", BRDc.VehicleActualWeight);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[PayAmount]", BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[PayAmount]", BRDc.PayAmount.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Currency]", BRDc.Currency);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Currency]", BRDc.Currency);
                                }
                                else if (BillRecieptDc.CategoryRecieptPDFDc.RecieptType == "TollReceipt4")
                                {
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[TollPlaza]", BRDc.TollPlaza);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[TollPlaza]", BRDc.TollPlaza);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Lane]", BRDc.Lane);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Lane]", BRDc.Lane);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Operator]", BRDc.Operator);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Operator]", BRDc.Operator);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[VehicleType]", BRDc.VehicleType);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[VehicleType]", BRDc.VehicleType);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RegNo]", BRDc.RegNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RegNo]", BRDc.RegNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[CSH]", BRDc.CSH);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[CSH]", BRDc.CSH);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[BaseFare]", BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[BaseFare]", BRDc.BaseFare.ToString());
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[RecieptNo]", BRDc.RecieptNo);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Currency]", BRDc.Currency);
                                    BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Currency]", BRDc.Currency);
                                }
                                else
                                {
                                    if (BRDc.BillRecieptItemInfoDc != null && BRDc.BillRecieptItemInfoDc.Count > 0) { foreach (var item in BRDc.BillRecieptItemInfoDc) { SubTotal += Math.Round(Convert.ToDouble(item.Quantity * item.Price), 2); } }
                                    TotalWithItemTax = "<table width='100%' class='custom-table aovel-font' style='text-transform:uppercase;'><tr><td width='40%'>SubTotal:</td><td>" + BRDc.Currency + " " + SubTotal + "</td></tr>";
                                    if (BRDc.BillRecieptTaxInfoDc != null && BRDc.BillRecieptTaxInfoDc.Count > 0) { foreach (var tax in BRDc.BillRecieptTaxInfoDc) { double taxtot = ((SubTotal * double.Parse(tax.TaxInPercentage.ToString())) / 100); TaxTotal += taxtot; TotalWithItemTax += "<tr><td>" + tax.Tax + ":</td><td>" + BRDc.Currency + " " + Math.Round(taxtot, 2) + "</td></tr>"; } } else { TotalWithItemTax += "<tr><td>NO TAX:</td><td>" + BRDc.Currency + " 0.00</td></tr>"; }
                                    TotalWithItemTax += "<tr><td>Total:</td><td>" + BRDc.Currency + " " + Math.Round(Convert.ToDouble(SubTotal + TaxTotal), 2) + "</td></tr></table>";
                                }
                                #endregion
                            }
                            catch (Exception ex) { }
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml.Replace("[Total]", TotalWithItemTax);
                            BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml = BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("[Total]", TotalWithItemTax);
                            htmltopdf(HttpContext.Current.Server.MapPath("~/RecieptPdf/bill_reciept_" + BillRecieptDc.BillRecieptId + ".pdf"), BillRecieptDc.CategoryRecieptPDFDc.BillRecieptPdfHtml.Replace("custom-table " + FontDc.FontClass, "dynamic-font"), BillRecieptDc.CategoryRecieptPDFDc.ReceiptWidth, BillRecieptDc.CategoryRecieptPDFDc.ReceiptHight, BillRecieptDc.SubscriptionPlanId);
                            BillRecieptDc.BillRecieptId = BillRecieptManager.Update_DynamicRecieptHTML(BillRecieptDc.BillRecieptId, BillRecieptDc.CategoryRecieptPDFDc.BillRecieptDynamicHtml);
                        }
                    }
                    catch (Exception ex) { }
                }
            }
            catch (Exception ex)
            {
            }
            return BillRecieptDc.BillRecieptId;
        }

        public string ConvertNumbertoWords(int number)
        {
            if (number == 0)
                return "Zero";
            if (number < 0)
                return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";

            if ((number / 1000000000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000000000) + " Billion ";
                number %= 1000000000;
            }

            if ((number / 10000000) > 0)
            {
                words += ConvertNumbertoWords(number / 10000000) + " Crore ";
                number %= 10000000;
            }

            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000000) + " Million ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " Thousand ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " Hundred ";
                number %= 100;
            }
            if (number > 0)
            {
                if (words != "")
                    words += "And ";
                var unitsMap = new[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Ninteen" };
                var tensMap = new[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }

        #region html to pdf 
        public string htmltopdf(string filepath, string htmltext, string ReceiptWidth, string ReceiptHight, int SubscriptionPlanId)
        {
            CreatePDFManager CreatePDFManager = new CreatePDFManager();
            try
            {
                CreatePDFManager.CreatePDF(filepath, htmltext, "pdfcrowd", ReceiptWidth, ReceiptHight, SubscriptionPlanId);
                //CreatePDFManager.CreatePDF(filepath, htmltext, "itextsharp");
            }
            catch (Exception ex)
            {
            }
            return "Ok";
        }

        #endregion

        #endregion

        #region Get 

        #region Get Bill Reciept By Id
        [HttpGet]
        public BillRecieptDc GetBillRecieptById(int BillRecieptId)
        {
            BillRecieptDc BillRecieptDc = null;
            BillRecieptManager BillRecieptManager = new BillRecieptManager();
            try
            {
                BillRecieptDc = BillRecieptManager.GetBillRecieptById(BillRecieptId);
            }
            catch (Exception ex)
            {
            }
            return BillRecieptDc;
        }
        #endregion

        #region Search Bill Reciept
        [HttpPost]
        public List<BillRecieptDc> SearchBillReciept(BillRecieptSearchDc BillRecieptSearchDc)
        {
            List<BillRecieptDc> BillRecieptDc = null;
            BillRecieptManager BillRecieptManager = new BillRecieptManager();
            try
            {
                BillRecieptDc = BillRecieptManager.SearchBillReciept(BillRecieptSearchDc);
            }
            catch (Exception ex)
            {
            }
            return BillRecieptDc;
        }
        #endregion

        #endregion

        #region Delete Bill Reciept By Id 
        [HttpGet]
        public bool DeleteBillRecieptById(int BillRecieptId)
        {
            bool Ans = false;
            try
            {
                BillRecieptManager BillRecieptManager = new BillRecieptManager();
                Ans = BillRecieptManager.DeleteBillReciept(BillRecieptId);
            }
            catch (Exception ex)
            {
            }
            return Ans;
        }
        #endregion

        #endregion

        //#region Bill Reciept Tax

        //#region Add Update Bill Reciept Tax
        //[HttpPost]
        //public long AddUpdateBillRecieptTax(BillRecieptTaxInfoDc BillRecieptTaxInfoDc)
        //{
        //    BillRecieptManager BillRecieptManager = new BillRecieptManager();
        //    try
        //    {
        //        BillRecieptTaxInfoDc.BillRecieptTaxId = BillRecieptManager.InsertUpdate_BillRecieptTax(BillRecieptTaxInfoDc);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return BillRecieptTaxInfoDc.BillRecieptTaxId;
        //}
        //#endregion

        //#region Delete Bill Reciept Tax Info By Id 
        //[HttpGet]
        //public bool DeleteBillRecieptTaxInfoById(int BillRecieptTaxId)
        //{
        //    bool Ans = false;
        //    try
        //    {
        //        BillRecieptManager BillRecieptManager = new BillRecieptManager();
        //        Ans = BillRecieptManager.DeleteBillRecieptTax(BillRecieptTaxId);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return Ans;
        //}
        //#endregion

        //#endregion

        //#region Bill Reciept Item

        //#region Add Update Bill Reciept Item
        //[HttpPost]
        //public long AddUpdateBillRecieptItem(BillRecieptItemInfoDc BillRecieptItemInfoDc)
        //{
        //    BillRecieptManager BillRecieptManager = new BillRecieptManager();
        //    try
        //    {
        //        BillRecieptItemInfoDc.BillRecieptItemId = BillRecieptManager.InsertUpdate_BillRecieptItem(BillRecieptItemInfoDc);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return BillRecieptItemInfoDc.BillRecieptItemId;
        //}
        //#endregion

        //#region Delete Bill Reciept Item Info By Id 
        //[HttpGet]
        //public bool DeleteBillRecieptItemInfoById(int BillRecieptItemId)
        //{
        //    bool Ans = false;
        //    try
        //    {
        //        BillRecieptManager BillRecieptManager = new BillRecieptManager();
        //        Ans = BillRecieptManager.DeleteBillRecieptItem(BillRecieptItemId);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return Ans;
        //}
        //#endregion

        //#endregion

    }
}
