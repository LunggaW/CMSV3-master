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

namespace KBS.KBS.CMSV3.Administration
{
    public partial class ParameterManagementHeaderNew : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSiteClass = new DataTable();
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
            DTSiteClass = CMSfunction.GetParameterValueAndDescbyClassAndTabID(Session["Class"].ToString(), "3");
            ComboSiteClass.DataSource = DTSiteClass;
            ComboSiteClass.ValueField = "PARVALUE";
            ComboSiteClass.ValueType = typeof(string);
            ComboSiteClass.TextField = "PARDESCRIPTION";
            ComboSiteClass.DataBind();

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
            ASPxCopy.Checked = false;
            ASPxMemoComment.Text = "";
            ASPxTextBoxId.Text = "";
            ASPxTextLock.Checked = false;
            ASPxTextBoxName.Text = "";
            //ASPxTextBoxSClass.Text = "";
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            Session.Remove("ParamHeaderIDforUpdate");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("ParameterManagementHeader.aspx");
            else

                Response.Redirect("ParameterManagementHeader.aspx");
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
            if (ASPxTextBoxName.Text != "" && ComboSiteClass.Value.ToString() != "" && ASPxTextBoxId.Text != "" && ASPxMemoComment.Text != "")
            {
                Response.Redirect("ParameterManagementHeader.aspx");
            }
           
        }

        private void ProcessInsert()
        {
            if (ASPxTextBoxName.Text == "" || ComboSiteClass.Value.ToString() == "" ||ASPxTextBoxId.Text == "" || ASPxMemoComment.Text == "" )
            {
                
                String alert = "Mohon isi semua Field ";
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
            
                ParameterHeader parHeader = new ParameterHeader();
                parHeader.Comment = ASPxMemoComment.Text;
                parHeader.Copy = Convert.ToInt32(ASPxCopy.Checked).ToString();
                parHeader.ID = ASPxTextBoxId.Text;
                parHeader.Lock = Convert.ToInt32(ASPxTextLock.Checked).ToString();
                parHeader.Name = ASPxTextBoxName.Text;
                parHeader.SClass = ComboSiteClass.Value.ToString();

                message = CMSfunction.InsertParameterHeader(parHeader, Session["UserID"].ToString());
            }
        }

    }
}