using AccountingNote.Auth;
using AccountingNote.DBSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AccountingNote
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["UserLogin"] != null)
            {
                this.plcLogin.Visible = true;
                //Response.Redirect("/SystemAdmin/UserInfo.aspx");
            }
        }

        protected void btnLogin_Click1(object sender, EventArgs e)
        {
            string inp_Account = this.txtAccount.Text;
            string inp_PWD = this.txtPWD.Text;

            string msg;
            if (!AuthManager.TryLogin(inp_Account, inp_PWD, out msg))
            {
                this.ltlMsg.Text = msg;
                return;
            }

            Response.Redirect("/SystemAdmin/UserInfo.aspx");

            //if (string.IsNullOrWhiteSpace(inp_Account) || string.IsNullOrWhiteSpace(inp_PWD))
            //{
            //    this.ltlMsg.Text = "Account / PWD is required.";
            //    return;
            //}

            //var dr = UserInfoManager.GetUserInfoByAccount(inp_Account);

            ////check null
            //if (dr == null)
            //{
            //    this.ltlMsg.Text = "Account doesn't exists.";
            //    return;
            //}

            ////check account / PWD
            //if (string.Compare(dr["Account"].ToString(), inp_Account, true) == 0 && string.Compare(dr["PWD"].ToString(), inp_PWD, false) == 0)
            //{
            //    this.Session["UserLoginInfo"] = dr["Account"].ToString();
            //    Response.Redirect("/SystemAdmin/UserInfo.aspx");
            //}
            //else
            //{
            //    this.ltlMsg.Text = "Login fail. Place check Account / PWD.";
            //    return;
            //}
        }
    }
}