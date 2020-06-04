using BillGeneration.BusinessLayer;
using BillGeneration.DataContract;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Http;
namespace BillGeneration.Controllers
{
    //[RoutePrefix("api/subscriptionplanpay")]
    public class SubscriptionPlanPayController : ApiController
    {
        [AllowAnonymous]

        #region Add Update Subscription Plan Payment
        [HttpPost]
        public string AddUpdateSubscriptionPlanPay(SubscriptionPlanPaymentInputDc SubscriptionPlanPaymentInputDc)
        {
            //{"SubscriptionPlanPayId":0,"UserId":3,"SubscriptionPlanId":4,"Name":"Rahul Patel","Number":"4000056655665556","ExpMonth":11,"ExpYear":24,"Cvc":232,"Currency":"inr"}
            string Response = "";
            SubscriptionPlanPaymentManager SubscriptionPlanPaymentManager = new SubscriptionPlanPaymentManager();
            //PaymentIntegrationManager PaymentIntegrationManager = new PaymentIntegrationManager();
            //CommonManager CommonManager = new CommonManager();
            SubscriptionPlanPaymentDc SubscriptionPlanPaymentDc = new SubscriptionPlanPaymentDc();
            try
            {
                SubscriptionPlanPaymentDc.SubscriptionPlanPayId = SubscriptionPlanPaymentInputDc.SubscriptionPlanPayId;
                SubscriptionPlanPaymentDc.SubscriptionPlanId = SubscriptionPlanPaymentInputDc.SubscriptionPlanId;
                SubscriptionPlanPaymentDc.UserId = SubscriptionPlanPaymentInputDc.UserId;
                SubscriptionPlanPaymentDc.TransactionId = SubscriptionPlanPaymentInputDc.TokenId;
                SubscriptionPlanPaymentDc.PaymentAmount = SubscriptionPlanPaymentInputDc.PaymentAmount;
                //#region Payment Integration
                if (SubscriptionPlanPaymentInputDc.SubscriptionPlanId > 1)
                {
                    //    DataTable dt = CommonManager.GetUserPaymentDetailByUPSId(SubscriptionPlanPaymentInputDc.UserId, SubscriptionPlanPaymentInputDc.SubscriptionPlanId);
                    //    if (dt != null && dt.Rows.Count > 0)
                    //    {
                    //PaymentInputModel PaymentInputModel = new PaymentInputModel();
                    //PaymentInputModel.Amount = long.Parse(Math.Round(decimal.Parse(dt.Rows[0]["Amount"].ToString()) * 100, 0).ToString());
                    //DataTable dtpriceperdoller = CommonManager.GetPaymentCurrency(SubscriptionPlanPaymentInputDc.Currency);
                    //if (dtpriceperdoller != null && dtpriceperdoller.Rows.Count > 0)
                    //{
                    //    PaymentInputModel.Amount = PaymentInputModel.Amount * long.Parse(dtpriceperdoller.Rows[0]["PricePerDoller"].ToString());
                    //}
                    //PaymentInputModel.Currency = (!string.IsNullOrEmpty(SubscriptionPlanPaymentInputDc.Currency) && SubscriptionPlanPaymentInputDc.Currency != "undefined") ? SubscriptionPlanPaymentInputDc.Currency : "usd";
                    //PaymentInputModel.Phone = dt.Rows[0]["MobileNo"].ToString();
                    //PaymentInputModel.ReceiptEmail = dt.Rows[0]["EmailId"].ToString();
                    //PaymentInputModel.Line1 = SubscriptionPlanPaymentInputDc.Address;
                    //PaymentInputModel.Cvc = SubscriptionPlanPaymentInputDc.Cvc;
                    //PaymentInputModel.ExpMonth = SubscriptionPlanPaymentInputDc.ExpMonth;
                    //PaymentInputModel.ExpYear = SubscriptionPlanPaymentInputDc.ExpYear;
                    //PaymentInputModel.Name = SubscriptionPlanPaymentInputDc.Name;
                    //PaymentInputModel.Number = SubscriptionPlanPaymentInputDc.Number;
                    //PaymentInputModel.Country = SubscriptionPlanPaymentInputDc.Country;
                    //PaymentInputModel.State = SubscriptionPlanPaymentInputDc.State;
                    //PaymentInputModel.City = SubscriptionPlanPaymentInputDc.City;
                    //PaymentInputModel.PostalCode = SubscriptionPlanPaymentInputDc.PostalCode;
                    //PaymentInputModel.TokenId = SubscriptionPlanPaymentInputDc.TokenId;

                    //SubscriptionPlanPaymentDc = PaymentIntegrationManager.PaymentIntegration(PaymentInputModel, SubscriptionPlanPaymentDc);
                    //if (SubscriptionPlanPaymentDc.Status == "succeeded")
                    //{
                    //SubscriptionPlanPaymentDc.PaymentAmount = dt.Rows[0]["Amount"].ToString();
                    SubscriptionPlanPaymentDc.Currency = (!string.IsNullOrEmpty(SubscriptionPlanPaymentInputDc.Currency) && SubscriptionPlanPaymentInputDc.Currency != "undefined") ? SubscriptionPlanPaymentInputDc.Currency : "usd";
                    SubscriptionPlanPaymentDc.Status = "succeeded";
                    SubscriptionPlanPaymentDc.PaymentMethod = "Card";
                    //SubscriptionPlanPaymentDc.Cvc = SubscriptionPlanPaymentInputDc.Cvc;
                    //SubscriptionPlanPaymentDc.ExpMonth = SubscriptionPlanPaymentInputDc.ExpMonth;
                    //SubscriptionPlanPaymentDc.ExpYear = SubscriptionPlanPaymentInputDc.ExpYear;
                    //SubscriptionPlanPaymentDc.CardName = SubscriptionPlanPaymentInputDc.Name;
                    //SubscriptionPlanPaymentDc.CardNumber = SubscriptionPlanPaymentInputDc.Number;
                    SubscriptionPlanPaymentDc.SubscriptionPlanPayId = SubscriptionPlanPaymentManager.InsertUpdate_SubscriptionPlanPayment(SubscriptionPlanPaymentDc);
                    //}
                    //        Response = SubscriptionPlanPaymentDc.Status;
                    //    }
                    Response = "succeeded";
                }
                else
                {
                    SubscriptionPlanPaymentDc.SubscriptionPlanPayId = SubscriptionPlanPaymentManager.InsertUpdate_SubscriptionPlanPayment(SubscriptionPlanPaymentDc);
                    Response = "Added";
                }
                //#endregion
            }
            catch (Exception ex)
            {
                Response = ex.Message.ToString();
            }
            return Response;
        }
        #endregion

        #region Get

        #region Search SubscriptionPlanPay
        [HttpPost]
        public List<SubscriptionPlanPaymentDc> SearchSubscriptionPlanPay(SubscriptionPlanPaymentSearchDc SubscriptionPlanPaySearchDc)
        {
            List<SubscriptionPlanPaymentDc> SubscriptionPlanPaymentDc = null;
            SubscriptionPlanPaymentManager SubscriptionPlanPaymentManager = new SubscriptionPlanPaymentManager();
            try
            {
                SubscriptionPlanPaymentDc = SubscriptionPlanPaymentManager.SearchSubscriptionPlanPayment(SubscriptionPlanPaySearchDc);
            }
            catch (Exception ex)
            {
            }
            return SubscriptionPlanPaymentDc;
        }
        #endregion 

        #region Get SubscriptionPlanPay By Id
        [HttpGet]
        public SubscriptionPlanPaymentDc GetSubscriptionPlanPayById(int SubscriptionPlanPayId)
        {
            SubscriptionPlanPaymentDc SubscriptionPlanPaymentDc = null;
            SubscriptionPlanPaymentManager SubscriptionPlanPaymentManager = new SubscriptionPlanPaymentManager();
            try
            {
                SubscriptionPlanPaymentDc = SubscriptionPlanPaymentManager.GetSubscriptionPlanPaymentById(SubscriptionPlanPayId);
            }
            catch (Exception ex)
            {
            }
            return SubscriptionPlanPaymentDc;
        }
        #endregion 

        #region Get Payment Currency
        [HttpGet]
        public List<PaymentCurrencyDc> GetPaymentCurrency(string Currency)
        {
            List<PaymentCurrencyDc> PaymentCurrencylistDc = new List<PaymentCurrencyDc>();
            CommonManager CommonManager = new CommonManager();
            try
            {
                DataTable dt = CommonManager.GetPaymentCurrency(Currency);
                if (dt != null && dt.Rows.Count > 0)
                    foreach (DataRow dr in dt.Rows)
                    {
                        PaymentCurrencyDc PaymentCurrencyDc = new PaymentCurrencyDc();
                        PaymentCurrencyDc.PaymentCurrencyId = int.Parse(dr["PaymentCurrencyId"].ToString());
                        PaymentCurrencyDc.Currency = dr["Currency"].ToString();
                        PaymentCurrencyDc.Description = dr["Description"].ToString();
                        PaymentCurrencyDc.PricePerDoller = dr["PricePerDoller"].ToString();
                        PaymentCurrencyDc.IsActive = dr["IsActive"].ToString();
                        PaymentCurrencylistDc.Add(PaymentCurrencyDc);
                    }
            }
            catch (Exception ex)
            {
            }
            return PaymentCurrencylistDc;
        }
        #endregion

        #region Get Session Id
        [HttpPost]
        public PaymentResponseModel GetSessionId(PaymentCheckoutModel PaymentCheckoutModel)
        {
            PaymentResponseModel objPaymentResponseModel = new PaymentResponseModel();
            try
            {
                //StripeConfiguration.ApiKey = "sk_test_5dIzjTeUu8962UGvezkZWoKT00ayvmSf2R"; //Testing Key
                StripeConfiguration.ApiKey = "sk_live_Tt1v1Bt2yH0TMBPgjH74plqm00EvEAv6Uf"; //Live Key 
                if (PaymentCheckoutModel.SubscriptionPlanId == 2)
                {
                    var options = new SessionCreateOptions
                    {
                        PaymentMethodTypes = new List<string> { "card", },
                        LineItems = new List<SessionLineItemOptions> { new SessionLineItemOptions
                        {
                            Name = "Receipt",
                            Description = "Bills99 Receipt",
                            Amount = PaymentCheckoutModel.Amount,
                            Currency = PaymentCheckoutModel.Currency,
                            Quantity = 1,
                        },},

                        SuccessUrl = PaymentCheckoutModel.ReturnUrl + "session_id={CHECKOUT_SESSION_ID}",
                        CancelUrl = PaymentCheckoutModel.CancelUrl,
                        CustomerEmail = PaymentCheckoutModel.CustomerEmail
                    };

                    var service = new SessionService();
                    Session session = service.Create(options);
                    objPaymentResponseModel.SessionId = session.Id;
                    objPaymentResponseModel.Result = true; 
                }
                else if (PaymentCheckoutModel.SubscriptionPlanId == 3 || PaymentCheckoutModel.SubscriptionPlanId == 4)
                {
                    CommonManager com = new CommonManager();
                    DataTable dtcom = com.GetStripeCurrencyPlan(PaymentCheckoutModel.Currency, PaymentCheckoutModel.SubscriptionPlanId.ToString());
                    if (dtcom != null && dtcom.Rows.Count > 0)
                    {
                        var options = new SessionCreateOptions
                        {
                            PaymentMethodTypes = new List<string> { "card", },
                            SubscriptionData = new SessionSubscriptionDataOptions
                            {
                                Items = new List<SessionSubscriptionDataItemOptions> { new SessionSubscriptionDataItemOptions { Plan = dtcom.Rows[0]["PlanId"].ToString(), }, },
                            },
                            SuccessUrl = PaymentCheckoutModel.ReturnUrl + "session_id={CHECKOUT_SESSION_ID}",
                            CancelUrl = PaymentCheckoutModel.CancelUrl,
                            CustomerEmail = PaymentCheckoutModel.CustomerEmail
                        };

                        var service = new SessionService();
                        Session session = service.Create(options);
                        objPaymentResponseModel.SessionId = session.Id;
                        objPaymentResponseModel.Result = true; 
                    }
                } 
            }
            catch (Exception ex)
            {
                objPaymentResponseModel.SessionId = ex.Message.ToString();
                objPaymentResponseModel.Result = false; 
            }
            return objPaymentResponseModel;
        }
        #endregion 

        #endregion

        #region Delete SubscriptionPlanPay By Id 
        [HttpGet]
        public bool DeleteSubscriptionPlanPayById(int SubscriptionPlanPayId)
        {
            bool Ans = false;
            try
            {
                SubscriptionPlanPaymentManager SubscriptionPlanPaymentManager = new SubscriptionPlanPaymentManager();
                Ans = SubscriptionPlanPaymentManager.DeleteSubscriptionPlanPayment(SubscriptionPlanPayId);
            }
            catch (Exception ex)
            {
            }
            return Ans;
        }
        #endregion 

    }
}
