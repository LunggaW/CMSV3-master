using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.Administration.UserManagement
{
    public partial class UserManagementHeaderDetail : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTAccessProfile = new DataTable();
        private DataTable DTSiteProfile = new DataTable();
        private DataTable DTUserStatus = new DataTable();
        private DataTable DTUserType = new DataTable();
        private DataTable DTMenuProfile = new DataTable();
        OutputMessage message = new OutputMessage();
        private User user;

        protected override void OnInit(EventArgs e)
        {
            LabelMessage.Visible = false;
            //user = CMSfunction.SelectUserDataFromUserID(User.Identity.Name);
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserIdUserManagement"] == null)
            
            {
                Response.Redirect("UserManagementHeader.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    
                    loadNavBar();

                    DTAccessProfile = CMSfunction.GetAccessProfile();

                    DTSiteProfile = CMSfunction.GetSiteProfile();

                    DTMenuProfile = CMSfunction.GetMenuProfile();

                    DTUserStatus = CMSfunction.GetParameterbyClassAndTabID("0", "1");

                    DTUserType = CMSfunction.GetParameterbyClassAndTabID("0", "2");

                    ComboSiteProfile.DataSource = DTSiteProfile;
                    ComboSiteProfile.ValueField = "SITEPROFILE";
                    ComboSiteProfile.ValueType = typeof(string);
                    ComboSiteProfile.TextField = "SITEPROFILEDESC";
                    ComboSiteProfile.DataBind();

                    ComboAccessProfile.DataSource = DTAccessProfile;
                    ComboAccessProfile.ValueField = "ACCESSPROFILE";
                    ComboAccessProfile.ValueType = typeof(string);
                    ComboAccessProfile.TextField = "ACCESSPROFILEDESC";
                    ComboAccessProfile.DataBind();

                    ComboMenuProfile.DataSource = DTMenuProfile;
                    ComboMenuProfile.ValueField = "MENUPROFILE";
                    ComboMenuProfile.ValueType = typeof(string);
                    ComboMenuProfile.TextField = "MENUPROFILEDESC";
                    ComboMenuProfile.DataBind();

                    ComboUserType.DataSource = DTUserType;
                    ComboUserType.ValueField = "PARVALUE";
                    ComboUserType.ValueType = typeof(string);
                    ComboUserType.TextField = "PARDESCRIPTION";
                    ComboUserType.DataBind();

                    ComboUserStatus.DataSource = DTUserStatus;
                    ComboUserStatus.ValueField = "PARVALUE";
                    ComboUserStatus.ValueType = typeof(string);
                    ComboUserStatus.TextField = "PARDESCRIPTION";
                    ComboUserStatus.DataBind();

                    
                    user = new User();
                    user = CMSfunction.GetUserFromUserId(Session["UserIdUserManagement"].ToString());

                    user.UserID = Session["UserIdUserManagement"].ToString();
                    ASPxTextBoxUserManagementUserID.Text = user.UserID;
                    ASPxTextBoxUserManagementUserName.Text = user.Username;
                    UserPassword.Text = user.Password;
                    UserDesc.Text = user.Description;
                    IMEI.Text = user.IMEI;
                    ComboUserStatus.SelectedItem = ComboUserType.Items.FindByValue(user.Status);
                    ComboUserType.SelectedItem = ComboUserType.Items.FindByValue(user.UserType);
                    ComboUserStatus.SelectedItem = ComboUserStatus.Items.FindByValue(user.Status);
                    ComboMenuProfile.SelectedItem = ComboMenuProfile.Items.FindByValue(user.MenuProfile);
                    ComboSiteProfile.SelectedItem = ComboSiteProfile.Items.FindByValue(user.SiteProfile);
                    ComboAccessProfile.SelectedItem = ComboAccessProfile.Items.FindByValue(user.AccessProfile);


                    if (user.StartDate.HasValue)
                    {
                        sdate.Date = user.StartDate.Value;
                    }

                    if (user.EndDate.HasValue)
                    {
                        edate.Date = user.EndDate.Value;
                    }

                }
            }


        }


        #region LoadNavBar LoadButton
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

        
        #endregion

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
            Session.Remove("UserIdUserManagement");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("UserManagementHeader.aspx");
            else

                Response.Redirect("UserManagementHeader.aspx");
            //Session.Remove("ParamHeaderID");
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            processUpdate();
            
            LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            LabelMessage.Text = message.Message;

            LabelMessage.Visible = true;
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            processUpdate();
            Response.Redirect("UserManagementHeader.aspx");
        }

        private void processUpdate()
        {
            user = new User();

            user.UserID = !string.IsNullOrWhiteSpace(ASPxTextBoxUserManagementUserID.Text) ? ASPxTextBoxUserManagementUserID.Text : "";
            user.Username = !string.IsNullOrWhiteSpace(ASPxTextBoxUserManagementUserName.Text) ? ASPxTextBoxUserManagementUserName.Text : "";
            user.Password = !string.IsNullOrWhiteSpace(UserPassword.Text) ? UserPassword.Text : ""; 
            user.IMEI = !string.IsNullOrWhiteSpace(IMEI.Text) ? IMEI.Text : "";
            user.UserType = !string.IsNullOrWhiteSpace(ComboUserType.Value.ToString()) ? ComboUserType.Value.ToString() : "";
            user.Status = !string.IsNullOrWhiteSpace(ComboUserStatus.Value.ToString()) ? ComboUserStatus.Value.ToString() : "";
            user.Description = !string.IsNullOrWhiteSpace(UserDesc.Text) ? UserDesc.Text : "";
            user.StartDate = !string.IsNullOrWhiteSpace(sdate.Date.ToString()) ? sdate.Date : sdate.MinDate;
            user.EndDate = !string.IsNullOrWhiteSpace(edate.Date.ToString()) ? edate.Date : sdate.MinDate;
            user.AccessProfile = !string.IsNullOrWhiteSpace(ComboAccessProfile.Value.ToString()) ? ComboAccessProfile.Value.ToString() : "";
            user.SiteProfile = !string.IsNullOrWhiteSpace(ComboSiteProfile.Value.ToString()) ? ComboSiteProfile.Value.ToString() : "";
            user.MenuProfile = !string.IsNullOrWhiteSpace(ComboMenuProfile.Value.ToString()) ? ComboMenuProfile.Value.ToString() : "";
            user.IMEI = !string.IsNullOrWhiteSpace(IMEI.Text) ? IMEI.Text : "";



            message = CMSfunction.updateUser(user, Session["UserID"].ToString());

        }

    }
}