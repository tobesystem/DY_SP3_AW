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
    internal class cErrorData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cErrorData()
        {
            _conn = _serverInfo.Server();
        }

        //에러코드 조회
        public cErrorItemList getErrorList(string machine_type, string err_code, string err_name)
        {
            cErrorItemList _list = new cErrorItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_ERROR_GET", connect);
                    cmd.Parameters.Add(new SqlParameter("MACHINE_TYPE", machine_type));
                    cmd.Parameters.Add(new SqlParameter("ERR_CODE", err_code));
                    cmd.Parameters.Add(new SqlParameter("ERR_NAME", err_name));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cErrorItem
                        {
                            ROW_NUM = Convert.ToInt32(row["ROW_NUM"]),
                            ID_MACH = row["ID_MACH"].ToString(),
                            ERR_CODE = row["ERR_CODE"].ToString(),
                            ERR_NAME = row["ERR_NAME"].ToString(),
                            ERR_ETC = row["ERR_ETC"].ToString(),
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

        public cMessage updateErrorCode(string id_mach, string err_code, string err_name, string err_etc,string keyword)
        {
            cMessage _items = new cMessage();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_ERROR_ADD_EDIT", connect);
                    cmd.Parameters.Add(new SqlParameter("ID_MACH", id_mach));
                    cmd.Parameters.Add(new SqlParameter("ERR_CODE", err_code));
                    cmd.Parameters.Add(new SqlParameter("ERR_NAME", err_name));
                    cmd.Parameters.Add(new SqlParameter("ERR_ETC", err_etc));
                    cmd.Parameters.Add(new SqlParameter("KEYWORD", keyword));

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        _items.RESULT = row["RESULT"].ToString();
                        _items.MSG = row["MSG"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _items;
        }

        public cMessage deleteErrorCode(string err_code)
        {
            cMessage _items = new cMessage();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_ERROR_DEL", connect);
                    cmd.Parameters.Add(new SqlParameter("ERR_CODE", err_code));

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        _items.RESULT = row["RESULT"].ToString();
                        _items.MSG = row["MESSAGE"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _items;
        }


        //설비에러 이력 조회
        public cErrorItemList getMachList(string proc_type, string start_date, string stop_date, string mach_type, string mach_code, string err_code)
        {
            cErrorItemList _list = new cErrorItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_ERROR_HISTORY_GET", connect);
                    cmd.Parameters.Add(new SqlParameter("PROC_TYPE", proc_type));
                    cmd.Parameters.Add(new SqlParameter("START_DATETIME", start_date));
                    cmd.Parameters.Add(new SqlParameter("STOP_DATETIME", stop_date));
                    cmd.Parameters.Add(new SqlParameter("MACH_TYPE", mach_type));
                    cmd.Parameters.Add(new SqlParameter("MACH_CODE", mach_code));
                    cmd.Parameters.Add(new SqlParameter("ERR_CODE", err_code));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cErrorItem
                        {
                            ID_MACH = row["ID_MACH"].ToString(),
                            ID_MACHNO = row["ID_MACHNO"].ToString(),
                            ERR_CODE = row["ERR_CODE"].ToString(),
                            ERR_DESC = row["ERR_DESC"].ToString(),
                            ERR_START = row["ERR_START"].ToString(),
                            ID_MEMO = row["ID_MEMO"].ToString(),
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

        //설비에러 집계 조회
        public cErrorItemList getMachSumList(string proc_type, string start_date, string stop_date, string mach_type, string mach_code, string err_code)
        {
            cErrorItemList _list = new cErrorItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_ERROR_HISTORY_GET", connect);
                    cmd.Parameters.Add(new SqlParameter("PROC_TYPE", proc_type));
                    cmd.Parameters.Add(new SqlParameter("START_DATETIME", start_date));
                    cmd.Parameters.Add(new SqlParameter("STOP_DATETIME", stop_date));
                    cmd.Parameters.Add(new SqlParameter("MACH_TYPE", mach_type));
                    cmd.Parameters.Add(new SqlParameter("MACH_CODE", mach_code));
                    cmd.Parameters.Add(new SqlParameter("ERR_CODE", err_code));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cErrorItem
                        {
                            ID_MACH = row["ID_MACH"].ToString(),
                            ID_MACHNO = row["ID_MACHNO"].ToString(),
                            ERR_CODE = row["ERR_CODE"].ToString(),
                            ERR_DESC = row["ERR_DESC"].ToString(),
                            ERR_CNT = Convert.ToInt32(row["ERR_CNT"]),
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

        //컨베이어 에러조회
        public cErrorItemList CvErrorGet()
        {
            cErrorItemList list = new cErrorItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_ERROR_INFO_GET", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new cErrorItem
                        {
                            ERR_MSG = row["ERR_MSG"].ToString(),
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return list;
        }

        //컨베이어 에러조회(SP3 용)
        public cErrorItemList Wh2CvErrorGet()
        {
            cErrorItemList list = new cErrorItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_ERROR_INFO_GET_WH2", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new cErrorItem
                        {
                            ERR_MSG = row["ERR_MSG"].ToString(),
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return list;
        }

    }
}
