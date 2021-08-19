using AccountingNote.DBSource;
using AccountingNote.Extension;
using AccountingNote.Models;
using AccountingNote.ORM2.DBModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AccountingNote.Handler
{
    /// <summary>
    /// AccountingNotrList 的摘要描述
    /// </summary>
    public class AccountingNotrList : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string account = context.Request.QueryString["Account"];

            if (string.IsNullOrWhiteSpace(account))
            {
                context.Response.StatusCode = 404;
                context.Response.End();
                return;
            }

            var userInfo = UserInfoManager.GetUserInfoByAccount_ORM(account);

            if (userInfo == null) 
            {
                context.Response.StatusCode = 404;
                context.Response.End();
                return;
            }

            List<Accounting> sourceList = AccountingManager.GetAccountingList(userInfo.ID);

            //List <AccountingNoteViewModel> list = new List<AccountingNoteViewModel>();
            //foreach (DataRow drAccounting in dataTable.Rows) 
            //{
            //    AccountingNoteViewModel model = new AccountingNoteViewModel()
            //    {
            //        ID = drAccounting["ID"].ToString(),
            //        Caption = drAccounting["Caption"].ToString(),
            //        Amount = drAccounting.Field<int>("Amount"),
            //        ActType = (drAccounting.Field<int>("ActType") == 0) ? "支出" : "收入",
            //        CreateDate = drAccounting.Field<DateTime>("CreateDate").ToString("yyyy-MM-dd")
            //    };

            //    list.Add(model);
            //}

            List<AccountingNoteViewModel> list =
                sourceList.Select(obj => new AccountingNoteViewModel()
                {
                    ID = obj.ID,
                    Caption = obj.Caption.ToString(),
                    Amount = obj.Amount,
                    ActType = (obj.ActType == 0) ? "支出" : "收入",
                    CreateDate = obj.CreateDate.ToString("yyyy-MM-dd")
                }).ToList();

            string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(list);

            context.Response.ContentType = "application/json";
            context.Response.Write(jsonText);

            //context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}