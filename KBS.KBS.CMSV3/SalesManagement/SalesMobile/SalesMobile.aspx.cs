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


namespace KBS.KBS.CMSV3.SalesManagement.SalesMobile
{
    public partial class SalesMobile : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTDetailInput = new DataTable();
        OutputMessage message = new OutputMessage();

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

            if (!IsPostBack)
            {
                DTDetailInput = new DataTable();
                DTDetailInput = CMSfunction.GetItemByAssortment(Session["DefaultSite"].ToString());
                ITEMBOX.DataSource = DTDetailInput;
                ITEMBOX.ValueField = "VALUE";
                ITEMBOX.ValueType = typeof(string);
                ITEMBOX.TextField = "DESCRIPTION";
                ITEMBOX.DataBind();
               
               
            }
            
        }

        protected void Search(object sender, EventArgs e)
        {
            Session["SearchRedirect"] = "SalesDetailInputNew.aspx";
            Response.Redirect("SearchItemMaster.aspx");
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



        protected void ClearBtn_Click(object sender, EventArgs e)
        {

            ITEMTXT.Text = "";
            VID.Text = "";
            BARCODETXT.Text = "";
            QTYTXT.Text = "";
            ITEMBOX.SelectedIndex = -1;
            DTDetailInput = new DataTable();
            DTDetailInput = CMSfunction.GetVariantByAssortment(ITEMBOX.Value.ToString(), Session["DefaultSite"].ToString());
            VARIANTBOX.DataSource = DTDetailInput;
            VARIANTBOX.ValueField = "VALUE";
            VARIANTBOX.ValueType = typeof(string);
            VARIANTBOX.TextField = "DESCRIPTION";
            VARIANTBOX.DataBind();


        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            //Session.Remove("ParamHeaderIDforUpdate");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("SalesDetailInput.aspx");
            else

                Response.Redirect("SalesDetailInput.aspx");
            //Session.Remove("ParamHeaderID");
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            ProcessInsert();

            ASPxLabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            ASPxLabelMessage.Visible = true;
            ASPxLabelMessage.Text = message.Message;
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
                        
            ProcessInsert();
            Response.Redirect("SalesDetailInput.aspx");                       
        }

        private void ProcessInsert()
        {
            SalesHeader salesheader = new SalesHeader();
            salesheader.COMMENT = COMMENTXT.Text;
            salesheader.DATE = DateTime.Parse(TDATE.Value.ToString());
            salesheader.NOTA = NOTATXT.Text;
            salesheader.STATUS = 0; //Created
            salesheader.FLAG = 1; //Sales
            salesheader.SITE = Session["DefaultSite"].ToString();
            salesheader.RECEIPTID = "abc";
            salesheader.SALESID = "def";
            salesheader.IID = "98798";

            message = CMSfunction.InsertSalesInputHeader(salesheader, Session["UserID"].ToString());

            SalesInputDetail salesinputdetail = new SalesInputDetail();
            salesinputdetail.SALESID = Session["INPUTSALESID"].ToString();
            salesinputdetail.IID = Session["INPUTIID"].ToString();
            salesinputdetail.NOTA = NOTATXT.Text;
            salesinputdetail.RECEIPTID = Session["INPUTRECEIPTID"].ToString();
            salesinputdetail.DATE = DateTime.Parse(TDATE.Value.ToString());
            salesinputdetail.SITE = Session["DefaultSite"].ToString();
            salesinputdetail.ITEMID = ITEMBOX.Value.ToString();
            salesinputdetail.VARIANTID = VARIANTBOX.Value.ToString();
            salesinputdetail.BARCODE = BARCODETXT.Text;
            salesinputdetail.SALESQTY= QTYTXT.Text;
            salesinputdetail.SALESPRICE = PRICETXT.Text;
            salesinputdetail.COMMENT = COMMENTXT.Text;
            salesinputdetail.SKUID = SKUBOX.Value.ToString();

            ITEMTXT.Text = "";
            VID.Text = "";
            BARCODETXT.Text = "";
            QTYTXT.Text = "";
            PRICETXT.Text = "0";
            SKUBOX.Text = "";
            SKUBOX.Value = 0;

            Session["SearchVariantforUpdate"] = "";                
            Session["SearchItemIDforUpdate"] = "";                
            Session["SearchBarcodeforUpdate"] = "";
            message = CMSfunction.InsertSalesInputDetail(salesinputdetail, Session["UserID"].ToString());

        }

        protected void ITEMBOX_SelectedIndexChanged(object sender, EventArgs e)
        {
            DTDetailInput = new DataTable();
            DTDetailInput = CMSfunction.GetVariantByAssortment(ITEMBOX.Value.ToString(), Session["DefaultSite"].ToString());
            VARIANTBOX.DataSource = DTDetailInput;
            VARIANTBOX.ValueField = "VALUE";
            VARIANTBOX.ValueType = typeof(string);
            VARIANTBOX.TextField = "DESCRIPTION";
            VARIANTBOX.DataBind();
            DTDetailInput = new DataTable();
            DTDetailInput = CMSfunction.GetSKULinkBox(Session["DefaultSite"].ToString(), ITEMTXT.Text);
            SKUBOX.DataSource = DTDetailInput;
            SKUBOX.ValueField = "VALUE";
            SKUBOX.ValueType = typeof(string);
            SKUBOX.TextField = "DESCRIPTION";
            SKUBOX.DataBind();


        }
        protected void BarcodeCek(object sender, EventArgs e)
        {
            TransferOrderDetail transferorderdetail = new TransferOrderDetail();
            transferorderdetail.BARCODE = BARCODETXT.Text;
            transferorderdetail = CMSfunction.GetBarcodeTransferDetail2(transferorderdetail, Session["DefaultSite"].ToString());
            if (transferorderdetail.BARCODE != "Not Found")
            {
                ITEMTXT.Text = transferorderdetail.ITEMID;
                VID.Text = transferorderdetail.VARIANT;
                BARCODETXT.Text = transferorderdetail.BARCODE;
                ITEMBOX.Value = transferorderdetail.ITEMID;

                DTDetailInput = new DataTable();
                DTDetailInput = CMSfunction.GetVariantByAssortment(ITEMBOX.Value.ToString(), Session["DefaultSite"].ToString());
                VARIANTBOX.DataSource = DTDetailInput;
                VARIANTBOX.ValueField = "VALUE";
                VARIANTBOX.ValueType = typeof(string);
                VARIANTBOX.TextField = "DESCRIPTION";
                VARIANTBOX.DataBind();
                VARIANTBOX.Value = transferorderdetail.VARIANT;
                transferorderdetail = new TransferOrderDetail();
                transferorderdetail.VARIANT = VARIANTBOX.Value.ToString();
                transferorderdetail.ITEMID = ITEMBOX.Value.ToString();
                transferorderdetail = CMSfunction.GetPriceDetail(transferorderdetail, Session["DefaultSite"].ToString());
                PRICETXT.Text = transferorderdetail.PRICE;

                DTDetailInput = new DataTable();
                DTDetailInput = CMSfunction.GetSKULinkBox(Session["DefaultSite"].ToString(), ITEMTXT.Text);
                SKUBOX.DataSource = DTDetailInput;
                SKUBOX.ValueField = "VALUE";
                SKUBOX.ValueType = typeof(string);
                SKUBOX.TextField = "DESCRIPTION";
                SKUBOX.DataBind();
            }
            else
            {
                string script = "alert('Item Dengan Barcode Tidak Tersedia , Mohon Hubungi Admin');";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
                BARCODETXT.Text = transferorderdetail.BARCODE;

                ITEMTXT.Text = "";
                VID.Text = "";
                
                ITEMBOX.SelectedIndex = -1;
                DTDetailInput = new DataTable();
                
                DTDetailInput = CMSfunction.GetVariantByAssortment("0", Session["DefaultSite"].ToString());
                VARIANTBOX.DataSource = DTDetailInput;
                VARIANTBOX.ValueField = "VALUE";
                VARIANTBOX.ValueType = typeof(string);
                VARIANTBOX.TextField = "DESCRIPTION";
                VARIANTBOX.DataBind();
               
            }
        }
        protected void Variant_SelectedIndexChanged(object sender, EventArgs e)
        {
            TransferOrderDetail transferorderdetail = new TransferOrderDetail();
            transferorderdetail.ITEMID = ITEMBOX.Value.ToString();
            transferorderdetail.VARIANT = VARIANTBOX.Value.ToString();
            transferorderdetail = CMSfunction.GetBarcodeByItemVariant(transferorderdetail);            
            BARCODETXT.Text = transferorderdetail.BARCODE;
            ITEMTXT.Text = transferorderdetail.ITEMID;
            VID.Text = transferorderdetail.VARIANT;

            transferorderdetail = new TransferOrderDetail();
            transferorderdetail.VARIANT = VARIANTBOX.Value.ToString();
            transferorderdetail.ITEMID = ITEMBOX.Value.ToString();
            transferorderdetail = CMSfunction.GetPriceDetail(transferorderdetail, Session["DefaultSite"].ToString());
            PRICETXT.Text = transferorderdetail.PRICE;

        }
    }
}