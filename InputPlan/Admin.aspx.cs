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
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Session["USERID"] = "1254";
            //Session["USERNAME"] = "문찬식";
            //Session["DEPTCODE"] = "Q031ERP00";

            Session["USERID"] = "1254";
            Session["USERNAME"] = "문찬식";
            Session["DEPTCODE"] = "Q031ERP00";
        }
    }
}
