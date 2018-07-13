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
//var token=localStorage.getItem("token")
var token="abc"
var appId=10000000;
minedel(token,appId);
function minedel(token,appId){
	$.ajax({
        type: "post",
		data: {
			  "Token": token,
			  "AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/User/GetUserInfo",
		dataType: "json",
        success:function(res){
        	
            if(res.Code == 00){
            	var data=res.Result.ResultUserInfo;
            	$('.main-content-top-name,.lc_name').text(data.Name);//昵称
				$('.main-content-top-tel,.info-tel').text(data.Telphone);//用户id
				$('.head-icon').text(data.PhotoUrl);//头像
				$('.points,.integral').html(data.Score);//积分
				
				
            }
        }
	})
}
getScrollNews(token,appId)
function getScrollNews(token,appId){
	$.ajax({
        type: "post",
		data: {
			  "Token": token,
			  "AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/Scroll/GetScrollNews",
		dataType: "json",
        success:function(res){
        	$('#scrollobj').html('');
            if(res.Code == 00){
            	var data=res.Result.SystemNewsList;
            	var divs='';
            	for(var i=0;i<data.length;i++){
            		divs+=''+data[i].NewsName+'';
            	}
            	$('#scrollobj').append(divs)
            	setInterval("scroll(document.getElementById('scrollobj'))",20);
            }else{
            	
            }
        }
	})
}
function scroll(obj){
  var tmp=(obj.scrollLeft)++;
  if(obj.scrollLeft==tmp){
    obj.innerHTML += obj.innerHTML;
  }
  if(obj.scrollLeft>=obj.firstChild.offsetWidth){
    obj.scrollLeft=0;
  }
}
unreadMessage(token);
function unreadMessage(token) {
	$.ajax({
		type: "get",
		url: "http://51service.xyz/Eagles/api/UserMessage/GetUserUnreadMessage?token="+token+"",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			if(res.Code == 00) {
				$('.news_list').text(data.UnreadMessageCount);
				
			}
		}
	});
}




