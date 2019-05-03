<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminLogin.aspx.cs" Inherits="AdminLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Admin Login - Scheduleit</title>
    <style type="text/css">
        body{
            margin:0;
            background-color:#455A64;            
        }
        .logo{
	        width: 25%;
	        padding: 5px;
    	    margin: 0%;
        }
        .textStyle{
            padding:15px;
            font-family:'HoloLens MDL2 Assets';
            font-size:15px;
            border:1px solid #a1a3a3;
            outline:0;
            border-radius:8px;
            margin:3px;
            width:50%;
        }
        .buttonStyle{
            padding:15px;
            font-family:'HoloLens MDL2 Assets';
            font-size:15px;
            border:none;
            outline:0;
            border-radius:8px;
            margin:3px;
            width:60%;
            cursor:pointer;
            background-color:black;
            color:white;
            font-weight:bold;
        }
        .buttonStyle:hover{
            background-color:#2E531F;
        }
    </style>
</head>
<body>
    <form id="frmAdminLogin" runat="server">
    <div>
        <img src="images/logo.png" class="logo" />
    </div>
    <br /><br /><br />
    <div style="text-align: center;">
    <div style="width:25%; background-color:white; border-radius:4px; margin:0 auto; padding:20px;">
        <br /><br />
        <center>
            <asp:TextBox runat="server" ID="txtusername" placeholder="Username" CssClass="textStyle" required="required" /><br />
            <asp:TextBox runat="server" ID="txtpassword" TextMode="Password" placeholder="Password" CssClass="textStyle" required="required" /><br />
            <br />
            <asp:Button runat="server" ID="btnLogIn" CssClass="buttonStyle" Text="Login" OnClick="btnLogIn_Click" />
        </center>
        <br /><br />
    </div>
    </div>
    </form>
</body>
</html>
