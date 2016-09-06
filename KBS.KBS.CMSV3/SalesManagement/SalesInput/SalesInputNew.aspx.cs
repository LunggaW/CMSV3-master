
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
    public partial class SalesInputNew : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTColor = new DataTable();
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

            COMMENTXT.Text = "";
            TDATE.Text = "";
            NOTATXT.Text = "";            
            //ASPxTextBoxSClass.Text = "";
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            //Session.Remove("ParamHeaderIDforUpdate");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("SalesInputHeader.aspx");
            else

                Response.Redirect("SalesInputHeader.aspx");
            //Session.Remove("ParamHeaderID");
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            ProcessInsert();

            ASPxLabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            ASPxLabelMessage.Visible = true;
            ASPxLabelMessage.Text = message.Message;
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            ProcessInsert();
            Response.Redirect("SalesInputHeader.aspx");
        }

        private void ProcessInsert()
        {
            
            SalesHeader salesheader = new SalesHeader();
            salesheader.COMMENT = COMMENTXT.Text;
            salesheader.DATE = DateTime.Parse(TDATE.Value.ToString());
            salesheader.NOTA = NOTATXT.Text;
            salesheader.STATUS = 0; //Created
            salesheader.FLAG = 1; //Sales
            salesheader.SITE = Session["DefaultSite"].ToString();
            salesheader.RECEIPTID = "WebApp";
            salesheader.SALESID = "def";
            salesheader.IID = "98798";

            message = CMSfunction.InsertSalesInputHeader(salesheader, Session["UserID"].ToString());

        }

    }
}
