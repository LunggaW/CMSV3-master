using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using DevExpress.Web;

namespace KBS.KBS.CMSV3 {
    public partial class RootMaster : System.Web.UI.MasterPage {
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


        }
        protected void Logout_clicked(object sender, EventArgs e)
        {
            Session["UserID"] = "";
            Session["Username"] = "";
            Session["DefaultSite"] = "";
            Session["Class"] = "";
            Session["SiteProfile"] = "";               
            Response.Redirect("~/Account/Logins.aspx");
        }
        protected void Home(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
    }
}