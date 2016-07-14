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
    public partial class TOShipmentNew : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTTOShipment = new DataTable();
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
            //String IDGRP = Session["SKUIDforUpdate"].ToString();
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
           
            IDTXT.Text = "";
            IIDTXT.Text = "";
            TDATE.Text = "";
            FROMBOX.Text = "";
            TOBOX.Text = "";
            //ASPxTextBoxSClass.Text = "";
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            //Session.Remove("ParamHeaderIDforUpdate");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("TOShipmentHeader.aspx");
            else

                Response.Redirect("TOShipmentHeader.aspx");
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
            Response.Redirect("TOShipmentHeader.aspx");
        }

        private void ProcessInsert()
        {
            TransferOrderHeader transferorderheader = new TransferOrderHeader();
            transferorderheader.ID = IDTXT.Text;
            transferorderheader.IID = IIDTXT.Text ;
            transferorderheader.DATE = TDATE.Date != DateTime.MinValue ? (DateTime?)TDATE.Date : null; 
            transferorderheader.FROM = FROMBOX.Value.ToString() ;
            transferorderheader.TO = TOBOX.Value.ToString();
            transferorderheader.STATUS = "0";


            message = CMSfunction.InsertTOShipmentGroup(transferorderheader, Session["UserID"].ToString());

        }

    }
}