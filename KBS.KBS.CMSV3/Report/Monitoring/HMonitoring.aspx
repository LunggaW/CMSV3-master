﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeBehind="HMonitoring.aspx.cs" Inherits="KBS.KBS.CMSV3.Report.Monitoring.HMonitoring" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function UpdateDetailGrid(s, e)
        {
            detailGridView.PerformCallback(e.visibleIndex);
        }
    </script>
    <link rel="stylesheet" type="text/css" href="../../Content/New.css" />
    <div>
        <span>
            <dx:ASPxButton ID="BackhomeBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/back3.png" ToolTip="Previous Page" BackColor="Transparent"
                OnClick="BackhomeBtn_Click" Enabled="false">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/back3Disable.png">
                </Image><Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="ValidateBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/valid.png" BackColor="Transparent" ToolTip="Valid" OnClick="ValidateBtn_Click" Enabled="false">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/validDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="SaveBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/save.png" ToolTip="Save" BackColor="Transparent" OnClick="SaveBtn_Click" Enabled="false">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/saveDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="ClearBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/clear.png" ToolTip="Clear" BackColor="Transparent" OnClick="ClearBtn_Click">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/clearDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="SearchBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/search.png" ToolTip="Search" BackColor="Transparent" Enabled="True" OnClick="SearchBtn_Click">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/searchDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="EditBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/edit.png" ToolTip="Edit" BackColor="Transparent" Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/editDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="AddBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/plus.png" ToolTip="Add" BackColor="Transparent" Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/plusDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="DelBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/min.png" ToolTip="Delete" BackColor="Transparent" Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/minDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="LprevBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/back2.png" BackColor="Transparent" ToolTip="First Page" Enabled="False">
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
                Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/backDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="NextBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/next.png" BackColor="Transparent" ToolTip="Next Page" Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/nextDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="LnextBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/next2.png" BackColor="Transparent" ToolTip="Last Page" Enabled="False">
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
                <ClientSideEvents Click="function (s, e) {window.open('../../Help/InventoryManagement/stockmovement.aspx', '_blank')}"></ClientSideEvents>
                <Image Height="20px" Width="20px">
                </Image>
                <BackgroundImage ImageUrl="~/image/transback.png"></BackgroundImage>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span>
    </div>
    <br />
    <div align="center" class="title">
        <asp:Label ID="Label1" runat="server" Font-Size="Large" Text="Transaction Monitoring"></asp:Label>
    </div>
    <br />
    <asp:Label ID="LabelMessage" runat="server" Font-Size="Large" Visible="False"></asp:Label>
    <br />
    <br />
    <div>
        <table class="tableTop">            
            <tr>
                 <td class="tableHeader2Column" colspan="5">
                    <dx:ASPxComboBox runat="server" Caption="Site" ID="ComboSite">
                        <CaptionSettings ShowColon="False"></CaptionSettings>
                        <CaptionCellStyle Width="110px">
                        </CaptionCellStyle>
                    </dx:ASPxComboBox>
                </td>
              
                <td class="tableHeader2Column" colspan="5">
                    <dx:ASPxDateEdit ID="DateFrom" runat="server" Caption="Start Date">
                        <CaptionCellStyle Width="110px">
                        </CaptionCellStyle>
                        <CaptionSettings ShowColon="False"></CaptionSettings>
                    </dx:ASPxDateEdit>
                </td>
                <td class="tableHeader2Column" colspan="5">
                    <dx:ASPxDateEdit ID="DateEnd" runat="server" Caption="End Date">
                        <CaptionCellStyle Width="110px">
                        </CaptionCellStyle>
                        <CaptionSettings ShowColon="False"></CaptionSettings>
                    </dx:ASPxDateEdit>
                </td>
            </tr>

        </table>
        <br />
        <dx:ASPxGridView runat="server" ClientInstanceName="HeaderGridView" CssClass="ASPXGridView"
            ID="HeaderGridView">
           <%-- <ClientSideEvents RowDblClick="UpdateDetailGrid"></ClientSideEvents>--%>
            <SettingsBehavior AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="True"></SettingsBehavior>
            <SettingsCookies Enabled="True" />
        </dx:ASPxGridView>
        <br />
        <div align="center">
            <br />
            <dx:ASPxButton ID="ASPxButtonEntry" runat="server" OnClick="ASPxButtonEntry_Click"
                Text="Detail" Font-Size="Medium">
            </dx:ASPxButton>
            <br />
        </div>
    </div>
</asp:Content>
