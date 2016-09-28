<%@ Page Language="C#" AutoEventWireup="true" CodeFile="transferordercreation.aspx.cs" Inherits="transferordercreation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfer Order Creation</title>
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
    <form id="creationtrfordr" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>TRANSFER ORDER CREATION</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Adalah proses pembuatan transfer dari site origin ke site destiny
Pada proses pembuatan /creation, maka user harus membuat transfer order level header dan detailnya.
        </p>
        <p>
            Type transfer order terdapat 2 jenis :<br />
•	Creation	: Data transfer di buat oleh HQ dikirim ke site store origin, kemudian site store origin  melakukan shipment ke site destiny.<br />
•	Shipment	: Data transfer di buat oleh HQ/WHS dan shipment/pengiriman dilakukan langsung oleh HQ/WHS ke site destiny.
</p>
        <h4><span style="mso-bidi-language:AR-SA;mso-no-proof:yes">Akses menu Transfer Order type Cteation:<o:p></o:p></span></h4>
        <p>
            Transfer Order -> Creation</p>
        <p>
            Screen Transfer Order Creation.</p>
        <p>
            &nbsp;<asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/tocreation.png" Width="829px" />
        </p>
        <p>
            Keterangan Kolom-kolom yang terdapat dalam Transfer Order Creation :</p>
        <p>
            <asp:Image ID="Image6" runat="server" ImageUrl="~/Help/Images/keterangantocreation.png" />
        </p>
        <h3><a name="_Toc456613669">Navigasi </a>Transfer Order Creation</h3>
        <p>
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Help/Images/navigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <asp:Image ID="Image8" runat="server" ImageUrl="~/Help/Images/keterangannavigasi5.png" />
            <o:p></o:p>
        </p>
        <p>
            <o:p><strong>Delete Transfer Order Creation</strong></o:p></p>
        <p>
            Untuk menghapus Transfer Order pada level header, maka harus menghapus terlebih dahulu detail nya. Apabila detail nya sudah di hapus, maka header </p>
        <p>
            <o:p>Tekan tombol delete <asp:Image ID="Image1" runat="server" ImageUrl="~/Help/Images/navigasiDelete.png" Height="31px" Width="33px" /></o:p>
        </p>
        <p>
            <o:p>Maka akan muncul popup untuk validasi, apakah yakin data yang dipilih akan di hapus.</o:p></p>
        <p>
            <asp:Image ID="Image11" runat="server" Height="314px" ImageUrl="~/Help/Images/colormanagementDeleteProses.png" Width="829px" />
        <p>
            <o:p>Setelah yakin pilih OK, maka data yang dipilih akan dihapus.</o:p></p>
        <p>
            <asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/colormanagementDeleteDone.png" Width="829px" />
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
