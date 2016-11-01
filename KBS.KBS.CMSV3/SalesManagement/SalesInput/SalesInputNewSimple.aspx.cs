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


namespace KBS.KBS.CMSV3.SalesManagement.SalesInput
{
    public partial class SalesInputNewSimple : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTDetailInput = new DataTable();
        private DataTable DTSku= new DataTable();
        OutputMessage message = new OutputMessage();

        protected override void OnInit(EventArgs e)
        {
                if (Session["UserID"] == null)
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
            if (!IsPostBack)
            {

                //ComboSKU.Dispose();
                //DTSku = CMSfunction.GetSKULinkBoxNoBrand(Session["DefaultSite"].ToString() );
                //ComboSKU.DataSource = DTSku;
                //ComboSKU.ValueField = "VALUE";
                //ComboSKU.ValueType = typeof(string);
                //ComboSKU.TextField = "DESCRIPTION";
                //ComboSKU.DataBind();
            }
        }

        protected void Search(object sender, EventArgs e)
        {
            
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



        protected void ClearBtn_Click(object sender, EventArgs e)
        {

            TextBoxNormalPrice.Text = "";
            BARCODETXT.Text = "";
            QTYTXT.Text = "";
            
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(BARCODETXT.Text) || String.IsNullOrWhiteSpace(QTYTXT.Text))
            {
                ASPxLabelMessage.ForeColor = Color.Red;

                ASPxLabelMessage.Visible = true;
                ASPxLabelMessage.Text = "Quantity or Barcode is empty";
            }
            else
            {
                ProcessInsert();

                ASPxLabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                ASPxLabelMessage.Visible = true;
                ASPxLabelMessage.Text = message.Message;
            }
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
                        
            ProcessInsert();                    
        }

        private void ProcessInsert()
        {
            
            SalesInputSimple salesInputSimple = new SalesInputSimple();

            salesInputSimple.NormalPrice = (!String.IsNullOrWhiteSpace(TextBoxNormalPrice.Text)) ? TextBoxNormalPrice.Text : "0";
            salesInputSimple.BARCODE = BARCODETXT.Text;
            //salesInputSimple.SKU = (ComboSKU.Value != null) ? ComboSKU.Value.ToString() : "";
            salesInputSimple.SALESQTY = QTYTXT.Text;
            salesInputSimple.FinalPrice = (!String.IsNullOrWhiteSpace(ASPxTextBoxFinalPrice.Text)) ? ASPxTextBoxFinalPrice.Text : "0";
            salesInputSimple.DISCOUNT = (!String.IsNullOrWhiteSpace(TextBoxDiscount.Text)) ? Int32.Parse(TextBoxDiscount.Text) : 0 ;
            salesInputSimple.TransDate = (!String.IsNullOrWhiteSpace(ASPxDateEditTransDate.Date.ToLongDateString())) ? ASPxDateEditTransDate.Date: DateTime.Now;

            TextBoxNormalPrice.Text = "";
            BARCODETXT.Text = "";
            QTYTXT.Text = "";
            ASPxTextBoxFinalPrice.Text = "";
            TextBoxDiscount.Text = "";
            TextBoxNormalPrice.Text = "";

            message = CMSfunction.InsertSalesSimple(salesInputSimple, Session["UserID"].ToString(), Session["DefaultSite"].ToString(), 1);
            
        }

    }
}