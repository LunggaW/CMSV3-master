using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.MasterData
{
    public partial class SearchItemMasterManagement : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSearch = new DataTable();
        private DataTable DTSearchItem = new DataTable();
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
            RefreshDataGrid();
            if (!IsPostBack)
            {
                
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

        protected void ASPxGridViewHeader_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            Session["SearchItemIDforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ITEM ID").ToString();
            Session["SearchVariantforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "VARIANT").ToString();
            Session["SearchItemIIDforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ITEMID").ToString();
            Session["SearchVariantIDforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "VARIANTID").ToString();


            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback(Session["SearchRedirect"].ToString());
            else

                Response.Redirect(Session["SearchRedirect"].ToString());
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();

        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {

            TextBoxItemID.Text = "";
            TextBoxVariant.Text = "";           
            
        }


        private void RefreshDataGrid()
        {
            AssortmentMaster assortment = new AssortmentMaster();

            assortment.ItemID = !string.IsNullOrWhiteSpace(TextBoxItemID.Text) ? TextBoxItemID.Text : "";
            assortment.VariantID = !string.IsNullOrWhiteSpace(TextBoxVariant.Text) ? TextBoxVariant.Text : "";
           

            DTSearchItem = CMSfunction.GetItemVariant(assortment);

            ASPxGridViewHeader.DataSource = DTSearchItem;
            ASPxGridViewHeader.KeyFieldName = "ITEM ID";
            ASPxGridViewHeader.DataBind();
            ASPxGridViewHeader.Columns[0].Visible = false;
            ASPxGridViewHeader.Columns[3].Visible = false;
        }



    }

}