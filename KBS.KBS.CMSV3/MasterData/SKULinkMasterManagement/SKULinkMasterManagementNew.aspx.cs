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


namespace KBS.KBS.CMSV3.MasterData.SKULinkMasterManagement
{
    public partial class SKULinkMasterManagementNew : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSKULink = new DataTable();
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
            DTSKULink = CMSfunction.GetSKUBox();
            SKUBOX.DataSource = DTSKULink;
            SKUBOX.ValueField = "VALUE";
            SKUBOX.ValueType = typeof(string);
            SKUBOX.TextField = "DESCRIPTION";
            SKUBOX.DataBind();
            DTSKULink = CMSfunction.GetBRANDBox();
            BRANDBOX.DataSource = DTSKULink;
            BRANDBOX.ValueField = "VALUE";
            BRANDBOX.ValueType = typeof(string);
            BRANDBOX.TextField = "DESCRIPTION";
            BRANDBOX.DataBind();
            DTSKULink = CMSfunction.GetSITEBox();
            SITEBOX.DataSource = DTSKULink;
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

            SKUBOX.Text = "";
            SITEBOX.Text = "";
            BRANDBOX.Text = "";            
            EDATE.Value = "";
            SDATE.Value = "";
            EDATE.Text = "";
            SDATE.Text = "";
     
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            //Session.Remove("ParamHeaderIDforUpdate");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("SKULinkMasterManagementHeader.aspx");
            else

                Response.Redirect("SKULinkMasterManagementHeader.aspx");
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
            Response.Redirect("SKULinkMasterManagementHeader.aspx");
        }

        private void ProcessInsert()
        {
            SKULink skulink = new SKULink();

            
            skulink.SITE = SITEBOX.Value.ToString();
            skulink.SKU = SKUBOX.Value.ToString();
            skulink.BRAND = BRANDBOX.Value.ToString();
            skulink.EDate = EDATE.Date != DateTime.MinValue ? (DateTime?)EDATE.Date : null;
            skulink.SDate = SDATE.Date != DateTime.MinValue ? (DateTime?)SDATE.Date : null;
            message = CMSfunction.InsertSKULinkGroup(skulink, Session["UserID"].ToString());

        }

    }
}