using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KBS.KBS.CMSV3.DATAMODEL
{
    public class SalesInputSimple
    {
        public string NOTA { get; set; }
        public string BARCODE { get; set; }
        public string SALESQTY { get; set; }
        public string SKU { get; set; }

        public string AMOUNT { get; set; }

        public DateTime? TransDate { get; set; }


        //Update GAGAN
        public int DISCOUNT { get; set; }
        public string NormalPrice { get; set; }
        public string FinalPrice { get; set; }

    }
}
