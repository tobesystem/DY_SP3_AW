using dayouAWSWh2.Class;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dayouAWSWh2.Server;

namespace dayouAWSWh2.Data
{
    internal class cAlarmData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cAlarmData()
        {
            _conn = _serverInfo.Server();
        }
        public cAlarmItem getAlarm()
        {
            cAlarmItem _items = new cAlarmItem();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_ALARM_GET", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        _items.Current_Desc = row["Current_Desc"].ToString();

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return _items;
        }

        public cAlarmItem UpdateAlarm()
        {
            cAlarmItem _items = new cAlarmItem();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_ALARM_UPDATE", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];

                        _items.Current_Desc = row["Current_Desc"].ToString();

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
