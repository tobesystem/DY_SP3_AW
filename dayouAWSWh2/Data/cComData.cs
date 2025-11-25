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
    public class cComData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cComData()
        {
            _conn = _serverInfo.Server();
        }


        //SCIO 정보 조회
        public cComStatus ComStatusGet()
        {
            cComStatus item = new cComStatus();
            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_COM_STATUS", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        item.CVC = row["CVC"].ToString();
                        item.SCC = row["SCC"].ToString();
                        item.BR1 = row["BR1"].ToString();
                        item.BR2 = row["BR2"].ToString();
                        item.BR3 = row["BR3"].ToString();
                        item.BR4 = row["BR4"].ToString();
                        item.RF1 = row["RF1"].ToString();
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return item;
        }

        //SCIO 정보 조회(SP3)
        public cComStatus Wh2ComStatusGet()
        {
            cComStatus item = new cComStatus();
            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_COM_STATUS_WH2", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        item.CVC = row["CVC"].ToString();
                        item.SCC = row["SCC"].ToString();
                        item.BR1 = row["BR1"].ToString();
                        item.BR2 = row["BR2"].ToString();
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return item;
        }

    }
}
