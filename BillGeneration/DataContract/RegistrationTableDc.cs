using System;

namespace BillGeneration.DataContract
{
    public class RegistrationTableDc
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; }
        public string Designation { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string TermsOfUse { get; set; }
        public string Role { get; set; }
        public string ProfileImage { get; set; }
        public bool? IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsSubscription { get; set; }
        public int SubscriptionPlanId { get; set; }
        public string Cvc { get; set; }
        public long ExpMonth { get; set; }
        public long ExpYear { get; set; }
        public string CardName { get; set; }
        public string Number { get; set; }
        public string Currency { get; set; }
        public string PaymentAmount { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string TokenId { get; set; }
    }
    public class RegistrationSearchDc
    {
        public string Name { get; set; }
        public string Designation { get; set; }
        public string EmailId { get; set; }
        public string Role { get; set; }
        public string IsActive { get; set; }
    }
    public class RegistrationDropdownDc
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}