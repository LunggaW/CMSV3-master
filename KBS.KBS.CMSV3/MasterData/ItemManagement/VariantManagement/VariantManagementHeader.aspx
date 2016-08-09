<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeBehind="VariantManagementHeader.aspx.cs" Inherits="KBS.KBS.CMSV3.MasterData.ItemManagement.VariantManagement.VariantManagementHeader" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../../Content/New.css" rel="stylesheet" type="text/css" />
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
                OnClick="BackhomeBtn_Click" Enabled="True">
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
                Image-Url="~/image/clear.png" ToolTip="Clear" BackColor="Transparent">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/clearDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
            <dx:ASPxButton ID="SearchBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/search.png" ToolTip="Search" BackColor="Transparent" Enabled="TRUE"
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
            <dx:ASPxButton ID="AddBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/plus.png" ToolTip="Add" BackColor="Transparent" Enabled="true"
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
                <ClientSideEvents Click="function (s, e) {window.open('../../Help/Administration/variantmanagement.aspx', '_blank')}"></ClientSideEvents>
                <Image Height="20px" Width="20px">
                </Image>
                <BackgroundImage ImageUrl="~/image/transback.png"></BackgroundImage>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span>
    </div>
    <div align="center">
        <asp:Label ID="Label1" runat="server" Font-Size="Large" Text="Variant Management Header"></asp:Label>
    </div>
    <br />
    <table class="tableTopLabel">
        <tr>
            <td class="tableHeader2Column" colspan="10">
                <dx:ASPxTextBox runat="server" Width="500px" Caption="ITEM" ID="ASPxTextBoxItem"
                    ReadOnly="True">
                    <ReadOnlyStyle BackColor="Silver">
                    </ReadOnlyStyle>
                    <CaptionSettings ShowColon="False"></CaptionSettings>
                    <CaptionCellStyle Width="110px">
                    </CaptionCellStyle>
                </dx:ASPxTextBox>
            </td>
        </tr>
    </table>
    <br />
    <asp:Label ID="LabelMessage" runat="server" Font-Size="Large" Visible="False"></asp:Label>
    <br />
    <div>
        <table class="tableTop">
            <tr>
                <td class="tableHeader2Column" colspan="5">
                    <dx:ASPxTextBox runat="server" Width="170px" Caption="Variant ID" ID="TextBoxVariantIdExternal" ReadOnly="False">
                        <ReadOnlyStyle BackColor="Silver">
                        </ReadOnlyStyle>
                        <CaptionSettings ShowColon="False"></CaptionSettings>
                        <CaptionCellStyle Width="110px">
                        </CaptionCellStyle>
                    </dx:ASPxTextBox>
                </td>
                <td class="tableHeader2Column" colspan="10">
                    <dx:ASPxComboBox runat="server" Caption="Status" ID="ComboStatus" Width="170px">
                        <CaptionSettings ShowColon="False"></CaptionSettings>
                        <CaptionCellStyle Width="110px">
                        </CaptionCellStyle>
                    </dx:ASPxComboBox>
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
        ID="ASPxGridViewVariant" OnCustomCallback="ASPxGridViewDetail_CustomCallback">
        <ClientSideEvents RowDblClick="UpdateDetailGrid"></ClientSideEvents>
        <SettingsBehavior AllowFocusedRow="True" ProcessFocusedRowChangedOnServer="True"></SettingsBehavior>
    </dx:ASPxGridView>
    <br />
    <div align="center">
        <div>
            <br />
            <dx:ASPxButton ID="ASPxButtonEntry" runat="server" OnClick="ASPxButtonEntry_Click"
                Text="Entry" Font-Size="Medium">
            </dx:ASPxButton>
            <dx:ASPxButton ID="ASPxButtonBarcode" runat="server"
                Text="Barcode" Font-Size="Medium" OnClick="ASPxButtonBarcode_Click">
            </dx:ASPxButton>
            <br />
        </div>
    </div>
</asp:Content>
