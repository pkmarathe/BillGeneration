using BillGeneration.DataContract;
using BillGeneration.DataLayer;
using BillGeneration.DataLayer.EF;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BillGeneration.BusinessLayer
{
    public class SubscriptionPlanPaymentManager
    {
        Repository objRep = new Repository();

        #region Add Update Subscription Plan Payment
        public int InsertUpdate_SubscriptionPlanPayment(SubscriptionPlanPaymentDc Obj)
        {
            SubscriptionPlanPayment SubscriptionPlanPayments = new SubscriptionPlanPayment();
            if (Obj.SubscriptionPlanPayId > 0)
            {
                SubscriptionPlanPayments = objRep.Get<SubscriptionPlanPayment>(Obj.SubscriptionPlanPayId);
                SubscriptionPlanPayments.SubscriptionPlanId = Obj.SubscriptionPlanId;
                SubscriptionPlanPayments.UserId = Obj.UserId;
                SubscriptionPlanPayments.PaymentAmount = Obj.PaymentAmount;
                SubscriptionPlanPayments.Cvc = Obj.Cvc;
                SubscriptionPlanPayments.ExpMonth = Obj.ExpMonth;
                SubscriptionPlanPayments.ExpYear = Obj.ExpYear;
                SubscriptionPlanPayments.CardNumber = Obj.CardNumber;
                SubscriptionPlanPayments.CardName = Obj.CardName;
                SubscriptionPlanPayments.Brand = Obj.Brand;
                SubscriptionPlanPayments.ChargeId = Obj.ChargeId;
                SubscriptionPlanPayments.Country = Obj.Country;
                SubscriptionPlanPayments.Created = Obj.Created;
                SubscriptionPlanPayments.Status = Obj.Status;
                SubscriptionPlanPayments.Currency = Obj.Currency;
                SubscriptionPlanPayments.LiveMode = Obj.LiveMode;
                SubscriptionPlanPayments.PaymentMethod = Obj.PaymentMethod;
                SubscriptionPlanPayments.ReceiptNumber = Obj.ReceiptNumber;
                SubscriptionPlanPayments.ReceiptUrl = Obj.ReceiptUrl;
                SubscriptionPlanPayments.TrackingNumber = Obj.TrackingNumber;
                SubscriptionPlanPayments.TransactionId = Obj.TransactionId;
                objRep.Update<SubscriptionPlanPayment>(SubscriptionPlanPayments);
            }
            else
            {
                if (Obj.SubscriptionPlanId > 0)
                {
                    SubscriptionPlanPayments.SubscriptionFromDate = DateTime.Now;
                    if (Obj.SubscriptionPlanId == 2)
                        SubscriptionPlanPayments.SubscriptionToDate = DateTime.Now.AddDays(+7);
                    else if (Obj.SubscriptionPlanId == 3)
                        SubscriptionPlanPayments.SubscriptionToDate = DateTime.Now.AddMonths(+1);
                    else if (Obj.SubscriptionPlanId == 4)
                        SubscriptionPlanPayments.SubscriptionToDate = DateTime.Now.AddYears(+1);
                    else
                        SubscriptionPlanPayments.SubscriptionToDate = DateTime.Now.AddDays(+7);
                }
                SubscriptionPlanPayments.SubscriptionPlanPayId = 0;
                SubscriptionPlanPayments.SubscriptionPlanId = Obj.SubscriptionPlanId;
                SubscriptionPlanPayments.UserId = Obj.UserId;
                SubscriptionPlanPayments.PaymentAmount = Obj.PaymentAmount;
                SubscriptionPlanPayments.Cvc = Obj.Cvc;
                SubscriptionPlanPayments.ExpMonth = Obj.ExpMonth;
                SubscriptionPlanPayments.ExpYear = Obj.ExpYear;
                SubscriptionPlanPayments.CardNumber = Obj.CardNumber;
                SubscriptionPlanPayments.CardName = Obj.CardName;
                SubscriptionPlanPayments.Brand = Obj.Brand;
                SubscriptionPlanPayments.ChargeId = Obj.ChargeId;
                SubscriptionPlanPayments.Country = Obj.Country;
                SubscriptionPlanPayments.Created = Obj.Created;
                SubscriptionPlanPayments.Status = Obj.Status;
                SubscriptionPlanPayments.Currency = Obj.Currency;
                SubscriptionPlanPayments.LiveMode = Obj.LiveMode;
                SubscriptionPlanPayments.PaymentMethod = Obj.PaymentMethod;
                SubscriptionPlanPayments.ReceiptNumber = Obj.ReceiptNumber;
                SubscriptionPlanPayments.ReceiptUrl = Obj.ReceiptUrl;
                SubscriptionPlanPayments.TrackingNumber = Obj.TrackingNumber;
                SubscriptionPlanPayments.TransactionId = Obj.TransactionId;
                SubscriptionPlanPayments.CreatedDate = DateTime.Now;
                objRep.Add<SubscriptionPlanPayment>(SubscriptionPlanPayments);
            }
            return SubscriptionPlanPayments.SubscriptionPlanPayId;
        }

        #endregion 

        #region Delete Subscription Plan Payment
        public bool DeleteSubscriptionPlanPayment(int SubscriptionPlanPayId)
        {
            bool Ans = false;
            try
            {
                SubscriptionPlanPayment SubscriptionPlanPayment = new SubscriptionPlanPayment();
                if (SubscriptionPlanPayId > 0)
                {
                    SubscriptionPlanPayment = objRep.Get<SubscriptionPlanPayment>(SubscriptionPlanPayId);
                    Ans = objRep.Remove<SubscriptionPlanPayment>(SubscriptionPlanPayment);
                }
            }
            catch (Exception ex)
            {
                Ans = false;
            }
            return Ans;
        }
        #endregion

        #region Get Subscription Plan Payment By Id
        public SubscriptionPlanPaymentDc GetSubscriptionPlanPaymentById(int SubscriptionPlanPayId)
        {
            try
            {
                SubscriptionPlanPaymentDc SubscriptionPlanPaymentDc = objRep.FindAllBy<SubscriptionPlanPayment>(x => x.SubscriptionPlanPayId == SubscriptionPlanPayId).Select(x => new SubscriptionPlanPaymentDc
                {
                    SubscriptionPlanPayId = x.SubscriptionPlanPayId,
                    SubscriptionPlanId = x.SubscriptionPlanId,
                    UserId = x.UserId,
                    TransactionId = x.TransactionId,
                    PaymentAmount = x.PaymentAmount,
                    Status = x.Status,
                    Cvc = x.Cvc,
                    ExpMonth = x.ExpMonth,
                    ExpYear = x.ExpYear,
                    CardNumber = x.CardNumber,
                    CardName = x.CardName,
                    Brand = x.Brand,
                    ChargeId = x.ChargeId,
                    Country = x.Country,
                    Created = x.Created,
                    Currency = x.Currency,
                    LiveMode = x.LiveMode,
                    PaymentMethod = x.PaymentMethod,
                    ReceiptNumber = x.ReceiptNumber,
                    ReceiptUrl = x.ReceiptUrl,
                    TrackingNumber = x.TrackingNumber,
                    CreatedDate = x.CreatedDate,
                    SubscriptionFromDate = x.SubscriptionFromDate,
                    SubscriptionToDate = x.SubscriptionToDate,
                }).FirstOrDefault();

                return SubscriptionPlanPaymentDc;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                throw ex;
            }
        }
        #endregion

        #region Search Subscription Plan Payment
        public List<SubscriptionPlanPaymentDc> SearchSubscriptionPlanPayment(SubscriptionPlanPaymentSearchDc CategorySearchDc)
        {
            var LoginPredicate = PredicateBuilder.New<SubscriptionPlanPayment>(true);

            List<SubscriptionPlanPaymentDc> SubscriptionPlanPaymentDcList;
            if (!string.IsNullOrEmpty(CategorySearchDc.Currency) && CategorySearchDc.Currency != "0")
            {
                LoginPredicate = LoginPredicate.And(p => p.Currency.Contains(CategorySearchDc.Currency));
            }
            if (!string.IsNullOrEmpty(CategorySearchDc.Status))
            {
                LoginPredicate = LoginPredicate.And(p => p.Status.Contains(CategorySearchDc.Status));
            }
            if (!string.IsNullOrEmpty(CategorySearchDc.SubscriptionPlanId.ToString()) && CategorySearchDc.SubscriptionPlanId.ToString() != "0")
            {
                LoginPredicate = LoginPredicate.And(p => p.SubscriptionPlanId == CategorySearchDc.SubscriptionPlanId);
            }
            if (!string.IsNullOrEmpty(CategorySearchDc.UserId.ToString()) && CategorySearchDc.UserId.ToString() != "0")
            {
                LoginPredicate = LoginPredicate.And(p => p.UserId == CategorySearchDc.UserId);
            }
            SubscriptionPlanPaymentDcList = objRep.FindAllBy1<SubscriptionPlanPayment>(LoginPredicate, x => x.OrderByDescending(y => y.SubscriptionPlanPayId)).Select(x => new SubscriptionPlanPaymentDc
            {
                SubscriptionPlanPayId = x.SubscriptionPlanPayId,
                SubscriptionPlanId = x.SubscriptionPlanId,
                UserId = x.UserId,
                TransactionId = x.TransactionId,
                PaymentAmount = x.PaymentAmount,
                Status = x.Status,
                Cvc = x.Cvc,
                ExpMonth = x.ExpMonth,
                ExpYear = x.ExpYear,
                CardNumber = x.CardNumber,
                CardName = x.CardName,
                Brand = x.Brand,
                ChargeId = x.ChargeId,
                Country = x.Country,
                Created = x.Created,
                Currency = x.Currency,
                LiveMode = x.LiveMode,
                PaymentMethod = x.PaymentMethod,
                ReceiptNumber = x.ReceiptNumber,
                ReceiptUrl = x.ReceiptUrl,
                TrackingNumber = x.TrackingNumber,
                CreatedDate = x.CreatedDate,
                Name = x.RegistrationTable.Name,
                SubscriptionPlan = x.SubscriptionPlan.SubscriptionPlan1,
                SubscriptionFromDate = x.SubscriptionFromDate,
                SubscriptionToDate = x.SubscriptionToDate,
            }).ToList();

            return SubscriptionPlanPaymentDcList;
        }
        #endregion
    }
}
