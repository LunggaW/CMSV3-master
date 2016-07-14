using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using DevExpress.Web;
using KBS.KBS.CMSV3.DATAMODEL;
using KBS.KBS.CMSV3.FUNCTION;
using System.IO;

namespace KBS.KBS.CMSV3.Administration.License
{
    public partial class ImportLicense : System.Web.UI.Page
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private function CMSfunction = new function();
        private DataTable DTTable = new DataTable();
        private String MenuID = ConfigurationManager.AppSettings["ImportLicense"];
        private OutputMessage message = new OutputMessage();
        protected override void OnInit(EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Account/logins.aspx");
            }           
            else
            {
                loadNavBar();
                //loadButton(MenuID);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void UploadButton_Click(object sender, EventArgs e)
    {
        
        if (LicenseUpload.HasFile)
            try
            {
                    if (LicenseUpload.FileName == "License.txt")
                    {
                        LicenseUpload.SaveAs(Server.MapPath("CMS") + LicenseUpload.FileName);
                        /*
                        Info.Text = "File name: " +
                        LicenseUpload.PostedFile.FileName + "<br>" +
                        LicenseUpload.PostedFile.ContentLength + " kb<br>" + "Content type: " +
                        LicenseUpload.PostedFile.ContentType + "<br><b>Uploaded Successfully";
                        */
                        try
                        {   // Open the text file using a stream reader.
                            string a = Server.MapPath("CMSLicense.txt");
                            ResultFile.Text = System.IO.File.ReadAllText(a);
                            String ResultLicense = CMSfunction.Decrypt(ResultFile.Text);
                            if (ResultLicense == "License Not Valid")
                            {
                                ResultFile.Text = "License Not Valid";
                                string message = "License Not Valid";
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.Append("<script type = 'text/javascript'>");
                                sb.Append("window.onload=function(){");
                                sb.Append("alert('");
                                sb.Append(message);
                                sb.Append("')};");
                                sb.Append("</script>");
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                            }
                            else
                            {
                                string[] values = ResultLicense.Split("|".ToCharArray());
                                message = CMSfunction.License(ResultFile.Text, Session["Username"].ToString());
                                Info.Text = "License For : " + values[0] + "<br>" +
                                                  "Max Store   : " + values[1] + "<br>" +
                                                  "Valid Until : " + values[2] + "<br>" +
                                                  "" + values[3] + "<br>" +
                                                  "" + values[4] + "<br>" +
                                                  "" + values[5];
                            }
                            //string file = Path.GetFileNameWithoutExtension(a);
                            //string NewPath = a.Replace(file, file + DateTime.Today.ToString("ddMMMyy"));
                            //ResultSub.Text = ResultFile.Text.Remove(ResultFile.Text.Length - 10);
                        }
                        catch (Exception)
                        {
                            ResultFile.Text = "The file could not be read";
                            string message = "The file could not be read";
                            System.Text.StringBuilder sb = new System.Text.StringBuilder();
                            sb.Append("<script type = 'text/javascript'>");
                            sb.Append("window.onload=function(){");
                            sb.Append("alert('");
                            sb.Append(message);
                            sb.Append("')};");
                            sb.Append("</script>");
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                        }
                    }
                    else
                    {
                        string message = "Wrong License";
                        System.Text.StringBuilder sb = new System.Text.StringBuilder();
                        sb.Append("<script type = 'text/javascript'>");
                        sb.Append("window.onload=function(){");
                        sb.Append("alert('");
                        sb.Append(message);
                        sb.Append("')};");
                        sb.Append("</script>");
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
                    }


                }
            catch (Exception ex)
            {
                    Info.Text = "ERROR: " + ex.Message.ToString();
            }
        else
        {
                Info.Text = "You have not specified a file.";
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


    }
}