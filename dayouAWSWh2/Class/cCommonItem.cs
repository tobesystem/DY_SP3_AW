using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dayouAWSWh2.Class
{
    internal class cCommonItem
    {
        public string CODE_TYPE { get; set; }
        public string CODE_NAME { get; set; }

        // 부서용
        public string DEPT_CODE {  get; set; }
        public string DEPT_NAME {  get; set; }


        // 권한용
        public string AUTH_CODE { get; set; }
        public string AUTH_NAME { get; set; }
    }
}
