<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
CodeBehind="ReportSales.aspx.cs" 
Inherits="KBS.KBS.CMSV3.Report.ReportSales.ReportSales" %>


<%@ Register assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/New.css" rel="stylesheet" type="text/css" />
     <div>
        <span>
            
         <dx:ASPxDocumentViewer ID="ASPxDocumentViewer1" runat="server" ReportTypeName="KBS.KBS.CMSV3.DevExpressWizard.XtraReportSales">
         </dx:ASPxDocumentViewer>
        </span>
         <br />
    </div>    
</asp:Content>

