using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;

namespace KBS.KBS.CMSV3 {
    public partial class ChangePassword : System.Web.UI.Page 
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        //private int MinutesToLogout = Convert.ToInt32(ConfigurationManager.AppSettings["MinutesToLogout"]);
        private function CMSfunction = new function();
        private User user;

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            //user = CMSfunction.SelectUserDataFromUserID(User.Identity.Name);

            //if (tbCurrentPassword.Text != user.Password)
            //{
            //    tbCurrentPassword.ErrorText = "Old Password is not valid";
            //    tbCurrentPassword.IsValid = false;

            //}
            //else if (tbPassword.Text == user.Password)
            //{
            //    tbCurrentPassword.ErrorText = "Old Password and new Password is identical";
            //    tbCurrentPassword.IsValid = false;
            //}
            //else if (tbPassword.Text != tbConfirmPassword.Text)
            //{
            //    tbCurrentPassword.ErrorText = "New Password and Confirm Password is different";
            //    tbCurrentPassword.IsValid = false;
            //}
            //else if (tbPassword.Text.Contains(" "))
            //{
            //    tbCurrentPassword.ErrorText = "New Password cannot contain empty string";
            //    tbCurrentPassword.IsValid = false;
            //}
            //else
            //{
            //    String Message = CMSfunction.changePassword(tbPassword.Text, User.Identity.Name);
               
            //}
            //MembershipUser user = Membership.GetUser(User.Identity.Name);
            //if (!Membership.ValidateUser(user.UserName, tbCurrentPassword.Text)) 
            //{
            //    tbCurrentPassword.ErrorText = "Old Password is not valid";
            //    tbCurrentPassword.IsValid = false;
            //}
            //else if (!user.ChangePassword(tbCurrentPassword.Text, tbPassword.Text)) 
            //{
            //    tbPassword.ErrorText = "Password is not valid";
            //    tbPassword.IsValid = false;
            //}
            //else
            //    Response.Redirect("~/");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
/*
            if (!String.IsNullOrWhiteSpace(User.Identity.Name))
            {
                Response.Redirect("~/");
            }
            */
        }
    }
}