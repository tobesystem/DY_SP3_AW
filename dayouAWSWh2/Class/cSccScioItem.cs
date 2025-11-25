using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dayouAWSWh2.Class
{
    public class cSccScioItem
    {
        public string ID_DATE {  get; set; }
        public string ID_TIME {  get; set; }
        public int ID_INDEX {  get; set; }
        public int ID_SUBIDX {  get; set; }
        public string IO_TYPE {  get; set; }
        public string ID_SC {  get; set; }
        public string LOAD_BANK {  get; set; }
        public string LOAD_BAY {  get; set; }
        public string LOAD_LEVEL {  get; set; }
        public string UNLOAD_BANK {  get; set; }
        public string UNLOAD_BAY {  get; set; }
        public string UNLOAD_LEVEL {  get; set; }
        public string SC_STATUS {  get; set; }
    }
}
