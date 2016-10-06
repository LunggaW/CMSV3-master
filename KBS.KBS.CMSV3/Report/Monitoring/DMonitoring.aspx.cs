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
    public partial class DMonitoring : System.Web.UI.Page
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
           
            DTTransType = CMSfunction.GetParameterValueAndDescbyClassAndTabID(Session["Class"].ToString(), "9");
            ComboTransType.DataSource = DTTransType;
            ComboTransType.ValueField = "PARVALUE";
            ComboTransType.ValueType = typeof(string);
            ComboTransType.TextField = "PARDESCRIPTION";
            ComboTransType.DataBind();

            DTTransType = CMSfunction.GetParameterValueAndDescbyClassAndTabID(Session["Class"].ToString(), "22");
            ComboInput.DataSource = DTTransType;
            ComboInput.ValueField = "PARVALUE";
            ComboInput.ValueType = typeof(string);
            ComboInput.TextField = "PARDESCRIPTION";
            ComboInput.DataBind();

            DTSite = CMSfunction.GetSiteBySiteProfile(Session["SiteProfile"].ToString());
            ComboSite.DataSource = DTSite;
            ComboSite.ValueField = "SITESITE";
            ComboSite.ValueType = typeof(string);
            ComboSite.TextField = "SITESITENAME";
            ComboSite.DataBind();
           
            if (!IsPostBack)
            {
                if (Session["Filter"].ToString() == "HMonitoring")
                {
                    DateFrom.Text = Session["FromMonitoring"].ToString();
                    DateEnd.Text = Session["EndMonitoring"].ToString();
                    ComboSite.Value = Session["SiteMonitoring"].ToString();
                    Session["Filter"] = "DMonitoring";
                }
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
                            GridMonitoring.ClientSideEvents.RowDblClick = null;
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
            ComboTransType.Text = "";
            TextBoxBarcode.Text = "";


            DTTransType = CMSfunction.GetParameterValueAndDescbyClassAndTabID(Session["Class"].ToString(), "22");
            ComboInput.DataSource = DTTransType;
            ComboInput.ValueField = "PARVALUE";
            ComboInput.ValueType = typeof(string);
            ComboInput.TextField = "PARDESCRIPTION";
            ComboInput.DataBind();
            


            ComboSite.Text = "";
            DTTransType = CMSfunction.GetParameterValueAndDescbyClassAndTabID(Session["Class"].ToString(), "9");
            ComboTransType.DataSource = DTTransType;
            ComboTransType.ValueField = "PARVALUE";
            ComboTransType.ValueType = typeof(string);
            ComboTransType.TextField = "PARDESCRIPTION";
            ComboTransType.DataBind();


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
      
        private void RefreshDataGrid()
        {
            Stock StockDisplay = new Stock();




           

            StockDisplay.DateFrom = DateFrom.Date != DateTime.MinValue ? (DateTime?)DateFrom.Date : null; 
            StockDisplay.Barcode = !string.IsNullOrWhiteSpace(TextBoxBarcode.Text) ? TextBoxBarcode.Text : "";
            StockDisplay.DateEnd = DateEnd.Date != DateTime.MinValue ? (DateTime?)DateEnd.Date : null;
            StockDisplay.Site = (ComboSite.Value != null) ? ComboSite.Value.ToString() : "";
            StockDisplay.TransactionType = (ComboTransType.Value != null) ? ComboTransType.Value.ToString() : "";
            StockDisplay.Nota = (ComboInput.Value != null) ? ComboInput.Value.ToString() : "";


            DTStock = CMSfunction.GetAllTransactionTableMonitoring(StockDisplay, Session["SiteProfile"].ToString());
            GridMonitoring.DataSource = DTStock;
            GridMonitoring.KeyFieldName = "SITE";
            GridMonitoring.DataBind();

        }
        
    }
}