using BillGeneration.DataContract;
using BillGeneration.DataLayer;
using BillGeneration.DataLayer.EF;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BillGeneration.BusinessLayer
{
    public class MasterManager
    {
        Repository objRep = new Repository();

        #region Tax Master

        #region Add Update Tax
        public int InsertUpdate_Tax(TaxMasterDc Obj)
        {
            TaxMaster TaxMasters = new TaxMaster();
            if (Obj.TaxId > 0)
            {
                TaxMasters = objRep.Get<TaxMaster>(Obj.TaxId);
                TaxMasters.Tax = Obj.Tax;
                TaxMasters.IsActive = Obj.IsActive;
                objRep.Update<TaxMaster>(TaxMasters);
            }
            else
            {
                TaxMasters.TaxId = 0;
                TaxMasters.Tax = Obj.Tax;
                TaxMasters.IsActive = Obj.IsActive;
                objRep.Add<TaxMaster>(TaxMasters);
            }
            return TaxMasters.TaxId;
        }

        #endregion 

        #region DeleteTax
        public bool DeleteTax(int TaxId)
        {
            bool Ans = false;
            try
            {
                TaxMaster TaxMaster = new TaxMaster();
                if (TaxId > 0)
                {
                    TaxMaster = objRep.Get<TaxMaster>(TaxId);
                    TaxMaster.IsActive = false;
                    Ans = objRep.Update<TaxMaster>(TaxMaster);
                }
            }
            catch (Exception ex)
            {
                Ans = false;
            }
            return Ans;
        }
        #endregion

        #region Get Tax By Id
        public TaxMasterDc GetTaxById(int TaxId)
        {
            try
            {
                TaxMasterDc TaxMasterDc = objRep.FindAllBy<TaxMaster>(x => x.TaxId == TaxId).Select(x => new TaxMasterDc
                {
                    TaxId = x.TaxId,
                    Tax = x.Tax,
                    IsActive = x.IsActive,
                }).FirstOrDefault();

                return TaxMasterDc;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                throw ex;
            }
        }
        #endregion

        #region Search Tax
        public List<TaxMasterDc> SearchTax(string IsActive)
        {
            var LoginPredicate = PredicateBuilder.New<TaxMaster>(true);

            List<TaxMasterDc> TaxMasterDcList;
            if (IsActive != "All")
            {
                bool isact = bool.Parse(IsActive);
                TaxMasterDcList = objRep.FindAllBy1<TaxMaster>(y => y.IsActive == isact, x => x.OrderBy(y => y.TaxId)).Select(x => new TaxMasterDc { TaxId = x.TaxId, Tax = x.Tax, IsActive = x.IsActive, }).ToList();
            }
            else
            {
                TaxMasterDcList = objRep.FindAllBy1<TaxMaster>(y => y.TaxId > 0, x => x.OrderBy(y => y.TaxId)).Select(x => new TaxMasterDc { TaxId = x.TaxId, Tax = x.Tax, IsActive = x.IsActive, }).ToList();
            }
            return TaxMasterDcList;
        }
        #endregion

        #endregion

        #region Currency Master

        #region Add Update Currency
        public int InsertUpdate_Currency(CurrencyMasterDc Obj)
        {
            CurrencyMaster CurrencyMasters = new CurrencyMaster();
            if (Obj.CurrencyId > 0)
            {
                CurrencyMasters = objRep.Get<CurrencyMaster>(Obj.CurrencyId);
                CurrencyMasters.Currency = Obj.Currency;
                CurrencyMasters.IsActive = Obj.IsActive;
                objRep.Update<CurrencyMaster>(CurrencyMasters);
            }
            else
            {
                CurrencyMasters.CurrencyId = 0;
                CurrencyMasters.Currency = Obj.Currency;
                CurrencyMasters.IsActive = Obj.IsActive;
                objRep.Add<CurrencyMaster>(CurrencyMasters);
            }
            return CurrencyMasters.CurrencyId;
        }

        #endregion 

        #region DeleteCurrency
        public bool DeleteCurrency(int CurrencyId)
        {
            bool Ans = false;
            try
            {
                CurrencyMaster CurrencyMaster = new CurrencyMaster();
                if (CurrencyId > 0)
                {
                    CurrencyMaster = objRep.Get<CurrencyMaster>(CurrencyId);
                    CurrencyMaster.IsActive = false;
                    Ans = objRep.Update<CurrencyMaster>(CurrencyMaster);
                }
            }
            catch (Exception ex)
            {
                Ans = false;
            }
            return Ans;
        }
        #endregion

        #region Get Currency By Id
        public CurrencyMasterDc GetCurrencyById(int CurrencyId)
        {
            try
            {
                CurrencyMasterDc CurrencyMasterDc = objRep.FindAllBy<CurrencyMaster>(x => x.CurrencyId == CurrencyId).Select(x => new CurrencyMasterDc
                {
                    CurrencyId = x.CurrencyId,
                    Currency = x.Currency,
                    IsActive = x.IsActive,
                }).FirstOrDefault();

                return CurrencyMasterDc;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                throw ex;
            }
        }
        #endregion

        #region Search Currency
        public List<CurrencyMasterDc> SearchCurrency(string IsActive)
        {
            var LoginPredicate = PredicateBuilder.New<CurrencyMaster>(true);

            List<CurrencyMasterDc> CurrencyMasterDcList;
            if (IsActive != "All")
            {
                bool isact = bool.Parse(IsActive);
                CurrencyMasterDcList = objRep.FindAllBy1<CurrencyMaster>(y => y.IsActive == isact, x => x.OrderBy(y => y.CurrencyId)).Select(x => new CurrencyMasterDc { CurrencyId = x.CurrencyId, Currency = x.Currency, IsActive = x.IsActive, }).ToList();
            }
            else
            {
                CurrencyMasterDcList = objRep.FindAllBy1<CurrencyMaster>(y => y.CurrencyId > 0, x => x.OrderBy(y => y.CurrencyId)).Select(x => new CurrencyMasterDc { CurrencyId = x.CurrencyId, Currency = x.Currency, IsActive = x.IsActive, }).ToList();
            }
            return CurrencyMasterDcList;
        }
        #endregion

        #endregion

        #region Subscription Plan Master

        #region Add Update Subscription Plan
        public int InsertUpdate_SubscriptionPlan(SubscriptionPlanDc Obj)
        {
            SubscriptionPlan SubscriptionPlans = new SubscriptionPlan();
            if (Obj.SubscriptionPlanId > 0)
            {
                SubscriptionPlans = objRep.Get<SubscriptionPlan>(Obj.SubscriptionPlanId);
                SubscriptionPlans.SubscriptionPlan1 = Obj.SubscriptionPlan;
                SubscriptionPlans.SubscriptionPlanDesc = Obj.SubscriptionPlanDesc;
                SubscriptionPlans.PaymentType = Obj.PaymentType;
                SubscriptionPlans.Amount = Obj.Amount;
                SubscriptionPlans.Color = Obj.Color;
                SubscriptionPlans.IsActive = Obj.IsActive;
                SubscriptionPlans.UpdatedDate = DateTime.Now;
                objRep.Update<SubscriptionPlan>(SubscriptionPlans);
            }
            else
            {
                SubscriptionPlans.SubscriptionPlanId = 0;
                SubscriptionPlans.SubscriptionPlan1 = Obj.SubscriptionPlan;
                SubscriptionPlans.SubscriptionPlanDesc = Obj.SubscriptionPlanDesc;
                SubscriptionPlans.PaymentType = Obj.PaymentType;
                SubscriptionPlans.Amount = Obj.Amount;
                SubscriptionPlans.Color = Obj.Color;
                SubscriptionPlans.IsActive = Obj.IsActive;
                SubscriptionPlans.CreatedDate = DateTime.Now;
                objRep.Add<SubscriptionPlan>(SubscriptionPlans);
            }
            return SubscriptionPlans.SubscriptionPlanId;
        }

        #endregion 

        #region Delete Subscription Plan
        public bool DeleteSubscriptionPlan(int SubscriptionPlanId)
        {
            bool Ans = false;
            try
            {
                SubscriptionPlan SubscriptionPlan = new SubscriptionPlan();
                if (SubscriptionPlanId > 0)
                {
                    SubscriptionPlan = objRep.Get<SubscriptionPlan>(SubscriptionPlanId);
                    SubscriptionPlan.IsActive = false;
                    Ans = objRep.Update<SubscriptionPlan>(SubscriptionPlan);
                }
            }
            catch (Exception ex)
            {
                Ans = false;
            }
            return Ans;
        }
        #endregion

        #region Get Subscription Plan By Id
        public SubscriptionPlanDc GetSubscriptionPlanById(int SubscriptionPlanId)
        {
            try
            {
                SubscriptionPlanDc SubscriptionPlanDc = objRep.FindAllBy<SubscriptionPlan>(x => x.SubscriptionPlanId == SubscriptionPlanId).Select(x => new SubscriptionPlanDc
                {
                    SubscriptionPlanId = x.SubscriptionPlanId,
                    SubscriptionPlan = x.SubscriptionPlan1,
                    SubscriptionPlanDesc = x.SubscriptionPlanDesc,
                    PaymentType = x.PaymentType,
                    Amount = x.Amount,
                    Color = x.Color,
                    IsActive = x.IsActive,
                }).FirstOrDefault();

                return SubscriptionPlanDc;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                throw ex;
            }
        }
        #endregion

        #region Search Subscription Plan
        public List<SubscriptionPlanDc> SearchSubscriptionPlan(string IsActive)
        {
            var LoginPredicate = PredicateBuilder.New<SubscriptionPlan>(true);

            List<SubscriptionPlanDc> SubscriptionPlanDcList;
            if (IsActive != "All")
            {
                bool isact = bool.Parse(IsActive);
                SubscriptionPlanDcList = objRep.FindAllBy1<SubscriptionPlan>(y => y.IsActive == isact, x => x.OrderBy(y => y.SubscriptionPlanId)).Select(x => new SubscriptionPlanDc { SubscriptionPlanId = x.SubscriptionPlanId, SubscriptionPlan = x.SubscriptionPlan1, SubscriptionPlanDesc = x.SubscriptionPlanDesc, PaymentType = x.PaymentType, Amount = x.Amount, Color = x.Color, IsActive = x.IsActive, CreatedDate = x.CreatedDate, UpdatedDate = x.UpdatedDate }).ToList();
            }
            else
            {
                SubscriptionPlanDcList = objRep.FindAllBy1<SubscriptionPlan>(y => y.SubscriptionPlanId > 0, x => x.OrderBy(y => y.SubscriptionPlanId)).Select(x => new SubscriptionPlanDc { SubscriptionPlanId = x.SubscriptionPlanId, SubscriptionPlan = x.SubscriptionPlan1, SubscriptionPlanDesc = x.SubscriptionPlanDesc, PaymentType = x.PaymentType, Amount = x.Amount, Color = x.Color, IsActive = x.IsActive, CreatedDate = x.CreatedDate, UpdatedDate = x.UpdatedDate }).ToList();
            }
            return SubscriptionPlanDcList;
        }
        #endregion

        #endregion

        #region Get Font Master Dropdown 
        public List<FontDropdownDc> GetFontDropdown()
        {
            List<FontDropdownDc> FontMasterDc = new List<FontDropdownDc>(); 
            try
            {
                FontMasterDc = objRep.FindAllBy1<FontMaster>(z => z.IsActive == true).Select(x => new FontDropdownDc
                {
                    FontId = x.FontId,
                    FontStyle = x.FontStyle,
                }).ToList();
            }
            catch (Exception ex)
            {
            }
            return FontMasterDc;
        }
        #endregion 

        #region Get Font Master by id 
        public FontMasterDc GetFontById(int FontId)
        {
            FontMasterDc FontMasterDc = new FontMasterDc();
            try
            {
                FontMasterDc = objRep.FindAllBy<FontMaster>(z => z.IsActive == true && z.FontId == FontId).Select(x => new FontMasterDc
                {
                    FontId = x.FontId,
                    FontStyle = x.FontStyle,
                    FontLink = x.FontLink,
                    FontClass = x.FontClass,
                    IsActive = x.IsActive,
                }).FirstOrDefault();
            }
            catch { }

            return FontMasterDc;
        }         
        #endregion
    }
}
