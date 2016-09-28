<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pricemanagement.aspx.cs" Inherits="pricemanagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Price Management</title>
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
    <form id="pricemanag" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1>SALES <a name="_Toc456613665"><strong>PRICE MANAGEMENT</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Price Management adalah sebuah menu yang CMS yang disediakan untuk perusahaan menganalisa, menegosiasikan, merencanakan, melaksanakan dan mempublikasikan harga agar dapat tetap kompetitif dalam persaingan bisnis.
        </p>
        <h4><span style="mso-bidi-language:AR-SA;mso-no-proof:yes">Akses menu Price management:<o:p></o:p></span></h4>
        <p>
            Master Data -> Price management</p>
        <p>
            Screen Price Management.</p>
        <p>
            &nbsp;<asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/pricemanagement.png" Width="829px" />
        </p>
        <p>
            Keterangan Kolom-kolom yang terdapat dalam Price Management :</p>
        <p>
            <asp:Image ID="Image6" runat="server" ImageUrl="~/Help/Images/keteranganpricemanagement.png" />
        </p>
        <p>
            <strong>Diagram Alur</strong></p>
        <p>
            <asp:Image ID="Image9" runat="server" ImageUrl="~/Help/Images/diagrampricemanagement.png" />
        </p>
        <h3><a name="_Toc456613669">Navigasi Price Management</a><o:p></o:p></h3>
        <p>
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Help/Images/navigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <asp:Image ID="Image8" runat="server" ImageUrl="~/Help/Images/keterangannavigasi1.png" />
            <o:p></o:p>
        </p>
        <p>
            <o:p><strong>Delete Price Management</strong></o:p></p>
        <p>
            <o:p>Sebuah Price, tidak dapat di hapus. Jika, price tersebut sudah tidak berlaku, maka dapat di update ending price nya, sehingga harga yang melawati ending price tersebut tidak akan berlaku.</o:p></p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
