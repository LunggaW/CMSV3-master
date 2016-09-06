<%@ Page Language="C#" AutoEventWireup="true" CodeFile="accessmanagement.aspx.cs" Inherits="accessmanagement" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Access Management</title>
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
    <form id="accessmanag" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>ACCESS MANAGEMENT</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Acces management adalah sebuah menu yang di buat untuk Administrator, agar dapat mengontrol kewenangan pengoperasian CMS sehingga masing-masing user pada profile mempunyai akses yang berbeda sesuai dengan functionalitynya.<br />
            Pada acces management, untuk memudahkan pengontrolan, maka di buat sebuah profile acces yang nantinya akan di hubungkan dengan user sehingga akan jelas functionalitynya.
        </p>
        <h4><span style="mso-bidi-language:AR-SA;mso-no-proof:yes">Akses menu User management:<o:p></o:p></span></h4>
        <p>
            Administration -> Access management</p>
        <p>
            <asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/accessmanagement.png" Width="829px" />
        </p>
        <p>
           Pada screen Acces profile header terdapat dua buah panel utama yaitu</p>
        <p>
            •	Panel atas: berfungsi sebagai kolom pencarian untuk acces yang sudah ada, baik secara nama akses profile atau deskkripsi profile</p>
        <p>
            <asp:Image ID="Image9" runat="server" Height="36px" ImageUrl="~/Help/Images/accessmanagementSearch.png" Width="821px" />
        <p>
            •	Panel bawah: berfungsi untuk menampilkan acces profile yang sudah ada</p>
        <p>
            <asp:Image ID="Image10" runat="server" Height="164px" ImageUrl="~/Help/Images/accessmanagement1.png" Width="829px" /></p>
        <p>
            <strong>Diagram Alur</strong></p>
        <p>
            <asp:Image ID="Image11" runat="server" ImageUrl="~/Help/Images/diagramaccesmanagement.png" />
        </p>
        <h3><a name="_Toc456613669">Navigasi Access Management</a><o:p></o:p></h3>
        <p>
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Help/Images/navigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <asp:Image ID="Image8" runat="server" ImageUrl="~/Help/Images/keterangannavigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <o:p><strong>Delete Access Management</strong></o:p></p>
        <p>
            <o:p>Acces profile yang sudah tidak di gunakan, dalam CMS bisa saja di hapus/di delete.<br />
Untuk menghapus sebuah Access Profile, maka harus dilakukan penghapusan pada level detail dan header.<br />
Apabila di lakukan pada header saja, maka akan terdapat  informasi  “FAILED DELETE” atau gagal hapus.
</o:p></p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
