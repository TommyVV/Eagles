$(document).ready(function(){
    var code = getRequest('code');
    if(code=='1'){
        $(".result-des").html('恭喜你，换购成功');
        $(".result-bg").addClass("result-success");
        $(".glyphicon").addClass("glyphicon-ok icon");
    }else{
        $(".result-des").html('对不起，换购失败！');
        $(".result-bg").addClass("result-fail");        
        $(".glyphicon").addClass("glyphicon-remove icon");
    }
    console.log('code---',code);
});