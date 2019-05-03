<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login at scheduleit</title>
    <link rel="shortcut icon" href="/icons/icon.png" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
<style type="text/css">
body{
	margin: 0%;
}
.main{
    background-image:url(images/background-login.jpg);
    background-size: cover;
    background-attachment:fixed;
}
.login{
	background-color: none;
	float: none;
    align-content:center;
}
.userpic{
    background: #E8DFDF;
    border: 1px solid #42464b;
    border-right:0px;
    border-top-left-radius: 10px;
    border-bottom-left-radius: 10px;
    height: 265px;
    width: 200px;
    align-items: center;
}
.loginbox {
  background: #E8DFDF;
  border: 1px solid #42464b;
  border-left:0px;
  border-top-right-radius: 10px;
  border-bottom-right-radius: 10px;
  height: 265px;
  width: 350px;
  align-items: center;
}
.textinput{
	font-size: 15px;
	margin: 0px;
	border-radius: 7px;
	border: 1px solid #a1a3a3;
  	box-shadow: 0 1px #fff;
  	box-sizing: border-box;
  	color: #696969;
  	height: 39px;
  	width: 80%;
  	padding-left: 30px;
  	padding-right: 30px;
}
.labelforinput{
	font-size: 15px;
    padding-left: 20px;
    font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    font-weight:bold;
    cursor:default;
}
.labelforgotpw{
    float:right;
    cursor:pointer;
    color:crimson;
    font-size:9pt;
    padding-right:40px;
    font-weight:bold;
    text-decoration:none;
}
.logo{
	width: 323px;
	padding: 10px;
	margin: 0%;
}
.buttonclass{
  width:280px;
  height:30px;
  display:block;
  font-size:16px;
  font-weight:bold;
  font-family:'Helvetica';
  color:#6F2525;
  text-decoration:none;
  text-align:center;
  text-shadow:1px 1px 0px #37a69b;
  padding-top:6px;
  padding-left:6px;
  position:relative;
  cursor:pointer;
  border: none;  
  background-color: #37a69b;
  background-image: linear-gradient(top,#3db0a6,#3111);
  border-radius: 5px;
  box-shadow: inset 0px 1px 0px #2ab7ec, 0px 5px 0px 0px #497a78, 0px 10px 5px #999;
}
button:active {
  top:3px;
  box-shadow: inset 0px 1px 0px #2ab7ec, 0px 2px 0px 0px #31524d, 0px 5px 3px #999;
}
input[class="textinput"]:focus{
	box-shadow: 0 0 4px 1px rgba(128, 0, 128, 0.4);
	outline: 0;
	border: 0px;
}

.footer{
    height: 250px;
    width:100%;
    background-color:#19696F;
}

    .btnlogin {
        border-style: none;
        border-color: inherit;
        border-width: medium;
        width: 280px;
        height: 30px;
        display: block;
        font-size: 16px;
        font-weight: bold;
        font-family: 'Helvetica';
        color: #6F2525;
        text-decoration: none;
        text-align: center;
        text-shadow: 1px 1px 0px #37a69b;
        padding-top: 6px;
        padding-left: 6px;
        position: relative;
        cursor: pointer;
        background-color: #37a69b;
        background-image: url('linear-gradient(top,#3db0a6,#3111)');
        border-radius: 5px;
        box-shadow: inset 0px 1px 0px #2ab7ec, 0px 5px 0px 0px #497a78, 0px 10px 5px #999;
        left: 0px;
        top: 0px;
    }
    .txtforforgotpw{
        padding:15px;
        outline:0;
        width:100%;
        border:1px solid #a1a3a3;
        border-radius:6px;
    }
</style>
</head>

<body>
    <form id="frmlogin" runat="server">
    <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>

    <div class="main">
    <a href="index.html"><img src="images/logo.png" class="logo"/></a>
    <br /><br />
    <div runat="server" id="divpwchangestatus" style="padding:6px; position:relative; margin:0 auto; top:35px; background-color:#009688; font-weight:bold; text-align:center; color:white; width:400px; border:2px solid white; border-radius:6px;">
        Your password has been changed successfully.
    </div>    
    <br /><br />
    <table style="width:100%;">
        <tr>
            <td style="width:10%;"></td>
            <td style="width:80%;">
            <center>
         <div style="position:sticky;">
             <table style="border:0px; border-spacing:0px;">
                 <tr>
                    <td>
                        <br /><br />
                        <div class="userpic">
                            <br /><br /><br />
                            <center>
                                <img src="icons/326497-128.png" width="80px" height="80px"/>
                                <br /><br /><br /><br />
                                <a href="registration.aspx" style="text-decoration:none; color:darkblue;">New user? Register here.</a>
                            </center>
                        </div>
                    </td>
                    <td>
                        <br /><br />
                        <div class="loginbox">
                        <center>
                        <br/><br />
                            <div class="login">
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox runat="server" CssClass="textinput" ID="txtphno" placeholder="Phone number" title="Please enter registered phone number" pattern= "[6-9]{1}[0-9]{9}" onchange="this.setCustomValidity(this.validity.patternMismatch ? this.title : '');" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            </div>
                        </center><br/>
                        <center>
                            <div class="login">
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox runat="server" CssClass="textinput" type="Password" ID="txtpassword" placeholder="Password" />
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            </div>
                        </center>
                        <br />
                        <div data-toggle="modal" data-target="#myModal"><asp:label runat="server" CssClass="labelforgotpw" Text="Forgot Password?" /></div>
                        <br /><br />
                        <center>
                            <div class="login" style="margin-top:5px;">
                                <asp:button ID="btnlogin" CssClass="btnlogin" type="submit" runat="server" OnClick="btnlogin_Click" Text="LOG IN" />
                            </div><br />
                        </center>
                        
                    </div>
                    </td>
                 </tr>
             </table>
        </div>
            </center>
                <br /><br /><br /><br />
            </td>
            <td style="width:10%;"></td>
        </tr>
    </table>
    <br /><br /><br /><br /><br /><br /><br />
    </div>

    <center>
    <div class="footer">
            <br /><br /><br /><br /><br /><br /><br />
            <table style="width:80%;">
                <tr>
                    <td>
                        <hr style="width:100%;" />
                        <div style="float:right; display:inline-block;">
                            <a href="#"><img src="icons/Facebook.png" /></a>&nbsp;
                            <a href="#"><img src="icons/Instagram.png" /></a>&nbsp;
                            <a href="#"><img src="icons/Linkedin.png" /></a>&nbsp;
                            <a href="#"><img src="icons/Youtube.png" /></a>&nbsp;
                            <a href="#"><img src="icons/Twitter.png" /></a>
                        </div>
                    </td>
                </tr>
            </table>
    </div>
    <div style="background-color:#2B2A29; color:white; padding:20px;">
        &copy;&nbsp;Copyright 2018 | Scheduleit Ltd.
    </div>
    </center>


    <!-- Modal -->
  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
    
      <!-- Modal content-->
      <div class="modal-content">

    <asp:UpdatePanel ID="panel1" runat="server">
    <ContentTemplate> 
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal">&times;</button>
          <h4 class="modal-title">Forgot password?</h4>
        </div>
        <div class="modal-body">
          <asp:TextBox runat="server" ID="txtphonenoforforgotpw" CssClass="txtforforgotpw" placeholder="Phone number" title="Please enter registered phone number" pattern= "[6-9]{1}[0-9]{9}" onchange="this.setCustomValidity(this.validity.patternMismatch ? this.title : '');" /><br /><br />
          <asp:TextBox runat="server" ID="txtotpforforgotpw" CssClass="txtforforgotpw" placeholder="OTP" /><br /><br />
          <asp:TextBox runat="server" ID="txtnewpw" CssClass="txtforforgotpw" TextMode="Password" placeholder="New Password" title="Password must contain at least 6 characters, including uppercase, lowercase and numbers" pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}" onchange="this.setCustomValidity(this.validity.patternMismatch ? this.title : ''); if(this.checkValidity()) form.txtreenterpw.pattern = RegExp.escape(this.value);" /><br /><br />
          <asp:TextBox runat="server" ID="txtconfirmpw" CssClass="txtforforgotpw" TextMode="Password" placeholder="Confirm Password" title="Please enter the same Password" pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}" onchange="this.setCustomValidity(this.validity.patternMismatch ? this.title : '');" />
        </div>
        <div class="modal-footer">
            <asp:button runat="server" ID="btnsubmit" Text="Submit" CssClass="btn btn-default" OnClick="btnsubmit_Click" />
            <asp:button runat="server" ID="btnverifyotp" Text="VERIFY OTP" CssClass="btn btn-default" OnClick="btnverifyotp_Click" />
            <asp:button runat="server" ID="btnchangepw" Text="Change Password" CssClass="btn btn-default" OnClick="btnchangepw_Click" />
        </div>
    </ContentTemplate>   
    <Triggers>
       <asp:AsyncPostBackTrigger ControlID="btnsubmit" EventName="Click" />
       <asp:AsyncPostBackTrigger ControlID="btnverifyotp" EventName="Click" />
       <asp:AsyncPostBackTrigger ControlID="btnchangepw" EventName="Click" />
    </Triggers>      
    </asp:UpdatePanel>
      </div>
      
    </div>
  </div>
    </form>
</body>
</html>