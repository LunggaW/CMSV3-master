using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using Menu = KBS.KBS.CMSV3.DATAMODEL.Menu;

namespace KBS.KBS.CMSV3.Administration.AccessManagement
{
    public partial class AccessProfileDetail : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTAccessProfileDetail = new DataTable();
        private DataTable DTListMenu = new DataTable();
        private DATAMODEL.MenuProfileHeader menuProfile;
        private OutputMessage message = new OutputMessage();
        private List<DATAMODEL.AccessProfileDetail> ListCheckBox;

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/logins.aspx");
            }
            else if (Session["AccessProfileManagement"] == null)
            {
                Response.Redirect("AccessProfileHeader.aspx");
            }
            else
            {
                loadNavBar();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                DTListMenu = CMSfunction.GetMenuDropDownMenuProfileFiltered(Session["MenuProfile"].ToString());
                ComboBoxMenu.DataSource = DTListMenu;
                ComboBoxMenu.ValueField = "MENUMENUID";
                ComboBoxMenu.ValueType = typeof(string);
                ComboBoxMenu.TextField = "MENUMENUNM";
                ComboBoxMenu.DataBind();
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
        }


        protected void DelBtn_Click(object sender, EventArgs e)
        {
            //if (ASPxGridViewMenuProfileLink.FocusedRowIndex != -1)
            //{
            //    MenuProfileLink menuProfileLink = new MenuProfileLink();

            //    menuProfileLink.MenuID = ASPxGridViewMenuProfileLink.GetRowValues(ASPxGridViewMenuProfileLink.FocusedRowIndex, "MENU ID").ToString();
            //    menuProfileLink.MenuProfile = Session["MenuProfile"].ToString();

            //    OutputMessage message = new OutputMessage();

            //    message = CMSfunction.DeleteMenuProfileLink(menuProfileLink);

            //    LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            //    LabelMessage.Visible = true;
            //    LabelMessage.Text = message.Message;
            //}
        }

        protected void ASPxGridViewMenuProfileLink_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            //DTTable = CMSfunction.GetMenuExcludeMenuProfile(Session["MenuProfile"].ToString());

            //if (DTTable.Rows.Count == 0)
            //{
            //    LabelMessage.ForeColor =  Color.Red;

            //    LabelMessage.Visible = true;
            //    LabelMessage.Text = "No More Menu to be added";

            //}
            //else
            //{
            //    Response.Redirect("MenuProfileLinkManagementNew.aspx");
            //}
            
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            ProcessData();

            LabelMessage.Visible = true;
            LabelMessage.ForeColor = message.Code < 0 ? Color.Red : Color.Black;

            LabelMessage.Text = message.Message;
        }

        protected void BackhomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("AccessProfileHeader.aspx");
        }

        protected void ValidateBtn_Click(object sender, EventArgs e)
        {
            ProcessData();
            Response.Redirect("AccessProfileHeader.aspx");
        }

        protected void ComboBoxMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            DTAccessProfileDetail = CMSfunction.GetListBoxAccessProfileDetail(Session["Class"].ToString(), ComboBoxMenu.Value.ToString(), Session["AccessProfileManagement"].ToString());
            CheckBoxListData.DataSource = DTAccessProfileDetail;
            CheckBoxListData.DataValueField = "AccPermission";
            CheckBoxListData.DataTextField = "AccDesc";
            CheckBoxListData.DataBind();

            for (int i = 0; i < DTAccessProfileDetail.Rows.Count; i++)
                CheckBoxListData.Items[i].Selected = Convert.ToBoolean(Int32.Parse(DTAccessProfileDetail.Rows[i]["AccPermission"].ToString()));

        }

        private void ProcessData()
        {
            ListCheckBox = new List<DATAMODEL.AccessProfileDetail>();

            DATAMODEL.AccessProfileDetail accessProfDetail;

            foreach (ListItem item in CheckBoxListData.Items)
            {
                accessProfDetail = new DATAMODEL.AccessProfileDetail();

                accessProfDetail.Profile = Session["AccessProfileManagement"].ToString();

                accessProfDetail.MenuId = ComboBoxMenu.Value.ToString();

                accessProfDetail.Type = item.Selected ? "1" : "0";

                accessProfDetail.FunctionId = CMSfunction.GetParameterEntryFromLongDescription(item.Text, Session["Class"].ToString(), "4");

                CMSfunction.ProcessAccessProfileDetail(accessProfDetail, Session["UserID"].ToString());
            }

            
        }

        protected void CheckBoxListData_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }

        protected void EditBtn_Click(object sender, EventArgs e)
        {

        }
    }
}