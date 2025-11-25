using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dayouAWSWh2.Class
{
    internal class cRFIDItem
    {
        public int RFIDNO { get; set; }
        public string RFID_RDATA { get; set; }
        public string RFID_WDATA { get; set; }
        public string FLAG { get; set; }
        public string COMMAND { get; set; }
        public string RWFLAG { get; set; }
        public string RESULT { get; set; }
        public string MEMO {  get; set; }
        public string CR_DATE { get; set; }
    }
}
