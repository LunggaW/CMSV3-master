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


namespace KBS.KBS.CMSV3.MasterData.BrandMasterManagement
{
    public partial class BrandMasterManagementNew : System.Web.UI.Page
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
            Session["SaveBrand"] = "0";
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
           
            ASPxTextBoxId.Text = "";
            BrandDesc.Text = "";            
            //ASPxTextBoxSClass.Text = "";
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            //Session.Remove("ParamHeaderIDforUpdate");

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("BrandMasterManagementHeader.aspx");
            else

                Response.Redirect("BrandMasterManagementHeader.aspx");
            //Session.Remove("ParamHeaderID");
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            string CekData = CMSfunction.cekBrand(ASPxTextBoxId.Text, BrandDesc.Text);

            if (CekData == "NO")
            {
                string script = "alert('Brand ID or Desc already exists, please try other ID or Desc');";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
            }
            else
            {
                ProcessInsert();
                if (message.Code >= 0)
                {
                    Session["SaveBrand"] = "OK";
                }
                
                ASPxLabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

                ASPxLabelMessage.Visible = true;
                ASPxLabelMessage.Text = message.Message;
            }
          

        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            string CekData = CMSfunction.cekBrand(ASPxTextBoxId.Text, BrandDesc.Text);
            if (Session["SaveBrand"] == "OK")
            {
                Response.Redirect("BrandMasterManagementHeader.aspx");                
            }
            else
            {
                if (CekData == "NO")
                {
                    string script = "alert('Brand ID or Desc already exists, please try other ID or Desc');";
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
                }
                else
                {
                    ProcessInsert();
                    if (message.Code >= 0)
                    {
                        Response.Redirect("BrandMasterManagementHeader.aspx");
                    }

                }
            }
        }

        private void ProcessInsert()
        {
            BrandGroup brandgroup = new BrandGroup();
            brandgroup.ID = ASPxTextBoxId.Text;
            brandgroup.Brand = BrandDesc.Text;
            if ((ASPxTextBoxId.Text == "") || (BrandDesc.Text == ""))
            {
                string script = "alert('Please Fill All Field');";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "Alert", script, true);
            }
            else
            {
                message = CMSfunction.InsertBrandGroup(brandgroup, Session["UserID"].ToString());
            }

        }

    }
}