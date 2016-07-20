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
    public partial class SKUMasterManagementHeader : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSKUHeader = new DataTable();
        private DataTable DTParameterDetail = new DataTable();
        private DataTable DTGridViewUser = new DataTable();
        private User user;
        private String MenuID = ConfigurationManager.AppSettings["MenuIdSKUManagement"];

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
            SKUGroup skugroup = new SKUGroup();
            DTSKUHeader = CMSfunction.GetSKUHeaderDataTable(skugroup);
            ASPxGridViewHeader.DataSource = DTSKUHeader;
            ASPxGridViewHeader.KeyFieldName = "INTERNAL ID";
            ASPxGridViewHeader.DataBind();
            string Compare = Session["Filter"].ToString();
            if (Compare != "")
            {
                if (Session["Filter"].ToString() != "SKUMaster")
                {
                    Administration.UserManagement.UserManagementHeader Sunda = new Administration.UserManagement.UserManagementHeader();
                    Sunda.ClearDataSeasson();
                }
            }


            else
            {
                try
                { 
                    if (Session["SKUFilter"].ToString() == "True")
                    {
                    

                        IDTXT.Text = !string.IsNullOrWhiteSpace(Session["SKUID"].ToString()) ? Session["SKUID"].ToString() : "";
                        EXIDTXT.Text = !string.IsNullOrWhiteSpace(Session["SKUEXID"].ToString()) ? Session["SKUEXID"].ToString() : "";
                        SDESCTXT.Text = !string.IsNullOrWhiteSpace(Session["SKUSDesc"].ToString()) ? Session["SKUSDesc"].ToString() : "";
                        LDESCTXT.Text = !string.IsNullOrWhiteSpace(Session["SKULDesc"].ToString()) ? Session["SKULDesc"].ToString() : "";
                        //EDATE.Date = DateTime.Parse(Session["SKUEDate"].ToString()) != DateTime.MinValue ? (DateTime?)DateTime.Parse(Session["SKUEDate"].ToString()) : null; 
                        //SDATE.Date = !string.IsNullOrWhiteSpace(Session["SKUSDate"].ToString()) ? Session["SKUSDate"].ToString() : "";
                    
                    }
                }
                catch
                {
                    String Data = "Not Found";
                }

        }
            SearchBtn_Click();
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
            if (ASPxGridViewHeader.PageIndex -1 > 0)
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

        protected void ASPxGridViewHeader_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            Session["SKUIDforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "INTERNAL ID").ToString();
            Session["SKUEXIDforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "EXTERNAL ID").ToString();
            Session["SKUSDESCforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SHORT DESC").ToString();
            Session["SKULDESCforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "LONG DESC").ToString();
            Session["SKUSDATEforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "START DATE").ToString();
            Session["SKUEDATEforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "END DATE").ToString();
            
            

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("SKUMasterManagementEdit.aspx");
            else

                Response.Redirect("SKUMasterManagementEdit.aspx");
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            Session["Filter"] = "SKUMaster";
            Session["SKUFilter"] = "True";
            Session["SKUID"] = !string.IsNullOrWhiteSpace(IDTXT.Text) ? IDTXT.Text : "";
            Session["SKUEXID"] = !string.IsNullOrWhiteSpace(EXIDTXT.Text) ? EXIDTXT.Text : "";
            Session["SKUSDesc"] = !string.IsNullOrWhiteSpace(SDESCTXT.Text) ? SDESCTXT.Text : "";
            Session["SKULDesc"] = !string.IsNullOrWhiteSpace(LDESCTXT.Text) ? LDESCTXT.Text : "";
            Session["SKUEDate"] = EDATE.Date != DateTime.MinValue ? (DateTime?)EDATE.Date : null;
            Session["SKUSDate"] = SDATE.Date != DateTime.MinValue ? (DateTime?)SDATE.Date : null;
            SearchBtn_Click();
        }

        private void SearchBtn_Click()
        {
            SKUGroup skugroup = new SKUGroup();


            skugroup.ID = !string.IsNullOrWhiteSpace(IDTXT.Text) ? IDTXT.Text : "";
            skugroup.EXID = !string.IsNullOrWhiteSpace(EXIDTXT.Text) ? EXIDTXT.Text : "";
            skugroup.SDesc = !string.IsNullOrWhiteSpace(SDESCTXT.Text) ? SDESCTXT.Text : "";
            skugroup.LDesc = !string.IsNullOrWhiteSpace(LDESCTXT.Text) ? LDESCTXT.Text : "";
            skugroup.EDate = EDATE.Date != DateTime.MinValue ? (DateTime?)EDATE.Date : null;
            skugroup.SDate = SDATE.Date != DateTime.MinValue ? (DateTime?)SDATE.Date : null;

            DTSKUHeader = CMSfunction.GetSKUHeaderDataTable(skugroup);
            ASPxGridViewHeader.DataSource = DTSKUHeader;
            ASPxGridViewHeader.KeyFieldName = "INTERNAL ID";
            ASPxGridViewHeader.DataBind();
        }
        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            IDTXT.Text = ""; EXIDTXT.Text = "";
            EDATE.Text = ""; SDATE.Text = "";
            SDESCTXT.Text = ""; LDESCTXT.Text = "";
            
            Session["SKUID"] = "";
            Session["SKUEXID"] = "";
            Session["SKUSDesc"] = "";
            Session["SKULDesc"] = "";
            Session["SKUEDate"] = "";
            Session["SKUSDate"] = "";
            Session["SKUFilter"] = "False";
            SearchBtn_Click();

        }


        protected void ASPxButtonEntry_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {

                Session["SKUIDforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "INTERNAL ID").ToString();
                
            
                Response.Redirect("SKUDetailMasterManagement.aspx");
            }


        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("SKUMasterManagementNew.aspx");
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                SKUGroup skugroup = new SKUGroup();
                skugroup.ID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "INTERNAL ID").ToString();

                skugroup = CMSfunction.Cekdatasku(skugroup);
                if (skugroup.LDesc == "NO")
                {

                    String alert = "Data Cannot Delete Because Already Use";
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
                    String SKUID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "INTERNAL ID").ToString();
                    OutputMessage message = new OutputMessage();

                    message = CMSfunction.DeleteSKUHeader(SKUID);

                    LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                    LabelMessage.Visible = true;
                    LabelMessage.Text = message.Message;

                    String alert = "Delete Success";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(alert);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

                    Response.Redirect("SKUMasterManagementHeader.aspx");
                }
            }
        }



    }

}