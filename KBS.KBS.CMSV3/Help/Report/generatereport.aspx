<%@ Page Language="C#" AutoEventWireup="true" CodeFile="generatereport.aspx.cs" Inherits="generatereport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Generate</title>
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
    <form id="rptgenerate" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>REPORT GENERATE</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Adalah report yang menyampaikan informasi secara spesifik tentang apapun yang di inginkan user, melalui sebuah perintah program yang disebut dengan query.
        </p>
        <p>
            Screen Report Generate.</p>
        <p>
            &nbsp;<asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/rptgenerate.png" Width="829px" />
        </p>
        <p>
            Keterangan kolom yang terdapat dalam Report Generate :</p>
        <p>
            <asp:Image ID="Image6" runat="server" ImageUrl="~/Help/Images/keteranganrptgenerate.png" />
        </p>
        <p>
            Masukkan query pada kolomnya.</p>
        <p>
            <asp:Image ID="Image1" runat="server" Height="314px" ImageUrl="~/Help/Images/rptgenerate1.png" Width="829px" />
        <p>
            Kemudian isi file name<p>
            <asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/rptgenerate2.png" Width="829px" /></p>
        <p>
            Tekan tab proses</p>
        <p>
            <asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/rptgenerate3.png" Width="829px" /></p>
        <p>
            Maka query terproses</p>
        <p>
           <asp:Image ID="Image7" runat="server" Height="314px" ImageUrl="~/Help/Images/rptgenerateDone.png" Width="829px" /></p>
        <p>
            Tekan salah satu tab bentuk format file Report yang di buat <br />
            <asp:Image ID="Image9" runat="server" ImageUrl="~/Help/Images/buttonrptgenerate.png" /></p>
        <p>
            <asp:Image ID="Image10" runat="server" Height="314px" ImageUrl="~/Help/Images/rptgenerateDone1.png" Width="829px" /></p>
        <p>
            Maka hasil tampilan reportnya adalah sebagai berikut.
        </p>
        <p>
            <asp:Image ID="Image12" runat="server" Height="314px" ImageUrl="~/Help/Images/rptgenerateDone2.png" Width="829px" /></p>
        <p>
            Certak report.</p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
