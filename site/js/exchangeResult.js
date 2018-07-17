$(document).ready(function(){
	var appId = getRequest('appId') //所有跳转到结果页的页面拼上appId
	$('#top-nav,#mobilenav').load('./head.html')
    var code = getRequest('code');
    var tip = getRequest("tip");
    if(code=='1'){
        $(".result-des").html(tip);
        $(".result-bg").addClass("result-success");
        $(".glyphicon").addClass("glyphicon-ok icon");
        $('.result-des').html('兑换成功')
    }else{
        $(".result-des").html(tip);
        $(".result-bg").addClass("result-fail");        
        $(".glyphicon").addClass("glyphicon-remove icon");
        $('.result-des').html('兑换失败')
    }
    console.log('code---',code);
});