using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.MasterData
{
    public partial class BrandMasterManagementHeader : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTBrand = new DataTable();
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
            BrandGroup brandgroup = new BrandGroup();
            DTBrand = CMSfunction.GetBrandHeaderDataTable(brandgroup);
            ASPxGridViewHeader.DataSource = DTBrand;
            ASPxGridViewHeader.KeyFieldName = "ID";
            ASPxGridViewHeader.DataBind();
            string Compare = Session["Filter"].ToString();
            if (Compare != "")
            {
                if (Session["Filter"].ToString() != "BrandMaster")
                {
                    Administration.UserManagement.UserManagementHeader Sunda = new Administration.UserManagement.UserManagementHeader();
                    Sunda.ClearDataSeasson();
                    
                }
                else
                {
                    try
                    {
                        if (Session["BrandFilter"].ToString() == "True")
                        {

                            ASPxTextBoxHeaderID.Text = !string.IsNullOrWhiteSpace(Session["FBrandIDforUpdate"].ToString()) ? Session["FBrandIDforUpdate"].ToString() : "";
                            ASPxTextBoxHeaderName.Text = !string.IsNullOrWhiteSpace(Session["FBrandDescforUpdate"].ToString()) ? Session["FBrandDescforUpdate"].ToString() : "";
                        }
                    }
                    catch
                    {
                        String Data = "Not Found";
                    }
                }
            }
            refreshgrid();

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

        protected void ASPxGridViewHeader_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            
            Session["BrandIDforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
            Session["BrandDescforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "BRAND DESC").ToString();
            

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("BrandMasterManagementEdit.aspx");
            else

                Response.Redirect("BrandMasterManagementEdit.aspx");
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
            if (ASPxGridViewHeader.PageIndex - 1 > 0)
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

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            Session["Filter"] = "BrandMaster";
            Session["BrandFilter"] = "True";
            Session["FBrandIDforUpdate"] = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderID.Text) ? ASPxTextBoxHeaderID.Text : "";
            Session["FBrandDescforUpdate"] = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderName.Text) ? ASPxTextBoxHeaderName.Text : "";

            refreshgrid();
        }

        private void refreshgrid()
        {
            BrandGroup brandgroup = new BrandGroup();


            brandgroup.ID = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderID.Text) ? ASPxTextBoxHeaderID.Text : "";
            brandgroup.Brand = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderName.Text) ? ASPxTextBoxHeaderName.Text : "";

            DTBrand = CMSfunction.GetBrandHeaderDataTable(brandgroup);
            ASPxGridViewHeader.DataSource = DTBrand;
            ASPxGridViewHeader.KeyFieldName = "ID";
            ASPxGridViewHeader.DataBind();

        }
        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            
            ASPxTextBoxHeaderID.Text = "";
            ASPxTextBoxHeaderName.Text = "";
            Session["FBrandIDforUpdate"] = "";
            Session["FBrandDescforUpdate"] = "";
            Session["BrandFilter"] = "False";
            refreshgrid();

        }


        //protected void ASPxButtonEntry_Click(object sender, EventArgs e)
        //{
        //    if (ASPxGridViewHeader.FocusedRowIndex != -1)
        //    {

        //        Session["BrandIDforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
        //        Session["BrandDescforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "BRAND DESC").ToString();

        //        Response.Redirect("BrandDetailMasterManagement.aspx");
        //    }


        //}

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("BrandMasterManagementNew.aspx");
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                String PBRNDBRNDID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
                //String ParamSClas = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SCLAS").ToString();


                OutputMessage message = new OutputMessage();

                message = CMSfunction.DeleteBrandHeader(PBRNDBRNDID);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;
            }
            Response.Redirect("BrandMasterManagementHeader.aspx");
        }



    }

}