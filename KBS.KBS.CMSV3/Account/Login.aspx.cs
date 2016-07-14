using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Oracle.DataAccess.Client;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3 
{
    public partial class Login : System.Web.UI.Page 
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        
        private function CMSfunction = new function();
        private User user;

        protected void Page_Load(object sender, EventArgs e) 
        {
            HttpRequest httpRequest = HttpContext.Current.Request;
            if (httpRequest.Browser.IsMobileDevice)
            {
                logger.Debug("Masuk Mobile");
                string path = httpRequest.Url.PathAndQuery;
                bool isOnMobilePage = path.StartsWith("/Mobile/",
                                                      StringComparison.OrdinalIgnoreCase);
                logger.Debug("Path : " + path);
                if (!isOnMobilePage)
                {
                    string redirectTo = "~/Mobile/Default.aspx";

                    // Could also add special logic to redirect from certain 
                    // recognised pages to the mobile equivalents of those 
                    // pages (where they exist). For example,
                    // if (HttpContext.Current.Handler is UserRegistration)
                    //     redirectTo = "~/Mobile/Register.aspx";
                    logger.Debug("Redirect To : " + redirectTo);
                    HttpContext.Current.Response.Redirect(redirectTo);

                }
            }
            else
            {
                logger.Debug("Tidak Masuk Mobile");
            }

            if (!String.IsNullOrWhiteSpace(User.Identity.Name))
            {
                Response.Redirect("~");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e) 
        {
           


        }

        

    }
}