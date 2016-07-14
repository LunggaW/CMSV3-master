using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using NLog;

namespace MobileEnabledWebFormsApp.Mobile
{public partial class _Default : System.Web.UI.Page
    {
       

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        private function CMSfunction = new function();
        private User user;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(Session["UserID"].ToString()))
            {
                Response.Redirect("~/Account/Logins.aspx");
            }
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            
        }
    }
}
