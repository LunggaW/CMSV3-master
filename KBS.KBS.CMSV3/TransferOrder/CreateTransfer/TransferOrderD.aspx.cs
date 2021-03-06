﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.TransferOrder
{
    public partial class TransferOrderD : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTTransferDetail = new DataTable();
        private DataTable DTParameterDetail = new DataTable();
        private DataTable DTGridViewUser = new DataTable();
        private User user;

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
            TransferOrderDetail transferorderdetail = new TransferOrderDetail();
            transferorderdetail.ID = Session["TRANSFERID"].ToString();
            transferorderdetail.IID = Session["INTERNALID"].ToString();

            DTTransferDetail = CMSfunction.GetTOShipmentDetailDataTable(transferorderdetail);
            ASPxGridViewHeader.DataSource = DTTransferDetail;
            ASPxGridViewHeader.KeyFieldName = "TRANSFER ID";
            ASPxGridViewHeader.DataBind();

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

        protected void ASPxGridViewHeader_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                Session["TOIDforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "TRANSFER ID").ToString();
                Session["TOIIDforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "INTERNAL ID").ToString();
                Session["TOITEMIDforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ITEM ID").ToString();
                Session["TOVARIANTIDforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "VARIANT ID").ToString();
                Session["TOBARCODEforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "BARCODE").ToString();
                Session["TOQTYforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "QTY").ToString();
                Session["TOforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SHIP").ToString();
                Session["TOCMTforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "COMMENT").ToString();


                if (Page.IsCallback)
                    ASPxWebControl.RedirectOnCallback("TransferOrderDetailEdit.aspx");
                else

                    Response.Redirect("TransferOrderDetailEdit.aspx");
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {

        }

        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.PageIndex <= ASPxGridViewHeader.PageCount - 1)
            {
                ASPxGridViewHeader.PageIndex = ASPxGridViewHeader.PageIndex + 1;
            }
        }
        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.PageIndex - 1 >= 0)
            {
                ASPxGridViewHeader.PageIndex = ASPxGridViewHeader.PageIndex - 1;
            }
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewHeader.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewHeader.PageIndex = ASPxGridViewHeader.PageCount - 1;
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("TransferOrderDetailNew.aspx");
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("TransferOrder.aspx");
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                String ColorID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
                String ColorGrpID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "GROUP ID").ToString();


                OutputMessage message = new OutputMessage();

                message = CMSfunction.DeleteColorDetail(ColorID, ColorGrpID);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;
            }
            Response.Redirect("TransferOrderD.aspx");
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                Session["TOIDforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "TRANSFER ID").ToString();
                Session["TOIIDforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "INTERNAL ID").ToString();
                Session["TOITEMIDforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ITEM ID").ToString();
                Session["TOVARIANTIDforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "VARIANT ID").ToString();
                Session["TOBARCODEforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "BARCODE").ToString();
                Session["TOQTYforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "QTY").ToString();
                Session["TOforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SHIP").ToString();
                Session["TOCMTforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "COMMENT").ToString();


                if (Page.IsCallback)
                    ASPxWebControl.RedirectOnCallback("TransferOrderDetailEdit.aspx");
                else

                    Response.Redirect("TransferOrderDetailEdit.aspx");
            }
        }
    }

}