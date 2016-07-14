using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.Administration.SiteProfileManagement
{
    public partial class SiteProfileLinkManagementDetail : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSite = new DataTable();
        OutputMessage message = new OutputMessage();

        protected override void OnInit(EventArgs e)
        {
            if (Session["Site"] == null)
            {
                Response.Redirect("SiteProfileLinkManagementDetail.aspx");
            }
            else if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/Logins.aspx");
            }
            else if (Session["SiteProfileManagement"] == null)
            {
                Response.Redirect("SiteProfileHeader.aspx");
            }
            else
            {
                LabelMessage.Visible = false;
                loadNavBar();
            }
           
        }

        protected void Page_Load(object sender, EventArgs e)
        {
                if (!IsPostBack)
                {
                    SiteProfileLink siteProfileLink = new SiteProfileLink();

                    siteProfileLink.SiteProfile = Session["SiteProfileManagement"].ToString();
                    siteProfileLink.Site = Session["Site"].ToString();

                    siteProfileLink = CMSfunction.GetSiteProfileLink(siteProfileLink);

                        sdate.Value = string.IsNullOrWhiteSpace(siteProfileLink.StartDate.ToString())
                        ? (object) ""
                        : siteProfileLink.StartDate;

                     edate.Value = string.IsNullOrWhiteSpace(siteProfileLink.EndDate.ToString())
                        ? (object) ""
                        : siteProfileLink.EndDate;
                    
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
            //ParameterDetail parDetail = new ParameterDetail();

            //parHeader.Lock = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderBlock.Text) ? ASPxTextBoxHeaderBlock.Text : "";
            //parHeader.Comment = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderComment.Text) ? ASPxTextBoxHeaderComment.Text : "";
            //parHeader.Copy = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderCopy.Text) ? ASPxTextBoxHeaderCopy.Text : "";
            //parHeader.ID = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderID.Text) ? ASPxTextBoxHeaderID.Text : "";
            //parHeader.Name = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderName.Text) ? ASPxTextBoxHeaderName.Text : "";

            //DTParameterHeader = CMSfunction.GetParameterHeaderData(parHeader);
            //ASPxGridViewDetail.DataSource = DTParameterHeader;
            //ASPxGridViewDetail.KeyFieldName = "ID"; ASPxGridViewHeader.DataBind();
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            //ASPxTextBoxHeaderBlock.Text = "";
            //ASPxTextBoxHeaderComment.Text = "";
            //ASPxTextBoxHeaderCopy.Text = "";
            //ASPxTextBoxHeaderID.Text = "";
            //ASPxTextBoxHeaderName.Text = "";
            //ASPxTextBoxHeaderSClas.Text = "";
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            //Session.Remove("ParamDetailClass");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("SiteProfileLinkManagement.aspx");
            else

                Response.Redirect("SiteProfileLinkManagement.aspx");
            //Session.Remove("ParamHeaderID");
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            ProcessUpdate();

            LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            LabelMessage.Text = message.Message;
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            ProcessUpdate();
            Response.Redirect("SiteProfileLinkManagement.aspx");
        }

        private void ProcessUpdate()
        {
            SiteProfileLink siteProfileLink = new SiteProfileLink();

            siteProfileLink.SiteProfile = Session["SiteProfileManagement"].ToString();
            siteProfileLink.Site = Session["Site"].ToString();

            siteProfileLink.StartDate = sdate.Date;
            siteProfileLink.EndDate = edate.Date;

            message = CMSfunction.updateSiteProfileLinkManagementDetail(siteProfileLink, Session["UserID"].ToString());
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {

        }



    }
}