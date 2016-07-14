<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="ParameterManagement.aspx.cs" Inherits="KBS.KBS.CMSV3.Administration.ParameterManagement" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>



<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <%-- DXCOMMENT: Configure your datasource for ASPxGridView --%>
    <dx:ASPxButton ID="ASPxButtonInsert" runat="server" 
         style="height: 21px" 
        Text="New" onclick="ASPxButtonInsert_Click">
    </dx:ASPxButton>
    &nbsp;<dx:ASPxButton ID="ASPxButtonDelete" runat="server" 
         Text="Delete">
    </dx:ASPxButton>
&nbsp;<dx:ASPxButton ID="ASPxButtonUpdate" runat="server" 
         Text="Edit">
    </dx:ASPxButton>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <dx:ASPxButton ID="ASPxButtonRefreshTextbox" runat="server" 
        Text="Refresh TextBox" onclick="ASPxButtonRefreshTextbox_Click" >
    </dx:ASPxButton>
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
    <dx:ASPxPageControl ID="ASPxPageControlProfileManagement" runat="server" 
        ActiveTabIndex="1" Height="700px" Width="500px" AutoPostBack="True" 
        EnableCallBacks="True">
        <TabPages>
            <dx:TabPage Name="TabParameterManagementHeader" Text="Header">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <br />
                        <dx:ASPxLabel ID="ASPxMessage" runat="server" Text="ASPxLabel">
                        </dx:ASPxLabel>
                        <dx:ASPxSplitter ID="ASPxSplitter2" runat="server" Height="195px" 
                            Visible="False">
                            <Panes>
                                <dx:SplitterPane>
                                    <ContentCollection>
                                        <dx:SplitterContentControl runat="server">
                                            ID<dx:ASPxTextBox ID="ASPxTextBoxHeaderID" runat="server" Width="170px">
                                            </dx:ASPxTextBox>
                                            <br />
                                            Name<dx:ASPxTextBox ID="ASPxTextBoxHeaderName" runat="server" 
                                                 Width="170px" AutoPostBack="True">
                                            </dx:ASPxTextBox>
                                            <br />
                                            S Class<br />
                                            <dx:ASPxTextBox ID="ASPxTextBoxHeaderSClas" runat="server" 
                                                Width="170px">
                                            </dx:ASPxTextBox>
                                            <br />
                                            Copy<dx:ASPxTextBox ID="ASPxTextBoxHeaderCopy" runat="server" Width="170px" 
                                                >
                                            </dx:ASPxTextBox>
                                            <br />
                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                                <dx:SplitterPane>
                                    <ContentCollection>
                                        <dx:SplitterContentControl runat="server">
                                            Comment<br />
                                            <dx:ASPxTextBox ID="ASPxTextBoxHeaderComment" runat="server" 
                                                Width="170px" >
                                            </dx:ASPxTextBox>
                                            <br />
                                            Block<br />
                                            <dx:ASPxTextBox ID="ASPxTextBoxHeaderBlock" runat="server" 
                                                Width="170px" >
                                            </dx:ASPxTextBox>
                                            <br />
                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                            </Panes>
                        </dx:ASPxSplitter>
                        <br />
                        <dx:ASPxGridView ID="ASPxGridViewHeader" runat="server" Width="1054px" 
                            OnFocusedRowChanged="ASPxGridViewHeader_FocusedRowChanged">
                            <SettingsBehavior AllowFocusedRow="True" 
                                ProcessFocusedRowChangedOnServer="True" />
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
            <dx:TabPage Name="TabParameterManagementDetail" Text="Detail">
                <ContentCollection>
                    <dx:ContentControl runat="server">
                        <dx:ASPxLabel ID="ASPxMessageDetail" runat="server" Text="ASPxLabel">
                        </dx:ASPxLabel>
                        <dx:ASPxSplitter ID="ASPxSplitter3" runat="server" Height="195px" 
                            Visible="False">
                            <Panes>
                                <dx:SplitterPane>
                                    <ContentCollection>
                                        <dx:SplitterContentControl runat="server">
                                            Entry<dx:ASPxTextBox ID="ASPxTextBoxDetailEntry" runat="server" 
                                                 Width="170px" ReadOnly="True">
                                            </dx:ASPxTextBox>
                                            <br />
                                            Short Description<dx:ASPxTextBox ID="ASPxTextBoxDetailSDesc" runat="server" 
                                                AutoPostBack="True"  
                                                Width="170px">
                                            </dx:ASPxTextBox>
                                            <br />
                                            Long Description<br />
                                            <dx:ASPxTextBox ID="ASPxTextBoxDetaillDesc" runat="server" Width="170px">
                                            </dx:ASPxTextBox>
                                            <br />
                                            SClas<dx:ASPxTextBox ID="ASPxTextBoxDetailSClas" runat="server" Width="170px">
                                            </dx:ASPxTextBox>
                                            <br />
                                            Parameter Value 1<dx:ASPxTextBox ID="ASPxTextBoxDetailParVal1" runat="server" 
                                                Width="170px">
                                            </dx:ASPxTextBox>
                                            <br />
                                            Parameter Value 2<dx:ASPxTextBox ID="ASPxTextBoxDetailParVal2" runat="server" 
                                                Width="170px">
                                            </dx:ASPxTextBox>
                                            <br />
                                            Parameter Value 3<dx:ASPxTextBox ID="ASPxTextBoxDetailParVal3" runat="server" 
                                                Width="170px">
                                            </dx:ASPxTextBox>
                                            <br />
                                            Parameter Value 4<dx:ASPxTextBox ID="ASPxTextBoxDetailParVal4" runat="server" 
                                                Width="170px">
                                            </dx:ASPxTextBox>
                                            <br />
                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                                <dx:SplitterPane>
                                    <ContentCollection>
                                        <dx:SplitterContentControl runat="server">
                                            Parameter Value Character 1<br />
                                            <dx:ASPxTextBox ID="ASPxTextBoxParValChar1" runat="server" Width="170px">
                                            </dx:ASPxTextBox>
                                            <br />
                                            Parameter Value Character 2<br />
                                            <dx:ASPxTextBox ID="ASPxTextBoxDetailParValChar2" runat="server" Width="170px">
                                            </dx:ASPxTextBox>
                                            <br />
                                            Parameter Value Character 3<dx:ASPxTextBox ID="ASPxTextBoxDetailParValChar3" 
                                                runat="server" Width="170px">
                                            </dx:ASPxTextBox>
                                            <br />
                                            Parameter Date 1<dx:ASPxDateEdit ID="ASPxDateEditPar1" runat="server">
                                            </dx:ASPxDateEdit>
                                            <br />
                                            Parameter Date 2<dx:ASPxDateEdit ID="ASPxDateEditPar2" runat="server">
                                            </dx:ASPxDateEdit>
                                            <br />
                                            Parameter Date 3<dx:ASPxDateEdit ID="ASPxDateEditPar3" runat="server">
                                            </dx:ASPxDateEdit>
                                            <br />
                                            Parameter Comment<dx:ASPxTextBox ID="ASPxTextBoxDetailParComment" 
                                                runat="server" Height="58px" Width="170px">
                                            </dx:ASPxTextBox>
                                            <br />
                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                            </Panes>
                        </dx:ASPxSplitter>
                        <dx:ASPxGridView ID="ASPxGridViewDetail" runat="server" 
                            OnFocusedRowChanged="ASPxGridViewHeader_FocusedRowChanged" Width="1054px">
                            <SettingsBehavior AllowFocusedRow="True" 
                                ProcessFocusedRowChangedOnServer="True" />
                        </dx:ASPxGridView>
                    </dx:ContentControl>
                </ContentCollection>
            </dx:TabPage>
        </TabPages>
    </dx:ASPxPageControl>
    <br />

<%-- DXCOMMENT: Configure your datasource for ASPxGridView --%>
    
</asp:Content>
