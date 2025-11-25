using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dayouAWSWh2.Class
{
    internal class cManualOutItem
    {
        public string ALC_CODE { get; set; }
        public string ALC_CLASS {  get; set; }
        public string ALC_TYPE { get; set; }
        public int SUM {  get; set; }

        public int ID_SUBIDX { get; set; }
        public string LOT_NO1 { get; set; }
        public string LOT_NO2 { get; set; }
        public string LOT_NO3 { get; set; }
        public string LOT_NO4 { get; set; }
        public string ID_BANK { get; set; }
        public string ID_BAY { get; set; }
        public string ID_LEVEL { get; set; }
        public string LOAD_CODE { get; set; }
        public string ID_CODE { get; set; }
        public int FR_CNT { get; set; }
        public int R2_CNT { get; set; }
        public int R3_CNT { get; set; }
    }
}
