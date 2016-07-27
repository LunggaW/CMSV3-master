using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using DevExpress.Web.Internal;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;

namespace KBS.KBS.CMSV3.Administration.SiteProfileManagement
{
    public partial class SiteProfileHeader : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSiteProfile = new DataTable();
        private DATAMODEL.SiteProfileHeader siteProfile;
        private String MenuID = ConfigurationManager.AppSettings["MenuIdSiteProfile"];

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/logins.aspx");
            }
            else
            {
                loadNavBar();
                loadButton(MenuID);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string Compare = Session["Filter"].ToString();
            if (Compare != "")
            {
                if (Session["Filter"].ToString() != "SiteManagement")
                {
                    UserManagement.UserManagementHeader Sunda = new UserManagement.UserManagementHeader();
                    Sunda.ClearDataSeasson();
                }
            }
            
            RefreshDataGrid();
            
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

        private void loadButton(String MenuID)
        {

            List<AccessContainer> listAccessCont =
                CMSfunction.SelectAccessByProfileAndMenuID(Session["AccessProfile"].ToString(), MenuID);

            foreach (var accessContainer in listAccessCont)
            {
                switch (accessContainer.FunctionId)
                {
                    case "1":
                        AddBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        break;
                    case "2":
                        //Ed.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        if (accessContainer.Type == "0")
                        {
                            ASPxGridViewSiteProfile.ClientSideEvents.RowDblClick = null;
                        }

                        break;
                    case "3":
                        SearchBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        break;
                    case "4":
                        DelBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        break;
                    default:
                        break;

                }
            }
        }


        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }


        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewSiteProfile.FocusedRowIndex != -1)
            {
                OutputMessage message = new OutputMessage();

                DATAMODEL.SiteProfileHeader siteProfile = new DATAMODEL.SiteProfileHeader();

                siteProfile.Profile =
                    ASPxGridViewSiteProfile.GetRowValues(ASPxGridViewSiteProfile.FocusedRowIndex, "SITE PROFILE")
                        .ToString();

                message = CMSfunction.DeleteSiteProfileHeader(siteProfile);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;

                RefreshDataGrid();
            }
        }

        protected void ASPxGridViewSiteProfile_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            Session["SiteProfileManagement"] = ASPxGridViewSiteProfile.GetRowValues(Convert.ToInt32(e.Parameters), "SITE PROFILE").ToString();

            Session["Filter"] = "SiteManagement";
            Session["SiteProfFilter"] = "True";
            Session["SiteProfIdFilter"] = !string.IsNullOrWhiteSpace(TextBoxSiteProfile.Text) ? TextBoxSiteProfile.Text : "";
            Session["SiteProfDescFilter"] = !string.IsNullOrWhiteSpace(TextBoxSiteProfileDescription.Text) ? TextBoxSiteProfileDescription.Text : "";


            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("SiteProfileHeaderDetail.aspx");
            else
                Response.Redirect("SiteProfileHeaderDetail.aspx");
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("SiteProfileHeaderNew.aspx");
        }

        protected void ASPxButtonEntry_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewSiteProfile.FocusedRowIndex != -1)
            {
                Session["SiteProfileManagement"] = ASPxGridViewSiteProfile.GetRowValues(ASPxGridViewSiteProfile.FocusedRowIndex, "SITE PROFILE").ToString();
                Response.Redirect("SiteProfileLinkManagement.aspx");
            }
        }

        private void RefreshDataGrid()
        {
            siteProfile = new DATAMODEL.SiteProfileHeader();

            if (Session["SiteProfFilter"] != null)
            {
                siteProfile.Profile = !string.IsNullOrWhiteSpace(Session["SiteProfIdFilter"].ToString()) ? Session["SiteProfIdFilter"].ToString() : "";
                siteProfile.Description = !string.IsNullOrWhiteSpace(Session["SiteProfDescFilter"].ToString()) ? Session["SiteProfDescFilter"].ToString() : "";

                if (!string.IsNullOrWhiteSpace(siteProfile.Profile))
                {
                    TextBoxSiteProfile.Text = siteProfile.Profile;
                }

                if (!string.IsNullOrWhiteSpace(siteProfile.Description))
                {
                    TextBoxSiteProfileDescription.Text = siteProfile.Description;
                }


                Session.Remove("MenuProfFilter");
            }
            else
            {
                siteProfile.Profile = !string.IsNullOrWhiteSpace(TextBoxSiteProfile.Text) ? TextBoxSiteProfile.Text : "";
                siteProfile.Description = !string.IsNullOrWhiteSpace(TextBoxSiteProfileDescription.Text) ? TextBoxSiteProfileDescription.Text : "";

            }

            

            DTSiteProfile = CMSfunction.GetAllSiteProfileFiltered(siteProfile);
            ASPxGridViewSiteProfile.DataSource = DTSiteProfile;
            ASPxGridViewSiteProfile.KeyFieldName = "SITE PROFILE";
            ASPxGridViewSiteProfile.DataBind();
        }
        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewSiteProfile.PageIndex <= ASPxGridViewSiteProfile.PageCount - 1)
            {
                ASPxGridViewSiteProfile.PageIndex = ASPxGridViewSiteProfile.PageIndex + 1;
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewSiteProfile.PageIndex - 1 >= 0)
            {
                ASPxGridViewSiteProfile.PageIndex = ASPxGridViewSiteProfile.PageIndex - 1;
            }
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewSiteProfile.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewSiteProfile.PageIndex = ASPxGridViewSiteProfile.PageCount - 1;
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            TextBoxSiteProfile.Text = "";
            TextBoxSiteProfileDescription.Text = "";
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {

        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewSiteProfile.FocusedRowIndex != -1)
            {
                Session["SiteProfileManagement"] =
                    ASPxGridViewSiteProfile.GetRowValues(ASPxGridViewSiteProfile.FocusedRowIndex, "SITE PROFILE").ToString();

                Session["Filter"] = "SiteManagement";
                Session["SiteProfFilter"] = "True";
                Session["SiteProfIdFilter"] = !string.IsNullOrWhiteSpace(TextBoxSiteProfile.Text)
                    ? TextBoxSiteProfile.Text
                    : "";
                Session["SiteProfDescFilter"] = !string.IsNullOrWhiteSpace(TextBoxSiteProfileDescription.Text)
                    ? TextBoxSiteProfileDescription.Text
                    : "";


                if (Page.IsCallback)
                    ASPxWebControl.RedirectOnCallback("SiteProfileHeaderDetail.aspx");
                else
                    Response.Redirect("SiteProfileHeaderDetail.aspx");
            }
        }
    }
}