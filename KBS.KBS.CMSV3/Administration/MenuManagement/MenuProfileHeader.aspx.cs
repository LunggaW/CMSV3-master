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
    public partial class MenuProfileHeader : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTMenuProfile = new DataTable();
        private DATAMODEL.MenuProfileHeader menuProfile;
        private String MenuID = ConfigurationManager.AppSettings["MenuIdMenuProfileManagement"];


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
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string Compare = Session["Filter"].ToString();
            if (Compare != "")
            {
                if (Session["Filter"].ToString() != "MenuProfManagement")
                {
                    UserManagement.UserManagementHeader Sunda = new UserManagement.UserManagementHeader();
                    Sunda.ClearDataSeasson();
                }
            }

            RefreshDataGrid();
            
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
                            ASPxGridViewMenuProfile.ClientSideEvents.RowDblClick = null;
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
            if (ASPxGridViewMenuProfile.FocusedRowIndex != -1)
            {
                OutputMessage message = new OutputMessage();

                DATAMODEL.MenuProfileHeader menuProfile = new DATAMODEL.MenuProfileHeader();

                menuProfile.Profile =
                    ASPxGridViewMenuProfile.GetRowValues(ASPxGridViewMenuProfile.FocusedRowIndex, "MENU PROFILE")
                        .ToString();

                message = CMSfunction.DeleteMenuProfileHeader(menuProfile);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;

                RefreshDataGrid();
            }
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuProfileHeaderNew.aspx");
        }

        protected void ASPxButtonEntry_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewMenuProfile.FocusedRowIndex != -1)
            {
                Session["MenuProfileManagement"] = ASPxGridViewMenuProfile.GetRowValues(ASPxGridViewMenuProfile.FocusedRowIndex, "MENU PROFILE").ToString();
                Response.Redirect("MenuProfileLinkManagement.aspx");
            }
        }

        private void RefreshDataGrid()
        {
            menuProfile = new DATAMODEL.MenuProfileHeader();

             

            if (Session["MenuProfFilter"] != null)
            {
                menuProfile.Profile = !string.IsNullOrWhiteSpace(Session["MenuProfProfFilter"].ToString()) ? Session["MenuProfProfFilter"].ToString() : "";
                menuProfile.Description = !string.IsNullOrWhiteSpace(Session["MenuProfDescFilter"].ToString()) ? Session["MenuProfDescFilter"].ToString() : "";

                if (!string.IsNullOrWhiteSpace(menuProfile.Profile))
                {
                    TextBoxMenuProfile.Text = menuProfile.Profile;
                }

                if (!string.IsNullOrWhiteSpace(menuProfile.Description))
                {
                    TextBoxMenuProfileDescription.Text = menuProfile.Description;
                }


                Session.Remove("MenuProfFilter");
            }
            else
            {
                menuProfile.Profile = !string.IsNullOrWhiteSpace(TextBoxMenuProfile.Text) ? TextBoxMenuProfile.Text : "";
                menuProfile.Description = !string.IsNullOrWhiteSpace(TextBoxMenuProfileDescription.Text) ? TextBoxMenuProfileDescription.Text : "";
   
            }


            

            DTMenuProfile = CMSfunction.GetAllMenuProfileFiltered(menuProfile);
            ASPxGridViewMenuProfile.DataSource = DTMenuProfile;
            ASPxGridViewMenuProfile.KeyFieldName = "MENU PROFILE";
            ASPxGridViewMenuProfile.DataBind();
        }

        protected void ASPxGridViewMenuProfile_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            Session["MenuProfileManagement"] = ASPxGridViewMenuProfile.GetRowValues(Convert.ToInt32(e.Parameters), "MENU PROFILE").ToString();


            Session["Filter"] = "MenuProfManagement";
            Session["MenuProfFilter"] = "True";
            Session["MenuProfProfFilter"] = !string.IsNullOrWhiteSpace(TextBoxMenuProfile.Text) ? TextBoxMenuProfile.Text : "";
            Session["MenuProfDescFilter"] = !string.IsNullOrWhiteSpace(TextBoxMenuProfileDescription.Text) ? TextBoxMenuProfileDescription.Text : "";


            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("MenuProfileHeaderDetail.aspx");
            else
                Response.Redirect("MenuProfileHeaderDetail.aspx");
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            TextBoxMenuProfile.Text = "";
            TextBoxMenuProfileDescription.Text = "";
        }

        #region Grid Navigation Button
        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewMenuProfile.PageIndex <= ASPxGridViewMenuProfile.PageCount - 1)
            {
                ASPxGridViewMenuProfile.PageIndex = ASPxGridViewMenuProfile.PageIndex + 1;
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewMenuProfile.PageIndex - 1 >= 0)
            {
                ASPxGridViewMenuProfile.PageIndex = ASPxGridViewMenuProfile.PageIndex - 1;
            }
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewMenuProfile.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewMenuProfile.PageIndex = ASPxGridViewMenuProfile.PageCount - 1;
        }
        #endregion
    }
}