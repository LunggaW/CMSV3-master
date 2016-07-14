using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;

namespace KBS.KBS.CMSV3.Administration.AccessManagement
{
    public partial class AccessProfileHeaderDetail : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DATAMODEL.AccessProfileHeader accessProfile;
        private OutputMessage message = new OutputMessage();

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/logins.aspx");
            }
            else if (Session["AccessProfileManagement"] == null)
            {
                Response.Redirect("AccessProfileHeader.aspx");
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
                RefreshDataGrid();
            }
           
        }


        private void loadNavBar()
        {

            List<Menu> listMenuGroup = CMSfunction.SelectMenuGroupByProfileID(Session["SiteProfile"].ToString());
            int i = 0;

            ASPxSplitter sp = Master.Master.FindControl("ASPxSplitter1").FindControl("Content").FindControl("ContentSplitter") as ASPxSplitter;
            ASPxNavBar masterNav = sp.FindControl("ASPxNavBar1") as ASPxNavBar;
            if (masterNav != null)
            {
                foreach (var menuGroup in listMenuGroup)
                {

                    masterNav.Groups.Add(menuGroup.MenuGroupName, menuGroup.MenuGroupID);


                    List<Menu> listMenu = CMSfunction.SelectMenuByProfileIDandMenuGroup(Session["SiteProfile"].ToString(),
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

            RefreshDataGrid();
        }

        

        private void ProcessUpdate()
        {
            DATAMODEL.AccessProfileHeader accessProfile = new DATAMODEL.AccessProfileHeader();

            accessProfile.Profile = Session["AccessProfileManagement"].ToString();
            accessProfile.Description = TextBoxAccessProfileDescription.Text;

            message = CMSfunction.updateAccessProfile(accessProfile, Session["UserID"].ToString());
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            ProcessUpdate();
            Response.Redirect("AccessProfileHeader.aspx");
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccessProfileHeader.aspx");
        }

        private void RefreshDataGrid()
        {
            accessProfile = new DATAMODEL.AccessProfileHeader();

            TextBoxAccessProfile.Text = Session["AccessProfileManagement"].ToString();
            accessProfile.Profile = Session["AccessProfileManagement"].ToString();

            accessProfile = CMSfunction.GetAccessProfileDetail(accessProfile);

            TextBoxAccessProfileDescription.Text = accessProfile.Description;
        }
    }
}