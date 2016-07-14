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

namespace KBS.KBS.CMSV3.MasterData
{
    public partial class ItemManagement : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTBrand = new DataTable();
        private DataTable DTSite = new DataTable();
        private DataTable DTVariant = new DataTable();
        private DataTable DTGridViewItem = new DataTable();
        private User user;
        private ItemMaster itemMaster;

        protected override void OnInit(EventArgs e)
        {
           // user = CMSfunction.SelectUserDataFromUserID(User.Identity.Name);
            itemMaster = new ItemMaster();
            loadNavBar();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            //user = CMSfunction.SelectUserDataFromUserID(User.Identity.Name);

            ////DTSite = CMSfunction.GetSiteDataByProfileID(user.ProfileID);
            ////ASPxComboBoxUserManagementSite.DataSource = DTSite;
            ////ASPxComboBoxUserManagementSite.ValueField = "SITECODE";
            ////ASPxComboBoxUserManagementSite.ValueType = typeof(string);
            ////ASPxComboBoxUserManagementSite.TextField = "SITENAME";
            ////ASPxComboBoxUserManagementSite.DataBind();

            //DTBrand = CMSfunction.GetAllBrandByProfileID(user.MenuProfile);
            //ASPxComboBoxBrand.DataSource = DTBrand;
            //ASPxComboBoxBrand.ValueField = "BRANDCODE";
            //ASPxComboBoxBrand.ValueType = typeof(string);
            //ASPxComboBoxBrand.TextField = "BRANDNAME";
            //ASPxComboBoxBrand.DataBind();

            //DTVariant = CMSfunction.GetAllVariant(user.MenuProfile);
            //ASPxComboBoxVariant.DataSource = DTVariant;
            //ASPxComboBoxVariant.ValueField = "VARIANTCODE";
            //ASPxComboBoxVariant.ValueType = typeof(string);
            //ASPxComboBoxVariant.TextField = "VARIANTNAME";
            //ASPxComboBoxVariant.DataBind();

            //DTGridViewItem = CMSfunction.GetItemMaster();
            //ASPxGridViewItem.DataSource = DTGridViewItem;
            //ASPxGridViewItem.KeyFieldName = "ID";
            //ASPxGridViewItem.DataBind();
            ////loadNavBar();

            //RefreshDataGrid();
            ////RefreshInputField();

        }

        protected void ASPxButtonUserManagementInsert_Click(object sender, EventArgs e)
        {

            //ASPxTextBoxUserManagementUserID.Text = String.Empty;
            //    ASPxTextBoxUserManagementUserName.Text = String.Empty;
            //    ASPxTextBoxUserManagementPassword.Text = String.Empty;

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

        protected void ASPxButtonCommit_Click(object sender, EventArgs e)
        {
            itemMaster = new ItemMaster();
            itemMaster.Barcode = ASpXTextBoxBarcode.Text;
            itemMaster.ItemID = ASPxTextBoxID.Text;
            itemMaster.Name = ASPxTextBoxName.Text;
            itemMaster.Size = ASpXTextBoxSize.Text;
            itemMaster.Brand = ASPxComboBoxBrand.SelectedItem.Value.ToString();
            itemMaster.Status = ASPxComboBoxStatus.SelectedItem.Value.ToString();
            itemMaster.Variant = ASPxComboBoxVariant.SelectedItem.Value.ToString();

            CMSfunction.insertItemMasterByProfileID(itemMaster, user.MenuProfile, user.Username);

            RefreshDataGrid();
            RefreshInputField();
        }


        protected void RefreshDataGrid()
        {
            DTGridViewItem = CMSfunction.GetItemMaster();
            ASPxGridViewItem.DataSource = DTGridViewItem;
            ASPxGridViewItem.KeyFieldName = "ID";
            ASPxGridViewItem.DataBind();
        }

        protected void RefreshInputField()
        {
            ASpXTextBoxBarcode.Text = "";
            ASPxComboBoxBrand.SelectedIndex = -1;
            ASPxTextBoxID.Text = "";
            ASPxTextBoxName.Text = "";
            ASpXTextBoxSize.Text = "";
            ASPxComboBoxStatus.SelectedIndex = -1;
            ASPxComboBoxVariant.SelectedIndex = -1;
        }

        protected void ASPxButtonUpdate_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewItem.FocusedRowIndex != -1)
            {
                itemMaster.ID = ASPxGridViewItem.GetRowValues(ASPxGridViewItem.FocusedRowIndex, "ID").ToString();
            }

            itemMaster.Barcode = ASpXTextBoxBarcode.Text;
            itemMaster.ItemID = ASPxTextBoxID.Text;
            itemMaster.Name = ASPxTextBoxName.Text;
            itemMaster.Size = ASpXTextBoxSize.Text;
            itemMaster.Brand = ASPxComboBoxBrand.SelectedItem.Value.ToString();
            itemMaster.Status = ASPxComboBoxStatus.SelectedItem.Value.ToString();
            itemMaster.Variant = ASPxComboBoxVariant.SelectedItem.Value.ToString();

            CMSfunction.UpdateItemMaster(itemMaster, user.Username);

            RefreshInputField();
            RefreshDataGrid();
        }

        protected void ASPxButtonNew_Click(object sender, EventArgs e)
        {
            RefreshInputField();
        }

        protected void ASPxGridViewItem_FocusedRowChanged(object sender, EventArgs e)
        {
            if (ASPxGridViewItem.FocusedRowIndex != -1)
            {
                itemMaster = new ItemMaster();
                ASpXTextBoxBarcode.Text = ASPxGridViewItem.GetRowValues(ASPxGridViewItem.FocusedRowIndex, "BARCODE").ToString();
                ASpXTextBoxSize.Text = ASPxGridViewItem.GetRowValues(ASPxGridViewItem.FocusedRowIndex, "SIZE").ToString();
                ASPxTextBoxID.Text = ASPxGridViewItem.GetRowValues(ASPxGridViewItem.FocusedRowIndex, "ITEMID").ToString();
                ASPxTextBoxName.Text = ASPxGridViewItem.GetRowValues(ASPxGridViewItem.FocusedRowIndex, "NAME").ToString();
                itemMaster.ID = ASPxGridViewItem.GetRowValues(ASPxGridViewItem.FocusedRowIndex, "ID").ToString();

                ASPxComboBoxBrand.SelectedItem =
                    ASPxComboBoxBrand.Items.FindByValue(
                        ASPxGridViewItem.GetRowValues(ASPxGridViewItem.FocusedRowIndex, "BRAND").ToString());

                ASPxComboBoxStatus.SelectedItem =
                    ASPxComboBoxStatus.Items.FindByText(
                        ASPxGridViewItem.GetRowValues(ASPxGridViewItem.FocusedRowIndex, "STATUS").ToString());

                ASPxComboBoxVariant.SelectedItem =
                    ASPxComboBoxVariant.Items.FindByValue(
                        ASPxGridViewItem.GetRowValues(ASPxGridViewItem.FocusedRowIndex, "VARIANT").ToString());

                
            }
        }

        protected void ASPxButtonDelete_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewItem.FocusedRowIndex != -1)
            {
                itemMaster.ID = ASPxGridViewItem.GetRowValues(ASPxGridViewItem.FocusedRowIndex, "ID").ToString();
                CMSfunction.DeleteItemMasterByID(itemMaster.ID);
            }
            RefreshInputField();
            RefreshDataGrid();

        }
    }
}