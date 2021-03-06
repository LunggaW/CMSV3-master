﻿using System;
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


namespace KBS.KBS.CMSV3.MasterData.PriceMasterManagement
{
    public partial class PriceMasterManagementNew : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTPrice = new DataTable();
        OutputMessage message = new OutputMessage();

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
                if (Session["SearchVariantforUpdate"] == null)
                {
                    Session["SearchVariantforUpdate"] = "";
                }
                if (Session["SearchItemIDforUpdate"] == null)
                {
                    Session["SearchItemIDforUpdate"] = "";
                }
                if (Session["SearchItemIIDforUpdate"] == null)
                {
                    Session["SearchItemIIDforUpdate"] = "";
                }
                if (Session["SearchVariantIDforUpdate"] == null)
                {
                    Session["SearchVariantIDforUpdate"] = "";
                }
                ITEMIDTXT.Text = Session["SearchItemIDforUpdate"].ToString();
                ITEMIDX.Text = Session["SearchItemIIDforUpdate"].ToString();              
                DTPrice = CMSfunction.GetVariant(ITEMIDX.Text);
                VARIANTBOX.DataSource = DTPrice;
                VARIANTBOX.ValueField = "VALUE";
                VARIANTBOX.ValueType = typeof(string);
                VARIANTBOX.TextField = "DESCRIPTION";
                VARIANTBOX.DataBind();
                VARIANTBOX.Text = Session["SearchVariantforUpdate"].ToString();
                VARIANTBOX.Value = Session["SearchVariantIDforUpdate"].ToString();

            }
            DTPrice = CMSfunction.GetSITEBox();
            SITEBOX.DataSource = DTPrice;
            SITEBOX.ValueField = "VALUE";
            SITEBOX.ValueType = typeof(string);
            SITEBOX.TextField = "DESCRIPTION";
            SITEBOX.DataBind();

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



        protected void ClearBtn_Click(object sender, EventArgs e)
        {

            ITEMIDTXT.Text = "";
            ITEMIDX.Text = "";
            VARIANTBOX.SelectedIndex = -1;
            SITEBOX.Text = "";
            PRICETXT.Text = "";
            VATBOX.Checked = false;
            EDATE.Value = "";
            SDATE.Value = "";
            EDATE.Text = "";
            SDATE.Text = "";
     
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            //Session.Remove("ParamHeaderIDforUpdate");
            Session.Remove("SearchVariantIDforUpdate");
            Session.Remove("SearchVariantforUpdate");
            Session.Remove("SearchItemIIDforUpdate");
            Session.Remove("SearchItemIDforUpdate");
            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("PriceMasterManagementHeader.aspx");
            else

                Response.Redirect("PriceMasterManagementHeader.aspx");
            //Session.Remove("ParamHeaderID");
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            ProcessInsert();

            ASPxLabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            ASPxLabelMessage.Visible = true;
            ASPxLabelMessage.Text = message.Message;
        }

        protected void Search(object sender, EventArgs e)
        {
            Session["SearchItemIDforUpdate"] = ITEMIDTXT.Text;
            Session["SearchItemIIDforUpdate"] = ITEMIDX.Text;            
            Session["SearchVariantforUpdate"] = VARIANTBOX.Text;
            if (VARIANTBOX.Text != "")
            {
                Session["SearchVariantIDforUpdate"] = VARIANTBOX.Value.ToString();
            }
            Session["SearchRedirect"] = "PriceMasterManagementNew.aspx";
            Response.Redirect("SearchItemMasterManagement.aspx");
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            ProcessInsert();
            Response.Redirect("PriceMasterManagementHeader.aspx");
        }

        private void ProcessInsert()
        {
            Session.Remove("SearchVariantIDforUpdate");
            Session.Remove("SearchVariantforUpdate");
            Session.Remove("SearchItemIIDforUpdate");
            Session.Remove("SearchItemIDforUpdate");
            PriceGroup pricegroup = new PriceGroup();
           
            pricegroup.ItemID = ITEMIDX.Text;
            pricegroup.VariantID = VARIANTBOX.Value.ToString();
            pricegroup.Site = SITEBOX.Value.ToString();
            pricegroup.Price = PRICETXT.Text;
            pricegroup.VAT = Convert.ToInt32(VATBOX.Checked).ToString(); 
            pricegroup.Edate = EDATE.Date != DateTime.MinValue ? (DateTime?)EDATE.Date : null;
            pricegroup.SDate = SDATE.Date != DateTime.MinValue ? (DateTime?)SDATE.Date : null;
            message = CMSfunction.InsertPriceGroup(pricegroup, Session["UserID"].ToString());

        }
        

        protected void ITEMIDTXT_TextChanged(object sender, EventArgs e)
        {
            VARIANTBOX.SelectedIndex = -1;
            ITEMIDX.Text = "";
            ITEMIDX.Text = CMSfunction.getinternalitemid(ITEMIDTXT.Text);
            DTPrice = CMSfunction.GetVariant(ITEMIDX.Text);
            VARIANTBOX.DataSource = DTPrice;
            VARIANTBOX.ValueField = "VALUE";
            VARIANTBOX.ValueType = typeof(string);
            VARIANTBOX.TextField = "DESCRIPTION";
            VARIANTBOX.DataBind();
        }
    }
}