<%@ Page Language="C#" AutoEventWireup="true" CodeFile="assortmentmanagement.aspx.cs" Inherits="assortmentmanagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assortment Management</title>
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
    <form id="assortmentmanag" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>ASSORTMENT MANAGEMENT</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Assortment Management dalam CMS adalah menu yang mengatur tentang kelengkapan jenis barang pada masing-masing kategori di sebuah site.
        </p>
        <h4><span style="mso-bidi-language:AR-SA;mso-no-proof:yes">Akses menu Assortment management:<o:p></o:p></span></h4>
        <p>
            Master Data -> Assortment management</p>
        <p>
            Screen Assortment Management.</p>
        <p>
            &nbsp;<asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/assortmentmanagement.png" Width="829px" />
        </p>
        <p>
            Keterangan Kolom-kolom yang terdapat dalam Assortment Management :</p>
        <p>
            <asp:Image ID="Image6" runat="server" ImageUrl="~/Help/Images/keteranganassortmentmanagement.png" />
        </p>
        <p>
            <strong>Diagram Alur</strong></p>
        <p>
            <asp:Image ID="Image12" runat="server" ImageUrl="~/Help/Images/diagramassortmentmanagement.png" />
        </p>
        <h3><a name="_Toc456613669">Navigasi Assortment Management</a><o:p></o:p></h3>
        <p>
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Help/Images/navigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <asp:Image ID="Image8" runat="server" ImageUrl="~/Help/Images/keterangannavigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <o:p><strong>Delete dan Add pada Assorment Management</strong></o:p></p>
        <p>
            <o:p>Tekan tombol edit <asp:Image ID="Image1" runat="server" ImageUrl="~/Help/Images/buttonedit.png" Height="31px" Width="33px" /></o:p>
        </p>
        <p>
            <o:p>Selanjutnya akan terbuka screen baru assortment management dengan toolbar yang aktif.</o:p></p>
        <p>
            <asp:Image ID="Image11" runat="server" Height="314px" ImageUrl="~/Help/Images/assortmentmanagementAdd.png" Width="829px" />
        <p>
            <o:p>Anda bisa melakukan Add dengan menekan tombol <asp:Image ID="Image4" runat="server" ImageUrl="~/Help/Images/buttonadd.png" Height="31px" Width="33px" /> atau menghapus dengan menekan tombol <asp:Image ID="Image9" runat="server" ImageUrl="~/Help/Images/navigasiDelete.png" Height="31px" Width="33px" />.</o:p></p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
