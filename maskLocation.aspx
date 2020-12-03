<%@ Page Language="C#" AutoEventWireup="true" CodeFile="maskLocation.aspx.cs" Inherits="maskLocation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>

<meta charset="UTF-8">
<title>Mask Location</title>
<meta name="viewport" content="initial-scale=1.0">        
<meta name="format-detection" content="telephone=no">
<meta name="msapplication-tap-highlight" content="no">
<style>

   /* Set the size of the div element that contains the map */
  #map {
    height: 600px;  /* The height is 400 pixels */
    width: 100%;  /* The width is the width of the web page */
   }

</style>
<script src="./js/jquery-1.11.0.js"></script>
<script>
    $(function() {                
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition (function(pos) {                
                $("#txtLat").val(pos.coords.latitude);
                $("#txtLon").val(pos.coords.longitude);

            });
        } else {
            alert("이 브라우저에서는 Geolocation이 지원되지 않습니다.")
        }                        
    });
    
    function copyClip(v) {
        var clip = $("#vclip");        
        clip.val(v);
        clip.select();
        document.execCommand("Copy");
        alert("복사 되었습니다.");
    }
    
</script>
</head>

<body>    
    <form id="form1" runat="server">           
    <input id="vclip" style="display:none;" />    
    
    <div>       
        <table border="1">
        <tr>
            <td colspan="2"><font color="black">[100개이상 녹색]</font><br /><font color="black">[30~99개 노란색]</font><br /><font color="black">[2개~30개 빨간색]</font><br /><font color="black">[1개 이하 회색]</font><br /><br /><font color="#ff6699">입고시간 최신순으로 정렬</font></td>
        </tr>
        <tr>
            <td>위도</td>
            <td><asp:TextBox runat="server" ID="txtLat"></asp:TextBox></td>
        </tr>
        <tr>
            <td>경도</td>
            <td><asp:TextBox runat="server" ID="txtLon"></asp:TextBox></td>            
        </tr>
        <tr>
            <td>최대거리 (기본 500 미터)</td>
            <td><asp:TextBox runat="server" ID="txtDistance" Text="500"></asp:TextBox></td>            
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button runat="server" ID="btnAction" Text="조회" Width="100%" 
                    onclick="btnAction_Click" />
                <asp:Button runat="server" ID="btnReload" Text="새로고침" Width="100%" onclick="btnReload_Click" 
                     />                    
        </td>
        </tr>
        </table>                        
    </div>
    </form>    
</body>

</html>