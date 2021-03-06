﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="KBS.KBS.CMSV3.WebForm1" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <link href="Content/New.css" rel="stylesheet" type="text/css" />
    </title>
    <link href="Content/new.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .dxeCaptionCell_DevEx.dxeCaptionVATSys.dxeTextEditCTypeSys
        {
            padding-top: 3px;
        }
        .dxeCaptionCell_DevEx
        {
            font: 11px Verdana, Geneva, sans-serif;
            color: #201f35;
            white-space: nowrap;
            line-height: 16px;
            height: 100%;
        }
        
        .dxeCLLSys, *[dir="rtl"] .dxeCLRSys
        {
            padding-left: 0px;
            padding-right: 6px;
        }
        .dxeCaptionVATSys
        {
            vertical-align: top;
        }
        .dxeCaptionHALSys
        {
            text-align: left;
        }
        .dxeTrackBar_DevEx, .dxeIRadioButton_DevEx, .dxeButtonEdit_DevEx, .dxeTextBox_DevEx, .dxeRadioButtonList_DevEx, .dxeCheckBoxList_DevEx, .dxeMemo_DevEx, .dxeListBox_DevEx, .dxeCalendar_DevEx, .dxeColorTable_DevEx
        {
            -webkit-tap-highlight-color: rgba(0,0,0,0);
        }
        
        .dxeTextBox_DevEx, .dxeButtonEdit_DevEx, .dxeIRadioButton_DevEx, .dxeRadioButtonList_DevEx, .dxeCheckBoxList_DevEx
        {
            cursor: default;
        }
        
        .dxeTextBox_DevEx
        {
            background-color: white;
            border-top: 1px solid #9da0aa;
            border-right: 1px solid #c2c4cb;
            border-bottom: 1px solid #d9dae0;
            border-left: 1px solid #c2c4cb;
            font: 11px Verdana, Geneva, sans-serif;
        }
        
        .dxeTextBoxSys, .dxeButtonEditSys
        {
            width: 170px;
        }
        
        .dxeTextBoxSys, .dxeMemoSys
        {
            border-collapse: separate !important;
        }
        
        input[type="text"].dxeEditArea_DevEx, /*Bootstrap correction*/ input[type="password"].dxeEditArea_DevEx /*Bootstrap correction*/
        {
            margin-top: 0;
            margin-bottom: 1px;
        }
        
        .dxeMemoEditAreaSys, /*Bootstrap correction*/ input[type="text"].dxeEditAreaSys, /*Bootstrap correction*/ input[type="password"].dxeEditAreaSys /*Bootstrap correction*/
        {
            display: block;
            -webkit-box-shadow: none;
            -moz-box-shadow: none;
            box-shadow: none;
            -webkit-transition: none;
            -moz-transition: none;
            -o-transition: none;
            transition: none;
            -webkit-border-radius: 0px;
            -moz-border-radius: 0px;
            border-radius: 0px;
        }
        .dxeEditAreaSys, .dxeMemoEditAreaSys, /*Bootstrap correction*/ input[type="text"].dxeEditAreaSys, /*Bootstrap correction*/ input[type="password"].dxeEditAreaSys /*Bootstrap correction*/
        {
            font: inherit;
            line-height: normal;
            outline: 0;
        }
        
        input[type="text"].dxeEditAreaSys, /*Bootstrap correction*/ input[type="password"].dxeEditAreaSys /*Bootstrap correction*/
        {
            margin-top: 0;
            margin-bottom: 0;
        }
        .dxeEditAreaSys, input[type="text"].dxeEditAreaSys, /*Bootstrap correction*/ input[type="password"].dxeEditAreaSys /*Bootstrap correction*/
        {
            padding: 0px 1px 0px 0px; /* B146658 */
        }
        .dxeTextBox_DevEx .dxeEditArea_DevEx
        {
            background-color: white;
        }
        
        .dxeEditArea_DevEx
        {
            border-top: 1px solid #9da0aa;
            border-right: 1px solid #c2c4cb;
            border-bottom: 1px solid #d9dae0;
            border-left: 1px solid #c2c4cb;
        }
        .dxeEditAreaSys
        {
            border: 0px !important;
            background-position: 0 0; /* iOS Safari */
            -webkit-box-sizing: content-box; /*Bootstrap correction*/
            -moz-box-sizing: content-box; /*Bootstrap correction*/
            box-sizing: content-box; /*Bootstrap correction*/
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table id="tableTop">
        <tr>
            <td colspan="2" class="table">
                <dx:ASPxTextBox runat="server" Width="100px" ReadOnly="True" Caption="Entry" 
                    ID="ASPxTextBoxDetailEntry">
                    <ReadOnlyStyle BackColor="Silver">
                    </ReadOnlyStyle>
                    <CaptionCellStyle Width="80px">
                    </CaptionCellStyle>
                </dx:ASPxTextBox>
            </td>
            <td colspan="6" class="table">
                <dx:ASPxTextBox runat="server" Width="80%" Caption="Short Desc" 
                    ID="ASPxTextBoxDetailSDesc">
                    <CaptionCellStyle Width="80px">
                    </CaptionCellStyle>
                </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td colspan="8" class="table">
                <dx:ASPxTextBox runat="server" Width="100%" Caption="Long Desc" ID="ASPxTextBoxDetailLDesc">
                    <CaptionCellStyle Width="80px">
                    </CaptionCellStyle>
                </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="table">
                <dx:ASPxTextBox runat="server" Width="50px" Caption=" Value 1" 
                    ID="ASPxTextBoxDetailParVal1">
                    <CaptionCellStyle Width="80px">
                    </CaptionCellStyle>
                </dx:ASPxTextBox>
            </td>
            <td colspan="2" class="table">
                <dx:ASPxTextBox runat="server" Width="50px" Caption="Value 2" 
                    ID="ASPxTextBoxDetailParVal2">
                    <CaptionCellStyle Width="80px">
                    </CaptionCellStyle>
                </dx:ASPxTextBox>
            </td>
            <td colspan="2" class="table">
                <dx:ASPxTextBox runat="server" Width="50px" Caption="Value 3" 
                    ID="ASPxTextBoxDetailParVal3">
                    <CaptionCellStyle Width="80px">
                    </CaptionCellStyle>
                </dx:ASPxTextBox>
            </td>
            <td colspan="2" class="table">
                <dx:ASPxTextBox runat="server" Width="50px" Caption=" Value 4" 
                    ID="ASPxTextBoxDetailParVal4">
                    <CaptionCellStyle Width="80px">
                    </CaptionCellStyle>
                </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="table">
            <dx:ASPxTextBox runat="server" Width="170px" Caption="Char 1" 
                ID="ASPxTextBoxDetailParValChar1">
                <CaptionCellStyle Width="80px">
                </CaptionCellStyle>
            </dx:ASPxTextBox>
            </td>
            <td colspan="2" class="table">
            <dx:ASPxTextBox runat="server" Width="170px" Caption="Char 2" 
                    ID="ASPxTextBoxDetailParValChar2">
                <CaptionCellStyle Width="80px">
                </CaptionCellStyle>
            </dx:ASPxTextBox>
            </td>
            <td colspan="2" class="table">
            <dx:ASPxTextBox runat="server" Width="170px" Caption="Char  3" 
                ID="ASPxTextBoxDetailParValChar3">
                <CaptionCellStyle Width="80px">
                </CaptionCellStyle>
            </dx:ASPxTextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="table">
            <dx:ASPxDateEdit runat="server" Caption="Date 1" ID="ASPxDateEditPar1">
                <CaptionCellStyle Width="80px">
                </CaptionCellStyle>
            </dx:ASPxDateEdit>
            </td>
            <td colspan="2" class="table">
            <dx:ASPxDateEdit runat="server" Caption="Date 2" ID="ASPxDateEditPar2">
                <CaptionCellStyle Width="80px">
                </CaptionCellStyle>
            </dx:ASPxDateEdit>
            </td>
            <td colspan="2" class="table">
            <dx:ASPxDateEdit runat="server" Caption="Date 3" ID="ASPxDateEditPar3">
                <CaptionCellStyle Width="80px">
                </CaptionCellStyle>
            </dx:ASPxDateEdit>
            </td>
        </tr>
         <tr>
            <td colspan="8" rowspan="3">
        
            <dx:ASPxMemo runat="server" Height="71px" Width="429px" Caption="Comment" 
            ID="ASPxMemoComment">
                <CaptionCellStyle Width="80px">
                </CaptionCellStyle>
            </dx:ASPxMemo>
            </td>
        </tr>
    </table>
    
    </form>
</body>
</html>
