using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.Administration.ParameterManagement
{
    public partial class ParameterManagementHeader : System.Web.UI.Page
    {
        
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTParameterHeader= new DataTable();        
        private String MenuID = ConfigurationManager.AppSettings["MenuIdParameterManagement"];

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
            //user = CMSfunction.SelectUserDataFromUserID(User.Identity.Name);
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            string Compare = Session["Filter"].ToString();
            if (Compare != "")
            {
                if (Session["Filter"].ToString() != "ParameterManagement")
                {
                    UserManagement.UserManagementHeader Sunda = new UserManagement.UserManagementHeader();
                    Sunda.ClearDataSeasson();
                }
            }
            RefreshGridView();
           
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


        protected void ASPxGridViewHeader_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            Session["ParamHeaderIDforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
            Session["ParamHeaderSClassforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SCLAS").ToString();


            Session["ParamIDFilter"] = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderID.Text) ? ASPxTextBoxHeaderID.Text : "";
            Session["ParamNameFilter"] = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderName.Text) ? ASPxTextBoxHeaderName.Text : "";

            Session["ParamForBack"] = "True";
            Session["ParamIDFilter"] = ASPxTextBoxHeaderID.Text;
            Session["ParamNameFilter"] = ASPxTextBoxHeaderName.Text;

            Session["ParamCopy"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "COPY").ToString();

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("ParameterManagementHeaderDetail.aspx");
            else

                Response.Redirect("ParameterManagementHeaderDetail.aspx");
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
           
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            //ASPxTextBoxHeaderBlock.Text = "";
            //ASPxTextBoxHeaderComment.Text = "";
            //ASPxTextBoxHeaderCopy.Text = "";
            ASPxTextBoxHeaderID.Text = "";ASPxTextBoxHeaderName.Text = "";
            //ASPxTextBoxHeaderSClas.Text = "";
        }

        
        protected void ASPxButtonEntry_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                Session["Filter"] = "ParameterManagement";
                Session["ParamForBack"] = "True";
                Session["ParamIDFilter"] = ASPxTextBoxHeaderID.Text;
                Session["ParamNameFilter"] = ASPxTextBoxHeaderName.Text;
                Session["ParamHeaderID"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
                Session["ParamHeaderSClass"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SCLAS").ToString();
                Session["ParamCopy"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "COPY").ToString();
                Response.Redirect("ParameterManagementDetail.aspx");
            }
            
            
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            Session["ParamForBack"] = "True";
            Session["ParamIDFilter"] = ASPxTextBoxHeaderID.Text;
            Session["ParamNameFilter"] = ASPxTextBoxHeaderName.Text;
            Response.Redirect("ParameterManagementHeaderNew.aspx");
            
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                String ParamHeaderID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
                String ParamSClas = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SCLAS").ToString();
                String ParamCopy = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "COPY").ToString();
                

                OutputMessage message = new OutputMessage();

                message = CMSfunction.DeleteParameterHeader(ParamHeaderID, ParamSClas, ParamCopy);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;

                RefreshGridView();
            }
        }

        #region Grid Navigation Button
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
        #endregion


        private void RefreshGridView()
        {
            ParameterHeader parHeader = new ParameterHeader();


            if (Session["MenuProfProfFilter"] != null || Session["ParamForBack"] != null)
            {
                parHeader.ID = !string.IsNullOrWhiteSpace(Session["ParamIDFilter"].ToString()) ? Session["ParamIDFilter"].ToString() : "";
                parHeader.Name = !string.IsNullOrWhiteSpace(Session["ParamNameFilter"].ToString()) ? Session["ParamNameFilter"].ToString() : "";

                if (!string.IsNullOrWhiteSpace(parHeader.ID))
                {
                    ASPxTextBoxHeaderID.Text = parHeader.ID;
                }

                if (!string.IsNullOrWhiteSpace(parHeader.Name))
                {
                    ASPxTextBoxHeaderName.Text = parHeader.Name;
                }

                
                Session.Remove("ParamForBack");
                Session.Remove("MenuProfProfFilter");
            }
            else
            {
                parHeader.ID = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderID.Text) ? ASPxTextBoxHeaderID.Text : "";
                parHeader.Name = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderName.Text) ? ASPxTextBoxHeaderName.Text : "";
            }

            

            DTParameterHeader = CMSfunction.GetParameterHeaderDataTable(parHeader, Session["Class"].ToString());
            ASPxGridViewHeader.DataSource = DTParameterHeader;
            ASPxGridViewHeader.KeyFieldName = "ID";
            ASPxGridViewHeader.DataBind();

        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                Session["ParamHeaderIDforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
                Session["ParamHeaderSClassforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SCLAS").ToString();


                Session["ParamIDFilter"] = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderID.Text)
                    ? ASPxTextBoxHeaderID.Text
                    : "";
                Session["ParamNameFilter"] = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderName.Text)
                    ? ASPxTextBoxHeaderName.Text
                    : "";

                Session["ParamForBack"] = "True";
                Session["ParamIDFilter"] = ASPxTextBoxHeaderID.Text;
                Session["ParamNameFilter"] = ASPxTextBoxHeaderName.Text;

                Session["ParamCopy"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "COPY").ToString();

                if (Page.IsCallback)
                    ASPxWebControl.RedirectOnCallback("ParameterManagementHeaderDetail.aspx");
                else

                    Response.Redirect("ParameterManagementHeaderDetail.aspx");
            }
        }
    }

}