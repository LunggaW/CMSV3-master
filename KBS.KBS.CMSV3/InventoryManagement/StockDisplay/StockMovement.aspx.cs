using System;
using System.Collections.Generic;
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
    public partial class StockMovement : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        OutputMessage message = new OutputMessage();
        private DataTable DTTransType = new DataTable();
        private DataTable DTSite = new DataTable();
        private DataTable DTStock = new DataTable();

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

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DTTransType = CMSfunction.GetParameterValueAndDescbyClassAndTabID(Session["Class"].ToString(), "18");
            ComboTransType.DataSource = DTTransType;
            ComboTransType.ValueField = "PARVALUE";
            ComboTransType.ValueType = typeof(string);
            ComboTransType.TextField = "PARDESCRIPTION";
            ComboTransType.DataBind();


            DTSite = CMSfunction.GetSiteBySiteProfile(Session["SiteProfile"].ToString());
            ComboSite.DataSource = DTSite;
            ComboSite.ValueField = "SITESITE";
            ComboSite.ValueType = typeof(string);
            ComboSite.TextField = "SITESITENAME";
            ComboSite.DataBind();
            /*
            if (Session["ItemIDStockDisplay"] != null)
            {
                TextBoxItemId.Text = Session["ItemIDStockDisplay"].ToString();
                ComboSite.SelectedIndex = ComboSite.Items.FindByValue(Session["SiteStockDisplay"].ToString()).Index;
            }
            */
            if (!IsPostBack)
            {
                RefreshDataGrid();
                //ItemMaster item = new ItemMaster();

                //item.ItemIDExternal = Session["ItemIDStockDisplay"].ToString();

                //item.ItemID = CMSfunction.GetItemIDByItemIDEx(item.ItemIDExternal);

                //item = CMSfunction.GetItemMaster(item);

                //TextBoxItemIdExternal.Text = item.ItemIDExternal;
                //TextBoxLongDescription.Text = item.LongDesc;
                //TextBoxShortDescription.Text = item.ShortDesc;
                //ComboBrand.SelectedItem = ComboBrand.Items.FindByValue(item.Brand);
                //ComboItemType.SelectedItem = ComboItemType.Items.FindByValue(item.Type);
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


        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
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
            TextBoxItemId.Text = "";
            ComboTransType.Text = "";
            TextBoxBarcode.Text = "";
            TextBoxNota.Text = "";
            TextBoxTransID.Text = "";
            ComboSite.Text = "";
            DTTransType = CMSfunction.GetParameterValueAndDescbyClassAndTabID(Session["Class"].ToString(), "18");
            ComboTransType.DataSource = DTTransType;
            ComboTransType.ValueField = "PARVALUE";
            ComboTransType.ValueType = typeof(string);
            ComboTransType.TextField = "PARDESCRIPTION";
            ComboTransType.DataBind();


            DTSite = CMSfunction.GetSiteBySiteProfile(Session["SiteProfile"].ToString());
            ComboSite.DataSource = DTSite;
            ComboSite.ValueField = "SITESITE";
            ComboSite.ValueType = typeof(string);
            ComboSite.TextField = "SITESITENAME";
            ComboSite.DataBind();
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            Session.Remove("ItemIDExManagement");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("ItemManagementHeader.aspx");
            else

                Response.Redirect("ItemManagementHeader.aspx");
            //Session.Remove("ParamHeaderID");
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            ProcessUpdate();


            LabelMessage.Visible = true;
            LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            LabelMessage.Text = message.Message;
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            //Response.Redirect("ParameterManagementDetailNew.aspx");
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            ProcessUpdate();
            Response.Redirect("ItemManagementHeader.aspx");
        }


        private void ProcessUpdate()
        {
            //ItemMaster item = new ItemMaster();

            //item.ItemIDExternal = !string.IsNullOrWhiteSpace(TextBoxItemIdExternal.Text) ? TextBoxItemIdExternal.Text : "";
            //item.ShortDesc = !string.IsNullOrWhiteSpace(TextBoxShortDescription.Text) ? TextBoxShortDescription.Text : "";
            //item.LongDesc = !string.IsNullOrWhiteSpace(TextBoxLongDescription.Text) ? TextBoxLongDescription.Text : "";
            //item.Brand = !string.IsNullOrWhiteSpace(TextBoxLongDescription.Text) ? TextBoxLongDescription.Text : "";
            //item.Type = (ComboItemType.Value != null) ? ComboItemType.Value.ToString() : "";
            //item.Brand = (ComboBrand.Value != null) ? ComboBrand.Value.ToString() : "";



            //message = CMSfunction.updateItemMaster(item, Session["UserID"].ToString());
        }

        private void RefreshDataGrid()
        {
            Stock StockDisplay = new Stock();






            StockDisplay.ItemID = !string.IsNullOrWhiteSpace(TextBoxItemId.Text) ? TextBoxItemId.Text : "";
            StockDisplay.Barcode = !string.IsNullOrWhiteSpace(TextBoxBarcode.Text) ? TextBoxBarcode.Text : "";
            StockDisplay.TransactionID = !string.IsNullOrWhiteSpace(TextBoxTransID.Text) ? TextBoxTransID.Text : "";
            StockDisplay.Site = (ComboSite.Value != null) ? ComboSite.Value.ToString() : "";
            StockDisplay.TransactionType = (ComboTransType.Value != null) ? ComboTransType.Value.ToString() : "";
            StockDisplay.Nota = !string.IsNullOrWhiteSpace(TextBoxNota.Text) ? TextBoxNota.Text : "";


            DTStock = CMSfunction.GetAllTransactionTableMovementFiltered(StockDisplay, Session["SiteProfile"].ToString());
            ASPxGridViewStockMovement.DataSource = DTStock;
            ASPxGridViewStockMovement.KeyFieldName = "BARCODE";
            ASPxGridViewStockMovement.DataBind();

        }
        
    }
}