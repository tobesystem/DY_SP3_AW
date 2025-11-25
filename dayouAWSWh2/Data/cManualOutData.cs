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
    internal class cManualOutData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cManualOutData()
        {
            _conn = _serverInfo.Server();
        }

        // 수동 서열 출고 
        public cManualOutItemList getManualOutList(string alc_code, string alc_class)
        {
            cManualOutItemList _list = new cManualOutItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_MANUAL_OUT_GET", connect);
                    cmd.Parameters.Add(new SqlParameter("ALC_CODE", alc_code));
                    cmd.Parameters.Add(new SqlParameter("ALC_CLASS", alc_class));

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cManualOutItem
                        {
                            ALC_CODE = row["ALC_CODE"].ToString(),
                            ALC_CLASS = row["ALC_CLASS"].ToString(),
                            ALC_TYPE = row["ALC_TYPE"].ToString(),
                            FR_CNT = Convert.ToInt32(row["FR_CNT"]),
                            R2_CNT = Convert.ToInt32(row["R2_CNT"]),
                            R3_CNT = Convert.ToInt32(row["R3_CNT"]),
                            //SUM = Convert.ToInt32(row["SUM"]),
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

        //수동 서열출고 전체 
        public cManualOutItemList getManualOutSubList(string carCode)
        {
            cManualOutItemList _list = new cManualOutItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = connect.CreateCommand();
                    if (carCode == "SW")
                    {
                        cmd = new SqlCommand("SP_CS_MANUAL_OUT_DETAIL", connect);
                    }
                    else if(carCode == "SP3")
                    {
                        cmd = new SqlCommand("SP_CS_MANUAL_OUT_DETAIL_WH2", connect);
                    }

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cManualOutItem
                        {
                            ID_SUBIDX = Convert.ToInt32(row["ID_SUBIDX"]),
                            ID_BANK = row["LOAD_BANK"].ToString(),
                            ID_BAY = row["LOAD_BAY"].ToString(),
                            ID_LEVEL = row["LOAD_LEVEL"].ToString(),
                            LOAD_CODE = row["LOAD_CODE"].ToString(),
                            LOT_NO1 = row["LOT_NO1"].ToString(),
                            LOT_NO2 = row["LOT_NO2"].ToString(),
                            LOT_NO3 = row["LOT_NO3"].ToString(),
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

        public cManualOutItemList ManualOutAdd(string alc_code, string carCode)
        {
            cManualOutItemList _list = new cManualOutItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = connect.CreateCommand();
                    if (carCode == "SW")
                    {
                        cmd = new SqlCommand("SP_WMS_OUT_TMP_MANUAL_ORDER_ADD", connect);
                    }
                    else if (carCode == "SP3")
                    {
                        cmd = new SqlCommand("SP_WMS_OUT_TMP_MANUAL_ORDER_ADD_WH2", connect);
                    }

                    cmd.Parameters.Add(new SqlParameter("ALC_CODE", alc_code));

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cManualOutItem
                        {
                           
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

        public cManualOutItemList ManualAllOutAdd(string carCode)
        {
            cManualOutItemList _list = new cManualOutItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = connect.CreateCommand();
                    if (carCode == "SW")
                    {
                        cmd = new SqlCommand("SP_WMS_OUT_MANUAL_ORDER_CREATE", connect);
                    }
                    else if (carCode == "SP3")
                    {
                        cmd = new SqlCommand("SP_WMS_OUT_MANUAL_ORDER_CREATE_WH2", connect);
                    }

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                   
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _list;
        }

        //출고 취소
        public cMessage OutDel(string id_code, string carCode)
        {
            cMessage _items = new cMessage();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = connect.CreateCommand();
                    if (carCode == "SW")
                    {
                        cmd = new SqlCommand("SP_WMS_OUT_TMP_MANUAL_ORDER_DEL", connect);
                    }
                    else if (carCode == "SP3")
                    {
                        cmd = new SqlCommand("SP_WMS_OUT_TMP_MANUAL_ORDER_DEL_WH2", connect);
                    }

                    cmd.Parameters.Add(new SqlParameter("ID_CODE", id_code));
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
    }
}

