﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeBehind="ParameterManagementDetailNew.aspx.cs" Inherits="KBS.KBS.CMSV3.Administration.ParameterManagementDetailNew" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Scripts/CustomJS.js" type="text/javascript"></script>
    <script src="../../Scripts/PopUp.js" type="text/javascript"></script>
    <link href="../../Content/New.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function UpdateDetailGrid(s, e)
        {
            detailGridView.PerformCallback(e.visibleIndex);
        }
    </script>
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
                                <dx:ASPxLabel ID="LabelData" runat="server" Text="Apakah Ingin Di Simpan Terlebih Dahulu ?"></dx:ASPxLabel>
                                <table style="margin-top: 20px">
                                    <tr>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <dx:ASPxButton ID="btnbacksave" runat="server" Text="Ok" Width="80px" AutoPostBack="False" OnClick="ValidateBtn_Click" Style="float: left; margin-right: 8px">
                                                <ClientSideEvents Click="function(s, e) { pcLogin.Hide(); }" />
                                            </dx:ASPxButton>


                                        </td>
                                        <td colspan="2">
                                            <dx:ASPxButton ID="btnbacknot" runat="server" Text="No" Width="80px" OnClick="BackhomeBtn_Click" AutoPostBack="False" Style="float: left; margin-right: 8px">
                                                <ClientSideEvents Click="function(s, e) { pcLogin.Hide(); }" />
                                            </dx:ASPxButton>
                                        </td>
                                        <td colspan="2">
                                            <dx:ASPxButton ID="btCancel" runat="server" Text="Cancel" Width="80px" AutoPostBack="False" Style="float: left; margin-right: 8px">
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
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/back3.png" ToolTip="Previous Page" BackColor="Transparent">
                <ClientSideEvents Click="Confirm" />
                <Image Height="20px" Width="20px" UrlDisabled="~/image/back3Disable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="ValidateBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/valid.png" BackColor="Transparent" ToolTip="Valid" OnClick="ValidateBtn_Click">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/validDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="SaveBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/save.png" ToolTip="Save" BackColor="Transparent" OnClick="SaveBtn_Click">
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
                Image-Url="~/image/search.png" ToolTip="Search" BackColor="Transparent" Enabled="False">
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
                <ClientSideEvents Click="function (s, e) {window.open('../../Help/Administration/parametermanagementDetailAdd.aspx', '_blank')}"></ClientSideEvents>

                <Image Height="20px" Width="20px">
                </Image>

                <BackgroundImage ImageUrl="~/image/transback.png"></BackgroundImage>

                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span>
    </div>
    <div align="center" class="title">
        <asp:Label ID="Label1" runat="server" Font-Size="Large" Text="Entry Management"></asp:Label>
    </div>
    <asp:Label ID="LabelMessage" runat="server" Font-Size="Large" Text="Entry Management"></asp:Label>
    <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="100%" Height="400"
        Direction="LeftToRight">
        <table class="tableTop">
            <tr>
                <td class="table">
                    <dx:ASPxTextBox runat="server" Width="100px" ReadOnly="False" Caption="Entry" ID="ASPxTextBoxDetailEntry">
                        <ReadOnlyStyle BackColor="Silver">
                        </ReadOnlyStyle>
                        <CaptionCellStyle Width="80px">
                        </CaptionCellStyle>
                    </dx:ASPxTextBox>
                </td>
                <td colspan="3" class="table">
                    <dx:ASPxTextBox runat="server" Width="80%" Caption="Short Desc" ID="ASPxTextBoxDetailSDesc">
                        <CaptionCellStyle Width="80px">
                        </CaptionCellStyle>
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="table">
                    <dx:ASPxTextBox runat="server" Width="100%" Caption="Long Desc" ID="ASPxTextBoxDetailLDesc">
                        <CaptionCellStyle Width="80px">
                        </CaptionCellStyle>
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="table">
                    <dx:ASPxTextBox runat="server" Width="50px" Caption=" Value 1" ID="ASPxTextBoxDetailParVal1"
                        onkeypress="return isNumberKey(event)">
                        <CaptionCellStyle Width="80px">
                        </CaptionCellStyle>
                    </dx:ASPxTextBox>
                </td>
                <td class="table">
                    <dx:ASPxTextBox runat="server" Width="50px" Caption="Value 2" ID="ASPxTextBoxDetailParVal2"
                        onkeypress="return isNumberKey(event)">
                        <CaptionCellStyle Width="80px">
                        </CaptionCellStyle>
                    </dx:ASPxTextBox>
                </td>
                <td class="table">
                    <dx:ASPxTextBox runat="server" Width="50px" Caption="Value 3" ID="ASPxTextBoxDetailParVal3"
                        onkeypress="return isNumberKey(event)">
                        <CaptionCellStyle Width="80px">
                        </CaptionCellStyle>
                    </dx:ASPxTextBox>
                </td>
                <td class="table">
                    <dx:ASPxTextBox runat="server" Width="50px" Caption=" Value 4" ID="ASPxTextBoxDetailParVal4"
                        onkeypress="return isNumberKey(event)">
                        <CaptionCellStyle Width="80px">
                        </CaptionCellStyle>
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="table">
                    <dx:ASPxTextBox runat="server" Width="170px" Caption="Char 1" ID="ASPxTextBoxDetailParValChar1">
                        <CaptionCellStyle Width="80px">
                        </CaptionCellStyle>
                    </dx:ASPxTextBox>
                </td>
                <td class="table">
                    <dx:ASPxTextBox runat="server" Width="170px" Caption="Char 2" ID="ASPxTextBoxDetailParValChar2">
                        <CaptionCellStyle Width="80px">
                        </CaptionCellStyle>
                    </dx:ASPxTextBox>
                </td>
                <td class="table">
                    <dx:ASPxTextBox runat="server" Width="170px" Caption="Char  3" ID="ASPxTextBoxDetailParValChar3">
                        <CaptionCellStyle Width="80px">
                        </CaptionCellStyle>
                    </dx:ASPxTextBox>
                </td>
                <td class="table">
                    <dx:ASPxTextBox runat="server" Width="50px" Caption=" Value 4" Visible="false" ID="ASPxTextBox1"
                        onkeypress="return isNumberKey(event)">
                        <CaptionCellStyle Width="80px">
                        </CaptionCellStyle>
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="table">
                    <dx:ASPxDateEdit runat="server" Caption="Date 1" ID="ASPxDateEditPar1">
                        <CaptionCellStyle Width="80px">
                        </CaptionCellStyle>
                    </dx:ASPxDateEdit>
                </td>
                <td class="table">
                    <dx:ASPxDateEdit runat="server" Caption="Date 2" ID="ASPxDateEditPar2">
                        <CaptionCellStyle Width="80px">
                        </CaptionCellStyle>
                    </dx:ASPxDateEdit>
                </td>
                <td class="table">
                    <dx:ASPxDateEdit runat="server" Caption="Date 3" ID="ASPxDateEditPar3">
                        <CaptionCellStyle Width="80px">
                        </CaptionCellStyle>
                    </dx:ASPxDateEdit>
                </td>
                <td class="table">
                    <dx:ASPxTextBox runat="server" Width="50px" Caption=" Value 4" Visible="false" ID="ASPxTextBox2"
                        onkeypress="return isNumberKey(event)">
                        <CaptionCellStyle Width="80px">
                        </CaptionCellStyle>
                    </dx:ASPxTextBox>
                </td>
            </tr>
            <tr>
                <td class="table" colspan="4" rowspan="3">
                    <dx:ASPxMemo runat="server" Height="71px" Width="429px" Caption="Comment" ID="ASPxMemoComment">
                        <CaptionCellStyle Width="80px">
                        </CaptionCellStyle>
                    </dx:ASPxMemo>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
</asp:Content>
