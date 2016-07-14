using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;

namespace KBS.KBS.CMSV3.Administration.SiteProfileManagement
{
    public partial class SiteProfileLinkManagement : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSiteProfileLink = new DataTable();
        private DataTable DTTable = new DataTable();
        private DATAMODEL.SiteProfileHeader siteProfile;

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/logins.aspx");
            }
            else if (Session["SiteProfileManagement"] == null)
            {
                Response.Redirect("SiteProfileHeader.aspx");
            }
            else
            {
                loadNavBar();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();

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
        }


        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewSiteProfileLink.FocusedRowIndex != -1)
            {
                SiteProfileLink siteProfileLink = new SiteProfileLink();

                siteProfileLink.Site = ASPxGridViewSiteProfileLink.GetRowValues(ASPxGridViewSiteProfileLink.FocusedRowIndex, "SITE").ToString();

                siteProfileLink.SiteProfile = Session["SiteProfileManagement"].ToString();

                OutputMessage message = new OutputMessage();

                message = CMSfunction.DeleteSiteProfileLink(siteProfileLink);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;

                RefreshDataGrid();
            }
        }

        protected void ASPxGridViewSiteProfile_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            Session["Site"] = ASPxGridViewSiteProfileLink.GetRowValues(Convert.ToInt32(e.Parameters), "SITE").ToString();
            
            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("SiteProfileLinkManagementDetail.aspx");
            else
                Response.Redirect("SiteProfileLinkManagementDetail.aspx");
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {

            DTTable = CMSfunction.GetSiteExcludeSiteProfile(Session["SiteProfileManagement"].ToString());

            if (DTTable.Rows.Count == 0)
            {
                LabelMessage.ForeColor =  Color.Red;

                LabelMessage.Visible = true;
                LabelMessage.Text = "No More Site to be added";

            }
            else
            {
                Response.Redirect("SiteProfileLinkManagementNew.aspx");
            }
            
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {

        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("SiteProfileHeader.aspx");
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {

        }
        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewSiteProfileLink.PageIndex <= ASPxGridViewSiteProfileLink.PageCount - 1)
            {
                ASPxGridViewSiteProfileLink.PageIndex = ASPxGridViewSiteProfileLink.PageIndex + 1;
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewSiteProfileLink.PageIndex - 1 > 0)
            {
                ASPxGridViewSiteProfileLink.PageIndex = ASPxGridViewSiteProfileLink.PageIndex - 1;
            }
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewSiteProfileLink.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewSiteProfileLink.PageIndex = ASPxGridViewSiteProfileLink.PageCount - 1;
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {

        }
        
        private void RefreshDataGrid()
        {
            DTSiteProfileLink = CMSfunction.GetSiteProfileLinkDataTable(Session["SiteProfileManagement"].ToString());
            ASPxGridViewSiteProfileLink.DataSource = DTSiteProfileLink;
            ASPxGridViewSiteProfileLink.KeyFieldName = "SITE";
            ASPxGridViewSiteProfileLink.DataBind();
        }


    }
}