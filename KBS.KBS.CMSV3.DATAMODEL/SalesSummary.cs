﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KBS.KBS.CMSV3.DATAMODEL
{
    public class SalesSummary
    {
        public int TotDisc;
        public int TotPrice;
        public int TotItem;
        public int TotSales;
        public int TotQty;
        public int TotNota;


        public DateTime? FromDate;
        public DateTime? ToDate;
        public String Nota;
        public String ReceiptID;
        public String Site;
    }
}
