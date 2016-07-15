using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.SalesManagement
{
    public partial class SalesValidationHeader : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTSalesHeader = new DataTable();
        private DataTable DTParameterDetail = new DataTable();
        private DataTable DTGridViewUser = new DataTable();
        OutputMessage message = new OutputMessage();
        private User user;
        private String MenuID = ConfigurationManager.AppSettings["MenuIdSalesValidation"];

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
            SalesHeader salesheader = new SalesHeader();
            DTSalesHeader = CMSfunction.GetSalesHeaderDataTable(salesheader, 1);
            ASPxGridViewHeader.DataSource = DTSalesHeader;
            ASPxGridViewHeader.KeyFieldName = "TRANSACTION ID";
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
            Session["INPUTSALESID"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "TRANSACTION ID").ToString();
            Session["INPUTIID"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "INTERNAL ID").ToString();
            Session["INPUTNOTA"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "NOTA").ToString();
            Session["INPUTRECEIPTID"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "RECEIPT ID").ToString();
            Session["INPUTDATE"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "DATE").ToString();
            Session["INPUTSITE"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SITE").ToString();
            Session["INPUTCOMMENT"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "COMMENT").ToString();

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("SalesValidationEdit.aspx");
            else

                Response.Redirect("SalesValidationEdit.aspx");
         
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            ColorGroup color = new ColorGroup();


            //color.ID = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderID.Text) ? ASPxTextBoxHeaderID.Text : "";
            //color.Color = !string.IsNullOrWhiteSpace(ASPxTextBoxHeaderName.Text) ? ASPxTextBoxHeaderName.Text : "";

            DTSalesHeader = CMSfunction.GetColorHeaderDataTable(color);
            ASPxGridViewHeader.DataSource = DTSalesHeader;
            ASPxGridViewHeader.KeyFieldName = "ID";
            ASPxGridViewHeader.DataBind();
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            
            //ASPxTextBoxHeaderID.Text = ""; ASPxTextBoxHeaderName.Text = "";
            
        }


        protected void ASPxButtonEntry_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {

                Session["INPUTSALESID"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "TRANSACTION ID").ToString();
                Session["INPUTIID"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "INTERNAL ID").ToString();
                Session["INPUTNOTA"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "NOTA").ToString();
                Session["INPUTRECEIPTID"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "RECEIPT ID").ToString();
                Session["INPUTDATE"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "DATE").ToString();
                Session["INPUTSITE"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SITE").ToString();
 
                Response.Redirect("SalesDetailValidation.aspx");
            }


        }
        protected void Reject_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                SalesInputDetail salesinputdetail = new SalesInputDetail();
                salesinputdetail.SALESID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "TRANSACTION ID").ToString();
                salesinputdetail.IID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "INTERNAL ID").ToString();
                salesinputdetail.NOTA = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "NOTA").ToString();
                salesinputdetail.RECEIPTID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "RECEIPT ID").ToString();
                salesinputdetail.COMMENT = "'2'";


                salesinputdetail = CMSfunction.CekComment(salesinputdetail);

                if (salesinputdetail.COMMENT == "NO")
                {

                    String alert = "Please Fill Reject Comment ";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(alert);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

                }
                else
                {
                    salesinputdetail = new SalesInputDetail();
                    salesinputdetail.SALESID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "TRANSACTION ID").ToString();
                    salesinputdetail.IID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "INTERNAL ID").ToString();
                    salesinputdetail.NOTA = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "NOTA").ToString();
                    salesinputdetail.RECEIPTID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "RECEIPT ID").ToString();
                    salesinputdetail.COMMENT = "'2'";


                    salesinputdetail = CMSfunction.Cekdata(salesinputdetail);

                    if (salesinputdetail.COMMENT == "NO")
                    {

                        String alert = "Mohon cek data karena sudah terdapat detail yang status Valid ";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(alert);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

                    }
                    else
                    {

                        SalesHeader salesheader = new SalesHeader();
                        salesheader.SALESID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "TRANSACTION ID").ToString();
                        salesheader.IID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "INTERNAL ID").ToString();
                        salesheader.NOTA = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "NOTA").ToString();
                        salesheader.RECEIPTID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "RECEIPT ID").ToString();
                        salesheader.DATE = DateTime.Parse(ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "DATE").ToString());
                        salesheader.SITE = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SITE").ToString();

                        message = CMSfunction.UpdateStatusSales(salesheader, Session["UserID"].ToString(), 3);
                    }
                }
            }


        }
        protected void Validate_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                SalesInputDetail salesinputdetail = new SalesInputDetail();
                salesinputdetail.SALESID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "TRANSACTION ID").ToString();
                salesinputdetail.IID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "INTERNAL ID").ToString();
                salesinputdetail.NOTA = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "NOTA").ToString();
                salesinputdetail.RECEIPTID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "RECEIPT ID").ToString();
                salesinputdetail.COMMENT = "'3'";
                salesinputdetail = CMSfunction.Cekdata(salesinputdetail);

                if (salesinputdetail.COMMENT == "NO")
                {

                    String alert = "Mohon cek data karena masih terdapat detail yang status Reject ";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(alert);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

                }
                else
                {

                    SalesHeader salesheader = new SalesHeader();
                    salesheader.SALESID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "TRANSACTION ID").ToString();
                    salesheader.IID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "INTERNAL ID").ToString();
                    salesheader.NOTA = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "NOTA").ToString();
                    salesheader.RECEIPTID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "RECEIPT ID").ToString();
                    salesheader.DATE = DateTime.Parse(ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "DATE").ToString());
                    salesheader.SITE = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SITE").ToString();

                    message = CMSfunction.UpdateStatusSales(salesheader, Session["UserID"].ToString(), 2);

                    String alert = "Sales Berhasil Di Validasi ";
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("<script type = 'text/javascript'>");
                    sb.Append("window.onload=function(){");
                    sb.Append("alert('");
                    sb.Append(alert);
                    sb.Append("')};");
                    sb.Append("</script>");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                }
            }


        }
        protected void AddBtn_Click(object sender, EventArgs e)
        {
            //Response.Redirect("SalesInputNew.aspx");
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                SalesHeader salesheader = new SalesHeader();
                salesheader.SALESID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "TRANSACTION ID").ToString();
                salesheader.IID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "INTERNAL ID").ToString();
                salesheader.NOTA = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "NOTA").ToString();
                salesheader.RECEIPTID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "RECEIPT ID").ToString();
                salesheader.DATE = DateTime.Parse(ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "DATE").ToString());
                salesheader.SITE = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SITE").ToString();

                message = CMSfunction.UpdateStatusSales(salesheader, Session["UserID"].ToString(), 4);
            }

            Response.Redirect("SalesValidationHeader.aspx");
        }



    }

}