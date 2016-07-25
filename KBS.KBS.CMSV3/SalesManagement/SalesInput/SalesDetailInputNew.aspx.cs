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
    public partial class SalesDetailInputNew : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTDetailInput = new DataTable();
        OutputMessage message = new OutputMessage();

        protected override void OnInit(EventArgs e)
        {
            if (Session["INPUTSALESID"] != null)
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
            else
            {
                Response.Redirect("~/Account/Logins.aspx");
            }
        }
        protected void BarcodeCek(object sender, EventArgs e)
        {
            TransferOrderDetail transferorderdetail = new TransferOrderDetail();
            transferorderdetail.BARCODE = BARCODETXT.Text;
            transferorderdetail = CMSfunction.GetBarcodeTransferDetail(transferorderdetail);
            ITEMTXT.Text = transferorderdetail.ITEMID;
            VID.Text = transferorderdetail.VARIANT;
            BARCODETXT.Text = transferorderdetail.BARCODE;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["SearchVariantforUpdate"] == null)
                {
                    Session["SearchVariantforUpdate"] = "";
                }
                if (Session["SearchItemIDforUpdate"] == null)
                {
                    Session["SearchItemIDforUpdate"] = "";
                }
                if (Session["SearchBarcodeforUpdate"] == null)
                {
                    Session["SearchBarcodeforUpdate"] = "";
                }
                PRICETXT.Text = "0";
                if ((Session["SearchVariantforUpdate"].ToString() != "") && (Session["SearchItemIDforUpdate"].ToString() != ""))
                {
                    TransferOrderDetail transferorderdetail = new TransferOrderDetail();
                    transferorderdetail.VARIANT = Session["SearchVariantforUpdate"].ToString();
                    transferorderdetail.ITEMID = Session["SearchItemIDforUpdate"].ToString();
                    transferorderdetail = CMSfunction.GetPriceDetail(transferorderdetail, Session["DefaultSite"].ToString());
                    PRICETXT.Text = transferorderdetail.PRICE;
                }
                ITEMTXT.Text = Session["SearchItemIDforUpdate"].ToString();
                VID.Text = Session["SearchVariantforUpdate"].ToString();
                BARCODETXT.Text = Session["SearchBarcodeforUpdate"].ToString();
                Session["SearchRedirect"] = null;
            }
            DTDetailInput = CMSfunction.GetSKULinkBox(Session["DefaultSite"].ToString());
            SKUBOX.DataSource = DTDetailInput;
            SKUBOX.ValueField = "VALUE";
            SKUBOX.ValueType = typeof(string);
            SKUBOX.TextField = "DESCRIPTION";
            SKUBOX.DataBind();
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
            
            SalesInputDetail salesinputdetail = new SalesInputDetail();
            salesinputdetail.SALESID = Session["INPUTSALESID"].ToString();
            salesinputdetail.IID = Session["INPUTIID"].ToString();
            salesinputdetail.NOTA = Session["INPUTNOTA"].ToString();
            salesinputdetail.RECEIPTID = Session["INPUTRECEIPTID"].ToString();
            salesinputdetail.DATE = DateTime.Parse(Session["INPUTDATE"].ToString());
            salesinputdetail.SITE = Session["INPUTSITE"].ToString();
            salesinputdetail.ITEMID = ITEMTXT.Text;
            salesinputdetail.VARIANTID = VID.Text;
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

    }
}