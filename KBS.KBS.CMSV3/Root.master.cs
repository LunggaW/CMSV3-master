using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KBS.KBS.CMSV3.FUNCTION;
using KBS.KBS.CMSV3.DATAMODEL;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Web;
using System.Data;

namespace KBS.KBS.CMSV3 {
    public partial class RootMaster : System.Web.UI.MasterPage {
        
        //private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSite = new DataTable();
        //private User user;
        //private OutputMessage message;

        protected void Page_Load(object sender, EventArgs e) {
            ASPxSplitter1.GetPaneByName("Header").Size = ASPxWebControl.GlobalTheme == "Moderno" ? 95 : 83;
            ASPxSplitter1.GetPaneByName("Header").MinSize = ASPxWebControl.GlobalTheme == "Moderno" ? 95 : 83;
            Time.Text = DateTime.Now.ToLongDateString();
            
            if ((Session["Username"] == null) || (Session["Username"].ToString() == ""))
            {
                //string script = "alert('Your Session not valid please login again');";
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Your session has expired')", true);
                Response.Redirect("~/Account/Logins.aspx");
                
            }
            else
            {
                int posisi = 3;
                string path = CMSfunction.GetPath(posisi);

                ASPxImage4.ImageUrl = "~/image/" + path + "";
                User.Text = Session["Username"].ToString();
            }
            if ((Session["SiteName"] == null))
            {
                Session["SiteName"] = Session["DefaultSite"].ToString() + " " + Session["DefaultSiteName"].ToString();
                State.Text = Session["DefaultSite"].ToString() + " " + Session["DefaultSiteName"].ToString();
            }
            else
            {
                State.Text = Session["SiteName"].ToString();
            }
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
            }



        }
        protected void GetSite(object sender, EventArgs e)
        {
            Session["DefaultSite"] = sitestore.Value;
            Session["SiteName"] = sitestore.Text;
            State.Text = Session["SiteName"].ToString();
            Response.Redirect("~/Default.aspx");
            //user = CMSfunction.SelectUserDataFromUserID(User.Identity.Name);
            //loadNavBar();
        }
        protected void Logout_clicked(object sender, EventArgs e)
        {
            Session["UserID"] = "";
            Session["Username"] = "";
            Session["DefaultSite"] = "";
            Session["Class"] = "";
            Session["SiteProfile"] = "";
            Session.Abandon();
            Response.Redirect("~/Account/Logins.aspx");
        }
        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}