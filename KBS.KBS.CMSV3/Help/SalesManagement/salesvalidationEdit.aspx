<%@ Page Language="C#" AutoEventWireup="true" CodeFile="salesvalidationEdit.aspx.cs" Inherits="salesvalidationEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Validation - Edit</title>
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
    <form id="salesvalidedit" runat="server">
    <p>
       <asp:Image ID="Image3" runat="server" Height="54px" ImageUrl="~/Help/Images/KDS.png" Width="58px" />
       <strong>PT. KAHAR DUTA SARANA</strong> </p>
       <div style="text-align:center; border-top:solid ; border-top-color:blue">
       <h1><a name="_Toc456613665"><strong>SALES VALIDATION - EDIT</strong></a><o:p> </o:p></h1>
    </div>
        <p>
            Screen Sales Validation Edit.</p>
        <p>
            &nbsp;<asp:Image ID="Image5" runat="server" Height="314px" ImageUrl="~/Help/Images/salesvalidationEdit.png" Width="829px" />
        </p>
        <p>
            Perubahan hanya dapat untuk merubah kolom Comment</p>
        <p>
            &nbsp;<asp:Image ID="Image6" runat="server" Height="314px" ImageUrl="~/Help/Images/salesvalidationEditProses.png" Width="829px" />
        </p>
        <p>
            Klik validation
            <asp:Image ID="Image7" runat="server" Height="26px" ImageUrl="~/Help/Images/navigasiValidasi.png" Width="32px" />
&nbsp;, maka data akan berubah sesuai dengan perubahan yang dilakukan.</p>
        <p>
            &nbsp;<asp:Image ID="Image1" runat="server" Height="314px" ImageUrl="~/Help/Images/salesvalidationEditDone.png" Width="829px" /></p>
        <p style="border-bottom:solid; border-bottom-color:blue;text-align:right">
           <strong>PT. KAHAR DUTA SARANA</strong></p>
    </form>
</body>
</html>
