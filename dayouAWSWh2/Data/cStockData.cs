using dayouAWSWh2.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dayouAWSWh2.Server;
using System.IO;

namespace dayouAWSWh2.Data
{
    internal class cStockData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cStockData()
        {
            _conn = _serverInfo.Server();
        }

        //적재코드별 재고현황
        public cCodeStockItemList getCodeStockList(string alc_code, string alc_class)
        {
            cCodeStockItemList _list = new cCodeStockItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CODE_STOCK_GET", connect);
                    cmd.Parameters.Add(new SqlParameter("ALC_CODE", alc_code));
                    cmd.Parameters.Add(new SqlParameter("ALC_CLASS", alc_class));
                   
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cCodeStockItem
                        {
                            ALC_CODE = row["ALC_CODE"].ToString(),
                            ALC_CLASS = row["ALC_CLASS"].ToString(),
                            ALC_TYPE = row["ALC_TYPE"].ToString(),
                            //SUM = Convert.ToInt32(row["SUM"]),
                            FR_CNT = Convert.ToInt32(row["FR_CNT"]),
                            R2_CNT = Convert.ToInt32(row["R2_CNT"]),
                            R3_CNT = Convert.ToInt32(row["R3_CNT"]),
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

        //적재코드별 재고현황 세부
        public cCodeStockItemList getCodeStockSubList(string alc_code)
        {
            cCodeStockItemList _list = new cCodeStockItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CODE_STOCK_DETAIL", connect);
                    cmd.Parameters.Add(new SqlParameter("ALC_CODE", alc_code));
                    
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cCodeStockItem
                        {
                            LOT_NO1 = row["LOT_NO1"].ToString(),
                            LOT_NO2 = row["LOT_NO2"].ToString(),
                            LOT_NO3 = row["LOT_NO3"].ToString(),
                            LOT_NO4 = row["LOT_NO4"].ToString(),
                            IN_DATE = row["IN_DATE"].ToString(),
                            ID_BANK = row["ID_BANK"].ToString(),
                            ID_BAY = row["ID_BAY"].ToString(),
                            ID_LEVEL = row["ID_LEVEL"].ToString(),
                            FR_GUBUN = row["FR_GUBUN"].ToString(),
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

        //적재위치별 재고현황
        public cLocStockItemList getLocStockList(string id_bank, string id_bay, string id_level, string cell_status, string car_code, string plt_code, string alc_code)
        {
            cLocStockItemList _list = new cLocStockItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_LOC_STOCK_GET", connect);
                    cmd.Parameters.Add(new SqlParameter("ID_BANK", id_bank));
                    cmd.Parameters.Add(new SqlParameter("ID_BAY", id_bay));
                    cmd.Parameters.Add(new SqlParameter("ID_LEVEL", id_level));
                    cmd.Parameters.Add(new SqlParameter("CELL_STATUS", cell_status));
                    cmd.Parameters.Add(new SqlParameter("ALC_CLASS", car_code));
                    cmd.Parameters.Add(new SqlParameter("PLT_CODE", plt_code));
                    cmd.Parameters.Add(new SqlParameter("ALC_CODE", alc_code));

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cLocStockItem
                        {
                           ALC_CODE = row["ALC_CODE"].ToString(),
                            ID_BANK = row["ID_BANK"].ToString(),
                            ID_BAY = row["ID_BAY"].ToString(),
                            ID_LEVEL = row["ID_LEVEL"].ToString(),
                            ITEM_CODE1 = row["ITEM_CODE1"].ToString(),
                            ITEM_CODE2 = row["ITEM_CODE2"].ToString(),
                            ITEM_CODE3 = row["ITEM_CODE3"].ToString(),
                            ITEM_CODE4 = row["ITEM_CODE4"].ToString(),
                            LOT_NO1 = row["LOT_NO1"].ToString(),
                            LOT_NO2 = row["LOT_NO2"].ToString(),
                            LOT_NO3 = row["LOT_NO3"].ToString(),
                            LOT_NO4 = row["LOT_NO4"].ToString(),
                            STATUS = row["STATUS"].ToString(),
                            PLT_CODE = row["PLT_CODE"].ToString(),
                            CELL = row["CELL"].ToString(),
                            FR_GUBUN = row["FR_GUBUN"].ToString(),
                            IN_DATE = row["IN_DATE"].ToString(),
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

        //작업시간별 작업실적
        public cTimeStockItemList getTimeStockList(string start_date )
        {
            cTimeStockItemList _list = new cTimeStockItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {

                    SqlCommand cmd = new SqlCommand("SP_CS_TIME_STOCK_LIST_GET", connect);
                    cmd.Parameters.Add(new SqlParameter("START_DATE", start_date));
                  

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cTimeStockItem
                        {
                            Round_Time = row["Round_Time"].ToString(),
                            Round_Type = row["Round_Type"].ToString(),
                            IN_Count = row["IN_Count"].ToString(),
                            OT_Count = row["OT_Count"].ToString(),
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

        //작업기간별 작업실적
        public cDateStockItemList getDateStockList(string start_date, string stop_date, string car_code, string fr_gbn)
        {
            cDateStockItemList _list = new cDateStockItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = connect.CreateCommand();
                    
                    cmd = new SqlCommand("SP_CS_DATE_STOCK_LIST_GET", connect);
                    
                    cmd.Parameters.Add(new SqlParameter("START_DATETIME", start_date));
                    cmd.Parameters.Add(new SqlParameter("STOP_DATETIME", stop_date));
                    cmd.Parameters.Add(new SqlParameter("ALC_CLASS", car_code));
                    cmd.Parameters.Add(new SqlParameter("FR_GUBUN", fr_gbn));


                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cDateStockItem
                        {
                            ALC_CODE = row["ALC_CODE"].ToString(),
                            IN_CNT = row["IN_CNT"].ToString(),
                            OT_CNT = row["OT_CNT"].ToString(),
                            ST_CNT = row["ST_CNT"].ToString(),
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

        ////도장실적 조회
        //public cProdItemList getProdResultList(string date1, string date2 , string alc_code, string body_no)
        //{
        //    cProdItemList _list = new cProdItemList();

        //    try
        //    {
        //        using (SqlConnection connect = new SqlConnection(_conn))
        //        {
        //            SqlCommand cmd = new SqlCommand("SP_CS_PROD_RESULT_GET", connect);
        //            cmd.Parameters.Add(new SqlParameter("START_DATETIME_SEC", date1));
        //            cmd.Parameters.Add(new SqlParameter("STOP_DATETIME_SEC", date2));
        //            cmd.Parameters.Add(new SqlParameter("ALC_CODE", alc_code));
        //            cmd.Parameters.Add(new SqlParameter("BODY_NO", body_no));


        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //            connect.Open();

        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            DataSet ds = new DataSet();

        //            adapter.Fill(ds);

        //            DataTable dt = ds.Tables[0];
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                _list.Add(new cProdItem
        //                {
        //                    COMMIT_NO = row["COMMIT_NO"].ToString(),
        //                    ALC_CODE = row["ALC_CODE"].ToString(),
        //                    ALC_CLASS = row["ALC_CLASS"].ToString(),
        //                    REPORT_SYS_DT = row["REPORT_SYS_DT"].ToString(),
        //                    DRIVE_TYPE = row["DRIVE_TYPE"].ToString(),
        //                    BODY_NO = row["BODY_NO"].ToString(),
        //                    REGION_NAME = row["REGION_NAME"].ToString(),
        //                    PROD_STATUS_NM = row["PROD_STATUS_NM"].ToString(),
        //                });
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }

        //    return _list;
        //}

        ////의장실적 조회
        //public cHostItemList getHostResultList(string date1, string date2, string alc_code, string alc_class, string lot_no, string commit_no, string date3, string date4, string status)
        //{
        //    cHostItemList _list = new cHostItemList();

        //    try
        //    {
        //        using (SqlConnection connect = new SqlConnection(_conn))
        //        {
        //            SqlCommand cmd = new SqlCommand("SP_CS_HOST_RESULT_GET", connect);
        //            cmd.Parameters.Add(new SqlParameter("START_DATETIME_SEC", date1));
        //            cmd.Parameters.Add(new SqlParameter("STOP_DATETIME_SEC", date2));
        //            cmd.Parameters.Add(new SqlParameter("ALC_CODE", alc_code));
        //            cmd.Parameters.Add(new SqlParameter("ALC_CLASS", alc_class));
        //            cmd.Parameters.Add(new SqlParameter("LOT_NO", lot_no));
        //            cmd.Parameters.Add(new SqlParameter("COMMIT_NO", commit_no));
        //            cmd.Parameters.Add(new SqlParameter("HOST_START_DATETIME", date3));
        //            cmd.Parameters.Add(new SqlParameter("HOST_STOP_DATETIME", date4));
        //            cmd.Parameters.Add(new SqlParameter("STATUS", status));


        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //            connect.Open();

        //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        //            DataSet ds = new DataSet();

        //            adapter.Fill(ds);

        //            DataTable dt = ds.Tables[0];
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                _list.Add(new cHostItem
        //                {
        //                   ROW_NUM = row["ROW_NUM"].ToString(),
        //                   COMMIT_NO = row["COMMIT_NO"].ToString(),
        //                   BODY_NO = row["BODY_NO"].ToString(),
        //                   HIS_DATE = row["HIS_DATE"].ToString(),
        //                   BR_NO = row["BR_NO"].ToString(),
        //                   DRIVE_TYPE = row["DRIVE_TYPE"].ToString(),
        //                   REGION_NAME = row["REGION_NAME"].ToString(),
        //                   ORDER_NO = row["ORDER_NO"].ToString(),
        //                   LOT_NO1 = row["LOT_NO1"].ToString(),
        //                   LOT_NO2 = row["LOT_NO2"].ToString(),
        //                   LOT_NO3 = row["LOT_NO3"].ToString(),
        //                   IN_DATE = row["IN_DATE"].ToString(),
        //                   REPORT_SYS_DTS = row["REPORT_SYS_DTS"].ToString(),
        //                   FR_GUBUN_DESC = row["FR_GUBUN_DESC"].ToString(),
        //                });
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }

        //    return _list;
        //}

        //적재별 실적조회
        public cLoadPerfItemList getLoadPerfList(string start_date, string stop_date, string kind, string load_code, string region, string cover, string fr_gubun, string alc_no)
        {
            cLoadPerfItemList _list = new cLoadPerfItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_LOAD_PERF_GET", connect);

                    cmd.Parameters.Add(new SqlParameter("START_DATE", start_date));
                    cmd.Parameters.Add(new SqlParameter("STOP_DATE", stop_date));
                    cmd.Parameters.Add(new SqlParameter("KIND", kind));
                    cmd.Parameters.Add(new SqlParameter("LOAD_CODE", load_code));
                    cmd.Parameters.Add(new SqlParameter("REGION", region));
                    cmd.Parameters.Add(new SqlParameter("COVER", cover));
                    cmd.Parameters.Add(new SqlParameter("FR_GUBUN", fr_gubun));
                    cmd.Parameters.Add(new SqlParameter("ALC_PART_NO", alc_no));


                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cLoadPerfItem
                        {
                            ROWNUM = Convert.ToInt32(row["ROWNUM"]),
                            LOAD_CODE = row["LOAD_CODE"].ToString(),
                            FR_GUBUN = row["FR_GUBUN"].ToString(),
                            IN_CNT = Convert.ToInt32(row["IN_CNT"]),
                            OT_CNT = Convert.ToInt32(row["OT_CNT"]),
                            ST_CNT = Convert.ToInt32(row["ST_CNT"]),
                            KIND = row["KIND"].ToString(),
                            COVER_COLOR = row["COVER_COLOR"].ToString(),
                            COVER_SET = row["COVER_SET"].ToString(),
                            DRIVER_TYPE = row["DRIVER_TYPE"].ToString(),
                            REGION = row["REGION"].ToString(),
                            DRIVER = row["DRIVER"].ToString(),
                            PASSENGER = row["PASSENGER"].ToString(),
                            SECOND_LH = row["SECOND_LH"].ToString(),
                            SECOND_CH = row["SECOND_CH"].ToString(),
                            SECOND_RH = row["SECOND_RH"].ToString(),
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

        //품목별 실적조회
        public cLoadItemPerfItemList getLoadItemPerfList(string start_date, string stop_date, string load_code)
        {
            cLoadItemPerfItemList _list = new cLoadItemPerfItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_LOAD_ITEM_PERF_GET", connect);

                    cmd.Parameters.Add(new SqlParameter("START_DATE", start_date));
                    cmd.Parameters.Add(new SqlParameter("STOP_DATE", stop_date));
                    cmd.Parameters.Add(new SqlParameter("LOAD_CODE", load_code));


                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cLoadItemPerfItem
                        {
                          LOAD_CODE = row["LOAD_CODE"].ToString(),
                          REGION = row["REGION"].ToString(),
                          ID_INDEX = Convert.ToInt32(row["ID_INDEX"]),
                          ITEM_CODE1 = row["ITEM_CODE1"].ToString(),
                          ITEM_CODE2 = row["ITEM_CODE2"].ToString(),
                          ITEM_CODE3 = row["ITEM_CODE3"].ToString(),
                          ITEM_CODE4 = row["ITEM_CODE4"].ToString(),
                          GRADE = row["GRADE"].ToString(),
                          ID_TYPE = row["ID_TYPE"].ToString(),
                          ID_BANK = row["ID_BANK"].ToString(),
                          ID_BAY = row["ID_BAY"].ToString(),
                          ID_LEVEL = row["ID_LEVEL"].ToString(),
                          LOT_NO1 = row["LOT_NO1"].ToString(),
                          LOT_NO2 = row["LOT_NO2"].ToString(),
                          LOT_NO3 = row["LOT_NO3"].ToString(),
                          LOT_NO4= row["LOT_NO4"].ToString(),
                          PLT_CODE = row["PLT_CODE"].ToString(),
                          COMMIT_NO = row["COMMIT_NO"].ToString(),
                          OUTDAte = row["OUTDAte"].ToString(),
                          INDAte = row["INDAte"].ToString(),
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

        //입고대 실적조회
        public cTotalJobItemList getTotalJobList(string start_date, string stop_date, string fr_gbn, string alc_code)
        {
            cTotalJobItemList _list = new cTotalJobItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_TOTAL_JOB_GET", connect);

                    cmd.Parameters.Add(new SqlParameter("START_DATETIME", start_date));
                    cmd.Parameters.Add(new SqlParameter("STOP_DATETIME", stop_date));
                    cmd.Parameters.Add(new SqlParameter("FR_GBN", fr_gbn));
                    cmd.Parameters.Add(new SqlParameter("ALC_CODE", alc_code));


                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cTotalJobItem
                        {
                            ROW_NUM = Convert.ToInt32(row["ROW_NUM"]),
                            IN_DT = row["IN_DT"].ToString(),
                            LOAD_CODE = row["LOAD_CODE"].ToString(),
                            LOT_NO1 = row["LOT_NO1"].ToString(),
                            LOT_NO2 = row["LOT_NO2"].ToString(),
                            LOT_NO3 = row["LOT_NO3"].ToString(),
                            FR_GUBUN = row["FR_GUBUN"].ToString(),
                            PALLET_CODE = row["PALLET_CODE"].ToString(),
                            ALC_CLASS = row["ALC_CLASS"].ToString(),
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
