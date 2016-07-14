using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.SalesManagement
{
    public partial class SalesDetailInput : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSalesDetail = new DataTable();
        private DataTable DTParameterDetail = new DataTable();
        private DataTable DTGridViewUser = new DataTable();
        private User user;

        protected override void OnInit(EventArgs e)
        {
            if (Session["INPUTSALESID"] != null)
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
            else
            {
                Response.Redirect("~/Account/Logins.aspx");
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SalesInputDetail salesinputdetail = new SalesInputDetail();
            salesinputdetail.SALESID = Session["INPUTSALESID"].ToString();
            salesinputdetail.IID = Session["INPUTIID"].ToString();
            salesinputdetail.NOTA = Session["INPUTNOTA"].ToString();
            salesinputdetail.RECEIPTID = Session["INPUTRECEIPTID"].ToString();
            salesinputdetail.DATE = DateTime.Parse(Session["INPUTDATE"].ToString());
            salesinputdetail.SITE = Session["INPUTSITE"].ToString();
            String Status = "'0','3'";
            DTSalesDetail = CMSfunction.GetSalesInputDetailDataTable(salesinputdetail,Status);
            ASPxGridViewHeader.DataSource = DTSalesDetail;
            ASPxGridViewHeader.KeyFieldName = "LINE";
            ASPxGridViewHeader.DataBind();
            TRANSACTIONIDTXT.Text = Session["INPUTSALESID"].ToString();
            INTERNALIDTXT.Text = Session["INPUTIID"].ToString();
            NOTATXT.Text = Session["INPUTNOTA"].ToString();
            RECEIPTIDTXT.Text = Session["INPUTRECEIPTID"].ToString();
            DATETXT.Text = Session["INPUTDATE"].ToString();
            SITETXT.Text = Session["INPUTSITE"].ToString();

        }

        
        private void loadNavBar()
        {

            List<Menu> listMenuGroup = CMSfunction.SelectMenuGroupByProfileID(Session["SiteProfile"].ToString());
            int i = 0;

            ASPxSplitter sp = Master.Master.FindControl("ASPxSplitter1").FindControl("Content").FindControl("ContentSplitter") as ASPxSplitter;
            ASPxNavBar masterNav = sp.FindControl("ASPxNavBar1") as ASPxNavBar;
            if (masterNav != null)
            {
                foreach (var menuGroup in listMenuGroup)
                {

                    masterNav.Groups.Add(menuGroup.MenuGroupName, menuGroup.MenuGroupID);


                    List<Menu> listMenu = CMSfunction.SelectMenuByProfileIDandMenuGroup(Session["SiteProfile"].ToString(),
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

        protected void ASPxGridViewHeader_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            Session["INPUTLINE"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "LINE").ToString();
           

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("SalesDetailInputEdit.aspx");
            else

                Response.Redirect("SalesDetailInputEdit.aspx");
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            ColorGroupDetail color = new ColorGroupDetail();

        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
                                    
        }

        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.PageIndex <= ASPxGridViewHeader.PageCount - 1)
            {
                ASPxGridViewHeader.PageIndex = ASPxGridViewHeader.PageIndex + 1;
            }
        }
        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.PageIndex -1 >= 0)
            {
                ASPxGridViewHeader.PageIndex = ASPxGridViewHeader.PageIndex - 1;
            }
        }

        protected void LprevBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewHeader.PageIndex = 0;
        }

        protected void LnextBtn_Click(object sender, EventArgs e)
        {
            ASPxGridViewHeader.PageIndex = ASPxGridViewHeader.PageCount - 1;
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("SalesDetailInputNew.aspx");
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("SalesInputHeader.aspx");
        }


        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
               
                String LINE = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "LINE").ToString();


                OutputMessage message = new OutputMessage();

                message = CMSfunction.DeleteSalesDetail(Session["INPUTSALESID"].ToString(),Session["INPUTIID"].ToString(),Session["INPUTNOTA"].ToString(),Session["INPUTRECEIPTID"].ToString(),Session["INPUTSITE"].ToString(), LINE);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;
            }
            Response.Redirect("SalesDetailInput.aspx");
        }



    }

}