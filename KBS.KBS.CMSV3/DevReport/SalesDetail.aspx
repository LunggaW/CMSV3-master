<%@ Page Language="C#" AutoEventWireup="true" 
    MasterPageFile="~/Main.master"
    CodeBehind="SalesDetail.aspx.cs" 
    Inherits="KBS.KBS.CMSV3.DevReport.SalesDetail" %>


<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register assembly="DevExpress.XtraReports.v15.1.Web, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraReports.Web" tagprefix="dx" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/PopUp.js" type="text/javascript"></script>
    <script type="text/javascript">
        function UpdateDetailGrid(s, e) {
            detailGridView.PerformCallback(e.visibleIndex);
        }
    </script>
    <link rel="stylesheet" type="text/css" href="../../Content/New.css" />
    <div>
        
       
    </div>
    <br />
    <div align="center" class="title">
        <asp:Label ID="Label1" runat="server" Font-Size="Large" Text="Report Sales Detail"></asp:Label>
        <br />
    </div>
    <br />
    <span>
        <dx:ASPxLabel ID="ASPxLabelMessage" runat="server" Font-Size="Large" Text="ASPxLabel"
            Visible="False">
        </dx:ASPxLabel>
        <dx:ASPxDocumentViewer ID="ASPxDocumentViewer1" runat="server" ReportTypeName="KBS.KBS.CMSV3.DevReport.ReportSales">
    </dx:ASPxDocumentViewer>
        <br />
    </span>
    <br />  
    <br />
</asp:Content>