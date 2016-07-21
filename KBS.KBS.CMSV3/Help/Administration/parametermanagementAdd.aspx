<%@ Page Language="C#" AutoEventWireup="true" CodeFile="parametermanagementAdd.aspx.cs" Inherits="parametermanagementAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add - Parameter Management</title>
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
    <form id="addparametermanagement" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>PARAMETER MANAGEMENT - ADD</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span><strong>New Parameter</strong></span></p>
        <p>
            Untuk membuat parameter baru, sangat di mungkinkan di CMS, Parameter di buat karena berkembangnya kebutuhan perusahaan sehingga perlu di buat aturan yang baru.
        </p>
        <p>
            Pembuatan parameter meliputi header dan detailnya.</p>
        <p>
            Pada saat pengisian kolom, untuk berpindah pada kolom lainnya, gunakan tombol tab, jangan menggunakan enter, karena system akan log out.</p>
        <p>
            <asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/parametermanagementAdd.png" Width="829px" />
        </p>
        <p>
            Keterangan kolom :</p>
        <p>
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Help/Images/keteranganparametermanagementHeaderAdd.png" />
        </p>
        <p>
            Gambar tampilan setelah kolom di isi :</p>
        <p>
            <asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/parametermanagementAddDone.png" Width="829px" />
        </p>
        <p>
            Lakukan Validasi <asp:Image ID="Image1" runat="server" Height="23px" ImageUrl="~/Help/Images/navigasiValidasi.png" Width="31px" />, jika berhasil, maka parameter akan terdaftar pada kolom Parameter</p>
        <p>
            <asp:Image ID="Image6" runat="server" Height="314px" ImageUrl="~/Help/Images/parametermanagementAddDone1.png" Width="829px" />
        </p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
