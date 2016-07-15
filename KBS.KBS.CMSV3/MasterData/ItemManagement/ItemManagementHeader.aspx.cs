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

namespace KBS.KBS.CMSV3.MasterData.ItemManagement
{
    public partial class ItemManagementHeader : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTItem = new DataTable();
        private DataTable DTItemType = new DataTable();
        private DataTable DTBrand = new DataTable();
        private String MenuID = ConfigurationManager.AppSettings["MenuIdItemMasterManagement"];

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
            RefreshDataGrid();

            DTItemType = CMSfunction.GetParameterValueAndDescbyClassAndTabID(Session["Class"].ToString(), "13");
            ComboItemType.DataSource = DTItemType;
            ComboItemType.ValueField = "PARVALUE";
            ComboItemType.ValueType = typeof(string);
            ComboItemType.TextField = "PARDESCRIPTION";
            ComboItemType.DataBind();


            DTBrand = CMSfunction.GetAllBrand();
            ComboBrand.DataSource = DTBrand;
            ComboBrand.ValueField = "BRAND";
            ComboBrand.ValueType = typeof(string);
            ComboBrand.TextField = "DESCRIPTION";
            ComboBrand.DataBind();
            
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
            string a = Session["MenuProfile"].ToString();
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
                            ASPxGridViewItem.ClientSideEvents.RowDblClick = null;
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

        protected void ClearBtn_Click(object sender, EventArgs e)
        {

            TextBoxItemIdExternal.Text = "";
            TextBoxShortDescription.Text = "";
            TextBoxLongDescription.Text = "";
            ComboItemType.Dispose();
            DTItemType = CMSfunction.GetParameterValueAndDescbyClassAndTabID(Session["Class"].ToString(), "13");
            ComboItemType.DataSource = DTItemType;
            ComboItemType.ValueField = "PARVALUE";
            ComboItemType.ValueType = typeof(string);
            ComboItemType.TextField = "PARDESCRIPTION";
            ComboItemType.DataBind();

            ComboBrand.Dispose();
            DTBrand = CMSfunction.GetAllBrand();
            ComboBrand.DataSource = DTBrand;
            ComboBrand.ValueField = "BRAND";
            ComboBrand.ValueType = typeof(string);
            ComboBrand.TextField = "DESCRIPTION";
            ComboBrand.DataBind();

            RefreshDataGrid();
        }

        protected void ASPxGridViewDetail_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            Session["ItemIDExManagement"] = ASPxGridViewItem.GetRowValues(Convert.ToInt32(e.Parameters), "ITEM ID").ToString();

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("ItemManagementHeaderDetail.aspx");
            else
                Response.Redirect("ItemManagementHeaderDetail.aspx");
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
                    ASPxWebControl.RedirectOnCallback("ItemManagementHeaderNew.aspx");
                else
                    Response.Redirect("ItemManagementHeaderNew.aspx");

            
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewItem.FocusedRowIndex != -1)
            {
                String ItemIDExternal = ASPxGridViewItem.GetRowValues(ASPxGridViewItem.FocusedRowIndex, "ITEM ID").ToString();

                OutputMessage message = new OutputMessage();

                message = CMSfunction.DeleteItem(ItemIDExternal);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;

                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            ItemMaster item = new ItemMaster();

            item.ItemIDExternal = !string.IsNullOrWhiteSpace(TextBoxItemIdExternal.Text) ? TextBoxItemIdExternal.Text : "";
            item.ShortDesc = !string.IsNullOrWhiteSpace(TextBoxShortDescription.Text) ? TextBoxShortDescription.Text : "";
            item.LongDesc = !string.IsNullOrWhiteSpace(TextBoxLongDescription.Text) ? TextBoxLongDescription.Text : "";
            item.Brand = !string.IsNullOrWhiteSpace(TextBoxLongDescription.Text) ? TextBoxLongDescription.Text : "";
            item.Type = (ComboItemType.Value != null) ? ComboItemType.Value.ToString() : "";
            item.Brand = (ComboBrand.Value != null) ? ComboBrand.Value.ToString() : "";


            DTItem = CMSfunction.GetAllItemFiltered(item, Session["Class"].ToString());
            ASPxGridViewItem.DataSource = DTItem;
            ASPxGridViewItem.KeyFieldName = "ITEM ID";
            ASPxGridViewItem.DataBind();
        }

        #region Grid Navigation Button
        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewItem.PageIndex <= ASPxGridViewItem.PageCount - 1)
            {
                ASPxGridViewItem.PageIndex = ASPxGridViewItem.PageIndex + 1;
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewItem.PageIndex - 1 >= 0)
            {
                ASPxGridViewItem.PageIndex = ASPxGridViewItem.PageIndex - 1;
            }
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewItem.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewItem.PageIndex = ASPxGridViewItem.PageCount - 1;
        }
        #endregion


        protected void ASPxButtonEntry_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewItem.FocusedRowIndex != -1)
            {
                Session["ItemIDExManagementVariant"] = ASPxGridViewItem.GetRowValues(ASPxGridViewItem.FocusedRowIndex, "ITEM ID").ToString();
                Response.Redirect("VariantManagement/VariantManagementHeader.aspx");
            }
        }

    }
}