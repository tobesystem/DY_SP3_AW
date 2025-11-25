using dayouAWSWh2.Class;
using dayouAWSWh2.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dayouAWSWh2.Data
{
    internal class cCommonData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cCommonData()
        {
            _conn = _serverInfo.Server();
        }

        public cCommonItemList getCommonList(string code_type, string code_option)
        {
            cCommonItemList _list = new cCommonItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_SYS_COMMON_LIST", connect);
                    cmd.Parameters.Add(new SqlParameter("CODE_TYPE", code_type));
                    cmd.Parameters.Add(new SqlParameter("CODE_OPTION", code_option));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cCommonItem
                        {
                            CODE_TYPE = row["CODE_TYPE"].ToString(),
                            CODE_NAME = row["CODE_NAME"].ToString(),
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


        //부서 조회용
        public cCommonItemList getDeptList()
        {
            cCommonItemList _list = new cCommonItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_DEPT_GET", connect);
                 
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cCommonItem
                        {
                            DEPT_CODE = row["DEPT_CODE"].ToString(),
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


        //권한 조회용
        public cCommonItemList getAuthList()
        {
            cCommonItemList _list = new cCommonItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_AUTH_GET", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cCommonItem
                        {
                            AUTH_CODE = row["AUTH_CODE"].ToString(),
                            AUTH_NAME = row["AUTH_NAME"].ToString(),
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


    }
}
