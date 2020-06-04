//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BillGeneration.DataLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class SubscriptionPlanPayment
    {
        public int SubscriptionPlanPayId { get; set; }
        public int UserId { get; set; }
        public int SubscriptionPlanId { get; set; }
        public string PaymentAmount { get; set; }
        public string TransactionId { get; set; }
        public string Status { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public Nullable<long> ExpMonth { get; set; }
        public Nullable<long> ExpYear { get; set; }
        public string Cvc { get; set; }
        public string Currency { get; set; }
        public string ChargeId { get; set; }
        public string Created { get; set; }
        public string Country { get; set; }
        public Nullable<bool> LiveMode { get; set; }
        public string PaymentMethod { get; set; }
        public string Brand { get; set; }
        public string ReceiptUrl { get; set; }
        public string ReceiptNumber { get; set; }
        public string TrackingNumber { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> SubscriptionFromDate { get; set; }
        public Nullable<System.DateTime> SubscriptionToDate { get; set; }
    
        public virtual RegistrationTable RegistrationTable { get; set; }
        public virtual SubscriptionPlan SubscriptionPlan { get; set; }
    }
}
