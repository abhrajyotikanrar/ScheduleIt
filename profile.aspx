<%@ Page Title="Profile" Language="C#" MasterPageFile="~/MainPage.master" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="profile" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        .editprofile:hover, .btnverify:hover{
            background-color:#3A863C;
        }
        .editprofile, .btnverify{
            color:white; 
            font-weight:bold; 
            border-radius:6px; 
            background-color:#4DB64F; 
            border:0; 
            cursor:pointer;
        }

        .modalDialogEditProfile {
	        position: fixed;
	        font-family: Arial, Helvetica, sans-serif;
	        top: 0;
	        right: 0;
	        bottom: 0;
	        left: 0;
	        background:rgba(0,0,0,0.8) ;
	        z-index: 99999;
	        opacity:0;
	        -webkit-transition: opacity 400ms ease-in;
	        -moz-transition: opacity 400ms ease-in;
	        transition: opacity 400ms ease-in;
	        pointer-events: none;
        }

        .modalDialogEditProfile:target {
	        opacity:1;
	        pointer-events: auto;
        }

        .modalDialogEditProfile > div {
	        width: 500px;
	        position: relative;
	        margin: 10% auto;
	        padding: 5px;
	        border-radius: 10px;
	        background-color: white;
        }

       .closeEditProfile{
           text-decoration:none;
           color:#DFD4D4;
           font-weight:bold;
           font-size:18px;
       }

       input:focus{
           outline:0;
       }

       .texteditinput{
           padding:5px;
           border-radius:5px;
           margin:3px;
           border:1px solid #a1a3a3;
           font-family:'HoloLens MDL2 Assets';
           font-size:15px;
       }

       .btnupdateprofile{
           padding:5px; 
           height:40px; 
           width:100px; 
           border-radius:6px; 
           border:1px solid #a1a3a3; 
           font-weight:bold; 
           cursor:pointer; 
           background-color:white;
           float:right;
       }

        .btnupdateprofile:hover {
            background-color: #E0D2CE;
        }
    </style>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <script type="text/javascript">
        function profileupdateSuccessAlert(){
            swal({
              title: "Success !",
              text: "Your profile has been updated successfully.",
              icon: "success",
              button: "OK",
            });
        }
    </script>
    

    <div>
        <asp:Label runat="server" ID="lblname" style="padding:10px 0; font-size:40px;" />&nbsp;<img src="icons/checked-user.png" height="26" /><br />
        <div style="float:right">
            <a href="#openEditProfile" style="text-decoration:none;"><div ID="editprofile" style="padding:8px;" class="editprofile">EDIT PROFILE</div></a>

            <div id="openEditProfile" class="modalDialogEditProfile">
	        <div>
                <div style="width:475px; padding:12px; border-radius:10px; background-color:#8A4029;">
                    <div style="float:right;"><a href="#closeEditProfile" title="Close" class="closeEditProfile">&times;</a></div>
                    <div style="font-size:17px; font-weight:bolder; color:white;">EDIT PROFILE</div>
                </div>
                <div style="width:475px; padding:12px; border-radius:10px;">
                    <table style="width:100%;">
                        <tr>
                            <td style="width:20%;">
                                <strong>Name:</strong>
                            </td>
                            <td style="width:80%;">
                                <center>
                                <asp:TextBox runat="server" ID="txtfname" CssClass="texteditinput" style="width:27%;" required="required" />
                                <asp:TextBox runat="server" ID="txtmname" CssClass="texteditinput" style="width:27%;" />
                                <asp:TextBox runat="server" ID="txtlname" CssClass="texteditinput" style="width:27%;" />
                                </center>
                            </td>
                        </tr>
                        <tr>
                            <td style="width:20%;">
                                <strong>Email:</strong>
                            </td>
                            <td style="width:80%;">
                                <center><asp:TextBox runat="server" ID="txtemail" CssClass="texteditinput" style="width:93%;" title="Please enter proper e-mail address" pattern="[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$" onchange="this.setCustomValidity(this.validity.patternMismatch ? this.title : '');" /></center>
                            </td>
                        </tr>
                    </table>
                    <br /><hr />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Button runat="server" ID="btnupdateprofile" CssClass="btnupdateprofile" Text="UPDATE" OnClick="btnupdateprofile_Click"/>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnupdateprofile" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <br /><br />
                </div>
	        </div>
            </div>
        </div>
        <asp:Label runat="server" ID="lbllogondatetime" style="padding:10px 0; font-size:15px; color:#5759F7; font-weight:bold;" /><br /><br />
        
        <hr/>

        <asp:Label runat="server" ID="lblemaillabel" style="color:#734820; padding-bottom:3px; font-size:15px; font-weight:bold;" Text="Email" /><br />
        <asp:label runat="server" ID="lblemail" style="padding-top:3px; font-size:25px; font-weight:bold;" />&nbsp;
        <asp:Image runat="server" ID="imgnotverified" src="icons/not-verified.png" height="20" /><asp:Image runat="server" ID="imgverified" src="icons/verified.png" height="20" />&nbsp;
        <asp:Button runat="server" ID="btnverify" style="padding:4px; margin:4px; width:60px;" CssClass="btnverify" Text="✓ Verify" OnClick="btnverify_Click" /><br /><br />

        <asp:Label runat="server" ID="lblphnolabel" style="color:#734820; font-size:15px; font-weight:bold;" Text="Phone No." /><br />
        <asp:label runat="server" ID="lblphno" style="padding-top:3px; font-size:25px; font-weight:bold;" />&nbsp;
        <img src="icons/verified.png" height="20" /><br /><br />

        <asp:Label runat="server" ID="lbldoblabel" style="color:#734820; font-size:15px; font-weight:bold;" Text="Date of Birth" /><br />
        <asp:label runat="server" ID="lbldob" style="padding-top:3px; font-size:25px; font-weight:bold;" /><br /><br />

        <asp:Label runat="server" ID="lblgenderlabel" style="color:#734820; font-size:15px; font-weight:bold;" Text="Gender" /><br />
        <asp:label runat="server" ID="lblgender" style="padding-top:3px; font-size:25px; font-weight:bold;" /><br /><br />

    </div>
</asp:Content>