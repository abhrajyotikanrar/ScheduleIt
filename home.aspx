<%@ Page Title="Home" Language="C#" MasterPageFile="~/MainPage.master" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="TaskList" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p style="padding:10px; border-radius:6px; background-color:#388E3C; color:white; font-size:25px;">Your scheduled tasks</p>
    <asp:Repeater ID="RepeaterIndividualTasks" runat="server" OnItemCommand="RepeaterIndividualTasks_ItemCommand">
        <ItemTemplate>
            <div style="border-radius: 6px; background-color: white; border: 2px solid #388E3C; margin-top:10px; padding:10px;">
                <table style="width: 100%; border: 0px; border-spacing: 0px;">
                    <tr>
                        <td style="width: 70%;">
                            <strong>Task ID:</strong>&nbsp;IT<%#Eval("[Task id]") %></td>
                        <td style="width: 30%;">
                            <div style="float:right;">&nbsp;<strong><%#Eval("Date") %>&nbsp;<%#Eval("Time") %></strong></div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                               <strong>Task type:</strong>&nbsp;<%#Eval("[Task type]") %>
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
                            <strong>Task status:</strong>&nbsp;<%#Eval("[Task status]") %></td>
                        <td style="width: 30%;">
                            <div style="float:right;"><strong>Notification status:</strong>&nbsp;<%#Eval("[Notification status]") %></div>
                        </td>
                    </tr>
                </table>
            </div>
        </ItemTemplate>
    </asp:Repeater>

    <p style="padding:10px; border-radius:6px; background-color:#455A64; color:white; font-size:25px;">Your scheduled group tasks</p>
    <asp:Repeater ID="RepeaterGroupTasks" runat="server">
        <ItemTemplate>
            <div style="border-radius: 6px; background-color: white; border: 2px solid #455A64; margin-top:10px; padding:10px;">
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
                            <strong>Group:</strong>&nbsp;#<%#Eval("GroupId") %>&nbsp;(<%#Eval("GroupName") %>)</td>
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
    </asp:Repeater>
</asp:Content>

