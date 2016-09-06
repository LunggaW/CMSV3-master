<%@ Page Language="C#" AutoEventWireup="true" CodeFile="assortmentmanagementAdd.aspx.cs" Inherits="assortmentmanagementAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add - Assortment Management</title>
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
    <form id="addassortmentmanagement" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>ASSORTMENT MANAGEMENT - ADD</strong></a><o:p></o:p></h1>
    </div>
        <p>
            Screen new assortment</p>
        <p>
            <asp:Image ID="Image2" runat="server" Height="314px" ImageUrl="~/Help/Images/assortmentmanagementAdd1.png" Width="829px" />
        </p>
        <p>
            Masukkan item/ artikel yang akan di masukkan menjadi assortment baru pada kolom item ID.</p>
        <p>
            <asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/assortmentmanagementAddProses.png" Width="829px" />
        </p>
        <p>
            Kemudian tombol search   <asp:Image ID="Image1" runat="server" Height="23px" ImageUrl="~/Help/Images/buttonsearch.png" Width="31px" />, maka jika artikel/ variantnya berada di halaman tersebut berarti item tersebut belum termasuk pada assortment site, sorot artikel</p>
        <p>
            <asp:Image ID="Image6" runat="server" Height="314px" ImageUrl="~/Help/Images/assortmentmanagementAddProses1.png" Width="829px" />
        </p>
        <p>
            Validasi, maka akan terdapat informasi “Success Inserting”, jika artikel telah masuk assortment site tersebut.</p>
        <p>
            <asp:Image ID="Image7" runat="server" Height="314px" ImageUrl="~/Help/Images/assortmentmanagementAddDone.png" Width="829px" />
        </p>
        <p>
            Lakukan pada item lain yang akan di masukkan pada assortment site.</p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
