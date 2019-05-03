<%@ Page Language="C#" AutoEventWireup="true" CodeFile="phone_no_verification.aspx.cs" Inherits="phone_no_verification" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Verify phone number - Scheduleit</title>
    <style type="text/css">
        body{
            margin:0;
            background-color:#CFC1BF;
        }
        .btnverify{
            border:none;
            outline:0;
            background-color:#287A15;
            color:white;
            padding:10px;
            cursor:pointer;
            border-radius:3px;
        }
        .btnverify:hover{
            background-color:#16410C;
        }
    </style>
</head>
<body>
    <form id="frmVerifyPhone" runat="server">
    <div>
         <div style="top:120px; position:fixed; width:100%;">
            <center>
                <div style="font-family:'HoloLens MDL2 Assets'; font-size:20px;">We have sent you an OTP on your number&nbsp;<asp:Label runat="server" ID="lblPhoneNo" />.</div><br /><br />
                <asp:Label runat="server" ID="lblTime" /><br /><br /><br />
                Enter your OTP here<br /><br />
                <asp:TextBox runat="server" ID="txtotp" placeholder="OTP" required="required" style="width:100px; font-family:'HoloLens MDL2 Assets'; font-size:15px; padding:15px; outline:0; border:1px solid #a1a3a3; border-radius:6px; text-align:center;" /><br /><br />
                <asp:Button runat="server" ID="btnverify" Text="Verify OTP" CssClass="btnverify" OnClick="btnverify_Click" />
            </center>
        </div>
    </div>
    </form>
</body>
</html>
