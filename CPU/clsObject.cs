using System;
using System.Collections.Generic;

using System.Text;
using System.Data.OracleClient;
using System.Text.RegularExpressions;
using System.IO;

namespace CPU
{
    public class clsObject : JSONObj
    {

        public clsObject()
        {
            // 일단은 기본값 초기화..
            for (int i = 0; i < COLUMN_NAME.Length; i++)
            {   
                COLUMNS.Add( COLUMN_NAME[i], "" );
            }

            for (int i = 0; i < FIXED_COLUMN_NAME.Length; i++)
            {
                FIXED_COLUMNS.Add(FIXED_COLUMN_NAME[i]);
            }

            
        }
        static string __TABLE_NAME__ = "TB_OBJECT";
        static string __SEQUENCE_NAME__ = "SQ_OBJECT";

        public string OBJECT_ID
        {
            get
            {
                return COLUMNS["OBJECT_ID"];
            }
            set
            {
                COLUMNS["OBJECT_ID"] = value;
            }
        }
        
        string[] COLUMN_NAME = { "OBJECT_ID",
                    "CREATE_DATE","CREATOR_ID","UPDATE_DATE","UPDATOR_ID","MASTER_ID","VERSION","OWNER","STATUS","CATE_PCODE","TITLE","CODE","INPUT_TYPE","INPUT_CYCLE","INPUT_CYCLE2","UNIT","DIVISION",
                    "YEAR","MONTH",
                    "VAL_YEAR_01","VAL_HALF_01","VAL_HALF_02","VAL_QTR_01","VAL_QTR_02","VAL_QTR_03","VAL_QTR_04",
                    "VAL_MONTH_01","VAL_MONTH_02","VAL_MONTH_03","VAL_MONTH_04","VAL_MONTH_05","VAL_MONTH_06",
                    "VAL_MONTH_07","VAL_MONTH_08","VAL_MONTH_09","VAL_MONTH_10","VAL_MONTH_11","VAL_MONTH_12",
                    "VAL_WEEK_01","VAL_WEEK_02","VAL_WEEK_03","VAL_WEEK_04","VAL_WEEK_05",
                    "VAL_DAY_01","VAL_DAY_02","VAL_DAY_03","VAL_DAY_04","VAL_DAY_05","VAL_DAY_06","VAL_DAY_07",
                    "VAL_DAY_08","VAL_DAY_09","VAL_DAY_10","VAL_DAY_11","VAL_DAY_12","VAL_DAY_13","VAL_DAY_14",
                    "VAL_DAY_15","VAL_DAY_16","VAL_DAY_17","VAL_DAY_18","VAL_DAY_19","VAL_DAY_20","VAL_DAY_21",
                    "VAL_DAY_22","VAL_DAY_23","VAL_DAY_24","VAL_DAY_25","VAL_DAY_26","VAL_DAY_27","VAL_DAY_28",
                    "VAL_DAY_29","VAL_DAY_30","VAL_DAY_31",
                    "ATTR1","ATTR2","ATTR3","ATTR4","ATTR5","ATTR6","ATTR7","ATTR8","ATTR9","DESCR" };

        string[] FIXED_COLUMN_NAME = { "OBJECT_ID", "CREATE_DATE", "CREATOR_ID", "UPDATE_DATE", "UPDATOR_ID", "MASTER_ID", "VERSION", "STATUS" };
                                                     
        Dictionary<string, string> COLUMNS = new Dictionary<string, string>();
        List<string> FIXED_COLUMNS = new List<string>();
        

        private string sql_getinfo = "SELECT * FROM " + __TABLE_NAME__ + " WHERE OBJECT_ID = {0}";  // SINGLE SELECT..
        //private string sql_update = "UPDATE " + __TABLE_NAME__ + " SET UPDATE_DATE = TO_CHAR( SYSDATE, 'YYYYMMDDHH24MISS'), UPDATOR_ID = '{1}', CLASSIFICATION_CODE = '{2}', CATE_NAME = '{3}', CATE_ORDER = '{4}', CATE_COMMENT = '{5}',DESCR = '{6}' WHERE OBJECT_ID = '{0}'";
        //private string sql_delete = "UPDATE " + __TABLE_NAME__ + " SET CATE_STATE  = 'D'  WHERE OBJECT_ID = '{0}'";
        //private string sql_insert = "INSERT INTO " + __TABLE_NAME__ + " VALUES ( " + __SEQUENCE_NAME__ + ".nextval, TO_CHAR( SYSDATE, 'YYYYMMDDHH24MISS'), '{0}', TO_CHAR( SYSDATE, 'YYYYMMDDHH24MISS'), '{0}', '{1}', '{2}', '{3}', '{4}', 'S', '{5}' )";

        // 노트..
        // 이제 해줘야 하는것..
        // 1. 조회 Single Obj
        // 2. 목록 조회 List.. -> 트리의 CLSCODE를 받아서 해주는 부분 포함.. 조건 까지 넣어서 검색 하도록..
        // 3. 생성 & Update..
        // 4. 삭제..
        // 5. 이력조회../????? -> 필요한가..?
        // 6. 

        // 그 다음으로 해줘야 하는 것은..
        // TREE와 GRID 붙이기..

        public string getJson()
        {
            // JSON만들어 내기...
            string res = "{";

            for (int i = 0; i < COLUMN_NAME.Length; i++)
            {   
                if( i != 0 ) res += ",";
                res += "'" + COLUMN_NAME[i] + "':'" + COLUMNS[ COLUMN_NAME[i] ] +"'";
            }
            res += "}";

            return res;

        }

        public bool loadFromJsonStr(string jsonstr)
        {
            string str = jsonstr.Replace("{", "").Replace("}", "" ).Replace( "\\\\", "\\" ) ;
            //string[] data = str.Split("\",\"");
            string[] data = Regex.Split(str, ",\"");
            

            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 0; i < data.Length; i++)
            {
                //string[] token = str.Split("\":\"");
                string[] token= Regex.Split( data[i] , "\":");
                dic.Add(token[0].Replace( "\"", "" ) , Decode( token[1].Replace( "\"", "" ) ) );
            }
            // 원래는 json 라이브러리를 사용해야 하지만..
            // 그냥 간단하게 처리를 하자..


            for (int i = 0; i < COLUMN_NAME.Length; i++)
            {
                string key = COLUMN_NAME[i];
                if (dic.ContainsKey(key))
                {
                    string value = dic[key];
                    if (COLUMNS.ContainsKey( key ))
                    {
                        System.Diagnostics.Debug.WriteLine("---------" + COLUMN_NAME[i]);
                        COLUMNS[key] = value;

                    }
                    else
                        COLUMNS.Add(key, value);
                }
            }
            return true;
            

        }
        public string Decode(string value)
        {
            return System.Text.RegularExpressions.Regex.Unescape(value);
            
        }

        public bool loadFromReader(OracleDataReader rd)
        {
            try
            {

                for (int i = 0; i < COLUMN_NAME.Length; i++)
                {
                    Object o = rd[COLUMN_NAME[i]];
                    if (o != null)
                    {
                        if (COLUMNS.ContainsKey(COLUMN_NAME[i] ))
                        {
                            System.Diagnostics.Debug.WriteLine("---------" + COLUMN_NAME[i]);
                            COLUMNS[ COLUMN_NAME[i] ] = o.ToString();

                        }
                        else
                            COLUMNS.Add(COLUMN_NAME[i], o.ToString());
                    }
                }

                COLUMNS["UPDATOR_ID"] = rd["PERSON_NAME"].ToString();
                return true;
            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine("ERROR.. in 98");
                System.Diagnostics.Debug.WriteLine( ex.Message );
                return false;
            }
        }
        public bool load(string id){
            #region 싱글데이터 로드하기
            //this.OBJECT_ID = id;
            OraDB DB = new OraDB();

            OracleDataReader rd = DB.ExecuteQuery(string.Format(sql_getinfo, id));
            try
            {
                if (rd.HasRows)
                {
                    rd.Read();
                    this.loadFromReader(rd);
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

        private bool _update(string person_id)
        {
            // 자아..
            // 업데이트 문법 만들어주기..
            // 이거는 내부 함수이다..
            // 실제 데이터를 넣어 주기 위한 것이고..
            // 고정된 값에 대해서는 업데이트 등을 해주지 않는다..
            // insert 또는 update 를 통해서 초기 값 또는 이력을 만든 이후..
            // 이놈을 통해서 실제 값을 업데이트 하도록 하자..

            // 자아.. 기본적으로는 다음과 같다.. {2} 번에 해당하는 값만 만들어주면 된다..
            string sql_update = "UPDATE " + __TABLE_NAME__ + " SET UPDATE_DATE = TO_CHAR( SYSDATE, 'YYYYMMDDHH24MISS'), UPDATOR_ID = '{1}' {2} WHERE OBJECT_ID = '{0}'";


            string update_str = "";
            for (int i = 0; i < COLUMN_NAME.Length; i++)
            {
                if (FIXED_COLUMNS.Contains(COLUMN_NAME[i])) continue;   // FIXED에 있으면 무시..

                string value = COLUMNS[COLUMN_NAME[i]];

                if (value == "") continue;          
                // "" 인 경우는 무시하자.. null 이 들어 갈수 있을지 모르겠는데..
                // 값을 지우려면 null 을 넣어주어야 할듯..
                 
                if (value == null) value = "";

                update_str += "," + COLUMN_NAME[i] + "='" + COLUMNS[ COLUMN_NAME[i] ] + "'";
                                //res += "'" + COLUMN_NAME[i] + "':'" + COLUMNS[COLUMN_NAME[i]] + "'";
            }

            if ("".Equals(update_str))      // 변경할게 없으면 리턴.. .. 진짜 그런가..??
                return false;           
              

            string sql = string.Format(sql_update, OBJECT_ID, person_id, update_str );
            OraDB DB = new OraDB();
            DB.Execute(sql);
            DB.Close();

            return true;
        }
        public bool delete(string person_id)
        {
            // 자아.. DELETE 해주는 부분..
            // 실제로 삭제는 하지 않고 상태만 바꿔준다..
            // 바꿔줘야 하는 것은 MASTER_ID를 기준으로 동일한 것을 전체 처리..

            if (OBJECT_ID == null || "".Equals(OBJECT_ID))
                return false;

            string sql_delete = "UPDATE " + __TABLE_NAME__ + " SET STATUS = 'D' WHERE MASTER_ID IN ( SELECT MASTER_ID FROM " + __TABLE_NAME__ + " WHERE OBJECT_ID = '{0}' )";
            string sql = string.Format(sql_delete, OBJECT_ID);
            OraDB DB = new OraDB();
            DB.Execute(sql);
            DB.Close();
            return true;
        }
        public bool insert(string person_id, string parent_id)
        {
            //private string sql_insert = "INSERT INTO " + __TABLE_NAME__ + " VALUES ( " + __SEQUENCE_NAME__ + ".nextval, TO_CHAR( SYSDATE, 'YYYYMMDDHH24MISS'), '{0}', TO_CHAR( SYSDATE, 'YYYYMMDDHH24MISS'), '{0}', '{1}', '{2}', '{3}', '{4}', 'S', '{5}' )";

            // insert 를 해주는 것인데.. 
            // 일단은 최소한의 값으로 값을 insert 해주면 된다.. ( row ) 
            // 그 이후 _update 를 호출 하자..
            // 자아.. 일단.. insert, update 시에는 FIXED COLUMN 항목들에 대해서는 별도로 처리를 해줄수 있도록 하자..


            string sql_object_id = "SELECT " + __SEQUENCE_NAME__ + ".NEXTVAL FROM DUAL";
            // 자아.. 사실은.. INSERT 에서 NEXTVAL 을 여러개를 써도 하나로 처리가 된다..
            // 하지만.. INSERT 한 이후에 UPDATE를 해줘야 하는데.. 어차피 OBJECT_ID 를 알아야 하기 때문에..
            // 그냥 따로 빼서 사용하도록 하자..
            // 최초로 만드는 것이기 때문에 OBJECT_ID 와 MASTER_ID 는 동일..
            // VERSION 은 0 으로 설정이 된다..
            // owner에 대해서는 조금 생각을 해줘야 할것 같기는 한데..
            // 일단은.. 나중에 팀코드를 넣어주어야 할것 같기도 하다..
            string sql_insert = "INSERT INTO " + __TABLE_NAME__ + " ( OBJECT_ID, CREATE_DATE, CREATOR_ID, UPDATE_DATE, UPDATOR_ID, MASTER_ID, VERSION, STATUS ) VALUES ( '{1}',TO_CHAR( SYSDATE, 'YYYYMMDDHH24MISS'), '{0}',TO_CHAR( SYSDATE, 'YYYYMMDDHH24MISS'), '{0}', '{1}', 0, 'W' )";

            OraDB DB = new OraDB();

            string object_id = DB.GetQueryResult<string>(sql_object_id, delegate(OracleDataReader reader)
            {
                string val = reader[0].ToString();
                return val;
            });


            if (object_id != null && !"".Equals(object_id))
            {
                // 일단은 신규 object_id 를 가져왔으면..
                string sql = string.Format(sql_insert, person_id, object_id);
                DB.Execute(sql);

                COLUMNS["CATE_PCODE"] = parent_id;
                this.OBJECT_ID = object_id; // 자아.. 일단 값을 넣고..
                // 마지막으로 실제 업데이트..
                _update(person_id);
            }
            DB.Close();
                                                                                                                                                                                    
            return true;
        }
        public bool checkAuth(string person_id, string object_id)
        {
            if( true ) return true;

            string sql = "SELECT NVL ( (SELECT 1 FROM TB_OBJECT WHERE OBJECT_ID = '" + person_id + "' AND ( ATTR3 IS NULL OR ATTR3 = (SELECT USER_ID FROM TB_PERSON WHERE OBJECT_ID = '" + object_id + "' ) ) ), 0 ) AUTH FROM DUAL";

            // 신규 쿼리..
            // ADMIN은 수정이 되어야 해..   
            sql = "SELECT RESULT1 + RESULT2 RESULT FROM ( "
                    + " SELECT NVL ( (SELECT 1 FROM TB_OBJECT WHERE OBJECT_ID = '" + person_id + "' AND ( ATTR3 IS NULL OR ATTR3 = (SELECT USER_ID FROM TB_PERSON WHERE OBJECT_ID = '" + object_id + "') ) ), 0 ) RESULT1, "
                    + " (SELECT NVL( MAX(OBJECT_ID), 0 ) RES FROM TB_ADMINTEAM WHERE CODE IN ( SELECT DEPT_CODE FROM TB_PERSON WHERE OBJECT_ID = '" + person_id + "' ) ) RESULT2 FROM DUAL ) ";

            OraDB DB = new OraDB();

            string result = DB.GetQueryResult<string>(sql, delegate(OracleDataReader reader)
            {
                string val = reader[0].ToString();
                return val;
            });

            if ("0".Equals(result)) 
                return false;
            else
                return true;
        }
        public bool update(string person_id )
        {
            // update.....
            // 자아.. UPDATE를 해주자..
            // UPDATE는 INSERT와 유사하다..
            // 하나의 ROW를 만들어 주고.. ( 물론 이때 MASTER_ID만 동일.. VERSION등이 변경. .)
            // OBJECT_ID를 얻고..
            // 신규로 만들어 진놈에 _update를 호출한다..
            
            string sql_object_id = "SELECT " + __SEQUENCE_NAME__ + ".NEXTVAL FROM DUAL";
            // 자아.. 똑같아.. 일단은 신규 ID를 가져와..


            string column_str = "";
            for (int i = 0; i < COLUMN_NAME.Length; i++)
            {
                string _name = COLUMN_NAME[i];
                if (FIXED_COLUMNS.Contains(_name)) continue;

                column_str += "," + _name;
                
            }

            string sql_update = "INSERT INTO " + __TABLE_NAME__ + " ( OBJECT_ID, CREATE_DATE, CREATOR_ID, UPDATE_DATE, UPDATOR_ID, MASTER_ID, VERSION, STATUS " + column_str + " ) "
                                + " SELECT '{2}' OBJECT_ID, CREATE_DATE, CREATOR_ID, TO_CHAR( SYSDATE, 'YYYYMMDDHH24MISS') UPDATE_DATE , "
                                + " '{0}' UPDATOR_ID, MASTER_ID, VERSION+1 VERSION, STATUS " + column_str  
                                + " FROM " + __TABLE_NAME__ + " "
                                + " WHERE OBJECT_ID = (SELECT MAX(OBJECT_ID) FROM " + __TABLE_NAME__ + " WHERE MASTER_ID IN ( "
                                + " SELECT MASTER_ID FROM " + __TABLE_NAME__ + " WHERE OBJECT_ID = '{1}' ) )";
            // 자아.. SQL설명..
            // 신규로 ROW를 하나 만드는 것이다..
            

          


            OraDB DB = new OraDB();

            string ori_object_id = this.OBJECT_ID;  // 일단 예전것..

            string object_id = DB.GetQueryResult<string>(sql_object_id, delegate(OracleDataReader reader)
            {
                string val = reader[0].ToString();
                return val;
            });


            if (object_id != null && !"".Equals(object_id))
            {
                // 일단은 신규 object_id 를 가져왔으면..
                string sql = string.Format(sql_update, person_id, ori_object_id, object_id);
                DB.Execute(sql);
                this.OBJECT_ID = object_id; // 자아.. 일단 값을 넣고..
                // 마지막으로 실제 업데이트..
                _update(person_id);
            }
            DB.Close();

            return true;
        }
        public static List<clsObject> getList(string person_id, string cls_code , string where )
        {
            #region 목록 데이터 로드하기
            OraDB DB = new OraDB();
            List<clsObject> res = new List<clsObject>();

            if (cls_code == null || "".Equals(cls_code)) cls_code = "%";

            string sql_getlist = "SELECT A.*, B.PERSON_NAME FROM TB_OBJECT A, TB_PERSON B WHERE A.STATUS != 'D' AND A.UPDATOR_ID = B.OBJECT_ID AND A.OBJECT_ID IN ( SELECT MAX( A.OBJECT_ID ) FROM TB_OBJECT A, TB_OBJECT_CLASSIFICATION B "
                    + " WHERE A.CATE_PCODE = B.OBJECT_ID AND B.CLASSIFICATION_CODE LIKE '" + cls_code + "' " + where + " GROUP BY MASTER_ID ) ORDER BY CODE, TITLE";


            OracleDataReader rd = DB.ExecuteQuery(string.Format(sql_getlist, ""));
            try
            {
                if (rd.HasRows)
                {
                    while (rd.Read())
                    {
                        clsObject obj = new clsObject();
                        // 실제 데이터 읽기..
                        obj.loadFromReader(rd);
                        res.Add(obj);
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
