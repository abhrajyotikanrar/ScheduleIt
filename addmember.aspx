<%@ Page Title="Add group member" Language="C#" MasterPageFile="~/MainPage.master" AutoEventWireup="true" CodeFile="addMember.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="addGroupMember" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        .inputstyle{
            width:250px;
            padding: 15px;
            border-radius: 5px;
            margin:5px 0;
            font-family:'HoloLens MDL2 Assets';
            font-size:15px;
            border:1px solid #a1a3a3;
        }
        .btnaddmember{
            height:50px; 
            width:280px;
            background-color:#0B6F19;
            color:white;
            font-family:'Lucida Fax';
            font-size:15px;
            border-width:0;
            border-radius:5px;
            cursor:pointer;
        }
        .btnaddmember:hover{
            background-color:#07440F;
        }
        .statuslabel{
            padding:12px;
            background-color:#87DA89;
            color:#074009;
            width:97%;
            border-radius:5px;
        }
    </style>
    
    <div style="color: white; background-color:#1FA4A4; width:99.4%; height:104px; margin:-2.2%; border-radius:5px 5px 0 0; padding: 35px 2.5% 35px 2.5%;">
        <br /><br />Add group member for<br />
        <asp:Label runat="server" ID="lblgroupname" style="font-size:44pt;"/>
    </div><br /><br />
    <br /><br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <center>
        <asp:TextBox runat="server" ID="txtphno" placeholder="Phone number" CssClass="inputstyle" title="Please enter proper phone number" pattern= "[6-9]{1}[0-9]{9}" onchange="this.setCustomValidity(this.validity.patternMismatch ? this.title : '');"  onKeyDown="submitButton(event)" />
    </center>
    <br /><br />
    <center>
        <asp:Button runat="server" ID="btnaddmember" CssClass="btnaddmember" Text="ADD MEMBER" OnClick="btnaddmember_Click" />
    </center>
    <br /><br />
    <div runat="server" id="divstatus" class="statuslabel" visible="false">
        <asp:Label runat="server" ID="lblstatus" style="font-weight:300;"  Text="Hi all" />
    </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnaddmember" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

