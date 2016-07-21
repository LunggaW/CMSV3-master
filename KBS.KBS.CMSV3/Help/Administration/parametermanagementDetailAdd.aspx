<%@ Page Language="C#" AutoEventWireup="true" CodeFile="parametermanagementDetailAdd.aspx.cs" Inherits="parametermanagementDetailAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add - Parameter Management Detail</title>
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
    <form id="addparammanagdetail" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>PARAMETER MANAGEMENT DETAIL - ADD</strong></a><o:p></o:p></h1>
    </div>
        <p>
            Kolom yang perlu diisi pada saat penambahan parameter detail</p>
        <p>
            <asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/parametermanagementDetailAdd.png" Width="829px" />
        <p>
            Pengertian kolom - kolom pada saat add parameter detail</p>
        <p>
            <asp:Image ID="Image12" runat="server" ImageUrl="~/Help/Images/keteranganparametermanagementDetailAdd.png" />
        </p>
        <p>
           Contoh parameter detail yang sudah di isi value 1, untuk kebutuhan transfer reason</p>
        <p>
            <asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/parametermanagementDetailAddProses.png" Width="829px" />
        <p>
            Lakukan Validasi <asp:Image ID="Image1" runat="server" Height="23px" ImageUrl="~/Help/Images/navigasiValidasi.png" Width="31px" />, jika berhasil, maka parameter akan terdaftar pada kolom Parameter Detail</p>
        <p>
            <asp:Image ID="Image2" runat="server" Height="117px" ImageUrl="~/Help/Images/parametermanagementDetailAddDone.png" Width="824px" />
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
