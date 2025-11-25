using dayouAWSWh2.Class;
using dayouAWSWh2.Server;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlzEx.Standard;

namespace dayouAWSWh2.Data
{
    internal class cPalletData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cPalletData()
        {
            _conn = _serverInfo.Server();
        }

        
        public cPalletItemList getPalletList(string plt_code)
        {
            cPalletItemList _list = new cPalletItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_PALLET_GET", connect);

                    cmd.Parameters.Add(new SqlParameter("PLT_CODE", plt_code));
                  
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cPalletItem
                        {
                            PALLETCD = row["PALLETCD"].ToString(),
                            PALLETNM = row["PALLETNM"].ToString(),
                            BADCODE = row["BADCODE"].ToString(),
                            CREATE_DATE = row["CREATE_DATE"].ToString(),
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

        public cPalletItemList getPalletStatus()
        {
            cPalletItemList _list = new cPalletItemList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_PALLET_STATUS_GET", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        _list.Add(new cPalletItem
                        {
                            BADCD = row["BADCD"].ToString(),
                            BADNM = row["BADNM"].ToString(),
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

        public cMessage updatePallet(string pallet_cd, string pallet_nm, string bad_code, string keyword)
        {
            cMessage _items = new cMessage();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_PALLET_ADD_EDIT", connect);
                   
                    cmd.Parameters.Add(new SqlParameter("PALLETCD", pallet_cd));
                    cmd.Parameters.Add(new SqlParameter("PALLETNM", pallet_nm));
                    cmd.Parameters.Add(new SqlParameter("BADCODE", bad_code));
                    cmd.Parameters.Add(new SqlParameter("KEYWORD", keyword));

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        _items.RESULT = row["RESULT"].ToString();
                        _items.MSG = row["MSG"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _items;
        }

        public cMessage deletePallet(string pallet_cd)
        {
            cMessage _items = new cMessage();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_PALLET_DEL", connect);

                    cmd.Parameters.Add(new SqlParameter("PALLETCD", pallet_cd));

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        _items.RESULT = row["RESULT"].ToString();
                        _items.MSG = row["MESSAGE"].ToString();
                    }
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
