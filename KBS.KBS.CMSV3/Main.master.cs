using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KBS.KBS.CMSV3
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void RefreshData()
        {
       
            string ABC = "";
            if (Session["UserFilter"] != null)
            {
                Session.Remove("UserFilter");
                Session.Remove("UserIdFilter");
                Session.Remove("StartDateFilter");
                Session.Remove("EndDateFilter");
                Session.Remove("UserDescFilter");
                Session.Remove("UserAccProfFilter");
                Session.Remove("UserNameFilter");
                Session.Remove("UserStatFilter");
                Session.Remove("UserTypeFilter");
                Session.Remove("UserMenuProfFilter");
                Session.Remove("UserSiteProfFilter");
                Session.Remove("UserIdUserManagement");
            }

            if (Session["MenuProfFilter"] != null)
            {
                Session.Remove("MenuProfProfFilter");
                Session.Remove("MenuProfDescFilter");
                Session.Remove("MenuProfFilter");
            }
        
        }
    protected void ASPxNavBar1_ItemClick(object source, DevExpress.Web.NavBarItemEventArgs e)
    {
        RefreshData();        
    }
       

    }
}