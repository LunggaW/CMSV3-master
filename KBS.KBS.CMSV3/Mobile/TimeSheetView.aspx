<%@ Page Title="" Language="C#" MasterPageFile="~/Mobile/Mobile.Master" AutoEventWireup="true" CodeBehind="TimeSheetView.aspx.cs" Inherits="KBS.TimeSheet.Mobile.Mobile.TimeSheetView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="LabelDataExist" runat="server" Text="Data Does not Exist"></asp:Label>
    <br />
    <br />
    Period From :&nbsp;
    <asp:TextBox ID="TextBoxFromDate" runat="server"></asp:TextBox>
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Calibri" 
                    ForeColor="Red" Text="Format : ddmmyyyy"></asp:Label>
            <br />
    To&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    :&nbsp;
    <asp:TextBox ID="TextBoxToDate" runat="server"></asp:TextBox>
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Calibri" 
                    ForeColor="Red" Text="Format : ddmmyyyy"></asp:Label>
            <br />
    <br />
    <asp:Button ID="ButtonFilter" runat="server" onclick="ButtonFilter_Click" 
        Text="Filter" Width="130px" />
    <br />
<br />
<asp:GridView ID="GridViewTimeSheet" runat="server" AllowSorting="True" 
        AutoGenerateSelectButton="True" 
        onselectedindexchanged="GridViewTimeSheet_SelectedIndexChanged"
        DataKeyNames="TMSID">
</asp:GridView>
    <br />
    </asp:Content>
