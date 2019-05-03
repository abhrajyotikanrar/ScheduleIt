<%@ Page Title="Create new group" Language="C#" MasterPageFile="~/MainPage.master" AutoEventWireup="true" CodeFile="createGroup.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="createGroup" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
            height:30px; 
            width:280px;
            background-color:#0B6F19;
            color:white;
            font-family:'Lucida Fax';
            font-size:15px;
            border-width:0;
            border-radius:5px;
            cursor:pointer;
        }
        .btncreategroup{
            height:30px; 
            width:280px;
            background-color:#A14812;
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
        .btncreategroup:hover{
            background-color:#823B0F;
        }
        .divInsideRepeaterNoList{
            border-radius:8px;
            border-color:transparent;
            padding:6px 6px 6px 15px; 
            margin:2px; 
            font-size:18px; 
            font-family: Arial, Helvetica, sans-serif; 
            width:140px; 
            background-color:#CDD4CE; 
            display: inline-block;
            cursor: pointer;
        }
        .divInsideRepeaterNoList:hover{
            width:145px;
            border-color:#CDD4CE;
        }
    </style>
    <script>
        function submitButton(event) {
            if (event.which == 13) {
                $('#btnaddmember').trigger('click');
            }
        }
    </script>  
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <script type="text/javascript">
        function groupCreateSuccessAlert() {
            swal({
              title: "Success !",
              text: "You have created the group successfully.",
              icon: "success",
              button: "OK",
            });
        }
    </script>

    <div style="color: white; background-color:#1FA4A4; width:99.4%; height:104px; margin:-2.2%; border-radius:5px 5px 0 0; padding: 35px 2.5% 35px 2.5%; font-size:44pt;">
        <br />Create new group
    </div><br /><br />
    <center>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <table style="width:60%;">
            <tr>
                <td style="width:40%">
                    <h3>Group name:</h3>
                </td>
                <td style="width:60%">
                    <asp:TextBox runat="server" ID="txtgroupname" CssClass="inputstyle" title="Please enter the group name" />
                </td>
            </tr>
            <tr>
                <td style="width:40%">
                    <h3>Add member phone number:</h3>
                </td>
                <td style="width:60%">
                    <asp:TextBox runat="server" ID="txtphno" CssClass="inputstyle" title="Please enter proper phone number" pattern= "[6-9]{1}[0-9]{9}" onchange="this.setCustomValidity(this.validity.patternMismatch ? this.title : '');"  onKeyDown="submitButton(event)" /><br />
                </td>
            </tr>
            <tr>
                <td style="width:40%"></td>
                <td style="width:60%">
                    <asp:Button runat="server" ID="btnaddmember" CssClass="btnaddmember" Text="ADD MEMBER" OnClick="btnaddmember_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Repeater runat="server" ID="RepeaterMemberList" OnItemCommand="RepeaterMemberList_ItemCommand">
                        <ItemTemplate>
                            <div class="divInsideRepeaterNoList">
                                <asp:LinkButton runat="server" ID="btncross" style="float:right; background-color:transparent; border-width:0; text-decoration:none;" CommandName="cross">&times;</asp:LinkButton>
                                <%#Eval("newMemberPhoneNo") %>&nbsp;&nbsp;&nbsp;
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td style="width:40%"></td>
                <td style="width:60%">
                    <br /><br /><asp:Button runat="server" ID="btncreategroup" CssClass="btncreategroup" Text="CREATE GROUP" OnClick="btncreategroup_Click" />
                </td>
            </tr>
        </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnaddmember" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btncreategroup" EventName="Click" />
        </Triggers>
        </asp:UpdatePanel>
    </center>
</asp:Content>

