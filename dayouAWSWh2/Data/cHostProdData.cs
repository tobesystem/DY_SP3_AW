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
    public class cHostProdData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cHostProdData()
        {
            _conn = _serverInfo.Server();
        }

        //의장/도장 정보 조회
        public cHostProdItem HostProdGet()
        {
            cHostProdItem item = new cHostProdItem();
            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_HOST_INFO_GET", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        item.HOST_CR_DATE = row["HOST_CR_DATE"].ToString();
                        item.PROD_CR_DATE = row["PROD_CR_DATE"].ToString();
                        item.HOST_OPTION = Convert.ToInt32(row["HOST_OPTION"]);
                        item.PROD_OPTION = Convert.ToInt32(row["PROD_OPTION"]);

                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return item;
        }

        //의장 정보 조회(SP3)
        public cHostProdItem Wh2HostProdGet()
        {
            cHostProdItem item = new cHostProdItem();
            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_HOST_INFO_GET_WH2", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        item.HOST_CR_DATE = row["HOST_CR_DATE"].ToString();
                        item.PROD_CR_DATE = row["PROD_CR_DATE"].ToString();
                        item.HOST_OPTION = Convert.ToInt32(row["HOST_OPTION"]);
                        item.PROD_OPTION = Convert.ToInt32(row["PROD_OPTION"]);

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
