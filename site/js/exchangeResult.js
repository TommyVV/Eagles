$(document).ready(function() {
    var appId = getRequest('appId') //所有跳转到结果页的页面拼上appId
    $('#top-nav,#mobilenav').load('./head.html')
    $('#footer').load('footer.html')
    var code = getRequest('code');
    var tip = getRequest("tip");
    if (code == '1') {
        $(".result-des").html(tip);
        $(".result-bg").addClass("result-success");
        $(".glyphicon").addClass("glyphicon-ok icon");
    } else {
        $(".result-des").html(tip);
        $(".result-bg").addClass("result-fail");
        $(".glyphicon").addClass("glyphicon-remove icon");
    }
    console.log('code---', code);
});