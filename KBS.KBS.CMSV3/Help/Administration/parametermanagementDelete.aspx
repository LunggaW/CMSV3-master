<%@ Page Language="C#" AutoEventWireup="true" CodeFile="parametermanagementDelete.aspx.cs" Inherits="parametermanagementDelete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delete - Parameter Management</title>
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
    <form id="deleteparametermanagement" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>PARAMETER MANAGEMENT - DELETE</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span><strong>Delete Parameter</strong></span></p>
        <p>
            Jika sebuah parameter tidak akan di gunakan lagi oleh user, maka parameter tersebut dapat di hapus/delete.
        </p>
        <p>
            Pada CMS, untuk menghapus sebuah parameter, tidak dapat dihapus hanya dari level headernya saja, tetapi harus dihapus dengan detailnya.</p>
        <p>
            •	Misalnya: pada saat user menceklist pada kolom copy, maka secara otomatis, parameter yang di tandai tersebut akan berlaku di site store.</p>
        <p>
            <asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/parametermanagementEdit.png" Width="829px" />
        <p>
            •	Atau pada saat user melepaskan ceklist pada kolom lock, maka secara otomatis, semua user yang mempunyai akses ke parameter management akan dapat merubah status parameter tersebut.</p>
        <p>
            <asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/parametermanagementEdit1.png" Width="829px" />
        </p>
        <p>
            Keterangan kolom :</p>
        <p>
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Help/Images/keteranganparametermanagementHeaderAdd.png" />
        </p>
        <p>
            Lakukan Validasi <asp:Image ID="Image1" runat="server" Height="23px" ImageUrl="~/Help/Images/navigasiValidasi.png" Width="31px" />, jika berhasil, maka parameter update sesuai yang anda ubah.</p>
        <p>
            <asp:Image ID="Image6" runat="server" Height="314px" ImageUrl="~/Help/Images/parametermanagementAddDone1.png" Width="829px" />
        </p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
