using System;
using System.Collections.Generic;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.Administration.ParameterManagement
{
    public partial class ParameterManagementDetailDetail : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        OutputMessage message = new OutputMessage();

        protected override void OnInit(EventArgs e)
        {
            if (Session["ParamDetailID"] == null || Session["ParamDetailEntry"] == null || Session["ParamDetailClass"] == null)
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
            if (Session["ParamDetailID"] == null || Session["ParamDetailEntry"] == null  || Session["ParamDetailClass"] == null)
            
            {
                Response.Redirect("ParameterManagementHeader.aspx");
            }
            else
            {
                if (!IsPostBack)
                {
                    ParameterDetail parDetail = new ParameterDetail();

                    parDetail.ID = Session["ParamDetailID"].ToString();
                    parDetail.Entry = Session["ParamDetailEntry"].ToString();
                    parDetail.SiteClass = Session["ParamDetailClass"].ToString();

                    parDetail = CMSfunction.GetParameterDetail(parDetail);
                    ASPxTextBoxDetailEntry.Text = parDetail.Entry;
                    ASPxTextBoxDetailParValChar1.Text = parDetail.Char1;
                    ASPxTextBoxDetailParValChar2.Text = parDetail.Char2;
                    ASPxTextBoxDetailParValChar3.Text = parDetail.Char3;
                    ASPxMemoComment.Text = parDetail.Comment;
                    ASPxTextBoxDetailLDesc.Text = parDetail.LongDescription;
                    ASPxTextBoxDetailParVal1.Text = parDetail.Number1.ToString();
                    ASPxTextBoxDetailParVal2.Text = parDetail.Number2.ToString();
                    ASPxTextBoxDetailParVal3.Text = parDetail.Number3.ToString();
                    ASPxTextBoxDetailParVal4.Text = parDetail.Number4.ToString();
                    ASPxTextBoxDetailSDesc.Text = parDetail.ShortDescription;

                    ASPxDateEditPar1.Value = parDetail.Date1.HasValue ? (object) parDetail.Date1 : "";

                    ASPxDateEditPar2.Value = parDetail.Date2.HasValue ? (object)parDetail.Date2 : "";

                    ASPxDateEditPar2.Value = parDetail.Date2.HasValue ? (object)parDetail.Date2 : "";
                    
                }
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
            Session.Remove("ParamDetailID");
            Session.Remove("ParamDetailEntry");
            Session.Remove("ParamDetailClass");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("ParameterManagementDetail.aspx");
            else

                Response.Redirect("ParameterManagementDetail.aspx");
            //Session.Remove("ParamHeaderID");
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            ProcessUpdate();

            LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            LabelMessage.Text = message.Message;

            LabelMessage.Visible = true;
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            ProcessUpdate();
            Response.Redirect("ParameterManagementDetail.aspx");
        }

        private void ProcessUpdate()
        {
            ParameterDetail parDetail = new ParameterDetail();

            parDetail.ID = Session["ParamDetailID"].ToString();
            parDetail.SiteClass = Session["ParamDetailClass"].ToString();

            parDetail.Entry = ASPxTextBoxDetailEntry.Text;
            parDetail.Char1 = ASPxTextBoxDetailParValChar1.Text;
            parDetail.Char2 = ASPxTextBoxDetailParValChar2.Text;
            parDetail.Char3 = ASPxTextBoxDetailParValChar3.Text;
            parDetail.Comment = ASPxMemoComment.Text;
            parDetail.LongDescription = ASPxTextBoxDetailLDesc.Text;

            parDetail.Number1 = !String.IsNullOrWhiteSpace(ASPxTextBoxDetailParVal1.Text)
                ? (int?) Int32.Parse(ASPxTextBoxDetailParVal1.Text)
                : null;

            parDetail.Number2 = !String.IsNullOrWhiteSpace(ASPxTextBoxDetailParVal2.Text)
                ? (int?)Int32.Parse(ASPxTextBoxDetailParVal2.Text)
                : null;

            parDetail.Number3 = !String.IsNullOrWhiteSpace(ASPxTextBoxDetailParVal3.Text)
                ? (int?)Int32.Parse(ASPxTextBoxDetailParVal3.Text)
                : null;

            parDetail.Number4 = !String.IsNullOrWhiteSpace(ASPxTextBoxDetailParVal4.Text)
                ? (int?)Int32.Parse(ASPxTextBoxDetailParVal4.Text)
                : null;

            parDetail.ShortDescription = ASPxTextBoxDetailSDesc.Text;

            parDetail.Date1 = ASPxDateEditPar1.Date != DateTime.MinValue ? (DateTime?) ASPxDateEditPar1.Date : null;
            parDetail.Date2 = ASPxDateEditPar2.Date != DateTime.MinValue ? (DateTime?)ASPxDateEditPar2.Date : null;
            parDetail.Date3 = ASPxDateEditPar3.Date != DateTime.MinValue ? (DateTime?)ASPxDateEditPar3.Date : null;



            message = CMSfunction.updateParameterDetail(parDetail, Session["UserID"].ToString(), Session["ParamCopy"].ToString());
        }



    }
}