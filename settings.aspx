<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.master" AutoEventWireup="true" CodeFile="settings.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Settings" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br /><br />
    <div style="font-size:50px;">Settings</div>
    <hr />
    <br /><br />
    
            <asp:Button runat="server" ID="btndeleteaccount" Text="Click here to delete your account" style="background-color:transparent; border:none; outline:none; cursor:pointer;" OnClick="btndeleteaccount_Click" OnClientClick="return confirm('Do you really want to delete your account?');" />
        
    
    <br /><br /><br />
</asp:Content>

