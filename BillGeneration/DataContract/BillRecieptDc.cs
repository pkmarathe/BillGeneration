using System;
using System.Collections.Generic;

namespace BillGeneration.DataContract
{
    public class BillRecieptDc
    {
        public int BillRecieptId { get; set; }
        public int UserId { get; set; }
        public int CategoryRecieptId { get; set; }
        public int CurrencyId { get; set; }
        public int FontId { get; set; }
        public string FontStyle { get; set; }
        public DateTime? RecieptDate { get; set; }
        public string Business { get; set; }
        public string Address { get; set; }
        public string CityState { get; set; }
        public string MobileNo { get; set; }
        public string RecieptLogo { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string BillRecieptDynamicHtml { get; set; }
        public string Currency { get; set; }
        public string RecieptNo { get; set; }
        public string PaymentType { get; set; }
        public string Auth { get; set; }
        public string Trans { get; set; }
        public string Mcc { get; set; }
        public string ApprCode { get; set; }

        #region Gas Reciept Fields
        public string WebSiteName { get; set; }
        public string DocNo { get; set; }
        public string GSTNo { get; set; }
        public string BillNo { get; set; }
        public string AtndId { get; set; }
        public string FPId { get; set; }
        public string Pump { get; set; }
        public string NozlNo { get; set; }
        public string Fuel { get; set; }
        public string Density { get; set; }
        public string TinNo { get; set; }
        public string TRXId { get; set; }
        public string AccountNo { get; set; }
        public string PompaSalans { get; set; }
        public string NomarNota { get; set; }
        public string JenisBBM { get; set; }
        public string StationNo { get; set; }
        #endregion

        #region Grocery Fields
        public string HelplineNo { get; set; }
        public string LandlineNo { get; set; }
        public string CINNo { get; set; }
        public string STNo { get; set; }
        public string OPNo { get; set; }
        public string TENo { get; set; }
        public string TCNo { get; set; }
        public string REFNo { get; set; }
        public string Validation { get; set; }
        public string PaymentService { get; set; }
        public decimal? PayAmount { get; set; }
        public string Lane { get; set; }
        public string Clerk { get; set; }
        public string Cashier { get; set; }
        public string MRCH { get; set; }
        public string EPSSequence { get; set; }
        public string NetworkId { get; set; }
        public string TerminalId { get; set; }
        #endregion

        #region Parking Fields
        public string SpaceNo { get; set; }
        public string TicketNo { get; set; }
        public string SNNo { get; set; }
        public string Lot_Location { get; set; }
        public string PayedAt { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? ExitDate { get; set; }
        public string MachName { get; set; }
        public string VehicleNo { get; set; }
        public string VehicleType { get; set; }
        #endregion

        #region Pharmacy Fields
        public string RegNo { get; set; }
        public string TRNNo { get; set; }
        public string CSHRNo { get; set; }
        public string STRNo { get; set; }
        public string HelpedBy { get; set; }
        public string AID { get; set; }
        public string Signature { get; set; }
        public string CVM { get; set; }
        public string StoreNo { get; set; }
        public string PharmacyNo { get; set; }
        #endregion

        #region Restaurant Fields
        public string OrderNo { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string Host { get; set; }
        public string FaxNo { get; set; }
        public string BillTo { get; set; }
        public string BillToEmailId { get; set; }
        public string ShipTo { get; set; }
        public string CustomerNo { get; set; }
        public string CustomerPONo { get; set; }
        public string CardEntry { get; set; }
        public string TransType { get; set; }
        public string CheckNo { get; set; }
        public string CheckId { get; set; }
        public string Tip { get; set; }
        #endregion

        #region Taxi Fields
        public DateTime? TaxiDate { get; set; }
        public DateTime? PickupTime { get; set; }
        public DateTime? DropTime { get; set; }
        public string PickupAddress { get; set; }
        public string DropAddress { get; set; }
        public string TaxiKM { get; set; }
        public string CarName { get; set; }
        public string Miles { get; set; }
        public DateTime? TaxiWTTime { get; set; }
        public decimal? BaseFare { get; set; }
        public decimal? DistanceFare { get; set; }
        public decimal? TimeFare { get; set; }
        public decimal? SubCharges { get; set; }
        public decimal? SafeRideFee { get; set; }
        public string TaxiCallNo { get; set; }
        public string ProfileImage { get; set; }
        public decimal? LiftLineDiscountPrice { get; set; }
        public decimal? PromotionalPricing { get; set; }
        public string RideBy { get; set; }
        public string LicensePlate { get; set; }
        public int? Rating { get; set; }
        
        #endregion

        #region Toll Fields
        public string TollPlaza { get; set; }
        public string Operator { get; set; }
        public string Section { get; set; }
        public string VehicleStanderedWeight { get; set; }
        public string VehicleActualWeight { get; set; }
        public decimal? OverloadedVehicleFee { get; set; }
        public string Shift { get; set; }
        public string Journey { get; set; }
        public string TCId { get; set; }
        public string BarCode { get; set; }
        public string CSH { get; set; }
        #endregion

        #region Phone & Internet Fields
        public string RelationShipNo { get; set; }
        public DateTime? BillDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string LandMark { get; set; }
        public string BillPeriod { get; set; }
        public DateTime? BillPayDate { get; set; }
        public decimal? CreditLimit { get; set; }
        public decimal? SecurityDeposit { get; set; }
        public decimal? PreviousBalance { get; set; }
        public decimal? Payments { get; set; }
        public decimal? Adjustments { get; set; }
        public decimal? LatePaymentFee { get; set; }
        public decimal? MonthlyRentals { get; set; }
        public decimal? UsageCharges { get; set; }
        public decimal? OneTimeCharges { get; set; }
        public decimal? Taxes { get; set; }
        public string ServiceTaxRegNo { get; set; }
        public string FixedlineNo { get; set; }
        public string BroadbandId { get; set; }
        public string AlternateMobileNo { get; set; }
        public string ShipToStateCode { get; set; }
        public string BillGenerateBy { get; set; }
        public string SoldBy { get; set; }
        public string SyNo { get; set; }
        public string PANNo { get; set; }
        public string InvoiceDetails { get; set; }
        public string AadharNo { get; set; }
        public string MemberShip { get; set; }
        public decimal? UsageP_ISD_IRCalling { get; set; }
        public decimal? UsageDataCharges { get; set; }
        public decimal? UsageSMSCharges { get; set; }
        public decimal? UsageVASCharges { get; set; }
        public decimal? CurruntMonthDiscount { get; set; }
        public decimal? BillDiscountWithTax { get; set; }
        #endregion
        public int SubscriptionPlanId { get; set; }
        public List<BillRecieptItemInfoDc> BillRecieptItemInfoDc { get; set; }
        public List<BillRecieptTaxInfoDc> BillRecieptTaxInfoDc { get; set; }
        public CategoryRecieptPDFDc CategoryRecieptPDFDc { get; set; }
    }
    public class BillRecieptSearchDc
    {
        public int BillRecieptId { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Business { get; set; }
        public string RecieptNo { get; set; }
        public DateTime RecieptFromDate { get; set; }
        public DateTime RecieptToDate { get; set; }
    }
    public class BillRecieptItemInfoDc
    {
        public int BillRecieptItemId { get; set; }
        public int BillRecieptId { get; set; }
        public string ItemNo { get; set; }
        public string ItemName { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Mfrs { get; set; }
        public string BatchNo { get; set; }
        public DateTime? Expiry { get; set; }
        public string UOM { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
    public class BillRecieptTaxInfoDc
    {
        public int BillRecieptTaxId { get; set; }
        public int BillRecieptId { get; set; }
        public int TaxId { get; set; }
        public decimal? TaxInPercentage { get; set; }
        public string Tax { get; set; }

    }
}