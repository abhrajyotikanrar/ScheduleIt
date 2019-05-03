<%@ Page Title="Group task history" Language="C#" MasterPageFile="~/MainPage.master" AutoEventWireup="true" CodeFile="taskHistory.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="tastHistory" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        .btnschedulenewtask{
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
        .btnschedulenewtask:hover{
            background-color:#105725;
        }
    </style>

    <div style="color: white; background-color:#1FA4A4; width:99.4%; height:104px; margin:-2.2%; border-radius:5px 5px 0 0; padding: 35px 2.5% 35px 2.5%;">
        <br /><br />Task scheduled in<br />
        <asp:Label runat="server" ID="lblgroupname" style="font-size:44pt;"/>
    </div><br /><br />
    <a href="scheduleGroupTask.aspx" style="text-decoration:none;"><div class="btnschedulenewtask">Schedule new task</div></a>
    <br />
    <asp:Repeater runat="server" ID="repeaterTaskList">
        <ItemTemplate>
            <div style="border:2px solid #a1a3a3; padding:15px; background-color:white; border-radius:6px;">
                <table style="width: 100%; border: 0px; border-spacing: 0px;">
                    <tr>
                        <td style="width: 70%;">
                            <strong>Task ID:</strong>&nbsp;GT<%#Eval("TaskId") %></td>
                        <td style="width: 30%;">
                            <div style="float:right;">&nbsp;<strong><%#Eval("Date") %>&nbsp;<%#Eval("Time") %></strong></div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <strong>Task type:</strong>&nbsp;<%#Eval("TaskType") %>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <strong><%#Eval("Title") %></strong>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <%#Eval("Description") %>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 70%;">
                            <strong>Task status:</strong>&nbsp;<%#Eval("Status") %></td>
                        <td style="width: 30%;">
                            <div style="float:right;"><strong>Scheduled by:</strong>&nbsp;<%#Eval("TaskGeneratorName") %></div>
                        </td>
                    </tr>
                </table>
            </div>
        </ItemTemplate>
        <SeparatorTemplate><br /></SeparatorTemplate>
    </asp:Repeater>
</asp:Content>