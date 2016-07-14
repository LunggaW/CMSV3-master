using System;
using System.Collections.Generic;
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

namespace KBS.KBS.CMSV3.MasterData.Assortment
{
    public partial class AssortmentManagement : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSite = new DataTable();
        private DataTable DTAssortment = new DataTable();
        private string ItemID;
        private string VariantID;
        private AssortmentMaster assortment;
        OutputMessage message = new OutputMessage();

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/logins.aspx");
            }
            else if (Session["ItemIDExAssortment"] == null && Session["VariantIDExAssortment"] == null)
            {
                Response.Redirect("ItemSearch.aspx");
            }
            {
                loadNavBar();
                ComboBoxSite.Visible = false;
                ASPxGridViewAssortment.Visible = false;
                ButtonChangeStatus.Visible = false;
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
                ItemID = CMSfunction.GetItemIDByItemIDEx(Session["ItemIDExAssortment"].ToString());
                TextBoxItem.Text = Session["ItemIDExAssortment"].ToString() + " - " + CMSfunction.GetItemDescByItemID(ItemID);

                VariantID = CMSfunction.GetVarIDByVarIDExandItemID(Session["VariantIDExAssortment"].ToString(), ItemID);
                TextBoxVariant.Text = Session["VariantIDExAssortment"].ToString() + " - " + CMSfunction.GetVariantDescByVariantIDandItemID(VariantID, ItemID);
               
                ComboBoxSite.Visible = true;
                ASPxGridViewAssortment.Visible = true;
                ButtonChangeStatus.Visible = true;

                

                RefreshDataGrid();

            //String ItemID = CMSfunction.GetItemIDByItemIDEx(Session["ItemIDExManagementVariant"].ToString());
            //ASPxTextBoxItem.Text = Session["ItemIDExManagementVariant"].ToString() +" - "+CMSfunction.GetItemDescByItemID(ItemID);

            //RefreshDataGrid();

            //DTStatus = CMSfunction.GetParameterValueAndDescbyClassAndTabID(Session["Class"].ToString(), "5");
            //ComboStatus.DataSource = DTStatus;
            //ComboStatus.ValueField = "PARVALUE";
            //ComboStatus.ValueType = typeof(string);
            //ComboStatus.TextField = "PARDESCRIPTION";
            //ComboStatus.DataBind();

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


        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("ItemSearch.aspx");
            else
                Response.Redirect("ItemSearch.aspx");



        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {



        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewAssortment.FocusedRowIndex != -1)
            {
                assortment = new AssortmentMaster();

                string dataitem = ItemID;
                string datavariant = VariantID;
                string datasite = ASPxGridViewAssortment.GetRowValues(ASPxGridViewAssortment.FocusedRowIndex, "SITE").ToString();



                String Result = CMSfunction.deleteAssortment(dataitem, datavariant, datasite);

               
                LabelMessage.Text = Result;
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            DTSite = CMSfunction.GetAllStoreAssortment(ItemID, VariantID);
            ComboBoxSite.DataSource = DTSite;
            ComboBoxSite.ValueField = "SITESITE";
            ComboBoxSite.ValueType = typeof(string);
            ComboBoxSite.TextField = "SITESITENAME";
            ComboBoxSite.DataBind();

            DTAssortment = CMSfunction.GetAllAssortmentByVarIDandItemID(ItemID, VariantID, Session["Class"].ToString());
            ASPxGridViewAssortment.DataSource = DTAssortment;
            ASPxGridViewAssortment.KeyFieldName = "SITE";
            ASPxGridViewAssortment.DataBind();
        }

        #region Grid Navigation Button
        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewAssortment.PageIndex <= ASPxGridViewAssortment.PageCount - 1)
            {
                ASPxGridViewAssortment.PageIndex = ASPxGridViewAssortment.PageIndex + 1;
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewAssortment.PageIndex - 1 >= 0)
            {
                ASPxGridViewAssortment.PageIndex = ASPxGridViewAssortment.PageIndex - 1;
            }
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewAssortment.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewAssortment.PageIndex = ASPxGridViewAssortment.PageCount - 1;
        }
        #endregion


        protected void ComboBoxSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            assortment = new AssortmentMaster();

            assortment.ItemID = ItemID;
            assortment.VariantID = VariantID;
            assortment.Site = ComboBoxSite.SelectedItem.Value.ToString();
            assortment.Status = "1";

            message = CMSfunction.insertAssortment(assortment, Session["UserID"].ToString());

            LabelMessage.Visible = true;
            LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            LabelMessage.Text = message.Message;

            RefreshDataGrid();
        }

        protected void ButtonChangeStatus_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewAssortment.FocusedRowIndex != -1)
            {
                assortment = new AssortmentMaster();

                assortment.ItemID = ItemID;
                assortment.VariantID = VariantID;
                assortment.Site = ASPxGridViewAssortment.GetRowValues(ASPxGridViewAssortment.FocusedRowIndex, "SITE").ToString();
                assortment.Status = "0";

                assortment.Status = ASPxGridViewAssortment.GetRowValues(ASPxGridViewAssortment.FocusedRowIndex, "STATUS").ToString() ==
                                    "Active" ? "0" : "1";

                message = CMSfunction.updateAssortment(assortment, Session["UserID"].ToString());

                LabelMessage.Visible = true;
                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Text = message.Message;
                RefreshDataGrid();
            }
        }


    }
}