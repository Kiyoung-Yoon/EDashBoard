using System;
using System.Collections.Generic;

using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.Data.OracleClient;


namespace CPU
{
    /*
 *  자아.. SQL 샘플 먹어라.
        OraDB DB= new OraDB();
        DB.Connect();
        List<String> data = null;
        data = DB.GetQueryResults<string>( "select * from ps32", delegate( OracleDataReader reader ) 
        {
            string val = reader[0].ToString();
            return val;
        });

 */

    public class OraDB
    {
        OracleConnection con = null;
        OracleCommand cmd = null;
        OracleDataReader reader = null;
        string ORA_CONNECT = "server=ENOVIA_REAL; uid=edashboard; pwd=edashboard#1";
        //string ORA_CONNECT = "server=ENOVIA_REAL; uid=enovia; pwd=enovia6";
        //string ORA_CONNECT = "server=ENOVIA; uid=edashboard; pwd=edashboard#1";
        string SQL = "";

        public OraDB()
        {
        }

        public OraDB(string TNSNAME, string uid, string pwd)
        {
            ORA_CONNECT = string.Format("server={0}; uid={1}; pwd={2}");
        }

        public void Connect(string TNSNAME, string uid, string pwd)
        {
            ORA_CONNECT = string.Format("server={0}; uid={1}; pwd={2}");
        }

        public bool Connect()
        {
            try
            {
                con = new OracleConnection(ORA_CONNECT);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }
        public void Close()
        {
            con.Close();
        }
        public void Open()
        {
            con.Open();
        } 
        public OracleDataReader ExecuteQuery(string sql)
        {
            SQL = sql;
            if (con == null)
            {
                if (con == null)
                    Connect();
                if( con == null )
                    return null;
            }

            try
            {
                con.Open();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                if (con.State != ConnectionState.Open)
                    return null;
            }


            if (con.State != ConnectionState.Open)
            {

                return null;
            }

            cmd = new OracleCommand(SQL, con);
            reader = cmd.ExecuteReader();

            return reader;
        }

        public int Execute(string sql)
        {
            SQL = sql;
            if (con == null)
                Connect();

            if (con == null)
            {
                
                System.Diagnostics.Debug.WriteLine("Try Connect first..");
                return -1;
            }
            try
            {
                con.Open();
            }
            catch
            {
                if (con.State != ConnectionState.Open)
                    return -1;
            }
            if (con.State != ConnectionState.Open)
            {
                System.Diagnostics.Debug.WriteLine("Connection State is " + con.State.ToString());
                return -1;
            }

            //SqlTransaction tr = con.BeginTransaction();
            cmd = new OracleCommand(SQL, con);
            //cmd.Transaction = tr;
            int rownum = cmd.ExecuteNonQuery();
            //tr.Rollback();
            return rownum;
        }

        public delegate void mapper_2nd(OracleDataReader rs);

        public bool GetQueryResults(string sql, mapper_2nd mapper)
        {
            Connect();
            OracleDataReader rs = null;
            try
            {
                rs = ExecuteQuery(sql);
                while (rs.Read())
                {
                    mapper(rs);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (rs != null)
                    rs.Close();
                Close();
            }
            return true;
        }

        public delegate T mapper<T>(OracleDataReader rs);

        public List<T> GetQueryResults<T>(string sql, mapper<T> mapper)
        {
            Connect();
            OracleDataReader rs = null;


            List<T> L = new List<T>();
            try
            {
                rs = ExecuteQuery(sql);
                while (rs.Read())
                {
                    L.Add(mapper(rs));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(sql);
                throw ex;
            }
            finally
            {
                if (rs != null)
                    rs.Close();
                Close();
            }
            return L;
        }

        public T GetQueryResult<T>(string sql, mapper<T> A)
        {
            List<T> L = GetQueryResults<T>(sql, A);
            if (L.Count == 0)
                return default(T);
            return L[0];
        }

        public bool GetQuery(string sql)
        {
            if (con == null)
                Connect();
            try
            {
                Execute(sql);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
            return true;
        }

    }
}
