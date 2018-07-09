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
		  "UserPwd": password,
		  "ValidCode":captcha,
		  "Seq": $('#inputCaptcha').attr('CodeSeq'),
		  "AppId": 10000000
		},
		url: "http://51service.xyz/Eagles/api/Register/Register",
		dataType: "json",
        success:function(res){
        	var data=res.Result;
            if(res.Code == 00){
            		loginIn($('#inputUser').val(),$('#inputPassword').val())//调登陆接口
            }else{
            	bootoast({
				    message: '注册失败',
					 type: 'warning',
					 position:'right-bottom',
					 timeout:2
				  });
            }
        }
	})
})
console.log($(".btn-cap").text())
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
           url: 'http://51service.xyz/Eagles/api/Register/GetValidateCode',
           data: {
			  "Phone": phone,
 			  "AppId": 10000000
			},
           success: function (res) {
               if(res.Code == 00){
               		$('#inputCaptcha').attr('placeholder','请输入序号为'+res.Result.CodeSeq+'的验证码');
               		$('#inputCaptcha').attr('CodeSeq',res.Result.CodeSeq);
					var time=60
					$('.btn-cap').text(time)
					timer = setInterval(function() {
	                  time--;
	                  $('.btn-cap').text(time)
	                  if(time <= 0) {
	                    clearInterval(timer);
	                    time = 60;
	                    $(".btn-cap").text('获取验证码');
	                    $(".btn-cap").removeAttr("disabled");
	                  }
	               }, 1000)
					return
				}
           }
     });
})
function loginIn(account,UserPwd){
	$.ajax({
        type: "post",
		data: {
			  "Phone": account,
			  "UserPwd": UserPwd,
			  "AppId": 10000000
		},
		url: "http://51service.xyz/Eagles/api/User/Login",
		dataType: "json",
        success:function(res){
        	var data=res.Result;
            if(res.Code == 00){
            		localStorage.setItem("token",data.Result.Token);//存储token
              	localStorage.setItem("userId",data.Result.UserId); //用户ID
              	localStorage.setItem("IsInternalUser",data.Result.IsInternalUser); //是否是内部用户
              	localStorage.setItem("IsVerifyCode",data.Result.IsVerifyCode); //是否需要验证码
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
clearInterval(timer);//清除定时器