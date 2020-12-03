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

/// <summary>
/// Stores의 요약 설명입니다.
/// </summary>
public class Stores
{
    public string addr { get; set; }
    public string code { get; set; }
    public string created_at { get; set; }
    public string lat { get; set; }
    public string lng { get; set; }
    public string name { get; set; }
    public string remain_stat { get; set; }
    public string stock_at { get; set; }
    public string type { get; set; }

	public Stores()
	{
		
	}
}
