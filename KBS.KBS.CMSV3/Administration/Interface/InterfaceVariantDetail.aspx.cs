using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using DevExpress.Data;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;

namespace KBS.KBS.CMSV3.Administration.Interface
{
    public partial class InterfaceVariantDetail : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTInterface = new DataTable();
        private DATAMODEL.AccessProfileHeader accessProfile;    
        private OutputMessage message = new OutputMessage();   
        private User user;

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/logins.aspx");
            }
            else if (Session["InterfaceVariantRowID"] == null)
            {
                Response.Redirect("InterfaceVariant.aspx");
            }
            else
            {
                loadNavBar();
                
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VariantDetail variantDetail = new VariantDetail();



                variantDetail = CMSfunction.GetVariantIntFromRowID(Session["InterfaceVariantRowID"].ToString());

                TextBoxIntItemID.Text = variantDetail.ItemID;
                TextBoxIntVariantID.Text = variantDetail.VariantID;
                TextBoxIntColorGrp.Text = variantDetail.ColorGroup;
                TextBoxIntColor.Text = variantDetail.ColorDetail;
                TextBoxIntSizeGroup.Text = variantDetail.SizeGroup;
                TextBoxIntSize.Text = variantDetail.SizeDetail;
                TextBoxIntStyleGrp.Text = variantDetail.StyleGroup;
                TextBoxIntStyle.Text = variantDetail.StyleDetail;
                
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

        protected void ButtonReset_Click(object sender, EventArgs e)
        {
            message = CMSfunction.resetIntVariant(Session["InterfaceVariantRowID"].ToString(), Session["UserID"].ToString());

            LabelMessage.Visible = true;
            LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            LabelMessage.Text = message.Message;
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            ProcessUpdate();

            LabelMessage.Visible = true;
            LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            LabelMessage.Text = message.Message;
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            ProcessUpdate();

            Response.Redirect("InterfaceVariant.aspx");
        }

        private void ProcessUpdate()
        {
            VariantDetail variantDetail  = new VariantDetail();



            variantDetail.ItemID = TextBoxIntItemID.Text;
            variantDetail.VariantID = TextBoxIntVariantID.Text;
            variantDetail.StyleGroup = TextBoxIntStyleGrp.Text;
            variantDetail.StyleDetail = TextBoxIntStyle.Text;
            variantDetail.SizeGroup = TextBoxIntSizeGroup.Text;
            variantDetail.SizeDetail = TextBoxIntSize.Text;
            variantDetail.ColorGroup = TextBoxIntColorGrp.Text;
            variantDetail.ColorDetail = TextBoxIntColor.Text;






            //To be Checked Site Flag
            message = CMSfunction.updateIntVariant(variantDetail, Session["InterfaceVariantRowID"].ToString(), Session["UserID"].ToString());
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("InterfaceVariant.aspx");
        }
    }
}