<%@ Page Title="Contacts" Language="C#" MasterPageFile="~/MainPage.master" AutoEventWireup="true" CodeFile="contact.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="contact" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <style>
       #map {
        height: 250px;
        width: 100%;
       }
    </style>
    <script>
      function initMap() {
          var uluru = { lat: 13.0683873, lng: 77.50784699999997 };
        var map = new google.maps.Map(document.getElementById('map'), {
          zoom: 16,
          center: uluru
        });
        var marker = new google.maps.Marker({
          position: uluru,
          map: map
        });
      }
    </script>
    <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCrMsPskfvNL**********************&callback=initMap">
    </script>

    <div style="color: white; background-color:#1FA4A4; width:99.4%; height:104px; margin:-2.2%; border-radius:5px 5px 0 0; padding: 35px 2.5% 35px 2.5%; font-size:44pt;">
        <br />Contact us
    </div><br /><br />
    <table style="width:96%;">
        <tr>
            <td style="width:40%; font-family:'Palatino Linotype';">
                <div style="padding-left: 60px;">
                    <h2>Scheduleit Ltd.</h2>
                    #31/B, 13<sup>th</sup> cross,<br />
                    Hanumanthe Gowder Main Road,<br />
                    Near Sapthagiri Engineering College,<br />
                    Bengaluru - 560073<br />
                    <img src="icons/phone.png" height="15" /> +91 9933994410<br />
                    <img src="icons/envelope.png" height="15" /> abhrajyotikanrar1996@gmail.com
                </div>
            </td>

            <td style="width:60%;">
                <br />
                <div id="map"></div>
            </td>
        </tr>
    </table>
    <br />
</asp:Content>

