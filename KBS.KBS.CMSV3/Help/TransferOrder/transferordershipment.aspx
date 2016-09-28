<%@ Page Language="C#" AutoEventWireup="true" CodeFile="transferordershipment.aspx.cs" Inherits="transferordershipment" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfer Order Shipment</title>
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
    <form id="shipmenttrfordr" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>TRANSFER ORDER SHIPMENT</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Adalah sebuah proses pengiriman data dan barang yang di lakukan site origin ke site destiny.
        </p>
        <h4><span style="mso-bidi-language:AR-SA;mso-no-proof:yes">Akses menu Transfer&nbsp; Shipment:<o:p></o:p></span></h4>
        <p>
            Transfer Order -&gt; Shipment</p>
        <p>
            Screen Transfer Order Shipment.</p>
        <p>
            &nbsp;<asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/toshipment.png" Width="829px" />
        </p>
        <p>
            Keterangan Kolom-kolom yang terdapat dalam Transfer Order Shipment :</p>
        <p>
            <asp:Image ID="Image6" runat="server" ImageUrl="~/Help/Images/keterangantoshipment.png" />
        </p>
        <h3><a name="_Toc456613669">Navigasi </a>Transfer Order Shipment</h3>
        <p>
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Help/Images/navigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <asp:Image ID="Image8" runat="server" ImageUrl="~/Help/Images/keterangannavigasi7.png" />
            <o:p></o:p>
        </p>
        <p>
            <o:p><strong>Edit qty yg akan di shipment</strong></o:p></p>
        <p>
            <o:p>Untuk melakukan shipment, qty ship harus di isi, yang menandakan berapa qty yang ingin di shipment oleh user.
            Dengan cara :</o:p></p>
        <p>
            <o:p>Klik entry pada shipment header</o:p></p>
        <p>
            <asp:Image ID="Image11" runat="server" Height="314px" ImageUrl="~/Help/Images/toshipment1.png" Width="829px" />
        <p>
            <o:p><strong>Melakukan Shiping data yang sudah benar.</strong></o:p></p>
        <p>
            Untuk melakukan shiping, cukup menekan tombol <strong>SHIPING</strong>.</p>
        <p>
            <asp:Image ID="Image1" runat="server" Height="314px" ImageUrl="~/Help/Images/toshipment2.png" Width="829px" />
            <p>
                Maka data shipment telah terkirim ke site destiny dan screen Transfer Order Shipment kembali bersih.<p>
                <asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/toshipmentDone.png" Width="829px" /><p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
