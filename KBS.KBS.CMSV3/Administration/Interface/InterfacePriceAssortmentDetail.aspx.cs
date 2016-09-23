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
    public partial class InterfacePriceAssortmentDetail : System.Web.UI.Page
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
            else if (Session["InterfacePriceAssortmentRowID"] == null)
            {
                Response.Redirect("InterfacePriceAssortment.aspx");
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
                PriceGroup priceGroup = new PriceGroup();
                
                priceGroup = CMSfunction.GetPriceAssortmentIntFromRowID(Session["InterfacePriceAssortmentRowID"].ToString());

                TextBoxIntItemID.Text = priceGroup.ItemID;
                TextBoxIntVariantID.Text = priceGroup.VariantID;
                TextBoxIntSite.Text = priceGroup.Site;
                TextBoxIntVAT.Text = priceGroup.VAT;
                TextBoxIntPrice.Text = priceGroup.Price;
                TextBoxIntStatus.Text = priceGroup.AssortmentStatus;

                DateStart.Value = priceGroup.SDate.HasValue ? (object)priceGroup.SDate : "";
                DateEnd.Value = priceGroup.Edate.HasValue ? (object)priceGroup.Edate : "";

                


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
            message = CMSfunction.resetIntPriceAssortment(Session["InterfacePriceAssortmentRowID"].ToString(), Session["UserID"].ToString());

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

            Response.Redirect("InterfacePriceAssortment.aspx");
        }

        private void ProcessUpdate()
        {
            PriceGroup price = new PriceGroup();

            price.ItemID = TextBoxIntItemID.Text;
            price.VariantID = TextBoxIntVariantID.Text;
            price.Site = TextBoxIntSite.Text;
            price.Price = TextBoxIntPrice.Text;
            price.VAT = TextBoxIntVAT.Text;
            price.SDate = DateStart.Date;
            price.Edate = DateEnd.Date;
            price.AssortmentStatus = TextBoxIntStatus.Text;
        
            
            //To be Checked Site Flag
            message = CMSfunction.updateIntPriceAssortment(price, Session["InterfacePriceAssortmentRowID"].ToString(), Session["UserID"].ToString());
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("InterfacePriceAssortment.aspx");
        }
    }
}