$(document).ready(function() {
    var appId = getRequest('appId') //所有跳转到结果页的页面拼上appId
    $('#top-nav,#mobilenav').load('./head.html')
    $('#footer').load('footer.html')
    var code = getRequest('code');
    var tip = getRequest("tip");
    var cbUrl=getRequest("cb");
    if (code == '1') {
        $(".result-des").html(tip);
        $("img").attr('src', 'icons/correct@2x.png');
    } else {
        $(".result-des").html(tip);
        $("img").attr('src', 'icons/mistake@2x.png');
    }
    if(cbUrl){
        var time = 5;
        $(".df_don").html(`<span id="result_psf">5</span>秒后跳转`);
        var timer=setInterval(() => {
            time--;
            console.log('time----',time);
            $("#result_psf").html(time);
            if(time==0){
                clearInterval(timer);
                window.location.href=cbUrl+"?appId="+appId;
            }
        }, 1000);
    }
    console.log('code---', code);
});