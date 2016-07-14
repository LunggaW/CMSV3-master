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

namespace KBS.KBS.CMSV3.Administration
{
    public partial class ParameterManagement : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTParameterHeader= new DataTable();
        private DataTable DTSite = new DataTable();
        private DataTable DTGridViewUser = new DataTable();
        private Member user;

        protected override void OnInit(EventArgs e)
        {
            //user = CMSfunction.SelectUserDataFromUserID(User.Identity.Name);
            //loadNavBar();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //user = CMSfunction.SelectUserDataFromUserID(User.Identity.Name);

            DTParameterHeader = CMSfunction.GetParameterHeaderData();
            ASPxGridViewHeader.DataSource = DTParameterHeader;
            ASPxGridViewHeader.KeyFieldName = "ID";
            ASPxGridViewHeader.DataBind();

            //ASPxComboBoxUserManagementSite.DataSource = DTSite;
            //ASPxComboBoxUserManagementSite.ValueField = "SITECODE";
            //ASPxComboBoxUserManagementSite.ValueType = typeof(string);
            //ASPxComboBoxUserManagementSite.TextField = "SITENAME";
            //ASPxComboBoxUserManagementSite.DataBind();

            //DTProfile = CMSfunction.GetAllProfile();
            //ASPxComboBoxUserManagementProfile.DataSource = DTProfile;
            //ASPxComboBoxUserManagementProfile.ValueField = "PROFILEID";
            //ASPxComboBoxUserManagementProfile.ValueType = typeof(string);
            //ASPxComboBoxUserManagementProfile.TextField = "PROFILENAME";
            //ASPxComboBoxUserManagementProfile.DataBind();

            //DTGridViewUser = CMSfunction.GetAllUser();
            //ASPxGridViewUserManagementUser.DataSource = DTGridViewUser;
            //ASPxGridViewUserManagementUser.KeyFieldName = "USERID";
            //ASPxGridViewUserManagementUser.DataBind();
            //loadNavBar();


        }


        private void loadNavBar()
        {

            List<Menu> listMenuGroup = CMSfunction.SelectMenuGroupByProfileID(user.MenuProfile);
            int i = 0;

            ASPxSplitter sp = Master.Master.FindControl("ASPxSplitter1").FindControl("Content").FindControl("ContentSplitter") as ASPxSplitter;
            ASPxNavBar masterNav = sp.FindControl("ASPxNavBar1") as ASPxNavBar;

            if (masterNav != null)
            {
                foreach (var menuGroup in listMenuGroup)
                {

                    masterNav.Groups.Add(menuGroup.MenuGroupName, menuGroup.MenuGroupNameID);


                    List<Menu> listMenu = CMSfunction.SelectMenuByProfileIDandMenuGroup(user.MenuProfile,
                        menuGroup.MenuGroupNameID);
                    foreach (var menuItem in listMenu)
                    {
                        masterNav.Groups[i].Items.Add(menuItem.MenuName, menuItem.MenuNameID, null, menuItem.MenuGroupURL);
                        masterNav.Groups[i].HeaderStyle.BackColor = ColorTranslator.FromHtml("#6076B4");
                        masterNav.Groups[i].HeaderStyle.BorderColor = ColorTranslator.FromHtml("#6076B4");
                    }
                    i++;

                }
            }
            masterNav.DataBind();

        }



        protected void ASPxGridViewHeader_FocusedRowChanged(object sender, EventArgs e)
        {
           

            
        }

        protected void ASPxButtonRefreshTextbox_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                ASPxTextBoxHeaderID.Text =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
                ASPxTextBoxHeaderName.Text =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "NAME").ToString();
                ASPxTextBoxHeaderSClas.Text =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SCLAS").ToString();
                ASPxTextBoxHeaderCopy.Text =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "COPY").ToString();
                ASPxTextBoxHeaderComment.Text =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "COMMENT").ToString();
                ASPxTextBoxHeaderBlock.Text =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "BLOCK").ToString();

                //StringBuilder sbScript = new StringBuilder();

                //sbScript.Append("<script language='JavaScript' type='text/javascript'>\n");
                //sbScript.Append("<!--\n");
                //sbScript.Append(this.GetPostBackEventReference(this, "PBArg") + ";\n");
                //sbScript.Append("// -->\n");
                //sbScript.Append("</script>\n");

                //this.RegisterStartupScript("AutoPostBackScript", sbScript.ToString());
            }
        }

        protected void ASPxButtonInsert_Click(object sender, EventArgs e)
        {
            ParameterHeader parHeader = new ParameterHeader();
            parHeader.ID = Int32.Parse(ASPxTextBoxHeaderID.Text);
            parHeader.Name = ASPxTextBoxHeaderName.Text;
            parHeader.Block = Int32.Parse(ASPxTextBoxHeaderBlock.Text);
            parHeader.Comment = ASPxTextBoxHeaderComment.Text;
            parHeader.Copy = Int32.Parse(ASPxTextBoxHeaderCopy.Text);
            parHeader.SClass = Int32.Parse(ASPxTextBoxHeaderSClas.Text);
            OutputMessage outputMsg = new OutputMessage();
            outputMsg = CMSfunction.insertParameterHeader(parHeader, "TestUser");
        }





        

    }
}