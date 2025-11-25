using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dayouAWSWh2.Class
{
    // 의장 데이터
    internal class cHostItem
    {
        public string ROW_NUM { get; set; }
        public string REPORT_DT { get; set; }
        public string CMT_NO { get; set; }
        public string ALC_CODE { get; set; }
        public string ALC_CHECK {  get; set; }
        public string ALC_CLASS { get; set; }
        public string BODY_NO { get; set; }
        public string HANGUEL_PART { get; set; }
        public string INNER_COLOR { get; set; }
        public string HIS_DATE { get; set; }
        public string BR_NO { get; set; }
        public string TERM_ID { get; set; }
        public string DRIVE_TYPE { get; set; }
        public string REGION_NAME { get; set; }
        public string NATION_NAME { get; set; }
      
        public string REPORT_SYS_DT { get; set; }
        public string STOCK_FR { get; set; }
        public string STOCK_R2 { get; set; }
        public string STOCK_R3 { get; set; }
        public string STATUS {  get; set; }
        public string CR_DATE { get; set; }
        public string COMPLETE_DT { get; set; }
        public string POP_SEND { get; set; }
        public string BODY_COLOR { get; set; }


        //의장출고내역 용
        public string BR_DATE {  get; set; }
        public int CNT {  get; set; }
        public int ID_SUBIDX { get; set; }
        public string WMS_OUT_NO { get; set; }
        public string COVER_COLOR {  get; set; }
        public string COVER_SET { get; set; }
        public string REGION {  get; set; }
        public string OUT_DATE { get; set; }
        public string IN_DATE { get; set;}
        public string LOCATION {  get; set; }
        public string PLT_CODE { get; set; }
        public String COMMIT_NO { get; set; }

        public string FR_GUBUN { get; set; }
    }
}
