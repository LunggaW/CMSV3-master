<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeBehind="SalesHeader.aspx.cs" Inherits="KBS.KBS.CMSV3.Report.SalesHeader" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/New.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function UpdateDetailGrid(s, e)
        {
            detailGridView.PerformCallback(e.visibleIndex);
        }
    </script>
    <div>
        <span>
            <dx:ASPxButton ID="BackhomeBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/back3.png" ToolTip="Previous Page" BackColor="Transparent"
                OnClick="BackhomeBtn_Click" Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/back3Disable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="ValidateBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/valid.png" BackColor="Transparent" ToolTip="Valid" Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/validDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="SaveBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/save.png" ToolTip="Save" BackColor="Transparent" Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/saveDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="ClearBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/clear.png" ToolTip="Clear" BackColor="Transparent" Enabled="True">
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
                Image-Url="~/image/plus.png" ToolTip="Add" BackColor="Transparent" Enabled="False"
                OnClick="AddBtn_Click">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/plusDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="DelBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/min.png" ToolTip="Delete" BackColor="Transparent" Enabled="False"
                OnClick="DelBtn_Click">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/minDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="LprevBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/back2.png" BackColor="Transparent" ToolTip="First Page" OnClick="LprevBtn_Click" Enabled="False">
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
                OnClick="PrevBtn_Click" Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/backDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="NextBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/next.png" BackColor="Transparent" ToolTip="Next Page" OnClick="NextBtn_Click" Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/nextDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="LnextBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/next2.png" BackColor="Transparent" ToolTip="Last Page" OnClick="LnextBtn_Click" Enabled="False">
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
                <ClientSideEvents Click="function (s, e) {window.open('../../Help/Administration/reportsalesheader.aspx', '_blank')}"></ClientSideEvents>
                <Image Height="20px" Width="20px">
                </Image>
                <BackgroundImage ImageUrl="~/image/transback.png"></BackgroundImage>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span>
    </div>
    <div align="center">
        <asp:Label ID="Label1" runat="server" Font-Size="Large" Text="Sales Header"></asp:Label>
    </div>
    <asp:Label ID="LabelMessage" runat="server" Font-Size="Large" Visible="False"></asp:Label>
    <div>
        <table class="tableTop">
            <tr>
                <td class="tableHeader2Column" colspan="5">
                    <dx:ASPxDateEdit ID="ASPxDateEditFrom" runat="server" Caption="From">
                        <CaptionCellStyle Width="110px">
                        </CaptionCellStyle>
                        <CaptionSettings ShowColon="False"></CaptionSettings>
                    </dx:ASPxDateEdit>
                </td>
                <td class="tableHeader2Column" colspan="10">
                    <dx:ASPxComboBox runat="server" Caption="Site" ID="ComboSite">
                        <CaptionSettings ShowColon="False"></CaptionSettings>
                        <CaptionCellStyle Width="110px">
                        </CaptionCellStyle>
                    </dx:ASPxComboBox>
                </td>
            </tr>
            <tr>
                <td class="tableHeader2Column" colspan="5">
                    <dx:ASPxDateEdit ID="ASPxDateEditTo" runat="server" Caption="To">
                        <CaptionCellStyle Width="110px">
                        </CaptionCellStyle>
                        <CaptionSettings ShowColon="False"></CaptionSettings>
                    </dx:ASPxDateEdit>
                </td>
                <td class="tableHeader2Column" colspan="5">

                    <dx:ASPxComboBox runat="server" Caption="Type" ID="ComboType" SelectedIndex="0">
                        <Items>
                            <dx:ListEditItem Selected="True" Text="All" Value="1" />
                            <dx:ListEditItem Text="Sales" Value="2" />
                            <dx:ListEditItem Text="Retur" Value="3" />
                        </Items>
                        <CaptionSettings ShowColon="False"></CaptionSettings>
                        <CaptionCellStyle Width="110px">
                        </CaptionCellStyle>
                    </dx:ASPxComboBox>

                </td>
            </tr>

        </table>
    </div>
    <br />
    <dx:ASPxGridView runat="server" ClientInstanceName="detailGridView" CssClass="ASPXGridView"
        ID="ASPxGridViewSalesHeader" OnCustomCallback="ASPxGridViewDetail_CustomCallback">
        <ClientSideEvents RowDblClick="UpdateDetailGrid"></ClientSideEvents>
        <SettingsBehavior AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="True"></SettingsBehavior>
        <SettingsCookies Enabled="True" />
    </dx:ASPxGridView>
    <br />
    <table class="tableTop">
        <tr>
            <td colspan="5">
                <asp:Label ID="LabelTotalNota" runat="server" Text="Total Nota" Visible="False" Width="170px"></asp:Label>
            </td>
            <td colspan="5">
                <asp:Label ID="LabelTotalItem" runat="server" Text="Total Item" Visible="False" Width="170px"></asp:Label>
            </td>
            <td colspan="5">
                <asp:Label ID="LabelTotalQuantity" runat="server" Text="Total Quantity" Visible="False" Width="170px"></asp:Label>
            </td>
            <td colspan="5">
                <asp:Label ID="LabelTotalDisc" runat="server" Text="Total Discount" Visible="False" Width="170px"></asp:Label>
            </td>
            <td colspan="5">
                <asp:Label ID="LabelTotalSales" runat="server" Text="Total Sales" Visible="False" Width="170px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="5" style="height: 28px">
                <dx:ASPxTextBox runat="server" Width="170px" ID="TextBoxTotalNota" ReadOnly="True" Visible="False">
                    <ReadOnlyStyle BackColor="Silver">
                    </ReadOnlyStyle>
                    <CaptionSettings ShowColon="False"></CaptionSettings>

                </dx:ASPxTextBox>
            </td>
            <td colspan="5" style="height: 28px">
                <dx:ASPxTextBox runat="server" Width="170px" ID="TextBoxTotalItem" ReadOnly="True" Visible="False">
                    <ReadOnlyStyle BackColor="Silver">
                    </ReadOnlyStyle>
                    <CaptionSettings ShowColon="False"></CaptionSettings>

                </dx:ASPxTextBox>
            </td>
            <td colspan="5" style="height: 28px">
                <dx:ASPxTextBox runat="server" Width="170px" ID="TextBoxTotalQty" ReadOnly="True" Visible="False">
                    <ReadOnlyStyle BackColor="Silver">
                    </ReadOnlyStyle>
                    <CaptionSettings ShowColon="False"></CaptionSettings>

                </dx:ASPxTextBox>
            </td>
            <td colspan="5" style="height: 28px">
                <dx:ASPxTextBox runat="server" Width="170px" ID="TextBoxTotalDisc" ReadOnly="True" Visible="False">
                    <ReadOnlyStyle BackColor="Silver">
                    </ReadOnlyStyle>
                    <CaptionSettings ShowColon="False"></CaptionSettings>

                </dx:ASPxTextBox>
            </td>
            <td colspan="5" style="height: 28px">
                <dx:ASPxTextBox runat="server" Width="170px" ID="TextBoxTotalSales" ReadOnly="True" Visible="False">
                    <ReadOnlyStyle BackColor="Silver">
                    </ReadOnlyStyle>
                    <CaptionSettings ShowColon="False"></CaptionSettings>

                </dx:ASPxTextBox>
            </td>
        </tr>
    </table>
    <br />
    <div align="center">
        <br />
        <br />
    </div>
</asp:Content>
