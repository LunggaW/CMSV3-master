﻿using System;
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
    public partial class TransferOrderDetailEdit : System.Web.UI.Page
    {

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        OutputMessage message = new OutputMessage();
        private DataTable DTDetailInput = new DataTable();

        protected override void OnInit(EventArgs e)
        {

            if (Session["TOIDforUpdate"] == null)
            {
                Response.Redirect("TransferOrderD.aspx");
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
                ITEMTXT.Text = Session["TOITEMIDforUpdate"].ToString();
                VID.Text = Session["TOVARIANTIDforUpdate"].ToString();
                BARCODETXT.Text = Session["TOBARCODEforUpdate"].ToString();
                QTYTXT.Text = Session["TOQTYforUpdate"].ToString();
             //   SHIPTXT.Text = Session["TOSHIPforUpdate"].ToString();
                COMENTEXT.Text = Session["TOCMTforUpdate"].ToString();

                DTDetailInput = new DataTable();
                DTDetailInput = CMSfunction.GetItemByAssortment2(Session["SITETO"].ToString(), Session["SITEFROM"].ToString());
                ITEMBOX.DataSource = DTDetailInput;
                ITEMBOX.ValueField = "VALUE";
                ITEMBOX.ValueType = typeof(string);
                ITEMBOX.TextField = "DESCRIPTION";
                ITEMBOX.DataBind();
                
                ITEMBOX.Value = Session["TOITEMIDforUpdate"].ToString();
                DTDetailInput = new DataTable();
                DTDetailInput = CMSfunction.GetVariantByAssortment2(ITEMBOX.Value.ToString(), Session["SITETO"].ToString(), Session["SITEFROM"].ToString());
                VARIANTBOX.DataSource = DTDetailInput;
                VARIANTBOX.ValueField = "VALUE";
                VARIANTBOX.ValueType = typeof(string);
                VARIANTBOX.TextField = "DESCRIPTION";
                VARIANTBOX.DataBind();
                VARIANTBOX.Value = Session["TOVARIANTIDforUpdate"].ToString();

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
            //ASPxTextBoxHeaderBlock.Text = "";
            //ASPxTextBoxHeaderComment.Text = "";
            //ASPxTextBoxHeaderCopy.Text = "";
            //ASPxTextBoxHeaderID.Text = "";
            //ASPxTextBoxHeaderName.Text = "";
            //ASPxTextBoxHeaderSClas.Text = "";
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            Session.Remove("ParamHeaderIDforUpdate");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("TransferOrderD.aspx");
            else

                Response.Redirect("TransferOrderD.aspx");
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
            Response.Redirect("TransferOrderD.aspx");
        }


        private void ProcessUpdate()
        {
            TransferOrderDetail transferorderdetail = new TransferOrderDetail();
            transferorderdetail.ID = Session["TOIDforUpdate"].ToString();
            transferorderdetail.IID = Session["TOIIDforUpdate"].ToString();
            transferorderdetail.ITEMID = ITEMTXT.Text;
            transferorderdetail.VARIANT = VID.Text;
            transferorderdetail.BARCODE = BARCODETXT.Text;
            transferorderdetail.QTY = QTYTXT.Text;
            //transferorderdetail.SHIP = SHIPTXT.Text;
            transferorderdetail.COMMENT = COMENTEXT.Text;

            message = CMSfunction.UpdateTOShipmentDetail(transferorderdetail, Session["UserID"].ToString());

        }

    }
}