<%@ Page Language="C#" AutoEventWireup="true" CodeFile="salesinput.aspx.cs" Inherits="salesinput" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Input</title>
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
    <form id="salesinp" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>SALES INPUT</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Berfungsi untuk menginput sales dari item agar menjadi transaksi sales di hari itu.<br />
Input sales dilakukan oleh user yang berada di site/store.</p>
        <p>
Penginputan sales terbagi atas 2 tahap yaitu input pada level header kemudian input pada level detail.
        </p>
        <h4><span style="mso-bidi-language:AR-SA;mso-no-proof:yes">Akses menu Sales Input:<o:p></o:p></span></h4>
        <p>
            Sales Management -> Sales Input</p>
        <p>
            Screen Sales Input.</p>
        <p>
            &nbsp;<asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/salesinput.png" Width="829px" />
        </p>
        <p>
            Keterangan Kolom-kolom yang terdapat dalam Site Management :</p>
        <p>
            <asp:Image ID="Image6" runat="server" ImageUrl="~/Help/Images/keterangansalesinput.png" />
        </p>
        <h3><a name="_Toc456613669">Navigasi S</a>ales Input</h3>
        <p>
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Help/Images/navigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <asp:Image ID="Image8" runat="server" ImageUrl="~/Help/Images/keterangannavigasi2.png" />
            <o:p></o:p>
        </p>
        <p>
            <o:p><strong>Menuju Sales Input Detail</strong></o:p></p>
        <p>
            &nbsp;<asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/salesinputkedetail.png" Width="829px" />
        <p>
            <o:p><strong>Confirm Sale Detail</strong></o:p></p>
        <p>
            &nbsp;<asp:Image ID="Image9" runat="server" Height="314px" ImageUrl="~/Help/Images/salesinputkeconfirm.png" Width="829px" />
        <p>
            Apabila sales telah di confirm, Maka sales berpindah ke screen menu sales validation menunggu utk di validasi.<p>
            <o:p><strong>Delete Sales Input</strong></o:p></p>
        <p>
            <o:p>Sales Input dapat di hapus apabila statusnya masih &#39;created&#39;, dengan cara :</o:p></p>
        <p>
            <o:p>Tekan tombol delete <asp:Image ID="Image1" runat="server" ImageUrl="~/Help/Images/navigasiDelete.png" Height="31px" Width="33px" /></o:p>
        </p>
        <p>
            <o:p>Maka akan muncul popup untuk validasi, apakah yakin data yang dipilih akan di hapus.</o:p></p>
        <p>
            <o:p>Apabila sudah yakin, tekan OK. Maka sales input akan terhapus.</o:p></p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
