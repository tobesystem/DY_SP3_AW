using dayouAWSWh2.Class;
using dayouAWSWh2.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace dayouAWSWh2.Data
{
    public class cCellData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cCellData()
        {
            _conn = _serverInfo.Server();
        }


        //셀 정보 조회
        public cCellList GetCell(string ID_BANK)
        {
            cCellList list = new cCellList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_TM_CELL_GET", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("ID_BANK", ID_BANK));

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new cCell
                        {
                            ID_BANK = row["ID_BANK"].ToString(),
                            ID_BAY = row["ID_BAY"].ToString(),
                            ID_LEVEL = row["ID_LEVEL"].ToString(),
                            ID_CODE = row["ID_CODE"].ToString(),
                            ID_MIXED = row["ID_MIXED"].ToString(),
                            STATUS = row["STATUS"].ToString(),
                            IN_SORT = Convert.ToInt32(row["IN_SORT"].ToString()),
                            ALC = row["ALC"].ToString(),
                            FRCODE = row["FRCODE"].ToString(),
                            ITEM_CODE1 = row["ITEM_CODE1"].ToString(),
                            ITEM_CODE2 = row["ITEM_CODE2"].ToString(),
                            ITEM_CODE3 = row["ITEM_CODE3"].ToString(),
                            ITEM_CODE4 = row["ITEM_CODE4"].ToString(),
                            ITEM_CODE5 = row["ITEM_CODE5"].ToString(),
                            LOT_NO1 = row["LOT_NO1"].ToString(),
                            LOT_NO2 = row["LOT_NO2"].ToString(),
                            LOT_NO3 = row["LOT_NO3"].ToString(),
                            LOT_NO4 = row["LOT_NO4"].ToString(),
                            LOT_NO5 = row["LOT_NO5"].ToString(),
                            PLT = row["PLT"].ToString(),
                            ID_DATE = row["ID_DATE"].ToString(),
                            ID_TIME = row["ID_TIME"].ToString(),
                            KIND = row["KIND"].ToString(),
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

        //셀 STATUS 리스트 조회
        public cCellList GetCellStatus(string TYPE)
        {
            cCellList list = new cCellList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CELL_STATUS", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("TYPE", TYPE));

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];

                    if (TYPE == "1")
                    {

                        foreach (DataRow row in dt.Rows)
                        {
                            list.Add(new cCell
                            {
                                STATUS = row["STATUS"].ToString(),
                            });
                        }
                    }
                    if (TYPE == "2")
                    {

                        foreach (DataRow row in dt.Rows)
                        {
                            list.Add(new cCell
                            {
                                STATUS = row["STATUS"].ToString(),
                            });
                        }
                    }
                    if (TYPE == "3")
                    {

                        foreach (DataRow row in dt.Rows)
                        {
                            list.Add(new cCell
                            {
                                FRCODE = row["FRCODE"].ToString(),
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return list;
        }

        //셀 사용현황 조회
        public cCellInfoList GetCellInfo()
        {
            cCellInfoList list = new cCellInfoList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CELL_INFO_GET", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new cCellInfo
                        {
                            IDBANK = row["IDBANK"].ToString(),
                            CELL_TOT = Convert.ToDecimal(row["CELL_TOT"].ToString()),
                            CELL_PERCENT = Convert.ToDecimal(row["CELL_PERCENT"].ToString()),
                            CELL_CNT1 =Convert.ToInt32(row["CELL_CNT1"].ToString()),
                            CELL_CNT2 =Convert.ToInt32(row["CELL_CNT2"].ToString()),
                            CELL_CNT3 =Convert.ToInt32(row["CELL_CNT3"].ToString()),
                            CELL_CNT4 =Convert.ToInt32(row["CELL_CNT4"].ToString()),
                            CELL_CNT5 =Convert.ToInt32(row["CELL_CNT5"].ToString()),
                            CELL_CNT6 =Convert.ToInt32(row["CELL_CNT6"].ToString()),
                            CELL_CNT7 =Convert.ToInt32(row["CELL_CNT7"].ToString()),
                      
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

        //셀 정보 업데이트
        public void AddCellInfo(string ID_CODE, string STATUS, string ALC_CODE, string FR_GUBUN, string LOT_NO1, 
            string LOT_NO2, string LOT_NO3, string LOT_NO4, string LOT_NO5, string PLT_CODE, string ID_DATE, string ID_TIME)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CELL_UPDATE", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("ID_CODE", ID_CODE));
                    cmd.Parameters.Add(new SqlParameter("STATUS", STATUS));
                    cmd.Parameters.Add(new SqlParameter("ALC_CODE", ALC_CODE));
                    cmd.Parameters.Add(new SqlParameter("FR_GUBUN", FR_GUBUN));
                    cmd.Parameters.Add(new SqlParameter("LOT_NO1", LOT_NO1));
                    cmd.Parameters.Add(new SqlParameter("LOT_NO2", LOT_NO2));
                    cmd.Parameters.Add(new SqlParameter("LOT_NO3", LOT_NO3));
                    cmd.Parameters.Add(new SqlParameter("LOT_NO4", LOT_NO4));
                    cmd.Parameters.Add(new SqlParameter("LOT_NO5", LOT_NO5));
                    cmd.Parameters.Add(new SqlParameter("PLT_CODE", PLT_CODE));
                    cmd.Parameters.Add(new SqlParameter("ID_DATE", ID_DATE));
                    cmd.Parameters.Add(new SqlParameter("ID_TIME", ID_TIME));

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


        // 셀 ALC 리스트 가져오기
        public cCellList GetAlcCode(string TYPE)
        {
            cCellList list = new cCellList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CELL_STATUS", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("TYPE", TYPE));

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new cCell
                        {
                            ALC = row["ALC"].ToString(),
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
