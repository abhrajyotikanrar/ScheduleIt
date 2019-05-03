<%@ Page Title="Admin - Scheduleit" Language="C#" MasterPageFile="~/MasterPageAdmin.master" AutoEventWireup="true" CodeFile="admin.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        .startSms, .stopSms{
            padding:10px 0;
            width:100%;
            font-size:16pt;
            cursor:pointer;
            outline:none;
            border:none;
            color:white;
            background-color:#388E3C;
            
        }
        .stopSms{
            padding:10px 0;
            width:100%;
            font-size:16pt;
            cursor:pointer;
            outline:none;
            border:none;
            color:white;
            background-color:#d50000;
            
        }
        .startSms:hover{
            background-color:#29632C;
        }
        .stopSms:hover{
            background-color:#9E0000;
        }
    </style>

    <asp:Button runat="server" ID="btnStartSms" Text="Start SMS service" CssClass="startSms" OnClick="btnStartSms_Click" />
    <br /><br />
    <asp:Button runat="server" ID="btnStopSms" Text="Stop SMS service" CssClass="stopSms" OnClick="btnStopSms_Click" />


    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:Timer ID="timerSmsSending" runat="server" OnTick="timerSmsSending_Tick">
    </asp:Timer>
</asp:Content>