using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.Administration.MenuMasterManagement
{
    public partial class MenuMasterDetail : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        OutputMessage message = new OutputMessage();
        private Menu menu;

        protected override void OnInit(EventArgs e)
        {
            LabelMessage.Visible = false;
            //user = CMSfunction.SelectUserDataFromUserID(User.Identity.Name);
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["MenuID"] == null)
            
            {
                Response.Redirect("MenuMaster.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    loadNavBar();

                    menu = new Menu();

                    menu = CMSfunction.GetMenuFromMenuID(Session["MenuID"].ToString());


                    menu.MenuID = Session["MenuID"].ToString();

                    TextBoxMenuID.Text = menu.MenuID;
                    TextBoxMenuName.Text = menu.MenuName;
                    TextBoxMenuGroupID.Text = menu.MenuGroupID;
                    TextBoxMenuGroupName.Text = menu.MenuGroupName;
                    ASPxMemoURL.Text = menu.MenuURL;
                    

                }
            }


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


        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            //ParameterDetail parDetail = new ParameterDetail();

            //parHeader.Lock = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderBlock.Text) ? ASPxTextBoxHeaderBlock.Text : "";
            //parHeader.Comment = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderComment.Text) ? ASPxTextBoxHeaderComment.Text : "";
            //parHeader.Copy = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderCopy.Text) ? ASPxTextBoxHeaderCopy.Text : "";
            //parHeader.ID = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderID.Text) ? ASPxTextBoxHeaderID.Text : "";
            //parHeader.Name = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderName.Text) ? ASPxTextBoxHeaderName.Text : "";

            //DTParameterHeader = CMSfunction.GetParameterHeaderData(parHeader);
            //ASPxGridViewDetail.DataSource = DTParameterHeader;
            //ASPxGridViewDetail.KeyFieldName = "ID"; ASPxGridViewHeader.DataBind();
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
                ASPxWebControl.RedirectOnCallback("MenuMaster.aspx");
            else

                Response.Redirect("MenuMaster.aspx");
            //Session.Remove("ParamHeaderID");
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            processUpdate();
            
            LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            LabelMessage.Text = message.Message;

            LabelMessage.Visible = true;
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            processUpdate();
            Response.Redirect("MenuMaster.aspx");
        }

        private void processUpdate()
        {
            menu = new Menu();

            menu.MenuID = !string.IsNullOrWhiteSpace(TextBoxMenuID.Text) ? TextBoxMenuID.Text : "";
            menu.MenuName = !string.IsNullOrWhiteSpace(TextBoxMenuName.Text) ? TextBoxMenuName.Text : "";
            menu.MenuGroupID = !string.IsNullOrWhiteSpace(TextBoxMenuGroupID.Text) ? TextBoxMenuGroupID.Text : "";
            menu.MenuGroupName = !string.IsNullOrWhiteSpace(TextBoxMenuGroupName.Text) ? TextBoxMenuGroupName.Text : "";
            menu.MenuURL = !string.IsNullOrWhiteSpace(ASPxMemoURL.Text) ? ASPxMemoURL.Text : "";


            message = CMSfunction.updateMenu(menu, Session["UserID"].ToString());

        }

    }
}