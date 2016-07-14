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

namespace KBS.KBS.CMSV3.MasterData.ItemManagement.VariantManagement.BarcodeManagement
{
    public partial class BarcodeManagementHeader : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTStatus = new DataTable();
        private DataTable DTType = new DataTable();
        private DataTable DTBarcode = new DataTable();
        private String ItemID;
        private String VariantID;

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/logins.aspx");
            }
            else if (Session["ItemIDExManagementVariant"] == null)
            {
                Response.Redirect("../ItemManagementHeader.aspx");
            }
            else if (Session["VariantIDExBarcode"] == null)
            {
                Response.Redirect("../VariantManagementHeader.aspx");
            }
            else
            {
                loadNavBar();
            }
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ItemID = CMSfunction.GetItemIDByItemIDEx(Session["ItemIDExManagementVariant"].ToString());
            TextBoxItem.Text = Session["ItemIDExManagementVariant"].ToString() +" - "+CMSfunction.GetItemDescByItemID(ItemID);

            VariantID = CMSfunction.GetVarIDByVarIDExandItemID(Session["VariantIDExBarcode"].ToString(), ItemID);
            TextBoxVariantID.Text = Session["VariantIDExBarcode"].ToString() + " - " + CMSfunction.GetVariantDescByVariantIDandItemID(VariantID, ItemID); ;

            DTStatus = CMSfunction.GetParameterValueAndDescbyClassAndTabID(Session["Class"].ToString(), "6");
            ComboStatus.DataSource = DTStatus;
            ComboStatus.ValueField = "PARVALUE";
            ComboStatus.ValueType = typeof(string);
            ComboStatus.TextField = "PARDESCRIPTION";
            ComboStatus.DataBind();

            DTType = CMSfunction.GetParameterValueAndDescbyClassAndTabID(Session["Class"].ToString(), "16");
            ComboType.DataSource = DTType;
            ComboType.ValueField = "PARVALUE";
            ComboType.ValueType = typeof(string);
            ComboType.TextField = "PARDESCRIPTION";
            ComboType.DataBind();

            RefreshDataGrid();
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
            RefreshDataGrid();
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
            Session["Barcode"] = ASPxGridViewBarcode.GetRowValues(Convert.ToInt32(e.Parameters), "BARCODE").ToString();

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("BarcodeManagementHeaderDetail.aspx");
            else
                Response.Redirect("BarcodeManagementHeaderDetail.aspx");
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("../VariantManagementHeader.aspx");
            else
                Response.Redirect("../VariantManagementHeader.aspx");
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("BarcodeManagementHeaderNew.aspx");
            else
                Response.Redirect("BarcodeManagementHeaderNew.aspx");
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewBarcode.FocusedRowIndex != -1)
            {
                BarcodeMaster barcode = new BarcodeMaster();

                OutputMessage message = new OutputMessage();

                barcode.ItemID = ItemID;
                barcode.VariantID = VariantID;
                barcode.Barcode = ASPxGridViewBarcode.GetRowValues(ASPxGridViewBarcode.FocusedRowIndex, "BARCODE").ToString();

                message = CMSfunction.DeleteBarcode(barcode);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;

                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            BarcodeMaster barcode = new BarcodeMaster();

            barcode.ItemID = CMSfunction.GetItemIDByItemIDEx(Session["ItemIDExManagementVariant"].ToString());
            barcode.VariantID = CMSfunction.GetVarIDByVarIDExandItemID(Session["VariantIDExBarcode"].ToString(), barcode.ItemID);

            barcode.Barcode = !string.IsNullOrWhiteSpace(TextBoxBarcode.Text) ? TextBoxBarcode.Text : "";

            barcode.Status = (ComboStatus.Value != null) ? ComboStatus.Value.ToString() : "";
            barcode.Type = (ComboType.Value != null) ? ComboType.Value.ToString() : "";

            barcode.StartDate = DateStart.Date != DateTime.MinValue ? (DateTime?)DateStart.Date : null;
            barcode.EndDate = DateEnd.Date != DateTime.MinValue ? (DateTime?)DateEnd.Date : null;

            DTBarcode = CMSfunction.GetAllBarcodeFiltered(barcode, Session["Class"].ToString());
            ASPxGridViewBarcode.DataSource = DTBarcode;
            ASPxGridViewBarcode.KeyFieldName = "BARCODE";
            ASPxGridViewBarcode.DataBind();
        }

        #region Grid Navigation Button
        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewBarcode.PageIndex <= ASPxGridViewBarcode.PageCount - 1)
            {
                ASPxGridViewBarcode.PageIndex = ASPxGridViewBarcode.PageIndex + 1;
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewBarcode.PageIndex - 1 >= 0)
            {
                ASPxGridViewBarcode.PageIndex = ASPxGridViewBarcode.PageIndex - 1;
            }
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewBarcode.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewBarcode.PageIndex = ASPxGridViewBarcode.PageCount - 1;
        }
        #endregion


    }
}