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
using System.Text;
using System.Net.Json;
using System.Collections.Generic;

public partial class maskLocation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }
    }

    protected void btnAction_Click(object sender, EventArgs e)
    {        
        // https://8oi9s0nnth.apigw.ntruss.com/corona19-masks/v1/storesByGeo/json?lat=37.556381&lng=126.837973&m=3000

        string url = "https://8oi9s0nnth.apigw.ntruss.com/corona19-masks/v1/storesByGeo/json?lat="+this.txtLat.Text+"&lng="+this.txtLon.Text+"&m=" + this.txtDistance.Text;

        Type cstype = this.GetType();
        System.Net.WebClient wc = new System.Net.WebClient();
        Stream read = wc.OpenRead(url);
        StreamReader sr = new StreamReader(read, Encoding.UTF8);

        string result = sr.ReadToEnd();

        sr.Close();
        read.Close();

        List<Stores> listValue = getJson(result);      

        //var orderResult = listValue.OrderByDescending(x => x.lat).ThenBy(x => x.lng).ToList();
        var orderResult = listValue.OrderByDescending(x => x.stock_at).ToList();
       
        listValue = (List<Stores>)orderResult;

        if (listValue.Count > 0)
        {
            StringBuilder sbM = new StringBuilder();
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='map'></div><br/>");
            sb.Append("<table border='1' width='100%'>");
            for (int i = 0; i < listValue.Count; i++)
            {
                sb.Append("<tr>");
                sb.Append("<td colspan='2' style='background-color:" + getColor(listValue[i].remain_stat) + ";'>" + string.Format("{0:0}", (i + 1)) + " 번째</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td>주소</td>");
                //sb.Append("<td>" + listValue[i].addr + "&nbsp;<a href='javascript:copyClip(\"" + listValue[i].addr + "\");'>복사</a></td>");
                sb.Append("<td>" + listValue[i].addr + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td>입고시간</td>");
                sb.Append("<td>" + listValue[i].stock_at + "</td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td>약국명</td>");
                sb.Append("<td><a href='javascript:placeMarker(" + listValue[i].lat + "," + listValue[i].lng + ",\"" + "[" + listValue[i].name + "] " + getStat(listValue[i].remain_stat) + "\");'>" + listValue[i].name + "</a></td>");
                sb.Append("</tr>");

                sb.Append("<tr>");
                sb.Append("<td>상태</td>");
                sb.Append("<td>" + getStat(listValue[i].remain_stat) + "</td>");
                sb.Append("</tr>");

                //수량 상태정보를 색상으로 표시할 경우 녹색(100개 이상)/노랑색(30~99개)/빨강색(2~29개)/회색(0~1개)

                string temp = "placeMarker(" + listValue[i].lat + "," + listValue[i].lng + ",\"" + "[" + listValue[i].name + "] " + getStat(listValue[i].remain_stat) + "\")";
                sbM.Append(@"
                    var marker = new google.maps.Marker({
                        position: {lat: " + listValue[i].lat + @", lng: " + listValue[i].lng + @"},
                        label: {
                            text: '" + listValue[i].name + "[" + getStat(listValue[i].remain_stat) + "]" + @"',
                            color: '" + getColor(listValue[i].remain_stat) + @"',
                            fontSize: '13px',
                            fontWeight: 'bold'
                        },
                        map: map,
                        title: '[" + getStat(listValue[i].remain_stat) + @"]'
                    });

                    //marker.setMap(map);
                    google.maps.event.addListener(marker, 'click', function() {                        
                        "+temp+@"
                    });                                        

                    //markerArray.push(marker);                    
                ");
            }

            sb.Append("</table>");

            Response.Clear();
            //Response.Write(sb.ToString());

            Page.ClientScript.RegisterClientScriptBlock(cstype, "tblInfo", sb.ToString());            

            StringBuilder sb2 = new StringBuilder();
            sb2.Append(@"                
                <script>
                    var markerArray = [];
                    var pathArray = [];
                    var map;
                    function initMap() {                                      
                        map = new google.maps.Map(document.getElementById('map'), {
                          center: {lat: "+this.txtLat.Text+@", lng: "+this.txtLon.Text+ @"},
                          gestureHandling: 'greedy',
                          zoom: 16
                        });

                        new google.maps.Marker({
                            position: {lat: " + this.txtLat.Text + @", lng: " + this.txtLon.Text + @"},
                            label: {
                                text: '현재 위치',
                                color: 'black',
                                fontSize: '13px',
                                fontWeight: 'bold'
                            },
                            map: map,
                            title: 'my location'
                        });

                        //google.maps.event.addListener(map, 'click', function(lat, lng, st) {
                        //    placeMarker(lat, lng, st);
                        //});
            ");            
            sb2.Append(sbM.ToString());
            sb2.Append(@"
                    }

                    function placeMarker(lat, lng, st) {
                        var dist = calcDist("+this.txtLat.Text+@", "+this.txtLon.Text+ @", lat, lng);

                        var marker = new google.maps.Marker({
                            position: {lat: lat, lng: lng},
                            map: map
                        });                    
                        
                        var infowindow = new google.maps.InfoWindow({
                            content: st + '(' + dist + ' km)'
                        });

                        var zoomCnt = 16;
                        if (dist <= 0.5) zoomCnt = 16;
                        else if (dist <= 1) zoomCnt = 15;
                        else zoomCnt = 14;

                        var position = new google.maps.LatLng(lat, lng);
                        map.setZoom(zoomCnt);
                        map.setCenter(position);                        

                        infowindow.open(map, marker);

                        for (var i = 0; i < pathArray.length; i++) {
                            pathArray[i].setMap(null);
                        }
                        pathArray.length = 0;

                        var pline = [{lat:" + this.txtLat.Text+@", lng:"+this.txtLon.Text+ @"}, position];
                        var flightPath = new google.maps.Polyline({
                            path:pline,
                            strokeColor: '#0000ff',
                            strokeOpacity: 0.5,
                            strokeWeight:4
                        });
                        flightPath.setMap(map);

                        pathArray.push(flightPath);

                        window.scrollTo(0,0);
                    }

                    function calcDist(lat1, lon1, lat2, lon2) {            
                        var EARTH_R, Rad, radLat1, radLat2, radDist; 
                        var distance, ret;

                        EARTH_R = 6371000.0;

                        Rad 		= Math.PI/180;

                        radLat1 = Rad * lat1;
                        radLat2 = Rad * lat2;
                        radDist = Rad * (lon1 - lon2);
                       
                        distance = Math.sin(radLat1) * Math.sin(radLat2);
                        distance = distance + Math.cos(radLat1) * Math.cos(radLat2) * Math.cos(radDist);

                        ret 		 = EARTH_R * Math.acos(distance);
            						
                        //var rtn = Math.round(Math.round(ret) / 1000);
                        var rtn = Math.round(ret) / 1000;
                        
                        return rtn;
                    }
                </script>
                <script src='https://maps.googleapis.com/maps/api/js?key=AIzaSyDXKnsx4UtkVAaWb7kcZlS4_b0V_E_OJME&callback=initMap' async defer></script>  
            ");
            //Response.Write(sb2.ToString());

            
            Page.ClientScript.RegisterClientScriptBlock(cstype, "googleScript", sb2.ToString());

            
        }
        else
        {
            Response.Write("<font color='red'>[해당 거리내에 약국 없음]</font><br/><br/>");
        }
    }

    protected string getStat(string stat)
    {
        string result = "";
        switch (stat)
        {
            case "plenty": result = "100개 이상"; break;
            case "some": result = "30개 이상 100개 미만"; break;
            case "few": result = "2개 이상 30개 미만"; break;
            case "empty": result = "1개 이하"; break;
            default: result = "알수없음"; break;
        }

        return result;
    }

    protected string getColor(string stat)
    {
        //수량 상태정보를 색상으로 표시할 경우 녹색(100개 이상)/노랑색(30~99개)/빨강색(2~29개)/회색(0~1개)
        string result = "";
        switch (stat)
        {
            case "plenty": result = "green"; break;
            case "some": result = "yellow"; break;
            case "few": result = "red"; break;
            case "empty": result = "gray"; break;
            default: result = "알수없음"; break;
        }

        return result;
    }

    protected List<Stores> getJson(string str)
    {        
        JsonTextParser parser = new JsonTextParser();
        JsonObject obj = parser.Parse(str);

        //Response.Write(obj.ToString());

        JsonUtility.GenerateIndentedJsonText = false;

        //Response.Write(obj.ToString());

        List<Stores> listStores = new List<Stores>();

        double tot = 0;        
        JsonObjectCollection collection = new JsonObjectCollection();        
        foreach (JsonObject field in obj as JsonObjectCollection)
        {
            string name = field.Name;
            string value = field.GetValue().ToString();
            string type = field.GetValue().GetType().Name;

            if (name == "count")
            {
                tot = Double.Parse(field.GetValue().ToString());
            }

            if (name == "stores")
            {
                List<JsonObject> tempList = ((List<JsonObject>)field.GetValue());

                //Response.Write(tempList.Count.ToString());
                for (int i = 0; i < tempList.Count; i++)
                {
                    Stores stores = new Stores();
                    foreach (JsonObject o in tempList[i] as JsonObjectCollection)
                    {                        
                        switch (o.Name)
                        {
                            case "addr": stores.addr = o.GetValue().ToString(); break;
                            case "code": stores.code = o.GetValue().ToString(); break;
                            case "created_at": stores.created_at = (o.GetValue() == null) ? "" : o.GetValue().ToString(); break;
                            case "lat": stores.lat = o.GetValue().ToString(); break;
                            case "lng": stores.lng = o.GetValue().ToString(); break;
                            case "name": stores.name = o.GetValue().ToString(); break;
                            case "remain_stat": stores.remain_stat = (o.GetValue() == null) ? "" : o.GetValue().ToString(); break;
                            case "stock_at": stores.stock_at = (o.GetValue() == null) ? "" : o.GetValue().ToString(); break;
                            case "type": stores.type = (o.GetValue() == null) ? "" : o.GetValue().ToString(); break;
                        }                        
                    }
                    listStores.Add(stores);
                }
            }            
        }

        return listStores;
    }

    protected void btnReload_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
}
