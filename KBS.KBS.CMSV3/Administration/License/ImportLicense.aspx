<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeBehind="ImportLicense.aspx.cs" Inherits="KBS.KBS.CMSV3.Administration.License.ImportLicense" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function showProfileLinkDetail(s, e) {
            detailGridView.PerformCallback(e.visibleIndex);
        }
    </script>
    <link href="../../Content/New.css" rel="stylesheet" type="text/css" />
   
    <div align="center">
        <asp:Label ID="LabelTitleSiteProfile" runat="server" Font-Size="Large" Text="License Management"></asp:Label>
    </div>
    <br />
    <asp:Label ID="LabelMessage" runat="server" Font-Size="Large" Text="License Management"
        Visible="false">
        
    </asp:Label>
    <br />
    <br />
    <br />
    <br />
    <asp:FileUpload ID="LicenseUpload" runat="server" />
    <br />
    <asp:Button ID="UploadButton" runat="server" Text="Upload" OnClick="UploadButton_Click" /><br />
    <br />
    <asp:Label ID="Info" runat="server"></asp:Label><br />
    <asp:TextBox ID="ResultFile" runat="server" Height="63px" ReadOnly="True" TextMode="MultiLine" Width="341px" Visible="false" Enabled="false" ></asp:TextBox>
    <br />
    <%--<asp:TextBox ID="ResultSub" runat="server" Height="63px" ReadOnly="True" TextMode="MultiLine" Width="341px" ></asp:TextBox>
     <br />
    
    <dx:ASPxGridView ID="ASPxGridViewMenuProfileLink" runat="server" CssClass="ASPXGridView"
        ClientInstanceName="detailGridView">
        <SettingsBehavior AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="True" />           
    </dx:ASPxGridView>--%>
    <br />
</asp:Content>
