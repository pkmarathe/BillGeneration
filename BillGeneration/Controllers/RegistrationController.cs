using BillGeneration.BusinessLayer;
using BillGeneration.DataContract;
using BillGeneration.sendmail;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Web.Http;
namespace BillGeneration.Controllers
{
    //[RoutePrefix("api/registration")]
    public class RegistrationController : ApiController
    {
        [AllowAnonymous]

        #region Add Update Registration
        [HttpPost]
        public long AddUpdateRegistration()
        {
            long Response = 0;
            RegistrationManager RegistrationManager = new RegistrationManager();
            RegistrationTableDc RegistrationTableDc = null;
            HttpResponseMessage response = new HttpResponseMessage();
            var httpRequest = HttpContext.Current.Request;
            try
            {
                string requestString = httpRequest.Headers["RequestModel"];
                RegistrationTableDc = JsonConvert.DeserializeObject<RegistrationTableDc>(requestString);
                int uid = RegistrationTableDc.UserId;
                if (httpRequest.Files.Count > 0)
                {
                    HttpFileCollection files = httpRequest.Files;
                    foreach (string key in files)
                    {
                        HttpPostedFile file = files[key];
                        string postedFile = file.FileName;
                        RegistrationTableDc.ProfileImage = "ProfileImage/" + DateTime.Now.ToString("hhmmss") + postedFile.Replace(" ", "");
                        var filePath = HttpContext.Current.Server.MapPath("~/" + RegistrationTableDc.ProfileImage);
                        if (File.Exists(filePath))
                        {
                            File.Delete(filePath);
                        }
                        file.SaveAs(filePath);
                    }
                }
                else
                {
                    if (RegistrationTableDc.UserId > 0)
                        RegistrationTableDc.ProfileImage = RegistrationManager.GetRegistrationById(RegistrationTableDc.UserId).ProfileImage;
                    else
                        RegistrationTableDc.ProfileImage = "ProfileImage/no-image.png";
                }
                RegistrationTableDc.UserId = RegistrationManager.InsertUpdate_Registration(RegistrationTableDc);
                if (RegistrationTableDc.UserId > 0)
                {
                    Response = 1;
                }
                else if (RegistrationTableDc.UserId == -1)
                {
                    Response = 2;
                }
                else
                {
                    Response = 3;
                }
            }
            catch (Exception ex)
            {
                Response = 3;
            }
            return Response;
        }
        #endregion

        #region Add Registration
        [HttpPost]
        public string AddRegistration(RegistrationTableDc RegistrationTableDc)
        {
            string Response = "";
            RegistrationManager RegistrationManager = new RegistrationManager();
            SubscriptionPlanPaymentManager spPaymentManager = new SubscriptionPlanPaymentManager();
            SubscriptionPlanPaymentDc spPaymentDc = new SubscriptionPlanPaymentDc();
            //CommonManager CommonManager = new CommonManager();
            try
            {
                RegistrationTableDc.ProfileImage = "ProfileImage/no-image.png";
                RegistrationTableDc.UserId = 0;
                spPaymentDc.PaymentAmount = RegistrationTableDc.PaymentAmount;
                spPaymentDc.Currency = (!string.IsNullOrEmpty(RegistrationTableDc.Currency) && RegistrationTableDc.Currency != "undefined") ? RegistrationTableDc.Currency : "usd";
                spPaymentDc.TransactionId = RegistrationTableDc.TokenId;
                spPaymentDc.SubscriptionPlanPayId = 0;
                spPaymentDc.SubscriptionPlanId = RegistrationTableDc.SubscriptionPlanId;
                spPaymentDc.PaymentMethod = "Card";

                #region Payment Integration
                if (RegistrationTableDc.SubscriptionPlanId > 1)
                {
                    //PaymentInputModel PaymentInputModel = new PaymentInputModel();
                    //PaymentIntegrationManager PaymentIntegrationManager = new PaymentIntegrationManager();
                    //PaymentInputModel.Amount = long.Parse(Math.Round(decimal.Parse(RegistrationTableDc.PaymentAmount) * 100, 0).ToString());
                    //DataTable dtpriceperdoller = CommonManager.GetPaymentCurrency(RegistrationTableDc.Currency);
                    //if (dtpriceperdoller != null && dtpriceperdoller.Rows.Count > 0)
                    //{
                    //    PaymentInputModel.Amount = PaymentInputModel.Amount * long.Parse(dtpriceperdoller.Rows[0]["PricePerDoller"].ToString());
                    //}
                    //PaymentInputModel.Currency = (!string.IsNullOrEmpty(RegistrationTableDc.Currency) && RegistrationTableDc.Currency != "undefined") ? RegistrationTableDc.Currency : "usd";
                    //PaymentInputModel.Phone = RegistrationTableDc.MobileNo;
                    //PaymentInputModel.ReceiptEmail = RegistrationTableDc.EmailId;
                    //PaymentInputModel.Line1 = RegistrationTableDc.Address;
                    //PaymentInputModel.Cvc = RegistrationTableDc.Cvc;
                    //PaymentInputModel.ExpMonth = RegistrationTableDc.ExpMonth;
                    //PaymentInputModel.ExpYear = RegistrationTableDc.ExpYear;
                    //PaymentInputModel.Name = RegistrationTableDc.CardName;
                    //PaymentInputModel.Number = RegistrationTableDc.Number;
                    //PaymentInputModel.Country = RegistrationTableDc.Country;
                    //PaymentInputModel.State = RegistrationTableDc.State;
                    //PaymentInputModel.City = RegistrationTableDc.City;
                    //PaymentInputModel.PostalCode = RegistrationTableDc.PostalCode;
                    //PaymentInputModel.TokenId = RegistrationTableDc.TokenId;
                    //spPaymentDc.SubscriptionPlanId = RegistrationTableDc.SubscriptionPlanId;
                    //spPaymentDc.SubscriptionPlanPayId = 0;
                    //spPaymentDc = PaymentIntegrationManager.PaymentIntegration(PaymentInputModel, spPaymentDc);
                    //if (spPaymentDc.Status == "succeeded")
                    //{
                    //    spPaymentDc.Cvc = RegistrationTableDc.Cvc;
                    //    spPaymentDc.ExpMonth = RegistrationTableDc.ExpMonth;
                    //    spPaymentDc.ExpYear = RegistrationTableDc.ExpYear;
                    //    spPaymentDc.CardName = RegistrationTableDc.CardName;
                    //    spPaymentDc.CardNumber = RegistrationTableDc.Number;
                    RegistrationTableDc.UserId = RegistrationManager.InsertUpdate_Registration(RegistrationTableDc);
                    if (RegistrationTableDc.UserId > 0)
                    {
                        spPaymentDc.UserId = RegistrationTableDc.UserId;
                        spPaymentDc.Status = "succeeded";
                        spPaymentDc.SubscriptionPlanPayId = spPaymentManager.InsertUpdate_SubscriptionPlanPayment(spPaymentDc);
                    }
                    //}
                    Response = "succeeded";
                }
                else
                {
                    RegistrationTableDc.UserId = RegistrationManager.InsertUpdate_Registration(RegistrationTableDc);
                    if (RegistrationTableDc.UserId > 0)
                    {
                        spPaymentDc.UserId = RegistrationTableDc.UserId;
                        spPaymentDc.SubscriptionPlanPayId = spPaymentManager.InsertUpdate_SubscriptionPlanPayment(spPaymentDc);
                        Response = "Added";
                    }
                    else if (RegistrationTableDc.UserId == -1)
                    {
                        Response = "Exist";
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Response = ex.Message.ToString();
            }
            return Response;
        }
        #endregion

        #region Get

        #region Search Registration
        [HttpPost]
        public List<RegistrationTableDc> SearchRegistration(RegistrationSearchDc RegistrationSearchDc)
        {
            List<RegistrationTableDc> RegistrationTableDc = null;
            RegistrationManager RegistrationManager = new RegistrationManager();
            try
            {
                RegistrationTableDc = RegistrationManager.SearchRegistration(RegistrationSearchDc);
            }
            catch (Exception ex)
            {
            }
            return RegistrationTableDc;
        }
        #endregion

        #region Get Registration Dropdown
        [HttpGet]
        public List<RegistrationDropdownDc> GetRegistrationDropdown(string Role)
        {
            List<RegistrationDropdownDc> RegistrationTableDc = null;
            RegistrationManager RegistrationManager = new RegistrationManager();
            try
            {
                RegistrationTableDc = RegistrationManager.GetRegistrationDropdown(Role);
            }
            catch (Exception ex)
            {
            }
            return RegistrationTableDc;
        }
        #endregion

        #region Get Registration By Id
        [HttpGet]
        public RegistrationTableDc GetRegistrationById(int UserId)
        {
            RegistrationTableDc RegistrationTableDc = null;
            RegistrationManager RegistrationManager = new RegistrationManager();
            try
            {
                RegistrationTableDc = RegistrationManager.GetRegistrationById(UserId);
            }
            catch (Exception ex)
            {
            }
            return RegistrationTableDc;
        }
        #endregion

        #region Get Registration By Credentials
        [HttpGet]
        public RegistrationTableDc GetRegistrationByCredential(string emailid, string password)
        {
            RegistrationTableDc RegistrationTableDc = null;
            RegistrationManager RegistrationManager = new RegistrationManager();
            try
            {
                RegistrationTableDc = RegistrationManager.GetRegistrationByusernamepassword(emailid, password);
            }
            catch (Exception ex)
            {
            }
            return RegistrationTableDc;
        }
        #endregion

        #endregion

        #region Export to Excel  
        [HttpPost]
        public string ExportToExcel(FileExportDc FileExportDc)
        {
            try
            {
                System.IO.File.WriteAllText(HttpContext.Current.Server.MapPath("~/ExportSheet/" + FileExportDc.FileName), FileExportDc.HtmlText);
            }
            catch (Exception ex)
            {
            }
            return "Export Successfully";
        }

        #endregion

        #region Get Dahsboard Data
        [HttpGet]
        public DashboardDc GetDashboardData(string CommonId, string Role)
        {
            DashboardDc DashboardDc = new DashboardDc();
            DashboardDc.DashboardCountDc = new DashboardCountDc();
            DataSet ds = new DataSet();
            CommonManager CommonManager = new CommonManager();
            try
            {
                ds = CommonManager.GetDashboardData(CommonId, Role);
                if (ds != null)
                {
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        DashboardDc.DashboardCountDc.TotalCategory = ds.Tables[0].Rows[0]["TotalCategory"].ToString();
                        DashboardDc.DashboardCountDc.TotalBillReciept = ds.Tables[0].Rows[0]["TotalBillReciept"].ToString();
                        DashboardDc.DashboardCountDc.TotalCategoryReciept = ds.Tables[0].Rows[0]["TotalCategoryReciept"].ToString();
                        DashboardDc.DashboardCountDc.TotalCustomer = ds.Tables[0].Rows[0]["TotalCustomer"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return DashboardDc;
        }
        #endregion

        #region Change Password
        [HttpGet]
        public long ChangePassword(string emailid, string oldpassword, string newpassword)
        {
            long loginid = 0;
            try
            {
                RegistrationManager RegistrationManager = new RegistrationManager();
                loginid = RegistrationManager.ChangePassword(emailid, oldpassword, newpassword);
            }
            catch (Exception ex)
            {
                loginid = 0;
            }
            return loginid;
        }
        #endregion

        #region Forgot Password
        [HttpGet]
        public long ForgotPassword(string EmailId)
        {
            long loginid = 0;
            try
            {
                RegistrationManager RegistrationManager = new RegistrationManager();
                RegistrationTableDc RegistrationTableDc = null;
                RegistrationTableDc = RegistrationManager.GetRegistrationByusernamepassword(EmailId, "");
                if (RegistrationTableDc != null)
                {
                    string message;
                    message = "<table border='0' cellspacing='0' cellpadding='6' style='font-family:Arial;font-size:13px;width:100%;border-top:1px solid #ccc;border-left:1px solid #ccc;'>";
                    message += "<tr><td colspan='5' style='background:#00bcd4 url(http://pmtool.stagingsoftware.com/assets/images/backgrounds/header-bg.png) center center; padding:10px;text-align:center;border-bottom:1px solid #ccc;border-right:1px solid #ccc;'><img src='https://app.bills99.com/assets/images/Bills99.png' style='max-width:200px;max-height:100px;' /></td></tr>";
                    message += "<tr ><td colspan='5' style='background:#ebedf0;border-bottom:1px solid #ccc;border-right:1px solid #ccc;'>Hello " + RegistrationTableDc.Name + ",<br><br>Your Bill Generation Website Password is - " + RegistrationTableDc.Password + " <br><br>Click Here to login :- https://app.bills99.com/login </td></tr> ";
                    message += "</table>";

                    SendEmailSMS objsendMail = new SendEmailSMS();
                    if (EmailId != "")
                    {
                        string ans = objsendMail.Sending_Email(ConfigurationManager.AppSettings["SenderEmailId"].ToString(), EmailId, "shreeram@infocratsweb.com", "Bills 99 - Forgot Password", message, "");
                    }
                    loginid = RegistrationTableDc.UserId;
                }
                else
                {
                    loginid = -1;
                }
            }
            catch (Exception ex)
            {
            }
            return loginid;
        }
        #endregion

        #region Delete Registration By Id 
        [HttpGet]
        public bool DeleteRegistrationById(int UserId,bool IsActive)
        {
            bool Ans = false;
            try
            {
                RegistrationManager RegistrationManager = new RegistrationManager();
                Ans = RegistrationManager.DeleteRegistration(UserId, IsActive);
            }
            catch (Exception ex)
            {
            }
            return Ans;
        }
        #endregion 

    }
}
