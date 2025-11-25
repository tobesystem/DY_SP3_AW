using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dayouAWSWh2.Class
{
    internal class cWmsOutItem
    {
        public string ID_TYPE { get; set; }
        public string ID_SC { get; set; }
        public string ID_DATE { get; set; }
        public string ID_TIME { get; set; }
        public string ID_INDEX { get; set; }
        public string ID_SUBIDX { get; set; }
        public string CELL { get; set; }
        public string HOGI { get; set; }
        public string LOAD_CODE { get; set; }
        public string PLT_CODE { get; set; }
        public string ITEM_CODE1 { get; set; }
        public string ITEM_CODE2 { get; set; }
        public string ITEM_CODE3 { get; set; }
        public string ITEM_CODE4 { get; set; }
        public string LOT_NO1 { get; set; }
        public string LOT_NO2 { get; set; }
        public string LOT_NO3 { get; set; }
        public string LOT_NO4 { get; set; }
        public string IN_DATE { get; set; }
        public string IDX { get; set; }
        public string LOCATION { get; set; }
        public string COMMIT_NO { get; set; }
        public string TRACKTYPE { get; set; }
        public string ORDATE { get; set; }
        public string STATUS { get; set; }
        public string RFID_STATUS { get; set; }
        public string REPORT_SYS_DT { get; set; }
        public string FR_GUBUN_DESC { get; set; }
        public string ID_CODE {  get; set; }

        public bool IsFooter { get; set; }
    }
}
