using System;
using System.Collections.Generic;

using System.Text;
using System.Data.OracleClient;

namespace CPU
{
    public class clsClassfication : JSONObj
    {
        // 조금더 세련되게 하려면.. 각각을 개별로 구분하고.. 시리얼라이즈를 통해서..
        // 어쩌구 저쩌구 하면 될텐데..
        // 그러고 싶지 않아..

        public clsClassfication()
        {
        }
        static string __TABLE_NAME__ = "TB_OBJECT_CLASSIFICATION";
        static string __SEQUENCE_NAME__ = "SQ_OBJECT_CLASSIFICATION";

        private string sql_getinfo= "SELECT * FROM " + __TABLE_NAME__ + " WHERE OBJECT_ID = {0}";
        private string sql_update = "UPDATE " + __TABLE_NAME__ + " SET UPDATE_DATE = TO_CHAR( SYSDATE, 'YYYYMMDDHH24MISS'), UPDATOR_ID = '{1}', CLASSIFICATION_CODE = '{2}', CATE_NAME = '{3}', CATE_ORDER = '{4}', CATE_COMMENT = '{5}',DESCR = '{6}' WHERE OBJECT_ID = '{0}'";
        private string sql_delete = "UPDATE " + __TABLE_NAME__ + " SET CATE_STATE  = 'D'  WHERE OBJECT_ID = '{0}'";
        private string sql_insert = "INSERT INTO " + __TABLE_NAME__ + " VALUES ( " + __SEQUENCE_NAME__ + ".nextval, TO_CHAR( SYSDATE, 'YYYYMMDDHH24MISS'), '{0}', TO_CHAR( SYSDATE, 'YYYYMMDDHH24MISS'), '{0}', '{1}', '{2}', '{3}', '{4}', 'S', '{5}' )";

        
        //string sql_getinfo = "SELECT * FROM TAB";
        
        public string OBJECT_ID = "";
        public string CREATE_DATE = "";
        public string CREATOR_ID = "";
        public string UPDATE_DATE = "";
        public string UPDATOR_ID = "";
        public string CLASSIFICATION_CODE = "";
        public string CATE_NAME = "";
        public string CATE_ORDER = "";
        public string CATE_COMMENT = "";
        public string CATE_STATE = "";
        public string DESCR = "";


        public string getJson()
        {
            string res = "{'id':'" + CLASSIFICATION_CODE + "',"
                + "'text':'" + CATE_NAME + "',"
                + "'cls':'folder',"
                + "'OBJECT_ID':'" + OBJECT_ID + "',"
                + "'CREATE_DATE':'" + CREATE_DATE + "',"
                + "'CREATOR_ID':'" + CREATOR_ID + "',"
                + "'UPDATE_DATE':'" + UPDATE_DATE + "',"
                + "'UPDATOR_ID':'" + UPDATOR_ID + "',"
                + "'CLASSIFICATION_CODE':'" + CLASSIFICATION_CODE + "',"
                + "'CATE_NAME':'" + CATE_NAME + "',"
                + "'CATE_ORDER':'" + CATE_ORDER + "',"
                + "'CATE_COMMENT':'" + CATE_COMMENT + "',"
                + "'CATE_STATE':'" + CATE_STATE + "',"
                + "'DESCR':'" + DESCR + "' }";

            return res;
                
        }
        
        public bool load(string id)
        {
            #region 싱글데이터 로드하기
            this.OBJECT_ID = id;
            OraDB DB = new OraDB();

            OracleDataReader rd =  DB.ExecuteQuery(string.Format(sql_getinfo, id));
            try
            {
                if (rd.HasRows)
                {
                    rd.Read();
                    OBJECT_ID   =rd["OBJECT_ID"].ToString();
                    CREATE_DATE = rd["CREATE_DATE"].ToString();
                    CREATOR_ID = rd["CREATOR_ID"].ToString();
                    UPDATE_DATE = rd["UPDATE_DATE"].ToString();
                    UPDATOR_ID = rd["UPDATOR_ID"].ToString();
                    CLASSIFICATION_CODE = rd["CLASSIFICATION_CODE"].ToString();
                    CATE_NAME = rd["CATE_NAME"].ToString();
                    CATE_ORDER = rd["CATE_ORDER"].ToString();
                    CATE_COMMENT = rd["CATE_COMMENT"].ToString();
                    CATE_STATE = rd["CATE_STATE"].ToString();
                    DESCR = rd["DESCR"].ToString();
                }
            }
            finally
            {
                if (rd != null) rd.Close();
                rd = null;
                DB.Close();
            }
            #endregion
            return true;
        }

        public bool update( string person_id )
        {

            string sql = string.Format(sql_update, OBJECT_ID, person_id, CLASSIFICATION_CODE, CATE_NAME, CATE_ORDER, CATE_COMMENT, DESCR);
            OraDB DB = new OraDB();
            DB.Execute(sql);
            DB.Close();

            return true;
        }

        public bool delete(string person_id)
        {
            if (OBJECT_ID == null || "".Equals(OBJECT_ID))
                return false;

            string sql = string.Format(sql_delete, OBJECT_ID);
            OraDB DB = new OraDB();
            DB.Execute(sql);
            DB.Close();
            return true;
        }

        public bool insert(string person_id, string parent_id )
        {
            // 자아.. INSERT..
            // 그나마 복잡한게 INSERT라고 보면 된다..
            // 우선 CLS코드를 직접 구해줘야만 하고..
            // 그에 맞는 ORDER도 만들어주어야 한다..
            // 그외에는 머.. 별겨 없다..

            if (parent_id == null || "".Equals(parent_id))
                parent_id = "0";

            OraDB DB = new OraDB();
            try
            {
                string sql_getCls = "SELECT PARENTCODE || CHILDCODE RES, CHILDCODE FROM ( SELECT (SELECT CLASSIFICATION_CODE CLASSIFICATION_CODE FROM TB_OBJECT_CLASSIFICATION WHERE OBJECT_ID = {0} ) PARENTCODE, CHILDCODE FROM  (SELECT SUBSTR( CHILDCODE, LENGTH(CHILDCODE)-4 ) CHILDCODE FROM (SELECT MAX( B.CLASSIFICATION_CODE ) PARENTCODE, '0000000' || ( NVL( SUBSTR( MAX(A.CLASSIFICATION_CODE), LENGTH( NVL( MAX( B.CLASSIFICATION_CODE ), 0 )  ) + 1 ), 0 ) + 1 ) CHILDCODE FROM TB_OBJECT_CLASSIFICATION A, (SELECT MAX(CLASSIFICATION_CODE) CLASSIFICATION_CODE, COUNT(*) CNT FROM TB_OBJECT_CLASSIFICATION WHERE OBJECT_ID = {0} ) B WHERE A.CLASSIFICATION_CODE LIKE B.CLASSIFICATION_CODE || '_____' ) ))"; 
                OracleDataReader rd = DB.ExecuteQuery(String.Format(sql_getCls, parent_id));
                try
                {
                    if (rd.HasRows)
                    {
                        rd.Read();
                        CLASSIFICATION_CODE = rd["RES"].ToString();
                        CATE_ORDER = rd["CHILDCODE"].ToString();
                    }
                    else
                    {
                        return false;
                    }
                }
                finally
                {
                    if (rd != null) rd.Close();
                    rd = null;
                }
                string sql = string.Format(sql_insert, person_id, CLASSIFICATION_CODE, CATE_NAME, CATE_ORDER, CATE_COMMENT, DESCR);
                DB.Execute(sql);
            }
            finally
            {
                DB.Close();
            }
            return true;
        }

        public static List<clsClassfication> getList(string person_id, string cls_code, bool expandAll )
        {
            #region 싱글데이터 로드하기
            OraDB DB = new OraDB();
            List<clsClassfication> res = new List<clsClassfication>();

            string likecondition = "_____";

            if( expandAll == true ) likecondition = "%";

            string sql_getlist = "SELECT * FROM " + __TABLE_NAME__ + " WHERE CATE_STATE != 'D' AND CLASSIFICATION_CODE LIKE '{0}" + likecondition + "' ORDER BY NVL( SUBSTR( CLASSIFICATION_CODE, 0, LENGTH( CLASSIFICATION_CODE ) -5 ), CLASSIFICATION_CODE ) , CATE_ORDER";

            OracleDataReader rd = DB.ExecuteQuery(string.Format(sql_getlist, cls_code));
            try
            {
                if(rd.HasRows)
                {
                    while (rd.Read())
                    {
                        clsClassfication cls = new clsClassfication();
                        cls.OBJECT_ID = rd["OBJECT_ID"].ToString();
                        cls.CREATE_DATE = rd["CREATE_DATE"].ToString();
                        cls.CREATOR_ID = rd["CREATOR_ID"].ToString();
                        cls.UPDATE_DATE = rd["UPDATE_DATE"].ToString();
                        cls.UPDATOR_ID = rd["UPDATOR_ID"].ToString();
                        cls.CLASSIFICATION_CODE = rd["CLASSIFICATION_CODE"].ToString();
                        cls.CATE_NAME = rd["CATE_NAME"].ToString();
                        cls.CATE_ORDER = rd["CATE_ORDER"].ToString();
                        cls.CATE_COMMENT = rd["CATE_COMMENT"].ToString();
                        cls.CATE_STATE = rd["CATE_STATE"].ToString();
                        cls.DESCR = rd["DESCR"].ToString();

                        res.Add(cls);
                    }
                }
            }
            finally
            {
                if (rd != null) rd.Close();
                rd = null;
                DB.Close();
            }
            #endregion
            return res;
        }
    }
}
