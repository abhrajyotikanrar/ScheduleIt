<%@ Page Title="Feedback forum" Language="C#" MasterPageFile="~/MainPage.master" AutoEventWireup="true" CodeFile="feedback.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="feedback" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style type="text/css">
        .feedback{
            background-color: #E2D6D3;
            padding: 15px; 
            border-radius: 3px;
            margin-bottom:10px;
            transition: all ease 1s;
            border:1px solid #a1a3a3;
        }
        .feedback:hover{
            box-shadow: 0 8px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 10px 0 rgba(0, 0, 0, 0.2);
        }
    </style>
    <div style="color: white; background-color:#1FA4A4; width:99.4%; height:104px; margin:-2.2%; border-radius:5px 5px 0 0; padding: 35px 2.5% 35px 2.5%; font-size:44pt;">
        <br />Feedback forum
    </div><br /><br />
    <asp:Repeater runat="server" ID="repeaterfeedbacklist">
        <ItemTemplate>
            <div class="feedback">
		        <table>
			        <tr>
				        <td style="font-size: 20pt; color: #235613;"><%#Eval("Name") %></td>
			        </tr>
			        <tr>
				        <td style="font-size: 10pt; color:blue;"><%#Eval("Date and Time") %></td>
			        </tr>
			        <tr>
				        <td style="font-size: 16pt; color: #8E720D;"><%#Eval("Title") %></td>
			        </tr>
			        <tr>
				        <td style="font-size: 12pt;"><%#Eval("Description") %></td>
			        </tr>
		        </table>
	        </div>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>

