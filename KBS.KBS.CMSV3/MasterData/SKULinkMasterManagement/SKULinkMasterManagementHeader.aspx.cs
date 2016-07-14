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
    public partial class SKULinkMasterManagementHeader : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSKULink = new DataTable();
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
            
                SKULink skulink = new SKULink();
                DTSKULink = CMSfunction.GetSKULinkHeaderDataTable(skulink);

                ASPxGridViewHeader.DataSource = DTSKULink;
                ASPxGridViewHeader.KeyFieldName = "SKU ID";
                ASPxGridViewHeader.DataBind();
            
            //SearchBtn_Click();
        }

        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.PageIndex <= ASPxGridViewHeader.PageCount - 1)
            {
                ASPxGridViewHeader.PageIndex = ASPxGridViewHeader.PageIndex + 1;
            }
        }
        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.PageIndex -1 >= 0)
            {
                ASPxGridViewHeader.PageIndex = ASPxGridViewHeader.PageIndex - 1;
            }
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewHeader.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewHeader.PageIndex = ASPxGridViewHeader.PageCount - 1;
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

        protected void ASPxGridViewHeader_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            Session["SKULinkSKUforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SKU ID").ToString();
            Session["SKULinkSiteforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SITE ID").ToString();
            Session["SKULinkBrandforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "BRAND ID").ToString();
            Session["SKULinkSDateforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "START DATE").ToString();
            Session["SKULinkEDateforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "END DATE").ToString();
            

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("SKULinkMasterManagementEdit.aspx");
            else

                Response.Redirect("SKULinkMasterManagementEdit.aspx");
        }
        private void SearchBtn_Click()
        {

            //PriceGroup pricegroup = new PriceGroup();


            //pricegroup.ItemID = !string.IsNullOrWhiteSpace(ITEMIDTXT.Text) ? ITEMIDTXT.Text : "";
            //pricegroup.VariantID = !string.IsNullOrWhiteSpace(VARIANTTXT.Text) ? VARIANTTXT.Text : "";
            //pricegroup.Site = !string.IsNullOrWhiteSpace(SITETXT.Text) ? SITETXT.Text : "";
            //pricegroup.Price = !string.IsNullOrWhiteSpace(PRICETXT.Text) ? PRICETXT.Text : "";
            //pricegroup.VAT = !string.IsNullOrWhiteSpace(VATTXT.Text) ? VATTXT.Text : "";
            //pricegroup.Edate = EDATE.Date != DateTime.MinValue ? (DateTime?)EDATE.Date : null;
            //pricegroup.SDate = SDATE.Date != DateTime.MinValue ? (DateTime?)SDATE.Date : null;

            //DTSKULink = CMSfunction.GetPriceHeaderDataTable(pricegroup);
            //ASPxGridViewHeader.DataSource = DTSKULink;
            //ASPxGridViewHeader.KeyFieldName = "ITEM ID";
            //ASPxGridViewHeader.DataBind();


        }
        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            SearchBtn_Click();
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            
            //ITEMIDTXT.Text = ""; 
            //VARIANTTXT.Text = "";
            //SITETXT.Text = "";
            //PRICETXT.Text = "";
            //VATTXT.Text = "";
            //EDATE.Value = "";
            //SDATE.Value = "";
            //EDATE.Text = "";
            //SDATE.Text = "";
            
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

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("SKULinkMasterManagementNew.aspx");
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                SKULink skulink = new SKULink();
                skulink.SKU = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SKU ID").ToString();
                skulink.SITE = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SITE ID").ToString();
                skulink.BRAND  = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "BRAND ID").ToString();
                
                skulink = CMSfunction.Cekdataskulink(skulink);
                if (skulink.BRAND == "NO")
                {

                    String alert = "Data Cannot Delete Because Already Use";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(alert);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                }
                else
                {
                    skulink = new SKULink();
                    skulink.SKU = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SKU ID").ToString();
                    skulink.SITE = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SITE ID").ToString();
                    skulink.BRAND = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "BRAND ID").ToString();
                    skulink.SDate = DateTime.Parse(ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "START DATE").ToString());
                    skulink.EDate = DateTime.Parse(ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "END DATE").ToString());

                    OutputMessage message = new OutputMessage();

                    message = CMSfunction.deleteSKULinkHeader(skulink);

                    LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                    LabelMessage.Visible = true;
                    LabelMessage.Text = message.Message;

                    Response.Redirect("SKULinkMasterManagementHeader.aspx");
                }
            }
            
        }



    }

}