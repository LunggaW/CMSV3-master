using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.TransferOrder
{
    public partial class SearchItemMaster : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSearch = new DataTable();
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

            if (!IsPostBack)
            {
                SearchItemVariant search = new SearchItemVariant();

                DTSearch = CMSfunction.GetSearchHeaderDataTable(search);
                ASPxGridViewHeader.DataSource = DTSearch;
                ASPxGridViewHeader.KeyFieldName = "ITEM ID";
                ASPxGridViewHeader.DataBind();
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
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                Session["SearchItemIDforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ITEM ID").ToString();
                Session["SearchVariantforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "VARIANT ID").ToString();
                Session["SearchBarcodeforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "BARCODE").ToString();


                if (Page.IsCallback)
                    ASPxWebControl.RedirectOnCallback(Session["SearchRedirect"].ToString());
                else

                    Response.Redirect(Session["SearchRedirect"].ToString());
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            SearchItemVariant search = new SearchItemVariant();


            search.ITEMID = !string.IsNullOrWhiteSpace(ITEMIDTXT.Text) ? ITEMIDTXT.Text : "";
            search.VARIANTID = !string.IsNullOrWhiteSpace(VARIANTTXT.Text) ? VARIANTTXT.Text : "";
            search.SHORTDESC = !string.IsNullOrWhiteSpace(SHORTDESCTXT.Text) ? SHORTDESCTXT.Text : "";
            search.LONGDESC = !string.IsNullOrWhiteSpace(LONGDESCTXT.Text) ? LONGDESCTXT.Text : "";
            search.COLORGRP = !string.IsNullOrWhiteSpace(COLORGRPTXT.Text) ? COLORGRPTXT.Text : "";
            search.COLOR = !string.IsNullOrWhiteSpace(COLORTXT.Text) ? COLORTXT.Text : "";
            search.STYLEGRP = !string.IsNullOrWhiteSpace(STYLEGRPTXT.Text) ? STYLEGRPTXT.Text : "";
            search.STYLE = !string.IsNullOrWhiteSpace(STYLETXT.Text) ? STYLETXT.Text : "";
            search.SIZEGRP = !string.IsNullOrWhiteSpace(SIZEGRPTXT.Text) ? SIZEGRPTXT.Text : "";
            search.SIZE = !string.IsNullOrWhiteSpace(SIZETXT.Text) ? SIZETXT.Text : "";
            search.SIZE = !string.IsNullOrWhiteSpace(BARCODETXT.Text) ? BARCODETXT.Text : "";




            DTSearch = CMSfunction.GetSearchHeaderDataTable(search);
            ASPxGridViewHeader.DataSource = DTSearch;
            ASPxGridViewHeader.KeyFieldName = "ITEM ID";
            ASPxGridViewHeader.DataBind();


        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {

            ITEMIDTXT.Text = "";
            VARIANTTXT.Text = "";
            SHORTDESCTXT.Text = "";
            LONGDESCTXT.Text = "";
            COLORGRPTXT.Text = "";
            COLORTXT.Value = "";
            SIZEGRPTXT.Value = "";
            SIZETXT.Text = "";
            STYLEGRPTXT.Text = "";
            STYLETXT.Text = "";
            BARCODETXT.Text = "";
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                Session["SearchItemIDforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ITEM ID").ToString();
                Session["SearchVariantforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "VARIANT ID").ToString();
                Session["SearchBarcodeforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "BARCODE").ToString();


                if (Page.IsCallback)
                    ASPxWebControl.RedirectOnCallback(Session["SearchRedirect"].ToString());
                else

                    Response.Redirect(Session["SearchRedirect"].ToString());
            }
        }


        //protected void ASPxButtonEntry_Click(object sender, EventArgs e)
        //{
        //    if (ASPxGridViewHeader.FocusedRowIndex != -1)
        //    {

        //        Session["BrandIDforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
        //        Session["BrandDescforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "BRAND DESC").ToString();

        //        Response.Redirect("BrandDetailMasterManagement.aspx");
        //    }


        //}



    }

}