﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="transferordershipmentDetailEdit.aspx.cs" Inherits="transferordershipmentDetailEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit - Transfer Order Shipment Detail</title>
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
    <form id="editshipmenttrfordrdetail" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>TRANSFER ORDER SHIPMENT DETAIL - EDIT</strong></a><o:p></o:p></h1>
    </div>
        <p>
            Pada saat klik button edit, maka akan tampil data shipment.</p>
        <p>
            <asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/toshipmentDetailProses.png" Width="829px" />
        </p>
        <p>
            Keterangan kolom - kolom diatas adalah :</p>
        <p>
            <asp:Image ID="Image5" runat="server" ImageUrl="~/Help/Images/keterangantoshipmentDetailProses.png" />
        </p>
        <p>
            Isi kolom quantity dengan jumlah real barang yang dikirimkan, jika qty kirim tidak sama dengan qty transfer, maka harus ada penjelasan yang di jabarkan dalam kolom comment.</p>
        <p>
            <asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/toshipmentDetailProses1.png" Width="829px" />
        <p>
            Lakukan Validasi <asp:Image ID="Image1" runat="server" Height="23px" ImageUrl="~/Help/Images/navigasiValidasi.png" Width="31px" />
            , jika berhasil, maka kolom qty shipment dan commentnya terisi.</p>
        <p>
            <asp:Image ID="Image6" runat="server" Height="314px" ImageUrl="~/Help/Images/toshipmentDetailDone.png" Width="829px" />
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
