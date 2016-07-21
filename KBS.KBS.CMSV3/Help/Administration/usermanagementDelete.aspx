<%@ Page Language="C#" AutoEventWireup="true" CodeFile="usermanagementDelete.aspx.cs" Inherits="usermanagementDelete" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delete - User Management</title>
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
    <form id="deleteusermanagement" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>USER MANAGEMENT - DELETE</strong></a><o:p></o:p></h1>
    </div>
        <p>
            <span><strong>Delete User</strong></span></p>
        <p>
            Fungsi delete digunakan apabila administrator menilai user sudah tidak lagi aktif menggunakan CMS dan mengurangi resiko dari penyalah gunaan password tersebut.
        </p>
        <p>
            Pada saat klik button 
            <asp:Image ID="Image2" runat="server" Height="25px" ImageUrl="~/Help/Images/navigasiDelete.png" Width="31px" /> (Delete), maka akan tampil popup untuk validasi apakah yakin data yang di pilih akan di hapus.<br />
            Jika yakin, pilih YA, maka data akan terhapus.
        </p>
        <p>
            <asp:Image ID="Image4" runat="server" Height="314px" ImageUrl="~/Help/Images/usermanagementDeleteDone.png" Width="829px" />
        </p>
        <p>
            Data yang dipilih sudah terhapus.</p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
