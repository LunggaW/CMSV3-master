<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
 CodeBehind="PriceMasterManagementNew.aspx.cs" Inherits="KBS.KBS.CMSV3.MasterData.PriceMasterManagement.PriceMasterManagementNew" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">   
    <script src="../../Scripts/PopUp.js" type="text/javascript"></script>
<script src="../../Scripts/CustomJS.js" type="text/javascript"></script>
    <script type="text/javascript">
        function UpdateDetailGrid(s, e) {
            detailGridView.PerformCallback(e.visibleIndex);
        }
    </script>
    <link rel="stylesheet" type="text/css" href="../../Content/New.css" />
    <div>
        <dx:ASPxPopupControl ID="pcLogin" runat="server" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
        PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ClientInstanceName="pcLogin"
        HeaderText="Warning" AllowDragging="True" PopupAnimationType="None" EnableViewState="False">
        <ClientSideEvents PopUp="function(s, e) { ASPxClientEdit.ClearGroup('entryGroup'); tbLogin.Focus(); }" />
        <ContentCollection>
            <dx:PopupControlContentControl runat="server">
                <dx:ASPxPanel ID="ASPxPanel1" runat="server" DefaultButton="btOK">
                    <PanelCollection>
                        <dx:PanelContent runat="server">
                             <dx:ASPxLabel ID="LabelData" runat="server" Text ="Apakah Ingin Di Simpan Terlebih Dahulu ?"></dx:ASPxLabel>
                            <table style="margin-top:20px"> 
                                <tr>
                                    <td>
                                       
                                    </td>
                                </tr>                                                              
                                <tr>
                                    <td colspan="2">
                                            <dx:ASPxButton ID="btnbacksave" runat="server" Text="Ok" Width="80px" AutoPostBack="False" OnClick="ValidateBtn_Click" style="float: left; margin-right: 8px">                                                
                                            <ClientSideEvents Click="function(s, e) { pcLogin.Hide(); }" />
                                            </dx:ASPxButton>
                                            
                                                                                   
                                    </td>
                                    <td colspan="2">
                                    <dx:ASPxButton ID="btnbacknot" runat="server" Text="No" Width="80px" OnClick="BackhomeBtn_Click" AutoPostBack="False" style="float: left; margin-right: 8px">                                                
                                        <ClientSideEvents Click="function(s, e) { pcLogin.Hide(); }" />        
                                    </dx:ASPxButton>
                                    </td>
                                    <td colspan="2">
                                    <dx:ASPxButton ID="btCancel" runat="server" Text="Cancel" Width="80px" AutoPostBack="False" style="float: left; margin-right: 8px">
                                                <ClientSideEvents Click="function(s, e) { pcLogin.Hide(); }" />
                                            </dx:ASPxButton> 
                                    </td>
                                </tr>
                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxPanel>
                
            </dx:PopupControlContentControl>
        </ContentCollection>
        <ContentStyle>
            <Paddings PaddingBottom="5px" />
        </ContentStyle>
    </dx:ASPxPopupControl>
        <span>
            <dx:ASPxButton ID="BackhomeBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/back3.png" ToolTip="Previous Page" BackColor="Transparent"
                > 
                <ClientSideEvents Click="Confirm" />
                <Image Height="20px" Width="20px" UrlDisabled="~/image/back3Disable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="ValidateBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/valid.png" BackColor="Transparent" ToolTip="Valid" OnClick="ValidateBtn_Click">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/validDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="SaveBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/save.png" ToolTip="Save" BackColor="Transparent" OnClick="SaveBtn_Click">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/saveDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="ClearBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/clear.png" ToolTip="Clear" BackColor="Transparent">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/clearDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
            <dx:ASPxButton ID="SearchBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/search.png" ToolTip="Search" BackColor="Transparent" Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/searchDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="AddBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/plus.png" ToolTip="Add" BackColor="Transparent" Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/plusDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="DelBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/min.png" ToolTip="Delete" BackColor="Transparent" Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/minDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="LprevBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/back2.png" BackColor="Transparent" ToolTip="First Page" Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/back2Disable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="PrevBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/back.png" BackColor="Transparent" ToolTip="Previous Page"
                Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/backDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="NextBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/next.png" BackColor="Transparent" ToolTip="Next Page" Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/nextDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="LnextBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/next2.png" BackColor="Transparent" ToolTip="Last Page" Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/next2Disable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span>
    </div>
    <br />
    <div align="center" class="title">
        <asp:Label ID="Label1" runat="server" Font-Size="Large" Text="New Price"></asp:Label>
    </div>
    <br />
    <span>
        <dx:ASPxLabel ID="ASPxLabelMessage" runat="server" Font-Size="Large" Text="New Price"
            Visible="False">
        </dx:ASPxLabel>
        <br />
    </span>
    <br />
    <div>
        <table class="tableTop">
           <tr>
                <td class="tableHeader2Column" colspan="5">
                    <dx:ASPxTextBox runat="server" Width="170px" CaptionCellStyle-Width="100px" Caption="ITEM ID" ID="ITEMIDTXT" OnDisposed="ITEMIDTXT_Disposed">
                    </dx:ASPxTextBox>
                </td>
                <td class="tableHeader2Column" colspan="5">
                    <%--<dx:ASPxTextBox runat="server" Width="170px" Caption="VARIANT ID" CaptionCellStyle-Width="100px" ID="VARIANTTXT">
                    </dx:ASPxTextBox>--%>
                     <dx:ASPxComboBox runat="server" Width="170px" CaptionCellStyle-Width="100px" Caption="VARIANT ID" ID="VARIANTBOX">
                    </dx:ASPxComboBox>
                </td>
                  <td class="tableHeader2Column" colspan="5">
                    <dx:ASPxButton runat="server" Width="170px" ID="SearchBtnItem" Text="Search Item Dan Variant" OnClick="Search" ></dx:ASPxButton>
                    
                </td>
            </tr>
            <tr>
                <td class="tableHeader2Column" colspan="5">
                    <dx:ASPxComboBox runat="server" Width="170px" CaptionCellStyle-Width="100px" Caption="SITE" ID="SITEBOX">
                    </dx:ASPxComboBox>
                </td>
                <td class="tableHeader2Column" colspan="5">
                    <dx:ASPxTextBox runat="server" Width="170px" CaptionCellStyle-Width="100px"  onkeypress="return isNumberKey(event)" Caption="PRICE" ID="PRICETXT">
                    </dx:ASPxTextBox>
                </td>
                 <td class="tableHeader2Column" colspan="5">
                    <dx:ASPxCheckBox runat="server" Width="170px" Visible="true"  Text="VAT" ID="VATBOX">
                    </dx:ASPxCheckBox>
                </td>
            </tr>
            <tr>            
                <td class="tableHeader2Column" colspan="5">
                <dx:ASPxDateEdit runat="server" Width="170px" CaptionCellStyle-Width="100px" Caption="START DATE" ID="SDATE">
                </dx:ASPxDateEdit>                    
                </td>            
                <td class="tableHeader2Column" colspan="5">
                <dx:ASPxDateEdit  runat="server" Width="170px" CaptionCellStyle-Width="100px" Caption="END DATE" ID="EDATE">
                </dx:ASPxDateEdit>
                </td>
                  <td class="tableHeader2Column" colspan="5">
                    
                    <dx:ASPxTextBox runat="server" Width="170px" Visible=false CaptionCellStyle-Width="100px" Caption="Internal Item" ID="ITEMIDX">
                    </dx:ASPxTextBox>
                </td>                
            </tr>
            
        </table>
    </div>
    <br />
</asp:Content>
