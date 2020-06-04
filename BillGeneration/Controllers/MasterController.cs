using BillGeneration.BusinessLayer;
using BillGeneration.DataContract; 
using System;
using System.Collections.Generic; 
using System.Web.Http;
namespace BillGeneration.Controllers
{
    //[RoutePrefix("api/master")]
    public class MasterController : ApiController
    {
        [AllowAnonymous]

        #region Tax Master

        #region Add Update Tax
        [HttpPost]
        public long AddUpdateTaxMaster(TaxMasterDc TaxMasterDc)
        {
            MasterManager MasterManager = new MasterManager();
            try
            {
                TaxMasterDc.TaxId = MasterManager.InsertUpdate_Tax(TaxMasterDc);
            }
            catch (Exception ex)
            {
            }
            return TaxMasterDc.TaxId;
        }
        #endregion

        #region Get 

        #region Get Tax By Id
        [HttpGet]
        public TaxMasterDc GetTaxById(int TaxId)
        {
            TaxMasterDc TaxMasterDc = null;
            MasterManager MasterManager = new MasterManager();
            try
            {
                TaxMasterDc = MasterManager.GetTaxById(TaxId);
            }
            catch (Exception ex)
            {
            }
            return TaxMasterDc;
        }
        #endregion

        #region Get Tax
        [HttpGet]
        public List<TaxMasterDc> GetTax(string IsActive)
        {
            List<TaxMasterDc> TaxMasterDc = null;
            MasterManager MasterManager = new MasterManager();
            try
            {
                TaxMasterDc = MasterManager.SearchTax(IsActive);
            }
            catch (Exception ex)
            {
            }
            return TaxMasterDc;
        }
        #endregion

        #endregion

        #region Delete Tax By Id 
        [HttpGet]
        public bool DeleteTaxById(int TaxId)
        {
            bool Ans = false;
            try
            {
                MasterManager MasterManager = new MasterManager();
                Ans = MasterManager.DeleteTax(TaxId);
            }
            catch (Exception ex)
            {
            }
            return Ans;
        }
        #endregion

        #endregion

        #region Currency Master

        #region Add Update Currency
        [HttpPost]
        public long AddUpdateCurrencyMaster(CurrencyMasterDc CurrencyMasterDc)
        {
            MasterManager MasterManager = new MasterManager();
            try
            {
                CurrencyMasterDc.CurrencyId = MasterManager.InsertUpdate_Currency(CurrencyMasterDc);
            }
            catch (Exception ex)
            {
            }
            return CurrencyMasterDc.CurrencyId;
        }
        #endregion

        #region Get 

        #region Get Currency By Id
        [HttpGet]
        public CurrencyMasterDc GetCurrencyById(int CurrencyId)
        {
            CurrencyMasterDc CurrencyMasterDc = null;
            MasterManager MasterManager = new MasterManager();
            try
            {
                CurrencyMasterDc = MasterManager.GetCurrencyById(CurrencyId);
            }
            catch (Exception ex)
            {
            }
            return CurrencyMasterDc;
        }
        #endregion

        #region Get Currency
        [HttpGet]
        public List<CurrencyMasterDc> GetCurrency(string IsActive)
        {
            List<CurrencyMasterDc> CurrencyMasterDc = null;
            MasterManager MasterManager = new MasterManager();
            try
            {
                CurrencyMasterDc = MasterManager.SearchCurrency(IsActive);
            }
            catch (Exception ex)
            {
            }
            return CurrencyMasterDc;
        }
        #endregion

        #endregion

        #region Delete Currency By Id 
        [HttpGet]
        public bool DeleteCurrencyById(int CurrencyId)
        {
            bool Ans = false;
            try
            {
                MasterManager MasterManager = new MasterManager();
                Ans = MasterManager.DeleteCurrency(CurrencyId);
            }
            catch (Exception ex)
            {
            }
            return Ans;
        }
        #endregion

        #endregion

        #region Subscription Plan

        #region Add Update Subscription Plan
        [HttpPost]
        public long AddUpdateSubscriptionPlan(SubscriptionPlanDc SubscriptionPlanDc)
        {
            MasterManager MasterManager = new MasterManager();
            try
            {
                SubscriptionPlanDc.SubscriptionPlanId = MasterManager.InsertUpdate_SubscriptionPlan(SubscriptionPlanDc);
            }
            catch (Exception ex)
            {
            }
            return SubscriptionPlanDc.SubscriptionPlanId;
        }
        #endregion

        #region Get 

        #region Get Subscription Plan By Id
        [HttpGet]
        public SubscriptionPlanDc GetSubscriptionPlanById(int SubscriptionPlanId)
        {
            SubscriptionPlanDc SubscriptionPlanDc = null;
            MasterManager MasterManager = new MasterManager();
            try
            {
                SubscriptionPlanDc = MasterManager.GetSubscriptionPlanById(SubscriptionPlanId);
            }
            catch (Exception ex)
            {
            }
            return SubscriptionPlanDc;
        }
        #endregion

        #region Get Subscription Plan
        [HttpGet]
        public List<SubscriptionPlanDc> GetSubscriptionPlan(string IsActive)
        {
            List<SubscriptionPlanDc> SubscriptionPlanDc = null;
            MasterManager MasterManager = new MasterManager();
            try
            {
                SubscriptionPlanDc = MasterManager.SearchSubscriptionPlan(IsActive);
            }
            catch (Exception ex)
            {
            }
            return SubscriptionPlanDc;
        }
        #endregion

        #endregion

        #region Delete Subscription Plan By Id 
        [HttpGet]
        public bool DeleteSubscriptionPlanById(int SubscriptionPlanId)
        {
            bool Ans = false;
            try
            {
                MasterManager MasterManager = new MasterManager();
                Ans = MasterManager.DeleteSubscriptionPlan(SubscriptionPlanId);
            }
            catch (Exception ex)
            {
            }
            return Ans;
        }
        #endregion

        #endregion

        #region Get Font Dropdown
        [HttpGet]
        public List<FontDropdownDc> GetFontDropdown()
        {
            List<FontDropdownDc> FontDropdownDc = null;
            MasterManager MasterManager = new MasterManager();
            try
            {
                FontDropdownDc = MasterManager.GetFontDropdown();
            }
            catch (Exception ex)
            {
            }
            return FontDropdownDc;
        }
        #endregion

        #region Get Font By Id
        [HttpGet]
        public FontMasterDc GetFontById(int FontId)
        {
            FontMasterDc FontMasterDc = null;
            MasterManager MasterManager = new MasterManager();
            try
            {
                FontMasterDc = MasterManager.GetFontById(FontId);
            }
            catch (Exception ex)
            {
            }
            return FontMasterDc;
        }
        #endregion

    }
}
