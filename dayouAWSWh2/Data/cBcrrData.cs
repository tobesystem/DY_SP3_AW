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
    public class cBcrrData
    {
        cServerInfo _serverInfo = new cServerInfo();
        private string _conn;

        public cBcrrData()
        {
            _conn = _serverInfo.Server();
        }


        //셀 정보 조회
        public cBcrrList BcrPltGet()
        {
            cBcrrList list = new cBcrrList();

            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_BCRR_PLT_GET", connect);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;


                    connect.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();

                    adapter.Fill(ds);

                    DataTable dt = ds.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add(new cBcrrItem
                        {
                            BCRNO = Convert.ToInt32(row["BCRNO"].ToString()),
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

        /// <summary>
        /// COMMAND 값 업데이트
        /// </summary>
        public void AddCommand(int BCRNO, string COMMAND, string PALLET_CODE)
        {
            try
            {

                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_COMMAND_ADD", connect);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("BCRNO", BCRNO));
                    cmd.Parameters.Add(new SqlParameter("COMMAND", COMMAND));
                    cmd.Parameters.Add(new SqlParameter("PALLET_CODE", PALLET_CODE));
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

        /// <summary>
        /// 파레트코드 수동입력
        /// </summary>
        public void AddBcrrPlt(int BCRNO, string PALLET_CODE)
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(_conn))
                {
                    SqlCommand cmd = new SqlCommand("SP_CS_BCRR_PLT_ADD", connect);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("BCRNO", BCRNO));
                    cmd.Parameters.Add(new SqlParameter("PALLET_CODE", PALLET_CODE));
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
