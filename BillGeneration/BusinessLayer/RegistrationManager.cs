using BillGeneration.DataContract;
using BillGeneration.DataLayer;
using BillGeneration.DataLayer.EF;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BillGeneration.BusinessLayer
{
    public class RegistrationManager
    {
        Repository objRep = new Repository();

        #region Add Update Registration
        public int InsertUpdate_Registration(RegistrationTableDc Obj)
        {
            RegistrationTable RegistrationTables = new RegistrationTable();
            if (Obj.UserId > 0)
            {
                if (GetUserIdBYEmailId(Obj.UserId, Obj.EmailId) == 0)
                {
                    RegistrationTables = objRep.Get<RegistrationTable>(Obj.UserId);
                    RegistrationTables.Name = Obj.Name;
                    RegistrationTables.MobileNo = Obj.MobileNo;
                    RegistrationTables.Address = Obj.Address;
                    RegistrationTables.EmailId = Obj.EmailId;
                    RegistrationTables.Password = Obj.Password;
                    RegistrationTables.Role = Obj.Role;
                    RegistrationTables.ProfileImage = Obj.ProfileImage;
                    RegistrationTables.Designation = Obj.Designation;
                    RegistrationTables.TermsOfUse = Obj.TermsOfUse;
                    RegistrationTables.IsActive = Obj.IsActive;
                    RegistrationTables.UpdatedBy = Obj.UpdatedBy;
                    RegistrationTables.UpdatedDate = DateTime.Now;
                    objRep.Update<RegistrationTable>(RegistrationTables);
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (GetUserIdBYEmailId(0, Obj.EmailId) == 0)
                {
                    RegistrationTables.UserId = 0;
                    RegistrationTables.Name = Obj.Name;
                    RegistrationTables.MobileNo = Obj.MobileNo;
                    RegistrationTables.Address = Obj.Address;
                    RegistrationTables.EmailId = Obj.EmailId;
                    RegistrationTables.Password = Obj.Password;
                    RegistrationTables.Role = Obj.Role;
                    RegistrationTables.ProfileImage = Obj.ProfileImage;
                    RegistrationTables.Designation = Obj.Designation;
                    RegistrationTables.TermsOfUse = Obj.TermsOfUse;
                    RegistrationTables.IsActive = Obj.IsActive;
                    RegistrationTables.CreatedBy = Obj.CreatedBy;
                    RegistrationTables.CreatedDate = DateTime.Now;
                    objRep.Add<RegistrationTable>(RegistrationTables);
                }
                else
                {
                    return -1;
                }
            }
            return RegistrationTables.UserId;
        }

        public long GetUserIdBYEmailId(long UId, string EmailId)
        {
            long UserId = 0;
            try
            {
                if (UId > 0)
                {
                    UserId = objRep.FindAllBy<RegistrationTable>(x => x.EmailId == EmailId && x.UserId != UId && x.IsActive == true).FirstOrDefault().UserId;
                }
                else
                {
                    UserId = objRep.FindAllBy<RegistrationTable>(x => x.EmailId == EmailId && x.IsActive == true).FirstOrDefault().UserId;
                }
            }
            catch (Exception ex)
            {
                //Common.ErrorLogging(ex);
            }
            return UserId;
        }


        #endregion 

        #region Change Password
        public int ChangePassword(string emailid, string oldpassword, string newpassword)
        {
            RegistrationTable RegistrationTables = new RegistrationTable();
            RegistrationTableDc RegistrationTableDc = new RegistrationTableDc();
            if (emailid != null)
            {
                RegistrationTableDc = GetRegistrationByusernamepassword(emailid, oldpassword);
                if (RegistrationTableDc != null)
                {
                    RegistrationTables = objRep.Get<RegistrationTable>(RegistrationTableDc.UserId);
                    RegistrationTables.Password = newpassword;
                    objRep.Update<RegistrationTable>(RegistrationTables);
                }
            }
            return RegistrationTables.UserId;
        }

        #endregion 

        #region DeleteRegistration
        public bool DeleteRegistration(int UserId,bool isactive)
        {
            bool Ans = false;
            try
            {
                RegistrationTable RegistrationTable = new RegistrationTable();
                if (UserId > 0)
                {
                    RegistrationTable = objRep.Get<RegistrationTable>(UserId);
                    RegistrationTable.IsActive = isactive;
                    Ans = objRep.Update<RegistrationTable>(RegistrationTable);
                    Ans = true;
                }
            }
            catch (Exception ex)
            {
                Ans = false;
            }
            return Ans;
        }
        #endregion

        #region Get Registration By Id
        public RegistrationTableDc GetRegistrationById(int UserId)
        {
            RegistrationTableDc RegistrationTableDc = new RegistrationTableDc();
            try
            {
                RegistrationTableDc = objRep.FindAllBy<RegistrationTable>(x => x.UserId == UserId).Select(x => new RegistrationTableDc
                {
                    UserId = x.UserId,
                    Name = x.Name,
                    MobileNo = x.MobileNo,
                    Address = x.Address,
                    EmailId = x.EmailId,
                    Password = x.Password,
                    Role = x.Role,
                    ProfileImage = x.ProfileImage,
                    Designation = x.Designation,
                    TermsOfUse = x.TermsOfUse,
                    IsActive = x.IsActive,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate,
                }).FirstOrDefault();

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                throw ex;
            }
            return RegistrationTableDc;
        }
        #endregion

        #region Get Registration By username password
        public RegistrationTableDc GetRegistrationByusernamepassword(string emailid, string password)
        {
            RegistrationTableDc RegistrationTableDc = new RegistrationTableDc();
            CommonManager CommonManager = new CommonManager();
            try
            {
                if (password != null && password != "" && password != "0")
                {
                    RegistrationTableDc = objRep.FindAllBy<RegistrationTable>(x => x.EmailId == emailid && x.Password == password && x.IsActive == true).Select(x => new RegistrationTableDc
                    {
                        UserId = x.UserId,
                        Name = x.Name,
                        MobileNo = x.MobileNo,
                        Address = x.Address,
                        EmailId = x.EmailId,
                        Password = x.Password,
                        Role = x.Role,
                        ProfileImage = x.ProfileImage,
                        Designation = x.Designation,
                        TermsOfUse = x.TermsOfUse,
                        IsActive = x.IsActive,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate,
                    }).FirstOrDefault();
                }
                else
                {
                    RegistrationTableDc = objRep.FindAllBy<RegistrationTable>(x => x.EmailId == emailid && x.IsActive == true).Select(x => new RegistrationTableDc
                    {
                        UserId = x.UserId,
                        Name = x.Name,
                        MobileNo = x.MobileNo,
                        Address = x.Address,
                        EmailId = x.EmailId,
                        Password = x.Password,
                        Role = x.Role,
                        ProfileImage = x.ProfileImage,
                        Designation = x.Designation,
                        TermsOfUse = x.TermsOfUse,
                        IsActive = x.IsActive,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        UpdatedBy = x.UpdatedBy,
                        UpdatedDate = x.UpdatedDate,
                    }).FirstOrDefault();
                }
                if (RegistrationTableDc != null)
                {
                    DataTable dt = CommonManager.GetPlantPayByUserId(RegistrationTableDc.UserId);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        RegistrationTableDc.IsSubscription = true;
                        RegistrationTableDc.SubscriptionPlanId = int.Parse(dt.Rows[0]["SubscriptionPlanId"].ToString());
                    }
                    else
                    {
                        RegistrationTableDc.IsSubscription = false;
                        RegistrationTableDc.SubscriptionPlanId = 0;
                    }
                }
                else
                {
                    RegistrationTableDc.IsSubscription = false;
                    RegistrationTableDc.SubscriptionPlanId = 0;
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                throw ex;
            }
            return RegistrationTableDc;
        }
        #endregion

        #region Get Registration Dropdown
        public List<RegistrationDropdownDc> GetRegistrationDropdown(string Role)
        {
            List<RegistrationDropdownDc> RegistrationDropdownDc = new List<RegistrationDropdownDc>();
            try
            {
                RegistrationDropdownDc = objRep.FindAllBy1<RegistrationTable>(z => z.IsActive == true && Role.Contains(z.Role), x => x.OrderBy(y => y.Name)).Select(x => new RegistrationDropdownDc
                {
                    UserId = x.UserId,
                    Name = x.Name,
                }).ToList();
            }
            catch { }

            return RegistrationDropdownDc;
        }
        #endregion

        #region Search Registration
        public List<RegistrationTableDc> SearchRegistration(RegistrationSearchDc RegistrationSearchDc)
        {
            var LoginPredicate = PredicateBuilder.New<RegistrationTable>(true);

            List<RegistrationTableDc> RegistrationTableDcList;

            if (!string.IsNullOrEmpty(RegistrationSearchDc.Name))
            {
                LoginPredicate = LoginPredicate.And(p => p.Name.Contains(RegistrationSearchDc.Name));
            }
            if (!string.IsNullOrEmpty(RegistrationSearchDc.Designation))
            {
                LoginPredicate = LoginPredicate.And(p => p.Designation.Contains(RegistrationSearchDc.Designation));
            }
            if (!string.IsNullOrEmpty(RegistrationSearchDc.Role))
            {
                LoginPredicate = LoginPredicate.And(p => p.Role.Contains(RegistrationSearchDc.Role));
            }
            if (!string.IsNullOrEmpty(RegistrationSearchDc.EmailId))
            {
                LoginPredicate = LoginPredicate.And(p => p.EmailId.Contains(RegistrationSearchDc.EmailId));
            }
            if (RegistrationSearchDc.IsActive.ToString() != "All" && !string.IsNullOrEmpty(RegistrationSearchDc.IsActive.ToString()))
            {
                LoginPredicate = LoginPredicate.And(p => p.IsActive.ToString() == RegistrationSearchDc.IsActive);
            }
            RegistrationTableDcList = objRep.FindAllBy1<RegistrationTable>(LoginPredicate, x => x.OrderByDescending(y => y.UserId)).Select(x => new RegistrationTableDc
            {
                UserId = x.UserId,
                Name = x.Name,
                MobileNo = x.MobileNo,
                Address = x.Address,
                EmailId = x.EmailId,
                Password = x.Password,
                Role = x.Role,
                ProfileImage = x.ProfileImage,
                Designation = x.Designation,
                TermsOfUse = x.TermsOfUse,
                IsActive = x.IsActive,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                UpdatedBy = x.UpdatedBy,
                UpdatedDate = x.UpdatedDate,
            }).ToList();

            return RegistrationTableDcList;
        }
        #endregion
    }
}
