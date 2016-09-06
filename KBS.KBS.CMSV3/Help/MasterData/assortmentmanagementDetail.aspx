<%@ Page Language="C#" AutoEventWireup="true" CodeFile="assortmentmanagementDetail.aspx.cs" Inherits="assortmentmanagementDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assortment Management Detail</title>
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
    <form id="assortmentmanagdetail" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>ASSORTMENT MANAGEMENT DETAIL</strong></a><o:p></o:p></h1>
    </div>
        <p>
        <span style="text-decoration: underline">Definition</span></p>
        <p>
            Merupakan detail dari assortment header yang anda pilih.
        </p>
        <p>
            Screen Assortment Management Detail.</p>
        <p>
            &nbsp;<asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/assortmentmanagementAdd.png" Width="829px" />
        </p>
        <p>
            Anda bisa melakukan Add dengan menekan tombol <asp:Image ID="Image2" runat="server" ImageUrl="~/Help/Images/buttonadd.png" Height="31px" Width="33px" /> atau menghapus dengan menekan tombol <asp:Image ID="Image10" runat="server" ImageUrl="~/Help/Images/navigasiDelete.png" Height="31px" Width="33px" />.</p>
        <p>
            <strong>Change Status Assortment</strong></p>
        <p>
            Masukkan artikel yang akan dirubah statusnya dari assortment pada kolom item ID, kemudian tekan search <asp:Image ID="Image11" runat="server" ImageUrl="~/Help/Images/buttonsearch.png" Height="31px" Width="33px" /> </p>
        <p>
            <asp:Image ID="Image12" runat="server" Height="314px" ImageUrl="~/Help/Images/assortmentmanagementEdit.png" Width="829px" />
        </p>
        <p>
            Pilih artikel/item yang akan dirubah status assortmentnya</p>
        <p>
            <asp:Image ID="Image13" runat="server" Height="314px" ImageUrl="~/Help/Images/assortmentmanagementEdit1.png" Width="829px" />
        </p>
        <p>
            Tekan tombol Change status</p>
        <p>
            <asp:Image ID="Image14" runat="server" Height="314px" ImageUrl="~/Help/Images/assortmentmanagementEdit2.png" Width="829px" />
        </p>
        <p>
            Apabila berhasil maka status assortment pada item akan berubah</p>
        <p>
            <asp:Image ID="Image15" runat="server" Height="314px" ImageUrl="~/Help/Images/assortmentmanagementDone.png" Width="829px" />
        </p>
        <p>
            Change Status Assortment selesai.</p>
        <h3><a name="_Toc456613669">Navigasi Assortment Management Detail</a> <o:p></o:p></h3>
        <p>
            <asp:Image ID="Image7" runat="server" ImageUrl="~/Help/Images/navigasi.png" />
            <o:p></o:p>
        </p>
        <p>
            <asp:Image ID="Image8" runat="server" ImageUrl="~/Help/Images/keterangannavigasi.png" />
            <o:p></o:p>
        </p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
