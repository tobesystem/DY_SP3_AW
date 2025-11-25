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
    internal class cLoginData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cLoginData()
        {
            _conn = _serverInfo.Server();
        }
        public cLoginItem getLogin(string _id, string _pw)
        {
            cLoginItem _items = new cLoginItem();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_LOGIN", connect);
                    cmd.Parameters.Add(new SqlParameter("USER_CODE", _id));
                    cmd.Parameters.Add(new SqlParameter("USER_PWD", _pw));

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
                        _items.MESSAGE = row["MESSAGE"].ToString();
                        _items.USER_CODE = row["USER_CODE"].ToString();
                        _items.USER_NAME = row["USER_NAME"].ToString();
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
