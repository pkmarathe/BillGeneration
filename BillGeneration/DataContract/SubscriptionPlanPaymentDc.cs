using System;

namespace BillGeneration.DataContract
{
    public class SubscriptionPlanPaymentDc
    {
        public int SubscriptionPlanPayId { get; set; }
        public int UserId { get; set; }
        public int SubscriptionPlanId { get; set; }
        public string PaymentAmount { get; set; }
        public string TransactionId { get; set; }
        public string Status { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public long? ExpMonth { get; set; }
        public long? ExpYear { get; set; }
        public string Cvc { get; set; }
        public string Currency { get; set; }
        public string ChargeId { get; set; }
        public string Created { get; set; } 
        public string Country { get; set; }
        public Boolean? LiveMode { get; set; }
        public string PaymentMethod { get; set; }
        public string Brand { get; set; }
        public string ReceiptUrl { get; set; }
        public string ReceiptNumber { get; set; } 
        public string TrackingNumber { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? SubscriptionFromDate { get; set; }
        public DateTime? SubscriptionToDate { get; set; }
        public string Name { get; set; }
        public string SubscriptionPlan { get; set; }

    }

    public class SubscriptionPlanPaymentInputDc
    {
        public int SubscriptionPlanPayId { get; set; }
        public int UserId { get; set; }
        public int SubscriptionPlanId { get; set; }
        public string Cvc { get; set; }
        public long ExpMonth { get; set; }
        public long ExpYear { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Currency { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string TokenId { get; set; }
        public string PaymentAmount { get; set; }

    }
    public class SubscriptionPlanPaymentSearchDc
    {
        public int UserId { get; set; }
        public int SubscriptionPlanId { get; set; }
        public string Status { get; set; }
        public string Currency { get; set; } 
    }
    public class PaymentInputModel
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Phone { get; set; }
        public string Cvc { get; set; }
        public long ExpMonth { get; set; }
        public long ExpYear { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public long Amount { get; set; }
        public string Currency { get; set; }
        public string ReceiptEmail { get; set; }
        public string TokenId { get; set; }
    }

    public class PaymentCheckoutModel
    {
        public long? Amount { get; set; }
        public string Currency { get; set; } 
        public string ReturnUrl { get; set; } 
        public string CancelUrl { get; set; }
        public int SubscriptionPlanId { get; set; }
        public string CustomerEmail { get; set; }
    }

    public class PaymentResponseModel
    {
        public bool Result { get; set; }
        public string SessionId { get; set; }  
    }

    public class PaymentCurrencyDc
    {
        public int PaymentCurrencyId { get; set; }
        public string Currency { get; set; }
        public string Description { get; set; }
        public string PricePerDoller { get; set; }
        public string IsActive { get; set; }
    }
}