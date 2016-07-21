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

namespace KBS.KBS.CMSV3.MasterData.ItemManagement.VariantManagement
{
    public partial class VariantManagementDetail : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();

        private DataTable DTColorGroup = new DataTable();
        private DataTable DTColorDetail = new DataTable();
        private DataTable DTStyleGroup = new DataTable();
        private DataTable DTStyleDetail = new DataTable();
        private DataTable DTSizeGroup = new DataTable();
        private DataTable DTSizeDetail = new DataTable();
        private DataTable DTVariant = new DataTable();
        private DataTable DTVariantDetail = new DataTable();

        private VariantDetail variantDetail;
        OutputMessage message = new OutputMessage();

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
            else if (Session["VariantIDEx"] == null)
            {
                Response.Redirect("VariantManagementHeader.aspx");
            }
            else
            {
                String ItemID = CMSfunction.GetItemIDByItemIDEx(Session["ItemIDExManagementVariant"].ToString());
                ASPxTextBoxItem.Text = Session["ItemIDExManagementVariant"].ToString() + " - " + CMSfunction.GetItemDescByItemID(ItemID);

                variantDetail = new VariantDetail();
                TextBoxVariantIdx.Text = Session["VariantIDEx"].ToString();
                String VariantID = CMSfunction.GetVarIDByVarIDExandItemID(Session["VariantIDEx"].ToString(), ItemID);
                TextBoxVariantId.Text = VariantID;
                

                variantDetail = CMSfunction.GetVariantDetail(VariantID);

                if (string.IsNullOrWhiteSpace(variantDetail.VariantID))
                {
                    Response.Redirect("VariantManagementDetailNew.aspx");
                }
                else
                {
                    loadNavBar();
                }
            }
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            DTColorGroup = CMSfunction.GetAllColorGroup();
            ComboGroupColor.DataSource = DTColorGroup;
            ComboGroupColor.ValueField = "cogrcogid";
            ComboGroupColor.ValueType = typeof(string);
            ComboGroupColor.TextField = "cogrdesc";
            ComboGroupColor.DataBind();

            DTStyleGroup = CMSfunction.GetAllStyleGroup();
            ComboGroupStyle.DataSource = DTStyleGroup;
            ComboGroupStyle.ValueField = "Stgrstgid";
            ComboGroupStyle.ValueType = typeof(string);
            ComboGroupStyle.TextField = "stgrdesc";
            ComboGroupStyle.DataBind();

            DTSizeGroup = CMSfunction.GetAllSizeGroup();
            ComboGroupSize.DataSource = DTSizeGroup;
            ComboGroupSize.ValueField = "Szgrszgid";
            ComboGroupSize.ValueType = typeof(string);
            ComboGroupSize.TextField = "Szgrdesc";
            ComboGroupSize.DataBind();


            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(variantDetail.ColorGroup))
                {
                   ComboGroupColor.SelectedItem =  ComboGroupColor.Items.FindByValue(variantDetail.ColorGroup);
                }

                if (!string.IsNullOrWhiteSpace(variantDetail.SizeGroup))
                {
                    ComboGroupSize.SelectedItem =  ComboGroupSize.Items.FindByValue(variantDetail.SizeGroup);
                }

                if (!string.IsNullOrWhiteSpace(variantDetail.StyleGroup))
                {
                    ComboGroupStyle.SelectedItem = ComboGroupStyle.Items.FindByValue(variantDetail.StyleGroup);
                }

                if (ComboGroupColor.SelectedIndex != -1)
                {
                    DTColorDetail = CMSfunction.GetAllColorDetailbyColorGroup(variantDetail.ColorGroup);
                    ComboDetailColor.DataSource = DTColorDetail;
                    ComboDetailColor.ValueField = "Colrcolid";
                    ComboDetailColor.ValueType = typeof(string);
                    ComboDetailColor.TextField = "colrdesc";
                    ComboDetailColor.DataBind();

                    if (!string.IsNullOrWhiteSpace(variantDetail.ColorDetail))
                    {
                        ComboDetailColor.SelectedItem = ComboDetailColor.Items.FindByValue(variantDetail.ColorDetail);
                        ComboGroupColor.ReadOnly = true;
                        ComboDetailColor.ReadOnly = true;
                        ComboGroupColor.Enabled = false;
                        ComboDetailColor.Enabled = false;
                    }
                }

                if (ComboGroupSize.SelectedIndex != -1)
                {
                    DTSizeDetail = CMSfunction.GetAllSizeDetailbySizeGroup(variantDetail.SizeGroup);
                    ComboDetailSize.DataSource = DTSizeDetail;
                    ComboDetailSize.ValueField = "sizeszid";
                    ComboDetailSize.ValueType = typeof(string);
                    ComboDetailSize.TextField = "sizedesc";
                    ComboDetailSize.DataBind();

                    if (!string.IsNullOrWhiteSpace(variantDetail.SizeDetail))
                    {
                        ComboDetailSize.SelectedItem = ComboDetailSize.Items.FindByValue(variantDetail.SizeDetail);
                        ComboGroupSize.ReadOnly = true;
                        ComboDetailSize.ReadOnly = true;
                        ComboGroupSize.Enabled = false;
                        ComboDetailSize.Enabled = false;
                    }
                }

                if (ComboGroupStyle.SelectedIndex != -1)
                {
                    DTStyleDetail = CMSfunction.GetAllStyleDetailbyStyleGroup(variantDetail.StyleGroup);
                    ComboDetailStyle.DataSource = DTStyleDetail;
                    ComboDetailStyle.ValueField = "Stylstylid";
                    ComboDetailStyle.ValueType = typeof(string);
                    ComboDetailStyle.TextField = "styldesc";
                    ComboDetailStyle.DataBind();

                    if (!string.IsNullOrWhiteSpace(variantDetail.StyleDetail))
                    {
                        ComboDetailStyle.SelectedItem = ComboDetailStyle.Items.FindByValue(variantDetail.StyleDetail);
                        ComboDetailStyle.ReadOnly = true;
                        ComboGroupStyle.ReadOnly = true;
                        ComboDetailStyle.Enabled = false;
                        ComboGroupStyle.Enabled = false;
                    }
                } 
            }
           
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
                ASPxWebControl.RedirectOnCallback("VariantManagementHeader.aspx");
            else
                Response.Redirect("VariantManagementHeader.aspx");
            
            
            
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
                String VariantID = TextBoxVariantId.Text;

                OutputMessage message = new OutputMessage();

                message = CMSfunction.DeleteVariantDetail(VariantID);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;

        }


        

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            ProcessUpdate();
            LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            LabelMessage.Visible = true;
            LabelMessage.Text = message.Message;
        }


        private void ProcessUpdate()
        {
            variantDetail = new VariantDetail();

            variantDetail.VariantID = !string.IsNullOrWhiteSpace(TextBoxVariantId.Text) ? TextBoxVariantId.Text : "";
            variantDetail.ColorDetail = (ComboDetailColor.Value != null) ? ComboDetailColor.Value.ToString() : "";
            variantDetail.ColorGroup = (ComboGroupColor.Value != null) ? ComboGroupColor.Value.ToString() : "";
            variantDetail.SizeDetail = (ComboDetailSize.Value != null) ? ComboDetailSize.Value.ToString() : "";
            variantDetail.SizeGroup = (ComboGroupSize.Value != null) ? ComboGroupSize.Value.ToString() : "";
            variantDetail.StyleDetail = (ComboDetailStyle.Value != null) ? ComboDetailStyle.Value.ToString() : "";
            variantDetail.StyleGroup = (ComboGroupStyle.Value != null) ? ComboGroupStyle.Value.ToString() : "";

            message = CMSfunction.updateVariantDetail(variantDetail, Session["UserID"].ToString());
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            ProcessUpdate();
        }

        protected void ComboGroupColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DTColorDetail = CMSfunction.GetAllColorDetailbyColorGroup(ComboGroupColor.SelectedItem.Value.ToString());
            ComboDetailColor.DataSource = DTColorDetail;
            ComboDetailColor.ValueField = "Colrcolid";
            ComboDetailColor.ValueType = typeof(string);
            ComboDetailColor.TextField = "colrdesc";
            ComboDetailColor.DataBind();
        }

        protected void ComboGroupStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            DTStyleDetail = CMSfunction.GetAllStyleDetailbyStyleGroup(ComboGroupStyle.SelectedItem.Value.ToString());
            ComboDetailStyle.DataSource = DTStyleDetail;
            ComboDetailStyle.ValueField = "Stylstylid";
            ComboDetailStyle.ValueType = typeof(string);
            ComboDetailStyle.TextField = "styldesc";
            ComboDetailStyle.DataBind();
        }

        protected void ComboGroupSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            DTSizeDetail = CMSfunction.GetAllSizeDetailbySizeGroup(ComboGroupSize.SelectedItem.Value.ToString());
            ComboDetailSize.DataSource = DTSizeDetail;
            ComboDetailSize.ValueField = "sizeszid";
            ComboDetailSize.ValueType = typeof(string);
            ComboDetailSize.TextField = "sizedesc";
            ComboDetailSize.DataBind();
        }
    }
}