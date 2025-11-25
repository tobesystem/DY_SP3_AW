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
    internal class cRFIDData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cRFIDData()
        {
            _conn = _serverInfo.Server();
        }


        //RFID Read 데이터 가져오기
        public cRFIDItemList RFIDRDataGet()
        {
            cRFIDItemList list = new cRFIDItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_RFID_RDATA_GET", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new cRFIDItem
                        {
                            RFIDNO = Convert.ToInt32(row["RFIDNO"].ToString()),
                            RFID_RDATA = row["RFID_RDATA"].ToString(),
                            RFID_WDATA = row["RFID_WDATA"].ToString(),
                            FLAG = row["FLAG"].ToString(),
                            COMMAND = row["COMMAND"].ToString(),
                            RWFLAG = row["RWFLAG"].ToString(),
                            RESULT = row["RESULT"].ToString(),
                            MEMO = row["MEMO"].ToString(),
                            CR_DATE = row["CR_DATE"].ToString(),
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

        //RFID 데이터 업데이트
        public void RFIDDataUpdate(string w_data, string flag, string command ,string rwFlag, string rw_gbn)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_RFID_UPDATE", connect);
                    cmd.Parameters.Add(new SqlParameter("W_DATA", w_data));
                    cmd.Parameters.Add(new SqlParameter("FLAG", flag));
                    cmd.Parameters.Add(new SqlParameter("COMMAND", command));
                    cmd.Parameters.Add(new SqlParameter("RWFLAG", rwFlag));
                    cmd.Parameters.Add(new SqlParameter("GBN", rw_gbn));
                    
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

        //RF 데이터 가져오기
        public string RFIDDataGet()
        {
            string rf_data = "";

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_RFID_DATA_EDIT", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.Rows[0];
                    rf_data = dr["RF_DATA"].ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return rf_data;
        }
    }
}
