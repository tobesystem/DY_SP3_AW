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
    public class cArrowData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cArrowData()
        {
            _conn = _serverInfo.Server();
        }

        // CVC 지시정보 조회
        public cArrowList ArrowGet()
        {
            cArrowList list = new cArrowList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CVC_ARROW_GET", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new cArrowItem
                        {
                            WADDR = row["WADDR"].ToString(),
                            BUFF_NO = row["BUFF_NO"].ToString(),
                            USE_DESC = row["USE_DESC"].ToString(),
                            NOW_VALUE = Convert.ToInt32(row["NOW_VALUE"]),
                            WBIT = Convert.ToInt32(row["WBIT"]),
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

        // CVC 지시정보 조회(SP3)
        public cArrowList Wh2ArrowGet()
        {
            cArrowList list = new cArrowList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CVC_ARROW_GET_WH2", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new cArrowItem
                        {
                            WADDR = row["WADDR"].ToString(),
                            BUFF_NO = row["BUFF_NO"].ToString(),
                            USE_DESC = row["USE_DESC"].ToString(),
                            NOW_VALUE = Convert.ToInt32(row["NOW_VALUE"]),
                            WBIT = Convert.ToInt32(row["WBIT"]),
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
