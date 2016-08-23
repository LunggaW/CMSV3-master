using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KBS.KBS.CMSV3.DATAMODEL
{
    public class DeliveryNote
    {
        public string CMSId { get; set; }
        public string ItemID { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public int Qty { get; set; }
        public Decimal Price { get; set; }
        public string Store { get; set; }
        public DateTime? Date { get; set; }
        public String UserID { get; set; }

    }
}
