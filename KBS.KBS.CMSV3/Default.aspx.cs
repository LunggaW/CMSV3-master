using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;
using System.Data;

namespace KBS.KBS.CMSV3 
{
    public partial class _Default : System.Web.UI.Page 
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private DataTable DTSite = new DataTable(); 
        private function CMSfunction = new function();
        private User user;

        protected override void OnInit(EventArgs e)
        {
            //user = CMSfunction.SelectUserDataFromUserID(User.Identity.Name);
            

            if (string.IsNullOrEmpty(Session["UserID"] as string))
            {
                //The code
                Response.Redirect("~/Account/Logins.aspx");

            }
            else
            {
                loadNavBar();    
            }
            
        }

        protected void GetSite(object sender, EventArgs e)
        {
            Session["DefaultSite"] = sitestore.Value;
            Session["SiteName"] = sitestore.Text;
            
            Response.Redirect("~/Default.aspx");
            //user = CMSfunction.SelectUserDataFromUserID(User.Identity.Name);
            //loadNavBar();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!Page.IsPostBack)            
            {
                DTSite = CMSfunction.GetSiteBySiteProfile(Session["SiteProfile"].ToString());
                sitestore.DataSource = DTSite;
                sitestore.ValueField = "SITESITE";
                sitestore.ValueType = typeof(string);
                sitestore.TextField = "SITESITENAME";
                sitestore.DataBind();

                if (Session["SiteName"] != null)
                {
                    sitestore.Text = Session["SiteName"].ToString();
                }


                int posisi = 2;
                string path = CMSfunction.GetPath(posisi);

                Background.ImageUrl = "~/image/" + path + "";
            }
            
        }
        private void LoadHeader()
        {

            
        }



        private void loadNavBar()
        {

            List<Menu> listMenuGroup = CMSfunction.SelectMenuGroupByProfileID(Session["MenuProfile"].ToString());
            int i = 0;

            ASPxSplitter sp = Master.Master.FindControl("ASPxSplitter1").FindControl("Content").FindControl("ContentSplitter") as ASPxSplitter;
            ASPxNavBar masterNav = sp.FindControl("ASPxNavBar1") as ASPxNavBar;
            if (masterNav != null)
            {
                foreach (var menuGroup in listMenuGroup)
                {

                    masterNav.Groups.Add(menuGroup.MenuGroupName, menuGroup.MenuGroupID);


                    List<Menu> listMenu = CMSfunction.SelectMenuByProfileIDandMenuGroup(Session["MenuProfile"].ToString(),
                        menuGroup.MenuGroupID);
                    foreach (var menuItem in listMenu)
                    {
                        masterNav.Groups[i].Items.Add(menuItem.MenuName, menuItem.MenuID, null, menuItem.MenuURL);

                    }
                    i++;

                }
            }
            masterNav.DataBind();
        }
    }
}