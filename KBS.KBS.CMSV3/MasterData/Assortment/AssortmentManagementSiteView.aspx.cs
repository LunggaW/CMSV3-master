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
using KBS.KBS.CMSV3.Administration;

namespace KBS.KBS.CMSV3.MasterData.Assortment
{
    public partial class AssortmentManagementSiteView : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSite = new DataTable();
        private DataTable DTSiteAssortment = new DataTable();
        private string ItemID;
        private string VariantID;
        private AssortmentMaster assortment;
        OutputMessage message = new OutputMessage();

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/logins.aspx");
            }
            else
            {
                loadNavBar();

                ASPxGridViewAssortment.Visible = false;

            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {


            ASPxGridViewAssortment.Visible = true;




            RefreshDataGrid();

            //String ItemID = CMSfunction.GetItemIDByItemIDEx(Session["ItemIDExManagementVariant"].ToString());
            //ASPxTextBoxItem.Text = Session["ItemIDExManagementVariant"].ToString() +" - "+CMSfunction.GetItemDescByItemID(ItemID);

            //RefreshDataGrid();

            //DTStatus = CMSfunction.GetParameterValueAndDescbyClassAndTabID(Session["Class"].ToString(), "5");
            //ComboStatus.DataSource = DTStatus;
            //ComboStatus.ValueField = "PARVALUE";
            //ComboStatus.ValueType = typeof(string);
            //ComboStatus.TextField = "PARDESCRIPTION";
            //ComboStatus.DataBind();

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


        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("ItemSearch.aspx");
            else
                Response.Redirect("ItemSearch.aspx");



        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {



        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewAssortment.FocusedRowIndex != -1)
            {
                assortment = new AssortmentMaster();



                string dataitem = ItemID;
                string datavariant = VariantID;
                string datasite = ASPxGridViewAssortment.GetRowValues(ASPxGridViewAssortment.FocusedRowIndex, "SITE").ToString();



                message = CMSfunction.deleteAssortment(assortment);


                LabelMessage.Text = message.Message;
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            SiteMaster siteMaster = new SiteMaster();

            siteMaster.Site = !string.IsNullOrWhiteSpace(TextBoxSite.Text) ? TextBoxSite.Text : "";
            siteMaster.SiteName = !string.IsNullOrWhiteSpace(TextBoxSiteName.Text) ? TextBoxSiteName.Text : "";

            DTSiteAssortment = CMSfunction.GetAllStoreAssortment(Session["SiteProfile"].ToString(), siteMaster);

            ASPxGridViewAssortment.DataSource = DTSiteAssortment;
            ASPxGridViewAssortment.KeyFieldName = "SITE";
            ASPxGridViewAssortment.DataBind();
        }

        #region Grid Navigation Button
        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewAssortment.PageIndex <= ASPxGridViewAssortment.PageCount - 1)
            {
                ASPxGridViewAssortment.PageIndex = ASPxGridViewAssortment.PageIndex + 1;
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewAssortment.PageIndex - 1 >= 0)
            {
                ASPxGridViewAssortment.PageIndex = ASPxGridViewAssortment.PageIndex - 1;
            }
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewAssortment.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewAssortment.PageIndex = ASPxGridViewAssortment.PageCount - 1;
        }
        #endregion

        protected void ASPxGridViewAssortment_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            Session["SiteAssortment"] = ASPxGridViewAssortment.GetRowValues(Convert.ToInt32(e.Parameters), "SITE").ToString();
            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("AssortmentManagementDetailView.aspx");
            else
                Response.Redirect("AssortmentManagementDetailView.aspx");
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewAssortment.FocusedRowIndex != -1)
            {
                Session["SiteAssortment"] =
                    ASPxGridViewAssortment.GetRowValues(ASPxGridViewAssortment.FocusedRowIndex, "SITE").ToString();
                if (Page.IsCallback)
                    ASPxWebControl.RedirectOnCallback("AssortmentManagementDetailView.aspx");
                else
                    Response.Redirect("AssortmentManagementDetailView.aspx");
            }
        }
    }
}