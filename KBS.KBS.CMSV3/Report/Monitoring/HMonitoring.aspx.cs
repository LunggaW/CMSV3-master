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
using System.Configuration;

namespace KBS.KBS.CMSV3.Report.Monitoring
{
    public partial class HMonitoring : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        OutputMessage message = new OutputMessage();
        private DataTable DTTransType = new DataTable();
        private DataTable DTSite = new DataTable();
        private DataTable DTStock = new DataTable();

        private String MenuID = ConfigurationManager.AppSettings["MenuIdStockMovement"];

        protected override void OnInit(EventArgs e)
        {

            
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/Logins.aspx");
                
            }
            else
            {
                loadNavBar();
                loadButton(MenuID);
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
          

           
           
            if (!IsPostBack)
            {
                DateEnd.Value = DateTime.Now;
                DateFrom.Value = DateTime.Now;
                DTSite = CMSfunction.GetSiteBySiteProfile(Session["SiteProfile"].ToString());
                ComboSite.DataSource = DTSite;
                ComboSite.ValueField = "SITESITE";
                ComboSite.ValueType = typeof(string);
                ComboSite.TextField = "SITESITENAME";
                ComboSite.DataBind();
                RefreshDataGrid();
               
            }
            RefreshDataGrid();


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
                        EditBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        
                        if (accessContainer.Type == "0")
                        {
                            HeaderGridView.ClientSideEvents.RowDblClick = null;
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
            DateEnd.Value = DateTime.Now;
            DateFrom.Value = DateTime.Now;
           
            
            ComboSite.Text = "";
            DTTransType = CMSfunction.GetParameterValueAndDescbyClassAndTabID(Session["Class"].ToString(), "18");
            


            DTSite = CMSfunction.GetSiteBySiteProfile(Session["SiteProfile"].ToString());
            ComboSite.DataSource = DTSite;
            ComboSite.ValueField = "SITESITE";
            ComboSite.ValueType = typeof(string);
            ComboSite.TextField = "SITESITENAME";
            ComboSite.DataBind();
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
          
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
           
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            //Response.Redirect("ParameterManagementDetailNew.aspx");
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
           
        }


        private void ProcessUpdate()
        {
          
        }
        protected void ASPxButtonEntry_Click(object sender, EventArgs e)
        {
            if (HeaderGridView.FocusedRowIndex != -1)
            {
                Session["Filter"] = "HMonitoring";
                Session["SiteMonitoring"] = HeaderGridView.GetRowValues(HeaderGridView.FocusedRowIndex, "SITE").ToString();
                Session["FromMonitoring"] = DateFrom.Text;
                Session["EndMonitoring"] = DateEnd.Text;

                Session["TransTypeMonitoring"] =
                    HeaderGridView.GetRowValues(HeaderGridView.FocusedRowIndex, "STATUS").ToString() == "Sales"
                        ? '1'
                        : '2';
                Response.Redirect("DMonitoring.aspx");
            }


        }
        private void RefreshDataGrid()
        {
            Stock StockDisplay = new Stock();

            

           

            StockDisplay.DateFrom = DateFrom.Date != DateTime.MinValue ? (DateTime?)DateFrom.Date : null; 
            
            StockDisplay.DateEnd = DateEnd.Date != DateTime.MinValue ? (DateTime?)DateEnd.Date : null;
            StockDisplay.Site = (ComboSite.Value != null) ? ComboSite.Value.ToString() : "";
            
            


            DTStock = CMSfunction.GetAllTransactionTableHeaderMonitoring(StockDisplay, Session["SiteProfile"].ToString());
            HeaderGridView.DataSource = DTStock;
            HeaderGridView.KeyFieldName = "SITE";
            HeaderGridView.DataBind();

        }
        
    }
}