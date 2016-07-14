using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KBS.KBS.CMSV3.FUNCTION;

namespace KBS.KBS.CMSV3.Mobile
{
    public partial class TimeSheetView : System.Web.UI.Page
    {
        DataTable DTTimeSheetEntry = new DataTable();
        //TimeSheetEntryPlaceHolder timeSheetEntry = new TimeSheetEntryPlaceHolder();
        function function = new function();
        string format = "ddmmyyyy";
        private DateTime dateTimeFrom  = DateTime.Now;
        private DateTime dateTimeTo = DateTime.Now;

        protected void Page_Load(object sender, EventArgs e)
        {
            //LabelDataExist.Visible = false;
            if (Session["EMPID"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            else
            {
                
                updateDataGridTimeSheet();
                
            }

        }

        private void updateDataGridTimeSheet()
        {
            //DTTimeSheetEntry = function.SelectTimeSheetEntrybyUserID(Session["USRID"].ToString(), dateTimeFrom, dateTimeTo);
            
            //if (DTTimeSheetEntry.Rows.Count == 0)
            //{
            //    LabelDataExist.Visible = true;
            //}
            //else 
            //{
            //    GridViewTimeSheet.DataSource = DTTimeSheetEntry;
            //    GridViewTimeSheet.DataBind();
            //    //var viewColumn = this.GridViewTimeSheet.Columns[1];
            //    //if (viewColumn != null)
            //    //    viewColumn.Visible = false;
            //}
            
        }

        protected void GridViewTimeSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            //GridViewRow row = GridViewTimeSheet.SelectedRow;
            //String TMSID;

            //Session["TMSID"] = GridViewTimeSheet.DataKeys[GridViewTimeSheet.SelectedRow.RowIndex]["TMSID"].ToString();
            //Response.Redirect("TimeSheetUpdate.aspx");
        }

        protected void ButtonFilter_Click(object sender, EventArgs e)
        {
            //if ((DateTime.TryParseExact(TextBoxFromDate.Text, format, CultureInfo.InvariantCulture,
            //        DateTimeStyles.None, out dateTimeFrom)) &&
            //        (DateTime.TryParseExact(TextBoxToDate.Text, format, CultureInfo.InvariantCulture,
            //        DateTimeStyles.None, out dateTimeTo)))
            //{
            //    updateDataGridTimeSheet();
            //}
        }
    }
}