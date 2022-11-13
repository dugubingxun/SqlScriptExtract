using System;
using System.Data;
using System.Configuration;
using System.Web;

using System.Data.OleDb;
using System.Collections;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;
using System.Data.Common;

namespace SqlScriptExtract
{
    public class DBReadOrWrite
    {
        private String _connstr;
        private String _dbType;
        private Boolean  _dbState;
        private String _StrError="";
        /// <summary>
        ///  数据类型
        /// </summary>
        public String dbType
        {
            get
            {
                return _dbType;
            }
            set
            {
                _dbType = value;
            }
        }
        /// <summary>
        ///  连接字符串
        /// </summary>
        public String connstr
        {
            get
            {
                return _connstr;
            }
            set
            {
                _connstr = value;
            }
        }
        /// <summary>
        ///  连接状态
        /// </summary>
        public Boolean dbState
        {
            get
            {
                return _dbState;
            }
            set
            {
                _dbState = value;
            }
        }
        /// <summary>
        ///  错误信息
        /// </summary>
        public String StrError
        {
            get
            {
                return _StrError;
            }
            set
            {
                _StrError = value;
            }
        }
    
        public DBReadOrWrite()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }
        private DbConnection conn;
        private DbConnection getConn()
        {
            try
            {
                if (conn == null)
                {
                    if (_dbType == "MySql")
                    {
                        conn = new MySqlConnection(_connstr);
                    }
                    else if (_dbType == "Oracle")
                    {
                        //
                        //conn = new OracleConnection(_connstr);
                        conn = new OleDbConnection(_connstr);
                    }
                    else if (_dbType == "SqlServer")
                    {
                        conn = new SqlConnection(_connstr);
                    }
                    conn.Open();

                }
                //else
                //{
                if (conn.State == ConnectionState.Open)
                {
                    _dbState = true;
                }
                else
                {
                    _dbState = false;
                }
                return conn;
            }
            catch (Exception ex)
            {
                _StrError = ex.Message;
                _dbState = false;
                return conn;
            }
        }
        public void BeginConn()
        {
            getConn();
        }
        public void EndConn()
        {
            if (conn != null)
            {
                conn.Close();
                conn = null;
            }
        }

        public ArrayList Select(string sql)
        {
            try
            {
                return Select(sql, null);
            }
            catch (Exception ex)
            {
                _StrError = ex.Message;
                throw;
            }
        }
        public ArrayList Select(string sql, Hashtable args)
        {
            try
            {
                DataTable data = new DataTable();

                bool isConn = conn != null;

                DbConnection con = getConn();

                if (_dbType == "MySql")
                {
                    MySqlCommand cmd = new MySqlCommand(sql, (MySqlConnection)con);
                    cmd.CommandTimeout = 10000;
                    if (args != null) SetArgs(sql, args, cmd);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    //MySqlDataAdapter adapter = new MySqlDataAdapter(sql, _connstr);
                    adapter.Fill(data);
                }
                else if (_dbType == "Oracle")
                {
                    OleDbCommand cmd = new OleDbCommand(sql, (OleDbConnection)con);
                    if (args != null) SetArgs(sql, args, cmd);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    //OleDbDataAdapter adapter = new OleDbDataAdapter(sql, _connstr);
                    adapter.Fill(data);
                }
                else if (_dbType == "SqlServer")
                {
                    SqlCommand cmd = new SqlCommand(sql, (SqlConnection)con);
                    cmd.CommandTimeout = 10000;
                    if (args != null) SetArgs(sql, args, cmd);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    //SqlDataAdapter adapter = new SqlDataAdapter(sql, _connstr);
                    adapter.Fill(data);
                }

                if (isConn == false)
                {
                    EndConn();
                }

                return DataTable2ArrayList(data);
            }
            catch (Exception ex)
            {
                _StrError = ex.Message;
                throw;
            }
        }

        public DataTable SelectDataTable(string sql, Hashtable args)
        {
            try
            {
                DataTable data = new DataTable();

                bool isConn = conn != null;

                DbConnection con = getConn();

                if (_dbType == "MySql")
                {
                    MySqlCommand cmd = new MySqlCommand(sql, (MySqlConnection)con);
                    cmd.CommandTimeout = 10000;
                    if (args != null) SetArgs(sql, args, cmd);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    //MySqlDataAdapter adapter = new MySqlDataAdapter(sql, _connstr);
                    adapter.Fill(data);
                }
                else if (_dbType == "Oracle")
                {
                    OleDbCommand cmd = new OleDbCommand(sql, (OleDbConnection)con);
                    if (args != null) SetArgs(sql, args, cmd);
                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    //OleDbDataAdapter adapter = new OleDbDataAdapter(sql, _connstr);
                    adapter.Fill(data);
                }
                else if (_dbType == "SqlServer")
                {
                    SqlCommand cmd = new SqlCommand(sql, (SqlConnection)con);
                    cmd.CommandTimeout = 10000;
                    if (args != null) SetArgs(sql, args, cmd);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    //SqlDataAdapter adapter = new SqlDataAdapter(sql, _connstr);
                    adapter.Fill(data);
                }

                if (isConn == false)
                {
                    EndConn();
                }

                return data;
            }
            catch (Exception ex)
            {
                _StrError = ex.Message;
                throw;
            }
        }
        public void Execute(string sql)
        {
            try
            {
                Execute(sql, null);
            }
            catch (Exception ex)
            {
                _StrError = ex.Message;
                throw;
            }
        }
        public void Execute(string sql, Hashtable args)
        {
            try
            {
                bool isConn = conn != null;
                DbConnection con = getConn();

                if (_dbType == "MySql")
                {
                    MySqlCommand cmd = new MySqlCommand(sql, (MySqlConnection)con);
                    if (args != null) SetArgs(sql, args, cmd);
                    cmd.ExecuteNonQuery();
                }
                else if (_dbType == "Oracle")
                {
                    OleDbCommand cmd = new OleDbCommand(sql, (OleDbConnection)con);
                    if (args != null) SetArgs(sql, args, cmd);
                    cmd.ExecuteNonQuery();
                }
                else if (_dbType == "SqlServer")
                {
                    SqlCommand cmd = new SqlCommand(sql, (SqlConnection)con);
                    cmd.CommandTimeout = 10000;
                    if (args != null) SetArgs(sql, args, cmd);
                    cmd.ExecuteNonQuery();
                }

                if (isConn == false)
                {
                    EndConn();
                }
            }
            catch (Exception ex)
            {
                _StrError = ex.Message;
                throw;
            }
        }
     
        #region 私有
        private void SetArgs(string sql, Hashtable args, IDbCommand cmd)
        {
            int k = 0;
            string[] strp;
            if (_dbType == "MySql")
            {
                MatchCollection ms = Regex.Matches(sql, @"@\w+");
                strp = new String[ms.Count];
                foreach (Match m in ms)
                {
                    string key = m.Value;
                    int id = Array.IndexOf(strp, key);
                    if (id == -1)
                    {
                        strp[k] = key;
                        string newKey = "?" + key.Substring(1);
                        sql = sql.Replace(key, newKey);

                        Object value = args[key];
                        if (value == null)
                        {
                            value = args[key.Substring(1)];
                        }

                        cmd.Parameters.Add(new MySqlParameter(newKey, value));
                    }
                    k++;
                }
                cmd.CommandText = sql;
            }
            else if (_dbType == "Oracle")
            {
                MatchCollection ms = Regex.Matches(sql, @"@\w+");
                int i = 1;
                strp = new String[ms.Count];
                foreach (Match m in ms)
                {
                    string key = m.Value;
                    int id = Array.IndexOf(strp, key);
                    if (id == -1)
                    {
                        strp[k] = key;
                        string newKey = "@P" + i++;
                        sql = sql.Replace(key, "?");

                        Object value = args[key];
                        if (value == null)
                        {
                            value = args[key.Substring(1)];
                        }

                        cmd.Parameters.Add(new OleDbParameter(newKey, value));
                    }
                    k++;
                }
                cmd.CommandText = sql;
            }
            else if (_dbType == "SqlServer")
            {
                MatchCollection ms = Regex.Matches(sql, @"@\w+");
                //int i = 1;
                strp = new String[ms.Count];
                foreach (Match m in ms)
                {
                    string key = m.Value;
                    int id = Array.IndexOf(strp, key);
                    if (id == -1)
                    {
                        strp[k] = key;
                        Object value = args[key];
                        if (value == null)
                        {
                            value = args[key.Substring(1)];
                        }
                        if (value == null) value = DBNull.Value;

                        cmd.Parameters.Add(new SqlParameter(key, value));
                    }
                    k++;
                }
                cmd.CommandText = sql;
            }

            //if (_dbType == "MySql")
            //{
            //    MatchCollection ms = Regex.Matches(sql, @"@\w+");
            //    foreach (Match m in ms)
            //    {
            //        string key = m.Value;
            //        string newKey = "?" + key.Substring(1);
            //        sql = sql.Replace(key, newKey);

            //        Object value = args[key];
            //        if (value == null)
            //        {
            //            value = args[key.Substring(1)];
            //        }

            //        cmd.Parameters.Add(new MySqlParameter(newKey, value));
            //    }
            //    cmd.CommandText = sql;
            //}
            //else if (_dbType == "Oracle")
            //{
            //    MatchCollection ms = Regex.Matches(sql, @"@\w+");
            //    int i = 1;
            //    foreach (Match m in ms)
            //    {
            //        string key = m.Value;
            //        string newKey = "@P" + i++;
            //        sql = sql.Replace(key, "?");

            //        Object value = args[key];
            //        if (value == null)
            //        {
            //            value = args[key.Substring(1)];
            //        }

            //        cmd.Parameters.Add(new OleDbParameter(newKey, value));
            //    }
            //    cmd.CommandText = sql;
            //}
            //else if (_dbType == "SqlServer")
            //{
            //    MatchCollection ms = Regex.Matches(sql, @"@\w+");
            //    int i = 1;
            //    foreach (Match m in ms)
            //    {
            //        string key = m.Value;

            //        Object value = args[key];
            //        if (value == null)
            //        {
            //            value = args[key.Substring(1)];
            //        }
            //        if (value == null) value = DBNull.Value;

            //        cmd.Parameters.Add(new SqlParameter(key, value));
            //    }
            //    cmd.CommandText = sql;
            //}
        }
        private ArrayList DataTable2ArrayList(DataTable data)
        {
            ArrayList array = new ArrayList();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                DataRow row = data.Rows[i];

                Hashtable record = new Hashtable();
                for (int j = 0; j < data.Columns.Count; j++)
                {
                    object cellValue = row[j];
                    if (cellValue.GetType() == typeof(DBNull))
                    {
                        cellValue = null;
                    }
                    record[data.Columns[j].ColumnName.ToLower()] = cellValue;
                }
                array.Add(record);
            }
            return array;
        }
        #endregion
    }
}

