<%@ Page Title="" Language="C#" MasterPageFile="~/MobileMain.master" AutoEventWireup="true"
    CodeBehind="SalesInputMobileSimple.aspx.cs" Inherits="KBS.KBS.CMSV3.SalesManagement.SalesInput.SalesInputMobileSimple" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../../Scripts/CustomJS.js" type="text/javascript"></script>
    <script src="../../Scripts/CustomJS2.js" type="text/javascript"></script>
    <script type="text/javascript">
        function UpdateDetailGrid(s, e)
        {
            detailGridView.PerformCallback(e.visibleIndex);
        }
    </script>

    <link rel="stylesheet" type="text/css" href="../../Content/New.css" />
 
    <br />
    <div style="text-align: center" class="title">
        <asp:Label ID="Label1" runat="server" Font-Size="50" Text="Simple Sales Mobile"></asp:Label>
    </div>
    <br />
    <span>
        <dx:ASPxLabel ID="ASPxLabelMessage" runat="server" Font-Size="Large" Text="ASPxLabel"
            Visible="False">
        </dx:ASPxLabel>
        <br />
    </span>
    <br />
    <div>
        <table style="width:100%;align-content:center;align-items:center;grid-column-align:center;grid-row-align:center" > 
            <tr>
               <td style="width :100%">
                   <asp:Label ID="Label7" runat="server" Font-Size="50" style="margin:auto; text-align:center" Text="TRANSACTION DATE" Width="100%"></asp:Label> 
                    <dx:ASPxDateEdit runat="server"  ID="ASPxDateEditTransDate" Font-Size="50" style="margin:auto" Width="80%" >
                        
                    </dx:ASPxDateEdit>
                </td>
            </tr>
            <tr>
               <td style="width :100%">
                   <asp:Label ID="Label2" runat="server" Font-Size="50" style="margin:auto; text-align:center" Text="NOTA" Width="100%"></asp:Label> 
                    <dx:ASPxTextBox runat="server" Font-Size="50" style="margin:auto" Width="80%"  ReadOnly="False"  ID="TextBoxNota" onkeypress="return isNumberKey(event)">
                        <ReadOnlyStyle BackColor="Silver">
                        </ReadOnlyStyle>
                        
                        
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
               <td style="width :100%">
                   <asp:Label ID="Label3" runat="server" Font-Size="50" style="margin:auto; text-align:center" Text="PROMO" Width="100%"></asp:Label> 
                    <dx:ASPxComboBox runat="server" ID="ComboSKU" Font-Size="50" style="margin:auto" Width="80%" >
                       
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
               <td style="width :100%">
                   <asp:Label ID="Label4" runat="server" Font-Size="50" style="margin:auto; text-align:center" Text="BARCODE" Width="100%"></asp:Label> 
                    <dx:ASPxTextBox runat="server" Font-Size="50" style="margin:auto" Width="80%" ReadOnly="False"  ID="BARCODETXT">
                        <ReadOnlyStyle BackColor="Silver">
                        </ReadOnlyStyle>
                        
                        
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
              <td style="width :100%">
                  <asp:Label ID="Label5" runat="server" Font-Size="50" style="margin:auto; text-align:center" Text="QTY" Width="100%"></asp:Label> 
                    <dx:ASPxTextBox runat="server"  ID="QTYTXT" onkeypress="return isNumberKeyQty(event)" Font-Size="50" style="margin:auto" Width="80%" >
                       
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
               <td style="width :100%">
                   <asp:Label ID="Label6" runat="server" Font-Size="50" style="margin:auto; text-align:center" Text="AMOUNT" Width="100%"></asp:Label> 
                    <dx:ASPxTextBox runat="server"  ReadOnly="False" ID="ASPxTextBoxAmt" onkeypress="return isNumberKey(event)" Font-Size="50" style="margin:auto" Width="80%" >
                        <ReadOnlyStyle BackColor="Silver">
                        </ReadOnlyStyle>
                        
                        
                    </dx:ASPxTextBox>
                </td>
            </tr>
            
            
        </table>
    </div>
     <br />
     <br />
    <div align="center">
               <dx:ASPxButton ID="SaveBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False" 
                        EnableViewState="False" Height="50px" Width="500px" UseSubmitBehavior="false"  Text="SAVE" 
                        Font-Size="50" style="margin-left:auto; margin-right:auto" HorizontalAlign="Center" 
                        ToolTip="Save" OnClick="SaveBtn_Click">                        
                    </dx:ASPxButton> 
            </div>
    <br />
</asp:Content>
