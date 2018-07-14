$(document).ready(function() {
	$('#top-nav,#mobilenav').load('head.html');
})

//var appId = getRequest('appId');
var appId = 10000000
navbar(appId)
function navbar(appId) {
	$.ajax({
		type: "post",
		data: {
			"AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/AppMenu/GetAppMenu",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			if(res.Code == 00) {
				$('#login_logo').attr("src",data.LogoUrl)
			}
		}
	});
}
//登录
$('.btn-login').on('click', function(e) {
	e.preventDefault();
	let password = $('#inputPassword').val(); //密码
	let account = $('#inputUser').val(); //用户名
	//let captcha = $('#inputCaptcha').val();
	if(!account) {
		alert('请输入账号');
		return;
	} else if(!password) {
		alert('请输入密码');
		return;
	}
	//var hash = hex_md5(password);
	$.ajax({
		type: "post",
		data: {
			"Phone": account,
			"UserPwd": password,
			"AppId": 10000000
		},
		url: "http://51service.xyz/Eagles/api/User/Login",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			if(res.Code == 00) {
				var f = localStorage.setItem("token", data.Token); //存储token
				localStorage.setItem("userId", data.UserId); //用户ID
				localStorage.setItem("IsInternalUser", data.IsInternalUser); //是否是内部用户
				localStorage.setItem("IsVerifyCode", data.IsVerifyCode); //是否需要验证码
				//登陆成功页面跳转地址
				var prevLink = document.referrer;
				if($.trim(prevLink) == '') {
					parent.location.href = 'mine.html?appId=' + appId + '';
				} else {
					//					if(prevLink.indexOf('http://51service.xyz/')==-1){	//来自其它站点
					//						parent.location.href = 'mine.html?appId='+appId+''
					//					}
					if(prevLink.indexOf('signup.html') != -1) { //来自注册页面
						parent.location.href = 'signup.html';
					}
					parent.location.href = prevLink;
				}

			}
		}
	})
})
