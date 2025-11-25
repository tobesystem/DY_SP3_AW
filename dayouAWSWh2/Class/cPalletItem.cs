using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dayouAWSWh2.Class
{
    //파레트 코드
    internal class cPalletItem
    {
        public string PALLETCD {  get; set; }
        public string PALLETNM {  get; set; }
        public string BADCODE {  get; set; }
        public string CREATE_DATE {  get; set; }
        public string ROW_NUM {  get; set; }

        // 파레트 상태용
        public string BADCD { get; set; }
        public string BADNM { get; set; }
    }
}
