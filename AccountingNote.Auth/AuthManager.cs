using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AccountingNote.Auth
{
    /// <summary> 負責處理登入的元件</summary>
    public class AuthManager
    {
        /// <summary> 檢查目前是否登入</summary>
        /// <returns></returns>
        public static bool IsLogined()
        {
            if (HttpContext.Current.Session["UserLoginInfo"] == null)
                return false;
            else
                return true;
        }

        public static UserInfoModel GetCurrentUser()
        {
            string account = HttpContext.Current.Session["UserLoginInfo"] as string;
            
            if (account == null)
                return null;

            var userInfo = UserInfoManager.GetUserInfoByAccount_ORM(account);

            if (userInfo == null)
            {
                HttpContext.Current.Session["UserLoginInfo"] = null;
                return null;
            }
            

            UserInfoModel model = new UserInfoModel();
            model.ID = userInfo.ID;
            model.Account = userInfo.Account;
            model.Name = userInfo.Name;
            model.Email = userInfo.Email;
            model.Phone = userInfo.Phone;
            //model.UserLevel = dr["UserLevel"].ToString();

            return model;
        }


        /// <summary> 登出 </summary>
        public static void Logout()
        {
           HttpContext.Current.Session["UserLoginInfo"] = null;
        }

        public static bool TryLogin(string account, string pwd, out string errorMsg)
        {
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(pwd))
            {
                errorMsg = "Account / PWD is required.";
                return false;
            }

            var userInfo = UserInfoManager.GetUserInfoByAccount_ORM(account);

            //check null
            if (userInfo == null)
            {
                errorMsg = "Account doesn't exists.";
                return false;
            }

            //check account / PWD
            if (string.Compare(userInfo.Account.ToString(), account, true) == 0 && 
                string.Compare(userInfo.PWD.ToString(), pwd, false) == 0)
            {
                HttpContext.Current.Session["UserLoginInfo"] = userInfo.Account;

                errorMsg = string.Empty;
                return true;
            }
            else
            {
                errorMsg = "Login fail. Place check Account / PWD.";
                return false;
            }

        }

    }
}
