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
    public partial class SizeDetailMasterManagementEdit : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        OutputMessage message = new OutputMessage();

        protected override void OnInit(EventArgs e)
        {

            if (Session["SizeDetailIDforUpdate"] == null)
            {
                Response.Redirect("SizeDetailMasterManagement.aspx");
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
            if (!IsPostBack)
            {
                SizeGroupDetail sizegroupdetail = new SizeGroupDetail();

                sizegroupdetail.GID = Session["SizeDetailGrpforUpdate"].ToString();
                sizegroupdetail.ID = Session["SizeDetailIDforUpdate"].ToString();


                sizegroupdetail = CMSfunction.GetSizeDetailUpdate(sizegroupdetail);

                 TextBoxId.Text = Session["SizeDetailIDforUpdate"].ToString();
                TextBoxSizeOrder.Text = sizegroupdetail.SizeOrder;
                 TextBoxSDesc.Text = sizegroupdetail.SizeSDesc;
                 TextBoxLDesc.Text = sizegroupdetail.SizeLDesc;




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
            //Session.Remove("ParamHeaderIDforUpdate");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("SizeDetailMasterManagement.aspx");
            else

                Response.Redirect("SizeDetailMasterManagement.aspx");
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
            Response.Redirect("SizeDetailMasterManagement.aspx");
        }


        private void ProcessUpdate()
        {
            String SizeGroup = Session["SizeDetailGrpforUpdate"].ToString();
            String SizeID = Session["SizeDetailIDforUpdate"].ToString();
            //String ParamSClas = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SCLAS").ToString();
            string CekData = CMSfunction.cekSize(SizeGroup, SizeID, TextBoxSizeOrder.Text);

            if (CekData == "NO")
            {
                string script = "alert('Size Order Already Exists, please try another Size order');";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
            }
            else
            {
                SizeGroupDetail sizegroupdetail = new SizeGroupDetail();

                sizegroupdetail.GID = Session["SizeDetailGrpforUpdate"].ToString();
                sizegroupdetail.ID = Session["SizeDetailIDforUpdate"].ToString();
                sizegroupdetail.SizeOrder = TextBoxSizeOrder.Text;
                sizegroupdetail.SizeSDesc = TextBoxSDesc.Text;
                sizegroupdetail.SizeLDesc = TextBoxLDesc.Text;


                message = CMSfunction.UpdateSizeDetail(sizegroupdetail, Session["UserID"].ToString());
            }
            
        }

    }
}