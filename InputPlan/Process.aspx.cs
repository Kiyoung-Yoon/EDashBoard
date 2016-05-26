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
using System.Collections.Generic;

using CPU;

namespace InputPlan
{
    public partial class Process : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*
             */
            string USERID = "";
            object _id = Session["USERID"];

            object user_id = Session["USERID"];
            object user_name = Session["USERNAME"];
            object dept_code = Session["DEPTCODE"];

            if (_id != null) USERID = _id.ToString();

            if( USERID == null || "".Equals( USERID ) )
            {
                Response.Clear();
                string res = "<script>alert('Please Login....');</script>";
                Response.Write(res);
                return;
            }
            
            /*
             */

            string action = Request.Params["action"];

            if (action == null) action = "";


            if ("GETLIST".Equals(action.ToUpper()))
            {
                Proc_GetList();
            }

            if ("CLSINS".Equals(action.ToUpper()))
            {
                Proc_ClsIns();
            }

            if ("CLSMOD".Equals(action.ToUpper()))
            {
                Proc_ClsMod();
            }

            if ("CLSDEL".Equals(action.ToUpper()))
            {
                Proc_ClsDel();
            }


            /*
             * 자아.. 해줘야 하는 것들 다시한번 확인..
             * 1. 트리 클릭시 store 를 로드.. ( 특정 위치로 로드 하기.. )
             * 2. 그리드 상에서 목록 추가하기
             * 
             */



            if ("GETOBJLIST".Equals(action.ToUpper()))
            {
                Proc_GetOBJList();
            }

            if ("OBJINS".Equals(action.ToUpper()))
            {
                Proc_OBJIns();
            }

            if ("OBJMOD".Equals(action.ToUpper()))
            {
                Proc_OBJMod();
            }

            if ("OBJDEL".Equals(action.ToUpper()))
            {
                Proc_OBJDel();
            }



            if ("GETCODELIST".Equals(action.ToUpper()))
            {
                Proc_GetCODEList();
            }

            // 권한설정을 위해서.. 사용자 직접 설정..
            if ("GETPERSON".Equals(action.ToUpper()))
            {
                Proc_getPerson();
            }

        }
        public void Proc_ClsIns()
        {
            string obj_id = Request.Params["OBJECT_ID"];
            string title = Request.Params["CATE_NAME"];
            string id = "";
            object _id = Session["USERID"];
            if (_id != null) id = _id.ToString();

            
            clsClassfication cls = new clsClassfication();
            cls.CATE_NAME = title;
            cls.insert(id, obj_id);

            Response.Clear();
            string res = "{ 'RESULT' : 'true' }";
            Response.Write(res);

        }
        public void Proc_ClsMod()
        {
            string obj_id = Request.Params["OBJECT_ID"];
            string title = Request.Params["CATE_NAME"];
            string id = "";
            object _id = Session["USERID"];
            if (_id != null) id = _id.ToString();

            clsClassfication cls = new clsClassfication();
            cls.load(obj_id);
            cls.CATE_NAME = title;
            cls.update( id );

            Response.Clear();
            string res = "{ 'RESULT' : 'true' }";
            Response.Write(res);

        }
        public void Proc_ClsDel()
        {
            string obj_id = Request.Params["OBJECT_ID"];
            string id = "";
            object _id = Session["USERID"];
            if (_id != null) id = _id.ToString();

            clsClassfication cls = new clsClassfication();
            cls.load(obj_id);
            cls.delete(id);
            Response.Clear();
            string res = "{ 'RESULT' : 'true' }";
            Response.Write(res);

        }

        public void Proc_GetList()
        {
            //bool getChild = true;
            string node = Request.Params["node"];
            if (node.StartsWith("root")) node = "";
            string id = "";
            object _id = Session["USERID"];
            if (_id != null) id = _id.ToString();


            List<clsClassfication> list = clsClassfication.getList(id, node, false);

            string res = "[";
            bool isfirst = true;
            foreach (JSONObj cls in list)
            {
                if (isfirst == false) res += ",";
                isfirst = false;
                res += cls.getJson();

            }
            res += "]";
            
            Response.Clear();
            Response.Write(res);
            
        }

        //------------ 여기부터 실제 ITEM처리하기..

        public void Proc_OBJIns()
        {
            string obj_id = Request.Params["OBJECT_ID"];
            string pcode = Request.Params["CATE_PCODE"];
            string id = "";
            object _id = Session["USERID"];
            if (_id != null) id = _id.ToString();


            clsObject obj = new clsObject();

            try
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(HttpContext.Current.Request.InputStream);
                string requestFromPost = reader.ReadToEnd();
                obj.loadFromJsonStr(requestFromPost);
            }
            catch (Exception _e)
            {
            }

            obj.insert(id, pcode);
            Response.Clear();
            string res = "{ 'RESULT' : 'true' }";
            Response.Write(res);

        }
        public void Proc_OBJMod()
        {
            string obj_id = Request.Params["OBJECT_ID"];
            string id = "";
            object _id = Session["USERID"];
            if (_id != null) id = _id.ToString();


            // 권한 체크..
            // ID를 통해서 대상의 OBJ_ID를 읽어오자..
            
            System.IO.StreamReader reader = new System.IO.StreamReader(HttpContext.Current.Request.InputStream);
            string requestFromPost = reader.ReadToEnd();


            clsObject obj = new clsObject();

            bool auth = obj.checkAuth(id, obj_id);
            if (auth == false)
            {
                Response.Clear();
                string r = "{ 'RESULT' : 'false', 'MESSAGE' : 'Access Dined' }";
                Response.Write(r);
                return;
            }


            obj.loadFromJsonStr(requestFromPost);
            string object_id = obj.OBJECT_ID;

            //obj.load(obj_id);
            // load를 따로 할필요가 있을까..?

            // 데이터 로드 하고....
            if (object_id == null || "".Equals(object_id))
            {
                return;
            }

            obj.update( id );

            Response.Clear();
            string res = "{ 'RESULT' : 'true' }";
            Response.Write(res);

        }
        public void Proc_OBJDel()
        {
            string obj_id = Request.Params["OBJECT_ID"];
            string id = "";
            object _id = Session["USERID"];
            if (_id != null) id = _id.ToString();

            clsObject obj = new clsObject();
            obj.load(obj_id);
            obj.delete(id);

            Response.Clear();
            string res = "{ 'RESULT' : 'true' }";
            Response.Write(res);
        }

        public void Proc_GetOBJList()
        {
            string code = Request.Params["clscode"];
            string id = "";
            object _id = Session["USERID"];
            if (_id != null) id = _id.ToString();


            string dept = "";
            object _dept = Session["DEPTCODE"];
            if (_dept != null) dept = _dept.ToString();


            string where = "";

            code += "%";



            if (dept == null) dept = "";

            // TB_ADMINTEAM에 팀코드가 있는 경우 모든 데이터를 보여줘버려..
            where += "AND ( ( OWNER = '" + dept + "' ) OR ( NVL( (SELECT MAX(OBJECT_ID) FROM TB_ADMINTEAM WHERE CODE = '" + dept + "' ), '0' ) > 0 ) ) ";

            List<clsObject> list = clsObject.getList(id, code, where);

            string res = "[";
            bool isfirst = true;
            foreach (JSONObj obj in list)
            {
                if (isfirst == false) res += ",";
                isfirst = false;
                res += obj.getJson();

            }
            res += "]";

            Response.Clear();
            Response.Write(res);

        }

        public void Proc_GetCODEList()
        {
            string code = Request.Params["CODE"];
            clsCode c = new clsCode( "TB_" + code.ToUpper() );
            string res = "[" + c.getJson() + "]";
            Response.Clear();
            Response.Write(res);

        }

        public void Proc_getPerson()
        {
            string code = Request.Params["CODE"];
            string teamcode = Request.Params["TEAMCODE"];
            clsCode c = new clsCode( "TB_" + code.ToUpper(), teamcode );
            string res = "[" + c.getJson() + "]";
            Response.Clear();
            Response.Write(res);

        }
        
    }
}
