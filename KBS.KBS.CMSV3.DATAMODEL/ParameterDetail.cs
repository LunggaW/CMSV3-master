using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KBS.KBS.CMSV3.DATAMODEL
{
    public class ParameterDetail
    {
        public string ID { get; set; }
        public string Entry { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string SiteClass { get; set; }
        public int? Number1 { get; set; }
        public int? Number2 { get; set; }
        public int? Number3 { get; set; }
        public int? Number4 { get; set; }
        public string Char1 { get; set; }
        public string Char2 { get; set; }
        public string Char3 { get; set; }
        public DateTime? Date1 { get; set; }
        public DateTime? Date2 { get; set; }
        public DateTime? Date3 { get; set; }
        public String Comment { get; set; }
        
    }
}
