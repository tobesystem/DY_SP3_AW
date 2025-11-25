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
    internal class cProdData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cProdData()
        {
            _conn = _serverInfo.Server();
        }

        public cProdItemList getProdList(string _alcClass, string _alcCode, string _bodyNo, string _cmtNo, string _status)
        {
            cProdItemList _list = new cProdItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_PROD_LIST_GET", connect);
                    cmd.Parameters.Add(new SqlParameter("ALC_CLASS", _alcClass));
                    cmd.Parameters.Add(new SqlParameter("ALC_CODE", _alcCode));
                    cmd.Parameters.Add(new SqlParameter("BODY_NO", _bodyNo));
                    cmd.Parameters.Add(new SqlParameter("CMT_NO", _cmtNo));
                    cmd.Parameters.Add(new SqlParameter("STATUS", _status));

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cProdItem
                        {
                            ROW_NUM = row["ROW_NUM"].ToString(),
                            REPORT_DT = row["REPORT_DT"].ToString(),
                            CMT_NO = row["COMMIT_NO"].ToString(),
                            ALC_CODE = row["ALC_CODE"].ToString(),
                            ALC_CHECK = row["ALC_CHECK"].ToString(),
                            BODY_NO = row["BODY_NO"].ToString(),
                            HANGUEL_PART = row["HANGUEL_PART"].ToString(),
                            REGION_NAME = row["REGION_NAME"].ToString(),
                            INNER_COLOR = row["INNER_COLOR"].ToString(),
                            ALC_CLASS = row["ALC_CLASS"].ToString(),
                            STOCK_FR = row["STOCK_FR"].ToString(),
                            STOCK_R2 = row["STOCK_R2"].ToString(),
                            STOCK_R3 = row["STOCK_R3"].ToString(),
                            CR_DATE = row["CR_DATE"].ToString(),
                            POP_SEND = row["POP_SEND"].ToString(),
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

        //도장 실적 조회
        public cProdItemList getProdResultList(string date1, string date2, string date3, string date4, string alc_code, string alc_class, string body_no, string _cmtNo, string _status)
        {
            cProdItemList _list = new cProdItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_HOST_RESULT_GET", connect);
                    cmd.Parameters.Add(new SqlParameter("START_DATETIME_SEC", date1));
                    cmd.Parameters.Add(new SqlParameter("STOP_DATETIME_SEC", date2));
                    cmd.Parameters.Add(new SqlParameter("HOST_START_DATETIME", date3));
                    cmd.Parameters.Add(new SqlParameter("HOST_STOP_DATETIME", date4));
                    cmd.Parameters.Add(new SqlParameter("ALC_CODE", alc_code));
                    cmd.Parameters.Add(new SqlParameter("ALC_CLASS", alc_class));
                    cmd.Parameters.Add(new SqlParameter("BODY_NO", body_no));
                    cmd.Parameters.Add(new SqlParameter("CMT_NO", _cmtNo));
                    cmd.Parameters.Add(new SqlParameter("STATUS", _status));
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cProdItem
                        {
                            ROW_NUM = row["ROW_NUM"].ToString(),
                            COMPLETE_DT = row["COMPLETE_DT"].ToString(),
                            REPORT_DT = row["REPORT_DT"].ToString(),
                            CMT_NO = row["CMT_NO"].ToString(),
                            ALC_CODE = row["ALC_CODE"].ToString(),
                            ALC_CHECK = row["ALC_CHECK"].ToString(),
                            BODY_NO = row["BODY_NO"].ToString(),
                            HANGUEL_PART = row["HANGUEL_PART"].ToString(),
                            REGION_NAME = row["REGION_NAME"].ToString(),
                            DRIVE_TYPE = row["DRIVE_TYPE"].ToString(),
                            BODY_COLOR = row["BODY_COLOR"].ToString(),
                            INNER_COLOR = row["INNER_COLOR"].ToString(),
                            ALC_CLASS = row["ALC_CLASS"].ToString(),
                            REPORT_SYS_DT = row["REPORT_SYS_DT"].ToString(),
                            STATUS = row["STATUS"].ToString(),


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

        //도장 삭제
        public cMessage delProd(string sys_dt, string cmt_no)
        {
            cMessage _items = new cMessage();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_PROD_LIST_DEL", connect);
                    cmd.Parameters.Add(new SqlParameter("REPORT_SYS_DT", sys_dt));
                    cmd.Parameters.Add(new SqlParameter("CMT_NO", cmt_no));

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    _items.MSG = dt.Rows[0]["MSG"].ToString();
                    _items.RESULT = dt.Rows[0]["RESULT"].ToString();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _items;
        }
    }
}
