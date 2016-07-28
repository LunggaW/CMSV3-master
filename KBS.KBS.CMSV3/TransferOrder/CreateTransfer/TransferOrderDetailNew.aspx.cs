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


namespace KBS.KBS.CMSV3.TransferOrder.CreateTransfer
{
    public partial class TransferOrderDetailNew : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTColor = new DataTable();
        OutputMessage message = new OutputMessage();
        private DataTable DTDetailInput = new DataTable();

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
                DTDetailInput = CMSfunction.GetItemByAssortment2(Session["SITETO"].ToString() , Session["SITEFROM"].ToString());
                ITEMBOX.DataSource = DTDetailInput;
                ITEMBOX.ValueField = "VALUE";
                ITEMBOX.ValueType = typeof(string);
                ITEMBOX.TextField = "DESCRIPTION";
                ITEMBOX.DataBind();

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



        protected void ClearBtn_Click(object sender, EventArgs e)
        {

            ITEMTXT.Text = "";
            VID.Text = "";
            BARCODETXT.Text = "";
            QTYTXT.Text = "";
            //ASPxTextBoxSClass.Text = "";
        }
        protected void Search(object sender, EventArgs e)
        {
            Session["SearchRedirect"] = "TransferOrderDetailNew.aspx";
            Response.Redirect("SearchItemMaster.aspx");
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            //Session.Remove("ParamHeaderIDforUpdate");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("TransferOrderD.aspx");
            else

                Response.Redirect("TransferOrderD.aspx");
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
            Response.Redirect("TransferOrderD.aspx");
        }
        protected void ITEMBOX_SelectedIndexChanged(object sender, EventArgs e)
        {
            DTDetailInput = new DataTable();
            DTDetailInput = CMSfunction.GetVariantByAssortment2(ITEMBOX.Value.ToString(), Session["SITETO"].ToString(), Session["SITEFROM"].ToString());
            VARIANTBOX.DataSource = DTDetailInput;
            VARIANTBOX.ValueField = "VALUE";
            VARIANTBOX.ValueType = typeof(string);
            VARIANTBOX.TextField = "DESCRIPTION";
            VARIANTBOX.DataBind();


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


        private void ProcessInsert()
        {
            TransferOrderDetail transferorderdetail = new TransferOrderDetail();
            transferorderdetail.ID = Session["TRANSFERID"].ToString();
            transferorderdetail.IID = Session["INTERNALID"].ToString();
            transferorderdetail.ITEMID = ITEMTXT.Text;
            transferorderdetail.VARIANT = VID.Text;
            transferorderdetail.BARCODE = BARCODETXT.Text;
            transferorderdetail.QTY = QTYTXT.Text;


            message = CMSfunction.InsertTOShipmentDetail(transferorderdetail, Session["UserID"].ToString());

        }

    }
}