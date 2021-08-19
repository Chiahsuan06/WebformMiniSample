using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TryWebApplication
{
    public partial class ucCoverImage : System.Web.UI.UserControl
    {
        public string MyTitle { get; set; }

        public enum Bcolor
        { 
            Blue,
            Red,
            Green
        }

        public Bcolor BackColor { get; set; } = Bcolor.Green;

        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Write("ucCoverImage_Page_Init<br/>");
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Response.Write("ucCoverImage_Page_PreRender<br/>");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("ucCoverImage_Page_Load<br/>");

            if (!string.IsNullOrWhiteSpace(this.MyTitle))
            {
                this.ltlTitle.Text = this.MyTitle;
                this.imgCover.Alt = this.MyTitle;
            }
            this.divMain.Style.Add("background-color", this.BackColor.ToString());
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("ucCoverImage_Button1_Click<br/>");

            this.ltlTitle.Text = "ucCoverImage_Click";
            this.imgCover.Alt = "ucCoverImage_Click";
        }

        public void SetText(string title)
        {
            if(!string.IsNullOrWhiteSpace(title))
            {
                this.ltlTitle.Text = title;
                this.imgCover.Alt = title;
            }
            
        }
    }
}