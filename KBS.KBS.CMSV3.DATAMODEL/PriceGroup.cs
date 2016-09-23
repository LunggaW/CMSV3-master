using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KBS.KBS.CMSV3.DATAMODEL
{
    public class PriceGroup
    {
        public string ItemID { get; set; }
        public string VariantID { get; set; } 
        public string Site { get; set; }
        public string Price { get; set; } 
        public string VAT { get; set; }
        public DateTime? SDate { get; set; }
        public DateTime? Edate { get; set; }

        public string AssortmentStatus { get; set; }
    }
}
