using dayouAWSWh2.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dayouAWSWh2.Server;

namespace dayouAWSWh2.Data
{
    internal class cUserData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cUserData()
        {
            _conn = _serverInfo.Server();
        }
        public cUserItemList getUser(string user_code, string user_name ,string use_yn)
        {
            cUserItemList _list = new cUserItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_USER_GET", connect);
                    cmd.Parameters.Add(new SqlParameter("USER_CODE", user_code));
                    cmd.Parameters.Add(new SqlParameter("USER_NAME", user_name));
                    cmd.Parameters.Add(new SqlParameter("USE_YN", use_yn));
                   
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cUserItem
                        {
                           USER_CODE = row["USER_CODE"].ToString(),
                           USER_NAME = row["USER_NAME"].ToString(),
                           TEL_NO = row["TEL_NO"].ToString(),
                           EMAIL = row["EMAIL"].ToString(),
                           USE_YN = row["USE_YN"].ToString(),
                           LOGIN_DATE = row["LOGIN_DATE"].ToString(),
                           CREATE_DATE = row["CREATE_DATE"].ToString(),
                           AUTH_NAME = row["AUTH_NAME"].ToString(),
                           DEPT_NAME = row["DEPT_NAME"].ToString(),
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _list;
        }

        public cUserItem getUserSetting(string user_code)
        {
            cUserItem _items = new cUserItem();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_USER_SETTING_GET", connect);
                    cmd.Parameters.Add(new SqlParameter("USER_CODE", user_code));

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        _items.USER_NAME = row["USER_NAME"].ToString();
                        _items.TEL_NO = row["TEL_NO"].ToString();
                        _items.EMAIL = row["EMAIL"].ToString();
                        _items.DEPT_NAME = row["DEPT_NAME"].ToString();
                        _items.EMAIL_USER_NAME = row["EMAIL_USER_NAME"].ToString();
                        _items.EMAIL_DOMAIN = row["EMAIL_DOMAIN"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _items;
        }

        public cMessage addUser(string user_code, string user_name, string user_pwd, string auth_code, string dept_code, string tel_no, string email, string user_yn, string edit_user_code, string save_type)
        {
            cMessage _items = new cMessage();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_USER_ADD", connect);
                    cmd.Parameters.Add(new SqlParameter("USER_CODE", user_code));
                    cmd.Parameters.Add(new SqlParameter("USER_NAME", user_name));
                    cmd.Parameters.Add(new SqlParameter("USER_PWD", user_pwd));
                    cmd.Parameters.Add(new SqlParameter("AUTH_CODE", auth_code));
                    cmd.Parameters.Add(new SqlParameter("DEPT_CODE", dept_code));
                    cmd.Parameters.Add(new SqlParameter("TEL_NO", tel_no));
                    cmd.Parameters.Add(new SqlParameter("EMAIL", email));
                    cmd.Parameters.Add(new SqlParameter("USE_YN", user_yn));
                    cmd.Parameters.Add(new SqlParameter("EDIT_USER_CODE", edit_user_code));
                    cmd.Parameters.Add(new SqlParameter("SAVE_TYPE", save_type));

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        _items.MSG = row["MESSAGE"].ToString();
                        _items.RESULT = row["RESULT"].ToString();
                      
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _items;
        }

        public cMessage delUser(string user_code)
        {
            cMessage _items = new cMessage();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_USER_DEL", connect);
                    cmd.Parameters.Add(new SqlParameter("USER_CODE", user_code));

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        _items.MSG = row["MSG"].ToString();
                        _items.RESULT = row["RESULT"].ToString();

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _items;
        }
    }
}
