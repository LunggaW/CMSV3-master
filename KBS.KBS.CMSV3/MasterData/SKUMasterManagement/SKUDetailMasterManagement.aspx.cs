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
    public partial class SKUDetailMasterManagement : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSKUDetail = new DataTable();
        private DataTable DTParameterDetail = new DataTable();
        private DataTable DTGridViewUser = new DataTable();
        private User user;
        private String MenuID = ConfigurationManager.AppSettings["MenuIdDiscountManagement"];

        protected override void OnInit(EventArgs e)
        {
            if (Session["SKUIDforUpdate"] == null)
            {
                Response.Redirect("SKUMasterManagementHeader.aspx");
            }
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/Logins.aspx");
            }
            else
            {
                loadNavBar();
            }

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
            if (ASPxGridViewHeader.PageIndex - 1 >= 0)
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

        protected void Page_Load(object sender, EventArgs e)
        {
            string SKUID = Session["SKUIDforUpdate"].ToString();
            SKUGroupDetail skugroupdetail = new SKUGroupDetail();
            DTSKUDetail = CMSfunction.GetSKUDetailDataTable(skugroupdetail, SKUID);
            ASPxGridViewHeader.DataSource = DTSKUDetail;
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
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                Session["SKUDetailIDforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
                Session["SKUDetailEXIDforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID EXTERNAL").ToString();
                Session["SKUDetailNAMEforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "NAME").ToString();
                Session["SKUDetailGRPIDforUpdate"] = Session["SKUIDforUpdate"].ToString();
                Session["SKUDetailVALUEforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "VALUE").ToString();
                Session["SKUDetailPARTforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "PARTICIPATION").ToString();
                Session["SKUDetailBASEforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "BASED ON").ToString();
                Session["SKUDetailTYPEforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "TYPE").ToString();


                if (Page.IsCallback)
                    ASPxWebControl.RedirectOnCallback("SKUDetailMasterManagementEdit.aspx");
                else

                    Response.Redirect("SKUDetailMasterManagementEdit.aspx");
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            ColorGroupDetail color = new ColorGroupDetail();

            /*
                        color.ID = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderID.Text) ? ASPxTextBoxHeaderID.Text : "";
                        color.Color = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderName.Text) ? ASPxTextBoxHeaderName.Text : "";

                        DTColorDetail = CMSfunction.GetColorHeaderDataTable(color);
                        ASPxGridViewHeader.DataSource = DTColorDetail;
                        ASPxGridViewHeader.KeyFieldName = "ID";
                        ASPxGridViewHeader.DataBind();
              */
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            //Session.Remove("PriceIDforUpdate");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("SKUMasterManagementHeader.aspx");
            else

                Response.Redirect("SKUMasterManagementHeader.aspx");
            //Session.Remove("ParamHeaderID");
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {

            //ASPxTextBoxHeaderID.Text = ""; SizeOrder.Text = "";
            //SizeSDesc.Text = ""; SizeLDesc.Text = "";

        }


        protected void AddBtn_Click(object sender, EventArgs e)
        {

            Response.Redirect("SKUDetailMasterManagementNew.aspx");
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                SKUGroup skugroup = new SKUGroup();
                skugroup.ID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "GROUP ID").ToString();

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
                    String IDGRP = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "GROUP ID").ToString();
                    String ID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
                    String Level = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "LEVEL").ToString();

                    OutputMessage message = new OutputMessage();

                    message = CMSfunction.DeleteSKUDetail(IDGRP, ID, Level);

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
                    Response.Redirect("SKUDetailMasterManagement.aspx");
                }
            }

        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                Session["SKUDetailIDforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
                Session["SKUDetailEXIDforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID EXTERNAL").ToString();
                Session["SKUDetailNAMEforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "NAME").ToString();
                Session["SKUDetailGRPIDforUpdate"] = Session["SKUIDforUpdate"].ToString();
                Session["SKUDetailVALUEforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "VALUE").ToString();
                Session["SKUDetailPARTforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "PARTICIPATION").ToString();
                Session["SKUDetailBASEforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "BASED ON").ToString();
                Session["SKUDetailTYPEforUpdate"] =
                    ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "TYPE").ToString();


                if (Page.IsCallback)
                    ASPxWebControl.RedirectOnCallback("SKUDetailMasterManagementEdit.aspx");
                else

                    Response.Redirect("SKUDetailMasterManagementEdit.aspx");
            }
        }
    }

}