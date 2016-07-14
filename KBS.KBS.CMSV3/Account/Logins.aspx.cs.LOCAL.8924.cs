using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
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

namespace KBS.KBS.CMSV3.Account
{
    public partial class Logins : System.Web.UI.Page
    {private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        
        private function CMSfunction = new function();
        private User user;
        private OutputMessage message;

        protected void Page_Load(object sender, EventArgs e)
        {
            Background.ImageUrl = "~/Image/sales.png";
            Logo.ImageUrl = "~/Image/cmslogo.png";
            //180205235
            
            
            //PasswordTxt.Attributes.Add("value", "ThePassword");
            //this.UserIDTxt.Attributes["onclick"] = "this.value=''";
            //this.PasswordTxt.Attributes["onclick"] = "this.value=''";
            //this.PasswordTxt.Attributes["onblur"] = "this.value='Password'";
            //this.UserIDTxt.Attributes["onblur"] = "this.value='User Id'";
        }
        protected void test(object sender, EventArgs e)
        {
            if (PasswordTxt.Text == "")
            {
                PasswordTxt.Attributes.Add("value", "ThePassword");
            }
            else
            {
                PasswordTxt.Attributes["type"] = "password";
            }
        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            message = new OutputMessage();
            message = CMSfunction.Login(UserIDTxt.Text, PasswordTxt.Text);

            

            if (message.Code == 1)
            {
                user = CMSfunction.SelectUserFromUserID(UserIDTxt.Text);
                ASPxLabelMessage.Visible = false;
                Session["UserID"] = UserIDTxt.Text;
                Session["Username"] = user.Username;
                Session["DefaultSite"] = user.DefaultSite;
                Session["DefaultSiteName"] = user.DefaultSiteName;
                Session["Class"] = user.SiteClass;
                Session["AccessProfile"] = user.AccessProfile;
                Session["MenuProfile"] = user.MenuProfile;
                Session["SiteProfile"] = user.SiteProfile;
                Session["SiteName"] = Session["DefaultSite"].ToString() + " " + Session["DefaultSiteName"].ToString();
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                ASPxLabelMessage.Visible = true;
                ASPxLabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;
                ASPxLabelMessage.Text = message.Message;
            }
            
        }

        protected void Exit_Click(object sender, EventArgs e)
        {

        }
        
    }
}