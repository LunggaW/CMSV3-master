using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;
using KBS.KBS.CMSV3.Administration;

namespace KBS.KBS.CMSV3.Administration
{
    public partial class ParameterManagementDetail : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTParameterDetail = new DataTable();
        private String MenuID = ConfigurationManager.AppSettings["MenuIdParameterManagement"];

        protected override void OnInit(EventArgs e)
        {
            if (Session["ParamHeaderID"] == null)
            {
                Response.Redirect("ParameterManagementHeader.aspx");
            }
            else
            {
                loadNavBar();
                //loadButton(MenuID);
            }
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["ParamHeaderID"] == null)
            {
                Response.Redirect("ParameterManagementHeader.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    RefreshDataGrid();
                }
            }

            
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
                            ASPxGridViewDetail.ClientSideEvents.RowDblClick = null;
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

        protected void ASPxGridViewDetail_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            Session["ParamDetailID"] = ASPxGridViewDetail.GetRowValues(Convert.ToInt32(e.Parameters), "ID").ToString();
            Session["ParamDetailEntry"] = ASPxGridViewDetail.GetRowValues(Convert.ToInt32(e.Parameters), "ENTRY").ToString();
            Session["ParamDetailClass"] = ASPxGridViewDetail.GetRowValues(Convert.ToInt32(e.Parameters), "SITE CLASS").ToString();
            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("ParameterManagementDetailDetail.aspx");
            else
                Response.Redirect("ParameterManagementDetailDetail.aspx");
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            Session.Remove("ParamHeaderID");
            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("ParameterManagementHeader.aspx");
            else
                Response.Redirect("ParameterManagementHeader.aspx");
            
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {

                if (Page.IsCallback)
                    ASPxWebControl.RedirectOnCallback("ParameterManagementDetailNew.aspx");
                else
                    Response.Redirect("ParameterManagementDetailNew.aspx");

            
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewDetail.FocusedRowIndex != -1)
            {
                String ParamHeaderID = ASPxGridViewDetail.GetRowValues(ASPxGridViewDetail.FocusedRowIndex, "ID").ToString();
                String ParamEntry = ASPxGridViewDetail.GetRowValues(ASPxGridViewDetail.FocusedRowIndex, "ENTRY").ToString();
                String ParamSClas = ASPxGridViewDetail.GetRowValues(ASPxGridViewDetail.FocusedRowIndex, "SITE CLASS").ToString();
                String Copy = Session["ParamHeaderCopy"].ToString();

                OutputMessage message = new OutputMessage();

                message = CMSfunction.DeleteParameterDetail(ParamHeaderID, ParamSClas, ParamEntry, Copy);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;

                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            DTParameterDetail = CMSfunction.GetParameterDetailDataTable(Session["ParamHeaderID"].ToString(), Session["Class"].ToString());
            ASPxGridViewDetail.DataSource = DTParameterDetail;
            ASPxGridViewDetail.KeyFieldName = "ID";
            ASPxGridViewDetail.DataBind();
        }


    }
}