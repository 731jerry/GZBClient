using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Data;

namespace GZBClient
{
    class DatabaseConnector
    {
        //连接用的字符串  
        private static String OnlineConnStr;

        private DatabaseConnector()
        {
        }

        //单例  
        private static DatabaseConnector _instance = null;
        public static DatabaseConnector Connector
        {
            get
            {
                OnlineConnStr = @"server=121.42.154.95; user id=vivid; password=vivid; database=vivid;Charset=utf8;";
                if (_instance == null)
                {
                    _instance = new DatabaseConnector();
                }
                return _instance;
            }
        }

        #region 联网

        public void UserLogin(String acc, String psw)
        {
            using (MySqlConnection con = new MySqlConnection(OnlineConnStr))
            {
                String hash = UniversalFunctions.GetInstence().GetMd5Hash(MD5.Create(), psw);

                StringBuilder sbSQL = new StringBuilder(
                        @"SELECT Count(id),id,userid,password,companyNickname,workloads,company,companyowner,address,bankname,bankcard,phone,fax,QQ,email,cast(GZB_addtime as char) as GZB_addtime,GZB_degree,GZB_expiretime,GZB_isonline,notification,companyBalance,GZB_signature FROM users WHERE userid = '");
                sbSQL.Append(acc);
                sbSQL.Append(@"'");
                sbSQL.Append(@" AND password = '");
                sbSQL.Append(hash.ToLower());
                sbSQL.Append(@"'");

                String SQLforGeneral = sbSQL.ToString();

                using (MySqlCommand cmd = new MySqlCommand(SQLforGeneral, con))
                {
                    con.Open();
                    MySqlDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        /*
                        MainWindow.IS_PASSWORD_CORRECT = (int.Parse((dataReader["Count(id)"].ToString() == "") ? "0" : dataReader["Count(id)"].ToString()) == 1) ? true : false;
                        if (MainWindow.IS_PASSWORD_CORRECT)
                        {
                            MainWindow.ID = dataReader["id"].ToString();
                            MainWindow.USER_ID = dataReader["userid"].ToString();
                            MainWindow.PASSWORD_HASH = dataReader["password"].ToString();
                            MainWindow.COMPANY_NICKNAME = dataReader["companyNickname"].ToString();
                            MainWindow.WORKLOADS = dataReader["workloads"].ToString();
                            MainWindow.COMPANY_NAME = dataReader["company"].ToString();
                            MainWindow.COMPANY_OWNER = dataReader["companyowner"].ToString();
                            MainWindow.ADDRESS = dataReader["address"].ToString();
                            MainWindow.BANK_NAME = dataReader["bankname"].ToString();
                            MainWindow.BANK_CARD = dataReader["bankcard"].ToString();
                            MainWindow.PHONE = dataReader["phone"].ToString();
                            MainWindow.FAX = dataReader["fax"].ToString();
                            MainWindow.QQ = dataReader["QQ"].ToString();
                            MainWindow.EMAIL = dataReader["email"].ToString();
                            MainWindow.NOTIFICATION = dataReader["notification"].ToString();
                            MainWindow.IS_USER_ONLINE = (int.Parse(dataReader["GZB_isonline"].ToString().Equals("") ? "0" : dataReader["GZB_isonline"].ToString()) == 0) ? false : true;
                            MainWindow.DEGREE = int.Parse(dataReader["GZB_degree"].ToString());
                            MainWindow.ADDTIME = DateTime.Parse(dataReader["GZB_addtime"].ToString());
                            MainWindow.EXPIRETIME = DateTime.Parse(dataReader["GZB_expiretime"].ToString());
                            MainWindow.COMPANY_BALANCE = float.Parse(dataReader["companyBalance"].ToString());
                            MainWindow.SIGNATURE = dataReader["GZB_signature"].ToString();
                            //MainWindow.LAST_LOGON_TIME = dataReader["lastLogonTime"].ToString().Equals("") ? "首次登录" : dataReader["lastLogonTime"].ToString();
                        }
                         */ 
                    }
                    dataReader.Close();
                }
            }
        }

        // 插入数据
        public int OnlineInsertData(String table, String query, String value)
        {
            int affectedRows = -1;
            using (MySqlConnection con = new MySqlConnection(OnlineConnStr))
            {
                String SQLforGeneral = "INSERT INTO " + table + " (" + query + ") VALUES(" + value + ")";
                using (MySqlCommand cmdInsert = new MySqlCommand(SQLforGeneral, con))
                {
                    con.Open();
                    affectedRows = cmdInsert.ExecuteNonQuery();
                }
            }
            return affectedRows;
        }

        // 修改数据
        public int OnlineUpdateData(String table, String[] query, String[] value, String id)
        {
            int affectedRows = -1;
            using (MySqlConnection con = new MySqlConnection(OnlineConnStr))
            {
                String innerSQL = "";

                for (int i = 0; i < query.Length; i++)
                {
                    innerSQL += query[i] + " = '" + value[i] + "',";
                }
                if (!innerSQL.Equals(""))
                {
                    innerSQL = innerSQL.Substring(0, innerSQL.Length - 1); // 去掉最后的逗号
                }
                String SQLforGeneral = "UPDATE " + table + " SET " + innerSQL + " WHERE id = '" + id + "'";
                using (MySqlCommand cmdInsert = new MySqlCommand(SQLforGeneral, con))
                {
                    con.Open();
                    affectedRows = cmdInsert.ExecuteNonQuery();
                }
            }
            return affectedRows;
        }

        // 修改原始数据
        public int OnlineUpdateDataFromOriginalSQL(String sql)
        {
            int affectedRows = -1;
            using (MySqlConnection con = new MySqlConnection(OnlineConnStr))
            {
                using (MySqlCommand cmdInsert = new MySqlCommand(sql, con))
                {
                    cmdInsert.CommandTimeout = 0;
                    con.Open();
                    affectedRows = cmdInsert.ExecuteNonQuery();
                }
            }
            return affectedRows;
        }

        public List<String> OnlineGetOneRowDataById(String table, List<String> query, String baseName, String id)
        {
            List<String> resultsStringList;
            using (MySqlConnection con = new MySqlConnection(OnlineConnStr))
            {
                // ORDER BY id ASC
                String innerSQL = "";

                for (int i = 0; i < query.Count; i++)
                {
                    innerSQL += query[i] + ",";
                }
                if (!innerSQL.Equals(""))
                {
                    innerSQL = innerSQL.Substring(0, innerSQL.Length - 1); // 去掉最后的逗号
                }
                String sql = "SELECT " + innerSQL + " FROM " + table + " WHERE " + baseName + "='" + id + "'";//建表语句  
                using (MySqlCommand cmdCreateTable = new MySqlCommand(sql, con))
                {
                    cmdCreateTable.CommandText = sql;
                    con.Open();
                    MySqlDataReader dataReader = cmdCreateTable.ExecuteReader();
                    //String[] resultsStringArray = new String[query.Count];
                    resultsStringList = new List<String>();

                    while (dataReader.Read())
                    {
                        for (int i = 0; i < query.Count; i++)
                        {
                            resultsStringList.Add(dataReader[query[i]].ToString());
                        }
                    }
                    dataReader.Close();
                }
            }
            return resultsStringList;
        }

        public List<List<String>> OnlineGetRowsDataById(String table, List<String> query, String baseName, String id, String condition)
        {
            List<List<String>> resultsStringList;
            using (MySqlConnection con = new MySqlConnection(OnlineConnStr))
            {
                // ORDER BY id ASC
                String innerSQL = "";

                /*
                for (int i = 0; i < query.Count; i++)
                {
                    innerSQL += query[i] + ",";
                }
                if (!innerSQL.Equals(""))
                {
                    innerSQL = innerSQL.Substring(0, innerSQL.Length - 1); // 去掉最后的逗号
                }
                 */
                innerSQL = String.Join(",", query);

                String sql = "SELECT " + innerSQL + " FROM " + table + " WHERE " + baseName + "='" + id + "' " + condition;//建表语句  
                using (MySqlCommand cmdCreateTable = new MySqlCommand(sql, con))
                {
                    cmdCreateTable.CommandTimeout = 0;
                    cmdCreateTable.CommandText = sql;
                    con.Open();
                    MySqlDataReader dataReader = cmdCreateTable.ExecuteReader();
                    //String[] resultsStringArray = new String[query.Count];
                    resultsStringList = new List<List<String>>();

                    while (dataReader.Read())
                    {
                        List<String> temp = new List<String>();
                        for (int i = 0; i < query.Count; i++)
                        {
                            temp.Add(dataReader[query[i]].ToString());
                        }
                        resultsStringList.Add(temp);
                    }
                    dataReader.Close();
                }
            }
            return resultsStringList;
        }

        public List<List<String>> OnlineGetRowsDataByCondition(String table, List<String> query, String condition)
        {
            List<List<String>> resultsStringList;
            using (MySqlConnection con = new MySqlConnection(OnlineConnStr))
            {
                // ORDER BY id ASC
                String innerSQL = String.Join(",", query);

                String sql = "SELECT " + innerSQL + " FROM " + table + condition;//建表语句  
                using (MySqlCommand cmdCreateTable = new MySqlCommand(sql, con))
                {
                    cmdCreateTable.CommandTimeout = 0;
                    cmdCreateTable.CommandText = sql;
                    con.Open();
                    MySqlDataReader dataReader = cmdCreateTable.ExecuteReader();
                    //String[] resultsStringArray = new String[query.Count];
                    resultsStringList = new List<List<String>>();

                    while (dataReader.Read())
                    {
                        List<String> temp = new List<String>();
                        for (int i = 0; i < query.Count; i++)
                        {
                            temp.Add(dataReader[query[i]].ToString());
                        }
                        resultsStringList.Add(temp);
                    }
                    dataReader.Close();
                }
            }
            return resultsStringList;
        }

        #endregion
    }
}
