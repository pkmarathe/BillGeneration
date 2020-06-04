using System;
using Stripe;
using BillGeneration.DataContract;

namespace BillGeneration.BusinessLayer
{
    public class PaymentIntegrationManager
    {
        #region Stripe Payment Integration
        public SubscriptionPlanPaymentDc PaymentIntegration(PaymentInputModel PaymentInputModel, SubscriptionPlanPaymentDc SubscriptionPlanPaymentDc)
        {
            try
            {
                //test publishable key - pk_test_HFSzYRbru21PsWpmO7hWROLs009prnonfu
                //test secure key - sk_test_5dIzjTeUu8962UGvezkZWoKT00ayvmSf2R
                //live publishable key - pk_live_V6B0hXJyJzQ82puhEYu4ov9U00M7oGzJYm
                //live secure key - sk_live_Tt1v1Bt2yH0TMBPgjH74plqm00EvEAv6Uf 
                //StripeConfiguration.SetApiKey("pk_test_HFSzYRbru21PsWpmO7hWROLs009prnonfu");   
                //-----------------------------------------Start Generate Token--------------------------------------------------------              
                //StripeConfiguration.ApiKey = "sk_test_5dIzjTeUu8962UGvezkZWoKT00ayvmSf2R"; //Testing Key
                StripeConfiguration.ApiKey = "sk_live_Tt1v1Bt2yH0TMBPgjH74plqm00EvEAv6Uf"; //Live Key
                //CreditCardOptions CardDetails = new CreditCardOptions();
                //CardDetails.Cvc = PaymentInputModel.Cvc;
                //CardDetails.ExpMonth = PaymentInputModel.ExpMonth;
                //CardDetails.ExpYear = PaymentInputModel.ExpYear;
                //CardDetails.Name = PaymentInputModel.Name;
                //CardDetails.Number = PaymentInputModel.Number;
                //var myToken = new TokenCreateOptions();
                //myToken.Card = CardDetails;
                //var tokenService = new TokenService();
                //var stripeToken = tokenService.Create(myToken);
                //--------------------------------------------Start Charge Payment-----------------------------------------------------  
                ChargeShippingOptions crgspadd = new ChargeShippingOptions();
                crgspadd.Address = new AddressOptions() { State = PaymentInputModel.State, City = PaymentInputModel.City, Country = PaymentInputModel.Country, PostalCode = PaymentInputModel.PostalCode, Line1 = PaymentInputModel.Line1, Line2 = PaymentInputModel.Line2 };
                //crgspadd.Address = new AddressOptions() { State = "SA", City = "Texas ", Country = "US", PostalCode = "78216", Line1 = "AHD-7, Sukhliya", Line2 = "Sukhliya" };
                crgspadd.Name = PaymentInputModel.Name;
                //crgspadd.Phone = PaymentInputModel.Phone;                
                var chargroptions = new ChargeCreateOptions()
                {
                    Amount = PaymentInputModel.Amount,
                    Currency = PaymentInputModel.Currency,
                    Source = PaymentInputModel.TokenId, //stripeToken.Id,
                    ReceiptEmail = PaymentInputModel.ReceiptEmail,
                    Description = "Payment Continue",
                    Shipping = crgspadd,
                    Capture = true
                };
                var chargrservices = new ChargeService();
                var result = chargrservices.Create(chargroptions);
                //Response = result.StripeResponse.ToString();
                //--------------------------------------------Payment Log-----------------------------------------------------  
                SubscriptionPlanPaymentDc.Status = result.Status;
                SubscriptionPlanPaymentDc.TransactionId = result.BalanceTransactionId;
                SubscriptionPlanPaymentDc.ChargeId = result.Id;
                SubscriptionPlanPaymentDc.Created = result.Created.ToString();
                SubscriptionPlanPaymentDc.Country = result.PaymentMethodDetails.Card.Country;
                SubscriptionPlanPaymentDc.LiveMode = result.Livemode;
                SubscriptionPlanPaymentDc.PaymentMethod = result.PaymentMethodId;
                SubscriptionPlanPaymentDc.Brand = result.PaymentMethodDetails.Card.Brand;
                SubscriptionPlanPaymentDc.ReceiptUrl = result.ReceiptUrl;
                SubscriptionPlanPaymentDc.ReceiptNumber = result.ReceiptNumber;
                SubscriptionPlanPaymentDc.TrackingNumber = result.Shipping.TrackingNumber;
                //-------------------------------------------------------------------------------------------------            
            }
            catch (StripeException e)
            {
                SubscriptionPlanPaymentDc.Status = e.Message.ToString();
            }
            return SubscriptionPlanPaymentDc;
        }
        #endregion
    }
}
