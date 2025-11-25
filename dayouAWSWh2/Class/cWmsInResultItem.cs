using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dayouAWSWh2.Class
{
    // 입고실적용
    internal class cWmsInResultItem
    {
        public string ROW_NUM {  get; set; }
        public string LOAD_CODE { get; set; }
        public string ITEM_CODE1 { get; set; }
        public string ITEM_CODE2 { get; set; }
        public string ITEM_CODE3 { get; set; }
        public string ITEM_CODE4 { get; set; }
        public string ALC_CLASS { get; set; }
        public string ALC_CODE { get; set; }
        public string PLT_CODE {  get; set; }
        public string LOT_NO1 { get; set; }
        public string LOT_NO2 { get; set; }
        public string LOT_NO3 { get; set; }
        public string LOT_NO4 { get; set; }

        public string COMMIT_NO { get; set; }

        public string ID_TYPE { get; set; }
        public string ID_BANK { get; set; }
        public string ID_BAY { get; set; }
        public string ID_LEVEL { get; set; }
        public string WORKDATE { get; set; }
        public string REGION { get; set; }
        
    }
}
