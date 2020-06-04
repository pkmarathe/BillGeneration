using BillGeneration.DataContract;
using BillGeneration.DataLayer;
using BillGeneration.DataLayer.EF;
using LinqKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BillGeneration.BusinessLayer
{
    public class BillRecieptManager
    {
        Repository objRep = new Repository();

        #region Add Update Bill Reciept
        public int InsertUpdate_BillReciept(BillRecieptDc Obj)
        {
            #region Add Bill Reciept
            CommonManager CommonManager = new CommonManager();
            BillRecieptTable BillReciepts = new BillRecieptTable();
            if (Obj.BillRecieptId > 0)
            {
                BillReciepts = objRep.Get<BillRecieptTable>(Obj.BillRecieptId);
                BillReciepts.UserId = Obj.UserId;
                BillReciepts.CategoryRecieptId = Obj.CategoryRecieptId;
                BillReciepts.CurrencyId = Obj.CurrencyId;
                BillReciepts.FontId = Obj.FontId;
                BillReciepts.RecieptDate = Obj.RecieptDate;
                BillReciepts.Business = Obj.Business;
                BillReciepts.Address = Obj.Address;
                BillReciepts.CityState = Obj.CityState;
                BillReciepts.MobileNo = Obj.MobileNo;
                BillReciepts.RecieptLogo = Obj.RecieptLogo;
                BillReciepts.PaymentType = Obj.PaymentType;
                BillReciepts.Auth = Obj.Auth;
                BillReciepts.Trans = Obj.Trans;
                BillReciepts.Mcc = Obj.Mcc;
                BillReciepts.ApprCode = Obj.ApprCode;
                //BillReciepts.BillRecieptDynamicHtml = Obj.BillRecieptDynamicHtml;
                BillReciepts.WebSiteName = Obj.WebSiteName;
                BillReciepts.DocNo = Obj.DocNo;
                BillReciepts.GSTNo = Obj.GSTNo;
                BillReciepts.BillNo = Obj.BillNo;
                BillReciepts.AtndId = Obj.AtndId;
                BillReciepts.FPId = Obj.FPId;
                BillReciepts.Pump = Obj.Pump;
                BillReciepts.NozlNo = Obj.NozlNo;
                BillReciepts.Fuel = Obj.Fuel;
                BillReciepts.Density = Obj.Density;
                BillReciepts.TinNo = Obj.TinNo;
                BillReciepts.TRXId = Obj.TRXId;
                BillReciepts.AccountNo = Obj.AccountNo;
                BillReciepts.PompaSalans = Obj.PompaSalans;
                BillReciepts.NomarNota = Obj.NomarNota;
                BillReciepts.JenisBBM = Obj.JenisBBM;
                BillReciepts.StationNo = Obj.StationNo;
                BillReciepts.HelplineNo = Obj.HelplineNo;
                BillReciepts.LandlineNo = Obj.LandlineNo;
                BillReciepts.CINNo = Obj.CINNo;
                BillReciepts.STNo = Obj.STNo;
                BillReciepts.OPNo = Obj.OPNo;
                BillReciepts.TENo = Obj.TENo;
                BillReciepts.TCNo = Obj.TCNo;
                BillReciepts.REFNo = Obj.REFNo;
                BillReciepts.Validation = Obj.Validation;
                BillReciepts.PaymentService = Obj.PaymentService;
                BillReciepts.PayAmount = Obj.PayAmount;
                BillReciepts.Lane = Obj.Lane;
                BillReciepts.Clerk = Obj.Clerk;
                BillReciepts.Cashier = Obj.Cashier;
                BillReciepts.MRCH = Obj.MRCH;
                BillReciepts.EPSSequence = Obj.EPSSequence;
                BillReciepts.NetworkId = Obj.NetworkId;
                BillReciepts.TerminalId = Obj.TerminalId;
                BillReciepts.SpaceNo = Obj.SpaceNo;
                BillReciepts.TicketNo = Obj.TicketNo;
                BillReciepts.SNNo = Obj.SNNo;
                BillReciepts.Lot_Location = Obj.Lot_Location;
                BillReciepts.PayedAt = Obj.PayedAt;
                BillReciepts.EntryDate = Obj.EntryDate;
                BillReciepts.ExitDate = Obj.ExitDate;
                BillReciepts.MachName = Obj.MachName;
                BillReciepts.VehicleNo = Obj.VehicleNo;
                BillReciepts.VehicleType = Obj.VehicleType;
                BillReciepts.RegNo = Obj.RegNo;
                BillReciepts.TRNNo = Obj.TRNNo;
                BillReciepts.CSHRNo = Obj.CSHRNo;
                BillReciepts.STRNo = Obj.STRNo;
                BillReciepts.HelpedBy = Obj.HelpedBy;
                BillReciepts.AID = Obj.AID;
                BillReciepts.Signature = Obj.Signature;
                BillReciepts.CVM = Obj.CVM;
                BillReciepts.StoreNo = Obj.StoreNo;
                BillReciepts.PharmacyNo = Obj.PharmacyNo;
                BillReciepts.OrderNo = Obj.OrderNo;
                BillReciepts.OrderDate = Obj.OrderDate;
                BillReciepts.DeliveryDate = Obj.DeliveryDate;
                BillReciepts.Host = Obj.Host;
                BillReciepts.FaxNo = Obj.FaxNo;
                BillReciepts.BillTo = Obj.BillTo;
                BillReciepts.ShipTo = Obj.ShipTo;
                BillReciepts.CustomerNo = Obj.CustomerNo;
                BillReciepts.CustomerPONo = Obj.CustomerPONo;
                BillReciepts.CardEntry = Obj.CardEntry;
                BillReciepts.TransType = Obj.TransType;
                BillReciepts.CheckNo = Obj.CheckNo;
                BillReciepts.CheckId = Obj.CheckId;
                BillReciepts.Tip = Obj.Tip;
                BillReciepts.TaxiDate = Obj.TaxiDate;
                BillReciepts.PickupTime = Obj.PickupTime;
                BillReciepts.DropTime = Obj.DropTime;
                BillReciepts.PickupAddress = Obj.PickupAddress;
                BillReciepts.DropAddress = Obj.DropAddress;
                BillReciepts.TaxiKM = Obj.TaxiKM;
                BillReciepts.CarName = Obj.CarName;
                BillReciepts.Miles = Obj.Miles;
                BillReciepts.TaxiWTTime = Obj.TaxiWTTime;
                BillReciepts.BaseFare = Obj.BaseFare;
                BillReciepts.DistanceFare = Obj.DistanceFare;
                BillReciepts.TimeFare = Obj.TimeFare;
                BillReciepts.SubCharges = Obj.SubCharges;
                BillReciepts.SafeRideFee = Obj.SafeRideFee;
                BillReciepts.TaxiCallNo = Obj.TaxiCallNo;
                BillReciepts.ProfileImage = Obj.ProfileImage;
                BillReciepts.LiftLineDiscountPrice = Obj.LiftLineDiscountPrice;
                BillReciepts.PromotionalPricing = Obj.PromotionalPricing;
                BillReciepts.RideBy = Obj.RideBy;
                BillReciepts.LicensePlate = Obj.LicensePlate;
                BillReciepts.TollPlaza = Obj.TollPlaza;
                BillReciepts.Operator = Obj.Operator;
                BillReciepts.Section = Obj.Section;
                BillReciepts.VehicleStanderedWeight = Obj.VehicleStanderedWeight;
                BillReciepts.VehicleActualWeight = Obj.VehicleActualWeight;
                BillReciepts.OverloadedVehicleFee = Obj.OverloadedVehicleFee;
                BillReciepts.Shift = Obj.Shift;
                BillReciepts.Journey = Obj.Journey;
                BillReciepts.TCId = Obj.TCId;
                BillReciepts.BarCode = Obj.BarCode;
                BillReciepts.CSH = Obj.CSH;
                BillReciepts.RelationShipNo = Obj.RelationShipNo;
                BillReciepts.BillDate = Obj.BillDate;
                BillReciepts.DueDate = Obj.DueDate;
                BillReciepts.LandMark = Obj.LandMark;
                BillReciepts.BillPeriod = Obj.BillPeriod;
                BillReciepts.BillPayDate = Obj.BillPayDate;
                BillReciepts.CreditLimit = Obj.CreditLimit;
                BillReciepts.SecurityDeposit = Obj.SecurityDeposit;
                BillReciepts.PreviousBalance = Obj.PreviousBalance;
                BillReciepts.Payments = Obj.Payments;
                BillReciepts.Adjustments = Obj.Adjustments;
                BillReciepts.LatePaymentFee = Obj.LatePaymentFee;
                BillReciepts.MonthlyRentals = Obj.MonthlyRentals;
                BillReciepts.UsageCharges = Obj.UsageCharges;
                BillReciepts.OneTimeCharges = Obj.OneTimeCharges;
                BillReciepts.Taxes = Obj.Taxes;
                BillReciepts.ServiceTaxRegNo = Obj.ServiceTaxRegNo;
                BillReciepts.FixedlineNo = Obj.FixedlineNo;
                BillReciepts.BroadbandId = Obj.BroadbandId;
                BillReciepts.AlternateMobileNo = Obj.AlternateMobileNo;
                BillReciepts.ShipToStateCode = Obj.ShipToStateCode;
                BillReciepts.BillGenerateBy = Obj.BillGenerateBy;
                BillReciepts.SoldBy = Obj.SoldBy;
                BillReciepts.SyNo = Obj.SyNo;
                BillReciepts.PANNo = Obj.PANNo;
                BillReciepts.InvoiceDetails = Obj.InvoiceDetails;
                BillReciepts.AadharNo = Obj.AadharNo;
                BillReciepts.MemberShip = Obj.MemberShip;
                BillReciepts.UsageP_ISD_IRCalling = Obj.UsageP_ISD_IRCalling;
                BillReciepts.UsageDataCharges = Obj.UsageDataCharges;
                BillReciepts.UsageSMSCharges = Obj.UsageSMSCharges;
                BillReciepts.UsageVASCharges = Obj.UsageVASCharges;
                BillReciepts.CurruntMonthDiscount = Obj.CurruntMonthDiscount;
                BillReciepts.BillDiscountWithTax = Obj.BillDiscountWithTax;
                BillReciepts.RecieptNo = Obj.RecieptNo;
                BillReciepts.BillToEmailId = Obj.BillToEmailId;
                BillReciepts.Rating = Obj.Rating;
                objRep.Update<BillRecieptTable>(BillReciepts);
                CommonManager.DeleteBillRecieptTax_Item(Obj.BillRecieptId);
            }
            else
            {
                BillReciepts.BillRecieptId = 0;
                BillReciepts.UserId = Obj.UserId;
                BillReciepts.CategoryRecieptId = Obj.CategoryRecieptId;
                BillReciepts.CurrencyId = Obj.CurrencyId;
                BillReciepts.FontId = Obj.FontId;
                BillReciepts.RecieptDate = Obj.RecieptDate;
                BillReciepts.Business = Obj.Business;
                BillReciepts.Address = Obj.Address;
                BillReciepts.CityState = Obj.CityState;
                BillReciepts.MobileNo = Obj.MobileNo;
                BillReciepts.RecieptLogo = Obj.RecieptLogo;
                BillReciepts.RecieptNo = Obj.RecieptNo;
                BillReciepts.PaymentType = Obj.PaymentType;
                BillReciepts.Auth = Obj.Auth;
                BillReciepts.Trans = Obj.Trans;
                BillReciepts.Mcc = Obj.Mcc;
                BillReciepts.ApprCode = Obj.ApprCode;
                //BillReciepts.BillRecieptDynamicHtml = Obj.BillRecieptDynamicHtml;
                BillReciepts.CreatedDate = DateTime.Now;
                BillReciepts.WebSiteName = Obj.WebSiteName;
                BillReciepts.DocNo = Obj.DocNo;
                BillReciepts.GSTNo = Obj.GSTNo;
                BillReciepts.BillNo = Obj.BillNo;
                BillReciepts.AtndId = Obj.AtndId;
                BillReciepts.FPId = Obj.FPId;
                BillReciepts.Pump = Obj.Pump;
                BillReciepts.NozlNo = Obj.NozlNo;
                BillReciepts.Fuel = Obj.Fuel;
                BillReciepts.Density = Obj.Density;
                BillReciepts.TinNo = Obj.TinNo;
                BillReciepts.TRXId = Obj.TRXId;
                BillReciepts.AccountNo = Obj.AccountNo;
                BillReciepts.PompaSalans = Obj.PompaSalans;
                BillReciepts.NomarNota = Obj.NomarNota;
                BillReciepts.JenisBBM = Obj.JenisBBM;
                BillReciepts.StationNo = Obj.StationNo;
                BillReciepts.HelplineNo = Obj.HelplineNo;
                BillReciepts.LandlineNo = Obj.LandlineNo;
                BillReciepts.CINNo = Obj.CINNo;
                BillReciepts.STNo = Obj.STNo;
                BillReciepts.OPNo = Obj.OPNo;
                BillReciepts.TENo = Obj.TENo;
                BillReciepts.TCNo = Obj.TCNo;
                BillReciepts.REFNo = Obj.REFNo;
                BillReciepts.Validation = Obj.Validation;
                BillReciepts.PaymentService = Obj.PaymentService;
                BillReciepts.PayAmount = Obj.PayAmount;
                BillReciepts.Lane = Obj.Lane;
                BillReciepts.Clerk = Obj.Clerk;
                BillReciepts.Cashier = Obj.Cashier;
                BillReciepts.MRCH = Obj.MRCH;
                BillReciepts.EPSSequence = Obj.EPSSequence;
                BillReciepts.NetworkId = Obj.NetworkId;
                BillReciepts.TerminalId = Obj.TerminalId;
                BillReciepts.SpaceNo = Obj.SpaceNo;
                BillReciepts.TicketNo = Obj.TicketNo;
                BillReciepts.SNNo = Obj.SNNo;
                BillReciepts.Lot_Location = Obj.Lot_Location;
                BillReciepts.PayedAt = Obj.PayedAt;
                BillReciepts.EntryDate = Obj.EntryDate;
                BillReciepts.ExitDate = Obj.ExitDate;
                BillReciepts.MachName = Obj.MachName;
                BillReciepts.VehicleNo = Obj.VehicleNo;
                BillReciepts.VehicleType = Obj.VehicleType;
                BillReciepts.RegNo = Obj.RegNo;
                BillReciepts.TRNNo = Obj.TRNNo;
                BillReciepts.CSHRNo = Obj.CSHRNo;
                BillReciepts.STRNo = Obj.STRNo;
                BillReciepts.HelpedBy = Obj.HelpedBy;
                BillReciepts.AID = Obj.AID;
                BillReciepts.Signature = Obj.Signature;
                BillReciepts.CVM = Obj.CVM;
                BillReciepts.StoreNo = Obj.StoreNo;
                BillReciepts.PharmacyNo = Obj.PharmacyNo;
                BillReciepts.OrderNo = Obj.OrderNo;
                BillReciepts.OrderDate = Obj.OrderDate;
                BillReciepts.DeliveryDate = Obj.DeliveryDate;
                BillReciepts.Host = Obj.Host;
                BillReciepts.FaxNo = Obj.FaxNo;
                BillReciepts.BillTo = Obj.BillTo;
                BillReciepts.ShipTo = Obj.ShipTo;
                BillReciepts.CustomerNo = Obj.CustomerNo;
                BillReciepts.CustomerPONo = Obj.CustomerPONo;
                BillReciepts.CardEntry = Obj.CardEntry;
                BillReciepts.TransType = Obj.TransType;
                BillReciepts.CheckNo = Obj.CheckNo;
                BillReciepts.CheckId = Obj.CheckId;
                BillReciepts.Tip = Obj.Tip;
                BillReciepts.TaxiDate = Obj.TaxiDate;
                BillReciepts.PickupTime = Obj.PickupTime;
                BillReciepts.DropTime = Obj.DropTime;
                BillReciepts.PickupAddress = Obj.PickupAddress;
                BillReciepts.DropAddress = Obj.DropAddress;
                BillReciepts.TaxiKM = Obj.TaxiKM;
                BillReciepts.CarName = Obj.CarName;
                BillReciepts.Miles = Obj.Miles;
                BillReciepts.TaxiWTTime = Obj.TaxiWTTime;
                BillReciepts.BaseFare = Obj.BaseFare;
                BillReciepts.DistanceFare = Obj.DistanceFare;
                BillReciepts.TimeFare = Obj.TimeFare;
                BillReciepts.SubCharges = Obj.SubCharges;
                BillReciepts.SafeRideFee = Obj.SafeRideFee;
                BillReciepts.TaxiCallNo = Obj.TaxiCallNo;
                BillReciepts.ProfileImage = Obj.ProfileImage;
                BillReciepts.LiftLineDiscountPrice = Obj.LiftLineDiscountPrice;
                BillReciepts.PromotionalPricing = Obj.PromotionalPricing;
                BillReciepts.RideBy = Obj.RideBy;
                BillReciepts.LicensePlate = Obj.LicensePlate;
                BillReciepts.TollPlaza = Obj.TollPlaza;
                BillReciepts.Operator = Obj.Operator;
                BillReciepts.Section = Obj.Section;
                BillReciepts.VehicleStanderedWeight = Obj.VehicleStanderedWeight;
                BillReciepts.VehicleActualWeight = Obj.VehicleActualWeight;
                BillReciepts.OverloadedVehicleFee = Obj.OverloadedVehicleFee;
                BillReciepts.Shift = Obj.Shift;
                BillReciepts.Journey = Obj.Journey;
                BillReciepts.TCId = Obj.TCId;
                BillReciepts.BarCode = Obj.BarCode;
                BillReciepts.CSH = Obj.CSH;
                BillReciepts.RelationShipNo = Obj.RelationShipNo;
                BillReciepts.BillDate = Obj.BillDate;
                BillReciepts.DueDate = Obj.DueDate;
                BillReciepts.LandMark = Obj.LandMark;
                BillReciepts.BillPeriod = Obj.BillPeriod;
                BillReciepts.BillPayDate = Obj.BillPayDate;
                BillReciepts.CreditLimit = Obj.CreditLimit;
                BillReciepts.SecurityDeposit = Obj.SecurityDeposit;
                BillReciepts.PreviousBalance = Obj.PreviousBalance;
                BillReciepts.Payments = Obj.Payments;
                BillReciepts.Adjustments = Obj.Adjustments;
                BillReciepts.LatePaymentFee = Obj.LatePaymentFee;
                BillReciepts.MonthlyRentals = Obj.MonthlyRentals;
                BillReciepts.UsageCharges = Obj.UsageCharges;
                BillReciepts.OneTimeCharges = Obj.OneTimeCharges;
                BillReciepts.Taxes = Obj.Taxes;
                BillReciepts.ServiceTaxRegNo = Obj.ServiceTaxRegNo;
                BillReciepts.FixedlineNo = Obj.FixedlineNo;
                BillReciepts.BroadbandId = Obj.BroadbandId;
                BillReciepts.AlternateMobileNo = Obj.AlternateMobileNo;
                BillReciepts.ShipToStateCode = Obj.ShipToStateCode;
                BillReciepts.BillGenerateBy = Obj.BillGenerateBy;
                BillReciepts.SoldBy = Obj.SoldBy;
                BillReciepts.SyNo = Obj.SyNo;
                BillReciepts.PANNo = Obj.PANNo;
                BillReciepts.InvoiceDetails = Obj.InvoiceDetails;
                BillReciepts.AadharNo = Obj.AadharNo;
                BillReciepts.MemberShip = Obj.MemberShip;
                BillReciepts.UsageP_ISD_IRCalling = Obj.UsageP_ISD_IRCalling;
                BillReciepts.UsageDataCharges = Obj.UsageDataCharges;
                BillReciepts.UsageSMSCharges = Obj.UsageSMSCharges;
                BillReciepts.UsageVASCharges = Obj.UsageVASCharges;
                BillReciepts.CurruntMonthDiscount = Obj.CurruntMonthDiscount;
                BillReciepts.BillDiscountWithTax = Obj.BillDiscountWithTax;
                BillReciepts.RecieptNo = Obj.RecieptNo;
                BillReciepts.BillToEmailId = Obj.BillToEmailId;
                BillReciepts.Rating = Obj.Rating;
                objRep.Add<BillRecieptTable>(BillReciepts);
            }
            #endregion

            #region Add Bill Reciept Tax
            if (Obj.BillRecieptTaxInfoDc != null && Obj.BillRecieptTaxInfoDc.Count > 0)
            {
                foreach (var tax in Obj.BillRecieptTaxInfoDc)
                {
                    tax.BillRecieptId = BillReciepts.BillRecieptId;
                    int i_tax = InsertUpdate_BillRecieptTax(tax);
                }
            }
            #endregion

            #region Add Bill Reciept Item
            if (Obj.BillRecieptItemInfoDc != null && Obj.BillRecieptItemInfoDc.Count > 0)
            {
                foreach (var item in Obj.BillRecieptItemInfoDc)
                {
                    item.BillRecieptId = BillReciepts.BillRecieptId;
                    int i_item = InsertUpdate_BillRecieptItem(item);
                }
            }
            #endregion

            return BillReciepts.BillRecieptId;
        }

        public int Update_DynamicRecieptHTML(int BillRecieptId, string BillRecieptDynamicHtml)
        {
            #region Update Dynamic Reciept HTML
            BillRecieptTable BillReciepts = new BillRecieptTable();
            if (BillRecieptId > 0)
            {
                BillReciepts = objRep.Get<BillRecieptTable>(BillRecieptId);
                BillReciepts.BillRecieptDynamicHtml = BillRecieptDynamicHtml;
                objRep.Update<BillRecieptTable>(BillReciepts);
            }
            #endregion 

            return BillReciepts.BillRecieptId;
        }

        #endregion 

        #region Delete Bill Reciept
        public bool DeleteBillReciept(int BillRecieptId)
        {
            bool Ans = false;
            try
            {
                CommonManager CommonManager = new CommonManager();
                BillRecieptTable BillRecieptTable = new BillRecieptTable();
                if (BillRecieptId > 0)
                {
                    BillRecieptTable = objRep.Get<BillRecieptTable>(BillRecieptId);
                    CommonManager.DeleteBillRecieptTax_Item(BillRecieptTable.BillRecieptId);
                    Ans = objRep.Remove<BillRecieptTable>(BillRecieptTable);
                    string path = HttpContext.Current.Server.MapPath("~/RecieptPdf/bill_reciept_" + BillRecieptTable.BillRecieptId + ".pdf");
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    Ans = true;
                }
            }
            catch (Exception ex)
            {
                Ans = false;
            }
            return Ans;
        }
        #endregion

        #region Get Bill Reciept By Id
        public BillRecieptDc GetBillRecieptById(int BillRecieptId)
        {
            try
            {
                BillRecieptDc BillRecieptDc = objRep.FindAllBy<BillRecieptTable>(x => x.BillRecieptId == BillRecieptId).Select(x => new BillRecieptDc
                {
                    BillRecieptId = x.BillRecieptId,
                    UserId = x.UserId,
                    CategoryRecieptId = x.CategoryRecieptId,
                    CurrencyId = x.CurrencyId,
                    FontId = x.FontId,
                    FontStyle = x.FontMaster.FontStyle,
                    RecieptDate = x.RecieptDate,
                    Business = x.Business,
                    Address = x.Address,
                    CityState = x.CityState,
                    MobileNo = x.MobileNo,
                    RecieptLogo = x.RecieptLogo,
                    CreatedDate = x.CreatedDate,
                    RecieptNo = x.RecieptNo,
                    PaymentType = x.PaymentType,
                    Auth = x.Auth,
                    Trans = x.Trans,
                    ApprCode = x.ApprCode,
                    Mcc = x.Mcc,
                    BillRecieptDynamicHtml = x.BillRecieptDynamicHtml,
                    Name = x.RegistrationTable.Name,
                    Currency = x.CurrencyMaster.Currency,
                    CategoryName = x.CategoryRecieptMaster.CategoryMaster.CategoryName,
                    WebSiteName = x.WebSiteName,
                    DocNo = x.DocNo,
                    GSTNo = x.GSTNo,
                    BillNo = x.BillNo,
                    AtndId = x.AtndId,
                    FPId = x.FPId,
                    Pump = x.Pump,
                    NozlNo = x.NozlNo,
                    Fuel = x.Fuel,
                    Density = x.Density,
                    TinNo = x.TinNo,
                    TRXId = x.TRXId,
                    AccountNo = x.AccountNo,
                    PompaSalans = x.PompaSalans,
                    NomarNota = x.NomarNota,
                    JenisBBM = x.JenisBBM,
                    StationNo = x.StationNo,
                    HelplineNo = x.HelplineNo,
                    LandlineNo = x.LandlineNo,
                    CINNo = x.CINNo,
                    STNo = x.STNo,
                    OPNo = x.OPNo,
                    TENo = x.TENo,
                    TCNo = x.TCNo,
                    REFNo = x.REFNo,
                    Validation = x.Validation,
                    PaymentService = x.PaymentService,
                    PayAmount = x.PayAmount,
                    Lane = x.Lane,
                    Clerk = x.Clerk,
                    Cashier = x.Cashier,
                    MRCH = x.MRCH,
                    EPSSequence = x.EPSSequence,
                    NetworkId = x.NetworkId,
                    TerminalId = x.TerminalId,
                    SpaceNo = x.SpaceNo,
                    TicketNo = x.TicketNo,
                    SNNo = x.SNNo,
                    Lot_Location = x.Lot_Location,
                    PayedAt = x.PayedAt,
                    EntryDate = x.EntryDate,
                    ExitDate = x.ExitDate,
                    MachName = x.MachName,
                    VehicleNo = x.VehicleNo,
                    VehicleType = x.VehicleType,
                    RegNo = x.RegNo,
                    TRNNo = x.TRNNo,
                    CSHRNo = x.CSHRNo,
                    STRNo = x.STRNo,
                    HelpedBy = x.HelpedBy,
                    AID = x.AID,
                    Signature = x.Signature,
                    CVM = x.CVM,
                    StoreNo = x.StoreNo,
                    PharmacyNo = x.PharmacyNo,
                    OrderNo = x.OrderNo,
                    OrderDate = x.OrderDate,
                    DeliveryDate = x.DeliveryDate,
                    Host = x.Host,
                    FaxNo = x.FaxNo,
                    BillTo = x.BillTo,
                    ShipTo = x.ShipTo,
                    CustomerNo = x.CustomerNo,
                    CustomerPONo = x.CustomerPONo,
                    CardEntry = x.CardEntry,
                    TransType = x.TransType,
                    CheckNo = x.CheckNo,
                    CheckId = x.CheckId,
                    Tip = x.Tip,
                    TaxiDate = x.TaxiDate,
                    PickupTime = x.PickupTime,
                    DropTime = x.DropTime,
                    PickupAddress = x.PickupAddress,
                    DropAddress = x.DropAddress,
                    TaxiKM = x.TaxiKM,
                    CarName = x.CarName,
                    Miles = x.Miles,
                    TaxiWTTime = x.TaxiWTTime,
                    BaseFare = x.BaseFare,
                    DistanceFare = x.DistanceFare,
                    TimeFare = x.TimeFare,
                    SubCharges = x.SubCharges,
                    SafeRideFee = x.SafeRideFee,
                    TaxiCallNo = x.TaxiCallNo,
                    ProfileImage = x.ProfileImage,
                    LiftLineDiscountPrice = x.LiftLineDiscountPrice,
                    PromotionalPricing = x.PromotionalPricing,
                    RideBy = x.RideBy,
                    LicensePlate = x.LicensePlate,
                    TollPlaza = x.TollPlaza,
                    Operator = x.Operator,
                    Section = x.Section,
                    VehicleStanderedWeight = x.VehicleStanderedWeight,
                    VehicleActualWeight = x.VehicleActualWeight,
                    OverloadedVehicleFee = x.OverloadedVehicleFee,
                    Shift = x.Shift,
                    Journey = x.Journey,
                    TCId = x.TCId,
                    BarCode = x.BarCode,
                    CSH = x.CSH,
                    RelationShipNo = x.RelationShipNo,
                    BillDate = x.BillDate,
                    DueDate = x.DueDate,
                    LandMark = x.LandMark,
                    BillPeriod = x.BillPeriod,
                    BillPayDate = x.BillPayDate,
                    CreditLimit = x.CreditLimit,
                    SecurityDeposit = x.SecurityDeposit,
                    PreviousBalance = x.PreviousBalance,
                    Payments = x.Payments,
                    Adjustments = x.Adjustments,
                    LatePaymentFee = x.LatePaymentFee,
                    MonthlyRentals = x.MonthlyRentals,
                    UsageCharges = x.UsageCharges,
                    OneTimeCharges = x.OneTimeCharges,
                    Taxes = x.Taxes,
                    ServiceTaxRegNo = x.ServiceTaxRegNo,
                    FixedlineNo = x.FixedlineNo,
                    BroadbandId = x.BroadbandId,
                    AlternateMobileNo = x.AlternateMobileNo,
                    ShipToStateCode = x.ShipToStateCode,
                    BillGenerateBy = x.BillGenerateBy,
                    SoldBy = x.SoldBy,
                    SyNo = x.SyNo,
                    PANNo = x.PANNo,
                    InvoiceDetails = x.InvoiceDetails,
                    AadharNo = x.AadharNo,
                    MemberShip = x.MemberShip,
                    UsageP_ISD_IRCalling = x.UsageP_ISD_IRCalling,
                    UsageDataCharges = x.UsageDataCharges,
                    UsageSMSCharges = x.UsageSMSCharges,
                    UsageVASCharges = x.UsageVASCharges,
                    CurruntMonthDiscount = x.CurruntMonthDiscount,
                    BillDiscountWithTax = x.BillDiscountWithTax,
                    BillToEmailId = x.BillToEmailId,
                    Rating = x.Rating,
                    BillRecieptTaxInfoDc = x.BillRecieptTaxInfoes.Select(z => new BillRecieptTaxInfoDc
                    {
                        BillRecieptTaxId = z.BillRecieptTaxId,
                        BillRecieptId = z.BillRecieptId,
                        TaxId = z.TaxId,
                        Tax = z.TaxMaster.Tax,
                        TaxInPercentage = z.TaxInPercentage,
                    }).ToList(),
                    BillRecieptItemInfoDc = x.BillRecieptItemInfoes.Select(y => new BillRecieptItemInfoDc
                    {
                        BillRecieptItemId = y.BillRecieptItemId,
                        BillRecieptId = y.BillRecieptId,
                        ItemName = y.ItemName,
                        Quantity = y.Quantity,
                        Price = y.Price,
                        ItemNo = y.ItemNo,
                        Description = y.Description,
                        Type = y.Type,
                        Mfrs = y.Mfrs,
                        BatchNo = y.BatchNo,
                        Expiry = y.Expiry,
                        UOM = y.UOM,
                        CreatedDate = y.CreatedDate,
                    }).ToList(),
                    CategoryRecieptPDFDc = x.CategoryRecieptMaster != null ? (new CategoryRecieptPDFDc()
                    {
                        BillRecieptSampleImage = x.CategoryRecieptMaster.BillRecieptSampleImage,
                        IsRecieptLogo = x.CategoryRecieptMaster.IsRecieptLogo,
                        RecieptType = x.CategoryRecieptMaster.RecieptType,
                        ReceiptHight = x.CategoryRecieptMaster.ReceiptHight,
                        ReceiptWidth = x.CategoryRecieptMaster.ReceiptWidth,
                    }) : null,
                }).FirstOrDefault();

                return BillRecieptDc;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                throw ex;
            }
        }
        #endregion

        #region Search BillReciept
        public List<BillRecieptDc> SearchBillReciept(BillRecieptSearchDc CategorySearchDc)
        {
            var LoginPredicate = PredicateBuilder.New<BillRecieptTable>(true);

            List<BillRecieptDc> BillRecieptDcList;
            if (!string.IsNullOrEmpty(CategorySearchDc.BillRecieptId.ToString()) && CategorySearchDc.BillRecieptId.ToString() != "0")
            {
                LoginPredicate = LoginPredicate.And(p => p.BillRecieptId.ToString() == CategorySearchDc.BillRecieptId.ToString());
            }
            if (!string.IsNullOrEmpty(CategorySearchDc.UserId.ToString()) && CategorySearchDc.UserId.ToString() != "0")
            {
                LoginPredicate = LoginPredicate.And(p => p.UserId.ToString() == CategorySearchDc.UserId.ToString());
            }
            if (!string.IsNullOrEmpty(CategorySearchDc.CategoryId.ToString()) && CategorySearchDc.CategoryId.ToString() != "0")
            {
                LoginPredicate = LoginPredicate.And(p => p.CategoryRecieptMaster.CategoryId.ToString() == CategorySearchDc.CategoryId.ToString());
            }
            if (!string.IsNullOrEmpty(CategorySearchDc.RecieptNo) && CategorySearchDc.RecieptNo != "0")
            {
                LoginPredicate = LoginPredicate.And(p => p.RecieptNo == CategorySearchDc.RecieptNo);
            }
            if (!string.IsNullOrEmpty(CategorySearchDc.RecieptFromDate.ToString()) && CategorySearchDc.RecieptFromDate.ToString() != "0" && !string.IsNullOrEmpty(CategorySearchDc.RecieptToDate.ToString()) && CategorySearchDc.RecieptToDate.ToString() != "0")
            {
                DateTime rdate1 = Convert.ToDateTime(CategorySearchDc.RecieptFromDate.ToString("MM/dd/yyyy"));
                DateTime rdate2 = Convert.ToDateTime(CategorySearchDc.RecieptToDate.AddDays(+1).ToString("MM/dd/yyyy"));
                LoginPredicate = LoginPredicate.And(p => p.RecieptDate >= rdate1 && p.RecieptDate <= rdate2);
            }
            if (!string.IsNullOrEmpty(CategorySearchDc.Business) && CategorySearchDc.Business != "0")
            {
                LoginPredicate = LoginPredicate.And(p => p.Business.Contains(CategorySearchDc.Business));
            }
            BillRecieptDcList = objRep.FindAllBy1<BillRecieptTable>(LoginPredicate, x => x.OrderByDescending(y => y.BillRecieptId)).Select(x => new BillRecieptDc
            {
                BillRecieptId = x.BillRecieptId,
                UserId = x.UserId,
                CategoryRecieptId = x.CategoryRecieptId,
                CurrencyId = x.CurrencyId,
                FontId = x.FontId,
                FontStyle = x.FontMaster.FontStyle,
                RecieptDate = x.RecieptDate,
                Business = x.Business,
                Address = x.Address,
                CityState = x.CityState,
                MobileNo = x.MobileNo,
                RecieptLogo = x.RecieptLogo,
                CreatedDate = x.CreatedDate,
                RecieptNo = x.RecieptNo,
                PaymentType = x.PaymentType,
                Auth = x.Auth,
                Trans = x.Trans,
                ApprCode = x.ApprCode,
                Mcc = x.Mcc,
                BillRecieptDynamicHtml = x.BillRecieptDynamicHtml,
                Name = x.RegistrationTable.Name,
                CategoryName = x.CategoryRecieptMaster.CategoryMaster.CategoryName,
                Currency = x.CurrencyMaster.Currency,
                BillRecieptTaxInfoDc = x.BillRecieptTaxInfoes.Select(z => new BillRecieptTaxInfoDc
                {
                    BillRecieptTaxId = z.BillRecieptTaxId,
                    BillRecieptId = z.BillRecieptId,
                    TaxId = z.TaxId,
                    Tax = z.TaxMaster.Tax,
                    TaxInPercentage = z.TaxInPercentage,
                }).ToList(),
                BillRecieptItemInfoDc = x.BillRecieptItemInfoes.Select(y => new BillRecieptItemInfoDc
                {
                    BillRecieptItemId = y.BillRecieptItemId,
                    BillRecieptId = y.BillRecieptId,
                    ItemName = y.ItemName,
                    Quantity = y.Quantity,
                    Price = y.Price,
                    CreatedDate = y.CreatedDate,
                }).ToList(),
            }).ToList();

            return BillRecieptDcList;
        }
        #endregion

        #region Bill Reciept Tax

        #region Add Update Bill Reciept Tax
        public int InsertUpdate_BillRecieptTax(BillRecieptTaxInfoDc Obj)
        {
            BillRecieptTaxInfo BillReciepts = new BillRecieptTaxInfo();
            //if (Obj.BillRecieptTaxId > 0)
            //{
            //    BillReciepts = objRep.Get<BillRecieptTaxInfo>(Obj.BillRecieptTaxId);
            //    BillReciepts.BillRecieptId = Obj.BillRecieptId;
            //    BillReciepts.TaxId = Obj.TaxId;
            //    BillReciepts.TaxInPercentage = Obj.TaxInPercentage;
            //    objRep.Update<BillRecieptTaxInfo>(BillReciepts);
            //}
            //else
            //{
            BillReciepts.BillRecieptTaxId = 0;
            BillReciepts.BillRecieptId = Obj.BillRecieptId;
            BillReciepts.TaxId = Obj.TaxId;
            BillReciepts.TaxInPercentage = Obj.TaxInPercentage;
            objRep.Add<BillRecieptTaxInfo>(BillReciepts);
            //}
            return BillReciepts.BillRecieptTaxId;
        }

        #endregion 

        #region Delete Bill Reciept Tax
        public bool DeleteBillRecieptTax(int BillRecieptTaxId)
        {
            bool Ans = false;
            try
            {
                BillRecieptTaxInfo BillRecieptTaxInfo = new BillRecieptTaxInfo();
                if (BillRecieptTaxId > 0)
                {
                    BillRecieptTaxInfo = objRep.Get<BillRecieptTaxInfo>(BillRecieptTaxId);
                    Ans = objRep.Remove<BillRecieptTaxInfo>(BillRecieptTaxInfo);
                }
            }
            catch (Exception ex)
            {
                Ans = false;
            }
            return Ans;
        }
        #endregion

        #endregion

        #region Bill Reciept Items

        #region Add Update Bill Reciept Items
        public int InsertUpdate_BillRecieptItem(BillRecieptItemInfoDc Obj)
        {
            BillRecieptItemInfo BillReciepts = new BillRecieptItemInfo();
            //if (Obj.BillRecieptItemId > 0)
            //{
            //    BillReciepts = objRep.Get<BillRecieptItemInfo>(Obj.BillRecieptItemId);
            //    BillReciepts.BillRecieptId = Obj.BillRecieptId;
            //    BillReciepts.ItemName = Obj.ItemName;
            //    BillReciepts.Quantity = Obj.Quantity;
            //    BillReciepts.Price = Obj.Price;
            //    objRep.Update<BillRecieptItemInfo>(BillReciepts);
            //}
            //else
            //{
            BillReciepts.BillRecieptItemId = 0;
            BillReciepts.BillRecieptId = Obj.BillRecieptId;
            BillReciepts.ItemName = Obj.ItemName;
            BillReciepts.Quantity = Obj.Quantity;
            BillReciepts.Price = Obj.Price;
            BillReciepts.ItemNo = Obj.ItemNo;
            BillReciepts.Description = Obj.Description;
            BillReciepts.Type = Obj.Type;
            BillReciepts.Mfrs = Obj.Mfrs;
            BillReciepts.BatchNo = Obj.BatchNo;
            BillReciepts.Expiry = Obj.Expiry;
            BillReciepts.UOM = Obj.UOM;
            objRep.Add<BillRecieptItemInfo>(BillReciepts);
            //}
            return BillReciepts.BillRecieptItemId;
        }

        #endregion 

        #region Delete Bill Reciept Item
        public bool DeleteBillRecieptItem(int BillRecieptItemId)
        {
            bool Ans = false;
            try
            {
                BillRecieptItemInfo BillReciepts = new BillRecieptItemInfo();
                if (BillRecieptItemId > 0)
                {
                    BillReciepts = objRep.Get<BillRecieptItemInfo>(BillRecieptItemId);
                    Ans = objRep.Remove<BillRecieptItemInfo>(BillReciepts);
                }
            }
            catch (Exception ex)
            {
                Ans = false;
            }
            return Ans;
        }
        #endregion

        #endregion

    }
}
