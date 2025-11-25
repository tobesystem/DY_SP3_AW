using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dayouAWSWh2.Class
{
    //적재코드별 재고현황용
    internal class cCodeStockItem
    {
        public string ALC_CODE {  get; set; }
        public string ALC_CLASS {  get; set; }
        public string ALC_TYPE {  get; set; }
        public int SUM {  get; set; }

        public int FR_CNT { get; set; }
        public int R2_CNT { get; set; }
        public int R3_CNT { get; set; }


        //적재코드별 재고현황 세부용
        public string ALC_AREA { get; set; }

        public string SBELT1 { get; set; }
        public string SBELT2 { get; set; }
        public string ITEM_CODE1 { get; set; }
        public string ITEM_CODE2 { get; set; }
        public string ITEM_CODE3 { get; set; }
        public string ITEM_CODE4 { get; set; }
        public int PRICE1 { get; set; }
        public int PRICE4 { get; set; }
        public string LOT_NO1 { get; set; }
        public string LOT_NO2 { get; set; }
        public string LOT_NO3 { get; set; }
        public string LOT_NO4 { get; set; }
        public string IN_DATE { get; set; }
        public string ID_BANK { get; set; }
        public string ID_BAY { get; set; }
        public string ID_LEVEL { get; set; }
        public string FR_GUBUN { get; set; }
        public string COVERGOPT { get; set; }
    }
}
