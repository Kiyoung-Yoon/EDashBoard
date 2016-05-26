using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace InputPlan
{
    public partial class Access : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //user_id=UTAzMjYxMzY1
            string action = Request.Params["user_id"];
            Session["USERID"] = action;



            Response.Clear();
            string res = "<script>this.location.href='/extjs/top.html';</script>";
            Response.Write(res);
            return;
        }
    }
}
