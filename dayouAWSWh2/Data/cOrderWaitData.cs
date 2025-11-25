using dayouAWSWh2.Class;
using dayouAWSWh2.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dayouAWSWh2.Data
{
    internal class cOrderWaitData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cOrderWaitData()
        {
            _conn = _serverInfo.Server();
        }

        //서열출고 대기 조회
        public cOrderWaitItemList OrderWaitGet(string carCode)
        {
            cOrderWaitItemList list = new cOrderWaitItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = connect.CreateCommand();
                    if (carCode == "SW")
                    {
                        cmd = new SqlCommand("SP_CS_ORDER_WAIT_GET", connect);
                    }
                    else if (carCode == "SP3")
                    {
                        cmd = new SqlCommand("SP_CS_ORDER_WAIT_GET_WH2", connect);
                    }

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new cOrderWaitItem
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
            return list;
        }

        public void deleteOrderWait(string lot_no, string carCode)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = connect.CreateCommand();
                    if (carCode == "SW")
                    {
                        cmd = new SqlCommand("SP_CS_ORDER_WAIT_DELETE", connect);
                    }
                    else if (carCode == "SP3")
                    {
                        cmd = new SqlCommand("SP_CS_ORDER_WAIT_DELETE_WH2", connect);
                    }
                    cmd.Parameters.Add(new SqlParameter("LOT_NO", lot_no));

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
    }
}
