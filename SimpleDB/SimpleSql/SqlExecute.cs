using System.Data;
using System.Data.SqlClient;

namespace SimpleSql
{
    public static class SqlExecute
    {
        // <summary>Execute the SqlCommand object and return the results in a DataTable</summary>
        // <param name="Cmd" type="SqlCommand">The SqlCommand object to execute</param>
        // <return>DataTable</return>
        public static DataTable Table(SqlCommand Cmd)
        {
            var dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return dt;
            }
        }

        // <summary>Execute the SqlCommand object and return the results in a DataTable</summary>
        // <param name="Cmd" type="string">The command text to execute</param>
        // <param name="Con" type="SqlConnection">The SqlConnection object to connect to</param>
        // <return>DataTable</return>
        public static DataTable Table(string Cmd, SqlConnection Con)
        {
            var dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(Cmd, Con);
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return dt;
            }
        }

        // <summary>Execute the SqlCommand object and return the results in a DataTable</summary>
        // <param name="Cmd" type="string">The command text to execute</param>
        // <param name="Con" type="string">The connection string text to use</param>
        // <return>DataTable</return>
        public static DataTable Table(string Cmd, string Con)
        {
            var dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(Cmd, Con);
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return dt;
            }
        }

        // <summary>Execute the SqlCommand object and return the results in a DataTable</summary>
        // <param name="Cmd" type="SqlCommand">The SqlCommand object to execute</param>
        // <return>DataTable</return>
        public static DataSet Set(SqlCommand Cmd)
        {
            var ds = new DataSet();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(ds);
                return ds;
            }
            catch
            {
                return ds;
            }
        }

        // <summary>Execute the SqlCommand object and return the results in a DataTable</summary>
        // <param name="Cmd" type="string">The command text to execute</param>
        // <param name="Con" type="SqlConnection">The SqlConnection object to connect to</param>
        // <return>DataTable</return>
        public static DataSet Set(string Cmd, SqlConnection Con)
        {
            var ds = new DataSet();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(Cmd, Con);
                da.Fill(ds);
                return ds;
            }
            catch
            {
                return ds;
            }
        }

        // <summary>Execute the SqlCommand object and return the results in a DataTable</summary>
        // <param name="Cmd" type="string">The command text to execute</param>
        // <param name="Con" type="string">The connection string text to use</param>
        // <return>DataTable</return>
        public static DataSet Set(string Cmd, string Con)
        {
            var ds = new DataSet();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(Cmd, Con);
                da.Fill(ds);
                return ds;
            }
            catch
            {
                return ds;
            }
        }
        
        // <summary>Execute the SqlCommand object and return the results in a DataTable</summary>
        // <param name="Cmd" type="SqlCommand">The SqlCommand object to execute</param>
        // <return>DataTable</return>
        public static bool NonQuery(SqlCommand Cmd)
        {
            try
            {
                Cmd.Connection.Open();
                int i = Cmd.ExecuteNonQuery();
                return (i == 0);
            }
            catch
            {
                return false;
            }
            finally
            {
                Cmd.Connection.Close();
            }
        }

        // <summary>Execute the SqlCommand object and return the results in a DataTable</summary>
        // <param name="Cmd" type="SqlCommand">The commmand text to execute</param>
        // <param name="Con" type="SqlConnection">The SqlConnection object to connect to</param>
        // <return>DataTable</return>
        public static bool NonQuery(string Cmd, SqlConnection Con)
        {
            return NonQuery(new SqlCommand(Cmd, Con));
        }

        // <summary>Execute the SqlCommand object and return the results in a DataTable</summary>
        // <param name="Cmd" type="SqlCommand">The commmand text to execute</param>
        // <param name="Con" type="string">The connection string text to use</param>
        // <return>DataTable</return>
        public static bool NonQuery(string Cmd, string Con)
        {
            return NonQuery(new SqlCommand(Cmd, new SqlConnection(Con)));
        }

        /// <summary>
        /// Runs an ExecuteScalar method on the SqlCommand and returns the value of type T
        /// </summary>
        /// <typeparam name="T">The Type of expected return value</typeparam>
        /// <param name="cmd">The SqlCommand to execute</param>
        /// <returns></returns>
        public static T Scalar<T>(SqlCommand cmd)
        {
            try
            {
                cmd.Connection.Open();
                return (T)cmd.ExecuteScalar();
            }
            finally
            {
                cmd.Connection.Close();
            }
        }
    }
}
