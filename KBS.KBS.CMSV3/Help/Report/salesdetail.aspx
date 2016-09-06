<%@ Page Language="C#" AutoEventWireup="true" CodeFile="salesdetail.aspx.cs" Inherits="salesdetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Detail</title>
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
    <form id="slsdetail" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>SALES DETAIL</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Adalah report yang menyampaikan informasi secara spesifik tentang detail sales yang terjadi berdasarkan Transaction ID, Nota, Tanggal, Barcode, Qty dan total salesnya dalam periode waktu tertentu.<br />
            Untuk saat ini sales yang dapat muncul di Report ini adalah sales yang berasal dari Simple Sales Input.
        </p>
        <p>
            Screen Report Sales Detail.</p>
        <p>
            &nbsp;<asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/rptsalesdetail.png" Width="829px" />
        </p>
        <p>
            Keterangan function yang terdapat dalam Report Sales Detail :</p>
        <p>
            <asp:Image ID="Image6" runat="server" ImageUrl="~/Help/Images/keteranganrptsalesdetail.png" />
        </p>
        <p>
            Masukkan Site pada kolom </p>
        <p>
            <asp:Image ID="Image1" runat="server" Height="314px" ImageUrl="~/Help/Images/rptsalesdetail1.png" Width="829px" />
        <p>
            Masukkan periode sales</p>
        <p>
            <asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/rptsalesdetail2.png" Width="829px" /></p>
        <p>
            Setelah itu tekan tombol Submit</p>
        <p>
            <asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/rptsalesdetail3.png" Width="829px" /></p>
        <p>
            Maka report sales terbentuk</p>
        <p>
            <asp:Image ID="Image7" runat="server" Height="314px" ImageUrl="~/Help/Images/rptsalesdetailDone.png" Width="829px" /></p>
        <p>
            Keterangan kolom-kolom pada report tersebut adalah :</p>
        <p>
            <asp:Image ID="Image8" runat="server" ImageUrl="~/Help/Images/keteranganrptsalesdetailDone.png" />
        </p>
        <p>
            Jika ingin melihat tampilan report lebih jelas, maka user bisa terlebih dahulu menekan tombol 
            <asp:Image ID="Image9" runat="server" ImageUrl="~/Help/Images/buttonsaverptsalesdetail.png" />
&nbsp;save</p>
        <p>
            <asp:Image ID="Image10" runat="server" Height="314px" ImageUrl="~/Help/Images/rptsalesdetailDone1.png" Width="829px" /></p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
