<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeBehind="Default.aspx.cs" Inherits="KBS.KBS.CMSV3._Default" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>





<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <%-- DXCOMMENT: Configure ASPxGridView control --%>
<%--<dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="True" DataSourceID="SqlDataSource1" ClientInstanceName="ASPxGridView1"
    Width="100%">
    <SettingsPager Visible="False" PageSize="30" />
    <Settings VerticalScrollBarMode="Visible" VerticalScrollableHeight="350" VerticalScrollBarStyle="Virtual" />
    <Paddings Padding="0px" />
    <Border BorderWidth="0px" /><BorderBottom BorderWidth="1px" />
--%>    <%-- DXCOMMENT: Configure ASPxGridView's columns in accordance with datasource fields --%>

<div style="float:right;text-align: center;width:30% " height="20%">            
<dx:ASPxComboBox ID="sitestore" Caption="Site" Visible="false" runat="server" AutoPostBack=true
 OnSelectedIndexChanged="GetSite"></dx:ASPxComboBox>
</div>
<div style="float:left;  width:100%" height="70%" margin-left: "300px">        
    <div style="float:left;text-align: right;width:100%" height="60%">
        <br />
        <br />
        <br />
        <asp:Image ID="Background" runat="server" height="460px" width="100%" 
             ImageAlign="Middle"  />    
    </div>
<div>
    <Columns>
    </Columns>
</dx:ASPxGridView>


<%-- DXCOMMENT: Configure your datasource for ASPxGridView --%>
<%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:NWindConnectionString %>" 
        SelectCommand="SELECT * FROM [Customers]">
</asp:SqlDataSource>
--%>
</asp:Content>