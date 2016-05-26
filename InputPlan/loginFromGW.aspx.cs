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
using System.Data.OracleClient;
using CPU;
using MSXML2;



namespace InputPlan
{
    public partial class loginFromGW : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string url = @"http://autoever.hyundai.net/Webservices/SSO/SitemapWS.asmx";
            string enccode = Request.Params["Encode"];
            string companycode = Request.Params["CompanyCode"];
            string strHKMCENC_ID = "EBOARD";
            string param = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Body><GetPlainText xmlns=\"http://tempuri.org/\">"
                        + "<strEncID>" + strHKMCENC_ID + "</strEncID>"
                        + "<strCompanyCode>" + companycode + "</strCompanyCode>"
                        + "<strEncText>" + enccode + "</strEncText>"
                        + "</GetPlainText></soap:Body></soap:Envelope>";

            System.Diagnostics.Debug.WriteLine("----------22");

            ServerXMLHTTP srv = new ServerXMLHTTP();
            srv.open("POST", url, false, null, null);
            srv.setRequestHeader("Content-Type", "text/xml; charset=utf-8");
            srv.setRequestHeader("SOAPAction", "http://tempuri.org/GetPlainText");

            srv.send(param);
            Response.Clear();
            string __res__ = srv.responseText;
            /*
             * 자아.. 주석을 조금 달자..
             * 일단은 다음과 같이 웹서비스를 호출해도 정상적으로 동작한다..
             */

            //net.hyundai.autoever.SitemapWS aaa = new InputPlan.net.hyundai.autoever.SitemapWS();
            //String aa = aaa.GetPlainText(strHKMCENC_ID, companycode, enccode);



            bool isLogin = false;
            
            try{
                int val1 = __res__.IndexOf("<GetPlainTextResult>");
                int val2 = __res__.IndexOf("</GetPlainTextResult>");

                if (val1 <= 0 || val2 <= 0)
                {
                    return;
                }
                string result = __res__.Substring(val1, (val2 - val1));

                string[] token = System.Text.RegularExpressions.Regex.Split(result, "___");

                for (int i = 0; i < token.Length; i++)
                {
                    string[] value = System.Text.RegularExpressions.Regex.Split( token[i] , "\\|\\|");

                    Session[value[0]] = value[1];
                    
                    if ("User_ID".EndsWith(value[0]))
                    {   
                        //Session["USERID"] = value[1];

                        string user = value[1];
                        // 그리고 사번..
                        OraDB DB = new OraDB();
                        DB.Connect();
                        try
                        {
                            string user_name = "";
                            string dept_code = "";
                            string login_sql = "select * from tb_person where user_id = '" + user + "'";
                            string object_id = DB.GetQueryResult<string>(login_sql, delegate(OracleDataReader reader)
                            {
                                string val = reader[0].ToString();
                                user_name = reader[1].ToString();
                                dept_code = reader[3].ToString();
                                return val;
                            });

                            if (object_id == null || "".Equals(object_id))
                                isLogin = false;
                            else
                            {
                                Session["USERID"] = object_id;
                                Session["USERNAME"] = user_name;
                                Session["DEPTCODE"] = dept_code;
                                isLogin = true;
                            }

                            DB.Close();
                        }
                        catch (Exception eee)
                        {
                            System.Diagnostics.Debug.WriteLine(eee.Message);
                            isLogin = false;
                        }
                        finally
                        {
                            DB.Close();
                        }
                    }
                    /*
                    if ("Dept_Code".EndsWith(value[0]))
                    {
                        Session["DEPTCODE"] = value[1];
                    }

                    if ("User_Name".EndsWith(value[0]))
                    {   
                        Session["USERNAME"] = value[1];
                    } */
                }
            }catch( Exception ex ){
                Response.Write( ex.Message );
            }

            if (isLogin == false)
            {
                Response.Clear();
                string res = "<script>alert('Access Dinied....');</script>";
                Response.Write(res);

                return;
            }

            Response.Clear();
            string res1 = "<script>this.location.href='/extjs/top.html';</script>";
            Response.Write(res1);
            return;
        }
    }
}
