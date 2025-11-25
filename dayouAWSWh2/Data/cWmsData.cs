using dayouAWSWh2.Class;
using dayouAWSWh2.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace dayouAWSWh2.Data
{
    internal class cWmsData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn ;

        public cWmsData()
        {
            _conn = _serverInfo.Server();
        }

        //입출고 현황
        public cWmsOutItemList getWmsOut()
        {
            cWmsOutItemList _list = new cWmsOutItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_WMS_OUT_GET", connect);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cWmsOutItem
                        {
                            ID_TYPE = row["ID_TYPE"].ToString(),
                            ID_SC = row["ID_SC"].ToString(),
                            ID_DATE = row["ID_DATE"].ToString(),
                            ID_TIME = row["ID_TIME"].ToString(),
                            ID_INDEX = row["ID_INDEX"].ToString(),
                            ID_SUBIDX = row["ID_SUBIDX"].ToString(),
                            CELL = row["CELL"].ToString(),
                            HOGI = row["HOGI"].ToString(),
                            LOAD_CODE = row["LOAD_CODE"].ToString(),
                            PLT_CODE = row["PLT_CODE"].ToString(),
                            ITEM_CODE1 = row["ITEM_CODE1"].ToString(),
                            ITEM_CODE2 = row["ITEM_CODE2"].ToString(),
                            ITEM_CODE3 = row["ITEM_CODE3"].ToString(),
                            ITEM_CODE4 = row["ITEM_CODE4"].ToString(),
                            LOT_NO1 = row["LOT_NO1"].ToString(),
                            LOT_NO2 = row["LOT_NO2"].ToString(),
                            LOT_NO3 = row["LOT_NO3"].ToString(),
                            LOT_NO4 = row["LOT_NO4"].ToString(),
                            IN_DATE = row["IN_DATE"].ToString(),
                            IDX = row["IDX"].ToString(),
                            LOCATION = row["LOCATION"].ToString(),
                            COMMIT_NO = row["COMMIT_NO"].ToString(),
                            TRACKTYPE = row["TRACKTYPE"].ToString(),
                            ORDATE = row["ORDATE"].ToString(),
                            STATUS = row["STATUS"].ToString(),
                            RFID_STATUS = row["RFID_STATUS"].ToString(),
                            REPORT_SYS_DT = row["REPORT_SYS_DT"].ToString(),
                            FR_GUBUN_DESC = row["FR_GUBUN_DESC"].ToString(),
                            ID_CODE = row["ID_CODE"].ToString(),
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

        public cWmsInItemList getWmsIn()
        {
            cWmsInItemList _list = new cWmsInItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_WMS_IN_GET", connect);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cWmsInItem
                        {
                            ID_TYPE = row["ID_TYPE"].ToString(),
                            ID_SC = row["ID_SC"].ToString(),
                            ID_DATE = row["ID_DATE"].ToString(),
                            ID_TIME = row["ID_TIME"].ToString(),
                            ID_INDEX = row["ID_INDEX"].ToString(),
                            ID_SUBIDX = row["ID_SUBIDX"].ToString(),
                            CELL = row["CELL"].ToString(),
                            HOGI = row["HOGI"].ToString(),
                            COMMIT_NO = row["COMMIT_NO"].ToString(),
                            LOAD_CODE = row["LOAD_CODE"].ToString(),
                            PLT_CODE = row["PLT_CODE"].ToString(),
                            ITEM_CODE1 = row["ITEM_CODE1"].ToString(),
                            ITEM_CODE2 = row["ITEM_CODE2"].ToString(),
                            ITEM_CODE3 = row["ITEM_CODE3"].ToString(),
                            ITEM_CODE4 = row["ITEM_CODE4"].ToString(),
                            LOT_NO1 = row["LOT_NO1"].ToString(),
                            LOT_NO2 = row["LOT_NO2"].ToString(),
                            LOT_NO3 = row["LOT_NO3"].ToString(),
                            LOT_NO4 = row["LOT_NO4"].ToString(),
                            IDX = row["IDX"].ToString(),
                            LOCATION = row["LOCATION"].ToString(),
                            ORDATE = row["ORDATE"].ToString(),
                            STATUS = row["STATUS"].ToString(),
                            FR_GUBUN_DESC = row["FR_GUBUN_DESC"].ToString(),
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

        public cWmsInResultItemList getWmsInResult(string start_date, string stop_date, string car_code, string fr_gubun, string alc_code)
        {
            cWmsInResultItemList _list = new cWmsInResultItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_WMS_IN_RESULT_GET", connect);
                    cmd.Parameters.Add(new SqlParameter("START_DATETIME", start_date));
                    cmd.Parameters.Add(new SqlParameter("STOP_DATETIME", stop_date));
                    cmd.Parameters.Add(new SqlParameter("ALC_CLASS", car_code));
                    cmd.Parameters.Add(new SqlParameter("FR_GUBUN", fr_gubun));
                    cmd.Parameters.Add(new SqlParameter("ALC_CODE", alc_code));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cWmsInResultItem
                        {
                            ID_TYPE = row["ID_TYPE"].ToString(),
                            ROW_NUM = row["ROW_NUM"].ToString(),
                           
                            LOAD_CODE = row["LOAD_CODE"].ToString(),
                            PLT_CODE = row["PLT_CODE"].ToString(),
                            ITEM_CODE1 = row["ITEM_CODE1"].ToString(),
                            ITEM_CODE2 = row["ITEM_CODE2"].ToString(),
                            ITEM_CODE3 = row["ITEM_CODE3"].ToString(),
                            ITEM_CODE4 = row["ITEM_CODE4"].ToString(),
                            LOT_NO1 = row["LOT_NO1"].ToString(),
                            LOT_NO2 = row["LOT_NO2"].ToString(),
                            LOT_NO3 = row["LOT_NO3"].ToString(),
                            LOT_NO4 = row["LOT_NO4"].ToString(),
                            ID_BANK = row["ID_BANK"].ToString(),
                            ID_BAY = row["ID_BAY"].ToString(),
                            ID_LEVEL = row["ID_LEVEL"].ToString(),
                            WORKDATE = row["WORKDATE"].ToString(),
                            REGION = row["REGION"].ToString(),
                            ALC_CLASS = row["ALC_CLASS"].ToString(),
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

        public cWmsOutResultItemList getWmsOutResult(string start_date, string stop_date, string car_code, string fr_gubun, string alc_code)
        {
            cWmsOutResultItemList _list = new cWmsOutResultItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_WMS_OUT_RESULT_GET", connect);
                    cmd.Parameters.Add(new SqlParameter("START_DATETIME", start_date));
                    cmd.Parameters.Add(new SqlParameter("STOP_DATETIME", stop_date));
                    cmd.Parameters.Add(new SqlParameter("ALC_CLASS", car_code));
                    cmd.Parameters.Add(new SqlParameter("FR_GUBUN", fr_gubun));
                    cmd.Parameters.Add(new SqlParameter("ALC_CODE", alc_code));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cWmsOutResultItem
                        {
                            ID_TYPE = row["ID_TYPE"].ToString(),
                            ROW_NUM = row["ROW_NUM"].ToString(),

                            LOAD_CODE = row["LOAD_CODE"].ToString(),
                            PLT_CODE = row["PLT_CODE"].ToString(),
                            ITEM_CODE1 = row["ITEM_CODE1"].ToString(),
                            ITEM_CODE2 = row["ITEM_CODE2"].ToString(),
                            ITEM_CODE3 = row["ITEM_CODE3"].ToString(),
                            ITEM_CODE4 = row["ITEM_CODE4"].ToString(),
                            LOT_NO1 = row["LOT_NO1"].ToString(),
                            LOT_NO2 = row["LOT_NO2"].ToString(),
                            LOT_NO3 = row["LOT_NO3"].ToString(),
                            LOT_NO4 = row["LOT_NO4"].ToString(),
                            ID_BANK = row["ID_BANK"].ToString(),
                            ID_BAY = row["ID_BAY"].ToString(),
                            ID_LEVEL = row["ID_LEVEL"].ToString(),
                            WORKDATE = row["WORKDATE"].ToString(),
                            REGION = row["REGION"].ToString(),
                            ALC_CLASS = row["ALC_CLASS"].ToString(),
                            ID_INDEX = Convert.ToInt32(row["ID_INDEX"]),
                            
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

        //입고 강제완료
        public cMessage WmsInForce(string id_type, string id_date, string id_time, string id_index, string id_subidx)
        {
            cMessage _items = new cMessage();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_WMS_IN_ORDER_FORCE", connect);
                    cmd.Parameters.Add(new SqlParameter("ID_TYPE", id_type));
                    cmd.Parameters.Add(new SqlParameter("ID_DATE", id_date));
                    cmd.Parameters.Add(new SqlParameter("ID_TIME", id_time));
                    cmd.Parameters.Add(new SqlParameter("ID_INDEX", id_index));
                    cmd.Parameters.Add(new SqlParameter("ID_SUBIDX", id_subidx));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    //DataTable dt = ds.Tables[0];
                    //_items.MSG = dt.Rows[0]["MSG"].ToString();
                    //_items.RESULT = dt.Rows[0]["RESULT"].ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _items;
        }

        //입고 취소
        public cMessage WmsInCancel(string id_type, string id_date, string id_time, string id_index, string id_subidx)
        {
            cMessage _items = new cMessage();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_WMS_IN_ORDER_CANCEL", connect);
                    cmd.Parameters.Add(new SqlParameter("ID_TYPE", id_type));
                    cmd.Parameters.Add(new SqlParameter("ID_DATE", id_date));
                    cmd.Parameters.Add(new SqlParameter("ID_TIME", id_time));
                    cmd.Parameters.Add(new SqlParameter("ID_INDEX", id_index));
                    cmd.Parameters.Add(new SqlParameter("ID_SUBIDX", id_subidx));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    //DataTable dt = ds.Tables[0];
                    //_items.MSG = dt.Rows[0]["MSG"].ToString();
                    //_items.RESULT = dt.Rows[0]["RESULT"].ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _items;
        }

        //출고 강제완료
        public cMessage WmsOutForce(string id_type, string id_date, string id_time, string id_index, string id_subidx)
        {
            cMessage _items = new cMessage();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_WMS_OUT_ORDER_FORCE", connect);
                    cmd.Parameters.Add(new SqlParameter("ID_TYPE", id_type));
                    cmd.Parameters.Add(new SqlParameter("ID_DATE", id_date));
                    cmd.Parameters.Add(new SqlParameter("ID_TIME", id_time));
                    cmd.Parameters.Add(new SqlParameter("ID_INDEX", id_index));
                    cmd.Parameters.Add(new SqlParameter("ID_SUBIDX", id_subidx));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    //DataTable dt = ds.Tables[0];
                    //_items.MSG = dt.Rows[0]["MSG"].ToString();
                    //_items.RESULT = dt.Rows[0]["RESULT"].ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _items;
        }

        //출고 취소
        public cMessage WmsOutCancel(string id_type, string id_date, string id_time, string id_index, string id_subidx)
        {
            cMessage _items = new cMessage();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_WMS_OUT_ORDER_CANCEL", connect);
                    cmd.Parameters.Add(new SqlParameter("ID_TYPE", id_type));
                    cmd.Parameters.Add(new SqlParameter("ID_DATE", id_date));
                    cmd.Parameters.Add(new SqlParameter("ID_TIME", id_time));
                    cmd.Parameters.Add(new SqlParameter("ID_INDEX", id_index));
                    cmd.Parameters.Add(new SqlParameter("ID_SUBIDX", id_subidx));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    //DataTable dt = ds.Tables[0];
                    //_items.MSG = dt.Rows[0]["MSG"].ToString();
                    //_items.RESULT = dt.Rows[0]["RESULT"].ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _items;
        }

        //출하 검증조회
        public cPDAWmsOutList PDAWmsOut()
        {
            cPDAWmsOutList _list = new cPDAWmsOutList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_WMS_OUT_PDA_GET", connect);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cPDAWmsOutItem
                        {
                            OUT_DATE = row["OUT_DATE"].ToString(),
                            ID_INDEX = row["ID_INDEX"].ToString(),

                            ID_SUBIDX = row["ID_SUBIDX"].ToString(),
                            COMMIT_NO = row["COMMIT_NO"].ToString(),
                            ALC_CODE = row["ALC_CODE"].ToString(),
                            LOT_NO1 = row["LOT_NO1"].ToString(),
                            PLT_CODE = row["PLT_CODE"].ToString(),
                            REPORT_SYS_DT = row["REPORT_SYS_DT"].ToString(),
                            BODY_NO = row["BODY_NO"].ToString(),
                            FR_GUBUN = row["FR_GUBUN"].ToString(),
                            PDA_CHECK = row["PDA_CHECK"].ToString(),

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

        //출하 검증 이력조회
        public cPDAWmsOutList PDAWmsOutHist(string start_date, string stop_date, string body_no, string lot_no)
        {
            cPDAWmsOutList _list = new cPDAWmsOutList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_WMS_OUT_PDA_HIST_GET", connect);
                    cmd.Parameters.Add(new SqlParameter("START_DATE", start_date));
                    cmd.Parameters.Add(new SqlParameter("STOP_DATE", stop_date));
                    cmd.Parameters.Add(new SqlParameter("BODY_NO", body_no));
                    cmd.Parameters.Add(new SqlParameter("LOT_NO", lot_no));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cPDAWmsOutItem
                        {
                            CHECK_DATE = row["CHECK_DATE"].ToString(),
                            CHECK_USER = row["CHECK_USER"].ToString(),
                            OUT_DATE = row["OUT_DATE"].ToString(),
                            ID_INDEX = row["ID_INDEX"].ToString(),

                            ID_SUBIDX = row["ID_SUBIDX"].ToString(),
                            COMMIT_NO = row["COMMIT_NO"].ToString(),
                            ALC_CODE = row["ALC_CODE"].ToString(),
                            LOT_NO1 = row["LOT_NO1"].ToString(),
                            PLT_CODE = row["PLT_CODE"].ToString(),
                            REPORT_SYS_DT = row["REPORT_SYS_DT"].ToString(),
                            BODY_NO = row["BODY_NO"].ToString(),
                            FR_GUBUN = row["FR_GUBUN"].ToString(),
                            PDA_CHECK = row["PDA_CHECK"].ToString(),

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
