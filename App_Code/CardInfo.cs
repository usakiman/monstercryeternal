﻿using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Data.SqlClient;

/// <summary>
/// CardInfo의 요약 설명입니다.
/// </summary>
public class CardInfo
{
    public string CARDNAME { get; set; }
    public string CARDLEVEL { get; set; } // sss, ss, s
    public string CARDTYPE { get; set; } // 만능, 공격, 방어
    public string CARDRACE { get; set; } // 인간, 악마, 요정
    public string CARDACTPOWER { get; set; } // 기본행동력
    public string CARDACTIVE1 { get; set; } // 액티브스킬
    public string CARDACTIVE1_WAITING { get; set; }
    public string CARDACTIVE2 { get; set; }
    public string CARDACTIVE2_WAITING { get; set; }
    public string CARDPASSIVE1 { get; set; } // 패시브스킬
    public string CARDPASSIVE2 { get; set; }
    public string MAINEFFECT { get; set; }
    public string CARDIMAGE { get; set; } // 이미지

	public CardInfo()
	{
        /*
         * create table card_info
         (
             seq int identity(1,1) not null primary key,
             cardname varchar(255) not null,
             cardlevel varchar(25) not null,
             cardtype varchar(25) not null,
             cardrace varchar(25) not null,
             cardactpower varchar(25) not null,
             cardactive1 varchar(255) null,
             cardactive1_waiting varchar(5) default 0,
             cardactive2 varchar(255) null,
             cardactive2_waiting varchar(5) default 0,
             cardpassive1 varchar(255) null,
             cardpassive2 varchar(255) null,
             maineffect varchar(255) null,
             cardimage varchar(255) null
         )
         go
         * 
         * 마야
 85 5강 78050 13950 24800
 85 3강 67643 12090 21493

 80 5강 64147 11464 20381
 80 3강 55594 9936  17664

 75 3강 45696 8167  14519
 75 5강 52726 9423  16752


 이오니아
 85 5강 91335 24578 17714
 85 3강 79157 21301 15352

 80 5강 75066 20199 14558
 80 3강 65057 17506 12617

 75 5강 61701 16603 11966
 75 3강 53474 14389 10370

 그라시아
 85 5강 84693 22585 21257
 85 3강 73400 19574 18422

 80 5강 69607 18561 17470
 80 3강 60326 16087 15140

 75 5강 57214 15257 14359
 75 3강 49585 13222 12445

 산타인장
 최대 생명력 3767 -> 4667
 최대 생명력 12%

 산타제복
 방어력 1048 -> 1263
 방어력 +24%

 산타의장갑
 공격력 1048 -> 1263
 공격력 6%

 검
 공격력 994 -> 1209

 방어구
 방어력 994 -> 1209

 장신구
 최대생명력 3542 -> 4442

 마석
 자색 눈의 결정
 무기   공격력 6%
 방어구 방어력 24%
 장신구 최대생명력 12%
         */
    }

    public string insertCard(string name, string level, string type, string race, string actpower,
        string active1, string active1_w, string active2, string active2_w, string passive1, string passive2,
        string maineffect, string image)
    {
        string result = "";

        string image_edit = name + ".jpg";

        StringBuilder sb = new StringBuilder();
        sb.Append(@"
            insert into card_info
            select
                '"+name+@"','"+level+@"','"+type+@"',
                '"+race+@"','"+actpower+@"','"+active1+@"',
                '" + active1_w + @"','" + active2 + @"','" + active2_w + @"',
                '" + passive1 + @"','" + passive2 + @"','" + maineffect + @"',
                '" + image_edit + @"'
        ");

        SqlDataBase db = new SqlDataBase();

        try
        {
            db.sqlExeQuery(sb.ToString(), null, "TEXT");
            result = "입력 성공";
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }

        return result;
    }

    public string updateCard(string name, string maineffect)
    {
        string result = "";

        StringBuilder sb = new StringBuilder();
        sb.Append(@"
            update card_info
            set maineffect = '"+maineffect+@"'
            where cardname = '"+name+@"'                
        ");

        SqlDataBase db = new SqlDataBase();

        try
        {
            db.sqlExeQuery(sb.ToString(), null, "TEXT");
            result = "업데이트 성공";
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }

        return result;
    }

    public string deleteCard(string name)
    {
        string result = "";

        StringBuilder sb = new StringBuilder();
        sb.Append(@"
            delete card_info where cardname = '"+name+@"'            
        ");

        SqlDataBase db = new SqlDataBase();

        try
        {
            db.sqlExeQuery(sb.ToString(), null, "TEXT");
            result = "삭제 성공";
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }

        return result;
    }

    public DataTable getCardInfo(string cardname, string cardlevel, string cardrace, string effect)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(@"
            select * from card_info where 1=1
        ");

        if (cardname != "")
        {
            sb.Append(" and cardname = '"+cardname+"' ");
        }

        if (cardlevel != "")
        {
            sb.Append(" and cardlevel = '" + cardlevel + "' ");
        }

        if (cardrace != "")
        {
            sb.Append(" and cardrace = '" + cardrace + "' ");
        }

        if (effect != "")
        {
            sb.Append(" and maineffect is not null ");
        }

        sb.Append(@" order by cardname ");

        SqlDataBase db = new SqlDataBase();
        DataSet ds = db.sqlReaderQuery(sb.ToString(), null, "TEXT");

        return ds.Tables[0];
    }

    public DataTable getWeaponList(string level_kind)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("LEVEL");
        dt.Columns.Add("KIND");
        dt.Columns.Add("EFFECT");
        dt.Columns.Add("EFFECT_V");

        string[] list_x = new string[] { "심연의", "철퇴", "공격량", "7.84" };
        string[] list_y = new string[] { "불멸의", "검", "공속", "7.84" };
        string[] list_z = new string[] { "심해의", "채찍", "명중", "10.5" };
        string[] list_zz = new string[] { "학살의", "창", "공속", "7.84" };
        string[] list_zzz = new string[] { "파멸의", "지팡이", "치명타", "10.5" };

        string[] list_a = new string[] { "승천자", "검", "공속", "7.84" };
        string[] list_b = new string[] { "승천자", "도끼", "치명타", "10.5" };
        string[] list_c = new string[] { "승천자", "활", "공격량", "7.84" };

        string[] list_1 = new string[] { "군단장", "검", "공속", "6.72" };
        string[] list_2 = new string[] { "군단장", "도끼", "치명타", "9" };
        string[] list_3 = new string[] { "군단장", "활", "공격량", "6.72" };

        string[] list_4 = new string[] { "천인장", "검", "공속", "5.6" };
        string[] list_5 = new string[] { "천인장", "도끼", "치명타", "7.5" };
        string[] list_6 = new string[] { "천인장", "활", "공격량", "5.6" };

        string[] list_7 = new string[] { "백인장", "검", "공속", "4.48" };
        string[] list_8 = new string[] { "백인장", "도끼", "치명타", "6" };
        string[] list_9 = new string[] { "백인장", "활", "공격량", "4.48" };

        string[] list_10 = new string[] { "정예병", "검", "공속", "3.36" };
        string[] list_11 = new string[] { "정예병", "도끼", "치명타", "4.5" };
        string[] list_12 = new string[] { "정예병", "활", "공격량", "3.36" };        

        switch (level_kind)
        {
            case "심연의철퇴": dt.Rows.Add(list_x); break;
            case "불멸의검": dt.Rows.Add(list_y); break;
            case "심해의채찍": dt.Rows.Add(list_z); break;
            case "학살의창": dt.Rows.Add(list_zz); break;
            case "파멸의지팡이": dt.Rows.Add(list_zzz); break;

            case "승천자검": dt.Rows.Add(list_a); break;
            case "승천자도끼": dt.Rows.Add(list_b); break;
            case "승천자활": dt.Rows.Add(list_c); break;

            case "군단장검": dt.Rows.Add(list_1); break;
            case "군단장도끼": dt.Rows.Add(list_2); break;
            case "군단장활": dt.Rows.Add(list_3); break;

            case "천인장검": dt.Rows.Add(list_4); break;
            case "천인장도끼": dt.Rows.Add(list_5); break;
            case "천인장활": dt.Rows.Add(list_6); break;

            case "백인장검": dt.Rows.Add(list_7); break;
            case "백인장도끼": dt.Rows.Add(list_8); break;
            case "백인장활": dt.Rows.Add(list_9); break;

            case "정예병검": dt.Rows.Add(list_10); break;
            case "정예병도끼": dt.Rows.Add(list_11); break;
            case "정예병활": dt.Rows.Add(list_12); break;
            default:
                dt.Rows.Add(list_x);
                dt.Rows.Add(list_y);
                dt.Rows.Add(list_z);
                dt.Rows.Add(list_zz);
                dt.Rows.Add(list_zzz);
                dt.Rows.Add(list_a);
                dt.Rows.Add(list_b);
                dt.Rows.Add(list_c);
                dt.Rows.Add(list_1);
                dt.Rows.Add(list_2);
                dt.Rows.Add(list_3);
                dt.Rows.Add(list_4);
                dt.Rows.Add(list_5);
                dt.Rows.Add(list_6);
                dt.Rows.Add(list_7);
                dt.Rows.Add(list_8);
                dt.Rows.Add(list_9);
                dt.Rows.Add(list_10);
                dt.Rows.Add(list_11);
                dt.Rows.Add(list_12);
                break;
        }

        return dt;
    }

    public DataTable getDefenseList(string level_kind)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("LEVEL");
        dt.Columns.Add("KIND");
        dt.Columns.Add("EFFECT");
        dt.Columns.Add("EFFECT_V");

        string[] list_x = new string[] { "심연의", "갑옷", "방어량", "6.3" };
        string[] list_y = new string[] { "불멸의", "벨트", "회피", "7" };
        string[] list_z = new string[] { "심해의", "망토", "회복량", "21.28" };
        string[] list_zz = new string[] { "학살의", "방패", "막기", "14" };
        string[] list_zzz = new string[] { "파멸의", "갑옷", "탄력", "26.25" };

        string[] list_a = new string[] { "승천자", "갑옷", "방어량", "6.3" };
        string[] list_b = new string[] { "승천자", "방패", "막기", "14" };
        string[] list_c = new string[] { "승천자", "투구", "회피", "7" };

        string[] list_1 = new string[] { "군단장", "갑옷", "방어량", "5.4" };
        string[] list_2 = new string[] { "군단장", "방패", "막기", "12" };
        string[] list_3 = new string[] { "군단장", "투구", "회피", "6" };

        string[] list_4 = new string[] { "천인장", "갑옷", "방어량", "4.5" };
        string[] list_5 = new string[] { "천인장", "방패", "막기", "10" };
        string[] list_6 = new string[] { "천인장", "투구", "회피", "5" };

        string[] list_7 = new string[] { "백인장", "갑옷", "방어량", "3.6" };
        string[] list_8 = new string[] { "백인장", "방패", "막기", "8" };
        string[] list_9 = new string[] { "백인장", "투구", "회피", "4" };

        string[] list_10 = new string[] { "정예병", "갑옷", "방어량", "2.7" };
        string[] list_11 = new string[] { "정예병", "방패", "막기", "6" };
        string[] list_12 = new string[] { "정예병", "투구", "회피", "3" };

        switch (level_kind)
        {
            case "심연의갑옷": dt.Rows.Add(list_x); break;
            case "불멸의벨트": dt.Rows.Add(list_y); break;
            case "심해의망토": dt.Rows.Add(list_z); break;
            case "학살의방패": dt.Rows.Add(list_zz); break;
            case "파멸의갑옷": dt.Rows.Add(list_zzz); break;

            case "승천자갑옷": dt.Rows.Add(list_a); break;
            case "승천자방패": dt.Rows.Add(list_b); break;
            case "승천자투구": dt.Rows.Add(list_c); break;

            case "군단장갑옷": dt.Rows.Add(list_1); break;
            case "군단장방패": dt.Rows.Add(list_2); break;
            case "군단장투구": dt.Rows.Add(list_3); break;

            case "천인장갑옷": dt.Rows.Add(list_4); break;
            case "천인장방패": dt.Rows.Add(list_5); break;
            case "천인장투구": dt.Rows.Add(list_6); break;

            case "백인장갑옷": dt.Rows.Add(list_7); break;
            case "백인장방패": dt.Rows.Add(list_8); break;
            case "백인장투구": dt.Rows.Add(list_9); break;

            case "정예병갑옷": dt.Rows.Add(list_10); break;
            case "정예병방패": dt.Rows.Add(list_11); break;
            case "정예병투구": dt.Rows.Add(list_12); break;
            default:
                dt.Rows.Add(list_x);
                dt.Rows.Add(list_y);
                dt.Rows.Add(list_z);
                dt.Rows.Add(list_zz);
                dt.Rows.Add(list_zzz);
                dt.Rows.Add(list_a);
                dt.Rows.Add(list_b);
                dt.Rows.Add(list_c);
                dt.Rows.Add(list_1);
                dt.Rows.Add(list_2);
                dt.Rows.Add(list_3);
                dt.Rows.Add(list_4);
                dt.Rows.Add(list_5);
                dt.Rows.Add(list_6);
                dt.Rows.Add(list_7);
                dt.Rows.Add(list_8);
                dt.Rows.Add(list_9);
                dt.Rows.Add(list_10);
                dt.Rows.Add(list_11);
                dt.Rows.Add(list_12);
                break;
        }

        return dt;
    }

    public DataTable getAssList(string level_kind)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("LEVEL");
        dt.Columns.Add("KIND");
        dt.Columns.Add("EFFECT");
        dt.Columns.Add("EFFECT_V");

        string[] list_x = new string[] { "심연의", "건틀렛", "반격", "7.84" };
        string[] list_y = new string[] { "불멸의", "피리", "흡혈량", "10.64" };
        string[] list_z = new string[] { "심해의", "보주", "스속", "23.52" };
        string[] list_zz = new string[] { "학살의", "휘장", "반격", "7.84" };
        string[] list_zzz = new string[] { "파멸의", "팔찌", "반격", "7.84" };

        string[] list_a = new string[] { "승천자", "목걸이", "스속", "23.52" };
        string[] list_b = new string[] { "승천자", "반지", "반격", "7.84" };
        string[] list_c = new string[] { "승천자", "귀걸이", "흡혈량", "10.64" };

        string[] list_1 = new string[] { "군단장", "목걸이", "스속", "20.16" };
        string[] list_2 = new string[] { "군단장", "반지", "반격", "6.72" };
        string[] list_3 = new string[] { "군단장", "귀걸이", "흡혈량", "9.12" };

        string[] list_4 = new string[] { "천인장", "목걸이", "스속", "16.8" };
        string[] list_5 = new string[] { "천인장", "반지", "반격", "5.6" };
        string[] list_6 = new string[] { "천인장", "귀걸이", "흡혈량", "7.6" };

        string[] list_7 = new string[] { "백인장", "목걸이", "스속", "13.44" };
        string[] list_8 = new string[] { "백인장", "반지", "반격", "4.48" };
        string[] list_9 = new string[] { "백인장", "귀걸이", "흡혈량", "6.08" };

        string[] list_10 = new string[] { "정예병", "목걸이", "스속", "10.08" };
        string[] list_11 = new string[] { "정예병", "반지", "반격", "3.36" };
        string[] list_12 = new string[] { "정예병", "귀걸이", "흡혈량", "4.56" };

        switch (level_kind)
        {
            case "심연의건틀렛": dt.Rows.Add(list_x); break;
            case "불멸의피리": dt.Rows.Add(list_y); break;
            case "심해의보주": dt.Rows.Add(list_z); break;
            case "학살의휘장": dt.Rows.Add(list_zz); break;
            case "파멸의팔찌": dt.Rows.Add(list_zzz); break;

            case "승천자목걸이": dt.Rows.Add(list_a); break;
            case "승천자반지": dt.Rows.Add(list_b); break;
            case "승천자귀걸이": dt.Rows.Add(list_c); break;

            case "군단장목걸이": dt.Rows.Add(list_1); break;
            case "군단장반지": dt.Rows.Add(list_2); break;
            case "군단장귀걸이": dt.Rows.Add(list_3); break;

            case "천인장목걸이": dt.Rows.Add(list_4); break;
            case "천인장반지": dt.Rows.Add(list_5); break;
            case "천인장귀걸이": dt.Rows.Add(list_6); break;

            case "백인장목걸이": dt.Rows.Add(list_7); break;
            case "백인장반지": dt.Rows.Add(list_8); break;
            case "백인장귀걸이": dt.Rows.Add(list_9); break;

            case "정예병목걸이": dt.Rows.Add(list_10); break;
            case "정예병반지": dt.Rows.Add(list_11); break;
            case "정예병귀걸이": dt.Rows.Add(list_12); break;
            default:
                dt.Rows.Add(list_x);
                dt.Rows.Add(list_y);
                dt.Rows.Add(list_z);
                dt.Rows.Add(list_zz);
                dt.Rows.Add(list_zzz);
                dt.Rows.Add(list_a);
                dt.Rows.Add(list_b);
                dt.Rows.Add(list_c);
                dt.Rows.Add(list_1);
                dt.Rows.Add(list_2);
                dt.Rows.Add(list_3);
                dt.Rows.Add(list_4);
                dt.Rows.Add(list_5);
                dt.Rows.Add(list_6);
                dt.Rows.Add(list_7);
                dt.Rows.Add(list_8);
                dt.Rows.Add(list_9);
                dt.Rows.Add(list_10);
                dt.Rows.Add(list_11);
                dt.Rows.Add(list_12);
                break;
        }

        return dt;
    }

    public DataTable getStoneList(string level_kind)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("LEVEL");
        dt.Columns.Add("KIND");
        dt.Columns.Add("EFFECT_W");
        dt.Columns.Add("EFFECT_W_V");
        dt.Columns.Add("EFFECT_D");
        dt.Columns.Add("EFFECT_D_V");
        dt.Columns.Add("EFFECT_A");
        dt.Columns.Add("EFFECT_A_V");

        string[] list_a = new string[] { "초특급", "홍마석", "치명타", "10.5", "회피", "7", "흡혈량", "10.64" };
        string[] list_b = new string[] { "초특급", "청마석", "공속", "7.84", "막기", "14", "반격", "7.84" };
        string[] list_c = new string[] { "초특급", "녹마석", "공격량", "7.84", "방어량", "6.3", "스속", "23.52" };
        string[] list_d = new string[] { "초특급", "흑마석", "명중", "10.5", "회복량", "21.28", "치유량", "21.28" };
        string[] list_e = new string[] { "초특급", "자마석", "극대화", "15.75", "탄력", "26.25", "피해반사량", "5.32" };

        string[] list_1 = new string[] { "특급", "홍마석", "치명타", "9", "회피", "6", "흡혈량", "9.12" };
        string[] list_2 = new string[] { "특급", "청마석", "공속", "6.72", "막기", "12", "반격", "6.72" };
        string[] list_3 = new string[] { "특급", "녹마석", "공격량", "6.72", "방어량", "5.4", "스속", "20.16" };
        string[] list_4 = new string[] { "특급", "흑마석", "명중", "9", "회복량", "18.24", "치유량", "18.24" };
        string[] list_5 = new string[] { "특급", "자마석", "극대화", "13.5", "탄력", "22.5", "피해반사량", "4.56" };

        string[] list_6 = new string[] { "최상급", "홍마석", "치명타", "7.5", "회피", "5", "흡혈량", "7.6" };
        string[] list_7 = new string[] { "최상급", "청마석", "공속", "5.6", "막기", "10", "반격", "5.6" };
        string[] list_8 = new string[] { "최상급", "녹마석", "공격량", "5.6", "방어량", "4.5", "스속", "16.8" };
        string[] list_9 = new string[] { "최상급", "흑마석", "명중", "7.5", "회복량", "15.2", "치유량", "15.2" };
        string[] list_10 = new string[] { "최상급", "자마석", "극대화", "11.25", "탄력", "18.75", "피해반사량", "3.8" };

        string[] list_11 = new string[] { "상급", "홍마석", "치명타", "6", "회피", "4", "흡혈량", "6.08" };
        string[] list_12 = new string[] { "상급", "청마석", "공속", "4.48", "막기", "8", "반격", "4.48" };
        string[] list_13 = new string[] { "상급", "녹마석", "공격량", "4.48", "방어량", "3.6", "스속", "13.44" };
        string[] list_14 = new string[] { "상급", "흑마석", "명중", "6", "회복량", "12.16", "치유량", "12.16" };
        string[] list_15 = new string[] { "상급", "자마석", "극대화", "9", "탄력", "15", "피해반사량", "3.04" };

        string[] list_16 = new string[] { "중급", "홍마석", "치명타", "4.5", "회피", "3", "흡혈량", "4.56" };
        string[] list_17 = new string[] { "중급", "청마석", "공속", "3.36", "막기", "6", "반격", "3.36" };
        string[] list_18 = new string[] { "중급", "녹마석", "공격량", "3.36", "방어량", "2.7", "스속", "10.08" };
        string[] list_19 = new string[] { "중급", "흑마석", "명중", "4.5", "회복량", "9.12", "치유량", "9.12" };
        string[] list_20 = new string[] { "중급", "자마석", "극대화", "6.75", "탄력", "11.25", "피해반사량", "2.28" };

        switch (level_kind)
        {
            case "초특급홍마석": dt.Rows.Add(list_a); break;
            case "초특급청마석": dt.Rows.Add(list_b); break;
            case "초특급녹마석": dt.Rows.Add(list_c); break;
            case "초특급흑마석": dt.Rows.Add(list_d); break;
            case "초특급자마석": dt.Rows.Add(list_e); break;

            case "특급홍마석": dt.Rows.Add(list_1); break;
            case "특급청마석": dt.Rows.Add(list_2); break;
            case "특급녹마석": dt.Rows.Add(list_3); break;
            case "특급흑마석": dt.Rows.Add(list_4); break;
            case "특급자마석": dt.Rows.Add(list_5); break;

            case "최상급홍마석": dt.Rows.Add(list_6); break;
            case "최상급청마석": dt.Rows.Add(list_7); break;
            case "최상급녹마석": dt.Rows.Add(list_8); break;
            case "최상급흑마석": dt.Rows.Add(list_9); break;
            case "최상급자마석": dt.Rows.Add(list_10); break;

            case "상급홍마석": dt.Rows.Add(list_11); break;
            case "상급청마석": dt.Rows.Add(list_12); break;
            case "상급녹마석": dt.Rows.Add(list_13); break;
            case "상급흑마석": dt.Rows.Add(list_14); break;
            case "상급자마석": dt.Rows.Add(list_15); break;

            case "중급홍마석": dt.Rows.Add(list_16); break;
            case "중급청마석": dt.Rows.Add(list_17); break;
            case "중급녹마석": dt.Rows.Add(list_18); break;
            case "중급흑마석": dt.Rows.Add(list_19); break;
            case "중급자마석": dt.Rows.Add(list_20); break;


            default:
                dt.Rows.Add(list_a);
                dt.Rows.Add(list_b);
                dt.Rows.Add(list_c);
                dt.Rows.Add(list_d);
                dt.Rows.Add(list_e);
                dt.Rows.Add(list_1);
                dt.Rows.Add(list_2);
                dt.Rows.Add(list_3);
                dt.Rows.Add(list_4);
                dt.Rows.Add(list_5);
                dt.Rows.Add(list_6);
                dt.Rows.Add(list_7);
                dt.Rows.Add(list_8);
                dt.Rows.Add(list_9);
                dt.Rows.Add(list_10);
                dt.Rows.Add(list_11);
                dt.Rows.Add(list_12);
                dt.Rows.Add(list_13);
                dt.Rows.Add(list_14);
                dt.Rows.Add(list_15);
                dt.Rows.Add(list_16);
                dt.Rows.Add(list_17);
                dt.Rows.Add(list_18);
                dt.Rows.Add(list_19);
                dt.Rows.Add(list_20);
                break;
        }

        return dt;
    }
}
