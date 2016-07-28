using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;

namespace KBS.KBS.CMSV3.Administration.MenuManagement
{
    public partial class MenuProfileLinkManagement : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTMenuProfileLink = new DataTable();
        private DataTable DTTable = new DataTable();
        
        private String MenuID = ConfigurationManager.AppSettings["MenuIdMenuProfileManagement"];

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/logins.aspx");
            }
            else if (Session["MenuProfileManagement"] == null)
            {
                Response.Redirect("MenuProfileHeader.aspx");
            }
            else
            {
                loadNavBar();
                //loadButton(MenuID);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();
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

        private void loadButton(String MenuID)
        {

            List<AccessContainer> listAccessCont =
                CMSfunction.SelectAccessByProfileAndMenuID(Session["AccessProfile"].ToString(), MenuID);

            foreach (var accessContainer in listAccessCont)
            {
                switch (accessContainer.FunctionId)
                {
                    case "1":
                        AddBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        break;
                    case "2":
                        EditBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        
                        if (accessContainer.Type == "0")
                        {
                            ASPxGridViewMenuProfileLink.ClientSideEvents.RowDblClick = null;
                        }

                        break;
                    case "3":
                        SearchBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        break;
                    case "4":
                        DelBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        break;
                    default:
                        break;

                }
            }
        }


        protected void SearchBtn_Click(object sender, EventArgs e)
        {
        }


        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewMenuProfileLink.FocusedRowIndex != -1)
            {
                MenuProfileLink menuProfileLink = new MenuProfileLink();

                menuProfileLink.MenuID = ASPxGridViewMenuProfileLink.GetRowValues(ASPxGridViewMenuProfileLink.FocusedRowIndex, "MENU ID").ToString();

                menuProfileLink.MenuProfile = Session["MenuProfileManagement"].ToString();

                OutputMessage message = new OutputMessage();

                message = CMSfunction.DeleteMenuProfileLink(menuProfileLink);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;

                RefreshDataGrid();
            }
        }

        protected void ASPxGridViewMenuProfileLink_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            DTTable = CMSfunction.GetMenuExcludeMenuProfile(Session["MenuProfileManagement"].ToString());

            if (DTTable.Rows.Count == 0)
            {
                LabelMessage.ForeColor = Color.Red;

                LabelMessage.Visible = true;
                LabelMessage.Text = "No More Menu to be added";

            }
            else
            {
                Response.Redirect("MenuProfileLinkManagementNew.aspx");
            }

        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {

        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuProfileHeader.aspx");
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {

        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {

        }


        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewMenuProfileLink.PageIndex <= ASPxGridViewMenuProfileLink.PageCount - 1)
            {
                ASPxGridViewMenuProfileLink.PageIndex = ASPxGridViewMenuProfileLink.PageIndex + 1;
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewMenuProfileLink.PageIndex - 1 > 0)
            {
                ASPxGridViewMenuProfileLink.PageIndex = ASPxGridViewMenuProfileLink.PageIndex - 1;
            }
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewMenuProfileLink.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewMenuProfileLink.PageIndex = ASPxGridViewMenuProfileLink.PageCount - 1;
        }

        private void RefreshDataGrid()
        {
            DTMenuProfileLink = CMSfunction.GetMenuProfileLinkDataTable(Session["MenuProfileManagement"].ToString());
            ASPxGridViewMenuProfileLink.DataSource = DTMenuProfileLink;
            ASPxGridViewMenuProfileLink.KeyFieldName = "MENU PROFILE";
            ASPxGridViewMenuProfileLink.DataBind();
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {

        }
    }
}