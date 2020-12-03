var compassID = null; // 나침반 객체
var geoID = null; // 위치 측정 객체
 
$("document").ready(function(){ 
    
    document.addEventListener('deviceready',function(){
        
        $("#notok").css("display", "none");
        $("#ok").css("display", "block");
    
        /*방위측정 객체 생성(한번만 측정)
        //navegator.compass.gecurrentHeading(성공시 호출 함수, 실패시 호출 함수)
        compassId = navigator.compass.getCurrentHeading(
        compassSuccess, onFail);
        */
        
        //방위 실시간 측정
        var option = {frequency:1000};
        navigator.compass.watchHeading(compassSuccess, onFail, option);
        
        //현재 위치 측정
        navigator.geolocation.getCurrentPosition(function(pos){
            console.log(pos);
            //받은 포지션 정보로 지도 표시
            mapshow(pos);
            
        }, onFail);
    
    });
});
 
//지도 생성
function mapshow(pos){
    //위치 정보
    var lati;
    var longi;
    if(pos.coords.latitude == null || pos.coords.longitude ==null){
        lati = 37.5760076;
        longi = 126.974728;
    }else{
        lati = pos.coords.latitude;
        longi = pos.coords.longitude;
    }
    
    //구글 맵 표현
    // 센터설정
    var mapCenter =  new google.maps.LatLng(lati,longi);
    //맵 옵션
    var option = {
        center : mapCenter,
        zoom : 17,
        mapTypeId : google.maps.MapTypeId.ROADMAP 
    };
    //맵 표현
    var canvas = document.getElementById("googleMap");
    var map = new google.maps.Map(canvas, option);
    
    //마커옵션
    var marker = new google.maps.Marker({
        position: mapCenter,
        title: 'my position'
    });
    
    //마커 표현
    marker.setMap(map);
    
}
 
 
//방위측정 성공시...
function compassSuccess(data){
    // 0 ~ 359.99(north = 0/ east = 90/ south = 180/ west = 270)
    console.log(data.magneticHeading);
    var direction = data.magneticHeading;
    
    if((direction>315 || direction<=45)){
        $("#compass").html("North");
    }else if(direction>45 && direction<=135){
        $("#compass").html("East");
    }else if(direction>135 && direction<=225){
        $("#compass").html("South");
    }else{
        $("#compass").html("West");
    }
    
    
    //$("#compass").html(data.magneticHeading);
}
//실패시...
function onFail(error){
    console.log(error);
    alert("방위 호출 실패");
}

