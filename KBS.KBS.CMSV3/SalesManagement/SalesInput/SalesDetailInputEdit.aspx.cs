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

namespace KBS.KBS.CMSV3.SalesManagement.SalesInput
{
    public partial class SalesDetailInputEdit : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        OutputMessage message = new OutputMessage();
        private DataTable DTDetailInput = new DataTable();

        protected override void OnInit(EventArgs e)
        {

            if (Session["INPUTLINE"] == null || Session["INPUTSALESID"] == null || Session["INPUTSALESID"] == null)
            {
                Response.Redirect("SalesDetailInput.aspx");
            }
            else if (Session["UserID"] == null)
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
            DTDetailInput = CMSfunction.GetSKULinkBox(Session["DefaultSite"].ToString());
            SKUBOX.DataSource = DTDetailInput;
            SKUBOX.ValueField = "VALUE";
            SKUBOX.ValueType = typeof(string);
            SKUBOX.TextField = "DESCRIPTION";
            SKUBOX.DataBind();

            if (!IsPostBack)
            {
                DTDetailInput = new DataTable();
                DTDetailInput = CMSfunction.GetItemByAssortment(Session["DefaultSite"].ToString());
                ITEMBOX.DataSource = DTDetailInput;
                ITEMBOX.ValueField = "VALUE";
                ITEMBOX.ValueType = typeof(string);
                ITEMBOX.TextField = "DESCRIPTION";
                ITEMBOX.DataBind();

                

                SalesInputDetail salesinputdetail = new SalesInputDetail();
                salesinputdetail.SALESID = Session["INPUTSALESID"].ToString();
                salesinputdetail.IID = Session["INPUTIID"].ToString();
                salesinputdetail.NOTA = Session["INPUTNOTA"].ToString();
                salesinputdetail.RECEIPTID = Session["INPUTRECEIPTID"].ToString();
                salesinputdetail.LINE = Session["INPUTLINE"].ToString();
                salesinputdetail = CMSfunction.GetSalesInputDetailUpdate(salesinputdetail);

                ITEMTXT.Text = salesinputdetail.ITEMID;
                VID.Text = salesinputdetail.VARIANTID;
                BARCODETXT.Text = salesinputdetail.BARCODE;
                QTYTXT.Text = salesinputdetail.SALESQTY;
                PRICETXT.Text = salesinputdetail.SALESPRICE;
                COMMENTXT.Text = salesinputdetail.COMMENT;

                ITEMBOX.Value = salesinputdetail.ITEMID;
                DTDetailInput = new DataTable();
                DTDetailInput = CMSfunction.GetVariantByAssortment(ITEMBOX.Value.ToString(), Session["DefaultSite"].ToString());
                VARIANTBOX.DataSource = DTDetailInput;
                VARIANTBOX.ValueField = "VALUE";
                VARIANTBOX.ValueType = typeof(string);
                VARIANTBOX.TextField = "DESCRIPTION";
                VARIANTBOX.DataBind();
                VARIANTBOX.Value = salesinputdetail.VARIANTID;
                SKUBOX.Value = int.Parse(salesinputdetail.SKUID);
                SKUBOX.Text = salesinputdetail.NOTA;
                

            }
            

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
           
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
           
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            Session.Remove("ParamHeaderIDforUpdate");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("SalesDetailInput.aspx");
            else

                Response.Redirect("SalesDetailInput.aspx");
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
            
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            ProcessUpdate();
            Response.Redirect("SalesDetailInputEdit.aspx");
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
        protected void ITEMBOX_SelectedIndexChanged(object sender, EventArgs e)
        {
            DTDetailInput = new DataTable();
            DTDetailInput = CMSfunction.GetVariantByAssortment(ITEMBOX.Value.ToString(), Session["DefaultSite"].ToString());
            VARIANTBOX.DataSource = DTDetailInput;
            VARIANTBOX.ValueField = "VALUE";
            VARIANTBOX.ValueType = typeof(string);
            VARIANTBOX.TextField = "DESCRIPTION";
            VARIANTBOX.DataBind();


        }
        protected void Search(object sender, EventArgs e)
        {
            Session["SearchRedirect"] = "SalesDetailInputEdit.aspx";
            Response.Redirect("SearchItemMaster.aspx");
        }
        private void ProcessUpdate()
        {

            SalesInputDetail salesinputdetail = new SalesInputDetail();
            salesinputdetail.SALESID = Session["INPUTSALESID"].ToString();
            salesinputdetail.IID = Session["INPUTIID"].ToString();
            salesinputdetail.NOTA = Session["INPUTNOTA"].ToString();
            salesinputdetail.LINE = Session["INPUTLINE"].ToString();
            salesinputdetail.RECEIPTID = Session["INPUTRECEIPTID"].ToString();
            salesinputdetail.DATE = DateTime.Parse(Session["INPUTDATE"].ToString());
            salesinputdetail.SITE = Session["INPUTSITE"].ToString();
            salesinputdetail.ITEMID = ITEMBOX.Value.ToString();
            salesinputdetail.VARIANTID = VARIANTBOX.Value.ToString();
            salesinputdetail.BARCODE = BARCODETXT.Text;
            salesinputdetail.SALESQTY = QTYTXT.Text;
            salesinputdetail.SALESPRICE = PRICETXT.Text;
            salesinputdetail.COMMENT = COMMENTXT.Text;
            salesinputdetail.SKUID = SKUBOX.Value.ToString();

            ITEMTXT.Text = "";
            VID.Text = "";
            BARCODETXT.Text = "";
            QTYTXT.Text = "";
            PRICETXT.Text = "";
            SKUBOX.Text = "";
            SKUBOX.Value = 0;

            Session["SearchVariantforUpdate"] = "";
            Session["SearchItemIDforUpdate"] = "";
            Session["SearchBarcodeforUpdate"] = "";
            message = CMSfunction.UpdateSalesInputDetail(salesinputdetail, Session["UserID"].ToString());

        }

    }
}