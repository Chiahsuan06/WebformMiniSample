using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingNote.DBSource
{
    public class AccountingManager
    {
        //public static string GetConnectionString()
        //{
        //    string val = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //    return val;
        //}
        //
        public static DataTable GetAccountingList(string userID)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" SELECT ID, Caption, Amount, ActType, CreateDate, Body
                    FROM Accounting
                    WHERE UserID = @userID
                ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@userID", userID));
            try
            {
                return DBHelper.ReadDataTable(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return null;
            }
        }

        public static DataRow GetAccounting(int id, string userID)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" SELECT ID, Caption, Amount, ActType, CreateDate, Body
                    FROM Accounting
                    WHERE id = @id AND UserID = @userID
                ";

            List<SqlParameter> list = new List<SqlParameter>();
            list.Add(new SqlParameter("@id", id));
            list.Add(new SqlParameter("@userID", userID));
            try 
            { 
            return DBHelper.ReadDataRow(connStr, dbCommand, list);
            }
            catch (Exception ex)
            {
                 Logger.WriteLog(ex);
                 return null;
            }

        }

        

        //
        public static void CreateAccounting(string userID, string caption, int amount, int actType, string body)
        {
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1000000.");
            if (actType < 0 || actType > 1)
                throw new ArgumentException("ActType must be 0 or 1.");

            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" INSERT INTO [dbo].[Accounting]
                (
                        UserID
                       ,Caption
                       ,Amount
                       ,ActType
                       ,CreateDate
                       ,Body
                 )
                    VALUES
                (
                            @userID
                           ,@caption
                           ,@amount
                           ,@actType
                           ,@createDate
                           ,@body
                )
                ";

            List<SqlParameter> createList = new List<SqlParameter>();
            createList.Add(new SqlParameter("@userID", userID));
            createList.Add(new SqlParameter("@caption", caption));
            createList.Add(new SqlParameter("@amount", amount));
            createList.Add(new SqlParameter("@actType", actType));
            createList.Add(new SqlParameter("@createDate", DateTime.Now));
            createList.Add(new SqlParameter("@body", body));

            try
            {
                 DBHelper.CreateData(connStr, dbCommand, createList);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
        }


        public static bool UpdateAccounting(int ID, string userID, string caption, int amount, int actType, string body)
        {
            if (amount < 0 || amount > 1000000)
                throw new ArgumentException("Amount must between 0 and 1000000.");
            if (actType < 0 || actType > 1)
                throw new ArgumentException("ActType must be 0 or 1.");

            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" UPDATE [Accounting]
                    SET                     
                        UserID = @userID
                       ,Caption = @caption
                       ,Amount = @amount
                       ,ActType = @actType
                       ,CreateDate = @createDate
                       ,Body = @body
                    WHERE
                        ID = @id
                ";

            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@userID", userID));
            parameters.Add(new SqlParameter("@caption", caption)); 
            parameters.Add(new SqlParameter("@amount", amount)); 
            parameters.Add(new SqlParameter("@actType", actType)); 
            parameters.Add(new SqlParameter("@createDate", DateTime.Now)); 
            parameters.Add(new SqlParameter("@body", body)); 
            parameters.Add(new SqlParameter("@id", ID));


            try
            {
                int effectRows = DBHelper.ModifyData(connStr, dbCommand, parameters);

                if (effectRows == 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
                return false;
            }

        }


        public static void DeleteAccounting(int ID)
        {
            string connStr = DBHelper.GetConnectionString();
            string dbCommand =
                $@" DELETE [Accounting]
                    WHERE ID =@id
                ";

            List<SqlParameter> paramList = new List<SqlParameter>();
            paramList.Add(new SqlParameter("@id", ID));

            try
            {
               DBHelper.ModifyData(connStr, dbCommand, paramList);
            }
            catch (Exception ex)
            {
                Logger.WriteLog(ex);
            }
            
        }
    }
}