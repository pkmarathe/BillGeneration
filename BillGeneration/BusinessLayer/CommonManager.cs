using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace BillGeneration.BusinessLayer
{
    public class CommonManager
    { 

        #region Private Variable
        DatabaseProviderFactory DatabaseFactory = new DatabaseProviderFactory();
        Database database;
        DbCommand command;
        public String MessageToUser;
        public int ReturnValue;
        public DataTable ResultSet;
        public DataSet ResultDataSet;
        public string ReturnString;
        #endregion         

        #region Get Dashboard Data
        public DataSet GetDashboardData(string CommonId, string Role)
        {
            try
            {
                database = DatabaseFactory.Create("sqlConnectionstring");
                command = database.GetStoredProcCommand("Formated_GETDASHBOARD");
                database.AddInParameter(command, "CommonId", DbType.String, CommonId);
                database.AddInParameter(command, "Role", DbType.String, Role);
                ResultDataSet = new DataSet();
                ResultDataSet = database.ExecuteDataSet(command); 
            }
            catch (DbException DbEx)
            {
                ResultDataSet = null;
                MessageToUser = DbEx.Message;
            }
            catch (Exception ex)
            {
                ResultDataSet = null;
                MessageToUser = ex.Message;
            }
            finally
            {
                database = null; command = null;
            }
            return ResultDataSet;
        }
        #endregion

        #region Delete BillReciept Tax Item
        public int DeleteBillRecieptTax_Item(int BillRecieptId)
        {
            try
            {
                database = DatabaseFactory.Create("sqlConnectionstring");
                command = database.GetStoredProcCommand("Formated_DeleteBillRecieptTax_Item");
                database.AddInParameter(command, "BillRecieptId", DbType.Int32, BillRecieptId);
                ReturnValue = database.ExecuteNonQuery(command);
            }
            catch (DbException DbEx)
            {
                ReturnValue = -1;
                MessageToUser = DbEx.Message;
            }
            catch (Exception ex)
            {
                ReturnValue = -1;
                MessageToUser = ex.Message;
            }
            finally
            {
                database = null; command = null;
            }
            return ReturnValue;
        }
        #endregion

        #region GetPlantPayByUserId
        public DataTable GetPlantPayByUserId(int userid)
        {
            try
            {
                database = DatabaseFactory.Create("sqlConnectionstring");
                command = database.GetStoredProcCommand("Formated_GetPlantPayByUserId");
                database.AddInParameter(command, "userid", DbType.String, userid); 
                command.CommandTimeout = 120;
                IDataReader dr = database.ExecuteReader(command);
                ResultSet = new DataTable();
                ResultSet.Load(dr);
            }
            catch (DbException DbEx)
            {
                ResultSet = null;
                MessageToUser = DbEx.Message;
            }
            catch (Exception ex)
            {
                ResultSet = null;
                MessageToUser = ex.Message;
            }
            finally
            {
                database = null; command = null;
            }
            return ResultSet;
        }
        #endregion

        #region GetFontMasterByFontFontStyle
        public DataTable GetFontMasterByFontFontStyle(string FontStyle)
        {
            try
            {
                database = DatabaseFactory.Create("sqlConnectionstring");
                command = database.GetStoredProcCommand("Formated_GetFontMasterByFontFontStyle");
                database.AddInParameter(command, "FontStyle", DbType.String, FontStyle); 
                IDataReader dr = database.ExecuteReader(command);
                ResultSet = new DataTable();
                ResultSet.Load(dr);
            }
            catch (DbException DbEx)
            {
                ResultSet = null;
                MessageToUser = DbEx.Message;
            }
            catch (Exception ex)
            {
                ResultSet = null;
                MessageToUser = ex.Message;
            }
            finally
            {
                database = null; command = null;
            }
            return ResultSet;
        }
        #endregion

        #region GetFontMasterByFontFontStyle
        public DataTable GetUserPaymentDetailByUPSId(int UserId,int SubscriptionPlanId)
        {
            try
            {
                database = DatabaseFactory.Create("sqlConnectionstring");
                command = database.GetStoredProcCommand("Formated_GetUserPaymentDetailByUPSId");
                database.AddInParameter(command, "UserId", DbType.String, UserId);
                database.AddInParameter(command, "SubscriptionPlanId", DbType.String, SubscriptionPlanId);
                IDataReader dr = database.ExecuteReader(command);
                ResultSet = new DataTable();
                ResultSet.Load(dr);
            }
            catch (DbException DbEx)
            {
                ResultSet = null;
                MessageToUser = DbEx.Message;
            }
            catch (Exception ex)
            {
                ResultSet = null;
                MessageToUser = ex.Message;
            }
            finally
            {
                database = null; command = null;
            }
            return ResultSet;
        }
        #endregion

        #region Get Payment Currency
        public DataTable GetPaymentCurrency(string Currency)
        {
            try
            {
                database = DatabaseFactory.Create("sqlConnectionstring");
                command = database.GetStoredProcCommand("Formated_GetPaymentCurrency");
                database.AddInParameter(command, "Currency", DbType.String, Currency); 
                IDataReader dr = database.ExecuteReader(command);
                ResultSet = new DataTable();
                ResultSet.Load(dr);
            }
            catch (DbException DbEx)
            {
                ResultSet = null;
                MessageToUser = DbEx.Message;
            }
            catch (Exception ex)
            {
                ResultSet = null;
                MessageToUser = ex.Message;
            }
            finally
            {
                database = null; command = null;
            }
            return ResultSet;
        }
        #endregion

        #region Get Stripe Currency Plan
        public DataTable GetStripeCurrencyPlan(string Currency,string SubscriptionPlanId)
        {
            try
            {
                database = DatabaseFactory.Create("sqlConnectionstring");
                command = database.GetStoredProcCommand("Formated_GetStripeCurrencyPlan");
                database.AddInParameter(command, "Currency", DbType.String, Currency);
                database.AddInParameter(command, "SubscriptionPlanId", DbType.String, SubscriptionPlanId);
                IDataReader dr = database.ExecuteReader(command);
                ResultSet = new DataTable();
                ResultSet.Load(dr);
            }
            catch (DbException DbEx)
            {
                ResultSet = null;
                MessageToUser = DbEx.Message;
            }
            catch (Exception ex)
            {
                ResultSet = null;
                MessageToUser = ex.Message;
            }
            finally
            {
                database = null; command = null;
            }
            return ResultSet;
        }
        #endregion
    }
}
