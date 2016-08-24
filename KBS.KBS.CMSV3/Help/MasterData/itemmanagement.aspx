<%@ Page Language="C#" AutoEventWireup="true" CodeFile="itemmanagement.aspx.cs" Inherits="itemmanagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Item Management</title>
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
    <form id="itemmanag" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>ITEM MANAGEMENT</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Adalah  menu CMS yang membahas artikel dilihat dari tata cara pembuatan, editing, status artikel,dan lain-lain.
        </p>
        <p>
            <strong>Type Item</strong></p>
        <p>
            Dalam CMS dikenal 2 (dua) macam item type yaitu:<br />
•	Common: adalah artikel biasa yang tidak mempunyai colour dan size<br />
&nbsp;&nbsp;&nbsp;o	Misalnya: kalung, gelang, cincin<br />
•	Variant : adalah artikel biasa yang mempunyai colour dan size<br />
&nbsp;&nbsp;&nbsp;o	Misalnya: Baju Tshirt dengan ukuran size/ukuran: S, M, L, XL dll dan Colour/warna: Merah, Biru, Kuning dll.
</p>
        <p>
            <strong>Pre-requisites</strong></p>
        <p>
            Sebelum membuat sebuah artikel baru, diperlukan data awal dari beberapa modul lain yang di sebut dengan pre-requisites.<br />
Pre-requisites dalam pembuatan artikel baru:<br />
•	Untuk Item Type Common:<br />
&nbsp;&nbsp;&nbsp;o	Brand<br />
•	Untuk Item Type Variant :<br />
&nbsp;&nbsp;&nbsp;o	Brand<br />
&nbsp;&nbsp;&nbsp;o	Colour Group<br />
&nbsp;&nbsp;&nbsp;o	Style  Group<br />
&nbsp;&nbsp;&nbsp;o	Size Group
</p>

        <h4><span style="mso-bidi-language:AR-SA;mso-no-proof:yes">Akses menu Item management:<o:p></o:p></span></h4>
        <p>
            Master Data -> Item management</p>
        <p>
            Screen Item Management.</p>
        <p>
            &nbsp;<asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/itemmanagement.png" Width="829px" />
        </p>
        <p>
            Keterangan Kolom-kolom yang terdapat dalam Item Management :</p>
        <p>
            <asp:Image ID="Image6" runat="server" ImageUrl="~/Help/Images/keteranganitemmanagement.png" />
        </p>
        <p>
            <strong>Diagram Alur</strong></p>
        <p>
            Common</p>
        <p>
            <asp:Image ID="Image12" runat="server" ImageUrl="~/Help/Images/diagramitemmanagement(common).png" />
        </p>
        <p>
            Variant</p>
        <p>
            <asp:Image ID="Image13" runat="server" ImageUrl="~/Help/Images/diagramitemmanagement(Variant).png" />
        </p>
        <h3><a name="_Toc456613669">Navigasi Item Management</a><o:p></o:p></h3>
        <p>
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Help/Images/navigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <asp:Image ID="Image8" runat="server" ImageUrl="~/Help/Images/keterangannavigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <o:p><strong>Delete Item (Under development)</strong></o:p>
        </p>
        <p>
            <o:p>Tekan tombol delete <asp:Image ID="Image1" runat="server" ImageUrl="~/Help/Images/navigasiDelete.png" Height="31px" Width="33px" /></o:p>
        </p>
        <p>
            <o:p>Maka akan muncul popup untuk validasi, apakah yakin data yang dipilih akan di hapus.</o:p></p>
        <p>
            <asp:Image ID="Image11" runat="server" Height="314px" ImageUrl="~/Help/Images/usermanagementDeleteProses.png" Width="829px" />
        <p>
            <o:p>Setelah yakin pilih OK, maka data yang dipilih akan dihapus.</o:p></p>
        <p>
            <asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/usermanagement1.png" Width="829px" />
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
