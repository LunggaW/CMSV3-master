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
using DevExpress.Export;
using DevExpress.XtraPrinting;

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
            int posisi = 1;
            string path = CMSfunction.GetPath(posisi);

            Background.ImageUrl = "~/image/" + path + "";            
            Logo.ImageUrl = "~/Image/cmslogo.png";
            Session["Filter"] = "Login";
        }
        protected void test(object sender, EventArgs e)
        {
            //if (PasswordTxt.Text == "")
            //{
            //    PasswordTxt.Attributes.Add("value", "ThePassword");
            //}
            //else
            //{
            //    PasswordTxt.Attributes["type"] = "password";
            //}
            LoginBtn_Click();
        }

        private void LoginBtn_Click()
        {
            message = new OutputMessage();
            message = CMSfunction.Login(UserIDTxt.Text, PasswordTxt.Text);



            if (message.Code == 1)
            {
                user = CMSfunction.SelectUserFromUserID(UserIDTxt.Text);
                if (user.Username != null)
                {
                    ASPxLabelMessage.Visible = false;
                    Session["DATAQUERY"] = "";
                    Session["FileName"] = "";
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
                    string CheckLicense = CMSfunction.LicenseCheck();
                    if (CheckLicense == "Valid")
                    {
                        string message = "Tidak tersedia store untuk user ini mohon hubungi admin !";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                    }
                    else
                    {
                        string message = "License Expired Please Contact Administrator !";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                    }
                }

            }
            else
            {
                ASPxLabelMessage.Visible = true;
                ASPxLabelMessage.BackColor = message.Code < 0 ? Color.Red : Color.White;
                ASPxLabelMessage.ForeColor = message.Code < 0 ? Color.White : Color.Black;
                ASPxLabelMessage.Text = message.Message;
            }
        }
        


        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            message = new OutputMessage();
            if (UserIDTxt.Text == "")
            {
                message.Code = -1;
                message.Message = "Please Insert UserName";
            }
            else if (PasswordTxt.Text == "")
            {
                message.Code = -1;
                message.Message = "Please Insert Password";
            }
            else
            {
                message = CMSfunction.Login(UserIDTxt.Text, PasswordTxt.Text);
                if (message.Code == 1)
                {
                    user = CMSfunction.SelectUserFromUserID(UserIDTxt.Text);
                    if (user.Username != null)
                    {
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

                        string CheckLicense = CMSfunction.LicenseCheck();
                        if (CheckLicense == "Valid")
                        {
                            string message = "Tidak tersedia store untuk user ini mohon hubungi admin !";
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append("<script type = 'text/javascript'>");
                            sb.Append("window.onload=function(){");
                            sb.Append("alert('");
                            sb.Append(message);
                            sb.Append("')};");
                            sb.Append("</script>");
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                        }
                        else
                        {
                            string message = "License Expired Please Contact Administrator !";
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append("<script type = 'text/javascript'>");
                            sb.Append("window.onload=function(){");
                            sb.Append("alert('");
                            sb.Append(message);
                            sb.Append("')};");
                            sb.Append("</script>");
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                        }

                    }
                }
                else
                {
                    ASPxLabelMessage.Visible = true;
                    ASPxLabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;
                    ASPxLabelMessage.Text = message.Message;

                }
            }
            
            
        }

        protected void Exit_Click(object sender, EventArgs e)
        {

        }
        
    }
}