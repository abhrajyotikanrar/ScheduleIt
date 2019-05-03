<%@ Page Title="Verify email address" Language="C#" MasterPageFile="~/MainPage.master" AutoEventWireup="true" CodeFile="verifyemail.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="verifyemail" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        .txtotp{
            text-align:center;
            border-radius:6px;
            padding:10px;
            border:1px solid #a1a3a3;
            font-size:20px;
            width:100px;
        }
        .btnverify{
            border-radius:6px;
            padding:10px;
            background-color:#20811A;
            color:white;
            font-size:15px;
            cursor:pointer;
            border-width:0;
            width:100px;
        }
        .btnverify:hover{
            background-color:#155611;
        }
    </style>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <script type="text/javascript">
        function emailVerificationSuccessAlert(){
            swal({
              title: "Success !",
              text: "Your email has been verified successfully.",
              icon: "success",
              button: "OK",
            });
        }
    </script>

    <center><h3>Check your registered email address for OTP</h3></center><br /><br />
    <center><h3>Enter your OTP here</h3></center>
    <center><asp:TextBox runat="server" ID="txtotp" CssClass="txtotp" placeholder="OTP" required="required" /></center><br /><br /><br />
    <center><asp:Button runat="server" ID="btnverify" CssClass="btnverify" Text="VERIFY" OnClick="btnverify_Click" /></center><br /><br /><br /><br />

</asp:Content>

