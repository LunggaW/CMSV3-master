<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sizemanagementDetail.aspx.cs" Inherits="sizemanagementDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Size Management Detail</title>
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
    <form id="sizemanagdetail" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>SIZE MANAGEMENT DETAIL</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Merupakan detail dari size header yang telah di buat, yang lebih mendetail yang berhubungan dengan size.</p>
        <h4><span style="mso-bidi-language:AR-SA;mso-no-proof:yes">Akses menu Size management detail :</span></h4>
        <p>
            Master Data -> Size management -&gt; Entry</p>
        <p>
            Screen Size Management Detail.</p>
        <p>
            &nbsp;<asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/sizemanagementDetail.png" Width="829px" />
        </p>
        <p>
            Keterangan Kolom-kolom yang terdapat dalam Size Management Detail :</p>
        <p>
            <asp:Image ID="Image6" runat="server" ImageUrl="~/Help/Images/keterangansizemanagementDetail.png" />
        </p>
        <h3><a name="_Toc456613669">Navigasi Size Management</a><o:p> Detail</o:p></h3>
        <p>
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Help/Images/navigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <asp:Image ID="Image8" runat="server" ImageUrl="~/Help/Images/keterangannavigasi1.png" />
            <o:p></o:p>
        </p>
        <p>
            <o:p><strong>Delete Size Management Detail</strong></o:p></p>
        <p>
            <o:p>Sebuah Size Group dapat di hapus apabila belum digunakan/terhubung dengan item.  
Jika sebuah Size Group akan di hapus, maka data yang harus di hapus adalah data yang terdapat pada  level detail terlebih dahulu kemudian level header.
</o:p></p>
        <p>
            <o:p>Tekan tombol delete <asp:Image ID="Image1" runat="server" ImageUrl="~/Help/Images/navigasiDelete.png" Height="31px" Width="33px" /></o:p>
        </p>
        <p>
            <o:p>Maka akan muncul popup untuk validasi, apakah yakin data yang dipilih akan di hapus.</o:p></p>
        <p>
            <asp:Image ID="Image11" runat="server" Height="314px" ImageUrl="~/Help/Images/sizemanagementDetailDeleteProses.png" Width="829px" />
        <p>
            <o:p>Setelah yakin pilih OK, maka data yang dipilih akan dihapus.</o:p></p>
        <p>
            <asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/sizemanagementDetailDeleteDone.png" Width="829px" />
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
