var appId = getRequest('appId');
console.log(getRequest('onurl'))
var onurl=getRequest('onurl')
$('.btn-signup').on('click', function(e) {
	e.preventDefault();
	let password = $('#inputPassword').val();
	let account = $('#inputUser').val();
	let captcha = $('#inputCaptcha').val();
	if(!account) {
		bootoast({
			message: '请输入手机号',
			type: 'warning',
			position: 'toast-top-center',
			timeout: 3
		});
		alert('请输入手机号');
		return;
	} else if(!password) {
		bootoast({
			message: '请输入密码',
			type: 'warning',
			position: 'toast-top-center',
			timeout: 3
		});
		return;
	} else if(!captcha) {
		bootoast({
			message: '请输入验证码',
			type: 'warning',
			position: 'toast-top-center',
			timeout: 3
		});
		return false;
	}
	$.ajax({
		type: "post",
		data: {
			"Phone": account,
			"NewPwd": password,
			"ValidCode": captcha,
			"Seq": $('#inputCaptcha').attr('CodeSeq'),
			"AppId": appId
		},
		url: DOMAIN + "/api/User/ForgetPwd",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			if(res.Code == 00) {
                bootoast({
					message: '修改成功，3秒后跳转到登录页面。',
					type: 'warning',
					position: 'toast-top-center',
					timeout: 3
				});
                setTimeout(() => {
                    window.location="login.html?appId="+appId;    
                }, 3000);
				
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
var timer;
$(".btn-cap").click(function() {
	var phone = $('#inputUser').val();
	if(phone == "" || phone == undefined || phone == null||phone.length != 11) {
		bootoast({
			message: '请输入有效的手机号！',
			type: 'warning',
			position: 'toast-top-center',
			timeout: 3
		});
		return false;
	}
	$(this).attr("disabled", "disabled");
	$.ajax({
		type: "POST",
		url: DOMAIN + "/api/Register/GetValidateCode",
		data: {
			"Phone": phone,
			"AppId": appId
		},
		success: function(res) {
			if(res.Code == 00) {
				$('#inputCaptcha').attr('placeholder', '请输入序号为' + res.Result.CodeSeq + '的验证码');
				$('#inputCaptcha').attr('CodeSeq', res.Result.CodeSeq);
				var time = 60
				$('.btn-cap').text(''+time+'s重新获取');
				
				$('.btn-cap').css("background","#D6D6D6")
				$(this).attr("disabled", "disabled");
				timer = setInterval(function() {
					time--;
					$('.btn-cap').text(''+time+'s重新获取');
					if(time <= 0) {
						clearInterval(timer);
						time = 60;
						$(".btn-cap").text('获取验证码');
						$('.btn-cap').css("background","#EE2F2F")
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

function loginIn(account, UserPwd) {
	$.ajax({
		type: "post",
		data: {
			"Phone": account,
			"UserPwd": UserPwd,
			"IsRememberPwd":1,
			"AppId": appId
		},
		url: DOMAIN + "/api/User/Login",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			if(res.Code == 00) {
				localStorage.setItem("token", data.Token); //存储token
				localStorage.setItem("userId", data.UserId); //用户ID
				localStorage.setItem("IsInternalUser", data.IsInternalUser); //是否是内部用户
				localStorage.setItem("IsVerifyCode", data.IsVerifyCode); //是否需要验证码
				localStorage.setItem("TokenExpTime", data.TokenExpTime); //过期时间
				
					if(onurl!=undefined){
						location.href =onurl+'?appId='+appId
						return false;
					}else{
						if(localStorage.getItem('IsInternalUser')==1){
							location.href = 'mine.html?appId=' + appId + '';
							return false;
						}else{
							location.href = 'index.html?moduleType=0&appId=' + appId
							return false;
						}
					}

			}
		}
	})
}
clearInterval(timer); //清除定时器