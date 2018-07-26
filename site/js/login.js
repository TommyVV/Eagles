var appId = getRequest('appId');
var testId = getRequest('testId');
var testlist = getRequest('testlist');
var newsIds = getRequest('NewsId'); //获取来源d的新闻id
var onurl=getRequest('onurl')
//var appId = 10000000
navbar(appId)

function navbar(appId) {
    $.ajax({
        type: "post",
        data: {
            "AppId": appId
        },
        url: "http://51service.xyz/Eagles/api/AppMenu/GetAppMenu",
        dataType: "json",
        success: function (res) {
            var data = res.Result;
            if (res.Code == 00) {
                $('#login_logo').attr("src", data.LogoUrl)
            }
        }
    });
}
console.log(getRequest('onurl'))
$('.login-zc').on('click', function (e) {
	if(onurl==undefined){
		window.location.href = "signup.html?appId=" + appId
	}else{
		window.location.href = "signup.html?appId=" + appId+"&onurl="+onurl;
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
$('.btn-login').on('click', function (e) {
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
    //var hash = hex_md5(password);
    $.ajax({
        type: "post",
        data: {
            "Phone": account,
            "UserPwd": password,
			"IsRememberPwd":pubFlag == true ? 1 : 0,
            "AppId": appId
        },
        url: "http://51service.xyz/Eagles/api/User/Login",
        dataType: "json",
        success: function (res) {
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
                }else{
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
							location.href = 'partyLearning_detail.html?appId=' + appId + '&testlist=' +
								testlist + '&NewsId=' + newsIds + '&testId=' + testId + '';
						} else {
							location.href = prevLink;
						}
					}
				}
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
})
