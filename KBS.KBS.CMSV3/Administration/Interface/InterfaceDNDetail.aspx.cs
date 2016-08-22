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
    public partial class InterfaceDNDetail : System.Web.UI.Page
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
            else if (Session["InterfaceDNRowID"] == null)
            {
                Response.Redirect("InterfaceDN.aspx");
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
                DeliveryNote DN = new DeliveryNote();


                DN = CMSfunction.GetDNIntFromRowID(Session["InterfaceDNRowID"].ToString());

                TextBoxIntItemID.Text = DN.ItemID;
                TextBoxIntBarcode.Text = DN.Barcode;
                TextBoxIntCMSID.Text = DN.CMSId;
                TextBoxIntDesc.Text = DN.Description;
                TextBoxIntPrice.Text = DN.Price.ToString();
                TextBoxIntQty.Text = DN.Qty.ToString();
                TextBoxIntStore.Text = DN.Store;
                TextBoxIntUserID.Text = DN.UserID;
                DateCreated.Date = DN.Date.GetValueOrDefault();
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
            message = CMSfunction.resetIntDN(Session["InterfaceDNRowID"].ToString());

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

            Response.Redirect("InterfaceDN.aspx");
        }

        private void ProcessUpdate()
        {
            DeliveryNote DN = new DeliveryNote();



            DN.ItemID = TextBoxIntItemID.Text;
            DN.Barcode = TextBoxIntBarcode.Text;
            DN.Description = TextBoxIntDesc.Text;
            DN.Price = Decimal.Parse(TextBoxIntPrice.Text);
            DN.Qty = Int32.Parse(TextBoxIntQty.Text);
            DN.Store = TextBoxIntStore.Text;
            DN.Date = DateCreated.Date;
            DN.UserID = TextBoxIntUserID.Text;
            DN.CMSId = TextBoxIntCMSID.Text;
            



            //To be Checked Site Flag
            message = CMSfunction.updateIntDN(DN, Session["InterfaceDNRowID"].ToString());
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("InterfaceDN.aspx");
        }
    }
}