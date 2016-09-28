<%@ Page Language="C#" AutoEventWireup="true" CodeFile="parametermanagement.aspx.cs" Inherits="parametermanagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Parameter Management</title>
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
    <form id="parammanag" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>PARAMETER MANAGEMENT</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Definisi dari menu Parameter Management adalah menu yang mengatur bagaimana untuk pendefinisian berbagai macam Parameter di CMS.<br />
            Parameter  yang di pasang dalam CMS adalah Parameter yang mengikuti bisnis proses /disesuaikan dengan kebutuhan perusahaan, sehingga dengan Parameter ini, CMS dapat di operasikan mengikuti role perusahaan.<br />
            Dalam CMS, Parameter dapat di tambahkan atau di kurangi, contoh beberapa Parameter standar yang telah di pasang dalam CMS, yaitu: 
        </p>
        <p>
            •	User Status<br />
            •	User Type<br />
            •	Site Class<br />
            •	Acces profile Function ID<br />
            •	Variant Status<br />
            •	Barcode Status<br />
            •	SKU Type<br />
            •	Sales Status<br />
            •	Sales Flag<br />
            •	Transfer Status<br />
            •	Dll</p>
        <p>
            Parameter Management adalah sebuah kumpulan dari Parameter table.<br />
            Setiap table mempunyai nomer sebagai identitas, dan setiap table berisi entry yang memungkinkan entry itu di isi dengan value/nilai yang terhubung dengan entry tersebut.<br />
            Setiap Parameter mempunyai header dan detail yang dapat di sesuaikan dengan role perusahaan.
</p>
        <h4><span style="mso-bidi-language:AR-SA;mso-no-proof:yes">Akses menu Parameter management:<o:p></o:p></span></h4>
        <p>
            Administration -> Parameter management</p>
        <p>
            Maka akan tampil screen Parameter Header</p>
        <p>
            <asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/parametermanagement.png" Width="829px" />
        </p>
        <p>
            Pada Parameter header terdapat dua panel yaitu :</p>
        <p>
            •	Panel atas: yaitu panel yang berisi kolom untuk pencarian parameter berdasarkan ID ataupun nama</p>
        <p style="width: 812px">
            <asp:Image ID="Image9" runat="server" Height="36px" ImageUrl="~/Help/Images/parametermanagementSearch.png" Width="821px" />
        </p>
        <p style="width: 812px">
            •	Panel bawah: yaitu panel yang berisi keterangan tentang parameter yang sudah ada di CMS</p>
        <p style="width: 812px">
            <asp:Image ID="Image10" runat="server" Height="234px" ImageUrl="~/Help/Images/parametermanagementHeader.png" Width="829px" />
        </p>
        <p>
            Keterangan Kolom-kolom yang terdapat dalam parameter header :</p>
        <p>
            <asp:Image ID="Image6" runat="server" ImageUrl="~/Help/Images/keteranganparametermanagementHeader.png" />
        </p>
        <p>
            <strong>Diagram Alur</strong></p>
        <p>
            <asp:Image ID="Image12" runat="server" ImageUrl="~/Help/Images/diagramparametermanagement.png" />
        </p>
        <h3><a name="_Toc456613669">Navigasi Parameter Management</a><o:p></o:p></h3>
        <p>
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Help/Images/navigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <asp:Image ID="Image8" runat="server" ImageUrl="~/Help/Images/keterangannavigasi8(detail).png" />
            <o:p></o:p>
        </p>
        <p>
            <o:p><strong>Delete Paramater</strong></o:p></p>
        <p>
            <o:p>Tekan tombol delete <asp:Image ID="Image1" runat="server" ImageUrl="~/Help/Images/navigasiDelete.png" Height="31px" Width="33px" /></o:p>
        </p>
        <p>
            <o:p>Maka akan muncul popup untuk validasi, apakah yakin data yang dipilih akan di hapus.</o:p></p>
        <p>
            <asp:Image ID="Image11" runat="server" Height="314px" ImageUrl="~/Help/Images/parametermanagementHeaderDeleteProses.png" Width="829px" />
            <o:p></o:p>
        </p>
        <p>
            <o:p>Setelah yakin pilih OK, maka data yang dipilih akan dihapus.</o:p></p>
        <p>
            <asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/parametermanagementHeaderDeleteDone.png" Width="829px" />
        </p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
