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
    public partial class ItemSearch : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTItemVariant = new DataTable();
        private DataTable DTItemType = new DataTable();
        private DataTable DTBrand = new DataTable();
        private string ItemID;

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/logins.aspx");
            }
            else if (Session["SearchReturnURL"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                loadNavBar();
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RefreshDataGridItemVariant();


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
            RefreshDataGridItemVariant();
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
            //Session["ItemIDExSearch"] = ASPxGridViewItemVariant.GetRowValues(Convert.ToInt32(e.Parameters), "ITEM ID").ToString();
            //Session["VariantIDExSearch"] = ASPxGridViewItemVariant.GetRowValues(Convert.ToInt32(e.Parameters), "VARIANT ID").ToString();

            //if (Page.IsCallback)
            //    ASPxWebControl.RedirectOnCallback(Session["SearchReturnURL"].ToString());
            //else
            //    Response.Redirect(Session["SearchReturnURL"].ToString());
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            //    Session.Remove("ParamHeaderID");
            //    if (Page.IsCallback)
            //        ASPxWebControl.RedirectOnCallback("ParameterManagementHeader.aspx");
            //    else
            //        Response.Redirect("ParameterManagementHeader.aspx");



        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
        }


        private void RefreshDataGridItemVariant()
        {
            ItemVariant itemVariant = new ItemVariant();

            itemVariant.ItemIDExternal = !string.IsNullOrWhiteSpace(TextBoxItemIdExternal.Text) ? TextBoxItemIdExternal.Text : "";
            itemVariant.VariantIDExternal = !string.IsNullOrWhiteSpace(TextBoxVariantIdExternal.Text) ? TextBoxVariantIdExternal.Text : "";
            itemVariant.ShortDesc = !string.IsNullOrWhiteSpace(TextBoxShortDescription.Text) ? TextBoxShortDescription.Text : "";
            itemVariant.LongDesc = !string.IsNullOrWhiteSpace(TextBoxLongDescription.Text) ? TextBoxLongDescription.Text : "";


            DTItemVariant = CMSfunction.GetItemVarianFiltered(itemVariant);
            ASPxGridViewItemVariant.DataSource = DTItemVariant;
            ASPxGridViewItemVariant.KeyFieldName = "ITEM ID";
            ASPxGridViewItemVariant.DataBind();
        }

        #region Grid Navigation Button
        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewItemVariant.PageIndex <= ASPxGridViewItemVariant.PageCount - 1)
            {
                ASPxGridViewItemVariant.PageIndex = ASPxGridViewItemVariant.PageIndex + 1;
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewItemVariant.PageIndex - 1 >= 0)
            {
                ASPxGridViewItemVariant.PageIndex = ASPxGridViewItemVariant.PageIndex - 1;
            }
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewItemVariant.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewItemVariant.PageIndex = ASPxGridViewItemVariant.PageCount - 1;
        }
        #endregion

        protected void ASPxButtonSearch_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewItemVariant.FocusedRowIndex != -1)
            {
                Session["ItemIDExAssortment"] = ASPxGridViewItemVariant.GetRowValues(ASPxGridViewItemVariant.FocusedRowIndex, "ITEM ID").ToString();
                Session["VariantIDExAssortment"] = ASPxGridViewItemVariant.GetRowValues(ASPxGridViewItemVariant.FocusedRowIndex, "VARIANT ID").ToString();
                Response.Redirect(Session["SearchReturnURL"].ToString());
            }
            
        }




    }
}