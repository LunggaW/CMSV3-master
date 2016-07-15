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

namespace KBS.KBS.CMSV3.MasterData.ItemManagement
{
    public partial class ItemManagementHeaderDetail : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        OutputMessage message = new OutputMessage();
        private DataTable DTItemType = new DataTable();
        private DataTable DTBrand = new DataTable();

        protected override void OnInit(EventArgs e)
        {

            if (Session["ItemIDExManagement"] == null)
            {
                Response.Redirect("ItemManagementHeader.aspx");
            }
            else if (Session["UserID"] == null)
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
            DTItemType = CMSfunction.GetParameterValueAndDescbyClassAndTabID(Session["Class"].ToString(), "13");
            ComboItemType.DataSource = DTItemType;
            ComboItemType.ValueField = "PARVALUE";
            ComboItemType.ValueType = typeof(string);
            ComboItemType.TextField = "PARDESCRIPTION";
            ComboItemType.DataBind();


            DTBrand = CMSfunction.GetAllBrand();
            ComboBrand.DataSource = DTBrand;
            ComboBrand.ValueField = "BRAND";
            ComboBrand.ValueType = typeof(string);
            ComboBrand.TextField = "DESCRIPTION";
            ComboBrand.DataBind();


            if (!IsPostBack)
            {
                ItemMaster item = new ItemMaster();

                item.ItemIDExternal = Session["ItemIDExManagement"].ToString();

                item.ItemID = CMSfunction.GetItemIDByItemIDEx(item.ItemIDExternal);

                item = CMSfunction.GetItemMaster(item);

                TextBoxItemIdExternal.Text = item.ItemIDExternal;
                TextBoxLongDescription.Text = item.LongDesc;
                TextBoxShortDescription.Text = item.ShortDesc;
                ComboBrand.SelectedItem = ComboBrand.Items.FindByValue(item.Brand);
                ComboItemType.SelectedItem = ComboItemType.Items.FindByValue(item.Type);
            }

            
            

        }


        private void loadNavBar()
        {
            string a = Session["MenuProfile"].ToString();
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
            //ParameterDetail parDetail = new ParameterDetail();

            //parHeader.Lock = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderBlock.Text) ? ASPxTextBoxHeaderBlock.Text : "";
            //parHeader.Comment = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderComment.Text) ? ASPxTextBoxHeaderComment.Text : "";
            //parHeader.Copy = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderCopy.Text) ? ASPxTextBoxHeaderCopy.Text : "";
            //parHeader.ID = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderID.Text) ? ASPxTextBoxHeaderID.Text : "";
            //parHeader.Name = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderName.Text) ? ASPxTextBoxHeaderName.Text : "";

            //DTParameterHeader = CMSfunction.GetParameterHeaderData(parHeader);
            //ASPxGridViewDetail.DataSource = DTParameterHeader;
            //ASPxGridViewDetail.KeyFieldName = "ID"; ASPxGridViewHeader.DataBind();
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
            Session.Remove("ItemIDExManagement");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("ItemManagementHeader.aspx");
            else

                Response.Redirect("ItemManagementHeader.aspx");
            //Session.Remove("ParamHeaderID");
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            ProcessUpdate();


            LabelMessage.Visible = true;
            LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            LabelMessage.Text = message.Message;
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            //Response.Redirect("ParameterManagementDetailNew.aspx");
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            ProcessUpdate();
            Response.Redirect("ItemManagementHeader.aspx");
        }


        private void ProcessUpdate()
        {
            ItemMaster item = new ItemMaster();

            item.ItemIDExternal = !string.IsNullOrWhiteSpace(TextBoxItemIdExternal.Text) ? TextBoxItemIdExternal.Text : "";
            item.ShortDesc = !string.IsNullOrWhiteSpace(TextBoxShortDescription.Text) ? TextBoxShortDescription.Text : "";
            item.LongDesc = !string.IsNullOrWhiteSpace(TextBoxLongDescription.Text) ? TextBoxLongDescription.Text : "";
            item.Brand = !string.IsNullOrWhiteSpace(TextBoxLongDescription.Text) ? TextBoxLongDescription.Text : "";
            item.Type = (ComboItemType.Value != null) ? ComboItemType.Value.ToString() : "";
            item.Brand = (ComboBrand.Value != null) ? ComboBrand.Value.ToString() : "";



            message = CMSfunction.updateItemMaster(item, Session["UserID"].ToString());
        }

    }
}