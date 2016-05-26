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

using CPU;

namespace InputPlan
{   
    public partial class List : System.Web.UI.Page
    {
        CPU.CPU_InputPlan cpu = new CPU_InputPlan();

        protected void Page_Load(object sender, EventArgs e)
        {
            cpu.Test();
            
        }
    }
}
