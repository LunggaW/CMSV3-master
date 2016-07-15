using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.MasterData
{
    public partial class StyleMasterManagementHeader : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTStyleHeader = new DataTable();
        private DataTable DTParameterDetail = new DataTable();
        private DataTable DTGridViewUser = new DataTable();
        private User user;
        private String MenuID = ConfigurationManager.AppSettings["MenuIdStyleMasterManagement"];

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/Logins.aspx");
            }
            else
            {
                loadNavBar();
                loadButton(MenuID);
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            StyleGroup stylegroup = new StyleGroup();
            DTStyleHeader = CMSfunction.GetStyleHeaderDataTable(stylegroup);
            ASPxGridViewHeader.DataSource = DTStyleHeader;
            ASPxGridViewHeader.KeyFieldName = "ID";
            ASPxGridViewHeader.DataBind();
            string Compare = Session["Filter"].ToString();
            if (Compare != "")
            {
                if (Session["Filter"].ToString() != "StyleMaster")
                {
                    Administration.UserManagement.UserManagementHeader Sunda = new Administration.UserManagement.UserManagementHeader();
                    Sunda.ClearDataSeasson();
                }
            }

            else
            {
                try
                {
                    if (Session["StyleFilter"].ToString() == "True")
                    {
                   
                        ASPxTextBoxHeaderID.Text = !string.IsNullOrWhiteSpace(Session["StyleID"].ToString()) ? Session["StyleID"].ToString() : "";
                        ASPxTextBoxHeaderName.Text = !string.IsNullOrWhiteSpace(Session["StyleDesc"].ToString()) ? Session["StyleDesc"].ToString() : "";
                    
                    }
                }
                catch
                {
                    String Data = "Not Found";
                }
            }

            SearchBtn_Click();

        }

        private void SearchBtn_Click()
        {
            StyleGroup stylegroup = new StyleGroup();

            
            stylegroup.ID = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderID.Text) ? ASPxTextBoxHeaderID.Text : "";
            stylegroup.StyleDesc = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderName.Text) ? ASPxTextBoxHeaderName.Text : "";

            DTStyleHeader = CMSfunction.GetStyleHeaderDataTable(stylegroup);
            ASPxGridViewHeader.DataSource = DTStyleHeader;
            ASPxGridViewHeader.KeyFieldName = "ID";
            ASPxGridViewHeader.DataBind();
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
                            ASPxGridViewHeader.ClientSideEvents.RowDblClick = null;
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

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.PageIndex -1 > 0)
            {
                ASPxGridViewHeader.PageIndex = ASPxGridViewHeader.PageIndex - 1;
            }
        }
        

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewHeader.PageIndex = 0;
        }
        protected void NextBtn_Click(object sender, EventArgs e)        
        {
            if (ASPxGridViewHeader.PageIndex <= ASPxGridViewHeader.PageCount - 1)
            {
                ASPxGridViewHeader.PageIndex = ASPxGridViewHeader.PageIndex + 1;    
            }
        }
        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewHeader.PageIndex = ASPxGridViewHeader.PageCount - 1;
        }


        protected void ASPxGridViewHeader_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            Session["StyleIDforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
            Session["StyleDescforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "STYLE DESC").ToString();
            

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("StyleMasterManagementEdit.aspx");
            else

                Response.Redirect("StyleMasterManagementEdit.aspx");
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            Session["Filter"]= "StyleMaster";
            Session["StyleFilter"]= "True";
            Session["StyleID"] = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderID.Text) ? ASPxTextBoxHeaderID.Text : "";
            Session["StyleDesc"] = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderName.Text) ? ASPxTextBoxHeaderName.Text : "";

            SearchBtn_Click();
        }




        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            
            ASPxTextBoxHeaderID.Text = ""; ASPxTextBoxHeaderName.Text = "";
            Session["StyleID"] = "";
            Session["StyleDesc"] = "";
            Session["StyleFilter"] = "False";
            SearchBtn_Click();

        }


        protected void ASPxButtonEntry_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {

                Session["StyleIDforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
                Session["StyleDescforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "STYLE DESC").ToString();
            
                Response.Redirect("StyleDetailMasterManagement.aspx");
            }


        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("StyleMasterManagementNew.aspx");
        }
        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {

        }
        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                String IDGRP = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();



                OutputMessage message = new OutputMessage();

                message = CMSfunction.DeleteStyleHeader(IDGRP);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;
            }
            Response.Redirect("StyleMasterManagementHeader.aspx");
        }



    }

}