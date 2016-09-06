<%@ Page Language="C#" AutoEventWireup="true" CodeFile="skulinkmanagement .aspx.cs" Inherits="skulinkmanagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SKU Link Management</title>
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
    <form id="skulinkmanag" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>SKU LINK MANAGEMENT</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            SKU Link Management adalah sebuah menu yang berfungsi untuk menghubungkan SKU atau Discount dengan Site tempat diberlakukannya SKU tersebut.
        </p>
        <h4><span style="mso-bidi-language:AR-SA;mso-no-proof:yes">Akses menu SKU Link management:<o:p></o:p></span></h4>
        <p>
            Master Data -> SKU Link management</p>
        <p>
            Screen SKU Link Management.</p>
        <p>
            &nbsp;<asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/skulinkmanagement.png" Width="829px" />
        </p>
        <p>
            Keterangan Kolom-kolom yang terdapat dalam SKU Management :</p>
        <p>
            <asp:Image ID="Image6" runat="server" ImageUrl="~/Help/Images/keteranganskulinkmanagement.png" />
        </p>
        <p>
            <strong>Diagram Alur</strong></p>
        <p>
            <asp:Image ID="Image9" runat="server" ImageUrl="~/Help/Images/diagramskulinkmanagement.png" />
        </p>
        <h3><a name="_Toc456613669">Navigasi SKU Link Management</a><o:p></o:p></h3>
        <p>
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Help/Images/navigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <asp:Image ID="Image8" runat="server" ImageUrl="~/Help/Images/keterangannavigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <o:p><strong>Delete SKU Link Management</strong></o:p></p>
        <p>
            <o:p>Sebuah Sku Link tidak dapat di hapus. Jika, sku link tersebut sudah tidak berlaku, maka dapat di update end date nya, sehingga sku link yang melawati end date tersebut tidak akan berlaku.</o:p></p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
