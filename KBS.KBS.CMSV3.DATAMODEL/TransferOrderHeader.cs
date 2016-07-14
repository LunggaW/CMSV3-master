using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KBS.KBS.CMSV3.DATAMODEL
{
    public class TransferOrderHeader
    {
        public string ID { get; set; }
        public string IID { get; set; }
        public DateTime? DATE { get; set; }
        public string FROM { get; set; }
        public string TO { get; set; }
        public string STATUS { get; set; }
        public string FLAG { get; set; }
        public string VALIDATION { get; set; }

    }
}
