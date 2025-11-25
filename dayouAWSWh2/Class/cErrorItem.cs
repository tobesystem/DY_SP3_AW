using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dayouAWSWh2.Class
{
    internal class cErrorItem
    {
        //에러코드 , 설비에러 조회용
        public int ROW_NUM { get; set; }
        public string ID_MACH { get; set; }
        public string ERR_CODE { get; set; }
        public string ERR_NAME { get; set; }
        public string ERR_ETC { get; set; }
        public string ID_MACHNO { get; set; }
        public string ERR_DESC { get; set; }
        public string ERR_START { get; set; }
        public string ID_MEMO { get; set; }
        public int ERR_CNT { get; set; }
        public string ERR_MSG { get; set; }
    }
}
