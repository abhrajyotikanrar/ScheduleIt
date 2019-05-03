<%@ Page Language="C#" AutoEventWireup="true" CodeFile="registration.aspx.cs" Inherits="registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register at scheduleit</title>
    <link rel="shortcut icon" href="/icons/icon.png" />
<style type="text/css">
    body{
        margin: 0%;
    }
    .logo{
	    width: 320px;
	    padding: 10px;
	    margin: 0%;
    }
    .registrationbox{
        width:100%;
        background: #333;
        border-radius: 8px;
    }
    .textstyle{
        padding: 15px;
        border-radius: 10px;
        width: 150px;
        margin: 5px;
        font-family:'HoloLens MDL2 Assets';
        font-size:20px;
        border:1px solid #a1a3a3;
    }
    input:focus{
        border-color:orange;
        outline:none;
        box-shadow: 0 0 4px 2px rgba(128, 0, 128, 0.4);
    }
    select:focus{
        border-color:orange;
        outline:none;
        box-shadow: 0 0 4px 2px rgba(128, 0, 128, 0.4);
    }
    #btnregister{
        width:250px; 
        cursor:pointer; 
        padding:15px; 
        font-family:'Bookman Old Style'; 
        background-color:#2EA4A0; 
        border-radius:10px; 
        border-width:0; 
        font-size: 18px; 
        font-weight:bold; 
        box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24), 0 17px 50px 0 rgba(0,0,0,0.19); 
        position:relative; 
        top:-90px;
    }
    #btnregister:hover{
        background-color:#217673;
    }
    .optionInRightCornerContainer{
        float:right;
        display:inline-block;
    }
    .optionInRightCorner{
        height:20px;
        padding:15px;
        width:100px;
        color:white;
        font-weight:bold;
        text-decoration:none;
    }
    .optionInRightCorner:hover{
        background-color:black;
        opacity:0.5;
    }
    .footer{
        height: 250px;
        width:100%;
        background-color:#19696F;
    }
</style>
</head>
<body>
    <div style="background-image:url(images/registration-background.jpg); background-size: cover; background-attachment:fixed;">
        <a href="index.html"><img src="images/logo.png" class="logo"/></a>
        <div class="optionInRightCornerContainer">
            <br /><br />
            <a href="index.html" class="optionInRightCorner">HOME</a>
            <a href="login.aspx" class="optionInRightCorner">LOG IN</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </div>

    <form id="frmregistration" runat="server" onsubmit="return checkForm(this);">
    <br />
    <table style="width:100%">
        <tr>
            <td style="width:20%"></td>
            <td style="width:60%">
                <div class="registrationbox" style="opacity: 0.9;">
                   <center><br />
                    <h1 style="font-family:'Bookman Old Style'; color:olive; font-weight:bold; opacity:1.0;">Create An Account</h1>
                    <asp:TextBox runat="server" placeholder="First Name" ID="txtfname" CssClass="textstyle" title="Please enter your first name" required="required" />
                    <asp:TextBox runat="server" placeholder="Middle Name" ID="txtmname" CssClass="textstyle" title="Please enter your middle name" />
                    <asp:TextBox runat="server" placeholder="Last Name" ID="txtlname" CssClass="textstyle" title="Please enter your last name" /><br />
                    <asp:TextBox runat="server" style="width:250px;" type="email" placeholder="E-mail" ID="txtmail" CssClass="textstyle" required="required" title="Please enter proper e-mail address" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" onchange="this.setCustomValidity(this.validity.patternMismatch ? this.title : '');" />
                    <asp:TextBox runat="server" style="width:250px;" placeholder="Phone Number" ID="txtphno" CssClass="textstyle" required="required" title="Please enter your phone number" pattern= "[6-9]{1}[0-9]{9}" onchange="this.setCustomValidity(this.validity.patternMismatch ? this.title : '');" /><br />
                    <asp:TextBox runat="server" style="width:250px; font-size:18px;" placeholder="Date of Birth (DD/MM/YYYY)" ID="txtdob" CssClass="textstyle" required="required" title="Please enter your Date of Birth in DD/MM/YYYY format only" />
                    <asp:DropDownList runat="server" id="gender" style="width:280px; height:47px; padding-top:8px; padding-bottom:8px;" class="textstyle">
                        <asp:ListItem value="Male">Male</asp:ListItem>
                        <asp:ListItem value="Female">Female</asp:ListItem>
                    </asp:DropDownList><br />
                    <asp:TextBox runat="server" type="password" style="width:250px;" placeholder="Password" name="txtpassword" ID="txtpassword" CssClass="textstyle" title="Password must contain at least 6 characters, including uppercase, lowercase and numbers" required="required" pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}" onchange="this.setCustomValidity(this.validity.patternMismatch ? this.title : ''); if(this.checkValidity()) form.txtreenterpw.pattern = RegExp.escape(this.value);" />

                    <asp:TextBox runat="server" type="password" style="width:250px;" placeholder="Re-enter Password" name="txtreenterpw" ID="txtreenterpw" CssClass="textstyle" title="Please enter the same Password" required="required" pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}" onchange="this.setCustomValidity(this.validity.patternMismatch ? this.title : '');" /><br />

                    <br />
                    <div style="margin-left:13.5%; float:left;"><asp:CheckBox runat="server" ID="chkagree" Checked="true" style="color:darkkhaki;" Text="I agree to the terms and conditions." /></div>
                    <br /><br /><br /><br /><br /><br /><br />
                    </center>
                </div>
                <center><asp:Button runat="server" type="submit" ID="btnregister" Text="Sign Up" OnClick="btnregister_Click"/></center>

            </td>
            <td style="width:20%"></td>
        </tr>
    </table>
    </form>
    <br />
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
</body>
</html>
