using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KBS.KBS.CMSV3.DATAMODEL
{
    public class VariantDetail
    {
        public string VariantID { get; set; }
        public string StyleGroup { get; set; }
        public string StyleDetail { get; set; }
        public string ColorGroup { get; set; }
        public string ColorDetail { get; set; }
        public string SizeGroup { get; set; }
        public string SizeDetail { get; set; }

        public string ItemID { get; set; }

    }
}
