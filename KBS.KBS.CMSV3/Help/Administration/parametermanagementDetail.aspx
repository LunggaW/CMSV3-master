<%@ Page Language="C#" AutoEventWireup="true" CodeFile="parametermanagementDetail.aspx.cs" Inherits="parametermanagementDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Parameter Management Detail</title>
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
    <form id="parammanagdetail" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>PARAMETER MANAGEMENT DETAIL</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Adalah parameter tambahan yang lebih spesifik mengatur fungsi khusus lebih dalam dari sebuah Parameter header.
        </p>
        <p>
            Menu parameter detail</p>
        <p>
            <asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/parametermanagementDetail.png" Width="829px" />
        <p>
            Keterangan Kolom-kolom yang terdapat dalam parameter header :</p>
        <p>
            <asp:Image ID="Image6" runat="server" ImageUrl="~/Help/Images/keteranganparametermanagementDetail.png" />
        </p>
        <h3><a name="_Toc456613669">Navigasi Parameter Detail</a></h3>
        <p>
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Help/Images/navigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <asp:Image ID="Image8" runat="server" ImageUrl="~/Help/Images/keterangannavigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <o:p><strong>Delete Paramater Detail</strong></o:p></p>
        <p>
            <o:p>Tekan tombol delete <asp:Image ID="Image1" runat="server" ImageUrl="~/Help/Images/navigasiDelete.png" Height="31px" Width="33px" /></o:p>
        </p>
        <p>
            <o:p>Maka akan muncul popup untuk validasi, apakah yakin data yang dipilih akan di hapus.</o:p></p>
        <p>
            <asp:Image ID="Image11" runat="server" Height="314px" ImageUrl="~/Help/Images/parametermanagementDetailDeleteProses.png" Width="829px" />
            <o:p></o:p>
        </p>
        <p>
            <o:p>Setelah yakin pilih OK, maka data yang dipilih akan dihapus.</o:p></p>
        <p>
            <asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/parametermanagementDetailDeleteDone.png" Width="829px" />
        </p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
