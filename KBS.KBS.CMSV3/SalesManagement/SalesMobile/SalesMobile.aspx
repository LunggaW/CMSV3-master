<%@ Page Title="" Language="C#" MasterPageFile="~/MobileMain.master" AutoEventWireup="true"
 CodeBehind="SalesMobile.aspx.cs" Inherits="KBS.KBS.CMSV3.SalesManagement.SalesMobile.SalesMobile" %>

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
        <asp:Label ID="Label1" runat="server" Font-Size="50" Text="Sales Mobile"></asp:Label>
    </div>
    <br />
    <span>
        <dx:ASPxLabel ID="ASPxLabelMessage" runat="server" Font-Size="50" Text="ASPxLabel"
            Visible="False">
        </dx:ASPxLabel>
        <br />
    </span>
    <br />
    <div style="text-align: center; grid-column-align:center;grid-row-align:center; align-content:center;align-items:center;" class="title">
        
    </div>
    <div >
        <table style="width:100%;align-content:center;align-items:center;grid-column-align:center;grid-row-align:center" > 
             <tr>
               <td  >
                    <dx:ASPxTextBox runat="server" Width="100%"  ReadOnly="False" Caption="ITEM ID" ID="ITEMTXT" Visible="false" onkeypress="return isNumberKey(event)" >
                        <CaptionSettings ShowColon="False" Position="Top" HorizontalAlign="Center"></CaptionSettings>
                        <ReadOnlyStyle BackColor="Silver">
                        </ReadOnlyStyle>
                    </dx:ASPxTextBox>
                 </td>
            </tr>
            
            <tr> 
                <td >
                    <dx:ASPxTextBox runat="server" Caption="VARIANT ID" ID="VID" onkeypress="return isNumberKey(event)" Visible="false"  >
                       <CaptionSettings ShowColon="False" Position="Top" HorizontalAlign="Center"></CaptionSettings> 
                        
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr> 
                <td style="width :100%">
                    <br />
                   <asp:Label ID="Label2" runat="server" Font-Size="50" style="margin:auto; text-align:center" Text="TRANSACTION DATE" Width="100%"></asp:Label> 
                    <dx:ASPxDateEdit runat="server"  ID="TDATE" Width="80%" style="margin:auto" Font-Size="50" >
                     </dx:ASPxDateEdit>
                </td> 
           </tr>  
            <tr>  
            <td style="width :100%">
                <asp:Label ID="Label3" runat="server" Font-Size="50" style="margin:auto; text-align:center" Text="NOTA" Width="100%"></asp:Label> 
                    <dx:ASPxTextBox runat="server" Width="80%" style="margin:auto" ReadOnly="False" HorizontalAlign="Center"   ID="NOTATXT" Font-Size="50">
                       
                        <ReadOnlyStyle BackColor="Silver">
                        </ReadOnlyStyle>
                    </dx:ASPxTextBox>
                </td>
            </tr>                          
         
            <tr>
                <td style="width :100%">
                    <asp:Label ID="Label4" runat="server" Font-Size="50" style="margin:auto; text-align:center" Text="ITEM ID" Width="100%"></asp:Label> 
                 <dx:ASPxComboBox runat="server" Width="80%" style="margin:auto" ReadOnly="False"  AutoPostBack="true" Font-Size="50" ID="ITEMBOX" OnSelectedIndexChanged="ITEMBOX_SelectedIndexChanged">
                     
                        <ReadOnlyStyle BackColor="Silver">
                        </ReadOnlyStyle>
                 </dx:ASPxComboBox>
                </td>
            </tr>
            <tr> 
                <td style="width :100%">
                    <asp:Label ID="Label5" runat="server" Font-Size="50" style="margin:auto; text-align:center" Text="VARIANT" Width="100%"></asp:Label> 
                    <dx:ASPxComboBox runat="server" Width="80%" style="margin:auto" ReadOnly="False" AutoPostBack="true" Font-Size="50" ID="VARIANTBOX" OnSelectedIndexChanged="Variant_SelectedIndexChanged">
                      
                         <ReadOnlyStyle BackColor="Silver">
                        </ReadOnlyStyle>
                 </dx:ASPxComboBox>
                </td>
            </tr>
            
            <tr>
                <td style="width :100%">
                    <asp:Label ID="Label6" runat="server" Font-Size="50" style="margin:auto; text-align:center" Text="BARCODE" Width="100%"></asp:Label> 
                    <dx:ASPxTextBox runat="server" Width="80%" style="margin:auto"  ReadOnly="False"  AutoPostBack="true" Font-Size="50" OnTextChanged="BarcodeCek" ID="BARCODETXT">
                      
                        <ReadOnlyStyle BackColor="Silver">
                        </ReadOnlyStyle>
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr> 
                <td style="width :100%">
                    <asp:Label ID="Label7" runat="server" Font-Size="50" style="margin:auto; text-align:center" Text="SKU" Width="100%"></asp:Label> 
                    <dx:ASPxComboBox runat="server" Width="80%" style="margin:auto" ReadOnly="False"  AutoPostBack="true" Font-Size="50" ID="SKUBOX"  >
                     
                        <ReadOnlyStyle BackColor="Silver">
                        </ReadOnlyStyle>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>                
                <td style="width :100%">
                    <asp:Label ID="Label8" runat="server" Font-Size="50" style="margin:auto; text-align:center" Text="QTY" Width="100%"></asp:Label> 
                    <dx:ASPxTextBox runat="server"  ID="QTYTXT" onkeypress="return isNumberKeyQty(event)" Font-Size="50" style="margin:auto" Width="80%" >
                       
                        
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr> 
                <td style="width :100%">
                    <asp:Label ID="Label9" runat="server" Font-Size="50" style="margin:auto; text-align:center" Text="PRICE" Width="100%"></asp:Label> 
                    <dx:ASPxTextBox runat="server"  ID="PRICETXT" onkeypress="return isNumberKey(event)" Font-Size="50" style="margin:auto" Width="80%" >
                       
                        
                    </dx:ASPxTextBox>
                </td>
            </tr>            
            <tr>
                
                <td style="width :100%">
                <asp:Label ID="Label10" runat="server" Font-Size="50" style="margin:auto; text-align:center" Text="COMMENT" Width="100%"></asp:Label> 
                <dx:ASPxMemo
                     runat="server"  ID="COMMENTXT" Height="100px" Width="80%" style="margin:auto"  ReadOnly="False" Font-Size="50" >
                       
                        
                    </dx:ASPxMemo>
                </td>                
            </tr>
            <tr>
                <td >
                    <br />
                    
                </td>
            </tr>
        </table>
        <div align="center">
               <dx:ASPxButton ID="SaveBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False" 
                        EnableViewState="False" Height="50px" Width="500px" UseSubmitBehavior="false"  Text="SAVE" 
                        Font-Size="50" style="margin-left:auto; margin-right:auto" HorizontalAlign="Center" 
                        ToolTip="Save" OnClick="SaveBtn_Click">                        
                    </dx:ASPxButton> 
            </div>
    </div>
    <br />
</asp:Content>
