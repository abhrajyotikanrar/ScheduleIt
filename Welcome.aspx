<%@ Page Title="Welcome to scheduleit" Language="C#" MasterPageFile="~/MainPage.master" AutoEventWireup="true" CodeFile="Welcome.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="WelcomeMessage" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        .welcome{
             color:white; 
             font-size:60px;
             font-family:'Calibri';
        }
        .buttonStartScheduling{
            padding:10px;
            font-family:Calibri;
            font-size:30px;
            background-color:#448AFF;
            color:white;
            cursor:pointer;
            width:230px;
            border-radius:6px;
            
        }
        .buttonStartScheduling:hover{
            background-color:#396FC9;
        }
    </style>
    <div style="background-color:#00695c; background-size: cover; background-attachment: fixed;">
        <br /><br />
        <center><div class="welcome">Hi, <asp:label runat="server" ID="lblWelcomeName" /></div></center>
        <center><div class="welcome"><asp:label runat="server" ID="lblWish" text="Good Morning"/></div></center>
        <br /><br /><br /><br />
            <center><div class="buttonStartScheduling" onclick="window.location='schedule.aspx'">Start Scheduling</div></center> 
        <br /><br /><br />
    </div>
</asp:Content>

