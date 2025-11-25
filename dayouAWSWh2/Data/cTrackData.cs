using dayouAWSWh2.Class;
using dayouAWSWh2.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace dayouAWSWh2.Data
{
    public class cTrackData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cTrackData()
        {
            _conn = _serverInfo.Server();
        }

        public cTrack TrackGet(int ID_BUFF)
        {
            cTrack _items = new cTrack();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_TRACK_BUFF_GET", connect);
                    
                    cmd.Parameters.Add(new SqlParameter("ID_BUFF", ID_BUFF));

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        _items.ID_BUFF = Convert.ToInt32(row["ID_BUFF"].ToString());
                        _items.ID_DATA = row["ID_DATA"].ToString();
                        _items.ID_TYPE = row["ID_TYPE"].ToString();
                        _items.ORDER_DATE = row["ORDER_DATE"].ToString();
                        _items.ORDER_TIME = row["ORDER_TIME"].ToString();
                        _items.ORDER_INDEX = Convert.ToInt32(row["ORDER_INDEX"].ToString());
                        _items.ORDER_SUBIDX = Convert.ToInt32(row["ORDER_SUBIDX"].ToString());
                        _items.ID_SC = row["ID_SC"].ToString();
                        _items.ID_BANK = row["ID_BANK"].ToString();
                        _items.ID_BAY= row["ID_BAY"].ToString();
                        _items.ID_LEVEL= row["ID_LEVEL"].ToString();
                        _items.LOAD_STATUS= row["LOAD_STATUS"].ToString();
                        _items.LOAD_CODE= row["LOAD_CODE"].ToString();
                        _items.ITEM_CODE1= row["ITEM_CODE1"].ToString();
                        _items.ITEM_CODE2= row["ITEM_CODE1"].ToString();
                        _items.ITEM_CODE3= row["ITEM_CODE1"].ToString();
                        _items.ITEM_CODE4= row["ITEM_CODE1"].ToString();
                        _items.ITEM_CODE5= row["ITEM_CODE1"].ToString();
                        _items.LOT_NO1= row["LOT_NO1"].ToString();
                        _items.LOT_NO2= row["LOT_NO2"].ToString();
                        _items.LOT_NO3= row["LOT_NO3"].ToString();
                        _items.LOT_NO4= row["LOT_NO4"].ToString();
                        _items.LOT_NO5= row["LOT_NO5"].ToString();
                        _items.PALLET_CODE= row["PALLET_CODE"].ToString();
                        _items.COMMIT_NO= row["COMMIT_NO"].ToString();
                        _items.ID_MEMO= row["ID_MEMO"].ToString();
                        _items.COMMIT_NO = row["COMMIT_NO"].ToString();
                        _items.BODY_NO = row["BODY_NO"].ToString() ;
                    }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _items;
        }

        public cTrack Wh2TrackGet(int ID_BUFF)
        {
            cTrack _items = new cTrack();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_TRACK_BUFF_GET_WH2", connect);

                    cmd.Parameters.Add(new SqlParameter("ID_BUFF", ID_BUFF));

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        _items.ID_BUFF = Convert.ToInt32(row["ID_BUFF"].ToString());
                        _items.ID_DATA = row["ID_DATA"].ToString();
                        _items.ID_TYPE = row["ID_TYPE"].ToString();
                        _items.ORDER_DATE = row["ORDER_DATE"].ToString();
                        _items.ORDER_TIME = row["ORDER_TIME"].ToString();
                        _items.ORDER_INDEX = Convert.ToInt32(row["ORDER_INDEX"].ToString());
                        _items.ORDER_SUBIDX = Convert.ToInt32(row["ORDER_SUBIDX"].ToString());
                        _items.ID_SC = row["ID_SC"].ToString();
                        _items.ID_BANK = row["ID_BANK"].ToString();
                        _items.ID_BAY = row["ID_BAY"].ToString();
                        _items.ID_LEVEL = row["ID_LEVEL"].ToString();
                        _items.LOAD_STATUS = row["LOAD_STATUS"].ToString();
                        _items.LOAD_CODE = row["LOAD_CODE"].ToString();
                        _items.ITEM_CODE1 = row["ITEM_CODE1"].ToString();
                        _items.ITEM_CODE2 = row["ITEM_CODE1"].ToString();
                        _items.ITEM_CODE3 = row["ITEM_CODE1"].ToString();
                        _items.ITEM_CODE4 = row["ITEM_CODE1"].ToString();
                        _items.ITEM_CODE5 = row["ITEM_CODE1"].ToString();
                        _items.LOT_NO1 = row["LOT_NO1"].ToString();
                        _items.LOT_NO2 = row["LOT_NO2"].ToString();
                        _items.LOT_NO3 = row["LOT_NO3"].ToString();
                        _items.LOT_NO4 = row["LOT_NO4"].ToString();
                        _items.LOT_NO5 = row["LOT_NO5"].ToString();
                        _items.PALLET_CODE = row["PALLET_CODE"].ToString();
                        _items.COMMIT_NO = row["COMMIT_NO"].ToString();
                        _items.ID_MEMO = row["ID_MEMO"].ToString();
                        _items.COMMIT_NO = row["COMMIT_NO"].ToString();
                        _items.BODY_NO = row["BODY_NO"].ToString();
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _items;
        }

        //트래킹 정보 업데이트
        public void TrackUpdate(int ID_BUFF, string LOAD_CODE, string LOT_NO1,
            string LOT_NO2, string LOT_NO3, string LOT_NO4, string LOT_NO5, string PALLET_CODE, string COMMIT_NO, string BODY_NO)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_TRACK_BUFF_UPDATE", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("ID_BUFF", ID_BUFF));
                    cmd.Parameters.Add(new SqlParameter("LOAD_CODE", LOAD_CODE));
                    cmd.Parameters.Add(new SqlParameter("LOT_NO1", LOT_NO1));
                    cmd.Parameters.Add(new SqlParameter("LOT_NO2", LOT_NO2));
                    cmd.Parameters.Add(new SqlParameter("LOT_NO3", LOT_NO3));
                    cmd.Parameters.Add(new SqlParameter("LOT_NO4", LOT_NO4));
                    cmd.Parameters.Add(new SqlParameter("LOT_NO5", LOT_NO5));
                    cmd.Parameters.Add(new SqlParameter("PALLET_CODE", PALLET_CODE));
                    cmd.Parameters.Add(new SqlParameter("COMMIT_NO", COMMIT_NO));
                    cmd.Parameters.Add(new SqlParameter("BODY_NO", BODY_NO));

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

        //트래킹 정보 업데이트(SP3)
        public void Wh2TrackUpdate(int ID_BUFF, string LOAD_CODE, string LOT_NO1,
            string LOT_NO2, string LOT_NO3, string LOT_NO4, string LOT_NO5, string PALLET_CODE, string COMMIT_NO, string BODY_NO)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_TRACK_BUFF_UPDATE_WH2", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("ID_BUFF", ID_BUFF));
                    cmd.Parameters.Add(new SqlParameter("LOAD_CODE", LOAD_CODE));
                    cmd.Parameters.Add(new SqlParameter("LOT_NO1", LOT_NO1));
                    cmd.Parameters.Add(new SqlParameter("LOT_NO2", LOT_NO2));
                    cmd.Parameters.Add(new SqlParameter("LOT_NO3", LOT_NO3));
                    cmd.Parameters.Add(new SqlParameter("LOT_NO4", LOT_NO4));
                    cmd.Parameters.Add(new SqlParameter("LOT_NO5", LOT_NO5));
                    cmd.Parameters.Add(new SqlParameter("PALLET_CODE", PALLET_CODE));
                    cmd.Parameters.Add(new SqlParameter("COMMIT_NO", COMMIT_NO));
                    cmd.Parameters.Add(new SqlParameter("BODY_NO", BODY_NO));

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

        // 입,출고 바코드 확인을 위해 파레트코드를 가져온다.
        public cTrackList TrackPltGet()
        {
            cTrackList list = new cTrackList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_TRACK_IO_GET", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new cTrack
                        {
                            ID_BUFF = Convert.ToInt32(row["ID_BUFF"].ToString()),
                            PALLET_CODE = row["PALLET_CODE"].ToString(),
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

        // 입,출고 바코드 확인을 위해 파레트코드를 가져온다.(SP3)
        public cTrackList Wh2TrackPltGet()
        {
            cTrackList list = new cTrackList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_TRACK_IO_GET_WH2", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new cTrack
                        {
                            ID_BUFF = Convert.ToInt32(row["ID_BUFF"].ToString()),
                            PALLET_CODE = row["PALLET_CODE"].ToString(),
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
