<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true"
    CodeBehind="AccessProfileDetail.aspx.cs" Inherits="KBS.KBS.CMSV3.Administration.AccessManagement.AccessProfileDetail" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        function showProfileLinkDetail(s, e)
        {
            detailGridView.PerformCallback(e.visibleIndex);
        }
    </script>
    <link href="../../Content/New.css" rel="stylesheet" type="text/css" />
    <div>
        <span>
            <dx:ASPxButton ID="BackhomeBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/back3.png" ToolTip="Previous Page" BackColor="Transparent"
                OnClick="BackhomeBtn_Click">
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
                Image-Url="~/image/clear.png" ToolTip="Clear" BackColor="Transparent"
                Enabled="False">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/clearDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
            <dx:ASPxButton ID="SearchBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/search.png" ToolTip="Search"
                BackColor="Transparent" Enabled="False"
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
                Image-Url="~/image/edit.png" ToolTip="Edit" BackColor="Transparent" Enabled="False" OnClick="EditBtn_Click">
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
                Image-Url="~/image/back2.png" BackColor="Transparent" Enabled="False"
                ToolTip="First Page">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/back2Disable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="PrevBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/back.png" BackColor="Transparent" Enabled="False"
                ToolTip="Previous Page">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/backDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="NextBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/next.png" BackColor="Transparent" Enabled="False"
                ToolTip="Next Page">
                <Image Height="20px" Width="20px" UrlDisabled="~/image/nextDisable.png">
                </Image>
                <Image Height="20px" Width="20px">
                </Image>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span><span>
            <dx:ASPxButton ID="LnextBtn" runat="server" EnableTheming="False" EnableDefaultAppearance="False"
                EnableViewState="False" Height="20px" Width="20px" BackgroundImage-ImageUrl="~/image/transback.png"   UseSubmitBehavior="false" 
                Image-Url="~/image/next2.png" BackColor="Transparent" Enabled="False"
                ToolTip="Last Page">
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
                <ClientSideEvents Click="function (s, e) {window.open('../../Help/Administration/accessmanagementDetail.aspx', '_blank')}"></ClientSideEvents>
                <Image Height="20px" Width="20px">
                </Image>
                <BackgroundImage ImageUrl="~/image/transback.png"></BackgroundImage>
                <Border BorderColor="Transparent" />
            </dx:ASPxButton>
        </span>
    </div>
    <div align="center">
        <asp:Label ID="LabelTitleSiteProfile" runat="server" Font-Size="Large" Text="Access Profile"></asp:Label>
    </div>
    <br />
    <asp:Label ID="LabelMessage" runat="server" Font-Size="Large" Text="Parameter Header"
        Visible="false">
    </asp:Label>
    <br />
    <br />
    <asp:Label ID="LabelAccessProfile" runat="server" Font-Size="Large" Text="" Visible="false">
    </asp:Label>
    <br />
    <br />
    <dx:ASPxComboBox ID="ComboBoxMenu" runat="server" ValueType="System.String" CssClass="AccessProfile"
        Caption="Choose Menu"
        OnSelectedIndexChanged="ComboBoxMenu_SelectedIndexChanged"
        AutoPostBack="True">
        <CaptionCellStyle Width="110px">
        </CaptionCellStyle>
    </dx:ASPxComboBox>
    <br />
    <br />
    <div>
        <asp:CheckBoxList ID="CheckBoxListData" runat="server" AutoPostBack="True"
            CssClass="AccessProfile"
            OnSelectedIndexChanged="CheckBoxListData_SelectedIndexChanged">
        </asp:CheckBoxList>
    </div>

</asp:Content>
