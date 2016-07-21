<%@ Page Language="C#" AutoEventWireup="true" CodeFile="siteprofilemanagementDetailAdd.aspx.cs" Inherits="siteprofilemanagementDetailAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add - Site Profile Management Detail</title>
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
    <form id="addstprflmanagdetail" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>SITE PROFILE MANAGEMENT DETAIL - ADD</strong></a><o:p></o:p></h1>
    </div>
        <p>
            Screen Site Profile Management Detail pada saat penambahan data</p>
        <p>
            <asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/siteprofilemanagementDetailAdd.png" Width="829px" />
        </p>
        <p>
           Keterangan kolom nya adalah :</p>
        <p>
            <asp:Image ID="Image10" runat="server" Height="77px" ImageUrl="~/Help/Images/keterangansiteprofilemanagementDetail.png" Width="607px" /></p>
        <p>
            Isi kolom-kolom yang terdapat pada screen New profile site link</p>
        <p>
            <asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/siteprofilemanagementDetailAddProses.png" Width="829px" /></o:p></p>
        <p>
            Lakukan Validasi <asp:Image ID="Image1" runat="server" Height="23px" ImageUrl="~/Help/Images/navigasiValidasi.png" Width="31px" />, maka site profil tersebut akan mempunyai acces terhadap site yang di tambahkan</p>
        <p>
            <asp:Image ID="Image6" runat="server" Height="314px" ImageUrl="~/Help/Images/siteprofilemanagementDetailAddDone.png" Width="829px" />
        </p>
        <p>
            Lakukan step yang sama jika masih ada site profile yang akan ditambahkan.</p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
