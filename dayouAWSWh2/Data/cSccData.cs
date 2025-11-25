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
    public class cSccData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cSccData()
        {
            _conn = _serverInfo.Server();
        }

        //호기 조회
        public cSccList GetSccNo()
        {
            cSccList list = new cSccList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_SCC_NO_GET", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new cSccItem
                        {
                            SCC_NO = Convert.ToInt32(row["SCC_NO"].ToString()),
                            SCC_NAME = row["SCC_NAME"].ToString(),
                           
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


        //크레인 정보 조회
        public cSccList GetSccData(int SCC_NO)
        {
            cSccList list = new cSccList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_SCC_INFO_GET", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("SCC_NO", SCC_NO));
                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new cSccItem
                        {
                            WBIT = Convert.ToInt32(row["WBIT"].ToString()),
                            NOW_VALUE = row["NOW_VALUE"].ToString(),
                            IO_TYPE = row["IO_TYPE"].ToString(),
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


        // 크레인 작업진행
        public void SccCurrentAdd(int SCC_NO, string TYPE)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_SCC_CURRENT", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("SCC_NO", SCC_NO));
                    cmd.Parameters.Add(new SqlParameter("TYPE", TYPE));

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
    
        //SCIO 정보 조회
        public cSccScioItem ScioGet(int SCC_NO)
        {
            cSccScioItem item = new cSccScioItem();
            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_SCC_SCIO_GET", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("SCC_NO", SCC_NO));

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        item.ID_DATE = row["ID_DATE"].ToString();
                        item.ID_TIME = row["ID_TIME"].ToString();
                        item.ID_INDEX = Convert.ToInt32(row["ID_INDEX"].ToString());
                        item.ID_SUBIDX = Convert.ToInt32(row["ID_SUBIDX"].ToString());
                        item.IO_TYPE = row["IO_TYPE"].ToString();
                        item.ID_SC = row["ID_SC"].ToString();
                        item.LOAD_BANK = row["LOAD_BANK"].ToString();
                        item.LOAD_BAY = row["LOAD_BAY"].ToString();
                        item.LOAD_LEVEL = row["LOAD_LEVEL"].ToString();
                        item.UNLOAD_BANK = row["UNLOAD_BANK"].ToString();
                        item.UNLOAD_BAY = row["UNLOAD_BAY"].ToString();
                        item.UNLOAD_LEVEL = row["UNLOAD_LEVEL"].ToString();
                        item.SC_STATUS = row["SC_STATUS"].ToString();
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
