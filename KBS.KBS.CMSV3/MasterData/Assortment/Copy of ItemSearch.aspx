<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeBehind="ItemSearch.aspx.cs" Inherits="KBS.KBS.CMSV3.MasterData.Assortment.ItemSearch" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../Content/New.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function UpdateDetailGrid(s, e) {
            detailGridView.PerformCallback(e.visibleIndex);
        }
    </script>
    <div>
        <span>
            <dx:ASPxButton ID="BackhomeBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/back3.png" ToolTip="Previous Page" BackColor="Transparent"
                OnClick="BackhomeBtn_Click">
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="ValidateBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/valid.png" BackColor="Transparent" ToolTip="Valid">
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="SaveBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/save.png" ToolTip="Save" BackColor="Transparent">
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="ClearBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/clear.png" ToolTip="Clear" BackColor="Transparent">
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
            <dx:ASPxButton ID="SearchBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/search.png" ToolTip="Search" BackColor="Transparent" Enabled="True"
                OnClick="SearchBtn_Click">
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="AddBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/plus.png" ToolTip="Add" BackColor="Transparent" Enabled="True"
                OnClick="AddBtn_Click">
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="DelBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/min.png" ToolTip="Delete" BackColor="Transparent" Enabled="True"
                OnClick="DelBtn_Click">
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="LprevBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/back2.png" BackColor="Transparent" ToolTip="First Page" OnClick="LprevBtn_Click">
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="PrevBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/back.png" BackColor="Transparent" ToolTip="Previous Page"
                OnClick="PrevBtn_Click">
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="NextBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/next.png" BackColor="Transparent" ToolTip="Next Page" OnClick="NextBtn_Click">
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="LnextBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"
                Image-Url="~/image/next2.png" BackColor="Transparent" ToolTip="Last Page" OnClick="LnextBtn_Click">
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span>
    </div>
    <div id="Item">
        <div align="center">
            <asp:Label ID="Label1" runat="server" Font-Size="Large" Text="Item Search"></asp:Label>
            <br />
        </div>
        <div>
            <table class="tableTop">
                <tr>
                    <td class="tableHeader2Column" colspan="5">
                        <dx:ASPxTextBox runat="server" Width="170px" Caption="Item ID" ID="TextBoxItemIdExternal">
                            <CaptionSettings ShowColon="False"></CaptionSettings>
                            <CaptionCellStyle Width="110px">
                            </CaptionCellStyle>
                        </dx:ASPxTextBox>
                    </td>
                    <td class="tableHeader2Column" colspan="10">
                        <dx:ASPxTextBox runat="server" Width="170px" Caption="Variant ID" 
                            ID="TextBoxVariantIdExternal">
                            <CaptionSettings ShowColon="False"></CaptionSettings>
                            <CaptionCellStyle Width="110px">
                            </CaptionCellStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tableHeader2Column" colspan="10">
                        <dx:ASPxTextBox runat="server" Width="300px" Caption="Short Description" ID="TextBoxShortDescription">
                            <CaptionSettings ShowColon="False"></CaptionSettings>
                            <CaptionCellStyle Width="110px">
                            </CaptionCellStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tableHeader2Column" colspan="10">
                        <dx:ASPxTextBox runat="server" Width="500px" Caption="Long Description" ID="TextBoxLongDescription">
                            <CaptionSettings ShowColon="False"></CaptionSettings>
                            <CaptionCellStyle Width="110px">
                            </CaptionCellStyle>
                        </dx:ASPxTextBox>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <dx:ASPxGridView runat="server" ClientInstanceName="detailGridView" CssClass="ASPXGridView"
            ID="ASPxGridViewItemVariant" OnCustomCallback="ASPxGridViewDetail_CustomCallback">
            <ClientSideEvents RowDblClick="UpdateDetailGrid"></ClientSideEvents>
            <SettingsBehavior AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="True">
            </SettingsBehavior>
        </dx:ASPxGridView>
        <br />
        <div align="center">
            <br />
            <dx:ASPxButton ID="ASPxButtonOK" runat="server" OnClick="ASPxButtonSearch_Click"
                Text="OK" Font-Size="Medium">
            </dx:ASPxButton>
            <br />
        </div>
    </div>
</asp:Content>
