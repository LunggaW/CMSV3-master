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

namespace KBS.KBS.CMSV3.InventoryManagement.StockDisplay
{
    public partial class StockDisplayHeader : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTStock = new DataTable();
        private DataTable DTTransactionType = new DataTable();
        private DataTable DTSite = new DataTable();
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
            if(IsPostBack)
            {
                RefreshDataGrid();
            }
            
            


            DTSite = CMSfunction.GetSiteBySiteProfile(Session["SiteProfile"].ToString());
            ComboSite.DataSource = DTSite;
            ComboSite.ValueField = "SITESITE";
            ComboSite.ValueType = typeof(string);
            ComboSite.TextField = "SITESITENAME";
            ComboSite.DataBind();

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

        private void loadButton(String MenuID)
        {

            List<AccessContainer> listAccessCont =
                CMSfunction.SelectAccessByProfileAndMenuID(Session["AccessProfile"].ToString(), MenuID);

            foreach (var accessContainer in listAccessCont)
            {
                switch (accessContainer.FunctionId)
                {
                    case "1":
                       // AddBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        break;
                    case "2":
                        //Ed.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        if (accessContainer.Type == "0")
                        {
                            ASPxGridViewStockDisplay.ClientSideEvents.RowDblClick = null;
                        }

                        break;
                    case "3":
                        SearchBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        break;
                    case "4":
                      //  DelBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
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
            TextBoxItemId.Text = "";
            ComboSite.Text = "";
            TextBoxBarcode.Text = "";
            TextBoxDesc.Text = "";

            DTSite = CMSfunction.GetSiteBySiteProfile(Session["SiteProfile"].ToString());
            ComboSite.DataSource = DTSite;
            ComboSite.ValueField = "SITESITE";
            ComboSite.ValueType = typeof(string);
            ComboSite.TextField = "SITESITENAME";
            ComboSite.DataBind();
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
            if (ASPxGridViewStockDisplay.FocusedRowIndex != -1)
            {
                String ItemIDExternal = ASPxGridViewStockDisplay.GetRowValues(ASPxGridViewStockDisplay.FocusedRowIndex, "ITEM ID").ToString();

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
            Stock StockDisplay = new Stock();


            



            StockDisplay.ItemID = !string.IsNullOrWhiteSpace(TextBoxItemId.Text) ? TextBoxItemId.Text : "";
            StockDisplay.Barcode= !string.IsNullOrWhiteSpace(TextBoxBarcode.Text) ? TextBoxBarcode.Text : "";
            StockDisplay.Site = (ComboSite.Value != null) ? ComboSite.Value.ToString() : "";
            StockDisplay.Desc = !string.IsNullOrWhiteSpace(TextBoxDesc.Text) ? TextBoxDesc.Text : "";
            


            DTStock = CMSfunction.GetAllTransactionTableFiltered(StockDisplay, Session["SiteProfile"].ToString());
            ASPxGridViewStockDisplay.DataSource = DTStock;
            ASPxGridViewStockDisplay.KeyFieldName = "BARCODE";
            ASPxGridViewStockDisplay.DataBind();
            
        }

        #region Grid Navigation Button
        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewStockDisplay.PageIndex <= ASPxGridViewStockDisplay.PageCount - 1)
            {
                ASPxGridViewStockDisplay.PageIndex = ASPxGridViewStockDisplay.PageIndex + 1;
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewStockDisplay.PageIndex - 1 >= 0)
            {
                ASPxGridViewStockDisplay.PageIndex = ASPxGridViewStockDisplay.PageIndex - 1;
            }
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewStockDisplay.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewStockDisplay.PageIndex = ASPxGridViewStockDisplay.PageCount - 1;
        }
        #endregion


        protected void ASPxButtonEntry_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewStockDisplay.FocusedRowIndex != -1)
            {
                Session["ItemIDExManagementVariant"] = ASPxGridViewStockDisplay.GetRowValues(ASPxGridViewStockDisplay.FocusedRowIndex, "ITEM ID").ToString();
                Response.Redirect("VariantManagement/VariantManagementHeader.aspx");
            }
        }

        protected void ASPxGridViewStockDisplay_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            Session["ItemIDStockDisplay"] = ASPxGridViewStockDisplay.GetRowValues(ASPxGridViewStockDisplay.FocusedRowIndex, "ITEM ID").ToString();

            Session["SiteStockDisplay"] = ASPxGridViewStockDisplay.GetRowValues(ASPxGridViewStockDisplay.FocusedRowIndex, "SITE").ToString();

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("StockMovement.aspx");
            else
                Response.Redirect("StockMovement.aspx");
        }
    }
}