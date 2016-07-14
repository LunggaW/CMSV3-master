using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;

namespace KBS.KBS.CMSV3.Administration.AccessManagement
{
    public partial class AccessProfileHeader : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTAccessProfile = new DataTable();
        private DATAMODEL.AccessProfileHeader accessProfile;
        private String MenuID = ConfigurationManager.AppSettings["MenuIdAccessProfileManagement"];
        

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
                if (Session["Filter"].ToString() != "AccessProfile")
                {
                    UserManagement.UserManagementHeader Sunda = new UserManagement.UserManagementHeader();
                    Sunda.ClearDataSeasson();
                }
                
                
                
            }
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
                            ASPxGridViewAccessProfile.ClientSideEvents.RowDblClick = null;
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
            Session["Filter"] = "AccessProfile";
            Session["AccProfFilter"] = "True";
            Session["AccProfProfFilter"] = !string.IsNullOrWhiteSpace(TextBoxACcessProfile.Text) ? TextBoxACcessProfile.Text : "";
            Session["AccProfDescFilter"] = !string.IsNullOrWhiteSpace(TextBoxAccessProfileDescription.Text) ? TextBoxAccessProfileDescription.Text : "";

            RefreshDataGrid();
        }

        
        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewAccessProfile.FocusedRowIndex != -1)
            {
                OutputMessage message = new OutputMessage();

                DATAMODEL.AccessProfileHeader accessProfile = new DATAMODEL.AccessProfileHeader();

                accessProfile.Profile =
                    ASPxGridViewAccessProfile.GetRowValues(ASPxGridViewAccessProfile.FocusedRowIndex, "ACCESS PROFILE")
                        .ToString();

                message = CMSfunction.DeleteAccessProfileHeader(accessProfile);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;

                RefreshDataGrid();
            }
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccessProfileHeaderNew.aspx");
        }

        protected void ASPxButtonEntry_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewAccessProfile.FocusedRowIndex != -1)
            {
                Session["AccessProfileManagement"] = ASPxGridViewAccessProfile.GetRowValues(ASPxGridViewAccessProfile.FocusedRowIndex, "ACCESS PROFILE").ToString();
                Response.Redirect("AccessProfileDetail.aspx");
            }
        }

        private void RefreshDataGrid()
        {
            accessProfile = new DATAMODEL.AccessProfileHeader();

            if (Session["AccProfFilter"] != null)
            {
                accessProfile.Profile = !string.IsNullOrWhiteSpace(Session["AccProfProfFilter"].ToString()) ? Session["AccProfProfFilter"].ToString() : "";
                accessProfile.Description = !string.IsNullOrWhiteSpace(Session["AccProfDescFilter"].ToString()) ? Session["AccProfDescFilter"].ToString() : "";

                if (!string.IsNullOrWhiteSpace(accessProfile.Profile))
                {
                    TextBoxACcessProfile.Text = accessProfile.Profile;
                }

                if (!string.IsNullOrWhiteSpace(accessProfile.Description))
                {
                    TextBoxAccessProfileDescription.Text = accessProfile.Description;
                }


                Session.Remove("AccProfFilter");
            }
            else
            {
                accessProfile.Profile = !string.IsNullOrWhiteSpace(TextBoxACcessProfile.Text) ? TextBoxACcessProfile.Text : "";
                accessProfile.Description = !string.IsNullOrWhiteSpace(TextBoxAccessProfileDescription.Text) ? TextBoxAccessProfileDescription.Text : "";
            }


            DTAccessProfile = CMSfunction.GetAllAccessProfileFiltered(accessProfile);
            ASPxGridViewAccessProfile.DataSource = DTAccessProfile;
            ASPxGridViewAccessProfile.KeyFieldName = "ACCESS PROFILE";
            ASPxGridViewAccessProfile.DataBind();
        }

        protected void ASPxGridViewAccessProfile_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            
            Session["AccessProfileManagement"] = ASPxGridViewAccessProfile.GetRowValues(Convert.ToInt32(e.Parameters), "ACCESS PROFILE").ToString();
            

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("AccessProfileHeaderDetail.aspx");
            else
                Response.Redirect("AccessProfileHeaderDetail.aspx");
        }

       
        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            TextBoxACcessProfile.Text = "";
            TextBoxAccessProfileDescription.Text = "";
        }

        #region Grid Navigation Button
        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewAccessProfile.PageIndex <= ASPxGridViewAccessProfile.PageCount - 1)
            {
                ASPxGridViewAccessProfile.PageIndex = ASPxGridViewAccessProfile.PageIndex + 1;
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewAccessProfile.PageIndex - 1 >= 0)
            {
                ASPxGridViewAccessProfile.PageIndex = ASPxGridViewAccessProfile.PageIndex - 1;
            }
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewAccessProfile.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewAccessProfile.PageIndex = ASPxGridViewAccessProfile.PageCount - 1;
        }
        #endregion

    }
}