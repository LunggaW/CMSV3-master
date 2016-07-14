using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KBS.KBS.CMSV3.DATAMODEL
{
    public class SiteMaster
    {
        public string Site { get; set; }
        public int? SiteClass { get; set; }
        public string SiteName { get; set; }
        public int? Enable { get; set; }
    }
}
