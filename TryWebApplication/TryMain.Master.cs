using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TryWebApplication
{
    public partial class TryMain : System.Web.UI.MasterPage
    {
        public string MyTitle { get; set; } = string.Empty;

        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Write("MasterPage_Page_Init<br/>");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("MasterPage_Page_Load<br/>");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Response.Write("MasterPage_Page_PreRender<br/>");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("MasterPage_Button1_Click<br/>");
            this.ltlMsg.Text = this.txtEmail.Text;
        }

        public void SetPageCaption(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
                this.ltlCaption.Text = title;
        }
    }
}