﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.MasterData
{
    public partial class ColorMasterManagementHeader : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTColorHeader = new DataTable();
        private DataTable DTParameterDetail = new DataTable();
        private DataTable DTGridViewUser = new DataTable();
        private User user;

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
            /*ColorGroup color = new ColorGroup();
            DTColorHeader = CMSfunction.GetColorHeaderDataTable(color);
            ASPxGridViewHeader.DataSource = DTColorHeader;
            ASPxGridViewHeader.KeyFieldName = "ID";
            ASPxGridViewHeader.DataBind();*/
            string Compare = Session["Filter"].ToString();
            if (Compare != "")
            {
                if (Session["Filter"].ToString() != "ColorMaster")
                {
                    Administration.UserManagement.UserManagementHeader Sunda = new Administration.UserManagement.UserManagementHeader();
                    Sunda.ClearDataSeasson();
                }
                else
                {
                    try
                    {
                        if (Session["ColorFilter"].ToString() == "True")
                        {

                            ASPxTextBoxHeaderID.Text = !string.IsNullOrWhiteSpace(Session["ColorID"].ToString()) ? Session["ColorID"].ToString() : "";
                            ASPxTextBoxHeaderName.Text = !string.IsNullOrWhiteSpace(Session["ColorColor"].ToString()) ? Session["ColorColor"].ToString() : "";

                        }
                    }
                    catch
                    {
                        String Data = "Not Found";
                    }
                }
            }


          
            RefreshGrid();
        }
        
        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.PageIndex <= ASPxGridViewHeader.PageCount - 1)
            {
                ASPxGridViewHeader.PageIndex = ASPxGridViewHeader.PageIndex + 1;    
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.PageIndex -1 >= 0)
            {
                ASPxGridViewHeader.PageIndex = ASPxGridViewHeader.PageIndex - 1;
            }
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewHeader.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewHeader.PageIndex = ASPxGridViewHeader.PageCount - 1;
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

        protected void ASPxGridViewHeader_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            Session["ColorIDforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
            Session["ColorGrpforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "COLOR").ToString();
            

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("ColorMasterManagementEdit.aspx");
            else

                Response.Redirect("ColorMasterManagementEdit.aspx");
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            Session["Filter"]= "ColorMaster";
            Session["ColorFilter"] = "True";
            Session["ColorID"] = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderID.Text) ? ASPxTextBoxHeaderID.Text : "";
            Session["ColorColor"] = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderName.Text) ? ASPxTextBoxHeaderName.Text : "";
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            ColorGroup color = new ColorGroup();


            color.ID = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderID.Text) ? ASPxTextBoxHeaderID.Text : "";
            color.Color = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderName.Text) ? ASPxTextBoxHeaderName.Text : "";

            DTColorHeader = CMSfunction.GetColorHeaderDataTable(color);
            ASPxGridViewHeader.DataSource = DTColorHeader;
            ASPxGridViewHeader.KeyFieldName = "ID";
            ASPxGridViewHeader.DataBind();
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            
            ASPxTextBoxHeaderID.Text = ""; ASPxTextBoxHeaderName.Text = "";
            Session["ColorID"] = "";
            Session["ColorColor"] = "";
            Session["ColorFilter"] = "";
            RefreshGrid();

        }


        protected void ASPxButtonEntry_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                
                Session["ColorIDforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
                Session["ColorforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "COLOR").ToString();
                
                Response.Redirect("ColorDetailMasterManagement.aspx");
            }


        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ColorMasterManagementNew.aspx");
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                String GRPID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
               


                OutputMessage message = new OutputMessage();

                message = CMSfunction.DeleteColorHeader(GRPID);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;
            }
            Response.Redirect("ColorMasterManagementHeader.aspx");
        }



    }

}