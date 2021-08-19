using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TryWebApplication
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TryMain mainMaster = this.Master as TryMain;
            mainMaster.MyTitle = "預設頁";
            mainMaster.SetPageCaption("預設頁");

            //this.ucCoverImage.SetText("第一個 uc");
            this.ucCoverImage.SetText("第二個 uc");
            this.ucCoverImage.SetText("第三個 uc");

        }
    }
}