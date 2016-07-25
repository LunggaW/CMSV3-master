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
    public partial class AssortmentManagementNewItem : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTAssortment = new DataTable();
        
        private AssortmentMaster assortment;
        OutputMessage message = new OutputMessage();

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/logins.aspx");
            }
            else if (Session["SiteAssortment"] == null)
            {
                Response.Redirect("AssortmentManagementItemView.aspx");

            }
            else
            {
                loadNavBar();
                TextBoxSite.Text = CMSfunction.GetSiteCodeandNameFromSiteCode(Session["SiteAssortment"].ToString());
                ASPxGridViewAssortment.Visible = false;
                
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
               
                
                ASPxGridViewAssortment.Visible = true;
                

                

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


        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("AssortmentManagementDetailView.aspx");
            else
                Response.Redirect("AssortmentManagementDetailView.aspx");



        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {


        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
        }

        private void RefreshDataGrid()
        {
            AssortmentMaster assortment = new AssortmentMaster();

            assortment.ItemID = !string.IsNullOrWhiteSpace(TextBoxItemID.Text) ? TextBoxItemID.Text : "";
            assortment.VariantID = !string.IsNullOrWhiteSpace(TextBoxVariant.Text) ? TextBoxVariant.Text : "";
            assortment.Site = Session["SiteAssortment"].ToString();
            

            DTAssortment = CMSfunction.GetItemNotExistInAssortment(Session["Class"].ToString(), assortment);
            
            ASPxGridViewAssortment.DataSource = DTAssortment;
            ASPxGridViewAssortment.KeyFieldName = "ITEM ID";
            ASPxGridViewAssortment.DataBind();
        }

        private void ProcessInsert()
        {
            if (ASPxGridViewAssortment.FocusedRowIndex != -1)
            {
                AssortmentMaster assortment = new AssortmentMaster();

                assortment.ItemID = CMSfunction.GetItemIDByItemIDEx(
                    ASPxGridViewAssortment.GetRowValues(ASPxGridViewAssortment.FocusedRowIndex, "ITEM ID").ToString());


                assortment.VariantID =
                    CMSfunction.GetVariantIDByVariantIDEx(
                        ASPxGridViewAssortment.GetRowValues(ASPxGridViewAssortment.FocusedRowIndex, "VARIANT")
                            .ToString());

                assortment.Site = Session["SiteAssortment"].ToString();
                assortment.Status = "1";
                



                message = CMSfunction.insertAssortment(assortment, Session["UserID"].ToString());
            }
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

        protected void ASPxGridViewAssortment_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            ProcessInsert();
            Response.Redirect("AssortmentManagementItemView.aspx");
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            RefreshDataGrid();
            ProcessInsert();

            LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            LabelMessage.Visible = true;
            LabelMessage.Text = message.Message;


        }
    }
}