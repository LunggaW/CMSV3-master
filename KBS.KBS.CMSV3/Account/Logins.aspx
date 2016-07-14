<%@ Page Language="C#"    AutoEventWireup="true" CodeBehind="Logins.aspx.cs" Inherits="KBS.KBS.CMSV3.Account.Logins" %>

<%@ Register assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Login</title>
    <link id="Link2" rel="shortcut icon" type="image/x-icon" href="~/Image/ct.ico" runat="server" />
    <link id="Link1" rel="icon" type="image/ico" href="~/Image/ct.ico" runat="server" />
</head>
<body>
    <form id="form2" runat="server">

   
                             

    <div style="float:left;  width:100%" height="100%" margin-left: "300px"; margin-right: "300px">        
    <div style="float:left;text-align: right;width:60%" height="60%">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <asp:Image ID="Background" runat="server" height="260px" width="100%" 
            ImageUrl="~/image/sales.png" ImageAlign="Middle"  />    
    </div>
    <div style="float:left;text-align: center;width:30% " height="20%">
        
        <br />
        <br />
        
        <br />
        <br />
        <br />
        <dx:ASPxLabel ID="ASPxLabelMessage" runat="server" Font-Size="Large" 
            Text="Message" Visible="False" width="50%">
        </dx:ASPxLabel>
        <br />
        <br />
        
        <asp:image ID="Logo" runat="server" height="50%" width="50%"  />
        <br />
        <div style="text-align:left">
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" ForeColor="Red" Text="User ID"></dx:ASPxLabel>
         </div>
        <br/>
        
    <%--<asp:TextBox ID="UserIDTxt" runat="server" onfocus="javascript:if(this.value == 'User ID') this.value=''" onblur="javascript:if(this.value == '') this.value='User ID'" Text="User ID" width="50%"></asp:TextBox>--%>
         <asp:TextBox ID="UserIDTxt" runat="server" width="50%"></asp:TextBox>
        <br />
    <br />
        <div style="text-align:left">
        <dx:ASPxLabel ID="ASPxLabel2" runat="server" ForeColor="Red"  Text="Password"></dx:ASPxLabel>
        </div>
        <br/>
        
    <asp:TextBox ID="PasswordTxt" runat="server" OnTextChanged="test"  AutoPostBack="true" width="50%" ToolTip="Password"  TextMode="Password"                        
            ></asp:TextBox>    <%--
            onfocus="javascript:if(this.value == 'Password') {this.value=''}   if(this.value != 'Password') {this.Type='Password'} " 
            onblur="javascript:if(this.value == '') {this.value='Password'} if(this.value = 'Password') {this.Type='SingleLine'} "--%>
        <br />
        <br />
        <span>
    <asp:Button ID="LoginBtn" runat="server" Text="Login" height="25%" width="25%" 
            onclick="LoginBtn_Click"/>    
    </span>
    <span>
    <asp:Button ID="Exit" runat="server" Text="Exit" height="25%" width="25%" 
            onclick="Exit_Click"/>
    </span>
    </div>        
    
        <br />
    
    </div>
    <br />
    <br />
    <br />
    <div style="margin-top: 100px">
            <div style="float:left; color:#DFE4F0; width:100%; margin-top: 10%" height="60%">
                <asp:Image ID="Image1" runat="server" height="50px" width="50px" 
                    ImageUrl="~/image/kds.png" ImageAlign="right"  />
            </div>
            <div style="margin-left: auto; margin-right: auto; text-align: right;">
            <asp:Label ID="Copyright" runat="server" Text="@KDS 2015" ></asp:Label>
            
            <br />
            <asp:Label ID="Label2" runat="server" Text="Contact Us: www.kahar.co.id/Enterprise/"></asp:Label>            
            </div>
    </div>
    </form>
    
        
</body>
</html>
