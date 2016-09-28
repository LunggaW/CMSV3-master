<%@ Page Language="C#" AutoEventWireup="true" CodeFile="salesvalidation.aspx.cs" Inherits="salesvalidation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Validation</title>
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
    <form id="salesvalid" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>SALES VALIDATION</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Berfungsi untuk memvalidasi transaksi yang telah di input di menu Sales input.
        </p>
        <h4><span style="mso-bidi-language:AR-SA;mso-no-proof:yes">Akses menu Sales Validation:<o:p></o:p></span></h4>
        <p>
            Sales Management -> Sales Validation</p>
        <p>
            Screen Sales Validation.</p>
        <p>
            &nbsp;<asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/salesvalidation.png" Width="829px" />
        </p>
        <p>
            Keterangan Kolom-kolom yang terdapat dalam Sales Validation :</p>
        <p>
            <asp:Image ID="Image6" runat="server" ImageUrl="~/Help/Images/keterangansalesvalidation.png" />
        </p>
        <h3><a name="_Toc456613669">Navigasi S</a>ales Validation</h3>
        <p>
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Help/Images/navigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <asp:Image ID="Image8" runat="server" ImageUrl="~/Help/Images/keterangannavigasi4.png" />
            <o:p></o:p>
        </p>
        <p>
            <o:p><strong>Button Pada Sales Validation</strong></o:p></p>
        <p>
            &nbsp;<asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/salesvalidationButton.png" Width="829px" />
        <p style="width: 824px">
            •	Reject	: berfungsi untuk menolak hasil input sales, jika sebuah sales di reject, maka sales akan kembali ke menu sales input.<br />
•	Validate: berfungsi untuk memvalidasi hasil input sales.<br />

•	Entry	: berfungsi untuk melihat detail sales input
.<p>
            <o:p><strong>Validate Sales Input</strong></o:p></p>
        <p>
            <o:p>Sales Input dapat di validate apabila sales input tersebut sudah benar, dengan cara :</o:p></p>
        <p>
            <o:p>Tekan tombol <asp:Image ID="Image1" runat="server" ImageUrl="~/Help/Images/validatebutton.png" Height="31px" Width="86px" /></o:p>
        </p>
        <p>
            <o:p>Maka akan muncul popup untuk validasi, apakah yakin data yang dipilih akan di validasi.</o:p></p>
        <p>
            &nbsp;<asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/salesvalidationvalidation.png" Width="829px" />
        <p>
            <o:p>Apabila sudah yakin, tekan OK. Maka sales input tersebut akan tervalidasi.</o:p></p>
        <p>
            <o:p><strong>Reject Sales Input</strong></o:p></p>
        <p>
            <o:p>Sales input dapat di reject, apabila data yang di input tidak sesuai dengan yang di inginkan.</o:p></p>
        <p>
            <o:p>Dengan menekan tombol edit, anda harus menambahkan comment untuk menjelaskan mengapa sales input tersebut di reject.</o:p></p>
        <p>
            <o:p>Untuk melakukan reject dapat dilakukan dengan menekan tombol <asp:Image ID="Image9" runat="server" ImageUrl="~/Help/Images/rejectbutton.png" Height="31px" Width="86px" /></o:p>
        </p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
