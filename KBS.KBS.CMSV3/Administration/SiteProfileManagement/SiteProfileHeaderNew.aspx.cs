﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;

namespace KBS.KBS.CMSV3.Administration.SiteProfileManagement
{
    public partial class SiteProfileHeaderNew : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSiteProfile = new DataTable();
        
        private OutputMessage message = new OutputMessage();

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/logins.aspx");
            }
            else
            {
                loadNavBar();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
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
        }


        protected void DelBtn_Click(object sender, EventArgs e)
        {
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            ProcessInsert();

            LabelMessage.Visible = true;
            LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            LabelMessage.Text = message.Message;
        }



        private void ProcessInsert()
        {
            DATAMODEL.SiteProfileHeader siteProfile = new DATAMODEL.SiteProfileHeader();

            siteProfile.Profile = TextBoxSiteProfile.Text;
            siteProfile.Description = TextBoxSiteProfileDescription.Text;

            message = CMSfunction.insertSiteProfile(siteProfile, Session["UserID"].ToString());
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            ProcessInsert();
            Response.Redirect("SiteProfileHeader.aspx");
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("SiteProfileHeader.aspx");
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            
        }


    }
}