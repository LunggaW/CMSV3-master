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


namespace KBS.KBS.CMSV3.MasterData.SizeMasterManagement
{
    public partial class SizeDetailMasterManagementNew : System.Web.UI.Page
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
            if (!IsPostBack)
            { Session["SizeSave"] = ""; }
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
            TextBoxSizeOrder.Text = "";
            TextBoxSDesc.Text = "";
            TextBoxLDesc.Text = "";
            //ASPxTextBoxSClass.Text = "";
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            //Session.Remove("ParamHeaderIDforUpdate");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("SizeDetailMasterManagement.aspx");
            else

                Response.Redirect("SizeDetailMasterManagement.aspx");
            //Session.Remove("ParamHeaderID");
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            String SizeGroup = Session["SizeIDforUpdate"].ToString();
            //String ParamSClas = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SCLAS").ToString();
            string CekData = CMSfunction.cekSize(SizeGroup, TextBoxId.Text, TextBoxSizeOrder.Text);

            if (CekData == "NO")
            {
                string script = "alert('Size Order Already Exists, please try another Size order');";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
            }
            else
            {
                ProcessInsert();

                ASPxLabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                if (message.Code > 0)
                { Session["SizeSave"] = "Sukses"; }
                ASPxLabelMessage.Visible = true;
                ASPxLabelMessage.Text = message.Message;
            }
           
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            String SizeGroup = Session["SizeIDforUpdate"].ToString();
            //String ParamSClas = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SCLAS").ToString();
            string CekData = CMSfunction.cekSize(SizeGroup, TextBoxId.Text, TextBoxSizeOrder.Text);

            if (Session["SizeSave"].ToString() != "Sukses")
            {
                if (CekData == "NO")
                {
                    string script = "alert('Size Group ID Already Exists, please try another Size order');";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
                }
                else
                {
                    ProcessInsert();
                    if (message.Code > 0)
                    { Response.Redirect("SizeDetailMasterManagement.aspx"); }
                }
            }
            else
            {
                Response.Redirect("SizeDetailMasterManagement.aspx");

            }
            
            
        }

        private void ProcessInsert()
        {
            SizeGroupDetail sizegroupdetail = new SizeGroupDetail();
            sizegroupdetail.GID = Session["SizeIDforUpdate"].ToString();
            sizegroupdetail.ID = TextBoxId.Text;
            sizegroupdetail.SizeOrder = TextBoxSizeOrder.Text;
            sizegroupdetail.SizeSDesc = TextBoxSDesc.Text;
            sizegroupdetail.SizeLDesc = TextBoxLDesc.Text;

            
            message = CMSfunction.InsertSizeDetail(sizegroupdetail, Session["UserID"].ToString());

        }

    }
}