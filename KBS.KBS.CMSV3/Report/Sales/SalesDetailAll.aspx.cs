﻿using System;
using System.Collections.Generic;
using System.Configuration;
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
using Microsoft.Reporting.WebForms;

namespace KBS.KBS.CMSV3.Report.Sales
{
    public partial class SalesDetailAll : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSalesDetailAll = new DataTable();
        private DataTable DTSite = new DataTable();
        private String MenuID = ConfigurationManager.AppSettings["MenuIdReportSalesHeader"];
        DATAMODEL.SalesDetail SalesDet = new DATAMODEL.SalesDetail();

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/logins.aspx");
            }
            else
            {
                loadNavBar();
                loadButton(MenuID);
            }
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //RefreshDataGrid();

            DTSite = CMSfunction.GetSiteBySiteProfile(Session["SiteProfile"].ToString());
            
            ComboSite.DataSource = DTSite;
            ComboSite.ValueField = "SITESITE";
            ComboSite.ValueType = typeof(string);
            ComboSite.TextField = "SITESITENAME";
            ComboSite.DataBind();


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

        private void loadButton(String MenuID)
        {

            List<AccessContainer> listAccessCont =
                CMSfunction.SelectAccessByProfileAndMenuID(Session["AccessProfile"].ToString(), MenuID);

            foreach (var accessContainer in listAccessCont)
            {
                switch (accessContainer.FunctionId)
                {
                    case "1":
                        AddBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        break;
                    case "2":
                        //Ed.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        if (accessContainer.Type == "0")
                        {
                            //ASPxGridViewSalesHeader.ClientSideEvents.RowDblClick = null;
                        }

                        break;
                    case "3":
                        SearchBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        break;
                    case "4":
                        DelBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        break;
                    default:
                        break;

                }
            }
        }


        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            //RefreshDataGrid();
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

        protected void ASPxGridViewDetail_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            //Session["SalesHeaderSalesID"] = ASPxGridViewSalesHeader.GetRowValues(ASPxGridViewSalesHeader.FocusedRowIndex, "Sales ID").ToString();
            //Session["SalesHeaderNota"] = ASPxGridViewSalesHeader.GetRowValues(ASPxGridViewSalesHeader.FocusedRowIndex, "Nomor Nota").ToString();
            //Session["SalesHeaderReceipt"] = ASPxGridViewSalesHeader.GetRowValues(ASPxGridViewSalesHeader.FocusedRowIndex, "Receipt ID").ToString();

            //if (Page.IsCallback)
            //    ASPxWebControl.RedirectOnCallback("SalesDetail.aspx");
            //else
            //    Response.Redirect("SalesDetail.aspx");
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            Session.Remove("ParamHeaderID");
            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("ParameterManagementHeader.aspx");
            else
                Response.Redirect("ParameterManagementHeader.aspx");
            
            
            
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {

                if (Page.IsCallback)
                    ASPxWebControl.RedirectOnCallback("ItemManagementHeaderNew.aspx");
                else
                    Response.Redirect("ItemManagementHeaderNew.aspx");

            
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            //if (ASPxGridViewSalesHeader.FocusedRowIndex != -1)
            //{
            //    String ItemIDExternal = ASPxGridViewSalesHeader.GetRowValues(ASPxGridViewSalesHeader.FocusedRowIndex, "ITEM ID").ToString();

            //    OutputMessage message = new OutputMessage();

            //    message = CMSfunction.DeleteItem(ItemIDExternal);

            //    LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            //    LabelMessage.Visible = true;
            //    LabelMessage.Text = message.Message;

            //    RefreshDataGrid();
            //}
        }

        private void RefreshDataGrid()
        {
            

            
            
           
            if (ASPxDateEditFrom.Value != null)
            {
                SalesDet.FromDate= ASPxDateEditFrom.Date;
            }

            if (ASPxDateEditTo.Value != null)
            {
                SalesDet.ToDate = ASPxDateEditTo.Date;
            }


            SalesDet.Site = (ComboSite.Value != null) ? ComboSite.Value.ToString() : "";

            if (String.IsNullOrWhiteSpace(TextBoxNota.Text))
            {
                SalesDet.Nota = null;
            }
            else
            {
                SalesDet.Nota = TextBoxNota.Text;
            }

            if (String.IsNullOrWhiteSpace(TextBoxReceipt.Text))
            {
                SalesDet.ReceiptID = null;
            }
            else
            {
                SalesDet.ReceiptID = TextBoxReceipt.Text;
            }

            if (String.IsNullOrWhiteSpace(TextBoxBarcode.Text))
            {
                SalesDet.Barcode = null;
            }
            else
            {
                SalesDet.Barcode = TextBoxBarcode.Text;
            }

            if (String.IsNullOrWhiteSpace(TextBoxItemID.Text))
            {
                SalesDet.ItemID = null;
            }
            else
            {
                SalesDet.ItemID = TextBoxItemID.Text;
            }

            DTSalesDetailAll = CMSfunction.GetSalesDetailAll(SalesDet, Session["SiteProfile"].ToString());
            
        }

        #region Grid Navigation Button
        protected void NextBtn_Click(object sender, EventArgs e)
        {
            //if (ASPxGridViewSalesHeader.PageIndex <= ASPxGridViewSalesHeader.PageCount - 1)
            //{
            //    ASPxGridViewSalesHeader.PageIndex = ASPxGridViewSalesHeader.PageIndex + 1;
            //}
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            //if (ASPxGridViewSalesHeader.PageIndex - 1 >= 0)
            //{
            //    ASPxGridViewSalesHeader.PageIndex = ASPxGridViewSalesHeader.PageIndex - 1;
            //}
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            //ASPxGridViewSalesHeader.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            //ASPxGridViewSalesHeader.PageIndex = ASPxGridViewSalesHeader.PageCount - 1;
        }
        #endregion
        

        protected void ASPxButtonReport_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
            ShowReport();
        }

        private void ShowReport()
        {
            try
            {

            
                //Reset
                ReportViewerSalesDetail.Reset();

                //Data Source
                //DataTable dt=   new DataTable();
                ReportDataSource rds= new ReportDataSource("DTSalesDetailAll", DTSalesDetailAll);

                //ReportViewerSalesSummary.LocalReport.DataSources.Add(rds);


                //Path  
                ReportViewerSalesDetail.LocalReport.ReportPath = "Report/Source/SalesDetail.rdlc";


                //Parameter
                ReportParameter[] rptParams = new ReportParameter[]
                {
                    new ReportParameter("Nota", SalesDet.Nota),
                    new ReportParameter("ReceiptID", SalesDet.ReceiptID),
                    new ReportParameter("DateFrom", SalesDet.FromDate.ToString()),
                    new ReportParameter("DateTo", SalesDet.ToDate.ToString()),
                };
                ReportViewerSalesDetail.LocalReport.SetParameters(rptParams);

                

                //Refresh
                ReportViewerSalesDetail.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                throw;
            }

            
            
        }
    }
}