using System;
using System.Collections;
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

public partial class insert_data_card : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
                                   
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        CardInfo ci = new CardInfo();

        if (this.txtPwd.Text == "90367318")
        {
            Response.Write(ci.insertCard(
                this.txtCardName.Text,
                this.txtCardLevel.Text,
                this.txtCardType.Text,
                this.txtCardRace.Text,
                this.txtActPower.Text,
                this.txtActive1.Text,
                this.txtActive1_Waiting.Text,
                this.txtActive2.Text,
                this.txtActive2_Waiting.Text,
                this.txtPassive1.Text,
                this.txtPassive2.Text,
                this.txtMainEffect.Text,
                this.txtCardImage.Text
            ));
        }
        else
            Response.Write("no");
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        CardInfo ci = new CardInfo();

        if (this.txtPwd.Text == "90367318")
        {
            Response.Write(ci.updateCard(this.txtCardName.Text, this.txtMainEffect.Text));
        }
        else
            Response.Write("no");
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CardInfo ci = new CardInfo();

        if (this.txtPwd.Text == "90367318")
        {
            Response.Write(ci.deleteCard(this.txtCardName.Text));
        }
        else
            Response.Write("no");
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        this.lblAccess_Log.Text = "";
        this.lblAccess_List.Text = "";

        string ymd = DateTime.Now.ToShortDateString().Replace("-", "");

        if (txtYmd.Text != "") ymd = txtYmd.Text;
        
        Module m = new Module();
        DataSet ds = m.getAccess_Log(ymd);

        if (ds.Tables[0].Rows.Count > 0)
        {
            this.lblAccess_Log.Text = "접속수 : ("+ds.Tables[0].Rows[0]["CNT"].ToString()+") ";
        }

        ds = m.getAccess_List(ymd);

        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                this.lblAccess_List.Text += "IP : (" + ds.Tables[0].Rows[i]["USER_IP"].ToString() + "), 시간 : (" + ds.Tables[0].Rows[i]["CRE_DATE"].ToString() + ")<br/>";
            }
        }
    }
}
