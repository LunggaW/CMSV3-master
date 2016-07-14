using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;

namespace KBS.KBS.CMSV3.Administration.MenuManagement
{
    public partial class MenuProfileHeaderDetail : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DATAMODEL.MenuProfileHeader menuProfile;
        private OutputMessage message = new OutputMessage();

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/logins.aspx");
            }
            else if (Session["MenuProfileManagement"] == null)
            {
                Response.Redirect("MenuProfileHeader.aspx");
            }
            else
            {
                loadNavBar();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                menuProfile = new DATAMODEL.MenuProfileHeader();

                TextBoxMenuProfile.Text = Session["MenuProfileManagement"].ToString();
                menuProfile.Profile = Session["MenuProfileManagement"].ToString();

                CMSfunction.GetMenuProfileDetail(menuProfile);

                TextBoxMenuProfileDescription.Text = menuProfile.Description;
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

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
           // siteProfile = new DATAMODEL.SiteProfileHeader();

           //siteProfile.Profile = !string.IsNullOrWhiteSpace(TextBoxSiteProfile.Text) ? TextBoxSiteProfile.Text : "";
           // siteProfile.Description = !string.IsNullOrWhiteSpace(TextBoxSiteProfileDescription.Text) ? TextBoxSiteProfileDescription.Text : "";
           

           // DTSiteProfile = CMSfunction.GetAllSiteProfileFiltered(siteProfile);
           // ASPxGridViewSiteProfile.DataSource = DTSiteProfile;
           // ASPxGridViewSiteProfile.KeyFieldName = "SITE PROFILE";
           // ASPxGridViewSiteProfile.DataBind();
        }


        protected void DelBtn_Click(object sender, EventArgs e)
        {
            //if (ASPxGridViewSiteProfile.FocusedRowIndex != -1)
            //{
            //    String ParamHeaderID = ASPxGridViewSiteProfile.GetRowValues(ASPxGridViewSiteProfile.FocusedRowIndex, "ID").ToString();
                
            //    OutputMessage message = new OutputMessage();

            //    //message = CMSfunction.DeleteUser(ASPxGridViewUser.GetRowValues(ASPxGridViewUser.FocusedRowIndex, "ID").ToString());

            //    LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            //    LabelMessage.Visible = true;
            //    LabelMessage.Text = message.Message;
            //}
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            ProcessUpdate();

            LabelMessage.Visible = true;
            LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            LabelMessage.Text = message.Message;
        }

        

        private void ProcessUpdate()
        {
            DATAMODEL.MenuProfileHeader menuProfile = new DATAMODEL.MenuProfileHeader();

            menuProfile.Profile = Session["MenuProfileManagement"].ToString();
            menuProfile.Description = TextBoxMenuProfileDescription.Text;

            message = CMSfunction.updateMenuProfile(menuProfile, Session["UserID"].ToString());
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            ProcessUpdate();
            Response.Redirect("MenuProfileHeader.aspx");
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuProfileHeader.aspx");
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            TextBoxMenuProfileDescription.Text = "";
        }


    }
}