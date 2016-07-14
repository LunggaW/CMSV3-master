using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KBS.KBS.CMSV3.DATAMODEL
{
    public class SalesInputDetail
    {
         public string SALESID { get; set; }
        public string IID { get; set; }
        public string NOTA { get; set; }
        public string RECEIPTID { get; set; }        
        public string LINE { get; set; }
        public DateTime? DATE { get; set; }
        public string SITE { get; set; }
        public string ITEMID { get; set; }
        public string VARIANTID { get; set; }
        public string BARCODE { get; set; }
        public string SALESQTY { get; set; }
        public string REJECT { get; set; }
        public string VALID { get; set; }
        public string SKUID { get; set; }
        public string SALESPRICE { get; set; }
        public string TOTALPRICE { get; set; }
        public string TOTALDISCOUNT { get; set; }
        public string TOTALSALES { get; set; }
        public string COMMENT { get; set; }

    }
}
