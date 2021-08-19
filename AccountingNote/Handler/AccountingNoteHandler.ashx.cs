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
    /// AccountingNoteHandler 的摘要描述
    /// </summary>
    public class AccountingNoteHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string actionName = context.Request.QueryString["ActionName"];

            if (string.IsNullOrEmpty(actionName))
            {
                context.Response.StatusCode = 404;
                context.Response.ContentType = "text/plain";
                context.Response.Write("ActionName is required.");
                context.Response.End();
            }

            if (actionName == "create")
            {
                string caption = context.Request.Form["Caption"];
                string amountText = context.Request.Form["Amount"];
                string actTypeText = context.Request.Form["ActType"];
                string body = context.Request.Form["Body"];

                //ID of admin
                string userID = "AEBB9636-D3D6-444E-9DB9-6659EB00F7A8";
                if (string.IsNullOrWhiteSpace(caption) ||
                   string.IsNullOrWhiteSpace(amountText) ||
                   string.IsNullOrWhiteSpace(actTypeText))
                {
                    this.ProcessError(context, "caption, amount, actType is required.");
                    return;
                }

                //轉型
                int tempAmount, tempActType;
                if (!int.TryParse(amountText, out tempAmount) ||
                    !int.TryParse(actTypeText, out tempActType))
                {
                    this.ProcessError(context, "caption, amount, actType should be a integer.");
                    return;
                }
                try
                {
                    //建立流水帳
                    Accounting accounting = new Accounting()
                    {
                        UserID = userID.ToGuid(),
                        Caption = caption,
                        Amount = tempAmount,
                        ActType = tempActType,
                        Body = body
                    };
                    AccountingManager.CreateAccounting(accounting);
                    //AccountingManager.CreateAccounting(userID, caption, tempAmount, tempActType, body);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Create ok");
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 503;
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Error");
                }

            }
            else if (actionName == "updata")   //為什麼沒有進去資料庫呢?  成功了耶
            {
                string caption = context.Request.Form["Caption"];
                string amountText = context.Request.Form["Amount"];
                string actTypeText = context.Request.Form["ActType"];
                string body = context.Request.Form["Body"];

                //ID of admin
                string userID = "AEBB9636-D3D6-444E-9DB9-6659EB00F7A8";
                int listID = 1039;
                if (string.IsNullOrWhiteSpace(caption) ||
                   string.IsNullOrWhiteSpace(amountText) ||
                   string.IsNullOrWhiteSpace(actTypeText))
                {
                    this.ProcessError(context, "caption, amount, actType is required.");
                    return;
                }

                //轉型
                int tempAmount, tempActType;
                if (!int.TryParse(amountText, out tempAmount) ||
                    !int.TryParse(actTypeText, out tempActType))
                {
                    this.ProcessError(context, "caption, amount, actType should be a integer.");
                    return;
                }
                try
                {
                    //更新流水帳
                    AccountingManager.UpdateAccounting(listID, userID, caption, tempAmount, tempActType, body);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Updata ok");
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 503;
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Error");
                }
            }
            else if (actionName == "delete")
            {
                string caption = context.Request.Form["Caption"];
                string amountText = context.Request.Form["Amount"];
                string actTypeText = context.Request.Form["ActType"];
                string body = context.Request.Form["Body"];

                //要刪除的明細ID
                int listID = 1039;
                if (string.IsNullOrWhiteSpace(caption) ||
                   string.IsNullOrWhiteSpace(amountText) ||
                   string.IsNullOrWhiteSpace(actTypeText))
                {
                    this.ProcessError(context, "caption, amount, actType is required.");
                    return;
                }

                //轉型
                int tempAmount, tempActType;
                if (!int.TryParse(amountText, out tempAmount) ||
                    !int.TryParse(actTypeText, out tempActType))
                {
                    this.ProcessError(context, "caption, amount, actType should be a integer.");
                    return;
                }
                try
                {
                    //刪除流水帳
                    AccountingManager.DeleteAccounting(listID);
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Delete ok");
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 503;
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("Error");
                }
            }
            else if(actionName == "query")
            {
                string idText = context.Request.Form["ID"];
                int id;
                int.TryParse(idText, out id);
                //string userID = "AEBB9636-D3D6-444E-9DB9-6659EB00F7A8";
                Guid userGUID = new Guid("AEBB9636-D3D6-444E-9DB9-6659EB00F7A8");

                var accounting = AccountingManager.GetAccounting(id, userGUID);

                if (accounting == null) 
                {
                    context.Response.StatusCode = 404;
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("No data" + idText);
                    context.Response.End();
                    return;
                }

                AccountingNoteViewModel model = new AccountingNoteViewModel()
                {
                    ID = accounting.ID,
                    Caption = accounting.Caption.ToString(),
                    Body = accounting.Body,
                    Amount = accounting.Amount,
                    ActType = (accounting.ActType == 0) ? "支出" : "收入",
                    CreateDate = accounting.CreateDate.ToString("yyyy-MM-dd"),

                };
                string jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                context.Response.ContentType = "application/json";
                context.Response.Write(jsonText);

            }
            else if (actionName == "list")
            {
                //string userID = "AEBB9636-D3D6-444E-9DB9-6659EB00F7A8";
                Guid userGUID = new Guid("AEBB9636-D3D6-444E-9DB9-6659EB00F7A8");

               List<Accounting> sourceList = AccountingManager.GetAccountingList(userGUID);
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
            }
        }

        private void ProcessError(HttpContext context, string msg)
        {
            context.Response.StatusCode = 404;
            context.Response.ContentType = "text/plain";
            context.Response.Write(msg);
            context.Response.End();
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