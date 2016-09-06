<%@ Page Language="C#" AutoEventWireup="true" CodeFile="transferorder.aspx.cs" Inherits="transferorder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfer Order</title>
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
    <form id="trforder" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>TRANSFER ORDER</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Adalah report yang menyampaikan informasi secara spesifik tentang transfer order yang terjadi meliputi Transfer ID, Site Origin, Site Destiny, Item ID, Varian, Barcode, Qty, Shipping, and Receiving.
        </p>
        <p>
            Screen Report Transfer Order.</p>
        <p>
            &nbsp;<asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/rpttransferorder.png" Width="829px" />
        </p>
        <p>
            Keterangan function yang terdapat dalam Report Transfer Order :</p>
        <p>
            <asp:Image ID="Image6" runat="server" ImageUrl="~/Help/Images/keteranganrptsalesdetail.png" />
        </p>
        <p>
            Variable untuk mencetak Report transfer order diantaranya:<br />
•	Transfer ID : nomor identitas transfer<br />
•	From store: Site Origin/store asal yang melakukan transfer<br />
•	To store: Site destiny/store penerima transfer dari site origin.
</p>
        <p>
            Tekan salah satu kolom maka akan terdapat bantuan drop down list tentang isi kolom tersebut, misalnya Transfer ID.</p>
        <p>
            <asp:Image ID="Image1" runat="server" Height="314px" ImageUrl="~/Help/Images/rpttransferorder1.png" Width="829px" />
        <p>
            Tekan tombol submit<p>
            <asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/rpttransferorder2.png" Width="829px" /></p>
        <p>
            Maka report transfer order akan muncul di screen</p>
        <p>
            <asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/rpttransferorderDone.png" Width="829px" /></p>
        <p>
            Jika ingin melihat tampilan report lebih jelas, maka user bisa terlebih dahulu menekan tombol 
            <asp:Image ID="Image9" runat="server" ImageUrl="~/Help/Images/buttonsaverptsalesdetail.png" />
&nbsp;save</p>
        <p>
            <asp:Image ID="Image10" runat="server" Height="314px" ImageUrl="~/Help/Images/rpttransferorderDone1.png" Width="829px" /></p>
        <p>
            Untuk bentuk format file lain, tekan drop down list pada field
            <asp:Image ID="Image11" runat="server" ImageUrl="~/Help/Images/buttontopdfreport.png" />
        </p>
        <p>
            <asp:Image ID="Image12" runat="server" Height="314px" ImageUrl="~/Help/Images/rpttransferorderDone2.png" Width="829px" /></p>
        <p>
            Lakukan hal yang sama untuk kolom store, jika user hanya mencari lewat variable store.</p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
