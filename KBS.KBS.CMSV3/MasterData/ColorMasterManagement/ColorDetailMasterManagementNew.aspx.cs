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


namespace KBS.KBS.CMSV3.MasterData.ColorMasterManagement
{
    public partial class ColorDetailMasterManagementNew : System.Web.UI.Page
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
            Session["ColorSave"] = "";
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
           
            TextBoxId.Text = "";
            TextBoxColorOrder.Text = "";
            TextBoxSDesc.Text = "";
            TextBoxLDesc.Text = "";
            //ASPxTextBoxSClass.Text = "";
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            //Session.Remove("ParamHeaderIDforUpdate");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("ColorDetailMasterManagement.aspx");
            else

                Response.Redirect("ColorDetailMasterManagement.aspx");
            //Session.Remove("ParamHeaderID");
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            String ColorGroup = Session["ColorIDforUpdate"].ToString();
            //String ParamSClas = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SCLAS").ToString();
            string CekData = CMSfunction.cekColor(ColorGroup, TextBoxId.Text, TextBoxColorOrder.Text);

            if (CekData == "NO")
            {
                string script = "alert('Color Order Already Exists, please try another color order');";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
            }
            else
            {
                ProcessInsert();

                ASPxLabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;
                if (message.Code > 0)
                { Session["ColorSave"] = "Sukses"; }
                ASPxLabelMessage.Visible = true;
                ASPxLabelMessage.Text = message.Message;
            }
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            String ColorGroup = Session["ColorIDforUpdate"].ToString();
            //String ParamSClas = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SCLAS").ToString();
            string CekData = CMSfunction.cekColor(ColorGroup, TextBoxId.Text, TextBoxColorOrder.Text);

            if (Session["ColorSave"].ToString() != "Sukses")
            {
                if (CekData == "NO")
                {
                    string script = "alert('Color Group ID Already Exists, please try another color order');";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
                }
                else
                {
                    ProcessInsert();
                    Response.Redirect("ColorDetailMasterManagement.aspx");
                }
            }
            else
            {
                ProcessInsert();
                if (message.Code > 0)
                { Response.Redirect("ColorDetailMasterManagement.aspx"); }

            }
        }

        private void ProcessInsert()
        {
            ColorGroupDetail colorgroupdetail = new ColorGroupDetail();
            colorgroupdetail.GID = Session["ColorIDforUpdate"].ToString();
            colorgroupdetail.ID = TextBoxId.Text;
            colorgroupdetail.ColorOrder = TextBoxColorOrder.Text;
            colorgroupdetail.ColorSDesc = TextBoxSDesc.Text;
            colorgroupdetail.ColorLDesc = TextBoxLDesc.Text;

            
            message = CMSfunction.InsertColorDetail(colorgroupdetail, Session["UserID"].ToString());

        }

    }
}