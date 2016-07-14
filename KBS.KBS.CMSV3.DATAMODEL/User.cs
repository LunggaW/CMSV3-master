using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KBS.KBS.CMSV3.DATAMODEL
{
    public class User
    {
        public enum UserStatus
        {
            Active = 1,
            Frozen,
            Delete,
        };

        public string Username { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate{ get; set; }
        public DateTime? EndDate { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string AccessProfile { get; set; }
        public string MenuProfile { get; set; }
        public string SiteProfile { get; set; }
        public string IMEI { get; set; }
        public string UserType { get; set; }
        public string DefaultSite { get; set; }
        public string SiteClass { get; set; }
        public string DefaultSiteName { get; set; }
    }
}
