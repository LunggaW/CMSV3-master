<%@ Page Language="C#" AutoEventWireup="true" CodeFile="stockdisplay.aspx.cs" Inherits="stockdisplay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Stock Display</title>
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
    <form id="stkdisplay" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>STOCK DISPLAY</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Yaitu menu yang memperlihatkan jumlah stock sebuah item dari sebuah site.
        </p>
        <p>
            Screen Stock Display.</p>
        <p>
            &nbsp;<asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/stockdisplay.png" Width="829px" />
        </p>
        <p>
            Untuk memperlebar view dari screen, maka user bisa menekan tanda panah yang berada di kiri screen stock display.</p>
        <p>
            &nbsp;<asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/stockdisplay1.png" Width="829px" /></p>
        <p>
            Tampilan setelah menekan tanda panah.</p>
        <p>
            &nbsp;<asp:Image ID="Image10" runat="server" Height="314px" ImageUrl="~/Help/Images/stockdisplay2.png" Width="829px" /></p>
        <p>
            Keterangan Kolom-kolom yang terdapat dalam Stock Display :</p>
        <p>
            <asp:Image ID="Image6" runat="server" ImageUrl="~/Help/Images/keteranganstockdisplay.png" />
        </p>
        <p>
            Pada menu ini, terdapat kolom pencarian yang dapat di gunakan user agar dapat melihat stock secara specific. Kolom tersebut diantaranya:<br />
•	Item ID<br />
•	Barcode<br />
• Site<br />
• Description</p>
        <p>
User hanya tinggal memasukkan salah satu variable kolom di atas, kemudian tekan tombol search.<br />
Contoh image screen pencarian dengan menggunakan Site sebagai variabel.
</p>
        <p>
            &nbsp;<asp:Image ID="Image1" runat="server" Height="314px" ImageUrl="~/Help/Images/stockdisplay3.png" Width="829px" style="margin-right: 0px" /></p>
        <p>
            Maka movement yang di tampilkan hanya stock yang sesuai dengan Site yang berada di kolom Site.</p>
        <p>
           &nbsp;<asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/stockdisplay4.png" Width="829px" style="margin-right: 0px" /></p>
        <p>
            Lakukan hal yang sama jika ingin mencari dengan variable lainnya seperti Item ID, Barcode, atau Description.</p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
