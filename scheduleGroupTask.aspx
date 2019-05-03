<%@ Page Title="Schedule group task" Language="C#" MasterPageFile="~/MainPage.master" AutoEventWireup="true" CodeFile="scheduleGroupTask.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="schedulegrouptask" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
        .inputstyle:focus{
            outline:0;
        }
        .labelstyle{
            font-weight:bold;
        }
        .btnschedule{
            padding:2px; 
            height:50px; 
            width:200px;
            background-color:#4717F6;
            color:white;
            font-family:'Lucida Fax';
            font-size:20px;
            border-width:0;
            border-radius:7px;
            cursor:pointer;
            font-weight:bold;
        }
        .btnschedule:hover{
            background-color:#240B7B;
        }
    </style>
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <script type="text/javascript">
        function scheduleSuccessAlert() {
            swal({
              title: "Success !",
              text: "Your task has been scheduled successfully.",
              icon: "success",
              button: "OK",
            });
        }
    </script>

    <div style="color: white; background-color:#1FA4A4; width:99.4%; height:104px; margin:-2.2%; border-radius:5px 5px 0 0; padding: 35px 2.5% 35px 2.5%;">
        <br /><br />Schedule task for<br />
        <asp:Label runat="server" ID="lblgroupname" style="font-size:44pt;"/>
    </div>
    <br /><br />
    <center>
    <div>
        <table style="width:60%;">
            <tr>
                <td style="width:35%;">
                    <asp:Label runat="server" ID="lbltasktype" Text="Task type" CssClass="labelstyle"/>
                </td>
                <td style="width:65%;">
                    <asp:DropDownList runat="server" id="type" CssClass="inputstyle" style="width:282px;">
                        <asp:ListItem value="Birthday">Birthday</asp:ListItem>
                        <asp:ListItem value="Anniversary">Anniversary</asp:ListItem>
                        <asp:ListItem value="Meeting reminder">Meeting reminder</asp:ListItem>
                        <asp:ListItem value="Work reminder">Work reminder</asp:ListItem>
                        <asp:ListItem value="Flight reminder">Flight reminder</asp:ListItem>
                        <asp:ListItem value="Get together">Get together</asp:ListItem>
                        <asp:ListItem value="Party">Party</asp:ListItem>
                        <asp:ListItem value="Others">Others</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width:35%;">
                    <asp:Label runat="server" ID="lbltitle" Text="Title" CssClass="labelstyle"/>
                </td>
                <td style="width:65%;">
                    <asp:TextBox runat="server" ID="txttitle" CssClass="inputstyle"/>
                </td>
            </tr>
            <tr>
                <td style="width:35%; vertical-align:top;">
                    <br /><asp:Label runat="server" ID="lbldescription" Text="Description" CssClass="labelstyle"/>
                </td>
                <td style="width:65%;">
                    <asp:TextBox runat="server" ID="txtdescription" CssClass="inputstyle" TextMode = "MultiLine" style = "min-height:50px; min-width:250px; max-height:100px; max-width:400px; height:50px;"/>
                </td>
            </tr>
            <tr>
                <td style="width:35%;">
                    <br /><asp:Label runat="server" ID="lbldate" Text="Date" CssClass="labelstyle"/>
                </td>
                <td style="width:65%;">
                    <asp:TextBox runat="server" ID="txtdate" TextMode="Date" CssClass="inputstyle"/>
                </td>
            </tr>
            <tr>
                <td style="width:35%;">
                    <br /><asp:Label runat="server" ID="lbltime" Text="Time" CssClass="labelstyle"/>
                </td>
                <td style="width:65%;">
                    <asp:TextBox runat="server" ID="txttime" TextMode="Time" CssClass="inputstyle"/>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br /><br /><center><asp:Button runat="server" ID="btnschedule" CssClass="btnschedule" Text="SCHEDULE" OnClick="btnschedule_Click" /></center>
                </td>
            </tr>
        </table>
        
    </div>
    </center>
</asp:Content>

