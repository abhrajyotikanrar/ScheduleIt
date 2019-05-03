<%@ Page Title="Group list" Language="C#" MasterPageFile="~/MainPage.master" AutoEventWireup="true" CodeFile="groups.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="grouplist" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        .btninrepeater{
            float:left;
            outline:0; 
            border:0; 
            cursor:pointer;
            background-color:#1862AB;
            color:white;
            padding:7px 0;
            width:20%;
            text-align:center;
            text-decoration:none;
            margin:0;
            font-family:'Segoe UI Symbol';
        }
        .btncreategroup{
            outline:0; 
            border:0; 
            cursor:pointer;
            background-color:#188337;
            color:white;
            width:100%;
            padding:12px 0;
            font-size:20px;
            font-family:'Times New Roman', Times, serif;
            text-align:center;
        }
        .btncreategroup:hover{
            background-color:#105725;
        }
        .btninrepeater:hover{
            background-color:#134A80;
        }
    </style>
    <div style="color: white; background-color:#1FA4A4; width:99.4%; height:104px; margin:-2.2%; border-radius:5px 5px 0 0; padding: 35px 2.5% 35px 2.5%; font-size:44pt;">
        <br />Groups you are in
    </div><br /><br />
    <a href="createGroup.aspx" style="text-decoration:none;"><div class="btncreategroup">Create new group</div></a>
    <br />
    <asp:Repeater ID="RepeaterGroupList" runat="server" OnItemCommand="RepeaterGroupList_ItemCommand">
        <ItemTemplate>
            <div style="border-radius:6px 6px 0 0; height:15px; background-color:#394F43"></div>
            <div style="background-color:#C9CDD0; border-width:1px 0; margin:0; padding:5px;">
                <table style="width:100%">
                    <tr>
                        <td style="width:60%;">
                            <h2><asp:Label runat="server" ID="lblgroupname" Text='<%#Eval("[Group name]")%>' /></h2><br />
                            <b>Group ID:</b>&nbsp;#<asp:Label runat="server" ID="lblgroupid" Text='<%#Eval("[Group id]")%>' /><br />
                            <b>Creator:</b>&nbsp;<%#Eval("[Creator name]")%><br />
                        </td>
                        <td style="width:40%;">
                            <div style="float:right;"><b>No of members:</b>&nbsp;<%#Eval("[No of members]")%></div><br />
                            <div style="float:right;"><b>Creating Date:</b>&nbsp;<%#Eval("[Creating date]")%></div><br />
                            <br /><br />
                        </td>
                    </tr>
                </table>
            </div>
            <div style="margin-bottom:50px;">
                <asp:LinkButton runat="server" ID="btnschedule" CssClass="btninrepeater" Text="SCHEDULE TASK" CommandName="scheduleTask" style="border-radius:0 0 0 6px" />
                <asp:LinkButton runat="server" ID="btnviewdetails" CssClass="btninrepeater" Text="VIEW DETAILS" CommandName="viewDetails" />
                <asp:LinkButton runat="server" ID="btnaddmembers" CssClass="btninrepeater" Text="ADD MEMBER" CommandName="addMember" />
                <asp:LinkButton runat="server" ID="btntaskhistory" CssClass="btninrepeater" Text="TASK HISTORY" CommandName="taskHistory" />
                <asp:LinkButton runat="server" ID="btndeletegroup" CssClass="btninrepeater" Text="DELETE GROUP" CommandName="deleteGroup" OnClientClick="return confirm('Are you sure you want to delete this group?');" style="border-radius:0 0 6px 0" />
            </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>