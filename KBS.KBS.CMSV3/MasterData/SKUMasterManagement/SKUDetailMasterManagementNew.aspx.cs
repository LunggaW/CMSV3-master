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


namespace KBS.KBS.CMSV3.MasterData.SKUMasterManagement
{
    public partial class SKUDetailMasterManagementNew : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSKUDetail = new DataTable();
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
            String IDGRP = Session["SKUIDforUpdate"].ToString();
            DTSKUDetail = CMSfunction.GetNameBox(Session["Class"].ToString(), "17", IDGRP);
            NAMEBOX.DataSource = DTSKUDetail;
            NAMEBOX.ValueField = "PARVALUE";
            NAMEBOX.ValueType = typeof(string);
            NAMEBOX.TextField = "PARDESCRIPTION";
            NAMEBOX.DataBind();

            DTSKUDetail = CMSfunction.GetTypeBox(Session["Class"].ToString(), "7");
            TYPEBOX.DataSource = DTSKUDetail;
            TYPEBOX.ValueField = "PARVALUE";
            TYPEBOX.ValueType = typeof(string);
            TYPEBOX.TextField = "PARDESCRIPTION";
            TYPEBOX.DataBind();

            DTSKUDetail = CMSfunction.BaseBox(IDGRP);
            BASEBOX.DataSource = DTSKUDetail;
            BASEBOX.ValueField = "BASE";
            BASEBOX.ValueType = typeof(string);
            BASEBOX.TextField = "BASE";
            BASEBOX.DataBind();
            if (!IsPostBack)
            {

                VALUETXT.Text = "0";
                PARTTXT.Text = "0";


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



        protected void ClearBtn_Click(object sender, EventArgs e)
        {

            //ASPxTextBoxSClass.Text = "";
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            //Session.Remove("ParamHeaderIDforUpdate");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("SKUDetailMasterManagement.aspx");
            else

                Response.Redirect("SKUDetailMasterManagement.aspx");
            //Session.Remove("ParamHeaderID");
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            ProcessInsert();

            ASPxLabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            ASPxLabelMessage.Visible = true;
            ASPxLabelMessage.Text = message.Message;
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            ProcessInsert();
            Response.Redirect("SKUDetailMasterManagement.aspx");
        }

        private void ProcessInsert()
        {
            if (VALUETXT.Text == "")
            {
                VALUETXT.Text = "0";

            }
            if (PARTTXT.Text == "")
            {

                PARTTXT.Text = "0";
            }

            if ((IDTXT.Text == "") || (EXIDTXT.Text == "") || (NAMEBOX.Text == "") || (VALUETXT.Text == "") || (PARTTXT.Text == "") || (BASEBOX.Text == "") || (TYPEBOX.Value.ToString() == ""))
            {
                string script = "alert('Please Fill All Field');";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
            }
            else
            {
                SKUGroupDetail skugroupdetail = new SKUGroupDetail();
                skugroupdetail.IDGRP = Session["SKUIDforUpdate"].ToString();
                skugroupdetail.ID = IDTXT.Text;
                skugroupdetail.EXID = EXIDTXT.Text;
                skugroupdetail.NAME = NAMEBOX.Value.ToString();
                skugroupdetail.VALUE = VALUETXT.Text;
                skugroupdetail.PARTISIPASI = PARTTXT.Text;
                skugroupdetail.BASEON = BASEBOX.Text;
                skugroupdetail.TYPE = TYPEBOX.Value.ToString();


                message = CMSfunction.InsertSKUDetail(skugroupdetail, Session["UserID"].ToString());
            }
        }

        protected void NAMEBOX_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (NAMEBOX.Text == "VAT")
            {
                string Results = CMSfunction.CekVATParam("VAT");
                VALUETXT.Text = Results;
            }
        }
    }
}