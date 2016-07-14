using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KBS.KBS.CMSV3.DATAMODEL
{
    public class ParameterHeader
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string SClass { get; set; }
        public string Copy { get; set; }
        public string Comment { get; set; }
        public string Lock { get; set; }

        //public Int32 ID { get; set; }
        //public string Name { get; set; }
        //public Int32 SClass { get; set; }
        //public Int32 Copy { get; set; }
        //public string Comment { get; set; }
        //public Int32 Lock { get; set; }
    }
}
