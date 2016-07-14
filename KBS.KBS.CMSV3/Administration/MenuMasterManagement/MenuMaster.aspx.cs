using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using DevExpress.Office.NumberConverters;
using DevExpress.Web;
using DevExpress.XtraSpreadsheet.Layout.Engine;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.Administration.MenuMasterManagement
{
    public partial class MenuMaster : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTGridViewMenu = new DataTable();
        private User user;
        private String MenuID = ConfigurationManager.AppSettings["MenuIdMenuMasterManagement"];

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/logins.aspx");
            }
            else
            {
                loadNavBar();
                loadButton(MenuID);
                //if (!IsPostBack)
                //{
                //    loadNavBar();
                //}
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
                        //Ed.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        if (accessContainer.Type == "0")
                        {
                            ASPxGridViewMenu.ClientSideEvents.RowDblClick = null;
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
            RefreshDataGrid();
        }


        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewMenu.FocusedRowIndex != -1)
            {
                String MenuID = ASPxGridViewMenu.GetRowValues(ASPxGridViewMenu.FocusedRowIndex, "ID").ToString();
                

                OutputMessage message = new OutputMessage();

                message = CMSfunction.DeleteMenu(MenuID, Session["UserID"].ToString());

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;

                RefreshDataGrid();
            }
        }

        protected void ASPxGridViewMenu_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            Session["MenuID"] = ASPxGridViewMenu.GetRowValues(Convert.ToInt32(e.Parameters), "ID").ToString();


            Session["MenuMasterFilter"] = "True";

            Session["MenuMasterGrpIDFilter"] = !string.IsNullOrWhiteSpace(TextBoxMenuGroupID.Text) ? TextBoxMenuGroupID.Text : "";
            Session["MenuMasterGrpNameFilter"] = !string.IsNullOrWhiteSpace(TextBoxMenuGroupName.Text) ? TextBoxMenuGroupName.Text : "";
            Session["MenuMasterMenuIDFilter"] = !string.IsNullOrWhiteSpace(TextBoxMenuID.Text) ? TextBoxMenuID.Text : "";
            Session["MenuMasterMenuNameFilter"] = !string.IsNullOrWhiteSpace(TextBoxMenuName.Text) ? TextBoxMenuName.Text : "";
            Session["MenuMasterMemoURLFilter"] = !string.IsNullOrWhiteSpace(ASPxMemoURL.Text) ? ASPxMemoURL.Text : "";



            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("MenuMasterDetail.aspx");
            else
                Response.Redirect("MenuMasterDetail.aspx");
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuMasterNew.aspx");
        }


        #region Grid Navigation Button
        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewMenu.PageIndex <= ASPxGridViewMenu.PageCount - 1)
            {
                ASPxGridViewMenu.PageIndex = ASPxGridViewMenu.PageIndex + 1;
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewMenu.PageIndex - 1 >= 0)
            {
                ASPxGridViewMenu.PageIndex = ASPxGridViewMenu.PageIndex - 1;
            }
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewMenu.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewMenu.PageIndex = ASPxGridViewMenu.PageCount - 1;
        }
        #endregion


        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            TextBoxMenuID.Text = "";
            TextBoxMenuName.Text = "";
            TextBoxMenuGroupID.Text = "";
            TextBoxMenuGroupName.Text = "";
            ASPxMemoURL.Text = "";
        }



        private void RefreshDataGrid()
        {
            Menu menu = new Menu();

            if (Session["MenuMasterFilter"] != null)
            {
                menu.MenuID = !string.IsNullOrWhiteSpace(Session["MenuMasterMenuIDFilter"].ToString())
                    ? Session["MenuMasterMenuIDFilter"].ToString()
                    : "";
                menu.MenuName = !string.IsNullOrWhiteSpace(Session["MenuMasterMenuNameFilter"].ToString())
                    ? Session["MenuMasterMenuNameFilter"].ToString()
                    : "";
                menu.MenuGroupID = !string.IsNullOrWhiteSpace(Session["MenuMasterGrpIDFilter"].ToString())
                    ? Session["MenuMasterGrpIDFilter"].ToString()
                    : "";
                menu.MenuGroupName = !string.IsNullOrWhiteSpace(Session["MenuMasterGrpNameFilter"].ToString())
                    ? Session["MenuMasterGrpNameFilter"].ToString()
                    : "";
                menu.MenuURL = !string.IsNullOrWhiteSpace(Session["MenuMasterMemoURLFilter"].ToString())
                    ? Session["MenuMasterMemoURLFilter"].ToString()
                    : "";

                if (!string.IsNullOrWhiteSpace(menu.MenuID))
                {
                    TextBoxMenuID.Text = menu.MenuID;
                }

                if (!string.IsNullOrWhiteSpace(menu.MenuName))
                {
                    TextBoxMenuName.Text = menu.MenuName;
                }

                if (!string.IsNullOrWhiteSpace(menu.MenuName))
                {
                    TextBoxMenuName.Text = menu.MenuName;
                }

                if (!string.IsNullOrWhiteSpace(menu.MenuName))
                {
                    TextBoxMenuName.Text = menu.MenuName;
                }

                if (!string.IsNullOrWhiteSpace(menu.MenuName))
                {
                    TextBoxMenuName.Text = menu.MenuName;
                }

                Session.Remove("MenuMasterFilter");
            }
            else
            {
                menu.MenuID = !string.IsNullOrWhiteSpace(TextBoxMenuID.Text) ? TextBoxMenuID.Text : "";
                menu.MenuName = !string.IsNullOrWhiteSpace(TextBoxMenuName.Text) ? TextBoxMenuName.Text : "";
                menu.MenuGroupID = !string.IsNullOrWhiteSpace(TextBoxMenuGroupID.Text) ? TextBoxMenuGroupID.Text : "";
                menu.MenuGroupName = !string.IsNullOrWhiteSpace(TextBoxMenuGroupName.Text)
                    ? TextBoxMenuGroupName.Text
                    : "";
                menu.MenuURL = !string.IsNullOrWhiteSpace(ASPxMemoURL.Text) ? ASPxMemoURL.Text : "";
            }



            DTGridViewMenu = CMSfunction.GetAllMenuFiltered(menu);

            ASPxGridViewMenu.DataSource = DTGridViewMenu;
            ASPxGridViewMenu.KeyFieldName = "ID";
            ASPxGridViewMenu.DataBind();
        }

    }
}