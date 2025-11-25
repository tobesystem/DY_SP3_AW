using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dayouAWSWh2.Class
{
    //입고대 실적 조회
    internal class cTotalJobItem
    {
        public int ROW_NUM { get; set; }
        public string ALC_CODE { get; set; }
        public string ALC_CLASS { get; set; }
        public string ALC_AREA { get; set; }
        public int IN_CNT { get; set; }
        public string IN_DT { get; set; }
        public string LOAD_CODE { get; set; }
        public string LOT_NO1 { get; set; }
        public string LOT_NO2 { get; set; }
        public string LOT_NO3 { get; set; }
        public string FR_GUBUN { get; set; }
        public string PALLET_CODE { get; set; }
   
    }
}
