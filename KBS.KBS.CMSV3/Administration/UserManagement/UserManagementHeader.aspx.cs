using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using DevExpress.Office.NumberConverters;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.Administration.UserManagement
{
    public partial class UserManagementHeader : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTAccessProfile = new DataTable();
        private DataTable DTSiteProfile = new DataTable();
        private DataTable DTUserStatus = new DataTable();
        private DataTable DTUserType = new DataTable();
        private DataTable DTMenuProfile = new DataTable();
        private DataTable DTGridViewUser = new DataTable();
        private User user;
        private String MenuID = ConfigurationManager.AppSettings["MenuIdUserManagement"];
        private String Lepat;
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
                //if (!IsPostBack)
                //{
                //    loadNavBar();
                //}
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
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
                string Compare = Session["Filter"].ToString();
                if (Compare != "")
                {
                    if (Session["Filter"].ToString() != "UserManagement")
                    {
                        UserManagementHeader Sunda = new UserManagementHeader();
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
                            ASPxGridViewUser.ClientSideEvents.RowDblClick = null;    
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
            
        }

        protected void ASPxGridViewUser_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            Session["Filter"] = "UserManagement";
            Session["UserFilter"] = "True";
            Session["UserIdFilter"] = !string.IsNullOrWhiteSpace(ASPxTextBoxUserManagementUserID.Text) ? ASPxTextBoxUserManagementUserID.Text : "";

            Session["StartDateFilter"] = sdate.Date != sdate.MinDate ? (object)sdate.Date : "";
            Session["EndDateFilter"] = edate.Date != edate.MinDate ? (object) edate.Date : "";

            Session["UserDescFilter"] = !string.IsNullOrWhiteSpace(UserDesc.Text) ? UserDesc.Text : "";
            Session["UserAccProfFilter"] = (ComboAccessProfile.Value != null) ? ComboAccessProfile.Value.ToString() : "";
            Session["UserNameFilter"] = !string.IsNullOrWhiteSpace(ASPxTextBoxUserManagementUserName.Text) ? ASPxTextBoxUserManagementUserName.Text : "";
            Session["UserStatFilter"] = (ComboUserStatus.Value != null) ? ComboUserStatus.Value.ToString() : "";
            Session["UserTypeFilter"] = (ComboUserType.Value != null) ? ComboUserType.Value.ToString() : "";
            Session["UserMenuProfFilter"] = (ComboMenuProfile.Value != null) ? ComboMenuProfile.Value.ToString() : "";
            Session["UserSiteProfFilter"] = (ComboSiteProfile.Value != null) ? ComboSiteProfile.Value.ToString() : "";



            Session["UserIdUserManagement"] = ASPxGridViewUser.GetRowValues(Convert.ToInt32(e.Parameters), "USER ID").ToString();

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("UserManagementHeaderDetail.aspx");
            else
                Response.Redirect("UserManagementHeaderDetail.aspx");
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewUser.FocusedRowIndex != -1)
            {
                String UserID = ASPxGridViewUser.GetRowValues(ASPxGridViewUser.FocusedRowIndex, "USER ID").ToString();
                


                OutputMessage message = new OutputMessage();

                message = CMSfunction.DeleteUser(UserID, Session["UserID"].ToString());

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;

                RefreshDataGrid();
            }
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserManagementHeaderNew.aspx");
        }


        #region Navigation Button
        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewUser.PageIndex <= ASPxGridViewUser.PageCount - 1)
            {
                ASPxGridViewUser.PageIndex = ASPxGridViewUser.PageIndex + 1;
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewUser.PageIndex - 1 >= 0)
            {
                ASPxGridViewUser.PageIndex = ASPxGridViewUser.PageIndex - 1;
            }
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewUser.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewUser.PageIndex = ASPxGridViewUser.PageCount - 1;
        }

        #endregion

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserManagementHeader.aspx");
            /*ASPxTextBoxUserManagementUserID.Text = "";
            ASPxTextBoxUserManagementUserName.Text = "";
            UserDesc.Text = "";
            */
        }



        private void RefreshDataGrid()
        {
            User user = new User();

            if (Session["UserFilter"] != null)
            {
                

                user.UserID = !string.IsNullOrWhiteSpace(Session["UserIdFilter"].ToString()) ? Session["UserIdFilter"].ToString() : "";

                DateTime container;
                user.StartDate = DateTime.TryParse(Session["StartDateFilter"].ToString(), out container)
                    ? (DateTime?) container
                    : null;

                user.EndDate = DateTime.TryParse(Session["EndDateFilter"].ToString(), out container)
                    ? (DateTime?)container
                    : null;

                user.Description = !string.IsNullOrWhiteSpace(Session["UserDescFilter"].ToString()) ? Session["UserDescFilter"].ToString() : "";
                user.AccessProfile = !string.IsNullOrWhiteSpace(Session["UserAccProfFilter"].ToString()) ? Session["UserAccProfFilter"].ToString() : "";
                user.Username = !string.IsNullOrWhiteSpace(Session["UserNameFilter"].ToString()) ? Session["UserNameFilter"].ToString() : "";
                user.Status = !string.IsNullOrWhiteSpace(Session["UserStatFilter"].ToString()) ? Session["UserStatFilter"].ToString() : "";
                user.UserType = !string.IsNullOrWhiteSpace(Session["UserTypeFilter"].ToString()) ? Session["UserTypeFilter"].ToString() : "";
                user.MenuProfile = !string.IsNullOrWhiteSpace(Session["UserMenuProfFilter"].ToString()) ? Session["UserMenuProfFilter"].ToString() : "";
                user.SiteProfile = !string.IsNullOrWhiteSpace(Session["UserSiteProfFilter"].ToString()) ? Session["UserSiteProfFilter"].ToString() : "";



                if (!string.IsNullOrWhiteSpace(user.UserID))
                {
                    ASPxTextBoxUserManagementUserID.Text = user.UserID;
                }

                if (!string.IsNullOrWhiteSpace(user.Username))
                {
                    ASPxTextBoxUserManagementUserName.Text = user.Username;
                }

                if (!string.IsNullOrWhiteSpace(user.Description))
                {
                    UserDesc.Text = user.Description;
                }

                if (!string.IsNullOrWhiteSpace(user.UserType))
                {
                    ComboUserType.Items.FindByValue(user.UserType);
                }

                if (!string.IsNullOrWhiteSpace(user.Status))
                {
                    ComboUserStatus.Items.FindByValue(user.Status);
                }

                if (!string.IsNullOrWhiteSpace(user.SiteProfile))
                {
                    ComboSiteProfile.Items.FindByValue(user.SiteProfile);
                }

                if (!string.IsNullOrWhiteSpace(user.AccessProfile))
                {
                    ComboAccessProfile.Items.FindByValue(user.AccessProfile);
                }

                if (!string.IsNullOrWhiteSpace(user.MenuProfile))
                {
                    ComboMenuProfile.Items.FindByValue(user.MenuProfile);
                }

                if (user.StartDate.HasValue)
                {
                    sdate.Date = user.StartDate.Value;
                }

                if (user.EndDate.HasValue)
                {
                    edate.Date = user.EndDate.Value;
                }


               // Session.Remove("UserFilter");
            }
            else
            {
                user.UserID = !string.IsNullOrWhiteSpace(ASPxTextBoxUserManagementUserID.Text) ? ASPxTextBoxUserManagementUserID.Text : "";
                user.Username = !string.IsNullOrWhiteSpace(ASPxTextBoxUserManagementUserName.Text) ? ASPxTextBoxUserManagementUserName.Text : "";
                user.UserType = (ComboUserType.Value != null) ? ComboUserType.Value.ToString() : "";
                user.Status = (ComboUserStatus.Value != null) ? ComboUserStatus.Value.ToString() : "";
                user.Description = !string.IsNullOrWhiteSpace(UserDesc.Text) ? UserDesc.Text : "";


                user.EndDate = edate.Date != edate.MinDate ? (DateTime?) edate.Date : null;
                user.StartDate = sdate.Date != sdate.MinDate ? (DateTime?) sdate.Date : null;

                user.AccessProfile = (ComboAccessProfile.Value != null) ? ComboAccessProfile.Value.ToString() : "";
                user.SiteProfile = (ComboSiteProfile.Value != null) ? ComboSiteProfile.Value.ToString() : "";
                user.MenuProfile = (ComboMenuProfile.Value != null) ? ComboMenuProfile.Value.ToString() : "";
            }

            
            

            

            DTGridViewUser = CMSfunction.GetAllUserFiltered(user);
            ASPxGridViewUser.DataSource = DTGridViewUser;
            ASPxGridViewUser.KeyFieldName = "USER ID";
            ASPxGridViewUser.DataBind();
            
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {

        }
        public void ClearDataSeasson()
        {
            //Assortment
            Session.Remove("OrderData"); 
            try
            {
                Session.Remove("VariantIDExAssortment");
                Session.Remove("ItemIDExAssortment");
            }
            catch
            {
                Lepat = "Failed";
            }
            //User Management
            try
            {
                if (Session["UserFilter"].ToString() == "True")
                {
                    Session.Remove("UserFilter");
                    Session.Remove("UserIdFilter");
                    Session.Remove("StartDateFilter");
                    Session.Remove("EndDateFilter");
                    Session.Remove("UserDescFilter");
                    Session.Remove("UserAccProfFilter");
                    Session.Remove("UserNameFilter");
                    Session.Remove("UserStatFilter");
                    Session.Remove("UserTypeFilter");
                    Session.Remove("UserMenuProfFilter");
                    Session.Remove("UserSiteProfFilter");
                    Session.Remove("UserIdUserManagement");
                }
            }
            catch
            {
                Lepat = "Failed";
            }
            // Menu Prof Master
            try
            {
                if (Session["MenuProfFilter"].ToString() == "True")
                {
                    Session.Remove("MenuProfProfFilter");
                    Session.Remove("MenuProfDescFilter");
                    Session.Remove("MenuProfFilter");
                }
            }
            catch
            {
                Lepat = "Failed";
            }
            // Param Master
            try
            {
                if (Session["ParamForBack"].ToString() == "True")
                {
                    Session.Remove("ParamIDFilter");
                    Session.Remove("ParamNameFilter");
                    Session.Remove("ParamForBack");
                }
            }
            catch
            {
               Lepat = "Failed";
            }
            // Site Master
            try
            {
                if (Session["SiteProfFilter"].ToString() == "True")
                {
                    Session.Remove("SiteProfIdFilter");
                    Session.Remove("SiteProfDescFilter");
                    Session.Remove("SiteProfFilter");
                }
            }
            catch
            {
                Lepat = "Failed";
            }

            // Access Management
            try
            {
                if (Session["AccProfFilter"].ToString() == "True")
                {
                    Session.Remove("AccProfProfFilter");
                    Session.Remove("AccProfDescFilter");
                    Session.Remove("AccProfFilter");

                }
            }
            catch
            {
               Lepat = "Failed";
            }
            // Brand Master
            try
            {
                if (Session["BrandFilter"].ToString() == "True")
                {
                    Session.Remove("FBrandIDforUpdate");
                    Session.Remove("FBrandDescforUpdate");
                    Session.Remove("BrandFilter");

                }
            }
            catch
            {
               Lepat = "Failed";
            }
            // Site Master
            try
            {
                if (Session["SiteFilter"].ToString() == "True")
                {
                    Session.Remove("FSiteName");
                    Session.Remove("FSite");
                    Session.Remove("SiteFilter");
                }
            }
            catch
            {
               Lepat = "Failed";
            }
            // Size Master
            try
            {
                if (Session["SizeFilter"].ToString() == "True")
                {
                    Session.Remove("FSizeIDforUpdate");
                    Session.Remove("FSizeDescforUpdate");
                    Session.Remove("SizeFilter");
                }
            }
            catch
            {
               Lepat = "Failed";
            }

            // Price Master
            try
            {
                if (Session["PriceFilter"].ToString() == "True")
                {
                    Session.Remove("FPItemId");
                    Session.Remove("FPVariant");
                    Session.Remove("PriceFilter");
                    Session.Remove("FPSite");
                    Session.Remove("FPPrice");
                    Session.Remove("FPVAT");
                    Session.Remove("FPriceEDate");
                    Session.Remove("FPriceSDate");
                    
                }
            }
            catch
            {
               Lepat = "Failed";
            }
            // Style Master
            try
            {
                if (Session["StyleFilter"].ToString() == "True")
                {
                    Session.Remove("StyleID");
                    Session.Remove("StyleDesc");
                    Session.Remove("StyleFilter");

                }
            }
            catch
            {
               Lepat = "Failed";
            }

            // SKU Master
            try
            {
                if (Session["SKUFilter"].ToString() == "True")
                {
                    Session.Remove("SKUID");
                    Session.Remove("SKUEXID");
                    Session.Remove("SKUSDesc");
                    Session.Remove("SKULDesc");
                    Session.Remove("SKUEDate");
                    Session.Remove("SKUSDate");
                    Session.Remove("SKUFilter");                    
                }
            }
            catch
            {
               Lepat = "Failed";
            }
            // Color Master
            try
            {
                if (Session["ColorFilter"].ToString() == "True")
                {
                    Session.Remove("ColorFilter");
                    Session.Remove("ColorID");
                    Session.Remove("ColorColor");
              
                }
            }
            catch
            {
               Lepat = "Failed";
            }
        }
    }
}