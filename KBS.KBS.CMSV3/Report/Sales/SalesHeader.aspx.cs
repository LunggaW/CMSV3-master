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

namespace KBS.KBS.CMSV3.Report
{
    public partial class SalesHeader : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSalesHeader = new DataTable();
        private DataTable DTSite = new DataTable();
        private String MenuID = ConfigurationManager.AppSettings["MenuIdReportSalesHeader"];

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
            ChangeTotalVisibility(false);

            DTSite = CMSfunction.GetSiteBySiteProfile(Session["SiteProfile"].ToString());
            
            ComboSite.DataSource = DTSite;
            ComboSite.ValueField = "SITESITE";
            ComboSite.ValueType = typeof(string);
            ComboSite.TextField = "SITESITENAME";
            ComboSite.DataBind();


            //user = CMSfunction.SelectUserDataFromUserID(User.Identity.Name);

            //ASPxComboBoxUserManagementSite.DataSource = DTSite;
            //ASPxComboBoxUserManagementSite.ValueField = "SITECODE";
            //ASPxComboBoxUserManagementSite.ValueType = typeof(string);
            //ASPxComboBoxUserManagementSite.TextField = "SITENAME";
            //ASPxComboBoxUserManagementSite.DataBind();

            //DTProfile = CMSfunction.GetAllProfile();
            //ASPxComboBoxUserManagementProfile.DataSource = DTProfile;
            //ASPxComboBoxUserManagementProfile.ValueField = "PROFILEID";
            //ASPxComboBoxUserManagementProfile.ValueType = typeof(string);
            //ASPxComboBoxUserManagementProfile.TextField = "PROFILENAME";
            //ASPxComboBoxUserManagementProfile.DataBind();

            //DTGridViewUser = CMSfunction.GetAllUser();
            //ASPxGridViewUserManagementUser.DataSource = DTGridViewUser;
            //ASPxGridViewUserManagementUser.KeyFieldName = "USERID";
            //ASPxGridViewUserManagementUser.DataBind();
            //loadNavBar();


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
                            ASPxGridViewSalesHeader.ClientSideEvents.RowDblClick = null;
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
            RefreshDataGrid();
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
            if (ASPxGridViewSalesHeader.FocusedRowIndex != -1)
            {
                String ItemIDExternal = ASPxGridViewSalesHeader.GetRowValues(ASPxGridViewSalesHeader.FocusedRowIndex, "ITEM ID").ToString();

                OutputMessage message = new OutputMessage();

                message = CMSfunction.DeleteItem(ItemIDExternal);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;

                
            }
        }

        private void RefreshDataGrid()
        {
            DateTime? DateFrom = null, DateTo = null;
            String Site, Type;
            
            
           
            if (ASPxDateEditFrom.Value != null)
            {
                DateFrom = ASPxDateEditFrom.Date;
            }

            if (ASPxDateEditTo.Value != null)
            {
                DateTo = ASPxDateEditTo.Date;
            }


            Site = (ComboSite.Value != null) ? ComboSite.Value.ToString() : "";

            Type = (ComboType.Value != null) ? ComboType.Value.ToString() : "";

            DTSalesHeader = CMSfunction.GetSalesDetail2(DateFrom, DateTo, Site,  Session["SiteProfile"].ToString(), Type);
            ASPxGridViewSalesHeader.DataSource = DTSalesHeader;
            ASPxGridViewSalesHeader.KeyFieldName = "Sales ID";
            ASPxGridViewSalesHeader.DataBind();
            

            
            DATAMODEL.SalesSummary SalesSum = new DATAMODEL.SalesSummary();

            SalesSum.FromDate = DateFrom;
            SalesSum.ToDate = DateTo;
            SalesSum.Site = Site;

            SalesSum = CMSfunction.GetSalesSummary(SalesSum, Session["SiteProfile"].ToString(), Type);

            
            TextBoxTotalDisc.Text = SalesSum.TotDisc.ToString();
            TextBoxTotalItem.Text = SalesSum.TotItem.ToString();
            TextBoxTotalNota.Text = SalesSum.TotNota.ToString();
            TextBoxTotalQty.Text = SalesSum.TotQty.ToString();
            TextBoxTotalSales.Text = SalesSum.TotSales.ToString();

            ChangeTotalVisibility(true);
        }

        #region Grid Navigation Button
        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewSalesHeader.PageIndex <= ASPxGridViewSalesHeader.PageCount - 1)
            {
                ASPxGridViewSalesHeader.PageIndex = ASPxGridViewSalesHeader.PageIndex + 1;
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewSalesHeader.PageIndex - 1 >= 0)
            {
                ASPxGridViewSalesHeader.PageIndex = ASPxGridViewSalesHeader.PageIndex - 1;
            }
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewSalesHeader.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewSalesHeader.PageIndex = ASPxGridViewSalesHeader.PageCount - 1;
        }
        #endregion


        protected void ASPxButtonEntry_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewSalesHeader.FocusedRowIndex != -1)
            {
                
                Response.Redirect("SalesDetail.aspx");
            }
        }

        protected void ASPxButtonReport_Click(object sender, EventArgs e)
        {

        }

        private void ChangeTotalVisibility(Boolean IsVisible)
        {
            if (IsVisible)
            {
                LabelTotalDisc.Visible = true;
                //LabelTotalItem.Visible = true;
                //LabelTotalNota.Visible = true;
                LabelTotalQuantity.Visible = true;
                LabelTotalSales.Visible = true;

                TextBoxTotalDisc.Visible = true;
                //TextBoxTotalItem.Visible = true;
                //TextBoxTotalNota.Visible = true;
                TextBoxTotalQty.Visible = true;
                TextBoxTotalSales.Visible = true;



            }
            else
            {
                LabelTotalDisc.Visible = false;
                //LabelTotalItem.Visible = false;
                //LabelTotalNota.Visible = false;
                LabelTotalQuantity.Visible = false;
                LabelTotalSales.Visible = false;

                TextBoxTotalDisc.Visible = false;
                //TextBoxTotalItem.Visible = false;
                //TextBoxTotalNota.Visible = false;
                TextBoxTotalQty.Visible = false;
                TextBoxTotalSales.Visible = false;
            }
        }
    }
}