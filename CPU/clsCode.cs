using System;
using System.Collections.Generic;

using System.Text;
using System.Data.OracleClient;
using System.Text.RegularExpressions;
using System.IO;

namespace CPU
{
    public class clsCode : JSONObj
    {
        string __TABLE_NAME__ = "";
        string __PARAM1__     = "";

        public clsCode( string table )
        {
            __TABLE_NAME__ = table;
        }
        public clsCode(string table, string param)
        {
            __TABLE_NAME__ = table;
            __PARAM1__ = param;
        }

        public string getJson()
        {

            OraDB DB = new OraDB();
            string res = null;

            string sql = "select OBJECT_ID, CODE, NAME from " + __TABLE_NAME__ + " order by code";

            if ("TB_DEPT".Equals(__TABLE_NAME__))
            {
                // 부서 가져올때는 별도로..
                sql = "select OBJECT_ID, DEPT_CODE, DEPT_NAME from TB_DEPT order by DEPT_NAME"; 
            }


            if ("TB_PERSON".Equals(__TABLE_NAME__))
            {
                sql = "SELECT OBJECT_ID, USER_ID, PERSON_NAME || ' ' || POS_NAME AS NAME, DEPT_CODE FROM TB_PERSON WHERE STATUS = 'Y'";

                

                string _object_id = DB.GetQueryResult<string>(sql, delegate(OracleDataReader reader)
                {
                    string o = reader[0].ToString();
                    string code = reader[1].ToString();
                    string name = reader[2].ToString();
                    string dept = reader[3].ToString();

                    if (res == null) res = "";
                    else res += ",";

                    res += "{'value':'" + code + "','view':'" + name + "','filter':'" + dept + "' }";

                    return o;
                });

                return res;

            }

            string object_id = DB.GetQueryResult<string>(sql , delegate(OracleDataReader reader)
            {
                string o = reader[0].ToString();
                string code = reader[1].ToString();
                string name = reader[2].ToString();

                if( res == null ) res = "";
                else res += ",";

                res += "{'value':'" + code + "','view':'" + name + "' }";

                return o;
            });

            return res;

        }
    }
}
