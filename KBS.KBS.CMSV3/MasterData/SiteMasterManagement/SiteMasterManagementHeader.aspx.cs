using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using DevExpress.Office.NumberConverters;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.MasterData.SiteMasterManagement
{
    public partial class SiteMasterManagementHeader : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSiteClass = new DataTable();
        private DataTable DTGridViewSite = new DataTable();
        private User user;
        private String MenuID = ConfigurationManager.AppSettings["MenuIdSiteMasterManagement"];

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
                //if (!IsPostBack)
                //{
                //    loadNavBar();
                //}
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
                
                DTSiteClass = CMSfunction.GetParameterValueAndDescbyClassAndTabID(Session["Class"].ToString(), "3");
                ComboSiteClas.DataSource = DTSiteClass;
                ComboSiteClas.ValueField = "PARVALUE";
                ComboSiteClas.ValueType = typeof(string);
                ComboSiteClas.TextField = "PARDESCRIPTION";
                ComboSiteClas.DataBind();
                string Compare = Session["Filter"].ToString();
                if (Compare != "")
                {
                    if (Session["Filter"].ToString() != "SiteMaster")
                    {
                        Administration.UserManagement.UserManagementHeader Sunda = new Administration.UserManagement.UserManagementHeader();
                        Sunda.ClearDataSeasson();
                    }
                    else
                    {
                        try
                        {
                            if (Session["SiteFilter"].ToString() == "True")
                            {
                                TextBoxSite.Text = Session["FSite"].ToString();
                                TextBoxSiteName.Text = Session["FSiteName"].ToString();                                
                            }
                        }
                        catch
                        {
                            String Erordeui = "";
                        }                    
                    }
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
                        //Ed.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        if (accessContainer.Type == "0")
                        {
                            ASPxGridViewSite.ClientSideEvents.RowDblClick = null;
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
            Session["Filter"] = "SiteMaster";
            Session["SiteFilter"] = "True";
            Session["FSite"] = !string.IsNullOrWhiteSpace(TextBoxSite.Text) ? TextBoxSite.Text : "";
            Session["FSiteName"] = !string.IsNullOrWhiteSpace(TextBoxSiteName.Text) ? TextBoxSiteName.Text : "";

            RefreshDataGrid();
        }


        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewSite.FocusedRowIndex != -1)
            {
                String Site = ASPxGridViewSite.GetRowValues(ASPxGridViewSite.FocusedRowIndex, "SITE").ToString();

                OutputMessage message = new OutputMessage();

                message = CMSfunction.DeleteSiteMaster(Site);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;

                RefreshDataGrid();
            }
        }

        protected void ASPxGridViewSite_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            
            Session["Site"] = ASPxGridViewSite.GetRowValues(Convert.ToInt32(e.Parameters), "SITE").ToString();
            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("SiteMasterManagementHeaderDetail.aspx");
            else
                Response.Redirect("SiteMasterManagementHeaderDetail.aspx");
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            License license = new License();
            int ValidSiteCount = Int32.Parse(CMSfunction.GetValidSiteCount());

            String LicenseText = CMSfunction.GetLicense();
            LicenseText = CMSfunction.Decrypt(LicenseText);

            license = CMSfunction.ParseLicenseText(LicenseText);
            if (ValidSiteCount >= Int32.Parse(license.StoreTotal))
            {
                //TextBoxSite.Text = "";
                string script = "alert('License Have Maximal Site, Please Update License For More Site ');";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
            }
            else
            {
                Response.Redirect("SiteMasterManagementHeaderNew.aspx");
            }
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {

        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            TextBoxSite.Text = "";
            TextBoxSiteName.Text = "";
            Session["FSite"] = "";
            Session["FSiteName"] = "";
            Session["SiteFilter"] = "False";
            Response.Redirect("SiteMasterManagementHeader.aspx");
        }

        private void RefreshDataGrid()
        {
            SiteMaster siteMaster = new SiteMaster();

            siteMaster.Site = !string.IsNullOrWhiteSpace(TextBoxSite.Text) ? TextBoxSite.Text : "";
            siteMaster.SiteName = !string.IsNullOrWhiteSpace(TextBoxSiteName.Text) ? TextBoxSiteName.Text : "";


            siteMaster.SiteClass = ComboSiteClas.Value != null
                ? (int?)Int32.Parse(ComboSiteClas.Value.ToString())
                : null;

            DTGridViewSite = CMSfunction.GetAllSiteFiltered(siteMaster);
            ASPxGridViewSite.DataSource = DTGridViewSite;
            ASPxGridViewSite.KeyFieldName = "SITE";
            ASPxGridViewSite.DataBind();
        }

        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewSite.PageIndex <= ASPxGridViewSite.PageCount - 1)
            {
                ASPxGridViewSite.PageIndex = ASPxGridViewSite.PageIndex + 1;
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewSite.PageIndex - 1 >= 0)
            {
                ASPxGridViewSite.PageIndex = ASPxGridViewSite.PageIndex - 1;
            }
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewSite.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewSite.PageIndex = ASPxGridViewSite.PageCount - 1;
        }



    }
}