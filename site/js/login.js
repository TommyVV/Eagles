
$('.btn-login').on('click', function (e) {
    e.preventDefault();

    let password = $('#inputPassword').val();
    let account = $('#inputUser').val();
    let captcha = $('#inputCaptcha').val();
    if (!account) {
        alert('请输入账号');
        return;
    } else if (!password) {
        alert('请输入密码');
        return;
    } 
    var hash = hex_md5(password);
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
					location.href = 'mine.html';
				}else{
					if(prevLink.indexOf('http://51service.xyz/')==-1){	//来自其它站点
						location.href = 'mine.html';
					}
					if(prevLink.indexOf('register.html')!=-1){		//来自注册页面
						location.href = 'signup.html';
					}
					location.href = prevLink;
				}

            }
        }
	})
})
$(".reg-box li img").click(function(){
         var url = "captchaCode";
         // var data = {type:1};
         $.ajax({
          type : "get",
          async : false, //同步请求
          url : url,
          // data : data,
          timeout:1000,
          success:function(dates){
          //alert(dates);
          $(".reg-box li img")[0].src="captchaCode";//要刷新的img
          },
          error: function() {
                // alert("失败，请稍后再试！");
              }
         });
     });
$('#pc-header').load('./head.html')
