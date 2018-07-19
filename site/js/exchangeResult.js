$(document).ready(function() {
    var appId = getRequest('appId') //所有跳转到结果页的页面拼上appId
    $('#top-nav,#mobilenav').load('./head.html')
    $('#footer').load('footer.html')
    var code = getRequest('code');
    var tip = getRequest("tip");
    if (code == '1') {
        $(".result-des").html(tip);
        $("img").attr('src', 'icons/correct@2x.png');
    } else {
        $(".result-des").html(tip);
        $("img").attr('src', 'icons/mistake@2x.png');
    }
    console.log('code---', code);
});