<%@ Page Language="C#" AutoEventWireup="true" CodeFile="usermanagementAdd.aspx.cs" Inherits="usermanagementAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add - User Management</title>
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
    <form id="addusermanagement" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>USER MANAGEMENT - ADD</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span><strong>New User</strong></span></p>
        <p>
            Sebelum membuat user baru, terdapat langkah awal (pre-requisites) yang harus di buat terlebih dahulu, yaitu :</p>
        <p>
            •	USER MENU PROFILE<br />
            •	USER ACCES PROFILE<br />
            •	USER SITE PROFILE
        </p>
        <p>
            Ketiga pre-requisite tersebut akan di bahas di bahasan yang akan datang.</p>
        <p>
            Isi dan keterangan kolom-kolom yang terdapat dalam header adalah :</p>
        <p>
            <asp:Image ID="Image4" runat="server" Height="2263px" ImageUrl="~/Help/Images/keteranganusermanagementAdd1024.png" Width="663px" />
        </p>
        <p>
            Gambar tampilan setelah kolom di isi :</p>
        <p>
            <asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/usermanagementAddDone.png" Width="829px" />
        </p>
        <p>
            Lakukan Validasi <asp:Image ID="Image1" runat="server" Height="23px" ImageUrl="~/Help/Images/navigasiValidasi.png" Width="31px" />, jika berhasil, maka user akan terdaftar pada kolom user</p>
        <p>
            <asp:Image ID="Image6" runat="server" Height="314px" ImageUrl="~/Help/Images/usermanagementAddDoneComplete.png" Width="829px" />
        </p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
