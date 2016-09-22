﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeBehind="SKULinkMasterManagementHeader.aspx.cs"
    Inherits="KBS.KBS.CMSV3.MasterData.SKULinkMasterManagementHeader" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/New.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function UpdateDetailGrid(s, e)
        {
            headerGridView.PerformCallback(e.visibleIndex);
        }
    </script>
    <div>
        <span>
            <dx:ASPxButton ID="BackhomeBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/back3.png" ToolTip="Previous Page" BackColor="Transparent"
                Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/back3Disable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="ValidateBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/valid.png" BackColor="Transparent" ToolTip="Valid"
                Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/validDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="SaveBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/save.png" ToolTip="Save" BackColor="Transparent"
                Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/saveDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="ClearBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/clear.png" ToolTip="Clear" BackColor="Transparent" OnClick="ClearBtn_Click" Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/clearDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="SearchBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/search.png" ToolTip="Search" BackColor="Transparent" Enabled="True"
                OnClick="SearchBtn_Click">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/searchDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="EditBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/edit.png" ToolTip="Edit" BackColor="Transparent" Enabled="True" OnClick="EditBtn_Click">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/editDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="AddBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False" EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/plus.png" ToolTip="Add" BackColor="Transparent" Enabled="True"
                OnClick="AddBtn_Click">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/plusDisable.png">
                </Image>
                <BackgroundImage ImageUrl="~/image/transback.png"></BackgroundImage>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="DelBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/min.png" ToolTip="Delete" BackColor="Transparent" Enabled="True"
                OnClick="DelBtn_Click">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/minDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
                <ClientSideEvents Click="function(s, e) {e.processOnServer = confirm('Are You Sure Want To Delete This Record ?');}" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="LprevBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/back2.png" BackColor="Transparent" ToolTip="First Page" OnClick="LprevBtn_Click">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/back2Disable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="PrevBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/back.png" BackColor="Transparent" ToolTip="Previous Page"
                OnClick="PrevBtn_Click">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/backDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="NextBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/next.png" BackColor="Transparent" ToolTip="Next Page" OnClick="NextBtn_Click">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/nextDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="LnextBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/next2.png" BackColor="Transparent" ToolTip="Last Page" OnClick="LnextBtn_Click">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/next2Disable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="Help" runat="server" EnableTheming="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/question.png" BackColor="Transparent" ToolTip="Help">
                <ClientSideEvents Click="function (s, e) {window.open('../../Help/MasterData/skulinkmanagement .aspx', '_blank')}"></ClientSideEvents>
                <Image Height="20px" Width="20px">
                </Image>
                <BackgroundImage ImageUrl="~/image/transback.png"></BackgroundImage>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
    </div>
    <div align="center">
        <asp:Label ID="Label1" runat="server" Font-Size="Large" Text="SKU Link Management"></asp:Label>
    </div>
    <asp:Label ID="LabelMessage" runat="server" Font-Size="Large" Text="SKU Link Management"
        Visible="false"></asp:Label>
    <div>
        <%--    <table class="tableTop">
            <tr>
                <td class="tableHeader2Column" colspan="5">
                    <dx:ASPxTextBox runat="server" Width="170px" CaptionCellStyle-Width="100px" Caption="SKU" ID="SKUTXT">
                    </dx:ASPxTextBox>
                </td>
                <td class="tableHeader2Column" colspan="5">
                    <dx:ASPxTextBox runat="server" Width="170px" Caption="VARIANT ID" CaptionCellStyle-Width="100px" ID="VARIANTTXT">
                    </dx:ASPxTextBox>
                </td>
                <td class="tableHeader2Column"colspan="5">
                    <dx:ASPxTextBox runat="server" Width="170px" Visible="false" CaptionCellStyle-Width="100px" Caption="VAT" ID="ASPxTextBox2">
                    </dx:ASPxTextBox>
                </td>

            </tr>
            <tr>
                <td class="tableHeader2Column" colspan="5">
                    <dx:ASPxTextBox runat="server" Width="170px" CaptionCellStyle-Width="100px" Caption="SITE" ID="SITETXT">
                    </dx:ASPxTextBox>
                </td>
                <td class="tableHeader2Column" colspan="5">
                    <dx:ASPxTextBox runat="server" Width="170px" CaptionCellStyle-Width="100px" Caption="PRICE" ID="PRICETXT">
                    </dx:ASPxTextBox>
                </td>
                 <td class="tableHeader2Column"colspan="5">
                    <dx:ASPxTextBox runat="server" Width="170px" CaptionCellStyle-Width="100px" Caption="VAT" ID="VATTXT">
                    </dx:ASPxTextBox>
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
                <td class="tableHeader2Column"colspan="5">
                    <dx:ASPxTextBox runat="server" Width="170px" Visible="false" CaptionCellStyle-Width="100px" Caption="VAT" ID="ASPxTextBox1">
                    </dx:ASPxTextBox>
                </td>
            </tr>
        </table>--%>
    </div>
    <br />
    <dx:ASPxGridView runat="server" ClientInstanceName="headerGridView" CssClass="ASPXGridView"
        ID="ASPxGridViewHeader" OnCustomCallback="ASPxGridViewHeader_CustomCallback">
        <ClientSideEvents RowDblClick="UpdateDetailGrid"></ClientSideEvents>
        <Settings ShowFilterRow="True" />
        <SettingsBehavior AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="True"></SettingsBehavior>
        <SettingsSearchPanel Visible="True" />
    </dx:ASPxGridView>
    <br />
</asp:Content>
