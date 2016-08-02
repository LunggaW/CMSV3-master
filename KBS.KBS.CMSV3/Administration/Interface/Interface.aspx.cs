using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;

namespace KBS.KBS.CMSV3.Administration.Interface
{
    public partial class Interface : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTAccessProfile = new DataTable();
        private DATAMODEL.AccessProfileHeader accessProfile;
        private String MenuID = ConfigurationManager.AppSettings["MenuIdAccessProfileManagement"];
        private User user;

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/logins.aspx");
            }
            else
            {
                loadNavBar();
                
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
            
        }
        
        protected void btnChangePassword_Click(object sender, EventArgs e)
        {

            if (tbCurrentPassword.Text != Session["Pass"].ToString())
            {
                tbCurrentPassword.ErrorText = "Old Password is not valid";
                tbCurrentPassword.IsValid = false;
                //btnChangePassword.Enabled = false;

            }
            else if (tbPassword.Text == "")
            {
                tbPassword.ErrorText = "New Password is required.";
                tbPassword.IsValid = false;
                //btnChangePassword.Enabled = false;
            }
            else if (tbPassword.Text == Session["Pass"].ToString())
            {
                tbPassword.ErrorText = "Old Password and new Password is identical";
                tbPassword.IsValid = false;
                //btnChangePassword.Enabled = false;
            }
            else if (tbPassword.Text != tbConfirmPassword.Text)
            {
                tbConfirmPassword.ErrorText = "Confirm Password is different";
                tbConfirmPassword.IsValid = false;
                //btnChangePassword.Enabled = false;
            }
            else if (tbPassword.Text.Contains(" "))
            {
                tbPassword.ErrorText = "New Password cannot contain empty string";
                tbPassword.IsValid = false;
                //btnChangePassword.Enabled = false;
            }
            else
            {
                String Message = CMSfunction.changePassword(tbPassword.Text, Session["UserID"].ToString());

                if (Message == "Success")
                {
                    Session["Pass"] = tbPassword.Text;
                    string script = "alert('Update Password Success');";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
                }
            }
            

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

        protected void tbCurrentPassword_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}