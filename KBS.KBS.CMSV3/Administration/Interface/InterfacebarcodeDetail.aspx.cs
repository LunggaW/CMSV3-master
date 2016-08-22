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
    public partial class InterfaceBarcodeDetail : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DATAMODEL.AccessProfileHeader accessProfile;    
        private OutputMessage message = new OutputMessage();   
        private User user;

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/logins.aspx");
            }
            else if (Session["InterfaceBarcodeRowID"] == null)
            {
                Response.Redirect("InterfaceBarcode.aspx");
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
                BarcodeMaster barcodeMaster = new BarcodeMaster();


                barcodeMaster = CMSfunction.GetBarcodeIntFromRowID(Session["InterfaceBarcodeRowID"].ToString());

                TextBoxIntItemID.Text = barcodeMaster.ItemID;
                TextBoxIntBarcode.Text = barcodeMaster.Barcode;
                TextBoxIntStatus.Text = barcodeMaster.Status;
                TextBoxIntType.Text = barcodeMaster.Type;
                TextBoxIntVariantID.Text = barcodeMaster.VariantID;


                DateStart.Value = barcodeMaster.StartDate.HasValue ? (object)barcodeMaster.StartDate : "";
                DateEnd.Value = barcodeMaster.EndDate.HasValue ? (object)barcodeMaster.EndDate : "";



                
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
            message = CMSfunction.resetIntBarcode(Session["InterfaceBarcodeRowID"].ToString(), Session["UserID"].ToString());

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

            Response.Redirect("InterfaceBarcode.aspx");
        }

        private void ProcessUpdate()
        {
            BarcodeMaster barcodeMaster = new BarcodeMaster();


            barcodeMaster.Barcode = TextBoxIntBarcode.Text;
            barcodeMaster.ItemID = TextBoxIntItemID.Text;
            barcodeMaster.Status = TextBoxIntStatus.Text;
            barcodeMaster.Type = TextBoxIntType.Text;
            barcodeMaster.VariantID = TextBoxIntVariantID.Text;

            barcodeMaster.StartDate = DateStart.Date;
            barcodeMaster.EndDate = DateEnd.Date;

            //To be Checked brand description
            message = CMSfunction.updateIntBarcode(barcodeMaster, Session["InterfaceBarcodeRowID"].ToString(), Session["UserID"].ToString());
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("InterfaceBarcode.aspx");
        }
        
    }
}