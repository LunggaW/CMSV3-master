using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.TransferOrder
{
    public partial class TOReceiveHeader : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTTransferReceive = new DataTable();
        private DataTable DTParameterDetail = new DataTable();
        private DataTable DTGridViewUser = new DataTable();
        private User user;
        OutputMessage message = new OutputMessage();
        private String MenuID = ConfigurationManager.AppSettings["MenuIdTransferOrderReceive"];

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/Logins.aspx");
            }
            else
            {
                loadNavBar();
                loadButton(MenuID);
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            TransferOrderHeader TOHeader = new TransferOrderHeader();
            DTTransferReceive = CMSfunction.GetTOShipmentHeaderDataTable(TOHeader, 2);
            ASPxGridViewHeader.DataSource = DTTransferReceive;
            ASPxGridViewHeader.KeyFieldName = "TRANSFER ID";
            ASPxGridViewHeader.DataBind();            

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

        private void loadButton(String MenuID)
        {

            List<AccessContainer> listAccessCont =
                CMSfunction.SelectAccessByProfileAndMenuID(Session["AccessProfile"].ToString(), MenuID);

            foreach (var accessContainer in listAccessCont)
            {
                switch (accessContainer.FunctionId)
                {
                    case "1":
                        AddBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        break;
                    case "2":
                        //Ed.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        if (accessContainer.Type == "0")
                        {
                            ASPxGridViewHeader.ClientSideEvents.RowDblClick = null;
                        }

                        break;
                    case "3":
                        SearchBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        break;
                    case "4":
                        DelBtn.Enabled = Convert.ToBoolean(Convert.ToInt32(accessContainer.Type));
                        break;
                    default:
                        break;

                }
            }
        }
        protected void ASPxGridViewHeader_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {

            Session["TOShipIDforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "TRANSFER ID").ToString();
            //Session["TGrpforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "COLOR").ToString();
            

            //if (Page.IsCallback)
           //     ASPxWebControl.RedirectOnCallback("TOReceiveHeader.aspx");
         //   else

             //   Response.Redirect("TOReceiveHeader.aspx");
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            //ColorGroup color = new ColorGroup();


            //color.ID = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderID.Text) ? ASPxTextBoxHeaderID.Text : "";
            //color.Color = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderName.Text) ? ASPxTextBoxHeaderName.Text : "";

            //DTTransferReceive = CMSfunction.GetColorHeaderDataTable(color);
            //ASPxGridViewHeader.DataSource = DTTransferReceive;
            //ASPxGridViewHeader.KeyFieldName = "ID";
            //ASPxGridViewHeader.DataBind();
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            
            
            
        }


        protected void ASPxButtonEntry_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                Session["TOReceiveIDforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "TRANSFER ID").ToString();
                Session["TOReceiveIIDforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "INTERNAL ID").ToString();                    
                Response.Redirect("TOReceiveDetail.aspx");
            }


        }
        protected void Receive_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                TransferOrderDetail transferorderdetail = new TransferOrderDetail();
                transferorderdetail.ID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "TRANSFER ID").ToString();
                transferorderdetail.IID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "INTERNAL ID").ToString();
                
                message = CMSfunction.UpdateStatus(transferorderdetail, Session["UserID"].ToString(), 3);
            }
            Response.Redirect("TOReceiveHeader.aspx");

        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
          //  Response.Redirect("TOShipmentNew.aspx");
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                String ParamHeaderID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
                String ParamSClas = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SCLAS").ToString();


                OutputMessage message = new OutputMessage();

                //message = CMSfunction.DeleteParameterHeader(ParamHeaderID, ParamSClas);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;
            }
        }



    }

}