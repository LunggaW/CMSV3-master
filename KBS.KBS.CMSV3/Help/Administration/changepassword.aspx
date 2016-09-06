<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changepassword.aspx.cs" Inherits="changepassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
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
    <form id="changepass" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>CHANGE PASSWORD</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Change Password adalah menu CMS yang mengatur tentang perubahan kata sandi user untuk masuk ke CMS.
        </p>
        <p>
            Password/ kata sandi perlu di ganti secara berkala agar tidak ada orang lain yang menyalah gunakan Password/kata sandi user tersebut. Sehingga privasi user aman dalam menggunakan CMS. </p>
        <p>
            Screen Change Password.</p>
        <p>
            &nbsp;<asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/changepass.png" Width="829px" />
        </p>
        <p>
            Penggunaan menu Change Password sangat mudah di gunakan oleh user, pada gambar diatas tampak jelas screen/layar  Change Password sangat familiar seperti pada system umumnya, dimana kolom-kolomnya mewajibkan user memasukkan password lama, password baru dan konfirmasi password.</p>
        <p>
            Berikut adalah keterangan kolom-kolom yang berada pada screen Change Password.</p>
        <p>
            <asp:Image ID="Image11" runat="server" ImageUrl="~/Help/Images/keteranganchangepass.png" />
        </p>
        <p>
            Diagram alur.</p>
        <p>
            <asp:Image ID="Image12" runat="server" ImageUrl="~/Help/Images/diagramchangepass.png" />
        </p>
        <p>
            Langkah - langkah change password.</p>
        <p>
            <asp:Image ID="Image13" runat="server" ImageUrl="~/Help/Images/keteranganchangepass1.png" />
        </p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
