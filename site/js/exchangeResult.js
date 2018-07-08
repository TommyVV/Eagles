$(document).ready(function(){
    var code = getRequest('code');
    var tip = getRequest("tip");
    if(code=='1'){
        $(".result-des").html(tip);
        $(".result-bg").addClass("result-success");
        $(".glyphicon").addClass("glyphicon-ok icon");
    }else{
        $(".result-des").html(tip);
        $(".result-bg").addClass("result-fail");        
        $(".glyphicon").addClass("glyphicon-remove icon");
    }
    console.log('code---',code);
});