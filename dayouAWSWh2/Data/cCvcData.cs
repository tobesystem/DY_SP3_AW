using dayouAWSWh2.Class;
using dayouAWSWh2.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace dayouAWSWh2.Data
{
    public class cCvcData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cCvcData()
        {
            _conn = _serverInfo.Server();
        }

        //적재코드 조회
        public cAlcList AlcCodeGet()
        {
            cAlcList list = new cAlcList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_ALCC_GET", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new cAlcItem
                        {
                            ALC_CODE = row["ALC_CODE"].ToString(),
                            ALC_CLASS = row["ALC_CLASS"].ToString(),
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

        //컨베이어 조회
        public cCvcList GetCvc()
        {
            cCvcList list = new cCvcList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CVC_GET", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new cCvc
                        {
                            BUFF_NO = row["BUFF_NO"].ToString(),
                            ERROR_VALUE = row["ERROR_VALUE"].ToString(),
                            READY_VALUE = row["READY_VALUE"].ToString(),
                            CARGO_VALUE = row["CARGO_VALUE"].ToString(),
                            DATA_VALUE = row["DATA_VALUE"].ToString(),
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

        //컨베이어 조회(SP3)
        public cCvcList Wh2CvcGet()
        {
            cCvcList list = new cCvcList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CVC_GET_WH2", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new cCvc
                        {
                            BUFF_NO = row["BUFF_NO"].ToString(),
                            ERROR_VALUE = row["ERROR_VALUE"].ToString(),
                            READY_VALUE = row["READY_VALUE"].ToString(),
                            CARGO_VALUE = row["CARGO_VALUE"].ToString(),
                            DATA_VALUE = row["DATA_VALUE"].ToString(),
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

        //CVC 이동
        public cCvc CvMove(int ID_BUFF, int ID_BUFF_TARGET)
        {
            cCvc item = new cCvc();
            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CVC_MOVE", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("ID_BUFF", ID_BUFF));
                    cmd.Parameters.Add(new SqlParameter("ID_BUFF_TARGET", ID_BUFF_TARGET));

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        item.RESULT = row["RESULT"].ToString();
                        item.MSG = row["MSG"].ToString();
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return item;
        }

        //CVC 이동(SP3)
        public cCvc Wh2CvMove(int ID_BUFF, int ID_BUFF_TARGET)
        {
            cCvc item = new cCvc();
            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CVC_MOVE_WH2", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("ID_BUFF", ID_BUFF));
                    cmd.Parameters.Add(new SqlParameter("ID_BUFF_TARGET", ID_BUFF_TARGET));

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        item.RESULT = row["RESULT"].ToString();
                        item.MSG = row["MSG"].ToString();
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return item;
        }

        //CVC 삭제
        public void CvDel(int ID_BUFF)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CVC_DEL", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("ID_BUFF", ID_BUFF));

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

        //CVC 삭제(SP3)
        public void Wh2CvDel(int ID_BUFF)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CVC_DEL_WH2", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("ID_BUFF", ID_BUFF));

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

        //CVC 교환
        public cCvc CvSwap(int ID_BUFF, int ID_BUFF_TARGET)
        {
            cCvc item = new cCvc();
            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CVC_SWAP", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("ID_BUFF", ID_BUFF));
                    cmd.Parameters.Add(new SqlParameter("ID_BUFF_TARGET", ID_BUFF_TARGET));

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        item.RESULT = row["RESULT"].ToString();
                        item.MSG = row["MSG"].ToString();
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return item;
        }

        //CVC 교환(SP3)
        public cCvc Wh2CvSwap(int ID_BUFF, int ID_BUFF_TARGET)
        {
            cCvc item = new cCvc();
            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CVC_SWAP_WH2", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("ID_BUFF", ID_BUFF));
                    cmd.Parameters.Add(new SqlParameter("ID_BUFF_TARGET", ID_BUFF_TARGET));

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        item.RESULT = row["RESULT"].ToString();
                        item.MSG = row["MSG"].ToString();
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return item;
        }

        //CVC 지시
        public void OrderCv(int ID_BUFF, string TYPE)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CVC_ORDER", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("ID_BUFF", ID_BUFF));
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

        //CVC 지시(SP3)
        public void Wh2OrderCv(int ID_BUFF, string TYPE)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_CVC_ORDER_WH2", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("ID_BUFF", ID_BUFF));
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

    }
}
