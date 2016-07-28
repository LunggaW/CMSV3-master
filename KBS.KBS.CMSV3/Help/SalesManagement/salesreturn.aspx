<%@ Page Language="C#" AutoEventWireup="true" CodeFile="salesreturn.aspx.cs" Inherits="salesreturn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Return</title>
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
    <form id="salesrtn" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>SALES RETURN</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Berfungsi untuk mengembalikan sales yang sudah divalidasi dari transaksi yang terjadi agar tidak menjadi sales, dikarenakan alasan tertentu seperti: barang cacat, tukar barang dan lain-lain.
        </p>
        <h4><span style="mso-bidi-language:AR-SA;mso-no-proof:yes">Akses menu Sales Return:<o:p></o:p></span></h4>
        <p>
            Sales Management -> Sales Return</p>
        <p>
            Screen Sales Return.</p>
        <p>
            &nbsp;<asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/salesreturn.png" Width="829px" />
        </p>
        <p>
            Keterangan Kolom-kolom yang terdapat dalam Sales Validation :</p>
        <p>
            <asp:Image ID="Image6" runat="server" ImageUrl="~/Help/Images/keterangansalesreturn.png" />
        </p>
        <h3><a name="_Toc456613669">Navigasi S</a>ales Return</h3>
        <p>
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Help/Images/navigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <asp:Image ID="Image8" runat="server" ImageUrl="~/Help/Images/keterangannavigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <o:p><strong>Button Pada Sales Return</strong></o:p></p>
        <p>
            &nbsp;<asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/salesreturnButton.png" Width="829px" />
        <p style="width: 824px">
            •	Return	: berfungsi untuk meretur transaksi yang telah di validasi.<br />
•	Entry	: berfungsi untuk melihat detail transaksi

.<p>
            <o:p><strong>Return Sales Return</strong></o:p></p>
        <p>
            Sebuah sales dapat di return karena banyak hal, untuk melakukan return adalah dengan cara :</p>
        <p>
            <o:p>Tekan tombol <asp:Image ID="Image1" runat="server" ImageUrl="~/Help/Images/returnbutton.png" Height="31px" Width="86px" /></o:p>
        </p>
        <p>
            <o:p>Maka akan muncul popup untuk validasi, apakah yakin data yang dipilih akan di return.</o:p></p>
        <p>
            <o:p>Apabila sudah yakin, tekan OK. Maka sales tersebut akan di return dan akan kembali ke menu sales validation.</o:p></p>
        <p>
            &nbsp;<asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/salesreturnDone.png" Width="829px" />
            <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
