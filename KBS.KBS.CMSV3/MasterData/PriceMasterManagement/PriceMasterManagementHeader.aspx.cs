using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.MasterData
{
    public partial class PriceMasterManagementHeader : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTPrice = new DataTable();
        private DataTable DTParameterDetail = new DataTable();
        private DataTable DTGridViewUser = new DataTable();
        private User user;
        private String MenuID = ConfigurationManager.AppSettings["MenuIdPriceMasterManagement"];

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
            if (!IsPostBack)
            {
                PriceGroup pricegroup = new PriceGroup();                
                DTPrice = CMSfunction.GetPriceHeaderDataTable(pricegroup);

                ASPxGridViewHeader.DataSource = DTPrice;
                ASPxGridViewHeader.KeyFieldName = "ITEM ID";
                ASPxGridViewHeader.DataBind();
            }
            string Compare = Session["Filter"].ToString();
            if (Compare != "")
            {
                if (Session["Filter"].ToString() != "PriceMaster")
                {
                    Administration.UserManagement.UserManagementHeader Sunda = new Administration.UserManagement.UserManagementHeader();
                    Sunda.ClearDataSeasson();
                }
                else
                {
                    try
                    { 
                        if (Session["PriceFilter"].ToString() == "True")
                        {
                            ITEMIDTXT.Text = Session["FPItemId"].ToString();
                            VARIANTTXT.Text = Session["FPVariant"].ToString();
                            SITETXT.Text = Session["FPSite"].ToString();
                            PRICETXT.Text = Session["FPPrice"].ToString();
                            VATTXT.Text = Session["FPVAT"].ToString();
                            Session.Remove("FPItemId");
                            Session.Remove("FPVariant");
                            Session.Remove("PriceFilter");
                            Session.Remove("FPSite");
                            Session.Remove("FPPrice");
                            Session.Remove("FPVAT");
                            Session.Remove("FPriceEDate");
                            Session.Remove("FPriceSDate");
                            // Session["FPriceEDate"] = EDATE.Date != DateTime.MinValue ? (DateTime?)EDATE.Date : null;
                            // Session["FPriceSDate"] = SDATE.Date != DateTime.MinValue ? (DateTime?)SDATE.Date : null;
                        }
                    }
                        catch
                    {
                        String Data = "Not Found";
                    }
            }
                    
               

            }
            SearchBtn_Click();
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

            
            Session["PriceIDforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ITEM ID").ToString();
            Session["PriceVarforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "VARIANT ID").ToString();
            Session["PriceSiteforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SITE").ToString();
            Session["PriceVATforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "VAT").ToString();
            Session["PriceforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "PRICE").ToString();
            Session["PriceSDateforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "START DATE").ToString();
            Session["PriceEDateforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "END DATE").ToString();
            

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("PriceMasterManagementEdit.aspx");
            else

                Response.Redirect("PriceMasterManagementEdit.aspx");
        }
        private void SearchBtn_Click()
        {
            PriceGroup pricegroup = new PriceGroup();
            pricegroup.ItemID = !string.IsNullOrWhiteSpace(ITEMIDTXT.Text) ? ITEMIDTXT.Text : "";
            pricegroup.VariantID = !string.IsNullOrWhiteSpace(VARIANTTXT.Text) ? VARIANTTXT.Text : "";
            pricegroup.Site = !string.IsNullOrWhiteSpace(SITETXT.Text) ? SITETXT.Text : "";
            pricegroup.Price = !string.IsNullOrWhiteSpace(PRICETXT.Text) ? PRICETXT.Text : "";
            pricegroup.VAT = !string.IsNullOrWhiteSpace(VATTXT.Text) ? VATTXT.Text : "";
            pricegroup.Edate = EDATE.Date != DateTime.MinValue ? (DateTime?)EDATE.Date : null;
            pricegroup.SDate = SDATE.Date != DateTime.MinValue ? (DateTime?)SDATE.Date : null;



            DTPrice = CMSfunction.GetPriceHeaderDataTable(pricegroup);
            ASPxGridViewHeader.DataSource = DTPrice;
            ASPxGridViewHeader.KeyFieldName = "ITEM ID";
            ASPxGridViewHeader.DataBind();


        }
        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            
            Session["Filter"] = "PriceMaster";
            Session["PriceFilter"] = "True";
            Session["FPItemId"] = !string.IsNullOrWhiteSpace(ITEMIDTXT.Text) ? ITEMIDTXT.Text : "";
            Session["FPVariant"] = !string.IsNullOrWhiteSpace(VARIANTTXT.Text) ? VARIANTTXT.Text : "";
            Session["FPSite"] = !string.IsNullOrWhiteSpace(SITETXT.Text) ? SITETXT.Text : "";
            Session["FPPrice"] = !string.IsNullOrWhiteSpace(PRICETXT.Text) ? PRICETXT.Text : "";
            Session["FPVAT"] = !string.IsNullOrWhiteSpace(VATTXT.Text) ? VATTXT.Text : "";
            Session["FPriceEDate"] = EDATE.Date != DateTime.MinValue ? (DateTime?)EDATE.Date : null;
            Session["FPriceSDate"] = SDATE.Date != DateTime.MinValue ? (DateTime?)SDATE.Date : null;

            SearchBtn_Click();
        }

        protected void ClearBtn_Click(object sender, EventArgs e)
        {
            
            ITEMIDTXT.Text = ""; 
            VARIANTTXT.Text = "";
            SITETXT.Text = "";
            PRICETXT.Text = "";
            VATTXT.Text = "";
            EDATE.Value = "";
            SDATE.Value = "";
            EDATE.Text = "";
            SDATE.Text = "";

            Session["FPItemId"] = "";
            Session["FPVariant"] = "";
            Session["FPSite"] = "";
            Session["FPPrice"] = "";
            Session["FPVAT"] = "";
            Session["FPriceEDate"] = "";
            Session["FPriceSDate"] = "";
            Session["PriceFilter"] = "False";
            SearchBtn_Click();

        }


        //protected void ASPxButtonEntry_Click(object sender, EventArgs e)
        //{
        //    if (ASPxGridViewHeader.FocusedRowIndex != -1)
        //    {

        //        Session["BrandIDforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ID").ToString();
        //        Session["BrandDescforUpdate"] = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "BRAND DESC").ToString();

        //        Response.Redirect("BrandDetailMasterManagement.aspx");
        //    }


        //}

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("PriceMasterManagementNew.aspx");
        }

        protected void DelBtn_Click(object sender, EventArgs e)
        {
            if (ASPxGridViewHeader.FocusedRowIndex != -1)
            {
                PriceGroup pricegroup = new PriceGroup();


                


                String ITEMID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "ITEM ID").ToString();
                String VARIANTID = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "VARIANT ID").ToString();
                String SITE = ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "SITE").ToString();
                DateTime SDATE = Convert.ToDateTime(ASPxGridViewHeader.GetRowValues(ASPxGridViewHeader.FocusedRowIndex, "START DATE").ToString());


                OutputMessage message = new OutputMessage();

                message = CMSfunction.DeletePrice(ITEMID, VARIANTID, SITE, SDATE);

                LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                LabelMessage.Visible = true;
                LabelMessage.Text = message.Message;
            }
            Response.Redirect("PriceMasterManagementHeader.aspx");
        }



    }

}