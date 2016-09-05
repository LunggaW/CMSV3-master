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

namespace KBS.KBS.CMSV3.Administration
{
    public partial class ParameterManagementDetailNew : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        OutputMessage message = new OutputMessage();

        protected override void OnInit(EventArgs e)
        {
            if (Session["ParamHeaderID"] == null)
            {
                Response.Redirect("ParameterManagementHeader.aspx");
            }
            else
            {
                LabelMessage.Visible = false;
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
            Session.Remove("ParamDetailIDNew");
            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("ParameterManagementDetail.aspx");
            else

                Response.Redirect("ParameterManagementDetail.aspx");
            //Session.Remove("ParamHeaderID");
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            ProcessInsert();

            LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;
            
            LabelMessage.Visible = true;
            LabelMessage.Text = message.Message;
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            ProcessInsert();
            Response.Redirect("ParameterManagementDetail.aspx");
        }

        private void ProcessInsert()
        {
            ParameterDetail parDetail = new ParameterDetail();



            parDetail.Entry = ASPxTextBoxDetailEntry.Text;
            parDetail.Char1 = ASPxTextBoxDetailParValChar1.Text;
            parDetail.Char2 = ASPxTextBoxDetailParValChar2.Text;
            if (Session["ParamHeaderID"].ToString() == "17")
            {
                parDetail.Char3 = ASPxTextBoxDetailParVal3.Text;
            }
            else
            {
                parDetail.Char3 = ASPxTextBoxDetailParValChar3.Text;
            }
            parDetail.Comment = ASPxMemoComment.Text;
            parDetail.SiteClass = Session["ParamHeaderSClass"].ToString();
            parDetail.ID = Session["ParamHeaderID"].ToString();
            parDetail.LongDescription = ASPxTextBoxDetailLDesc.Text;

            parDetail.Number1 = !string.IsNullOrWhiteSpace(ASPxTextBoxDetailParVal1.Text)
                ? (int?) Int32.Parse(ASPxTextBoxDetailParVal1.Text)
                : null;

            parDetail.Number2 = !string.IsNullOrWhiteSpace(ASPxTextBoxDetailParVal2.Text).
               ? (int?)Int32.Parse(ASPxTextBoxDetailParVal2.Text)
               : null;

            parDetail.Number3 = !string.IsNullOrWhiteSpace(ASPxTextBoxDetailParVal3.Text)
               ? (int?)Int32.Parse(ASPxTextBoxDetailParVal3.Text)
               : null;

            parDetail.Number4 = !string.IsNullOrWhiteSpace(ASPxTextBoxDetailParVal4.Text)
               ? (int?)Int32.Parse(ASPxTextBoxDetailParVal4.Text)
               : null;

            parDetail.ShortDescription = ASPxTextBoxDetailSDesc.Text;
            //parDetail.SiteClass = aspxtext.Text;

            parDetail.Date1 = ASPxDateEditPar1.Date != DateTime.MinValue ? (DateTime?) ASPxDateEditPar1.Date : null;
            parDetail.Date2 = ASPxDateEditPar2.Date != DateTime.MinValue ? (DateTime?)ASPxDateEditPar2.Date : null;
            parDetail.Date3 = ASPxDateEditPar3.Date != DateTime.MinValue ? (DateTime?)ASPxDateEditPar3.Date : null;

           
            
            message = CMSfunction.InsertParameterDetail(parDetail, Session["UserID"].ToString(), Session["ParamCopy"].ToString());
        }
    }
}