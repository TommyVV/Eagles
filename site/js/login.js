var appId = getRequest('appId');
var testId = getRequest('testId');
var testlist = getRequest('datalist');
console.log(testlist)
var newsIds = getRequest('NewsId'); //获取来源d的新闻id
var onurl = getRequest('onurl')
    //var appId = 10000000
navbar(appId)

function navbar(appId) {
    $.ajax({
        type: "post",
        data: {
            "AppId": appId
        },
        url: DOMAIN + "/api/AppMenu/GetAppMenu",
        dataType: "json",
        success: function(res) {
            var data = res.Result;
            if (res.Code == 00) {
                $('#login_logo').attr("src", data.LogoUrl)
            }
        }
    });
}

$(".flag-area").click(function() {
    window.location.href = "forgotpassword.html?appId=" + appId
});

$('.login-zc').on('click', function(e) {
    if (onurl == undefined) {
        window.location.href = "signup.html?appId=" + appId
    } else {
        window.location.href = "signup.html?appId=" + appId + "&onurl=" + onurl;
    }

})
$(".flag-area").click(function() {
    if ($(".pub-flag").hasClass("select")) {
        $(".pub-flag")
            .attr("src", "icons/sel_no@2x.png")
            .removeClass("select");
    } else {
        $(".pub-flag")
            .attr("src", "icons/sel_yes@2x.png")
            .addClass("select");
    }
});
//登录
$('.btn-login').on('click', function(e) {
    e.preventDefault();
    let password = $('#inputPassword').val(); //密码
    let account = $('#inputUser').val(); //用户名
    var pubFlag = $(".pub-flag").hasClass("select");
    //let captcha = $('#inputCaptcha').val();
    if (!account) {
        bootoast({
            message: '请输入账号',
            type: 'warning',
            position: 'toast-top-center',
            timeout: 1
        });
        return;
    } else if (!password) {
        bootoast({
            message: '请输入密码',
            type: 'warning',
            position: 'toast-top-center',
            timeout: 1
        });
        return;
    }
    var validFlage =$("#verCode").css("display")=="block";
    if(validFlage&&(!$("#inputCaptcha").val())){
        bootoast({
            message: '请输入验证码',
            type: 'warning',
            position: 'toast-top-center',
            timeout: 1
        });
        return;
    }
    var data = {
        "Phone": account,
        "UserPwd": password,
        "IsRememberPwd": 1,
        "AppId": appId
    };
    if(validFlage){
        data.ValidCode=$("#inputCaptcha").val();
        data.Seq =$('#inputCaptcha').attr('CodeSeq');
    }
    //var hash = hex_md5(password);
    $.ajax({
        type: "post",
        data: data,
        url: DOMAIN + "/api/User/Login",
        dataType: "json",
        success: function(res) {
            var data = res.Result;
            if (res.Code == 00) {
                var f = localStorage.setItem("token", data.Token); //存储token
                localStorage.setItem("userId", data.UserId); //用户ID
                localStorage.setItem("IsInternalUser", data.IsInternalUser); //是否是内部用户
                localStorage.setItem("IsVerifyCode", data.IsVerifyCode); //是否需要验证码
                localStorage.setItem("TokenExpTime", data.TokenExpTime); //过期时间

                if (data.IsInternalUser == "0") {
                    window.location.href = 'index.html?moduleType=0&appId=' + appId + '';
                    return false;
                } else {
                    //登陆成功页面跳转地址
                    var prevLink = document.referrer;
                    if ($.trim(prevLink) == '') {
                        location.href = 'mine.html?appId=' + appId + '';
                    } else {
                        if (prevLink.indexOf('signup.html') != -1) { //来自注册页面
                            location.href = 'signup.html?appId=' + appId + '';
                        } else if (prevLink.indexOf('login.html') != -1) {
                            location.href = 'mine.html?appId=' + appId + '';
                        } else if (prevLink.indexOf('partyLearning_detail.html') != -1) { //来自注册页面
                            location.href = 'partyLearning_detail.html?appId=' + appId + '&datalist=' +
                                testlist + '&NewsId=' + newsIds + '&testId=' + testId + '';
                        } else if (prevLink.indexOf('forgotpassword.html') != -1) {
                            location.href = 'mine.html?appId=' + appId + '';
                        } else {
                            location.href = prevLink;
                        }
                    }
                }
            }else if(res.Code==100){
                bootoast({
                    message: '' + res.Message + '',
                    type: 'warning',
                    position: 'toast-top-center',
                    timeout: 3
                });
                $("#verCode").css("display","block");
            } else {
                bootoast({
                    message: '' + res.Message + '',
                    type: 'warning',
                    position: 'toast-top-center',
                    timeout: 3
                });
            }
        }
    })
});
$('.eyeFlag').on('click', function(e) {
    var src = $(this).attr('src');
    if (src == 'icons/close.png') {
        $(this).attr('src', 'icons/open.png');
        $("#inputPassword").attr('type', "text");
    } else {
        $(this).attr('src', 'icons/close.png');
        $("#inputPassword").attr('type', "password");
    }
});
var timer;
$(".btn-cap").click(function() {
    var phone = $('#inputUser').val();
    if (phone == "" || phone == undefined || phone == null || phone.length != 11) {
        bootoast({
            message: '请输入有效的手机号！',
            type: 'warning',
            position: 'toast-top-center',
            timeout: 3
        });
        return false;
    }
    if ($(".btn-cap").attr("disabled") == "disabled") {
        return;
    }
    $(this).attr("disabled", "disabled");
    $.ajax({
        type: "POST",
        url: DOMAIN + "/api/Register/GetValidateCode",
        data: {
            "Phone": phone,
            "AppId": appId,
            "type":1
        },
        success: function(res) {
            if (res.Code == 00) {
                $('#inputCaptcha').attr('placeholder', '验证码序号为' + res.Result.CodeSeq + '');
                $('#inputCaptcha').attr('CodeSeq', res.Result.CodeSeq);
                var time = 60
                $('.btn-cap').text('' + time + 's重新获取');

                $('.btn-cap').css("background", "#D6D6D6")
                $(this).attr("disabled", "disabled");
                timer = setInterval(function() {
                        time--;
                        $('.btn-cap').text('' + time + 's重新获取');
                        if (time <= 0) {
                            clearInterval(timer);
                            $(".btn-cap").text('获取验证码');
                            $('.btn-cap').css("background", "#EE2F2F")
                            $(".btn-cap").removeAttr("disabled");
                        }
                    }, 1000)
                    //$('.btn-cap').removeAttr("disabled");
                return
            } else {
                bootoast({
                    message: '' + res.Message + '',
                    type: 'warning',
                    position: 'toast-top-center',
                    timeout: 3
                });
                $('.btn-cap').removeAttr("disabled");
            }
        }
    });
})