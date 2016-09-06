<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menumastermanagementAdd.aspx.cs" Inherits="menumastermanagementAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add - Menu Master Management</title>
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
    <form id="addmenumastermanag" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>MENU MASTER MANAGEMENT - ADD</strong></a><o:p></o:p></h1>
    </div>
        <p>
            Screen add menu master management
        </p>
        <p>
            <asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/menumastermanagementAdd.png" Width="829px" />
        </p>
        <p>
            •	Menu profile : kode menu profile yang harus di isi, kolom bersifat alphanumeric, artinya kolom dapat diisi dengan huruf saja, nomor saja, ataupun gabungan keduanya.</p>
        <p>
            •	Description : deskripsi site profile, kolom bersifat alphanumeric, artinya kolom dapat diisi dengan huruf saja, nomor saja, ataupun gabungan keduanya.</p>
        <p>
           Contoh kolom menu profile yang sudah di isi</p>
        <p>
            <asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/menumanagementAddProses.png" Width="829px" /></o:p></p>
        <p>
            Lakukan Validasi <asp:Image ID="Image1" runat="server" Height="23px" ImageUrl="~/Help/Images/navigasiValidasi.png" Width="31px" />, jika berhasil maka header menu profile telah terbentuk</p>
        <p>
            <asp:Image ID="Image6" runat="server" Height="314px" ImageUrl="~/Help/Images/menumanagementAddDone.png" Width="829px" />
        </p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
