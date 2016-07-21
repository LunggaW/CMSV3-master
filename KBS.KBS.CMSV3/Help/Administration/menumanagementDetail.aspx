<%@ Page Language="C#" AutoEventWireup="true" CodeFile="menumanagementDetail.aspx.cs" Inherits="menumanagementDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Menu Management Detail</title>
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
    <form id="menumanagdetail" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>MENU MANAGEMENT DETAIL</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Detail dari menu profile management header yang akan mendefinisikan menu mana saja yang bisa di akses oleh menu profile header tersebut.
        </p>
        <p>
            Screen Menu Management Detail</p>
        <p>
            <asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/menumanagementDetail.png" Width="829px" />
        </p>
        <p>
           Keterangan kolom nya adalah :</p>
        <p>
            <asp:Image ID="Image10" runat="server" Height="86px" ImageUrl="~/Help/Images/keteranganmenumanagementDetail.png" Width="593px" /></p>
        <h3><a name="_Toc456613669">Navigasi Menu Management</a><o:p> Detail</o:p></h3>
        <p>
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Help/Images/navigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <asp:Image ID="Image8" runat="server" ImageUrl="~/Help/Images/keterangannavigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <o:p><strong>Delete Menu Management Detail</strong></o:p></p>
        <p>
            Klik tombol <asp:Image ID="Image1" runat="server" ImageUrl="~/Help/Images/navigasiDelete.png" Height="31px" Width="33px" /><o:p>

</o:p>
        </p>
        <p>
            Akan muncul Pop up yang menanyakan apakah record akan di delete<o:p>

</o:p>
        </p>
        <p>
            Jika benar akan di hapus, maka tekan tombol Ok<o:p>

</o:p>
        </p>
        <p>
           Jika berhasil, maka akan terdapat Informasi  “SUCCES DELETE” atau berhasil di hapus<o:p>

</o:p>
        </p>
        <p>
            <asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/menumanagementDetailDeleteDone.png" Width="829px" /></o:p></p>
        <p>
            Lakukan step yang sama apabila masih terdapat menu profile detail yang akan di hapus.</p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
