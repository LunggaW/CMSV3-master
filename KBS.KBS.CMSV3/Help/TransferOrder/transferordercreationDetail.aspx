<%@ Page Language="C#" AutoEventWireup="true" CodeFile="transferordercreationDetail.aspx.cs" Inherits="transferordercreationDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transfer Order Creation Detail</title>
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
    <form id="trfordercreationdetail" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>TRANSFER ORDER CREATION DETAIL</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Detail dari transfer order yang telah dibuat.</p>
        <p>
            Screen Transfer order creation detail.</p>
        <p>
            &nbsp;<asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/tocreationDetail.png" Width="829px" />
        </p>
        <p>
            Keterangan Kolom-kolom yang terdapat dalam Transfer order creation detail :</p>
        <p>
            <asp:Image ID="Image6" runat="server" ImageUrl="~/Help/Images/keterangantocreationDetail.png" />
        </p>
        <h3><a name="_Toc456613669">Navigasi Transfer order creation Detail</h3>
        <p>
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Help/Images/navigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <asp:Image ID="Image8" runat="server" ImageUrl="~/Help/Images/keterangannavigasi1.png" />
        </p>
        <p>
            <o:p><strong>Delete Transfer Order Creation Detail</strong></o:p></p>
        <p>
             <o:p>Tekan tombol delete <asp:Image ID="Image1" runat="server" ImageUrl="~/Help/Images/navigasiDelete.png" Height="31px" Width="33px" /></o:p>
        </p>
        <p>
            <o:p>Maka akan muncul popup untuk validasi, apakah yakin data yang dipilih akan di hapus.</o:p></p>
        <p>
            <o:p>Klik OK, maka data akan terhapus.</o:p></p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
