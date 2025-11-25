using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dayouAWSWh2.Class
{
    //적재별 실적조회
    internal class cLoadPerfItem
    {
        public int ROWNUM { get; set; }
        public string LOAD_CODE {  get; set; }
        public string FR_GUBUN { get; set; }
        public int IN_CNT { get; set; }
        public int OT_CNT { get; set; }
        public int ST_CNT { get; set; }
        public string KIND {  get; set; }
        public string COVER_COLOR { get; set; }
        public string COVER_SET { get; set; }
        public string DRIVER_TYPE { get; set; }
        public string REGION { get; set; }
        public string DRIVER {  get; set; }
        public string PASSENGER { get; set; }
        public string SECOND_LH { get; set; }
        public string SECOND_CH { get; set; }
        public string SECOND_RH { get; set; }
    }
}
