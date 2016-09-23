using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;


namespace KBS.KBS.CMSV3.Administration.Interface
{
    public partial class InterfacePriceAssortment : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTInterface = new DataTable();
        private DATAMODEL.AccessProfileHeader accessProfile;       
        private User user;

        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/logins.aspx");
            }
            else
            {
                loadNavBar();
                
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        private void LoadDataGrid()
        {
            DTInterface = CMSfunction.GetInterfacePriceAssortment();
            ASPxGridViewHeader.DataSource = DTInterface;
            ASPxGridViewHeader.KeyFieldName = "ITEM ID";
            ASPxGridViewHeader.DataBind();

            ASPxGridViewHeader.Columns["ROWID"].Visible = false;
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

        protected void tbCurrentPassword_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void Execute_Click(object sender, EventArgs e)
        {
            CMSfunction.ExecuteSPProcessPrice();
            LoadDataGrid();
        }

        protected void Log_Click(object sender, EventArgs e)
        {

        }
        


        protected void Upload_Click(object sender, EventArgs e)
        {
            if (FileUploadPriceAssortment.HasFile == false)
            {
                // No file uploaded!
                //UploadDetails.Text = "Please first select a file to upload...";
            }
            else
            {
                try
                {
                    string filePath;

                    //filePath = 
                    //Server.MapPath(CMSfunction.getIncomingDirectory() + FileUploadSite.FileName);


                    filePath = CMSfunction.getIncomingDirectory() + FileUploadPriceAssortment.FileName;

                    FileUploadPriceAssortment.SaveAs(filePath);

                    OutputMessage message = new OutputMessage();

                    message = CMSfunction.ExecuteSPImportPriceAssortmentFile(FileUploadPriceAssortment.FileName);

                    LoadDataGrid();
                }
                catch (Exception ex)
                {
                    logger.Error("Error Mesage : "+ex.Message);
                    logger.Error("Inner Exception : " + ex.InnerException);
                    throw;
                }
                // Display the uploaded file's details
                //UploadDetails.Text = string.Format(
                //        @"Uploaded file: {0}<br />
                //  File size (in bytes): {1:N0}<br />
                //  Content-type: {2}",
                //          UploadTest.FileName,
                //          UploadTest.FileBytes.Length,
                //          UploadTest.PostedFile.ContentType);

                //// Save the file
               
            }
        }

        protected void ASPxGridViewHeader_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            Session["InterfacePriceAssortmentRowID"] = ASPxGridViewHeader.GetRowValues(Convert.ToInt32(e.Parameters), "ROWID").ToString();

            if (Page.IsCallback)
                ASPxWebControl.RedirectOnCallback("InterfacePriceAssortmentDetail.aspx");
            else
                Response.Redirect("InterfacePriceAssortmentDetail.aspx");
        }
    }
}