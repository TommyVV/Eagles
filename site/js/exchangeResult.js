$(document).ready(function(){
	if(!localStorage.getItem('token')){
		window.location.href = "login.html"
	}
	$('#top-nav,#mobilenav').load('./head.html')
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