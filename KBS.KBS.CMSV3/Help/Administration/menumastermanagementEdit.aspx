<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menumastermanagementEdit.aspx.cs" Inherits="menumastermanagementEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit - Menu Master Management</title>
    <style type="text/css">

h1
	{margin-top:24.0pt;
	margin-right:0in;
	margin-bottom:0in;
	margin-left:0in;
	margin-bottom:.0001pt;
	line-height:115%;
	font-size:14.0pt;
	font-family:"Cambria","serif";
	}
    h3
	{margin-top:10.0pt;
	margin-right:0in;
	margin-bottom:0in;
	margin-left:0in;
	margin-bottom:.0001pt;
	line-height:112%;
	font-size:11.0pt;
	font-family:"Cambria","serif";
	}

    body {
        size: 7in 9.25in;
        margin: 5mm 30mm 40mm 30mm;
    }
    h4
	{margin-top:10.0pt;
	margin-right:0in;
	margin-bottom:0in;
	margin-left:0in;
	margin-bottom:.0001pt;
	line-height:115%;
	font-size:11.0pt;
	font-family:"Cambria","serif";
	font-style:italic;
        }
    </style>
</head>
<body>
    <form id="editmenumastermanag" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>MENU MASTER MANAGEMENT - EDIT</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span><strong>Edit Menu Master Management</strong></span></p>
        <p>
            Pada saat klik button edit, maka akan tampil data menu master yang akan di edit sebagai berikut :</p>
        <p>
            Pada screen semua dapat di edit, kecuali <strong>Menu ID</strong>.</p>
        <p>
            <asp:Image ID="Image4" runat="server" Height="76px" ImageUrl="~/Help/Images/menumastermanagementEdit.png" Width="804px" />
        </p>
        <p>
            Lakukan Validasi <asp:Image ID="Image1" runat="server" Height="23px" ImageUrl="~/Help/Images/navigasiValidasi.png" Width="31px" />, Maka menu master management yang di edit sudah berubah.</p>
        <p>
            <strong>Note : Sebaiknya perubaan ini dilakukan oleh Admin, yang mengetahui URL menu pada aplikasi CMS ini, untuk meminimalisir kesalahan.</strong></p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
