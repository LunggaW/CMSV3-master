<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menumastermanagement.aspx.cs" Inherits="menumastermanagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Menu Master Management</title>
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
    <form id="manumstmanag" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>MENU MASTER MANAGEMENT</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Menu Master Management adalah sebuah menu yang mengatur tampilan daftar menu utama pada CMS.<br />
            Pada Menu Master Management, setiap menu yang telah ada atau selesai dibuat di data base, dapat di tampilkan pada menu utama CMS dengan cara menyimpan linknya pada kolom URL di Menu Master Management.<br />
            Namun untuk Menu baru/ yang akan di buat sebaiknya di bahas terlebih dahulu dengan sangat hati-hati agar menu tersebut tidak mengganggu jalannya bisnis perusahhan  tetap sesuai dengan bisnis proses yang ada.</p>
        <p>
            Penambahan menu hanya dapat dilakukan oleh Development PT.KDSBS.
        </p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
