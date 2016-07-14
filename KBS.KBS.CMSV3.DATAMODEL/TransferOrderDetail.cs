using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KBS.KBS.CMSV3.DATAMODEL
{
    public class TransferOrderDetail
    {
        public string ID { get; set; }
        public string IID { get; set; }
        public string ITEMID { get; set; }
        public string VARIANT { get; set; }
        public string BARCODE { get; set; }
        public string QTY { get; set; }
        public string SHIP { get; set; }        
        public string RECEIVE { get; set; }
        public string SCRAP { get; set; }
        public string COMMENT { get; set; }
        public string STATUS { get; set; }
        public string FLAG { get; set; }

        public string PRICE { get; set; }
    }
}

