using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Text;
using System.Collections;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.lblMsg.Text = "[몬스터 주식회사] 몬스터 크라이 이터널 공속, 스속 관련";

            this.panMain.Visible = true;
            this.panView.Visible = false;

            Module m = new Module();
            m.mergeAccess_Log();
            m.insertAccess_List(Request.UserHostAddress);

            Display();
        }
    }

    private void Display()
    {
        #region main 카드 셋팅
        DataTable dt = new DataTable();
        DataTable dtMain = new DataTable();
        CardInfo ci = new CardInfo();
        
        dt = ci.getCardInfo("", this.ddlPhotoType.SelectedValue, "", "");
        dtMain = ci.getCardInfo("", "", "", "YES");

        this.DataList1.DataSource = dt;
        this.DataList1.DataBind();
        #endregion

        #region 대표카드 셋팅
        this.ddlMainCard.Items.Clear();
        this.ddlMainCard.Items.Add(new ListItem("-선택-", ""));
        for (int i = 0; i < dtMain.Rows.Count; i++)
        {
            this.ddlMainCard.Items.Add(new ListItem(dtMain.Rows[i]["cardname"].ToString(), dtMain.Rows[i]["maineffect"].ToString()));
        }
        this.ddlMainCard.DataBind();

        #endregion

        #region 무기 셋팅
        DataTable dtW = new DataTable();
        dtW = ci.getWeaponList("");

        this.ddlWeapon.Items.Clear();
        this.ddlWeapon.Items.Add(new ListItem("-선택-", ""));
        for (int i = 0; i < dtW.Rows.Count; i++)
        {
            this.ddlWeapon.Items.Add(new ListItem(dtW.Rows[i][0].ToString() + dtW.Rows[i][1].ToString(), dtW.Rows[i][0].ToString() + dtW.Rows[i][1].ToString()));
        }
        this.ddlWeapon.DataBind();
        #endregion

        #region 방어구 셋팅
        DataTable dtD = new DataTable();
        dtD = ci.getDefenseList("");

        this.ddlDefense.Items.Clear();
        this.ddlDefense.Items.Add(new ListItem("-선택-", ""));
        for (int i = 0; i < dtD.Rows.Count; i++)
        {
            this.ddlDefense.Items.Add(new ListItem(dtD.Rows[i][0].ToString() + dtD.Rows[i][1].ToString(), dtD.Rows[i][0].ToString() + dtD.Rows[i][1].ToString()));
        }
        this.ddlDefense.DataBind();
        #endregion

        #region 장신구 셋팅
        DataTable dtA = new DataTable();
        dtA = ci.getAssList("");

        this.ddlAss.Items.Clear();
        this.ddlAss.Items.Add(new ListItem("-선택-", ""));
        for (int i = 0; i < dtA.Rows.Count; i++)
        {
            this.ddlAss.Items.Add(new ListItem(dtA.Rows[i][0].ToString() + dtA.Rows[i][1].ToString(), dtA.Rows[i][0].ToString() + dtA.Rows[i][1].ToString()));
        }
        this.ddlAss.DataBind();
        #endregion        

        #region 효과증가감소 및 기타 셋팅
        this.ddlActPowerMinus.Items.Clear();        
        this.ddlActPowerMinus.Items.Add(new ListItem("10%", "0.1"));
        this.ddlActPowerMinus.Items.Add(new ListItem("15%", "0.15"));
        this.ddlActPowerMinus.Items.Add(new ListItem("20%", "0.2"));
        this.ddlActPowerMinus.Items.Add(new ListItem("25%", "0.25"));
        this.ddlActPowerMinus.Items.Add(new ListItem("30%", "0.3"));
        this.ddlActPowerMinus.Items.Add(new ListItem("35%", "0.35"));
        this.ddlActPowerMinus.Items.Add(new ListItem("40%", "0.4"));
        this.ddlActPowerMinus.Items.Add(new ListItem("45%", "0.45"));
        this.ddlActPowerMinus.Items.Add(new ListItem("50%", "0.5"));
        this.ddlActPowerMinus.Items.Add(new ListItem("55%", "0.55"));
        this.ddlActPowerMinus.Items.Add(new ListItem("60%", "0.6"));
        this.ddlActPowerMinus.Items.Add(new ListItem("100%", "1.0"));
        this.ddlActPowerMinus.Items.Add(new ListItem("0%", ""));
        this.ddlActPowerMinus.Items.Add(new ListItem("-10%", "-0.1"));
        this.ddlActPowerMinus.Items.Add(new ListItem("-15%", "-0.15"));
        this.ddlActPowerMinus.Items.Add(new ListItem("-20%", "-0.2"));
        this.ddlActPowerMinus.Items.Add(new ListItem("-25%", "-0.25"));
        this.ddlActPowerMinus.Items.Add(new ListItem("-30%", "-0.3"));
        this.ddlActPowerMinus.Items.Add(new ListItem("-50%", "-0.5"));
        this.ddlActPowerMinus.Items.Add(new ListItem("-100%", "-1.0"));
        this.ddlActPowerMinus.DataBind();
        this.ddlActPowerMinus.SelectedValue = "";

        this.ddlSkillSpeedMinus.Items.Clear();        
        this.ddlSkillSpeedMinus.Items.Add(new ListItem("10%", "10"));
        this.ddlSkillSpeedMinus.Items.Add(new ListItem("20%", "20"));
        this.ddlSkillSpeedMinus.Items.Add(new ListItem("30%", "30"));
        this.ddlSkillSpeedMinus.Items.Add(new ListItem("40%", "40"));
        this.ddlSkillSpeedMinus.Items.Add(new ListItem("50%", "50"));
        this.ddlSkillSpeedMinus.Items.Add(new ListItem("0%", "0"));
        this.ddlSkillSpeedMinus.Items.Add(new ListItem("-10%", "-10"));
        this.ddlSkillSpeedMinus.Items.Add(new ListItem("-20%", "-20"));
        this.ddlSkillSpeedMinus.Items.Add(new ListItem("-30%", "-30"));
        this.ddlSkillSpeedMinus.Items.Add(new ListItem("-40%", "-40"));
        this.ddlSkillSpeedMinus.Items.Add(new ListItem("-50%", "-50"));
        this.ddlSkillSpeedMinus.DataBind();
        this.ddlSkillSpeedMinus.SelectedValue = "0";        

        this.ddlEtcTime1.Items.Clear();
        this.ddlEtcTime1.Items.Add(new ListItem("1초", "1"));
        this.ddlEtcTime1.Items.Add(new ListItem("2초", "2"));
        this.ddlEtcTime1.Items.Add(new ListItem("3초", "3"));
        this.ddlEtcTime1.DataBind();

        this.ddlEtcTime2.Items.Clear();
        this.ddlEtcTime2.Items.Add(new ListItem("1초", "1"));
        this.ddlEtcTime2.Items.Add(new ListItem("2초", "2"));
        this.ddlEtcTime2.Items.Add(new ListItem("3초", "3"));
        this.ddlEtcTime2.Items.Add(new ListItem("4초", "4"));
        this.ddlEtcTime2.Items.Add(new ListItem("5초", "5"));
        this.ddlEtcTime2.DataBind();

        this.ddlPassiveAttSpeed.Items.Clear();
        this.ddlPassiveAttSpeed.Items.Add(new ListItem("0%", "0"));
        this.ddlPassiveAttSpeed.Items.Add(new ListItem("5%", "5"));
        this.ddlPassiveAttSpeed.Items.Add(new ListItem("10%", "10"));
        this.ddlPassiveAttSpeed.Items.Add(new ListItem("15%", "15"));
        this.ddlPassiveAttSpeed.Items.Add(new ListItem("20%", "20"));
        this.ddlPassiveAttSpeed.Items.Add(new ListItem("25%", "25"));
        this.ddlPassiveAttSpeed.Items.Add(new ListItem("30%", "30"));
        this.ddlPassiveAttSpeed.DataBind();

        // 무기 : 투지 : 속도2 -> 공속 6.27~13.33%
        this.ddlEquipmentAttSpeedInt.Items.Clear();
        for (int i = 0; i <= 20; i++)
        {
            this.ddlEquipmentAttSpeedInt.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        this.ddlEquipmentAttSpeedInt.DataBind();

        this.ddlEquipmentAttSpeedMino.Items.Clear();
        for (int i = 0; i <= 99; i++)
        {
            this.ddlEquipmentAttSpeedMino.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        this.ddlEquipmentAttSpeedMino.DataBind();

        // 스속 : 스속 -> 스속 20~40%
        this.ddlEquipmentSkillSpeedInt.Items.Clear();
        for (int i = 0; i <= 40; i++)
        {
            this.ddlEquipmentSkillSpeedInt.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        this.ddlEquipmentSkillSpeedInt.DataBind();

        this.ddlEquipmentSkillSpeedMino.Items.Clear();
        for (int i = 0; i <= 99; i++)
        {
            this.ddlEquipmentSkillSpeedMino.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        this.ddlEquipmentSkillSpeedMino.DataBind();

        // 스속 패시브 20~40%
        this.ddlPassiveSkillSpeed.Items.Clear();
        for (int i = 0; i <= 40; i++)
        {
            this.ddlPassiveSkillSpeed.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        this.ddlPassiveSkillSpeed.DataBind();

        this.ddlPassiveSkillSpeedMino.Items.Clear();
        for (int i = 0; i <= 99; i++)
        {
            this.ddlPassiveSkillSpeedMino.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        this.ddlPassiveSkillSpeedMino.DataBind();
        #endregion
    }

    #region stone setting
    public void WeaponStoneSet(bool bFlag)
    {
        CardInfo ci = new CardInfo();
        if (bFlag)
        {
            if (this.ddlWeaponStone1.Items.Count == 0 || this.ddlWeaponStone2.Items.Count == 0)
            {
                DataTable dtStone = new DataTable();
                dtStone = ci.getStoneList("");

                this.ddlWeaponStone1.Items.Clear();
                this.ddlWeaponStone1.Items.Add(new ListItem("-선택-", ""));
                for (int i = 0; i < dtStone.Rows.Count; i++)
                {
                    this.ddlWeaponStone1.Items.Add(new ListItem(dtStone.Rows[i][0].ToString() + dtStone.Rows[i][1].ToString(), dtStone.Rows[i][0].ToString() + dtStone.Rows[i][1].ToString()));
                }
                this.ddlWeaponStone1.DataBind();

                this.ddlWeaponStone2.Items.Clear();
                this.ddlWeaponStone2.Items.Add(new ListItem("-선택-", ""));
                for (int i = 0; i < dtStone.Rows.Count; i++)
                {
                    this.ddlWeaponStone2.Items.Add(new ListItem(dtStone.Rows[i][0].ToString() + dtStone.Rows[i][1].ToString(), dtStone.Rows[i][0].ToString() + dtStone.Rows[i][1].ToString()));
                }
                this.ddlWeaponStone2.DataBind();
            }
        }
        else
        {
            this.ddlWeaponStone1.Items.Clear();
            this.ddlWeaponStone2.Items.Clear();
            this.ddlWeaponStone1.DataBind();
            this.ddlWeaponStone2.DataBind();

            this.lblWeaponStone1.Text = "";
            this.lblWeaponStone2.Text = "";
        }        
    }

    public void DefenseStoneSet(bool bFlag)
    {
        CardInfo ci = new CardInfo();
        if (bFlag)
        {
            if (this.ddlDefenseStone1.Items.Count == 0 || this.ddlDefenseStone2.Items.Count == 0)
            {
                DataTable dtStone = new DataTable();
                dtStone = ci.getStoneList("");

                this.ddlDefenseStone1.Items.Clear();
                this.ddlDefenseStone1.Items.Add(new ListItem("-선택-", ""));
                for (int i = 0; i < dtStone.Rows.Count; i++)
                {
                    this.ddlDefenseStone1.Items.Add(new ListItem(dtStone.Rows[i][0].ToString() + dtStone.Rows[i][1].ToString(), dtStone.Rows[i][0].ToString() + dtStone.Rows[i][1].ToString()));
                }
                this.ddlDefenseStone1.DataBind();

                this.ddlDefenseStone2.Items.Clear();
                this.ddlDefenseStone2.Items.Add(new ListItem("-선택-", ""));
                for (int i = 0; i < dtStone.Rows.Count; i++)
                {
                    this.ddlDefenseStone2.Items.Add(new ListItem(dtStone.Rows[i][0].ToString() + dtStone.Rows[i][1].ToString(), dtStone.Rows[i][0].ToString() + dtStone.Rows[i][1].ToString()));
                }
                this.ddlDefenseStone2.DataBind();
            }
        }
        else
        {
            this.ddlDefenseStone1.Items.Clear();
            this.ddlDefenseStone2.Items.Clear();
            this.ddlDefenseStone1.DataBind();
            this.ddlDefenseStone2.DataBind();

            this.lblDefenseStone1.Text = "";
            this.lblDefenseStone2.Text = "";
        }                
    }

    public void AssStoneSet(bool bFlag)
    {
        CardInfo ci = new CardInfo();
        if (bFlag)
        {
            if (this.ddlAssStone1.Items.Count == 0 || this.ddlAssStone2.Items.Count == 0)
            {
                DataTable dtStone = new DataTable();
                dtStone = ci.getStoneList("");

                this.ddlAssStone1.Items.Clear();
                this.ddlAssStone1.Items.Add(new ListItem("-선택-", ""));
                for (int i = 0; i < dtStone.Rows.Count; i++)
                {
                    this.ddlAssStone1.Items.Add(new ListItem(dtStone.Rows[i][0].ToString() + dtStone.Rows[i][1].ToString(), dtStone.Rows[i][0].ToString() + dtStone.Rows[i][1].ToString()));
                }
                this.ddlAssStone1.DataBind();

                this.ddlAssStone2.Items.Clear();
                this.ddlAssStone2.Items.Add(new ListItem("-선택-", ""));
                for (int i = 0; i < dtStone.Rows.Count; i++)
                {
                    this.ddlAssStone2.Items.Add(new ListItem(dtStone.Rows[i][0].ToString() + dtStone.Rows[i][1].ToString(), dtStone.Rows[i][0].ToString() + dtStone.Rows[i][1].ToString()));
                }
                this.ddlAssStone2.DataBind();
            }
        }
        else
        {
            this.ddlAssStone1.Items.Clear();
            this.ddlAssStone2.Items.Clear();
            this.ddlAssStone1.DataBind();
            this.ddlAssStone2.DataBind();

            this.lblAssStone1.Text = "";
            this.lblAssStone2.Text = "";
        }
    }
    #endregion

    protected void ddlPhotoType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Display();
    }    
    
    public void setResult()
    {
        CardInfo ci = new CardInfo();

        // 행동력, 치명타, 반격, 막기, 회피, 공속, 스속, 회복량, 공격량, 방어량
        // 공속 100 시작, 스속 100 시작
        // 명중, 치유량, 극대화, 탄력, 피해반사량, 흡혈
        double actPower = 0;
        double actPowerOther = 0;
        double critical = 0;
        double returnAttack = 0;
        double defense = 0;
        double vvoid = 0;
        double attackSpeed = 0;
        double skillSpeed = 0;
        double healthPer = 0;
        double attackPer = 0;
        double defensePer = 0;
        double attackData = 0;
        double defenseData = 0;
        double totalHealth = 0;
        double aim = 0;
        double health2Per = 0;
        double maxPer = 0;
        double flexPer = 0;
        double reducePer = 0;
        double bloodPer = 0;


        string actSkill1 = "";
        double actSkill1_wtime = 0;
        double actSkill1_chance = 0;        
        string actSkill2 = "";
        double actSkill2_wtime = 0;
        double actSkill2_chance = 0;
        double actEtctime1 = 0;
        double actEtctime2 = 0;
        string actPassive1 = "";
        string actPassive2 = "";

        string card_name = "";        
        Hashtable ht = new Hashtable();
        
        DataTable dt = new DataTable();        
        dt = ci.getCardInfo(this.hidV.Value, "", "", "");
        if (dt.Rows.Count > 0)
        {
            actPower = Convert.ToDouble(dt.Rows[0]["cardactpower"].ToString()); // 기본행동력            
            actSkill1 = dt.Rows[0]["cardactive1"].ToString();   // 액티브스킬1
            actSkill1_wtime = Convert.ToDouble(dt.Rows[0]["cardactive1_waiting"].ToString());
            actSkill2 = dt.Rows[0]["cardactive2"].ToString();   // 액티브스킬2
            actSkill2_wtime = Convert.ToDouble(dt.Rows[0]["cardactive2_waiting"].ToString());
            actPassive1 = dt.Rows[0]["cardpassive1"].ToString();
            actPassive2 = dt.Rows[0]["cardpassive2"].ToString();
            card_name = dt.Rows[0]["cardname"].ToString();

            ht["BASE_ACT_POWER"] = actPower;
            ht["SKILL1"] = actSkill1;
            ht["BASE_SKILL1_WTIME"] = actSkill1_wtime;
            ht["SKILL2"] = actSkill2;
            ht["BASE_SKILL2_WTIME"] = actSkill2_wtime;
            ht["PASSIVE1"] = actPassive1;
            ht["PASSIVE2"] = actPassive2;

            // 무조건 100 더한후 계산
            // 36 이면 136
            // 120 이면220
            // 250 이면350
            // 8 / 350 = 
            // 50.4 + 100 = (3 / 150.4) * 100 = 1.99468085
            //select (3.66 * 0.2) + 3.66 from dual
            //select (3.66 * -0.2) + 3.66 from dual
            string tempData = setEachData("공속");            
            // 장비 스킬 추가 (공속)
            if (Convert.ToInt32(this.ddlEquipmentAttSpeedInt.SelectedValue) > 0)
                tempData = String.Format("{0}", Convert.ToDouble(tempData) + Convert.ToDouble(this.ddlEquipmentAttSpeedInt.SelectedValue + "." + this.ddlEquipmentAttSpeedMino.SelectedValue));
            // 패시브 공속
            tempData = String.Format("{0}", Convert.ToDouble(tempData) + Convert.ToDouble(this.ddlPassiveAttSpeed.SelectedValue));

            tempData = String.Format("{0}", Convert.ToDouble(tempData) + 100);            
            attackSpeed = Convert.ToDouble(tempData); // 공속속도
            
            actPower = Math.Round((actPower / Convert.ToDouble(attackSpeed)) * 100, 3);

            // 행동력 수정 계산
            if (this.ddlActPowerMinus.SelectedValue != "")
                actPowerOther = Math.Round((actPower * Convert.ToDouble(this.ddlActPowerMinus.SelectedValue)) + actPower, 3);
            else
                actPowerOther = actPower;

            // 공속과 동일
            tempData = setEachData("스속");
            // 장비 스킬 추가 (스속)
            if (Convert.ToInt32(this.ddlEquipmentSkillSpeedInt.SelectedValue) > 0)
                tempData = String.Format("{0}", Convert.ToDouble(tempData) + Convert.ToDouble(this.ddlEquipmentSkillSpeedInt.SelectedValue + "." + this.ddlEquipmentSkillSpeedMino.SelectedValue));

            // 패시브 스속
            if (Convert.ToInt32(this.ddlPassiveSkillSpeed.SelectedValue) > 0)
                tempData = String.Format("{0}", Convert.ToDouble(tempData) + Convert.ToDouble(this.ddlPassiveSkillSpeed.SelectedValue + "." + this.ddlPassiveSkillSpeedMino.SelectedValue));

            tempData = String.Format("{0}", (Convert.ToDouble(tempData) + Convert.ToDouble(this.ddlSkillSpeedMinus.SelectedValue)) + 100);
            skillSpeed = Convert.ToDouble(tempData); // 스킬속도
            
            actSkill1_wtime = Math.Round((actSkill1_wtime / Convert.ToDouble(skillSpeed)) * 100, 3);
            actSkill2_wtime = Math.Round((actSkill2_wtime / Convert.ToDouble(skillSpeed)) * 100, 3);
            // 8 / 136 = 0.05882 실제 5.9초                                                         
   
            // 기타 계산
            actEtctime1 = Math.Round((Convert.ToDouble(this.ddlEtcTime1.SelectedValue) / Convert.ToDouble(skillSpeed)) * 100, 3);
            actEtctime2 = Math.Round((Convert.ToDouble(this.ddlEtcTime2.SelectedValue) / Convert.ToDouble(skillSpeed)) * 100, 3);

            tempData = setEachData("최대생명력");
            totalHealth = Convert.ToDouble(tempData);  // 최대생명력
            
            tempData = setEachData("치명타");
            critical = Convert.ToDouble(tempData);  // 치명타량

            tempData = setEachData("반격");
            returnAttack = Convert.ToDouble(tempData);  // 반격량

            tempData = setEachData("막기");
            defense = Convert.ToDouble(tempData);  // 막기량

            tempData = setEachData("회피");
            vvoid = Convert.ToDouble(tempData);  // 회피량

            tempData = setEachData("회복량");
            healthPer = Convert.ToDouble(tempData);  // 회복량

            tempData = setEachData("공격량");
            attackPer = Convert.ToDouble(tempData);  // 공격량

            tempData = setEachData("방어량");
            defensePer = Convert.ToDouble(tempData);  // 방어량            

            tempData = setEachData("공격력");
            attackData = Convert.ToDouble(tempData);  // 공격력

            tempData = setEachData("방어력");
            defenseData = Convert.ToDouble(tempData);  // 방어력

            tempData = setEachData("명중");
            aim = Convert.ToDouble(tempData);

            tempData = setEachData("치유량");
            health2Per = Convert.ToDouble(tempData);

            tempData = setEachData("극대화");
            maxPer = Convert.ToDouble(tempData);

            tempData = setEachData("탄력");
            flexPer = Convert.ToDouble(tempData);

            tempData = setEachData("피해반사량");
            reducePer = Convert.ToDouble(tempData);

            tempData = setEachData("흡혈량");
            bloodPer = Convert.ToDouble(tempData);


            ht["ACT_POWER"] = actPower;
            ht["ATTACK_SPEED"] = attackSpeed;
            ht["SKILL_SPEED"] = skillSpeed;
            ht["SKILL1_WTIME"] = actSkill1_wtime;
            ht["SKILL2_WTIME"] = actSkill2_wtime;
            ht["TOTAL_HEALTH"] = totalHealth;
            ht["CRITICAL"] = critical;
            ht["RETURN_ATTACK"] = returnAttack;
            ht["DEFENSE"] = defense;
            ht["VOID"] = vvoid;
            ht["HEALTH_PER"] = healthPer;
            ht["ATTACK_PER"] = attackPer;
            ht["DEFENSE_PER"] = defensePer;
            ht["ATTACK_DATA"] = attackData;
            ht["DEFENSE_DATA"] = defenseData;

            ht["AIM"] = aim;
            ht["HEALTH2_PER"] = health2Per;
            ht["MAX_PER"] = maxPer;
            ht["FLEX_PER"] = flexPer;
            ht["REDUCE_PER"] = reducePer;
            ht["BLOOD_PER"] = bloodPer;

            actSkill1_chance = Math.Ceiling(actSkill1_wtime / actPower);
            actSkill2_chance = Math.Ceiling(actSkill2_wtime / actPower);

            //eachMessage = getEachPassive(card_name, ht); // 카드별 개별 능력 적용
        }
        
        //this.lblResult.Text = "<font color='tomato'>[카드별 결과]</font>" + "<br/>";
        //this.lblResult.Text += eachMessage + "<br/>";        

        // 시전시간 계산
        // 초기화 시간 계산
        // 2.09초
        // 3초 시전
        // 50.4% 추가
        // 16.8 * 3
        // 50.4 + 100 = (3 / 150.4) * 100 = 1.99468085

        this.lblResult.Text = "<font color='blue'>[RESULT]</font>" + "<br/>";
        this.lblResult.Text += "BASIC ACTPower : " + actPower.ToString() + " Sec " + "";
        this.lblResult.Text += "(" + actPowerOther.ToString() + " Sec)" + "<br/>";
        this.lblResult.Text += actSkill1 + " CoolTime : " + actSkill1_wtime.ToString() + " Sec" + "<br/>" + "[" + actSkill1_chance.ToString() + " 번 Turn 올때 CoolTime Reset" + "] (Only Basic ActPower)" + "<br/>";
        this.lblResult.Text += actSkill2 + " CoolTime : " + actSkill2_wtime.ToString() + " Sec" + "<br/>" + "[" + actSkill2_chance.ToString() + " 번 Turn 올때 CoolTime Reset" + "] (Only Basic ActPower)" + "<br/><br/>";
        this.lblResult.Text += "<font color='blue'>[고유스킬에 행동력이나 공속 OR 스속에 영향이 있을경우 기본적인 계산방법으론 실제 전투에서 다를수 있으니 (EFFECT UP-Down) 에서 추가로 확인 하세요]</font><br/><br/>";

        this.lblEtcTime1.Text = actEtctime1.ToString() + " Sec";
        this.lblEtcTime2.Text = actEtctime2.ToString() + " Sec";
        
        
        this.lblResult.Text += "공격속도(AttSpeed) : " + attackSpeed.ToString() + " %" + "<br/>";
        this.lblResult.Text += "스킬속도(SkillSpeed) : " + skillSpeed.ToString() + " %" + "<br/>";
        this.lblResult.Text += "최대생명력(MaxHealth) : " + totalHealth.ToString() + " %" + "<br/>";
        this.lblResult.Text += "치명타(MaxCritical) : " + critical.ToString() + " %" + "<br/>";
        this.lblResult.Text += "반격(CounterAttack) : " + returnAttack.ToString() + " %" + "<br/>";
        this.lblResult.Text += "막기(Block) : " + defense.ToString() + " %" + "<br/>";
        this.lblResult.Text += "회피(Evasion) : " + vvoid.ToString() + " %" + "<br/>";
        this.lblResult.Text += "회복량(Recovery Amount) : " + healthPer.ToString() + " %" + "<br/>";
        this.lblResult.Text += "공격량(Attack Amount) : " + attackPer.ToString() + " %" + "<br/>";
        this.lblResult.Text += "방어량(Defense Amount) : " + defensePer.ToString() + " %" + "<br/>";
        this.lblResult.Text += "공격력(Offense Pow) : " + attackData.ToString() + " %" + "<br/>";
        this.lblResult.Text += "방어력(Defense Pow) : " + defenseData.ToString() + " %" + "<br/>";

        this.lblResult.Text += "명중확률(Hit Per) : " + aim.ToString() + " %" + "<br/>";
        this.lblResult.Text += "치유량(Cure Amount) : " + health2Per.ToString() + " %" + "<br/>";
        this.lblResult.Text += "극대화(Maximization) : " + maxPer.ToString() + " %" + "<br/>";
        this.lblResult.Text += "탄력(Elasticity) : " + flexPer.ToString() + " %" + "<br/>";
        this.lblResult.Text += "피해반사량(Damage Reflection Amount) : " + reducePer.ToString() + " %" + "<br/>";
        this.lblResult.Text += "흡혈량(BloodSucking Amount) : " + bloodPer.ToString() + " %" + "<br/>";
    }

    private string getEachPassive(string card_name, Hashtable ht)
    {
        string result = "";
        
        // 공격속도가 변할경우 행동력 다시 계산
        // 스킬속도가 변할경우 스킬쿨타임 다시 계산
        /*
        ht["BASE_ACT_POWER"] = actPower;
        ht["SKILL1"] = actSkill1;
        ht["BASE_SKILL1_WTIME"] = actSkill1_wtime;
        ht["SKILL2"] = actSkill2;
        ht["BASE_SKILL2_WTIME"] = actSkill2_wtime;
        ht["PASSIVE1"] = actPassive1;
        ht["PASSIVE1"] = actPassive1;

        ht["ACT_POWER"] = actPower;
        ht["ATTACK_SPEED"] = attackSpeed;
        ht["SKILL_SPEED"] = skillSpeed;
        ht["SKILL1_WTIME"] = actSkill1_wtime;
        ht["SKILL2_WTIME"] = actSkill2_wtime;
        ht["TOTAL_HEALTH"] = totalHealth;
        ht["CRITICAL"] = critical;
        ht["RETURN_ATTACK"] = returnAttack;
        ht["DEFENSE"] = defense;
        ht["VOID"] = vvoid;
        ht["HEALTH_PER"] = healthPer;
        ht["ATTACK_PER"] = attackPer;
        ht["DEFENSE_PER"] = defensePer;
        ht["ATTACK_DATA"] = attackData;
        ht["DEFENSE_DATA"] = defenseData;
        */

        double upAttackPer = 0;
        double upDefensePer = 0;
        double upAttackSpeed = 0;
        double upSkillSpeed = 0;
        double cActPower = 0;
        double upSkill1_WTime = 0;
        double upSkill2_WTime = 0;
        double upAttackData = 0;
        double upDefenseData = 0;
        double upCritical = 0;

        switch (card_name)
        {
            case "그라시아":                
                upAttackPer = Convert.ToDouble(ht["ATTACK_PER"].ToString()) + 10;
                upDefensePer = Convert.ToDouble(ht["DEFENSE_PER"].ToString()) + 40;
                upAttackSpeed = Convert.ToDouble(ht["ATTACK_SPEED"].ToString()) + 50;
                cActPower = Math.Round((Convert.ToDouble(ht["BASE_ACT_POWER"].ToString()) / upAttackSpeed) * 100, 3);

                result = "[" + card_name + "]" + "[" + ht["PASSIVE1"].ToString() + "] 패시브 " + "<br/>";
                result += "생명력이 50% 이상일경우 공격량 (" + upAttackPer.ToString() + ") %" + "<br/>";
                result += "생명력이 50% 이하일경우 방어량 (" + upDefensePer.ToString() + ") %," + 
                    " 공격속도 (" + upAttackSpeed.ToString() + ") %," + 
                    " 행동력 (" + cActPower + ") 초" 
                    + "<br/>";
                                                
                break;
            case "챠바나":                
                upDefensePer = Convert.ToDouble(ht["DEFENSE_PER"].ToString()) + 20;

                result = "[" + card_name + "]" + "[" + ht["PASSIVE2"].ToString() + "] 패시브 " + "<br/>";
                result += "생명력이 60% 이상일경우 방어량 (" + upDefensePer.ToString() + ") %" 
                    + "<br/>";

                break;

            case "벨리스트라":                
                cActPower = Math.Round(((Convert.ToDouble(ht["ACT_POWER"].ToString()) - 1) / 50) * 10, 3);

                result = "[" + card_name + "]" + "[" + ht["PASSIVE2"].ToString() + "] 패시브 " + "<br/>";
                result += "공격시 1초후 행동력 50% 증가시 남는 행동력 (" + cActPower.ToString() + ") 초"
                    + "<br/>";                

                break;

            case "루아나프라":
                cActPower = Math.Round(Convert.ToDouble(ht["ACT_POWER"].ToString()) - 
                    (Convert.ToDouble(ht["ACT_POWER"].ToString()) / 10), 3);

                upSkillSpeed = Convert.ToDouble(ht["SKILL_SPEED"].ToString()) + 100;

                upSkill1_WTime = Math.Round(((Convert.ToDouble(ht["SKILL1_WTIME"].ToString()) / Convert.ToDouble(upSkillSpeed)) * 100) - 1, 3);
                upSkill2_WTime = Math.Round(((Convert.ToDouble(ht["SKILL2_WTIME"].ToString()) / Convert.ToDouble(upSkillSpeed)) * 100) - 1, 3);                

                result = "[" + card_name + "]" + "[" + ht["PASSIVE1"].ToString() + "] 패시브 " + "<br/>";
                result += "피격시 행동력 10% 증가후 행동력 (" + cActPower.ToString() + ") 초"
                    + "<br/>";

                cActPower = Math.Round(cActPower -
                    (cActPower / 10), 3);
                result += "피격시 행동력 20% 증가후 행동력 (" + cActPower.ToString() + ") 초"
                    + "<br/>";                

                result += "공격시 자신에게 1초 동안 스킬속도 100% 증가후 [" + ht["SKILL1"].ToString() + "] 스킬 쿨타임 (" + 
                    upSkill1_WTime.ToString() + ") 초, " + " [" + ht["SKILL2"].ToString() + "] 스킬 쿨타임 (" + 
                    upSkill2_WTime.ToString() + ") 초 "
                    + "<br/>";                

                break;

            case "제롬올렌더":
                upAttackData = Convert.ToDouble(ht["ATTACK_DATA"].ToString()) + 50;

                result = "[" + card_name + "]" + "[" + ht["PASSIVE1"].ToString() + "] 패시브 " + "<br/>";
                result += "피격시 2초동안 공격력 50% 증가 (" + upAttackData.ToString() + ") %"
                    + "<br/>";

                break;

            case "레티르":
                upCritical = Convert.ToDouble(ht["CRITICAL"].ToString()) + 10;

                upAttackSpeed = Convert.ToDouble(ht["ATTACK_SPEED"].ToString()) + 25;
                cActPower = Math.Round((Convert.ToDouble(ht["BASE_ACT_POWER"].ToString()) / upAttackSpeed) * 100, 3);

                result = "[" + card_name + "]" + "[" + ht["PASSIVE1"].ToString() + "] 패시브 " + "<br/>";
                result += "공격시 8초동안 공격속도 25% 증가 (" + upAttackSpeed.ToString() + ") %,"
                    + "치명타 10% 증가 (" + upCritical.ToString() + ") %"
                    + "<br/>";

                break;

            case "로제타":
                result = "[" + card_name + "]" + "[" + ht["PASSIVE1"].ToString() + "] 패시브 " + "<br/>";
                result += "공격시 아군모두에게 행동력을 10% 증가 "
                    + "<br/>";

                upAttackSpeed = Convert.ToDouble(ht["ATTACK_SPEED"].ToString()) + 100;
                cActPower = Math.Round((Convert.ToDouble(ht["BASE_ACT_POWER"].ToString()) / upAttackSpeed) * 100, 3);

                result += "[" + card_name + "]" + "[" + ht["PASSIVE2"].ToString() + "] 패시브 " + "<br/>";
                result += "생명력 100% 일경우 행동력 (" + cActPower.ToString() + ") 초"
                    + "<br/>";

                upAttackSpeed = Convert.ToDouble(ht["ATTACK_SPEED"].ToString()) + 80;
                cActPower = Math.Round((Convert.ToDouble(ht["BASE_ACT_POWER"].ToString()) / upAttackSpeed) * 100, 3);

                result += "[" + card_name + "]" + "[" + ht["PASSIVE2"].ToString() + "] 패시브 " + "<br/>";
                result += "생명력 80% 일경우 행동력 (" + cActPower.ToString() + ") 초"
                    + "<br/>";

                upAttackSpeed = Convert.ToDouble(ht["ATTACK_SPEED"].ToString()) + 60;
                cActPower = Math.Round((Convert.ToDouble(ht["BASE_ACT_POWER"].ToString()) / upAttackSpeed) * 100, 3);

                result += "[" + card_name + "]" + "[" + ht["PASSIVE2"].ToString() + "] 패시브 " + "<br/>";
                result += "생명력 60% 일경우 행동력 (" + cActPower.ToString() + ") 초"
                    + "<br/>";                

                upSkillSpeed = Convert.ToDouble(ht["SKILL_SPEED"].ToString()) + 100;

                upSkill1_WTime = Math.Round(((Convert.ToDouble(ht["SKILL1_WTIME"].ToString()) / Convert.ToDouble(upSkillSpeed)) * 100) - 2, 3);
                upSkill2_WTime = Math.Round(((Convert.ToDouble(ht["SKILL2_WTIME"].ToString()) / Convert.ToDouble(upSkillSpeed)) * 100) - 2, 3);
                result += "[" + card_name + "]" + "[" + ht["PASSIVE2"].ToString() + "] 패시브 " + "<br/>";
                result += "공격시 자신에게 2초 동안 스킬속도 100% 증가후 [" + ht["SKILL1"].ToString() + "] 스킬 쿨타임 (" +
                    upSkill1_WTime.ToString() + ") 초, " + " [" + ht["SKILL2"].ToString() + "] 스킬 쿨타임 (" +
                    upSkill2_WTime.ToString() + ") 초 "
                    + "<br/>";                
                

                break;

            case "헬카이트":
                upAttackPer = Convert.ToDouble(ht["ATTACK_PER"].ToString()) + 30;
                upAttackData = Convert.ToDouble(ht["ATTACK_DATA"].ToString()) + 30;                
                upAttackSpeed = Convert.ToDouble(ht["ATTACK_SPEED"].ToString()) + 30;                
                cActPower = Math.Round((Convert.ToDouble(ht["BASE_ACT_POWER"].ToString()) / upAttackSpeed) * 100, 3);                

                result = "[" + card_name + "]" + "[" + ht["PASSIVE1"].ToString() + "] 패시브 " + "<br/>";
                result += "생명력이 50% 이상일경우 공격속도 30% 증가 (" + upAttackSpeed.ToString() + ") %, 행동력 (" + cActPower.ToString() + ") 초" + "<br/>";
                result += "생명력이 50% 이하일경우 공격력 30% 증가 (" + upAttackData + ") % 공격량 30% 증가 (" + upAttackPer.ToString() + ") %"                    
                    + "<br/>";

                upDefenseData = Convert.ToDouble(ht["DEFENSE_DATA"].ToString()) + 20;
                cActPower = Math.Round(Convert.ToDouble(ht["ACT_POWER"].ToString()) -
                    (Convert.ToDouble(ht["ACT_POWER"].ToString()) / 10), 3);

                upSkillSpeed = Convert.ToDouble(ht["SKILL_SPEED"].ToString()) + 20;

                upSkill1_WTime = Math.Round(((Convert.ToDouble(ht["SKILL1_WTIME"].ToString()) / Convert.ToDouble(upSkillSpeed)) * 100), 3);
                upSkill2_WTime = Math.Round(((Convert.ToDouble(ht["SKILL2_WTIME"].ToString()) / Convert.ToDouble(upSkillSpeed)) * 100), 3);                

                result += "[" + card_name + "]" + "[" + ht["PASSIVE2"].ToString() + "] 패시브 " + "<br/>";
                result += "피격시 행동력 10% 증가 (" + cActPower.ToString() + ") 초,  5초동안 방어력 증가 (" + upDefenseData.ToString() + ") %, 스킬속도 20% 증가 (" + upSkillSpeed.ToString() + ") %"
                    + "<br/>";

                result += "[" + ht["SKILL1"].ToString() + "] 스킬 쿨타임 " +
                    upSkill1_WTime.ToString() + " 초, " + " [" + ht["SKILL2"].ToString() + "] 스킬 쿨타임 " +
                    upSkill2_WTime.ToString() + " 초 "
                    + "<br/>";                

                break;

            case "히실리스":
                upDefenseData = Convert.ToDouble(ht["DEFENSE_DATA"].ToString()) + 20;

                upAttackSpeed = Convert.ToDouble(ht["ATTACK_SPEED"].ToString()) + 100;
                cActPower = Math.Round((Convert.ToDouble(ht["BASE_ACT_POWER"].ToString()) / upAttackSpeed) * 100, 3);

                result = "[" + card_name + "]" + "[" + ht["PASSIVE1"].ToString() + "] 패시브 " + "<br/>";
                result += "생명력이 20% 감소할때마다 방어량 20% 증가" + "<br/>";
                result += "생명력이 70% 이상일경우 공격속도가 100% 증가 (" + upAttackSpeed.ToString() + ") %," +
                    "행동력 (" + cActPower.ToString() + ") 초"
                    + "<br/>";

                break;

            case "프랑켄소와즈":
                cActPower = Math.Round(Convert.ToDouble(ht["ACT_POWER"].ToString()) -
                    ((Convert.ToDouble(ht["ACT_POWER"].ToString()) / 10) * 5), 3);

                result = "[" + card_name + "]" + "[" + ht["PASSIVE2"].ToString() + "] 패시브 " + "<br/>";
                result += "피격시 행동력 50% 증가 (" + cActPower.ToString() + ") 초"
                    + "<br/>";

                break;

            case "해변의클라우디아":
                upAttackSpeed = Convert.ToDouble(ht["ATTACK_SPEED"].ToString()) + 50;
                cActPower = Math.Round((Convert.ToDouble(ht["BASE_ACT_POWER"].ToString()) / upAttackSpeed) * 100, 3);                

                upSkillSpeed = Convert.ToDouble(ht["SKILL_SPEED"].ToString()) + 100;

                upSkill1_WTime = Math.Round(((Convert.ToDouble(ht["SKILL1_WTIME"].ToString()) / Convert.ToDouble(upSkillSpeed)) * 100) - 1, 3);
                upSkill2_WTime = Math.Round(((Convert.ToDouble(ht["SKILL2_WTIME"].ToString()) / Convert.ToDouble(upSkillSpeed)) * 100) - 1, 3);

                result += "[" + card_name + "]" + "[" + ht["PASSIVE2"].ToString() + "] 패시브 " + "<br/>";
                result += "명령을 수행할때마다 아군 모두에게 1초동안 스킬속도를 100% 증가 (" + upSkillSpeed.ToString() + ") %, 공격속도를 50% 증가 (" + upAttackSpeed.ToString() + ") %"
                    + ", 행동력 ("+cActPower.ToString()+") 초"
                    + "<br/>";

                result += "[" + ht["SKILL1"].ToString() + "] 스킬 쿨타임 " +
                    upSkill1_WTime.ToString() + " 초, " + " [" + ht["SKILL2"].ToString() + "] 스킬 쿨타임 " +
                    upSkill2_WTime.ToString() + " 초 "
                    + "<br/>";                

                break;

            case "마리안느":
                cActPower = Math.Round(Convert.ToDouble(ht["ACT_POWER"].ToString()) -
                    ((Convert.ToDouble(ht["ACT_POWER"].ToString()) / 10) * 2.5), 3);

                upAttackPer = Convert.ToDouble(ht["ATTACK_PER"].ToString()) + 20;

                result = "[" + card_name + "]" + "[" + ht["PASSIVE1"].ToString() + "] 패시브 " + "<br/>";
                result += "피격시 행동력 25% 증가 ("+ cActPower.ToString() + " 초) 5초동안 공격량 20% 증가 (" + upAttackPer.ToString() + ") %"
                    + "<br/>";
                
                break;

            case "브린힐트":
                upAttackData = Convert.ToDouble(ht["ATTACK_DATA"].ToString()) + 10;
                upDefenseData = Convert.ToDouble(ht["DEFENSE_DATA"].ToString()) + 20;

                result = "[" + card_name + "]" + "[" + ht["PASSIVE1"].ToString() + "] 패시브 " + "<br/>";
                result += "공격시 6초동안 공격력 10% 증가 (" + upAttackData.ToString() + " %) 방어력 20% 증가 (" + upDefenseData.ToString() + ") %"
                    + "<br/>";

                break;

            case "단테":

                break;
            default: break;
        }

        return result;
    }    

    #region 각 데이터 가져오기
    public string setEachData(string kind)
    {
        string result = "";

        double totalData = 0;
        #region 대표카드 이펙트
        if (this.lblMainCardEffect.Text != "")
        {
            // 능력치 2개이상
            if (this.lblMainCardEffect.Text.IndexOf(",") > -1)
            {
                string[] arrTemp = this.lblMainCardEffect.Text.Split(',');
                for (int i = 0; i < arrTemp.Length; i++)
                {
                    if (arrTemp[i].Trim().Split(' ')[0].IndexOf(kind) > -1)
                    {
                        totalData += Convert.ToDouble(arrTemp[i].Trim().Split(' ')[1].Replace("%",""));
                    }
                }
            }
            else // 능력치 1개
            {
                if (this.lblMainCardEffect.Text.Split(' ')[0].IndexOf(kind) > -1)
                {
                    totalData += Convert.ToDouble(this.lblMainCardEffect.Text.Split(' ')[1].Replace("%", ""));
                }
            }
        }
        #endregion

        #region 무기 이펙트
        if (this.lblWeapon.Text != "")
        {
            if (this.lblWeapon.Text.Split(' ')[0].IndexOf(kind) > -1)
            {
                totalData += Convert.ToDouble(this.lblWeapon.Text.Split(' ')[1].Replace("%", ""));
            }
        }
        if (this.lblWeaponStone1.Text != "")
        {
            if (this.lblWeaponStone1.Text.Split(' ')[0].IndexOf(kind) > -1)
            {
                totalData += Convert.ToDouble(this.lblWeaponStone1.Text.Split(' ')[1].Replace("%", ""));
            }
        }
        if (this.lblWeaponStone2.Text != "")
        {
            if (this.lblWeaponStone2.Text.Split(' ')[0].IndexOf(kind) > -1)
            {
                totalData += Convert.ToDouble(this.lblWeaponStone2.Text.Split(' ')[1].Replace("%", ""));
            }
        }        

        #endregion

        #region 방어구 이펙트
        if (this.lblDefense.Text != "")
        {
            if (this.lblDefense.Text.Split(' ')[0].IndexOf(kind) > -1)
            {
                totalData += Convert.ToDouble(this.lblDefense.Text.Split(' ')[1].Replace("%", ""));
            }
        }
        if (this.lblDefenseStone1.Text != "")
        {
            if (this.lblDefenseStone1.Text.Split(' ')[0].IndexOf(kind) > -1)
            {
                totalData += Convert.ToDouble(this.lblDefenseStone1.Text.Split(' ')[1].Replace("%", ""));
            }
        }
        if (this.lblDefenseStone2.Text != "")
        {
            if (this.lblDefenseStone2.Text.Split(' ')[0].IndexOf(kind) > -1)
            {
                totalData += Convert.ToDouble(this.lblDefenseStone2.Text.Split(' ')[1].Replace("%", ""));
            }
        }
        #endregion

        #region 장신구 이펙트
        if (this.lblAss.Text != "")
        {
            if (this.lblAss.Text.Split(' ')[0].IndexOf(kind) > -1)
            {
                totalData += Convert.ToDouble(this.lblAss.Text.Split(' ')[1].Replace("%", ""));
            }
        }
        if (this.lblAssStone1.Text != "")
        {
            if (this.lblAssStone1.Text.Split(' ')[0].IndexOf(kind) > -1)
            {
                totalData += Convert.ToDouble(this.lblAssStone1.Text.Split(' ')[1].Replace("%", ""));
            }
        }
        if (this.lblAssStone2.Text != "")
        {
            if (this.lblAssStone2.Text.Split(' ')[0].IndexOf(kind) > -1)
            {
                totalData += Convert.ToDouble(this.lblAssStone2.Text.Split(' ')[1].Replace("%", ""));
            }
        }
        #endregion

        result = totalData.ToString();

        return result;
    }
    #endregion

    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Image img = ((Image)e.Item.FindControl("imgPhoto"));
            Label lblcardname = ((Label)e.Item.FindControl("lblCardName"));
            Label lblcardinfo = ((Label)e.Item.FindControl("lblCardInfo"));
            Label lblcardmaininfo = ((Label)e.Item.FindControl("lblCardMainInfo"));                        

            DataRowView dr = ((DataRowView)e.Item.DataItem);
            string card_level = dr["cardlevel"].ToString();
            string card_name = dr["cardname"].ToString();
            string card_type = dr["cardtype"].ToString();
            string card_race = dr["cardrace"].ToString();
            string card_image = dr["cardimage"].ToString();
            string card_maininfo = dr["maineffect"].ToString();

            lblcardname.Text = card_name + " [" + card_level + "]";
            lblcardinfo.Text = "[" + card_type + "]" + "타입, " + "[" + card_race + "]" + "종족";
            lblcardmaininfo.Text = "-" + card_maininfo + "-";
                       
            if (File.Exists(Server.MapPath("Files/" + card_image)))
            {
                img.ImageUrl = "Files/" + card_image;
            }
            else
            {
                if (File.Exists(Server.MapPath("Files/" + card_name + ".png")))
                {
                    img.ImageUrl = "Files/" + card_name + ".png";
                }
                else
                {
                    img.ImageUrl = "Files/non.jpg";
                }                
            }
            img.Attributes.Add("onClick", "viewImage('"+card_name+"')");
            img.Attributes.Add("style", "width:100px;; height:120px;");            
        }
    }

    protected void btnGoMain_Click(object sender, EventArgs e)
    {
        this.panMain.Visible = true;
        this.panView.Visible = false;
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        CardInfo ci = new CardInfo();
        this.lblViewCardName.Text = this.hidV.Value;

        DataTable dt = new DataTable();        
        dt = ci.getCardInfo(this.hidV.Value, "", "", "");

        if (dt.Rows.Count > 0)
        {
            lblViewCardName.Text = dt.Rows[0]["cardname"].ToString() + " [" + dt.Rows[0]["cardlevel"].ToString() + "]" + "<br/>";
            lblViewCardName.Text += "[" + dt.Rows[0]["cardtype"].ToString() + "]" + "타입, " + "[" + dt.Rows[0]["cardrace"].ToString() + "]" + "종족" + "<br/>";
            lblViewCardName.Text += "대표카드 설정시 : " + "<br/>" + "-" + dt.Rows[0]["maineffect"].ToString() + "-";

            if (File.Exists(Server.MapPath("Files/" + dt.Rows[0]["cardimage"].ToString())))
            {
                imgViewPhoto.ImageUrl = "Files/" + dt.Rows[0]["cardimage"].ToString();
            }
            else
            {
                imgViewPhoto.ImageUrl = "Files/non.jpg";
            }
            imgViewPhoto.Attributes.Add("style", "width:70px; height:70px;");

            this.panMain.Visible = false;
            this.panView.Visible = true;

            setResult();
        }
        else
        {
            this.panMain.Visible = true;
            this.panView.Visible = false;
        }        
    }

    protected void ddlWeapon_SelectedIndexChanged(object sender, EventArgs e)
    {
        CardInfo ci = new CardInfo();
        DataTable dt = new DataTable();
        dt = ci.getWeaponList(this.ddlWeapon.SelectedValue);

        if (this.ddlWeapon.SelectedValue != "")
        {
            if (dt.Rows.Count > 0)
            {
                this.lblWeapon.Text = dt.Rows[0][2].ToString() + " " + dt.Rows[0][3].ToString() + "%";

                WeaponStoneSet(true);
            }
        }
        else
        {
            this.lblWeapon.Text = "";
            WeaponStoneSet(false);
        }

        setResult();
    }

    protected void ddlWeaponStone1_SelectedIndexChanged(object sender, EventArgs e)
    {
        CardInfo ci = new CardInfo();
        DataTable dt = new DataTable();
        dt = ci.getStoneList(this.ddlWeaponStone1.SelectedValue);

        if (this.ddlWeaponStone1.SelectedValue != "")
        {
            if (dt.Rows.Count > 0)
            {
                this.lblWeaponStone1.Text = dt.Rows[0][2].ToString() + " " + dt.Rows[0][3].ToString() + "%";
            }
        }
        else
            this.lblWeaponStone1.Text = "";

        setResult();
    }

    protected void ddlWeaponStone2_SelectedIndexChanged(object sender, EventArgs e)
    {
        CardInfo ci = new CardInfo();
        DataTable dt = new DataTable();
        dt = ci.getStoneList(this.ddlWeaponStone2.SelectedValue);

        if (this.ddlWeaponStone2.SelectedValue != "")
        {
            if (dt.Rows.Count > 0)
            {
                this.lblWeaponStone2.Text = dt.Rows[0][2].ToString() + " " + dt.Rows[0][3].ToString() + "%";
            }
        }
        else
            this.lblWeaponStone2.Text = "";

        setResult();
    }

    protected void ddlDefense_SelectedIndexChanged(object sender, EventArgs e)
    {
        CardInfo ci = new CardInfo();
        DataTable dt = new DataTable();
        dt = ci.getDefenseList(this.ddlDefense.SelectedValue);

        if (this.ddlDefense.SelectedValue != "")
        {
            if (dt.Rows.Count > 0)
            {
                this.lblDefense.Text = dt.Rows[0][2].ToString() + " " + dt.Rows[0][3].ToString() + "%";

                DefenseStoneSet(true);
            }
        }
        else
        {
            this.lblDefense.Text = "";
            DefenseStoneSet(false);
        }

        setResult();
    }

    protected void ddlDefenseStone1_SelectedIndexChanged(object sender, EventArgs e)
    {
        CardInfo ci = new CardInfo();
        DataTable dt = new DataTable();
        dt = ci.getStoneList(this.ddlDefenseStone1.SelectedValue);

        if (this.ddlDefenseStone1.SelectedValue != "")
        {
            if (dt.Rows.Count > 0)
            {
                this.lblDefenseStone1.Text = dt.Rows[0][4].ToString() + " " + dt.Rows[0][5].ToString() + "%";
            }
        }
        else
            this.lblDefenseStone1.Text = "";

        setResult();
    }

    protected void ddlDefenseStone2_SelectedIndexChanged(object sender, EventArgs e)
    {
        CardInfo ci = new CardInfo();
        DataTable dt = new DataTable();
        dt = ci.getStoneList(this.ddlDefenseStone2.SelectedValue);

        if (this.ddlDefenseStone2.SelectedValue != "")
        {
            if (dt.Rows.Count > 0)
            {
                this.lblDefenseStone2.Text = dt.Rows[0][4].ToString() + " " + dt.Rows[0][5].ToString() + "%";
            }
        }
        else
            this.lblDefenseStone2.Text = "";

        setResult();
    }

    protected void ddlAss_SelectedIndexChanged(object sender, EventArgs e)
    {
        CardInfo ci = new CardInfo();
        DataTable dt = new DataTable();
        dt = ci.getAssList(this.ddlAss.SelectedValue);

        if (this.ddlAss.SelectedValue != "")
        {
            if (dt.Rows.Count > 0)
            {
                this.lblAss.Text = dt.Rows[0][2].ToString() + " " + dt.Rows[0][3].ToString() + "%";

                AssStoneSet(true);
            }
        }
        else
        {
            this.lblAss.Text = "";
            AssStoneSet(false);
        }

        setResult();
    }

    protected void ddlAssStone1_SelectedIndexChanged(object sender, EventArgs e)
    {
        CardInfo ci = new CardInfo();
        DataTable dt = new DataTable();
        dt = ci.getStoneList(this.ddlAssStone1.SelectedValue);

        if (this.ddlAssStone1.SelectedValue != "")
        {
            if (dt.Rows.Count > 0)
            {
                this.lblAssStone1.Text = dt.Rows[0][6].ToString() + " " + dt.Rows[0][7].ToString() + "%";
            }
        }
        else
            this.lblAssStone1.Text = "";

        setResult();
    }

    protected void ddlAssStone2_SelectedIndexChanged(object sender, EventArgs e)
    {
        CardInfo ci = new CardInfo();
        DataTable dt = new DataTable();
        dt = ci.getStoneList(this.ddlAssStone2.SelectedValue);

        if (this.ddlAssStone2.SelectedValue != "")
        {
            if (dt.Rows.Count > 0)
            {
                this.lblAssStone2.Text = dt.Rows[0][6].ToString() + " " + dt.Rows[0][7].ToString() + "%";
            }
        }
        else
            this.lblAssStone2.Text = "";

        setResult();
    }

    protected void ddlActPowerMinus_SelectedIndexChanged(object sender, EventArgs e)
    {
        setResult();
    }
    protected void ddlSkillSpeedMinus_SelectedIndexChanged(object sender, EventArgs e)
    {
        setResult();
    }
    protected void ddlEtcTime1_SelectedIndexChanged(object sender, EventArgs e)
    {
        setResult();
    }
    protected void ddlEtcTime2_SelectedIndexChanged(object sender, EventArgs e)
    {
        setResult();
    }

    protected void ddlMainCard_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMainCard.SelectedValue == "")
        {
            this.lblMainCardEffect.Text = "";
        }
        else
        {
            this.lblMainCardEffect.Text = ddlMainCard.SelectedValue;            
        }

        setResult();
    }
    protected void btnImage_Click(object sender, EventArgs e)
    {
        Response.Redirect("PhotoGuideList.aspx");
    }

    protected void ddlEquipmentAttSpeedInt_SelectedIndexChanged(object sender, EventArgs e)
    {
        setResult();
    }
    protected void ddlEquipmentAttSpeedMino_SelectedIndexChanged(object sender, EventArgs e)
    {
        setResult();
    }
    protected void ddlEquipmentSkillSpeedInt_SelectedIndexChanged(object sender, EventArgs e)
    {
        setResult();
    }
    protected void ddlEquipmentSkillSpeedMino_SelectedIndexChanged(object sender, EventArgs e)
    {
        setResult();
    }
    protected void ddlPassiveAttSpeed_SelectedIndexChanged(object sender, EventArgs e)
    {
        setResult();
    }
    protected void ddlPassiveSkillSpeed_SelectedIndexChanged(object sender, EventArgs e)
    {
        setResult();
    }
    protected void ddlPassiveSkillSpeedMino_SelectedIndexChanged(object sender, EventArgs e)
    {
        setResult();
    }
    protected void btnGuild_Click(object sender, EventArgs e)
    {
        Response.Redirect("https://www.usaki.co.kr:40016/maskLocation.aspx");
    }    
}
