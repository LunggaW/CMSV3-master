<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
CodeBehind="Report.aspx.cs" 
Inherits="KBS.KBS.CMSV3.Report.Report" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/New.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function UpdateDetailGrid(s, e) {
            headerGridView.PerformCallback(e.visibleIndex);
        }
    </script>
    <div>
        <span>
            <dx:ASPxButton ID="BackhomeBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/back3.png" ToolTip="Previous Page" BackColor="Transparent" Enabled="false">
                 <Image Height="20px" Width="20px" UrlDisabled="~/image/back3Disable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="ValidateBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/valid.png" BackColor="Transparent" ToolTip="Valid" Enabled="false">
                 <Image Height="20px" Width="20px" UrlDisabled="~/image/validDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="SaveBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/save.png" ToolTip="Save" BackColor="Transparent" Enabled="false">
                 <Image Height="20px" Width="20px" UrlDisabled="~/image/saveDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="ClearBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/clear.png" ToolTip="Clear" BackColor="Transparent" Enabled="false">
                 <Image Height="20px" Width="20px" UrlDisabled="~/image/clearDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
            <dx:ASPxButton ID="SearchBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/search.png" ToolTip="Search" BackColor="Transparent" Enabled="false"
                OnClick="SearchBtn_Click">
                 <Image Height="20px" Width="20px" UrlDisabled="~/image/searchDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="AddBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/plus.png" ToolTip="Add" BackColor="Transparent" Enabled="false"
                OnClick="AddBtn_Click">
                 <Image Height="20px" Width="20px" UrlDisabled="~/image/plusDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="DelBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/min.png" ToolTip="Delete" BackColor="Transparent" Enabled="false"
                OnClick="DelBtn_Click">
                 <Image Height="20px" Width="20px" UrlDisabled="~/image/minDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="LprevBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/back2.png" BackColor="Transparent" ToolTip="First Page" OnClick="LprevBtn_Click">
                 <Image Height="20px" Width="20px" UrlDisabled="~/image/back2Disable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="PrevBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/back.png" BackColor="Transparent" ToolTip="Previous Page" OnClick="PrevBtn_Click">
                 <Image Height="20px" Width="20px" UrlDisabled="~/image/nextDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="NextBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/next.png" BackColor="Transparent" ToolTip="Next Page" OnClick="NextBtn_Click">
                 <Image Height="20px" Width="20px" UrlDisabled="~/image/next2Disable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="LnextBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/next2.png" BackColor="Transparent" ToolTip="Last Page" OnClick="LnextBtn_Click">
                 <Image Height="20px" Width="20px" UrlDisabled="~/image/backDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="Help" runat="server" EnableTheming="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/question.png" BackColor="Transparent" ToolTip="Help">
                <ClientSideEvents Click="function (s, e) {window.open('../../Help/Administration/stylemanagement.aspx', '_blank')}"></ClientSideEvents>
                <Image Height="20px" Width="20px">
                </Image>
                <BackgroundImage ImageUrl="~/image/transback.png"></BackgroundImage>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span>
    </div>
    <div align="center">
        <asp:Label ID="Label1" runat="server" Font-Size="Large" Text="Report Generate"></asp:Label>
    </div>
    <asp:Label ID="LabelMessage" runat="server" Font-Size="Large" Text="Report Generate"
        Visible="false"></asp:Label>
    <div>
        <table class="tableTop">
            <tr>
                                
            <td>
                <dx:ASPxMemo
                     runat="server" Caption="QUERY" ID="QUERYTXT" Height="75px" Width="500px"  ReadOnly="False" >
                        <CaptionSettings ShowColon="False"></CaptionSettings>
                        <CaptionCellStyle Width="100px">
                        </CaptionCellStyle>
                    </dx:ASPxMemo>
            </td>
            </tr>
            <tr>
                                
            <td>
                <dx:ASPxTextBox ID="FileName" runat="server" Caption="File Name" Width="500px">
                       <CaptionSettings ShowColon="False"></CaptionSettings>
                        <CaptionCellStyle Width="100px">
                        </CaptionCellStyle>
                </dx:ASPxTextBox>
            </td>
            </tr>
        </table>
    </div>
    <br />
    <table class="OptionsBottomMargin">
        <tr>
            <td style="padding-right: 4px">
                <dx:ASPxButton ID="btnPdfExport" runat="server" Text="Export to PDF" UseSubmitBehavior="False"
                    OnClick="btnPdfExport_Click" />
            </td>
            <td style="padding-right: 4px">
                <dx:ASPxButton ID="btnXlsExport" runat="server" Text="Export to XLS" UseSubmitBehavior="False"
                    OnClick="btnXlsExport_Click" />
            </td>
            <td style="padding-right: 4px">
                <dx:ASPxButton ID="btnXlsxExport" runat="server" Text="Export to XLSX" UseSubmitBehavior="False"
                    OnClick="btnXlsxExport_Click" />
            </td>
            <td style="padding-right: 4px">
                <dx:ASPxButton ID="btnRtfExport" runat="server" Text="Export to RTF" UseSubmitBehavior="False"
                    OnClick="btnRtfExport_Click"/>
            </td>
            <td>
                <dx:ASPxButton ID="btnCsvExport" runat="server" Text="Export to CSV" UseSubmitBehavior="False"
                    OnClick="btnCsvExport_Click" />
            </td>
        </tr>
    </table>
    <dx:ASPxGridView runat="server" ClientInstanceName="headerGridView" AutoGenerateColumns="true" ClientSideEvents-BeginCallback="true" CssClass="ASPXGridView"
        ID="ASPxGridViewHeader">        
        <SettingsBehavior AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="True">
        </SettingsBehavior>
    </dx:ASPxGridView>
    <%--<div class="Note">
        <b>Note:</b>
        If you export grouped data to RTF, be sure to open the resulting file with an editor that fully supports RTF, including tables. 
        For instance, Microsoft WordPad does not support this feature, and thus the file may appear corrupt.
    </div>--%>
    <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridViewHeader"></dx:ASPxGridViewExporter>

    <div align="center">
        <br />
        <dx:ASPxButton ID="ASPxButtonEntry" runat="server" OnClick="ASPxButtonEntry_Click"
            Text="Proses" Font-Size="Medium">
        </dx:ASPxButton>
        <br />
    </div>
    <br />
</asp:Content>
