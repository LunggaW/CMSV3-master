using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.MasterData
{
    public partial class ColorMasterManagement : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTParameterHeader = new DataTable();
        private DataTable DTParameterDetail = new DataTable();
        private DataTable DTGridViewUser = new DataTable();
        private User user;

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/Logins.aspx");
            }
            else
            {
                loadNavBar();
            }
            //user = CMSfunction.SelectUserDataFromUserID(User.Identity.Name);

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ParameterHeader parHeader = new ParameterHeader();
            DTParameterHeader = CMSfunction.GetParameterHeaderDataTable(parHeader, Session["Class"].ToString());
            ASPxGridViewHeader.DataSource = DTParameterHeader;
            ASPxGridViewHeader.KeyFieldName = "ID";
            ASPxGridViewHeader.DataBind();
            //user = CMSfunction.SelectUserDataFromUserID(User.Identity.Name);

            //ASPxComboBoxUserManagementSite.DataSource = DTSite;
            //ASPxComboBoxUserManagementSite.ValueField = "SITECODE";
            //ASPxComboBoxUserManagementSite.ValueType = typeof(string);
            //ASPxComboBoxUserManagementSite.TextField = "SITENAME";
            //ASPxComboBoxUserManagementSite.DataBind();

            //DTProfile = CMSfunction.GetAllProfile();
            //ASPxComboBoxUserManagementProfile.DataSource = DTProfile;
            //ASPxComboBoxUserManagementProfile.ValueField = "PROFILEID";
            //ASPxComboBoxUserManagementProfile.ValueType = typeof(string);
            //ASPxComboBoxUserManagementProfile.TextField = "PROFILENAME";
            //ASPxComboBoxUserManagementProfile.DataBind();

            //DTGridViewUser = CMSfunction.GetAllUser();
            //ASPxGridViewUserManagementUser.DataSource = DTGridViewUser;
            //ASPxGridViewUserManagementUser.KeyFieldName = "USERID";
            //ASPxGridViewUserManagementUser.DataBind();
            //loadNavBar();


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

                    masterNav.Groups.Add(menuGroup.MenuGroupName, menuGroup.MenuGroupNameID);


                    List<Menu> listMenu = CMSfunction.SelectMenuByProfileIDandMenuGroup(Session["SiteProfile"].ToString(),
                        menuGroup.MenuGroupNameID);
                    foreach (var menuItem in listMenu)
                    {
                        masterNav.Groups[i].Items.Add(menuItem.MenuName, menuItem.MenuNameID, null, menuItem.MenuGroupURL);

                    }
                    i++;

                }
            }
            masterNav.DataBind();

        }

        protected void ASPxGridViewHeader_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            Session["ParamHeaderIDforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
            Session["ParamHeaderSClassforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SCLAS").ToString();
            Session["ParamCopy"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "COPY").ToString();

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("ParameterManagementHeaderDetail.aspx");
            else

                Response.Redirect("ParameterManagementHeaderDetail.aspx");
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            ParameterHeader parHeader = new ParameterHeader();

            //parHeader.Lock = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderBlock.Text) ? ASPxTextBoxHeaderBlock.Text : "";
            //parHeader.Comment = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderComment.Text) ? ASPxTextBoxHeaderComment.Text : "";
            //parHeader.Copy = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderCopy.Text) ? ASPxTextBoxHeaderCopy.Text : "";
            parHeader.ID = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderID.Text) ? ASPxTextBoxHeaderID.Text : "";
            parHeader.Name = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderName.Text) ? ASPxTextBoxHeaderName.Text : "";

            DTParameterHeader = CMSfunction.GetParameterHeaderDataTable(parHeader, Session["Class"].ToString());
            ASPxGridViewHeader.DataSource = DTParameterHeader;
            ASPxGridViewHeader.KeyFieldName = "ID";
            ASPxGridViewHeader.DataBind();
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            //ASPxTextBoxHeaderBlock.Text = "";
            //ASPxTextBoxHeaderComment.Text = "";
            //ASPxTextBoxHeaderCopy.Text = "";
            ASPxTextBoxHeaderID.Text = ""; ASPxTextBoxHeaderName.Text = "";
            //ASPxTextBoxHeaderSClas.Text = "";
        }


        protected void ASPxButtonEntry_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                Session["ParamHeaderID"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
                Session["ParamHeaderSClass"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SCLAS").ToString();
                Session["ParamHeaderCopy"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "COPY").ToString();
                Response.Redirect("ParameterManagementDetail.aspx");
            }


        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ParameterManagementHeaderNew.aspx");
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                String ParamHeaderID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
                String ParamSClas = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SCLAS").ToString();


                OutputMessage message = new OutputMessage();

                message = CMSfunction.DeleteParameterHeader(ParamHeaderID, ParamSClas);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;
            }
        }



    }

}