using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dayouAWSWh2.Class
{
    internal class cStatusItem
    {
        //제어설정 상태용
        public string Current_Name { get; set; }
        public int Option1 { get; set; }
        public int Option2 { get; set; }
        public int Option3 { get; set; }
        public string Current_Type { get; set; }
        public int Current_index { get; set; }

        public string Current_Desc { get; set; }


        //소켓 통신설정 상태용
        public string ID_CODE { get; set; }
        public string ID_NAME { get; set; }
        public string ID_IP { get; set; }
        public string ID_PORT1 { get; set; }
        public string ID_PORT2 { get;set; }
        public int ID_LOG {  get; set; }
    }
}
