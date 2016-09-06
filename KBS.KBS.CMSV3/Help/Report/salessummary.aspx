<%@ Page Language="C#" AutoEventWireup="true" CodeFile="salessummary.aspx.cs" Inherits="salessummary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Summary</title>
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
    <form id="slssummary" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>SALES SUMMARY</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Adalah report yang menyampaikan informasi secara spesifik tentang sales yang terjadi di sebuah Site meliputi Site, Total Qty, dan Total Sales dalam periode waktu tertentu.<br />
            Untuk saat ini sales yang dapat muncul di Report ini adalah sales yang berasal dari Simple Sales Input.
        </p>
        <p>
            Screen Report Sales Summary.</p>
        <p>
            &nbsp;<asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/rptsalessummary.png" Width="829px" />
        </p>
        <p>
            Keterangan function yang terdapat dalam Report Sales Summary :</p>
        <p>
            <asp:Image ID="Image6" runat="server" ImageUrl="~/Help/Images/keteranganrptsalesdetail.png" />
        </p>
        <p>
            Masukkan Site pada kolom </p>
        <p>
            <asp:Image ID="Image1" runat="server" Height="314px" ImageUrl="~/Help/Images/rptsalessummary1.png" Width="829px" />
        <p>
            Masukkan periode sales</p>
        <p>
            <asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/rptsalessummary2.png" Width="829px" /></p>
        <p>
            Setelah itu tekan tombol Submit</p>
        <p>
            <asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/rptsalessummary3.png" Width="829px" /></p>
        <p>
            Maka report sales summary terbentuk</p>
        <p>
            <asp:Image ID="Image7" runat="server" Height="314px" ImageUrl="~/Help/Images/rptsalessummaryDone.png" Width="829px" /></p>
        <p>
            Keterangan kolom-kolom pada report tersebut adalah :</p>
        <p>
            <asp:Image ID="Image8" runat="server" ImageUrl="~/Help/Images/keteranganrptsalessummaryDone.png" />
        </p>
        <p>
            Jika ingin melihat tampilan report lebih jelas, maka user bisa terlebih dahulu menekan tombol 
            <asp:Image ID="Image9" runat="server" ImageUrl="~/Help/Images/buttonsaverptsalesdetail.png" />
&nbsp;save</p>
        <p>
            <asp:Image ID="Image10" runat="server" Height="314px" ImageUrl="~/Help/Images/rptsalessummaryDone1.png" Width="829px" /></p>
        <p>
            Untuk bentuk format file lain, tekan drop down list pada field
            <asp:Image ID="Image11" runat="server" ImageUrl="~/Help/Images/buttontopdfreport.png" />
        </p>
        <p>
            <asp:Image ID="Image12" runat="server" Height="314px" ImageUrl="~/Help/Images/rptsalessummaryDone2.png" Width="829px" /></p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
