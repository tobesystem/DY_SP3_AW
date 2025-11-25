using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dayouAWSWh2.Server
{
    public class cServerInfo
    {
        private string _server;
        public cServerInfo()
        {
              _server = "Data Source=tobesystem.co.kr,19813;Initial Catalog=DYH2_WH2;User Id=wmdayou;Password=wmpwd1@";
           // _server = "Data Source=192.168.5.10,15515;Initial Catalog=DYH2_WH2;User Id=wmdayou;Password=wmpwd1@";

        }

        public string Server()
        {
            return _server;
        }
    }
}
