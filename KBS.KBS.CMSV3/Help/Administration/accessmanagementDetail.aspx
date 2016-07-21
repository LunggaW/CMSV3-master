<%@ Page Language="C#" AutoEventWireup="true" CodeFile="accessmanagementDetail.aspx.cs" Inherits="accessmanagementDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Access Management Detail</title>
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
    <form id="accessmanagdetail" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>ACCESS MANAGEMENT DETAIL</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Acces profile detail yang berada di dalam profile header, yang akan menghubungkan dengan hak akses pengoprasian CMS.</p>
        <p>
            Screen Acces Profile Detail</p>
        <p>
            <asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/accessmanagementDetail.png" Width="829px" />
        </p>
        <p>
            Klik tanda dropdown list, sehingga akan muncul berbagai macam menu yang access nya dapat di sematkan dan di atur accesnya.</p>
        <p>
            <asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/accessmanagementDetailProses.png" Width="829px" />
        <p>
            Jika salah satu menu di pilih, maka akan muncul acces yang dapat di ceklist sesuai kebutuhan profile, dimana menu yang di ceklist tersebut akan muncul di tool bar user.</p>
        <p>
            <asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/accessmanagementDetailProses1.png" Width="829px" />
        <p>
            Lakukan Validasi <asp:Image ID="Image1" runat="server" Height="23px" ImageUrl="~/Help/Images/navigasiValidasi.png" Width="31px" />, maka acces profil tersebut akan mempunyai kewenangan sesuai acces detail yang di ceklist, dan akan muncul pada setiap toolbarnya.</p>
        <p>
            Lakukan step yang sama jika masih ada acces profile menu yang akan di beri menu acces.</p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
