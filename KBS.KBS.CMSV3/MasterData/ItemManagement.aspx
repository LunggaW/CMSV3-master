<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ItemManagement.aspx.cs" Inherits="KBS.KBS.CMSV3.MasterData.ItemManagement" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>



<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <dx:ASPxButton ID="ASPxButtonNew" runat="server" Text="New" 
        onclick="ASPxButtonNew_Click">
    </dx:ASPxButton>
    <br />
    <dx:ASPxButton ID="ASPxButtonUpdate" runat="server" Text="Edit" 
        onclick="ASPxButtonUpdate_Click">
    </dx:ASPxButton>
    <br />
    <dx:ASPxButton ID="ASPxButtonDelete" runat="server" Text="Delete" 
        onclick="ASPxButtonDelete_Click">
    </dx:ASPxButton>
    <br />
    <br />
    <dx:ASPxButton ID="ASPxButtonCommit" runat="server" 
        onclick="ASPxButtonCommit_Click" Text="Commit">
    </dx:ASPxButton>
    <dx:ASPxSplitter ID="ASPxSplitterItem" runat="server">
        <Panes>
            <dx:SplitterPane>
                <ContentCollection>
<dx:SplitterContentControl runat="server">
    <dx:ASPxLabel ID="ASPxLabelItemID" runat="server" Text="Item ID">
    </dx:ASPxLabel>
    <dx:ASPxTextBox ID="ASPxTextBoxID" runat="server" Width="170px">
    </dx:ASPxTextBox>
    <br />
    <dx:ASPxLabel ID="ASPxLabelItemName" runat="server" Text="Item Name">
    </dx:ASPxLabel>
    <dx:ASPxTextBox ID="ASPxTextBoxName" runat="server" Width="170px">
    </dx:ASPxTextBox>
    <br />
    <dx:ASPxLabel ID="ASPxLabelBrand" runat="server" Text="Brand">
    </dx:ASPxLabel>
    <dx:ASPxComboBox ID="ASPxComboBoxBrand" runat="server">
    </dx:ASPxComboBox>
    <br />
    <dx:ASPxLabel ID="ASPxLabelBarcode" runat="server" Text="Barcode">
    </dx:ASPxLabel>
    <dx:ASPxTextBox ID="ASpXTextBoxBarcode" runat="server" Width="170px">
    </dx:ASPxTextBox>
    <br />
    <dx:ASPxLabel ID="ASPxLabelSize" runat="server" Text="Item Size">
    </dx:ASPxLabel>
    <dx:ASPxTextBox ID="ASpXTextBoxSize" runat="server" Width="170px">
    </dx:ASPxTextBox>
                    </dx:SplitterContentControl>
</ContentCollection>
            </dx:SplitterPane>
            <dx:SplitterPane>
                <ContentCollection>
<dx:SplitterContentControl runat="server">
    <dx:ASPxLabel ID="ASPxLabelVariant" runat="server" Text="Variant">
    </dx:ASPxLabel>
    <dx:ASPxComboBox ID="ASPxComboBoxVariant" runat="server">
    </dx:ASPxComboBox>
    <br />
    <dx:ASPxLabel ID="ASPxLabelStatus" runat="server" Text="Status">
    </dx:ASPxLabel>
    <dx:ASPxComboBox ID="ASPxComboBoxStatus" runat="server">
        <Items>
            <dx:ListEditItem Text="Enable" Value="1" />
            <dx:ListEditItem Text="Disable" Value="2" />
        </Items>
    </dx:ASPxComboBox>
    <br />
                    </dx:SplitterContentControl>
</ContentCollection>
            </dx:SplitterPane>
        </Panes>
    </dx:ASPxSplitter>
    <br />

<%-- DXCOMMENT: Configure your datasource for ASPxGridView --%>
    
    <dx:ASPxGridView ID="ASPxGridViewItem" runat="server" 
        onfocusedrowchanged="ASPxGridViewItem_FocusedRowChanged" 
        EnableCallBacks="False">
        <SettingsBehavior AllowFocusedRow="True" 
            ProcessFocusedRowChangedOnServer="True" />
    </dx:ASPxGridView>
    
</asp:Content>
