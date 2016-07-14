using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.Administration
{
    public partial class UserManagement : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTProfile = new DataTable();
        private DataTable DTSite = new DataTable();
        private DataTable DTGridViewUser = new DataTable();
        private Member user;

        protected override void OnInit(EventArgs e)
        {
            user = CMSfunction.SelectUserDataFromUserID(User.Identity.Name);
            loadNavBar();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            user = CMSfunction.SelectUserDataFromUserID(User.Identity.Name);

            DTSite = CMSfunction.GetSiteDataByProfileID(user.MenuProfile);
            ASPxComboBoxUserManagementSite.DataSource = DTSite;
            ASPxComboBoxUserManagementSite.ValueField = "SITECODE";
            ASPxComboBoxUserManagementSite.ValueType = typeof(string);
            ASPxComboBoxUserManagementSite.TextField = "SITENAME";
            ASPxComboBoxUserManagementSite.DataBind();

            DTProfile = CMSfunction.GetAllProfile();
            ASPxComboBoxUserManagementProfile.DataSource = DTProfile;
            ASPxComboBoxUserManagementProfile.ValueField = "PROFILEID";
            ASPxComboBoxUserManagementProfile.ValueType = typeof(string);
            ASPxComboBoxUserManagementProfile.TextField = "PROFILENAME";
            ASPxComboBoxUserManagementProfile.DataBind();

            DTGridViewUser = CMSfunction.GetAllUser();
            ASPxGridViewUserManagementUser.DataSource = DTGridViewUser;
            ASPxGridViewUserManagementUser.KeyFieldName = "USERID";
            ASPxGridViewUserManagementUser.DataBind();
            //loadNavBar();

            
        }

        protected void ASPxButtonUserManagementInsert_Click(object sender, EventArgs e)
        {

            ASPxTextBoxUserManagementUserID.Text = String.Empty;
                ASPxTextBoxUserManagementUserName.Text = String.Empty;
                ASPxTextBoxUserManagementPassword.Text = String.Empty;

        }

        private void loadNavBar()
        {

            List<Menu> listMenuGroup = CMSfunction.SelectMenuGroupByProfileID(user.MenuProfile);
            int i = 0;

            ASPxSplitter sp = Master.Master.FindControl("ASPxSplitter1").FindControl("Content").FindControl("ContentSplitter") as ASPxSplitter;
            ASPxNavBar masterNav = sp.FindControl("ASPxNavBar1") as ASPxNavBar;

            if (masterNav != null)
            {
                foreach (var menuGroup in listMenuGroup)
                {

                    masterNav.Groups.Add(menuGroup.MenuGroupName, menuGroup.MenuGroupNameID);


                    List<Menu> listMenu = CMSfunction.SelectMenuByProfileIDandMenuGroup(user.MenuProfile,
                        menuGroup.MenuGroupNameID);
                    foreach (var menuItem in listMenu)
                    {
                        masterNav.Groups[i].Items.Add(menuItem.MenuName, menuItem.MenuNameID, null, menuItem.MenuGroupURL);
                        masterNav.Groups[i].HeaderStyle.BackColor = ColorTranslator.FromHtml("#6076B4");
                        masterNav.Groups[i].HeaderStyle.BorderColor = ColorTranslator.FromHtml("#6076B4");
                    }
                    i++;

                }
            }
            masterNav.DataBind();

        }

        protected void ASPxGridViewUserManagementUser_FocusedRowChanged(object sender, EventArgs e)
        {

        }

        protected void ASPxButtonUpdate_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewUserManagementUser.FocusedRowIndex != -1)
            {
                ASPxTextBoxUserManagementUserID.ReadOnly = true;
                ASPxTextBoxUserManagementUserID.Text = ASPxGridViewUserManagementUser.GetRowValues(ASPxGridViewUserManagementUser.FocusedRowIndex, "USERID").ToString();
                ASPxTextBoxUserManagementUserName.Text = ASPxGridViewUserManagementUser.GetRowValues(ASPxGridViewUserManagementUser.FocusedRowIndex, "USERNAME").ToString();
                ASPxTextBoxUserManagementPassword.Text = ASPxGridViewUserManagementUser.GetRowValues(ASPxGridViewUserManagementUser.FocusedRowIndex, "PASSWORD").ToString();


                ASPxComboBoxUserManagementProfile.SelectedItem =
                    ASPxComboBoxUserManagementProfile.Items.FindByText(
                        ASPxGridViewUserManagementUser.GetRowValues(ASPxGridViewUserManagementUser.FocusedRowIndex, "PROFILE")
                            .ToString());
            }
            
        }

        protected void ASPxButtonDelete_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewUserManagementUser.FocusedRowIndex != -1)
            {
                ASPxTextBoxUserManagementUserID.ReadOnly = true;
                ASPxTextBoxUserManagementUserID.Text = ASPxGridViewUserManagementUser.GetRowValues(ASPxGridViewUserManagementUser.FocusedRowIndex, "USERID").ToString();
                ASPxTextBoxUserManagementUserName.Text = ASPxGridViewUserManagementUser.GetRowValues(ASPxGridViewUserManagementUser.FocusedRowIndex, "USERNAME").ToString();
                ASPxTextBoxUserManagementPassword.Text = ASPxGridViewUserManagementUser.GetRowValues(ASPxGridViewUserManagementUser.FocusedRowIndex, "PASSWORD").ToString();


                ASPxComboBoxUserManagementProfile.SelectedItem =
                    ASPxComboBoxUserManagementProfile.Items.FindByText(
                        ASPxGridViewUserManagementUser.GetRowValues(ASPxGridViewUserManagementUser.FocusedRowIndex, "PROFILE")
                            .ToString());
            }
        }

        

    }
}