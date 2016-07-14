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
    public partial class BarcodeManagementHeaderDetail : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTStatus = new DataTable();
        private DataTable DTType = new DataTable();
        private DataTable DTBarcode = new DataTable();
        private OutputMessage message = new OutputMessage();
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
            else if (Session["Barcode"] == null)
            {
                Response.Redirect("BarcodeManagementHeader.aspx");
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

            if (!IsPostBack)
            {
                BarcodeMaster barcode = new BarcodeMaster();

                TextBoxBarcode.Text = Session["Barcode"].ToString();

                barcode.Barcode = TextBoxBarcode.Text;

                barcode.ItemID = ItemID;
                barcode.VariantID = VariantID;

                barcode = CMSfunction.GetBarcode(barcode);

                ComboStatus.SelectedItem = ComboStatus.Items.FindByValue(barcode.Status);
                ComboType.SelectedItem = ComboType.Items.FindByValue( barcode.Type);

                DateStart.Value = barcode.StartDate.HasValue ? (object)barcode.StartDate : "";
                DateEnd.Value = barcode.EndDate.HasValue ? (object)barcode.EndDate : "";

                

            }
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
            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("BarcodeManagementHeader.aspx");
            else
                Response.Redirect("BarcodeManagementHeader.aspx");
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
        }

        private void ProcessUpdate()
        {
            BarcodeMaster barcode = new BarcodeMaster();

            barcode.ItemID = ItemID;
            barcode.VariantID = VariantID;

            barcode.Barcode = TextBoxBarcode.Text;

            barcode.Status = (ComboStatus.Value != null) ? ComboStatus.Value.ToString() : "";
            barcode.Type = (ComboType.Value != null) ? ComboType.Value.ToString() : "";

            barcode.StartDate = DateStart.Date;
            barcode.EndDate = DateEnd.Date;

            message = CMSfunction.UpdateBarcode(barcode, Session["UserID"].ToString());
        }

        

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            ProcessUpdate();
            LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            LabelMessage.Visible = true;
            LabelMessage.Text = message.Message;
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {

            ProcessUpdate();
            Response.Redirect("BarcodeManagementHeader.aspx");
        }


    }
}