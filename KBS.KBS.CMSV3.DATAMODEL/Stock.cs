using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KBS.KBS.CMSV3.DATAMODEL
{
    public class Stock
    {
            public string TransactionID { get; set; }
            public string Nota { get; set; }
            public string Desc { get; set; }
            public string ItemID { get; set; }
            public string Barcode { get; set; }
            public string TransactionType { get; set; }
            public string Site { get; set; }
        }
}
