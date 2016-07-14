using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;
using KBS.KBS.CMSV3.Administration;

namespace KBS.KBS.CMSV3.MasterData.ItemManagement.VariantManagement
{
    public partial class VariantManagementHeader : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTStatus = new DataTable();
        private DataTable DTVariant = new DataTable();

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/logins.aspx");
            }
            else if (Session["ItemIDExManagementVariant"] == null)
            {
                Response.Redirect("../ItemManagementHeader.aspx");
            }
            else
            {
                loadNavBar();
            }
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            String ItemID = CMSfunction.GetItemIDByItemIDEx(Session["ItemIDExManagementVariant"].ToString());
            ASPxTextBoxItem.Text = Session["ItemIDExManagementVariant"].ToString() +" - "+CMSfunction.GetItemDescByItemID(ItemID);
            //Triger Valid Insert
            Session["DataValid"] = "0";
            RefreshDataGrid();

            DTStatus = CMSfunction.GetParameterValueAndDescbyClassAndTabID(Session["Class"].ToString(), "5");
            ComboStatus.DataSource = DTStatus;
            ComboStatus.ValueField = "PARVALUE";
            ComboStatus.ValueType = typeof(string);
            ComboStatus.TextField = "PARDESCRIPTION";
            ComboStatus.DataBind();

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

        


        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            //ASPxTextBoxHeaderBlock.Text = "";
            //ASPxTextBoxHeaderComment.Text = "";
            //ASPxTextBoxHeaderCopy.Text = "";
            //ASPxTextBoxHeaderID.Text = "";
            //ASPxTextBoxHeaderName.Text = "";
            //ASPxTextBoxHeaderSClas.Text = "";
        }

        protected void ASPxGridViewDetail_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            Session["VariantIDExternal"] = ASPxGridViewVariant.GetRowValues(Convert.ToInt32(e.Parameters), "VARIANT ID").ToString();

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("VariantManagementHeaderDetail.aspx");
            else
                Response.Redirect("VariantManagementHeaderDetail.aspx");
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("../ItemManagementHeader.aspx");
            else
                Response.Redirect("../ItemManagementHeader.aspx");
            
            
            
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {

                if (Page.IsCallback)
                   ASPxWebControl.RedirectOnCallback("VariantManagementHeaderNew.aspx");
                else
                    Response.Redirect("VariantManagementHeaderNew.aspx");

            
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewVariant.FocusedRowIndex != -1)
            {
                String ItemID = CMSfunction.GetItemIDByItemIDEx(Session["ItemIDExManagementVariant"].ToString());
                String VariantIDExt = ASPxGridViewVariant.GetRowValues(ASPxGridViewVariant.FocusedRowIndex, "VARIANT ID").ToString();

                OutputMessage message = new OutputMessage();

                message = CMSfunction.DeleteVariantMaster(VariantIDExt, ItemID);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;

                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            VariantMaster variant = new VariantMaster();

            variant.ItemID = CMSfunction.GetItemIDByItemIDEx(Session["ItemIDExManagementVariant"].ToString());
            variant.VariantIDExternal = !string.IsNullOrWhiteSpace(TextBoxVariantIdExternal.Text) ? TextBoxVariantIdExternal.Text : "";


            variant.ShortDesc = !string.IsNullOrWhiteSpace(TextBoxShortDescription.Text) ? TextBoxShortDescription.Text : "";
            variant.LongDesc = !string.IsNullOrWhiteSpace(TextBoxLongDescription.Text) ? TextBoxLongDescription.Text : "";
            variant.Status = (ComboStatus.Value != null) ? ComboStatus.Value.ToString() : "";


            DTVariant = CMSfunction.GetAllVariantFiltered(variant, Session["Class"].ToString());
            ASPxGridViewVariant.DataSource = DTVariant;
            ASPxGridViewVariant.KeyFieldName = "VARIANT ID";
            ASPxGridViewVariant.DataBind();
        }

        #region Grid Navigation Button
        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewVariant.PageIndex <= ASPxGridViewVariant.PageCount - 1)
            {
                ASPxGridViewVariant.PageIndex = ASPxGridViewVariant.PageIndex + 1;
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewVariant.PageIndex - 1 >= 0)
            {
                ASPxGridViewVariant.PageIndex = ASPxGridViewVariant.PageIndex - 1;
            }
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewVariant.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewVariant.PageIndex = ASPxGridViewVariant.PageCount - 1;
        }
        #endregion

        protected void ASPxButtonEntry_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewVariant.FocusedRowIndex != -1)
            {
                Session["VariantIDEx"] = ASPxGridViewVariant.GetRowValues(ASPxGridViewVariant.FocusedRowIndex, "VARIANT ID").ToString();
                Response.Redirect("VariantManagementDetail.aspx");
            }
        }

        protected void ASPxButtonBarcode_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewVariant.FocusedRowIndex != -1)
            {
                Session["VariantIDExBarcode"] = ASPxGridViewVariant.GetRowValues(ASPxGridViewVariant.FocusedRowIndex, "VARIANT ID").ToString();
                Response.Redirect("BarcodeManagement/BarcodeManagementHeader.aspx");
            }
        }


    }
}