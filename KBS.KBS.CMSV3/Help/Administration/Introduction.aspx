<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Introduction.aspx.cs" Inherits="testaspxhelp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Introduction</title>
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
    </style>
</head>
<body>
    <form id="introduction" runat="server">
        <p>
            <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
           <strong>PT. KAHAR DUTA SARANA</strong> </p>
        <div style="text-align:center; border-top:solid ; border-top-color:blue">
        <h1><a name="_Toc456613665"><strong>CONSIGNMENT MANAGEMENT SYSTEM</strong></a><o:p></o:p></h1>
    </div>
    <p>
        <span style="text-decoration: underline">Introduction</span></p>
    <p class="MsoNormal">
        Selamat datang dan Selamat menggunakan CMS (Consignment Management System).<br />CMS adalah sebuah system aplikasi pendukung (third party) berbasis Web yang dapat membantu sebuah ERP agar maksimal dalam pengoperasiannya.<o:p></o:p></p>
    <h3><a name="_Toc456613667">Portal masuk CMS</a></h3>
    <p class="MsoNormal">
        Untuk dapat masuk ke dalam CMS, user harus mengaksesnya lewat Web yang di sediakan oleh PT.KDSBS.<o:p></o:p></p>
    <h3><a name="_Toc456613668">Login masuk CMS</a><o:p></o:p></h3>
    <p class="MsoNormal">
        Berikut dalam gambar dibawah adalah tampilan utama CMS, dimana untuk masuk user diharuskan untuk login terlebih dahulu dengan User ID dan Password yang telah di tentukan.<o:p></o:p></p>
        <asp:Image ID="Image1" runat="server" Height="314px" ImageUrl="~/Help/Images/cmslogin.png" Width="829px" />
    <h3><a name="_Toc456613669">Feature CMS</a><o:p></o:p></h3>
    <p class="MsoNormal">
        Pada CMS terdapat tampilan dua panel utama, di sebelah kanan adalah tampilan eklusif yang menandakan bahwa user sedang menjalankan CMS, tampilan pada panel kiri adalah module yang terdapat pada CMS.<o:p></o:p></p>
    <p class="MsoNormal">
        <o:p>Panel atas, menandakan logo CMS, site yang sedang aktif dan tombol untuk keluar dari CMS/Log Out</o:p>
    </p>
    <p class="MsoNormal">
        Panel bawah menunjukkan hari, bulan, tanggal, tahun pada saat user log in dan disebelahnya terdapat logo provider CMS beserta profile user yang sedang di gunakan.</p>
        <p class="MsoNormal">
            <asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/menuutama.png" Width="829px" />
        </p>
        <h3><a name="_Toc456613669">Navigasi CMS</a><o:p></o:p></h3>
        <p>
            <o:p>Dalam CMS terdapat tombol navigasi untuk mengarahkan fungsi-fungsi CMS.<br />
Berdasarkan acces yang di berikan maka akan terdapat tombol navigasi aktif dan non aktif. <br />
Tombol aktif di tandai dengan warna biru yang menyala, artinya tombol dapat di gunakan <br />
Tombol non aktif di tandai dengan warna abu-abu, artinya tombol tidak dapat di gunakan
</o:p>
        </p>
        <p>
            <o:p>
</o:p>
            <asp:Image ID="Image4" runat="server" ImageUrl="~/Help/Images/navigasi.png" />
        </p>
        <p>
            <asp:Image ID="Image5" runat="server" ImageUrl="~/Help/Images/keterangannavigasi.png" />
        </p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
    </body>
</html>
