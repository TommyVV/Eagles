$(document).ready(function () {
    //积分换购
    $("#point-part").click(function () {
        window.location.href = 'exchange.html'
    });
    //兑换记录
    $("#record").click(function () {
        window.location.href = "exchangeRecord.html"
    });
    //我的任务
    $("#task").click(function () {
        window.location.href = "task.html"
    });
    class CalculateScreen {
        constructor() {
            this.isMobile = /Android|webOS|iPhone|iPod|BlackBerry/i.test(navigator.userAgent);
            this.init();
        }
        init() {
            if (!this.isMobile) {
                $('.mobile').hide();
                $('.pc').show();
                $('#top-nav').load('head.html', () => {

                })
                $('#left-nav').load('leftNav.html', () => {

                })
                $('#footer').load('footer.html', () => {

                })
                $('body').css('background-color', 'rgb(248,248,248)');
            } else {
                $('.mobile').show();
                $('.pc').hide();
                $('body').css('background-color', '#fff');
            }
        }
    }
    new CalculateScreen();

    $(window).resize(function () {
        new CalculateScreen();
    })
});
var token=localStorage.getItem("token")
minedel(token);
function minedel(token){
	$.ajax({
        type: "post",
		data: {
			  "Token": token,
			  "AppId": 10000000
		},
		url: "http://51service.xyz/Eagles/api/User/GetUserInfo",
		dataType: "json",
        success:function(res){
        	var data=res.Result.UserInfo;
            if(res.Code == 00){
            	$('.main-content-top-name,.lc_name').text(data.Name);//昵称
				$('.main-content-top-tel,.info-tel').text(data.UserId);//用户id
				$('.head-icon').text(data.PhotoUrl);//头像
				$('.points,.integral').html(data.Score);//积分
				
				
            }
        }
	})
}




