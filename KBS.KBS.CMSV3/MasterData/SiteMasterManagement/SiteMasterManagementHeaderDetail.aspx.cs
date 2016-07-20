using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.MasterData.SiteMasterManagement
{
    public partial class SiteMasterManagementHeaderDetail : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSiteClass = new DataTable();
        OutputMessage message = new OutputMessage();
        private User user;

        protected override void OnInit(EventArgs e)
        {
            LabelMessage.Visible = false;
            //user = CMSfunction.SelectUserDataFromUserID(User.Identity.Name);
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/Logins.aspx");
            }
            else if (Session["Site"] == null)
            
            {
                Response.Redirect("SiteMasterManagementHeader.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    loadNavBar();
                    


                    DTSiteClass = CMSfunction.GetParameterValueAndDescbyClassAndTabID(Session["Class"].ToString(), "3");
                    ComboSiteClas.DataSource = DTSiteClass;
                    ComboSiteClas.ValueField = "PARVALUE";
                    ComboSiteClas.ValueType = typeof(string);
                    ComboSiteClas.TextField = "PARDESCRIPTION";
                    ComboSiteClas.DataBind();

                    SiteMaster siteMaster = new SiteMaster();

                    siteMaster = CMSfunction.GetSiteFromSiteCode(Session["Site"].ToString());
                    if (siteMaster.Enable == 1 )
                    {
                        SiteEnable.Checked = true;
                    }
                    else
                    {
                        SiteEnable.Checked = false;
                    }
                    TextBoxSite.Text = siteMaster.Site;
                    TextBoxSiteName.Text = siteMaster.SiteName;
                    ComboSiteClas.SelectedItem = ComboSiteClas.Items.FindByValue(siteMaster.SiteClass.ToString());

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
            Session.Remove("Site");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("SiteMasterManagementHeader.aspx");
            else

                Response.Redirect("SiteMasterManagementHeader.aspx");
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            processUpdate();
            
            LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            LabelMessage.Text = message.Message;

            LabelMessage.Visible = true;
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            processUpdate();
            if (message.Code == -1)
            {
                Response.Redirect("SiteMasterManagementHeader.aspx");
            }
        }

        private void processUpdate()
        {
            License license = new License();
            int ValidSiteCount = Int32.Parse(CMSfunction.GetValidSiteCount());

            String LicenseText = CMSfunction.GetLicense();
            LicenseText = CMSfunction.Decrypt(LicenseText);

            license = CMSfunction.ParseLicenseText(LicenseText);
            
            SiteMaster siteMaster = new SiteMaster();

            

            siteMaster = CMSfunction.GetSiteFromSiteCode(Session["Site"].ToString());


            
            if (SiteEnable.Checked == true)
            {
                siteMaster.Enable = 1;
                if (ValidSiteCount >= Int32.Parse(license.StoreTotal))
                {
                    message.Code = -1;
                    message.Message = "Store Total Exceeded License Data";
                    siteMaster.Enable = 0;
                    string script = "alert('License Have Maximal Site, Please Update License For More Site ');";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
                }
                else
                {
                    siteMaster.Enable = 1;
                    UpdateSite(siteMaster);
                }
            }
            else
            {
                siteMaster.Enable = 0;
                UpdateSite(siteMaster);
            }

            

        }

        private void UpdateSite(SiteMaster siteMaster)
        {
            siteMaster.Site = !string.IsNullOrWhiteSpace(TextBoxSite.Text) ? TextBoxSite.Text : "";
            siteMaster.SiteName = !string.IsNullOrWhiteSpace(TextBoxSiteName.Text) ? TextBoxSiteName.Text : "";

            siteMaster.SiteClass = ComboSiteClas.Value != null
                ? (int?)Int32.Parse(ComboSiteClas.Value.ToString())
                : null;


            message = CMSfunction.updateSiteMaster(siteMaster, Session["UserID"].ToString());

        }

    }
}