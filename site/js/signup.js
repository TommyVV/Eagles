const DOMAIN = 'http://www.51service.xyz/Eagles/swagger/ui/index';

$('.btn-signup').on('click', function (e) {
    e.preventDefault();

    let password = $('#inputPassword').val();
    let account = $('#inputUser').val();
    let captcha = $('#inputCaptcha').val();
    if (!account) {
        alert('请输入手机号');
        return;
    } else if (!password) {
        alert('请输入密码');
        return;
    } else if (!captcha) {
        alert('请输入验证码');
        return;
    }
    $.ajax({
        type: "post",
		data: {
		  "Phone": account,
		  "Pwd": hex_md5(password),
		  "ValidCode": 0,
		  "Seq": 0,
		  "Token": "string",
		  "AppId": 0
		},
		url: "http://51service.xyz/Eagles/api/User/Register",
		dataType: "json",
        success:function(res){
        	var data=res.Result;
            if(res.Code == 00){
            	loginIn($('#inputUser').val(),hex_md5($('#inputPassword').val()))//调登陆接口
              	

            }
        }
	})
})
var timer;
$(".btn-cap").click(function(){
	$(this).attr("disabled","disabled");
	var phone = $('#inputUser').val();
    if (phone == ""|| phone == undefined || phone == null )
    {
    	alert("请输入有效的手机号！");
        return;
    } 
    $.ajax({
           type: "POST",
           url: 'http://51service.xyz/Eagles/api/User/Register',
           data: {
			  "Phone": phone,
			},
           success: function (data) {
               if(data == '1'){
					var time=60
					//$('.btn-cap').text()
					timer = setInterval(function() {
	                  time--;
	                  if(time <= 0) {
	                    clearInterval(timer);
	                    time = 60;
	                    $(".btn-cap").text('获取验证码');
	                    $(".btn-cap").removeAttr("disabled");
	                  }
	                  $('.btn-cap').text(time)
	               }, 1000)
					return
				}
           }
     });
})
function loginIn(account,hash){
	$.ajax({
        type: "post",
		data: {
			  "Phone": account,
			  "UserPwd": hash,
			  "VerifyCode": "string",
			  "Token": "string",
			  "AppId": 10000000
		},
		url: "http://51service.xyz/Eagles/api/User/Login",
		dataType: "json",
        success:function(res){
        	var data=res.Result;
            if(res.Code == 00){
            	localStorage.setItem("token",data.Token);//存储token
              	localStorage.setItem("userId",data.UserId); //用户ID
              	localStorage.setItem("IsInternalUser ",data.IsInternalUser); //是否是内部用户
              	localStorage.setItem("IsVerifyCode  ",data.IsVerifyCode); //是否需要验证码
              	//登陆成功页面跳转地址
            	var prevLink = document.referrer;
				if($.trim(prevLink)==''){
					location.href = 'index.html';
				}else{
					if(prevLink.indexOf('http://51service.xyz/')==-1){	//来自其它站点
						location.href = 'index.html';
					}
					location.href = prevLink;
				}

            }
        }
	})
}
clearInterval(timer);//清楚定时器