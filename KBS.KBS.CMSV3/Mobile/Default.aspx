<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Mobile/Login.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="MobileEnabledWebFormsApp.Mobile._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Label ID="LabelUserName" runat="server" Text="User Name"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:TextBox ID="TextBoxUserName" runat="server"></asp:TextBox>
<br />
<br />
<asp:Label ID="LabelPassword" runat="server" Text="Password"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:TextBox ID="TextBoxPassword" runat="server"></asp:TextBox>
<br />
<br />
<asp:Button ID="ButtonLogin" runat="server" onclick="ButtonLogin_Click" 
            Text="Login" Font-Size="X-Large"/>
</asp:Content>
