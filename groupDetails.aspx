<%@ Page Title="Group details" Language="C#" MasterPageFile="~/MainPage.master" AutoEventWireup="true" CodeFile="groupDetails.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="groupDetails" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        .btnEditSave{
            float:right;
            border:none;
            outline:none;
            background-color:#3A863C;
            padding:7px 12px;
            color:white;
            font-weight:bold;
            cursor:pointer;
            border-radius:6px; 
            margin-top:35px;
        }
        .btnEditSave:hover{
            background-color:#275A29;
        }
        .divdetails{
            border:1px solid #a1a3a3;
            width:70%;
            padding:10px;
            border-radius:6px;
        }
        .btnaddmember{
            float:right; 
            font-family:'MS Reference Sans Serif'; 
            font-size:14pt; 
            outline:none;
            border:none;
            background-color:transparent;
            color:#A56415;
            cursor:pointer;
            margin-top:7px;
            font-weight:bolder;
        }
        .btnsearchmember{
            font-family:'HoloLens MDL2 Assets'; 
            font-size:25px; 
            padding:6.5px; 
            outline:none;
            border-width:2px;
            border-color:black;
            border-radius:6px;
            width:30%;
            cursor:pointer;
            float:right;
            background-color:white;
        }
        .btnsearchmember:hover{
            background-color:black;
            color:white;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div style="color: white; background-color:#1FA4A4; width:99.4%; height:104px; margin:-2.2%; border-radius:5px 5px 0 0; padding: 35px 2.5% 35px 2.5%;">
        <br /><br /><br />
        <asp:Button runat="server" ID="btnedit" Text="Edit" CssClass="btnEditSave" OnClick="btnedit_Click" />
        <asp:Button runat="server" ID="btnsave" Text="Save" Visible="false" CssClass="btnEditSave" OnClick="btnsave_Click" />
        <asp:TextBox runat="server" ID="txtgroupname" Enabled="false" style="color: white; background-color:transparent; border:none; outline:none; padding:10px; font-size:35pt;" />
    </div><br /><br />
    <table style="width:100%;">
        <tr>
            <td style="width: 50%; vertical-align:top;">
                <br /><br />
                <asp:Button runat="server" ID="btnsearchmember" Text="Search" CssClass="btnsearchmember" OnClick="btnsearchmember_Click" />
                <asp:TextBox runat="server" ID="txtsearchmember" placeholder="Search member" style="font-family:'HoloLens MDL2 Assets'; font-size:15px; padding:15px; outline:0; border:1px solid #a1a3a3; border-radius:6px; width:60%;" />
                <br /><br />
                <div style="padding:15px; background-color:#B7C114; font-family:'MS Reference Sans Serif'; font-size:20pt; font-weight:bolder; color:#4A3B58; border-radius:4px;">
                    <asp:Button runat="server" ID="btnaddmember" Text="+" CssClass="btnaddmember" OnClick="btnaddmember_Click" />
                    Members
                </div><br />
                <asp:Repeater runat="server" ID="repeaterMember" OnItemCommand="repeaterMember_ItemCommand">
                    <ItemTemplate>
                        <table style="width:100%;">
                            <tr>
                                <td style="width:10%;">
                                    <div style="padding:15px; background-color:#7FBF4C; height:25px; width:25px; color:#4B6F2E; font-weight:bold; font-size:18pt; border-radius:100px;"><center><%#Eval("name_initial")%></center></div>
                                </td>
                                <td style="width:80%;">
                                    <div style="padding:15px; font-size:20px;">
                                        <b><font style="color:#7D4E39;"> <%#Eval("name")%> </font></b><br />
                                        <asp:Label runat="server" ID="lblmemberno" Text='<%#Eval("phonoNo")%>' />
                                    </div>
                                </td>
                                <td style="width:10%;">
                                    <asp:LinkButton runat="server" ID="btnremovemember" CommandName="removeMember" style="text-decoration:none; font-weight:bold; font-size:20pt;">&times;</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <SeparatorTemplate><hr style="width:85%;" /></SeparatorTemplate>
                </asp:Repeater>
                <br /><div runat="server" ID="lblmatchfound" style="padding:15px; background-color:#FFB8AE; color:#904F46; border-radius:6px;">No matches found</div>
            </td>
            <td style="width: 50%; vertical-align:top;">
                <br /><br />
                <center>
                <div class="divdetails">
                    <table style="width:100%;">
                        <tr>
                            <td style="width:50%">
                                <h4 style="color:#156814;">Group ID:</h4>
                            </td>
                            <td style="width:50%">
                                #<asp:Label runat="server" ID="lblid" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width:50%">
                                <h4 style="color:#156814;">Group name:</h4>
                            </td>
                            <td style="width:50%">
                                <asp:Label runat="server" ID="lblname" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width:50%">
                                <h4 style="color:#156814;">No. of members:</h4>
                            </td>
                            <td style="width:50%">
                                <asp:Label runat="server" ID="lblnoofmembers" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width:50%">
                                <h4 style="color:#156814;">Creator name:</h4>
                            </td>
                            <td style="width:50%">
                                <asp:Label runat="server" ID="lblcreatorname" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width:50%">
                                <h4 style="color:#156814;">Creator number:</h4>
                            </td>
                            <td style="width:50%">
                                <asp:Label runat="server" ID="lblcreatorno" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width:50%">
                                <h4 style="color:#156814;">Date &amp; time:</h4>
                            </td>
                            <td style="width:50%">
                                <asp:Label runat="server" ID="lbldatetime" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width:50%">
                                <h4 style="color:#156814;">Status:</h4>
                            </td>
                            <td style="width:50%">
                                <asp:Label runat="server" ID="lblstatus" />
                            </td>
                        </tr>
                    </table>
                </div>
                </center>
            </td>
        </tr>
    </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnedit" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnsave" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnsearchmember" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

