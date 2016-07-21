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
    public partial class TransferOrderEdit : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTTOShipment = new DataTable();
        OutputMessage message = new OutputMessage();

        protected override void OnInit(EventArgs e)
        {

            if (Session["TRANSFERID"] == null)
            {
                Response.Redirect("TransferOrder.aspx");
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
            if (!IsPostBack)
            {
                String sesi = Session["TRANSFERID"].ToString();
                TransferOrderHeader transferorder = new TransferOrderHeader();

                transferorder = CMSfunction.GetTOShipmentUpdate(transferorder, sesi);

                DTTOShipment = CMSfunction.GetSITEBox();
                FROMBOX.DataSource = DTTOShipment;
                FROMBOX.ValueField = "VALUE";
                FROMBOX.ValueType = typeof(string);
                FROMBOX.TextField = "DESCRIPTION";
                FROMBOX.DataBind();

                DTTOShipment = CMSfunction.GetSITEBox();
                TOBOX.DataSource = DTTOShipment;
                TOBOX.ValueField = "VALUE";
                TOBOX.ValueType = typeof(string);
                TOBOX.TextField = "DESCRIPTION";
                TOBOX.DataBind();

                IDTXT.Text = Session["TRANSFERID"].ToString();
                IIDTXT.Text = transferorder.IID;
                TDATE.Text = transferorder.DATE.ToString();
                TDATE.Date = Convert.ToDateTime(transferorder.DATE.ToString());
                FROMBOX.Value = transferorder.FROM;
                TOBOX.Value = transferorder.TO;
                STATUSBOX.Items.Clear();
                STATUSBOX.Items.Add("CREATION");
                STATUSBOX.Items.Add("SHIPMENT");
               
                if (transferorder.STATUS == "0")
                {
                    STATUSBOX.Text = "CREATION";
                    STATUSBOX.Value = "CREATION";
                }
                else
                {
                    STATUSBOX.Text = "CREATION";
                    STATUSBOX.Value = "CREATION";
                }
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
            Session.Remove("TRANSFERID");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("TransferOrder.aspx");
            else

                Response.Redirect("TransferOrder.aspx");
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
            Response.Redirect("TransferOrder.aspx");
        }


        private void ProcessUpdate()
        {
            TransferOrderHeader transferorderheader = new TransferOrderHeader();
            transferorderheader.ID = IDTXT.Text;
            transferorderheader.IID = IIDTXT.Text;
            transferorderheader.DATE = TDATE.Date != DateTime.MinValue ? (DateTime?)TDATE.Date : null;
            transferorderheader.FROM = FROMBOX.Value.ToString();
            transferorderheader.TO = TOBOX.Value.ToString();
            if (STATUSBOX.Text == "CREATION")
            {
                transferorderheader.STATUS = "0";
            }
            else
            {
                transferorderheader.STATUS = "2";
            }


            message = CMSfunction.UpdateTOShipmentGroup(transferorderheader, Session["UserID"].ToString());

        }

    }
}