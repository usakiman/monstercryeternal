using System;
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
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Module의 요약 설명입니다.
/// </summary>
public class Module
{
	public Module()
	{
		//
		// TODO: 여기에 생성자 논리를 추가합니다.
		//
	}

    public DataSet getList(string name, string type, string kind, string kind2)
    {
        DataSet ds = new DataSet();

        string where = "";

        if (name != "")
        {
            where = " where cardname = '" + name + "' ";
        }

        switch (type)
        {
            case "SIMUL":
                if (where == "")
                    where += " where card_etc <> 'NON' ";
                else
                    where += " and card_etc <> 'NON' ";
                break;
            case "PHOTO": break;
        }

        if (kind != "")
        {
            if (where == "")
                where += " where cardlevel = '" + kind + "' ";
            else
                where += " and cardlevel = '" + kind + "' ";

        }

        StringBuilder sb = new StringBuilder();
        sb.Append(@"
            select * from card_info
            :where
            order by 
            case 
	            when cardlevel = 'SSS+' then 0
	            when cardlevel = 'SSS' then 1
	            when cardlevel = 'SS' then 2
	            when cardlevel = 'S' then 3
	            when cardlevel = 'A' then 4
	            when cardlevel = 'B' then 5
	            when cardlevel = 'C' then 6	            
            else 99 end
            , cardname
        ");

        string sql = sb.ToString().Replace(":where", where);

        SqlDataBase db = new SqlDataBase();
        ds = db.sqlReaderQuery(sql, null, "TEXT");

        return ds;
    }

    public string mergeAccess_Log()
    {
        string result = "";
        string ymd = DateTime.Now.ToShortDateString().Replace("-", "");

        string updateQry = "update access_log set cnt = cnt + 1 where ymd = '"+ymd+"' ";
        string insertQry = "insert into access_log select '"+ymd+"' as ymd, 1";

        SqlDataBase db = new SqlDataBase();

        try
        {
            if (db.sqlExeQuery(updateQry, null, "TEXT") == 0)
            {
                result = db.sqlExeQuery(insertQry, null, "TEXT").ToString();
            }            
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }

        return result;
    }

    public DataSet getAccess_Log(string ymd)
    {
        DataSet ds = new DataSet();        

        StringBuilder sb = new StringBuilder();
        sb.Append(@"
            select * from access_log where ymd = '"+ymd+@"'            
        ");
        
        SqlDataBase db = new SqlDataBase();
        ds = db.sqlReaderQuery(sb.ToString(), null, "TEXT");        

        return ds;
    }

    public string insertAccess_List(string ip)
    {
        string result = "";
        string ymd = DateTime.Now.ToShortDateString().Replace("-", "");
        
        string insertQry = "insert into access_list select '" + ip + "' as ip, getdate()";

        SqlDataBase db = new SqlDataBase();

        try
        {            
            result = db.sqlExeQuery(insertQry, null, "TEXT").ToString();            
        }
        catch (Exception ex)
        {
            result = ex.Message;
        }

        return result;
    }

    public DataSet getAccess_List(string ymd)
    {
        DataSet ds = new DataSet();

        StringBuilder sb = new StringBuilder();
        sb.Append(@"
            select * from access_list where REPLACE(CONVERT(varchar(10), cre_date, 120), '-', '') = '"+ymd+@"' order by seq desc            
        ");

        SqlDataBase db = new SqlDataBase();
        ds = db.sqlReaderQuery(sb.ToString(), null, "TEXT");

        return ds;
    }
}
