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
    internal class cStatusData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cStatusData()
        {
            _conn = _serverInfo.Server();
        }
        //시스템 설정 조회(제어설정)
        public cStatusItemList getCurrentStatusList()
        {
            cStatusItemList _list = new cStatusItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CURRENT_STATUS_GET", connect);
                   
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cStatusItem
                        {
                            Current_Name = row["Current_Name"].ToString(),
                            Option1 = Convert.ToInt32(row["Option1"]),
                            Option2 = Convert.ToInt32(row["Option2"]),
                            Option3 = Convert.ToInt32(row["Option3"]),
                            Current_Type = row["Current_Type"].ToString(),
                            Current_Desc = row["Current_Desc"].ToString(),
                            Current_index = Convert.ToInt32(row["Current_index"])
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

        //소켓 통신설정
        public cStatusItemList getComsetStatusList()
        {
            cStatusItemList _list = new cStatusItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_COMSET_STATUS_GET", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cStatusItem
                        {
                            ID_CODE = row["ID_CODE"].ToString(),
                            ID_NAME = row["ID_NAME"].ToString(),
                            ID_IP = row["ID_IP"].ToString(),
                            ID_PORT1 = row["ID_PORT1"].ToString(),
                            ID_PORT2 = row["ID_PORT2"].ToString(),
                            ID_LOG = Convert.ToInt32(row["ID_LOG"]),
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

        //시스템 설정 저장
        public void statusUpdate(int hostOut_chk, int bcrMode_chk1 , int bcrMode_chk2, int bcrMode_chk3, int inSet_chk1, int inSet_chk2, int inOrder_chk1, int inOrder_chk2, int outSet_chk1
                                            ,int outSet_chk2, int outOrder_chk1, int outOrder_chk2, string _ip1 , string _ip2, string _ip3, string _ip4, string _ip8, string _ip9, string _ip5, string _ip6, string _ip7
                                            , string port1_1, string port1_2, string port1_3, string port1_4, string port1_8, string port1_9 ,string port1_5, string port1_6, string port1_7
                                            , string port2_1, string port2_2, string port2_3, string port2_4, string port2_8, string port2_9 ,string port2_5, string port2_6, string port2_7
                                            , int log_chk1, int log_chk2, int log_chk3, int log_chk4, int log_chk5, int RFID_chk1, int LOT_chk1)
        {

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_SYSTEM_STATUS_UPDATE", connect);

                    cmd.Parameters.Add(new SqlParameter("AUTO_HOSTOUT_CHK", hostOut_chk));
                    cmd.Parameters.Add(new SqlParameter("BCRMODE_CHK1", bcrMode_chk1));
                    cmd.Parameters.Add(new SqlParameter("BCRMODE_CHK2", bcrMode_chk2));
                    cmd.Parameters.Add(new SqlParameter("BCRMODE_CHK3", bcrMode_chk3));
                    cmd.Parameters.Add(new SqlParameter("SC_IN_SET_CHK1", inSet_chk1));
                    cmd.Parameters.Add(new SqlParameter("SC_IN_SET_CHK2", inSet_chk2));
                    cmd.Parameters.Add(new SqlParameter("SC_IN_ORDER_CHK1", inOrder_chk1));
                    cmd.Parameters.Add(new SqlParameter("SC_IN_ORDER_CHK2", inOrder_chk2));
                    cmd.Parameters.Add(new SqlParameter("SC_OUT_SET_CHK1", outSet_chk1));
                    cmd.Parameters.Add(new SqlParameter("SC_OUT_SET_CHK2", outSet_chk2));
                    cmd.Parameters.Add(new SqlParameter("SC_OUT_ORDER_CHK1", outOrder_chk1));
                    cmd.Parameters.Add(new SqlParameter("SC_OUT_ORDER_CHK2", outOrder_chk2));
                    cmd.Parameters.Add(new SqlParameter("IP_ADDRESS1", _ip1));
                    cmd.Parameters.Add(new SqlParameter("IP_ADDRESS2", _ip2));
                    cmd.Parameters.Add(new SqlParameter("IP_ADDRESS3", _ip3));
                    cmd.Parameters.Add(new SqlParameter("IP_ADDRESS4", _ip4));
                    cmd.Parameters.Add(new SqlParameter("IP_ADDRESS8", _ip8));
                    cmd.Parameters.Add(new SqlParameter("IP_ADDRESS9", _ip9));
                    cmd.Parameters.Add(new SqlParameter("IP_ADDRESS5", _ip5));
                    cmd.Parameters.Add(new SqlParameter("IP_ADDRESS6", _ip6));
                    cmd.Parameters.Add(new SqlParameter("IP_ADDRESS7", _ip7));
                    cmd.Parameters.Add(new SqlParameter("1PORT1", port1_1));
                    cmd.Parameters.Add(new SqlParameter("1PORT2", port1_2));
                    cmd.Parameters.Add(new SqlParameter("1PORT3", port1_3));
                    cmd.Parameters.Add(new SqlParameter("1PORT4", port1_4));
                    cmd.Parameters.Add(new SqlParameter("1PORT8", port1_8));
                    cmd.Parameters.Add(new SqlParameter("1PORT9", port1_9));
                    cmd.Parameters.Add(new SqlParameter("1PORT5", port1_5));
                    cmd.Parameters.Add(new SqlParameter("1PORT6", port1_6));
                    cmd.Parameters.Add(new SqlParameter("1PORT7", port1_7));
                    cmd.Parameters.Add(new SqlParameter("2PORT1", port2_1));
                    cmd.Parameters.Add(new SqlParameter("2PORT2", port2_2));
                    cmd.Parameters.Add(new SqlParameter("2PORT3", port2_3));
                    cmd.Parameters.Add(new SqlParameter("2PORT4", port2_4));
                    cmd.Parameters.Add(new SqlParameter("2PORT8", port2_8));
                    cmd.Parameters.Add(new SqlParameter("2PORT9", port2_9));
                    cmd.Parameters.Add(new SqlParameter("2PORT5", port2_5));
                    cmd.Parameters.Add(new SqlParameter("2PORT6", port2_6));
                    cmd.Parameters.Add(new SqlParameter("2PORT7", port2_7));
                    cmd.Parameters.Add(new SqlParameter("LOG_CHK1", log_chk1));
                    cmd.Parameters.Add(new SqlParameter("LOG_CHK2", log_chk2));
                    cmd.Parameters.Add(new SqlParameter("LOG_CHK3", log_chk3));
                    cmd.Parameters.Add(new SqlParameter("LOG_CHK4", log_chk4));
                    cmd.Parameters.Add(new SqlParameter("LOG_CHK5", log_chk5));
                    cmd.Parameters.Add(new SqlParameter("RFID_CHK1", RFID_chk1));
                    cmd.Parameters.Add(new SqlParameter("LOT_CHK1", LOT_chk1));

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

        }

        //프로그램 제어 상태값 변경
        public cStatusItemList programStatusUpdate(int current_idx, string _type)
        {
            cStatusItemList _list = new cStatusItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_PROGRAM_STATUS_UPDATE", connect);
                    cmd.Parameters.Add(new SqlParameter("IDX", current_idx));
                    cmd.Parameters.Add(new SqlParameter("TYPE", _type));

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

        //프로그램 상태값 가져오기
        public cStatusItemList programStatusget()
        {
            cStatusItemList _list = new cStatusItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_PROGRAM_STATUS_GET", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cStatusItem
                        {
                            Current_Name = row["Current_Name"].ToString(),
                            Option1 = Convert.ToInt32(row["Option1"]),
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

        /// <summary>
        /// SP3용 프로시저
        /// </summary>
        
        //시스템 설정 조회(제어설정) SP3용
        public cStatusItemList getCurrentStatusList_WH2()
        {
            cStatusItemList _list = new cStatusItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CURRENT_STATUS_GET_WH2", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cStatusItem
                        {
                            Current_Name = row["Current_Name"].ToString(),
                            Option1 = Convert.ToInt32(row["Option1"]),
                            Option2 = Convert.ToInt32(row["Option2"]),
                            Option3 = Convert.ToInt32(row["Option3"]),
                            Current_Type = row["Current_Type"].ToString(),
                            Current_Desc = row["Current_Desc"].ToString(),
                            Current_index = Convert.ToInt32(row["Current_index"])
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

        //소켓 통신설정 SP3용
        public cStatusItemList getComsetStatusList_WH2()
        {
            cStatusItemList _list = new cStatusItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_COMSET_STATUS_GET_WH2", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cStatusItem
                        {
                            ID_CODE = row["ID_CODE"].ToString(),
                            ID_NAME = row["ID_NAME"].ToString(),
                            ID_IP = row["ID_IP"].ToString(),
                            ID_PORT1 = row["ID_PORT1"].ToString(),
                            ID_PORT2 = row["ID_PORT2"].ToString(),
                            ID_LOG = Convert.ToInt32(row["ID_LOG"]),
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

        //시스템 설정 저장 SP3용
        public void statusUpdate_WH2(int hostOut_chk, int bcrMode_chk1, int bcrMode_chk2, int inSet_chk1, int inSet_chk2, int inOrder_chk1, int inOrder_chk2, int outSet_chk1
                                            , int outSet_chk2, int outOrder_chk1, int outOrder_chk2, string _ip1, string _ip2, string _ip3, string _ip4, string _ip5, string _ip6
                                            , string port1_1, string port1_2, string port1_3, string port1_4, string port1_5, string port1_6
                                            , string port2_1, string port2_2, string port2_3, string port2_4, string port2_5, string port2_6
                                            , int log_chk1, int log_chk2, int log_chk3, int log_chk4, int LOT_chk1)
        {

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_SYSTEM_STATUS_UPDATE_WH2", connect);

                    cmd.Parameters.Add(new SqlParameter("AUTO_HOSTOUT_CHK", hostOut_chk));
                    cmd.Parameters.Add(new SqlParameter("BCRMODE_CHK1", bcrMode_chk1));
                    cmd.Parameters.Add(new SqlParameter("BCRMODE_CHK2", bcrMode_chk2));
                    cmd.Parameters.Add(new SqlParameter("SC_IN_SET_CHK1", inSet_chk1));
                    cmd.Parameters.Add(new SqlParameter("SC_IN_SET_CHK2", inSet_chk2));
                    cmd.Parameters.Add(new SqlParameter("SC_IN_ORDER_CHK1", inOrder_chk1));
                    cmd.Parameters.Add(new SqlParameter("SC_IN_ORDER_CHK2", inOrder_chk2));
                    cmd.Parameters.Add(new SqlParameter("SC_OUT_SET_CHK1", outSet_chk1));
                    cmd.Parameters.Add(new SqlParameter("SC_OUT_SET_CHK2", outSet_chk2));
                    cmd.Parameters.Add(new SqlParameter("SC_OUT_ORDER_CHK1", outOrder_chk1));
                    cmd.Parameters.Add(new SqlParameter("SC_OUT_ORDER_CHK2", outOrder_chk2));
                    cmd.Parameters.Add(new SqlParameter("IP_ADDRESS1", _ip1));
                    cmd.Parameters.Add(new SqlParameter("IP_ADDRESS2", _ip2));
                    cmd.Parameters.Add(new SqlParameter("IP_ADDRESS3", _ip3));
                    cmd.Parameters.Add(new SqlParameter("IP_ADDRESS4", _ip4));
                    cmd.Parameters.Add(new SqlParameter("IP_ADDRESS5", _ip5));
                    cmd.Parameters.Add(new SqlParameter("IP_ADDRESS6", _ip6));
                    cmd.Parameters.Add(new SqlParameter("1PORT1", port1_1));
                    cmd.Parameters.Add(new SqlParameter("1PORT2", port1_2));
                    cmd.Parameters.Add(new SqlParameter("1PORT3", port1_3));
                    cmd.Parameters.Add(new SqlParameter("1PORT4", port1_4));
                    cmd.Parameters.Add(new SqlParameter("1PORT5", port1_5));
                    cmd.Parameters.Add(new SqlParameter("1PORT6", port1_6));
                    cmd.Parameters.Add(new SqlParameter("2PORT1", port2_1));
                    cmd.Parameters.Add(new SqlParameter("2PORT2", port2_2));
                    cmd.Parameters.Add(new SqlParameter("2PORT3", port2_3));
                    cmd.Parameters.Add(new SqlParameter("2PORT4", port2_4));
                    cmd.Parameters.Add(new SqlParameter("2PORT5", port2_5));
                    cmd.Parameters.Add(new SqlParameter("2PORT6", port2_6));
                    cmd.Parameters.Add(new SqlParameter("LOG_CHK1", log_chk1));
                    cmd.Parameters.Add(new SqlParameter("LOG_CHK2", log_chk2));
                    cmd.Parameters.Add(new SqlParameter("LOG_CHK3", log_chk3));
                    cmd.Parameters.Add(new SqlParameter("LOG_CHK4", log_chk4));
                    cmd.Parameters.Add(new SqlParameter("LOT_CHK1", LOT_chk1));

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

        }

        //프로그램 제어 상태값 변경 SP3용
        public cStatusItemList programStatusUpdate_WH2(int current_idx, string _type)
        {
            cStatusItemList _list = new cStatusItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_PROGRAM_STATUS_UPDATE_WH2", connect);
                    cmd.Parameters.Add(new SqlParameter("IDX", current_idx));
                    cmd.Parameters.Add(new SqlParameter("TYPE", _type));

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

        //프로그램 상태값 가져오기 SP3용
        public cStatusItemList programStatusget_WH2()
        {
            cStatusItemList _list = new cStatusItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_PROGRAM_STATUS_GET_WH2", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cStatusItem
                        {
                            Current_Name = row["Current_Name"].ToString(),
                            Option1 = Convert.ToInt32(row["Option1"]),
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
