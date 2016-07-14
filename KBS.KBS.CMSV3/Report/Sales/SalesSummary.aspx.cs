using System;
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

namespace KBS.KBS.CMSV3.Report
{
    public partial class SalesSummary : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSalesSummary = new DataTable();
        private DataTable DTSite = new DataTable();
        private String MenuID = ConfigurationManager.AppSettings["MenuIdReportSalesHeader"];
        DATAMODEL.SalesSummary SalesSum = new DATAMODEL.SalesSummary();

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
                SalesSum.FromDate= ASPxDateEditFrom.Date;
            }

            if (ASPxDateEditTo.Value != null)
            {
                SalesSum.ToDate = ASPxDateEditTo.Date;
            }


            SalesSum.Site = (ComboSite.Value != null) ? ComboSite.Value.ToString() : "";

            if (String.IsNullOrWhiteSpace(TextBoxNota.Text))
            {
                SalesSum.Nota = null;
            }
            else
            {
                SalesSum.Nota = TextBoxNota.Text;
            }

            SalesSum = CMSfunction.GetSalesSummary(SalesSum, Session["SiteProfile"].ToString(), "1"/*Type/*/);
            

            
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
            ReportViewerSalesSummary.Visible = true;
            ShowReport();
        }

        private void ShowReport()
        {
            try
            {

            
                //Reset
                ReportViewerSalesSummary.Reset();

                //Data Source
                //DataTable dt=   new DataTable();
                //ReportDataSource rds= new ReportDataSource("DataSetName", dt);

                //ReportViewerSalesSummary.LocalReport.DataSources.Add(rds);

                

                //Path  
                ReportViewerSalesSummary.LocalReport.ReportPath = "Report/Source/SalesSummary.rdlc";

                String Site = CMSfunction.GetSiteCodeandNameFromSiteCode(SalesSum.Site);
          
                //Parameter
                ReportParameter[] rptParams = new ReportParameter[]
                {
                    new ReportParameter("Nota", SalesSum.Nota),
                    new ReportParameter("Site", Site),
                    new ReportParameter("DateFrom", SalesSum.FromDate.HasValue ? SalesSum.FromDate.Value.ToString("dd-MMMM-yyyy") : ""),
                    new ReportParameter("DateTo", SalesSum.ToDate.HasValue ? SalesSum.ToDate.Value.ToString("dd-MMMM-yyyy") : ""),
                    new ReportParameter("TotQty", SalesSum.TotQty.ToString()),
                    new ReportParameter("TotItem", SalesSum.TotItem.ToString()),
                    new ReportParameter("TotDisc", SalesSum.TotDisc.ToString()),
                    new ReportParameter("TotPrice", SalesSum.TotPrice.ToString()),
                    new ReportParameter("TotNota", SalesSum.TotNota.ToString()),
                };
                ReportViewerSalesSummary.LocalReport.SetParameters(rptParams);
                

                //Refresh
                ReportViewerSalesSummary.LocalReport.Refresh();
            }
            catch (Exception ex)
            {
                throw;
            }

            
            
        }
    }
}