<%@ Page Language="C#" AutoEventWireup="true" CodeFile="insert_data_card.aspx.cs" Inherits="insert_data_card" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>으으으으</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    카드이름<asp:TextBox runat="server" ID="txtCardName"></asp:TextBox><br />
    카드레벨<asp:TextBox runat="server" ID="txtCardLevel"></asp:TextBox><br />
    카드타입<asp:TextBox runat="server" ID="txtCardType"></asp:TextBox><br />
    카드종족<asp:TextBox runat="server" ID="txtCardRace"></asp:TextBox><br />
    행동력<asp:TextBox runat="server" ID="txtActPower"></asp:TextBox><br />
    액티브1<asp:TextBox runat="server" ID="txtActive1"></asp:TextBox><br />
    액티브1_재사용<asp:TextBox runat="server" ID="txtActive1_Waiting"></asp:TextBox><br />
    액티브2<asp:TextBox runat="server" ID="txtActive2"></asp:TextBox><br />
    액티브2_재사용<asp:TextBox runat="server" ID="txtActive2_Waiting"></asp:TextBox><br />
    패시브1<asp:TextBox runat="server" ID="txtPassive1"></asp:TextBox><br />
    패시브2<asp:TextBox runat="server" ID="txtPassive2"></asp:TextBox><br />
    대표카드효과<asp:TextBox runat="server" ID="txtMainEffect"></asp:TextBox><br />
    이미지<asp:TextBox runat="server" ID="txtCardImage"></asp:TextBox><br />
    
    패스워드<asp:TextBox runat="server" ID="txtPwd"></asp:TextBox><br />
    
    <asp:Button runat="server" ID="btnSave" Text="저장" onclick="btnSave_Click" />    
    <asp:Button runat="server" ID="btnUpdate" Text="업데이트" onclick="btnUpdate_Click" />
    <asp:Button runat="server" ID="btnDelete" Text="삭제" onclick="btnDelete_Click"/>
    
    <br /><br />
    
    날짜<asp:TextBox runat="server" ID="txtYmd"></asp:TextBox>
    <asp:Button runat="server" ID="btnSearch" Text="조회" onclick="btnSearch_Click" />  
    <br />  
    
    <asp:Label runat="server" ID="lblAccess_Log"></asp:Label><br />
    <asp:Label runat="server" ID="lblAccess_List"></asp:Label>        
    
    </div>
    </form>
</body>
</html>
