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
using System.IO;

public partial class PhotoGuideList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Display();
        }
    }

    private void Display()
    {
        Module md = new Module();
        DataSet ds = md.getList("", "PHOTO", ddlPhotoType.SelectedValue, "");

        this.DataList1.DataSource = ds.Tables[0];
        this.DataList1.DataBind();        
        
        int normalEpicCnt = 0;        
        int normalSSSCnt = 0;        
        int normalSSCnt = 0;        
        int normalSCnt = 0;
        int normalACnt = 0;
        int normalBCnt = 0;
        int normalCCnt = 0;
        DataSet dsCnt = md.getList("", "PHOTO", "", "");

        for (int i = 0; i < dsCnt.Tables[0].Rows.Count; i++)
        {
            switch (dsCnt.Tables[0].Rows[i]["CARDLEVEL"].ToString())
            {                
                case "SSS+": normalEpicCnt++; break;
                case "SSS": normalSSSCnt++; break;
                case "SS": normalSSCnt++; break;
                case "S": normalSCnt++; break;
                case "A": normalACnt++; break;
                case "B": normalBCnt++; break;
                case "C": normalCCnt++; break;                                
            }
        }

        this.lblMsg.Text = "총 카드수 (" + dsCnt.Tables[0].Rows.Count.ToString() + ") 개 <br/>";
        this.lblMsg.Text += "[SSS+] (" + normalEpicCnt.ToString() + ") 개 <br/>";        
        this.lblMsg.Text += "[SSS] (" + normalSSSCnt.ToString() + ") 개 <br/>";
        this.lblMsg.Text += "[SS] (" + normalSSCnt.ToString() + ") 개 <br/>";
        this.lblMsg.Text += "[S] (" + normalSCnt.ToString() + ") 개 <br/>";
        this.lblMsg.Text += "[A] (" + normalACnt.ToString() + ") 개 <br/>";
        this.lblMsg.Text += "[B] (" + normalBCnt.ToString() + ") 개 <br/>";
        this.lblMsg.Text += "[C] (" + normalCCnt.ToString() + ") 개";            
    }

    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Image img = ((Image)e.Item.FindControl("imgPhoto"));
            Label lblcardname = ((Label)e.Item.FindControl("lblCardName"));
            Label lblcardinfo = ((Label)e.Item.FindControl("lblCardInfo"));
            Label lblcardinfo2 = ((Label)e.Item.FindControl("lblCardInfo2"));

            DataRowView dr = ((DataRowView)e.Item.DataItem);
            string card_level = dr["cardlevel"].ToString();
            string card_name = dr["cardname"].ToString();
            string card_passive1 = dr["cardactive1"].ToString();
            string card_passive2 = dr["cardactive2"].ToString();
            string lblcardmaininfo = dr["maineffect"].ToString();            
            

            #region 기본정보
            switch (card_level)
            {                
                case "SSS+": card_level = "SSS+"; break;
                case "SSS": card_level = "SSS"; break;
                case "SS": card_level = "SS"; break;
                case "S": card_level = "S"; break;
                case "A": card_level = "A"; break;
                case "B": card_level = "B"; break;
                case "C": card_level = "C"; break;                
            }

            lblcardname.Text = card_name + " [" + card_level + "]";
            lblcardinfo.Text = card_passive1;
            if (card_passive2 != "") lblcardinfo.Text += "," + card_passive2;
            lblcardinfo2.Text = "대표카드효과 : " + lblcardmaininfo;

            if (File.Exists(Server.MapPath("Files/" + card_name + ".jpg")))
            {
                img.ImageUrl = "Files/" + card_name + ".jpg";
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
            img.Attributes.Add("style", "width:100%");
            #endregion            
        }
    }

    protected void ddlPhotoType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Display();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
}
