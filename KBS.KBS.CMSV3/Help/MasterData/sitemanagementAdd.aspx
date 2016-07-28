﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sitemanagementAdd.aspx.cs" Inherits="sitemanagementAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add - Site Management</title>
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
    <form id="addsitemanagement" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>SITE MANAGEMENT - ADD</strong></a><o:p></o:p></h1>
    </div>
        <p>
            Screen new site management</p>
        <p>
            <asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/sitemanagementAdd.png" Width="829px" />
        </p>
        <p>
            Keterangan kolom di atas adalah sebagai berikut :</p>
        <p>
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Help/Images/keterangansitemanagementAdd.png" />
        </p>
        <p>
            Isi kolom di atas. sebagai contoh :</p>
        <p>
            <asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/sitemanagementAddProses.png" Width="829px" />
        </p>
        <p>
            Lakukan Validasi <asp:Image ID="Image1" runat="server" Height="23px" ImageUrl="~/Help/Images/navigasiValidasi.png" Width="31px" />, jika berhasil, maka site baru akan terbentuk</p>
        <p>
            <asp:Image ID="Image6" runat="server" Height="314px" ImageUrl="~/Help/Images/sitemanagementAddDone.png" Width="829px" />
        </p>
        <p>
            Setelah site terbentuk, maka site harus di link kan dengan sebuah profile yang telh di tentukan pada menu Site Profile Management.</p>
        <p>
            Jika sudah di link kan, cek kembali pada daftar site di profile tersebut, maka seharusnya Site terdaftar pada daftar sitenya.</p>
        <p>
            <asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/sitemanagementAddDone1.png" Width="829px" />
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>