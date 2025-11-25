using dayouAWSWh2.Class;
using dayouAWSWh2.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace dayouAWSWh2.Data
{
    internal class cHostData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cHostData()
        {
            _conn = _serverInfo.Server();
        }

        //의장정보 현황 조회
        public cHostItemList getHostList(string _alcClass, string _alcCode, string _bodyNo , string _cmtNo, string _status)
        {
            cHostItemList _list = new cHostItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_HOST_LIST_GET", connect);
                   
                    cmd.Parameters.Add(new SqlParameter("ALC_CLASS", _alcClass));
                    cmd.Parameters.Add(new SqlParameter("ALC_CODE", _alcCode));
                    cmd.Parameters.Add(new SqlParameter("BODY_NO", _bodyNo));
                    cmd.Parameters.Add(new SqlParameter("CMT_NO", _cmtNo));
                    cmd.Parameters.Add(new SqlParameter("STATUS", _status));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cHostItem
                        {
                           ROW_NUM = row["ROW_NUM"].ToString(),
                           REPORT_SYS_DT = row["REPORT_SYS_DT"].ToString(),
                           REPORT_DT = row["REPORT_DT"].ToString(),
                            CMT_NO = row["CMT_NO"].ToString(),
                           ALC_CODE = row["ALC_CODE"].ToString(),
                           ALC_CHECK = row["ALC_CHECK"].ToString(),
                           BODY_NO = row["BODY_NO"].ToString(),
                           HANGUEL_PART = row["HANGUEL_PART"].ToString(),
                           REGION_NAME = row["REGION_NAME"].ToString(),
                           INNER_COLOR = row["INNER_COLOR"].ToString(),
                           ALC_CLASS = row["ALC_CLASS"].ToString(),
                           STOCK_FR = row["STOCK_FR"].ToString(),
                           STOCK_R2 = row["STOCK_R2"].ToString(),
                           STOCK_R3 = row["STOCK_R3"].ToString(),
                           STATUS = row["STATUS"].ToString(),
                           CR_DATE = row["CR_DATE"].ToString(),
                           
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

        //의장 실적 조회
        public cHostItemList getHostResultList(string date1, string date2, string date3, string date4, string alc_code, string alc_class, string body_no,string _cmtNo, string _status)
        {
            cHostItemList _list = new cHostItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_HOST_RESULT_GET", connect);

                    cmd.Parameters.Add(new SqlParameter("COMP_START_DATETIME", date1));
                    cmd.Parameters.Add(new SqlParameter("COMP_STOP_DATETIME", date2));
                    cmd.Parameters.Add(new SqlParameter("HOST_START_DATETIME", date3));
                    cmd.Parameters.Add(new SqlParameter("HOST_STOP_DATETIME", date4));
                    cmd.Parameters.Add(new SqlParameter("ALC_CODE", alc_code));
                    cmd.Parameters.Add(new SqlParameter("ALC_CLASS", alc_class));
                    cmd.Parameters.Add(new SqlParameter("BODY_NO", body_no));
                    cmd.Parameters.Add(new SqlParameter("CMT_NO", _cmtNo));
                    cmd.Parameters.Add(new SqlParameter("STATUS", _status));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cHostItem
                        {
                            ROW_NUM = row["ROW_NUM"].ToString(),
                            COMPLETE_DT = row["COMPLETE_DT"].ToString(),
                            REPORT_DT = row["REPORT_DT"].ToString(),
                            CMT_NO = row["CMT_NO"].ToString(),
                            ALC_CODE = row["ALC_CODE"].ToString(),
                            ALC_CHECK = row["ALC_CHECK"].ToString(),
                            BODY_NO = row["BODY_NO"].ToString(),
                            HANGUEL_PART = row["HANGUEL_PART"].ToString(),
                            REGION_NAME = row["REGION_NAME"].ToString(),
                            DRIVE_TYPE = row["DRIVE_TYPE"].ToString(),
                            BODY_COLOR = row["BODY_COLOR"].ToString(),
                            INNER_COLOR = row["INNER_COLOR"].ToString(),
                            ALC_CLASS = row["ALC_CLASS"].ToString(),
                            REPORT_SYS_DT = row["REPORT_SYS_DT"].ToString(),
                            STATUS = row["STATUS"].ToString(),
                            

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

        //의장 출고 내역 메인
        public cHostItemList getHostOutList(string date1)
        {
            cHostItemList _list = new cHostItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_HOST_OUT_GET", connect);
                    cmd.Parameters.Add(new SqlParameter("SELECT_DATE", date1));
                
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cHostItem
                        {
                            ROW_NUM = row["ROW_NUM"].ToString(),
                            BR_DATE = row["BR_DATE"].ToString(),
                            BR_NO = row["BR_NO"].ToString(),
                            CNT = Convert.ToInt32(row["CNT"]),
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

        //의장 출고 내역 서브
        public cHostItemList getHostOutSubList(string _brNo)
        {
            cHostItemList _list = new cHostItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_HOST_OUT_SUB_GET", connect);
                    cmd.Parameters.Add(new SqlParameter("BR_NO", _brNo));

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cHostItem
                        {
                            ID_SUBIDX = Convert.ToInt32(row["ID_SUBIDX"]),
                            WMS_OUT_NO = row["WMS_OUT_NO"].ToString(),
                            COMMIT_NO = row["COMMIT_NO"].ToString(),
                            ALC_CLASS = row["ALC_CLASS"].ToString(),
                            ALC_CODE = row["ALC_CODE"].ToString(),
                            COVER_COLOR = row["COVER_COLOR"].ToString(),
                            COVER_SET = row["COVER_SET"].ToString(),
                            REGION = row["REGION"].ToString(),
                            OUT_DATE = row["OUT_DATE"].ToString(),
                            IN_DATE = row["IN_DATE"].ToString(),
                            LOCATION = row["LOCATION"].ToString(),
                            PLT_CODE = row["PLT_CODE"].ToString(),
                            FR_GUBUN = row["FR_GUBUN"].ToString(),
                            BODY_NO = row["BODY_NO"].ToString(),
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

        //의장 삭제
        public cMessage delHost(string sys_dt, string cmt_no, string status)
        {
            cMessage _items = new cMessage();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_HOST_LIST_DEL", connect);

                    cmd.Parameters.Add(new SqlParameter("REPORT_SYS_DT", sys_dt));
                    cmd.Parameters.Add(new SqlParameter("CMT_NO", cmt_no));
                    cmd.Parameters.Add(new SqlParameter("STATUS", status));

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    _items.MSG = dt.Rows[0]["MSG"].ToString();
                    _items.RESULT = dt.Rows[0]["RESULT"].ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _items;
        }

        //의장 출고
        public cMessage outHost()
        {
            cMessage _items = new cMessage();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CURRENT_HOST_OUT_UPDATE", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    _items.MSG = dt.Rows[0]["RESULT_MSG"].ToString();
                    _items.RESULT = dt.Rows[0]["RESULT"].ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _items;
        }

        //의장 ALC 변경
        public cMessage alcUpdateHost(string sys_dt, string cmt_no, string status, string _alc_code)
        {
            cMessage _items = new cMessage();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_HOST_ALC_UPDATE", connect);

                    cmd.Parameters.Add(new SqlParameter("REPORT_SYS_DT", sys_dt));
                    cmd.Parameters.Add(new SqlParameter("CMT_NO", cmt_no));
                    cmd.Parameters.Add(new SqlParameter("STATUS", status));
                    cmd.Parameters.Add(new SqlParameter("ALC_CODE", _alc_code));

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    _items.MSG = dt.Rows[0]["MSG"].ToString();
                    _items.RESULT = dt.Rows[0]["RESULT"].ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _items;
        }

        //의장 정보 추가
        public cMessage alcAddHost(string sys_dt, string cmt_no, string alc_code, string bodyNo, string region, string nation, string color)
        {
            cMessage _items = new cMessage();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_HOST_LIST_ADD", connect);
                   
                    cmd.Parameters.Add(new SqlParameter("REPORT_SYS_DT", sys_dt));
                    cmd.Parameters.Add(new SqlParameter("CMT_NO", cmt_no));
                    cmd.Parameters.Add(new SqlParameter("ALC_CODE", alc_code));
                    cmd.Parameters.Add(new SqlParameter("BODY_NO", bodyNo));
                    cmd.Parameters.Add(new SqlParameter("REGION_NAME", region));
                    cmd.Parameters.Add(new SqlParameter("NATION_NAME", nation));
                    cmd.Parameters.Add(new SqlParameter("INNER_COLOR", color));

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    _items.MSG = dt.Rows[0]["MSG"].ToString();
                    _items.RESULT = dt.Rows[0]["RESULT"].ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _items;
        }

        //의장 지정 갯수 출고
        public cMessage outCntHost(int TARGET_NO)
        {
            cMessage _items = new cMessage();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_WMS_OUT_HOST_ORDER_CREATE_TARGETNO", connect);
                   
                    cmd.Parameters.Add(new SqlParameter("TARGET_NO", TARGET_NO));

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    _items.MSG = dt.Rows[0]["RESULT_MSG"].ToString();
                    _items.RESULT = dt.Rows[0]["RESULT"].ToString();
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
