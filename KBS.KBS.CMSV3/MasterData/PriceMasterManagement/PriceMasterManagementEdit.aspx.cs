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

namespace KBS.KBS.CMSV3.MasterData.PriceMasterManagement
{
    public partial class PriceMasterManagementEdit : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        OutputMessage message = new OutputMessage();

        protected override void OnInit(EventArgs e)
        {

            if (Session["PriceIDforUpdate"] == null)
            {
                Response.Redirect("PriceMasterManagementHeader.aspx");
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
                PriceGroup pricegroup = new PriceGroup();                

                pricegroup.ItemID = Session["PriceIDforUpdate"].ToString();
                pricegroup.VariantID = Session["PriceVarforUpdate"].ToString();
                pricegroup.Site = Session["PriceSiteforUpdate"].ToString();
                pricegroup.Price = Session["PriceforUpdate"].ToString();
                pricegroup.VAT = Session["PriceVATforUpdate"].ToString();
                pricegroup.Edate = Convert.ToDateTime(Session["PriceEDateforUpdate"].ToString());
                pricegroup.SDate = Convert.ToDateTime(Session["PriceSDateforUpdate"].ToString());
                ITEMIDTXT.Text = pricegroup.ItemID;
                VARIANTTXT.Text = pricegroup.VariantID;
                SITETXT.Text = pricegroup.Site;
                VATBOX.Checked = Convert.ToBoolean(int.Parse(pricegroup.VAT));                
                PRICETXT.Text = pricegroup.Price;
                SDATE.Text = pricegroup.SDate.ToString();
                EDATE.Text = pricegroup.Edate.ToString();
                EDATE.Date = Convert.ToDateTime(Session["PriceEDateforUpdate"].ToString());
                SDATE.Date = Convert.ToDateTime(Session["PriceSDateforUpdate"].ToString());
        

                
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
            ITEMIDTXT.Text = "";
            VARIANTTXT.Text = "";
            SITETXT.Text = "";
            PRICETXT.Text = "";
            VATBOX.Checked = false;
            EDATE.Value = "";
            SDATE.Value = "";
            EDATE.Text = "";
            SDATE.Text = "";
     
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            Session.Remove("PriceIDforUpdate");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("PriceMasterManagementHeader.aspx");
            else

                Response.Redirect("PriceMasterManagementHeader.aspx");
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
            Response.Redirect("PriceMasterManagementHeader.aspx");
        }


        private void ProcessUpdate()
        {
            PriceGroup pricegroup = new PriceGroup();

            pricegroup.ItemID = ITEMIDTXT.Text;
            pricegroup.VariantID = VARIANTTXT.Text;
            pricegroup.Site = SITETXT.Text;
            pricegroup.Price = PRICETXT.Text;
            pricegroup.VAT = Convert.ToInt32(VATBOX.Checked).ToString();
            pricegroup.Edate = EDATE.Date != DateTime.MinValue ? (DateTime?)EDATE.Date : null;
            pricegroup.SDate = SDATE.Date != DateTime.MinValue ? (DateTime?)SDATE.Date : null;
            message = CMSfunction.updatePriceHeader(pricegroup, Session["UserID"].ToString());


            //message = CMSfunction.updateBrandHeader(brandgroup, Session["UserID"].ToString());
        }

    }
}
