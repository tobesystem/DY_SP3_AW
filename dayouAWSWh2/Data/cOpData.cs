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
    public class cOpData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cOpData()
        {
            _conn = _serverInfo.Server();
        }

        //OP,MOP 조회
        public cOpList OpGet()
        {
            cOpList list = new cOpList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_OP_GET", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new cOpItem
                        {
                            WADDR = row["WADDR"].ToString(),
                            WBIT = Convert.ToInt32(row["WBIT"]),
                            BUFF_STATUS = row["BUFF_STATUS"].ToString(),
                            USE_DESC = row["USE_DESC"].ToString(),
                            NOW_VALUE = Convert.ToInt32(row["NOW_VALUE"]),
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

        //OP,MOP 조회(SP3)
        public cOpList Wh2OpGet()
        {
            cOpList list = new cOpList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_OP_GET_WH2", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new cOpItem
                        {
                            WADDR = row["WADDR"].ToString(),
                            WBIT = Convert.ToInt32(row["WBIT"]),
                            BUFF_STATUS = row["BUFF_STATUS"].ToString(),
                            USE_DESC = row["USE_DESC"].ToString(),
                            NOW_VALUE = Convert.ToInt32(row["NOW_VALUE"]),
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
