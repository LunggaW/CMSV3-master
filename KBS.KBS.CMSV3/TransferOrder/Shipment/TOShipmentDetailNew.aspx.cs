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
using KBS.KBS.CMSV3.TransferOrder;


namespace KBS.KBS.CMSV3.TransferOrder.TOShipment
{
    public partial class TOShipmentDetailNew : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTColor = new DataTable();
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
                ITEMTXT.Text = Session["SearchItemIDforUpdate"].ToString();
                VID.Text = Session["SearchVariantforUpdate"].ToString();
                BARCODETXT.Text = Session["SearchBarcodeforUpdate"].ToString();
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
            Session["SearchRedirect"] = "TOShipmentDetailNew.aspx";
            Response.Redirect("SearchItemMaster.aspx");
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            //Session.Remove("ParamHeaderIDforUpdate");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("TOShipmentDetail.aspx");
            else

                Response.Redirect("TOShipmentDetail.aspx");
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
            Response.Redirect("TOShipmentDetail.aspx");
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


        private void ProcessInsert()
        {
            TransferOrderDetail transferorderdetail = new TransferOrderDetail();
            transferorderdetail.ID = Session["TOShipIDforUpdate"].ToString();
            transferorderdetail.IID = Session["TOShipIIDforUpdate"].ToString();
            transferorderdetail.ITEMID = ITEMTXT.Text;
            transferorderdetail.VARIANT = VID.Text;
            transferorderdetail.BARCODE = BARCODETXT.Text;
            transferorderdetail.QTY = QTYTXT.Text;


            message = CMSfunction.InsertTOShipmentDetail(transferorderdetail, Session["UserID"].ToString());

        }

    }
}