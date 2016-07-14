﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="UserManagement.aspx.cs" Inherits="KBS.KBS.CMSV3.Administration.UserManagement" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>



<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <%-- DXCOMMENT: Configure your datasource for ASPxGridView --%>
    <dx:ASPxButton ID="ASPxButtonUserManagementInsert" runat="server" 
        onclick="ASPxButtonUserManagementInsert_Click" style="height: 21px" 
        Text="New">
    </dx:ASPxButton>
    &nbsp;<dx:ASPxButton ID="ASPxButtonDelete" runat="server" 
        onclick="ASPxButtonDelete_Click" Text="Delete">
    </dx:ASPxButton>
&nbsp;<dx:ASPxButton ID="ASPxButtonUpdate" runat="server" 
        onclick="ASPxButtonUpdate_Click" Text="Edit">
    </dx:ASPxButton>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <dx:ASPxButton ID="ASPxButtonCommit" runat="server" Text="Confirm">
    </dx:ASPxButton>
    <dx:ASPxSplitter ID="ASPxSplitter2" runat="server" Height="195px">
        <panes>
            <dx:SplitterPane>
                <ContentCollection>
<dx:SplitterContentControl runat="server">UserID<dx:ASPxTextBox 
        ID="ASPxTextBoxUserManagementUserID" runat="server" Width="170px">
    </dx:ASPxTextBox>
    <br />
    Profile<dx:ASPxComboBox ID="ASPxComboBoxUserManagementProfile" runat="server">
    </dx:ASPxComboBox>
    <br />
    Max Allowed Sales Discount %<br /><asp:TextBox 
        ID="TextBoxUserManagementMaxSalesDiscount" runat="server" Width="170px"></asp:TextBox>
    <br />
    <br />
    UserName<dx:ASPxTextBox ID="ASPxTextBoxUserManagementUserName" runat="server" 
        Width="170px">
    </dx:ASPxTextBox>
    <br />
                    </dx:SplitterContentControl>
</ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane>
                <ContentCollection>
<dx:SplitterContentControl runat="server">Password<br /><dx:ASPxTextBox 
        ID="ASPxTextBoxUserManagementPassword" runat="server" Width="170px">
    </dx:ASPxTextBox>
    <br />
    Confirm Password<br /><dx:ASPxTextBox 
        ID="ASPxTextBoxUserManagementConfirmPassword" runat="server" Width="170px">
    </dx:ASPxTextBox>
    <br />
    Site<dx:ASPxComboBox ID="ASPxComboBoxUserManagementSite" runat="server">
    </dx:ASPxComboBox>
    <br />
                    </dx:SplitterContentControl>
</ContentCollection>
            </dx:SplitterPane>
        </panes>
    </dx:ASPxSplitter>
    <br />

<%-- DXCOMMENT: Configure your datasource for ASPxGridView --%>
    <dx:ASPxGridView ID="ASPxGridViewUserManagementUser" runat="server" 
        Width="1054px">
        <SettingsBehavior AllowFocusedRow="True" 
            ProcessFocusedRowChangedOnServer="True" />
    </dx:ASPxGridView>

</asp:Content>
