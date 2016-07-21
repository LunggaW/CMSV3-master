<%@ Page Language="C#" AutoEventWireup="true" CodeFile="siteprofilemanagement.aspx.cs" Inherits="siteprofilemanagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Site Profile Management</title>
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
    <form id="stprflmanag" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>SITE PROFILE MANAGEMENT</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Menu Site profile Management adalah salah satu menu CMS yang mengatur akses profile terhadap site mana saja yang dapat di akses.
        </p>
        <h4><span style="mso-bidi-language:AR-SA;mso-no-proof:yes">Akses menu Site profile management:<o:p></o:p></span></h4>
        <p>
            Administration -> Site Profile Management</p>
        <p>
            <asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/siteprofilemanagement.png" Width="829px" />
        </p>
        <p>
           Pada screen Site profile header terdapat dua buah panel utama yaitu</p>
        <p>
            • Panel atas: berfungsi sebagai kolom pencarian untuk site profile yang sudah ada, baik secara nama profile atau deskkripsi site profile</p>
        <p>
            <asp:Image ID="Image9" runat="server" Height="36px" ImageUrl="~/Help/Images/siteprofilemanagementSearch.png" Width="821px" />
        <p>
            • Panel bawah: berfungsi untuk menampilkan site profile yang sudah ada</p>
        <p>
            <asp:Image ID="Image10" runat="server" Height="164px" ImageUrl="~/Help/Images/siteprofilemanagement1.png" Width="829px" /></p>
        <h3><a name="_Toc456613669">Navigasi Site Profile Management</a><o:p></o:p></h3>
        <p>
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Help/Images/navigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <asp:Image ID="Image8" runat="server" ImageUrl="~/Help/Images/keterangannavigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <o:p><strong>Delete Site Profile Management</strong></o:p></p>
        <p>
            <o:p>Site profile yang sudah tidak di gunakan, dalam CMS bisa saja di hapus/di delete.<br />
Untuk menghapus sebuah Site Profile, maka harus dilakukan penghapusan pada level detail dan header.<br />
Apabila di lakukan pada header saja, maka akan terdapat  informasi  “FAILED DELETE” atau gagal hapus.

</o:p>
        </p>
        <p>
            Setelah memastikan bahwa detail pada header yang ingin dihapus sudah terhapus, maka site profile pada header dapat di hapus dengan klik tombol <asp:Image ID="Image1" runat="server" ImageUrl="~/Help/Images/navigasiDelete.png" Height="31px" Width="33px" /><o:p>

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
            <asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/siteprofilemanagementDeleteDone.png" Width="829px" /></o:p></p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
