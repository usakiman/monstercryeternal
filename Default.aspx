<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0, user-scalable=no, target-densitydpi=medium-dpi" />
<head runat="server">
    <title>MONSTER CRY Eternal - usaki fan page</title>
<script language="javascript" type="text/javascript">
<!--

    function viewImage(v) {            
        var hidv = document.getElementById("hidV");        
        hidv.value = v;                
        
        __doPostBack("btnChange", null);                        
    }

-->
</script>    
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" runat="server" id="hidV" />
    <div>
        <asp:Button runat="server" ID="btnChange" Text="상세" onclick="btnChange_Click" style="display:none;" />
        <asp:Panel runat="server" ID="panMain">
            <asp:DropDownList runat="server" ID="ddlPhotoType" AutoPostBack="true" 
                Width="20%" onselectedindexchanged="ddlPhotoType_SelectedIndexChanged">
            <asp:ListItem Text="--Total--" Value=""></asp:ListItem>
            <asp:ListItem Text="SSS+" Value="SSS+" Selected></asp:ListItem>
            <asp:ListItem Text="SSS" Value="SSS"></asp:ListItem>           
            <asp:ListItem Text="SS" Value="SS"></asp:ListItem>           
            </asp:DropDownList>            
            <asp:Button runat="server" id="btnImage" Text="PHOTO BOOK" onclick="btnImage_Click"/>&nbsp;
            <asp:Button runat="server" id="btnGuild" Text="MEDIPARM LOC" 
                onclick="btnGuild_Click"/>
            <br /><br />
            <asp:Label runat="server" ID="lblMsg"></asp:Label>
            <br /><br />
            <asp:DataList ID="DataList1" runat="server" BackColor="White" 
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                GridLines="Both" RepeatColumns="1" 
                onitemdatabound="DataList1_ItemDataBound" RepeatDirection="Horizontal">
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <AlternatingItemStyle BackColor="#DCDCDC" />
                <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
            <ItemTemplate>
            <table border="1" cellpadding="1" cellspacing="1" width="100%">            
            <tr>
                <td style="text-align:center; vertical-align:middle; width:30%;">
                    <asp:Image runat="server" ID="imgPhoto"/>
                </td>
                <td style="text-align:center; vertical-align:middle; width:70%;">
                    <asp:Label runat="server" ID="lblCardName" Font-Bold="true" Font-Size="Large"></asp:Label>
                    <br />
                    <asp:Label runat="server" ID="lblCardInfo"></asp:Label>                
                    <br />
                    <asp:Label runat="server" ID="lblCardMainInfo"></asp:Label>                
                </td>
            </tr>
            </table>        
            </ItemTemplate>
            </asp:DataList>
        </asp:Panel>
        
        <asp:Panel runat="server" ID="panView">        
        
        <table border="1" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width:350px; text-align:center; vertical-align:middle;">
                <asp:Label runat="server" ID="lblViewCardName"></asp:Label>
            </td>
             <td style="text-align:center; vertical-align:middle; width:30%;">
                <asp:Image runat="server" ID="imgViewPhoto" />
            </td>
        </tr>
        <tr>
            <td style="width:350px;" colspan="2" valign="top">
                <table border="0" width="100%">
                <tr>
                    <td colspan="3">[MAIN CARD EFFECT]</td>
                </tr>
                <tr>
                    <td colspan="3" align="right">
                        <asp:Label runat="server" ID="lblMainCardEffect"></asp:Label>
                        <asp:DropDownList runat="server" ID="ddlMainCard" AutoPostBack="true" 
                            onselectedindexchanged="ddlMainCard_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>                
                <tr>
                    <td colspan="3">[WEAPON]</td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlWeapon" 
                            onselectedindexchanged="ddlWeapon_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        <br />
                        <asp:Label runat="server" ID="lblWeapon"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlWeaponStone1" 
                            onselectedindexchanged="ddlWeaponStone1_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        <br />
                        <asp:Label runat="server" ID="lblWeaponStone1"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlWeaponStone2" 
                            onselectedindexchanged="ddlWeaponStone2_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        <br />
                        <asp:Label runat="server" ID="lblWeaponStone2"></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td colspan="3">[DEFENSE]</td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlDefense" 
                            AutoPostBack="true" 
                            onselectedindexchanged="ddlDefense_SelectedIndexChanged"></asp:DropDownList>
                        <br />
                        <asp:Label runat="server" ID="lblDefense"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlDefenseStone1" 
                            AutoPostBack="true" 
                            onselectedindexchanged="ddlDefenseStone1_SelectedIndexChanged"></asp:DropDownList>
                        <br />
                        <asp:Label runat="server" ID="lblDefenseStone1"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlDefenseStone2" 
                           AutoPostBack="true" 
                            onselectedindexchanged="ddlDefenseStone2_SelectedIndexChanged"></asp:DropDownList>
                        <br />
                        <asp:Label runat="server" ID="lblDefenseStone2"></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <td colspan="3">[ASSESSORIES]</td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlAss" 
                          AutoPostBack="true" onselectedindexchanged="ddlAss_SelectedIndexChanged"></asp:DropDownList>
                        <br />
                        <asp:Label runat="server" ID="lblAss"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlAssStone1" 
                           AutoPostBack="true" 
                            onselectedindexchanged="ddlAssStone1_SelectedIndexChanged"></asp:DropDownList>
                        <br />
                        <asp:Label runat="server" ID="lblAssStone1"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlAssStone2" 
                            AutoPostBack="true" 
                            onselectedindexchanged="ddlAssStone2_SelectedIndexChanged"></asp:DropDownList>
                        <br />
                        <asp:Label runat="server" ID="lblAssStone2"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>                
                <tr>
                    <td colspan="3">[장비 스킬] 1% 이하는 계산되지않음</td>
                </tr>
                <tr>
                    <td colspan="3">
                        공속
                        <asp:DropDownList runat="server" ID="ddlEquipmentAttSpeedInt" 
                            AutoPostBack="true" 
                            onselectedindexchanged="ddlEquipmentAttSpeedInt_SelectedIndexChanged"></asp:DropDownList>
                        .
                        <asp:DropDownList runat="server" ID="ddlEquipmentAttSpeedMino" 
                            AutoPostBack="true" 
                            onselectedindexchanged="ddlEquipmentAttSpeedMino_SelectedIndexChanged"></asp:DropDownList>%
                        &nbsp;&nbsp;&nbsp;&nbsp;
                        스속
                        <asp:DropDownList runat="server" ID="ddlEquipmentSkillSpeedInt" 
                            AutoPostBack="true" 
                            onselectedindexchanged="ddlEquipmentSkillSpeedInt_SelectedIndexChanged"></asp:DropDownList>   
                        .
                        <asp:DropDownList runat="server" ID="ddlEquipmentSkillSpeedMino" 
                            AutoPostBack="true" 
                            onselectedindexchanged="ddlEquipmentSkillSpeedMino_SelectedIndexChanged"></asp:DropDownList>%
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>                
                <tr>
                    <td colspan="3">[패시브] 1% 이하는 계산되지않음</td>
                </tr>             
                <tr>
                    <td colspan="3">
                    공속
                        <asp:DropDownList runat="server" ID="ddlPassiveAttSpeed" 
                            AutoPostBack="true" onselectedindexchanged="ddlPassiveAttSpeed_SelectedIndexChanged" 
                            ></asp:DropDownList>%&nbsp;&nbsp;&nbsp;&nbsp;                        
                    스속
                        <asp:DropDownList runat="server" ID="ddlPassiveSkillSpeed" 
                            AutoPostBack="true" onselectedindexchanged="ddlPassiveSkillSpeed_SelectedIndexChanged" 
                            ></asp:DropDownList>.
                        <asp:DropDownList runat="server" ID="ddlPassiveSkillSpeedMino" 
                            AutoPostBack="true" onselectedindexchanged="ddlPassiveSkillSpeedMino_SelectedIndexChanged" 
                            ></asp:DropDownList>%                        
                    </td>
                </tr>   
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>                
                <tr>
                    <td colspan="3">[EFFECT UP-DOWN]</td>
                </tr>
                <tr>
                    <td colspan="3">
                        전투중 행동력 UP or DOWN
                        <asp:DropDownList runat="server" ID="ddlActPowerMinus" 
                            AutoPostBack="true" 
                            onselectedindexchanged="ddlActPowerMinus_SelectedIndexChanged"></asp:DropDownList>                        
                        <br />
                        전투중 스속 UP or DOWN
                        <asp:DropDownList runat="server" ID="ddlSkillSpeedMinus" 
                            AutoPostBack="true" 
                            onselectedindexchanged="ddlSkillSpeedMinus_SelectedIndexChanged"></asp:DropDownList>   
                    </td>
                </tr>
                <tr>
                    <td colspan="3">[ETC]</td>
                </tr>
                <tr>
                    <td colspan="3">
                        스속에 따른 캐스팅 확인
                        <asp:DropDownList runat="server" ID="ddlEtcTime1" 
                            AutoPostBack="true" 
                            onselectedindexchanged="ddlEtcTime1_SelectedIndexChanged"></asp:DropDownList> ->
                        <asp:Label runat="server" ID="lblEtcTime1"></asp:Label>
                        <br />
                        스속에 따른 캐스팅 초기화 시간
                        <asp:DropDownList runat="server" ID="ddlEtcTime2" 
                            AutoPostBack="true" 
                            onselectedindexchanged="ddlEtcTime2_SelectedIndexChanged"></asp:DropDownList> ->
                        <asp:Label runat="server" ID="lblEtcTime2"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label runat="server" ID="lblResult"></asp:Label>
                    </td>
                </tr>
                
                </table>
            </td>
        </tr>
        </table>
        <br />
        <asp:Button runat="server" ID="btnGoMain" Text="MAIN" onclick="btnGoMain_Click" />
        </asp:Panel>
    </div>
    </form>
</body>
</html>
