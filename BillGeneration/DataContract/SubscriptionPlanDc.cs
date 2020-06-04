using System;

namespace BillGeneration.DataContract
{
    public class SubscriptionPlanDc
    {
        public int SubscriptionPlanId { get; set; }
        public string SubscriptionPlan { get; set; }
        public string SubscriptionPlanDesc { get; set; }        
        public string PaymentType { get; set; }
        public string Amount { get; set; }
        public string Color { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}