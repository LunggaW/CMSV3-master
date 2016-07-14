using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KBS.KBS.CMSV3.DATAMODEL
{
    public class SalesHeader
    {
        public string SALESID { get; set; }
        public string IID { get; set; }
        public string NOTA { get; set; }
        public string RECEIPTID { get; set; }
        public DateTime? DATE { get; set; }
        public string SITE { get; set; }
        public string COMMENT { get; set; }
        public string REJECT { get; set; }
        public string VALID { get; set; }
        public Decimal STATUS { get; set; }
        public Decimal FLAG { get; set; }        
    }
}
