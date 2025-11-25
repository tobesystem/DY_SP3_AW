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
    internal class cManagerOutData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cManagerOutData()
        {
            _conn = _serverInfo.Server();
        }

        public cManagerOutItemList getManagerOutList(string in_date1, string in_date2, string id_bank, string id_bay, string id_level, string car_code, string load_code, string lot_no, string fr_gbn)
        {
            cManagerOutItemList _list = new cManagerOutItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_MANAGER_OUT_GET", connect); 
                    cmd.Parameters.Add(new SqlParameter("IN_DATE1", in_date1));
                    cmd.Parameters.Add(new SqlParameter("IN_DATE2", in_date2));
                    cmd.Parameters.Add(new SqlParameter("ID_BANK", id_bank));
                    cmd.Parameters.Add(new SqlParameter("ID_BAY", id_bay));
                    cmd.Parameters.Add(new SqlParameter("ID_LEVEL", id_level));
                    cmd.Parameters.Add(new SqlParameter("CAR_CODE", car_code));
                    cmd.Parameters.Add(new SqlParameter("LOAD_CODE", load_code));
                    cmd.Parameters.Add(new SqlParameter("LOT_NO", lot_no));
                    cmd.Parameters.Add(new SqlParameter("FR_GUBUN", fr_gbn));

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cManagerOutItem
                        {
                            IN_DATE = row["IN_DATE"].ToString(),
                            KIND = row["KIND"].ToString(),
                            PLT = row["PLT"].ToString(),
                            ID_BANK = row["ID_BANK"].ToString(),
                            ID_BAY = row["ID_BAY"].ToString(),
                            ID_LEVEL = row["ID_LEVEL"].ToString(),
                            LOT_NO1 = row["LOT_NO1"].ToString(),
                            LOT_NO2 = row["LOT_NO1"].ToString(),
                            LOT_NO3 = row["LOT_NO1"].ToString(),
                            ALC = row["ALC"].ToString(),
                            ROW_NUM = row["ROW_NUM"].ToString(),
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

        public cManagerOutItemList ManagerOutAdd(string id_bank, string id_bay, string id_level)
        {
            cManagerOutItemList _list = new cManagerOutItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_WMS_OUT_ONE_MANUAL_ORDER_CREATE", connect);
                    cmd.Parameters.Add(new SqlParameter("ID_BANK", id_bank));
                    cmd.Parameters.Add(new SqlParameter("ID_BAY", id_bay));
                    cmd.Parameters.Add(new SqlParameter("ID_LEVEL", id_level));
                   

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cManagerOutItem
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
    }
}
