var token = localStorage.getItem('token');
var appId = getRequest('appId');
$('#top-nav,#mobilenav').html('')
$('#top-nav,#mobilenav').load('./head.html')
var onurl=window.location.href
if(!localStorage.getItem('token')||localStorage.getItem('IsInternalUser')==0) {
	window.location.href = 'login.html?appId=' + appId + '&onurl='+encodeURI(onurl);
}
var phonesmodiy;
$(function(){
	//修改密码
	$("#modifypass,.md_password").click(function() {
		console.log(1)
		window.location.href = 'modifPassword.html?appId=' + appId + '&phonesmodiy='+phonesmodiy
	});
})
class CalculateScreen {
		constructor() {
			this.isMobile = /Android|webOS|iPhone|iPod|BlackBerry/i.test(navigator.userAgent);
			this.init();
		}
		init() {
			if(!this.isMobile) {
				$('.mobile').hide();
				$('.pc').show();
				$('#left-nav').load('leftNav.html?appId=' + appId + '', () => {
	
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
	
	$(window).resize(function() {
		new CalculateScreen();
	})

//积分换购
$("#point-part").click(function() {
	window.location.href = 'exchange.html?appId=' + appId + ''
});
//兑换记录
$("#record").click(function() {
	window.location.href = 'exchangeRecord.html?appId=' + appId + ''
});
//我的文章
$(".lw_acile,.fb_wz").click(function() {
	window.location.href = 'myArticle.html?appId=' + appId + '&NewsType=0'
});
//我的会议
$("#myhuiyi").click(function() {
	window.location.href = 'myArticle.html?appId=' + appId + '&NewsType=2'
});
//我的积分
$(".lw_muscore,#myscroce").click(function() {
	window.location.href = 'rank.html?appId=' + appId + ''
});
//我的文章
$(".a_tz,.fb_wz").click(function() {
	window.location.href = 'myInfo.html?appId=' + appId + ''
});
//我的通知
$(".lw_news,.fb_info").click(function() {
	window.location.href = 'myNotice.html?appId=' + appId + ''
});
//我的活动
$("#myproduct,.fb_profuct").click(function() {
	window.location.href = 'activityList.html?appId=' + appId + ''
});
//我的学习
$("#mystudy").click(function() {
	window.location.href = 'studyList.html?appId=' + appId + ''
});
//我的收藏
$("#mycol").click(function() {
	window.location.href = 'myColList.html?appId=' + appId + ''
});
//我的会议
$("#mymeeting").click(function() {
	window.location.href = 'meetingList.html?appId=' + appId + ''
});
//文章发布
$("#pushace,.fb_wzby").click(function() {
	window.location.href = 'publishArticle.html?appId=' + appId + ''
});
//发布活动
$("#f_huod,.fb_fbgt").click(function() {
	window.location.href = 'publishTask.html?appId=' + appId + '&type=0'
});
//发布任务
$("#f_rw,.fb_wrw").click(function() {
	console.log(7)
	window.location.href = 'publishTask.html?appId=' + appId + '&type=1'
});
//我的任务
$("#task").click(function() {
	window.location.href = 'task.html?appId=' + appId + ''
});
$("#signout").click(function(){
	window.location.href = 'login.html?appId=' + appId + ''	
});



minedel(token, appId);

function minedel(token, appId) {
	$.ajax({
		type: "post",
		data: {
			"Token": token,
			"AppId": appId
		},
		url: DOMAIN + "/api/User/GetUserInfo",
		dataType: "json",
		success: function(res) {
        
			if(res.Code == 00) {
				var data = res.Result.ResultUserInfo;
				$('.main-content-top-name,.lc_name').text(data.Name); //昵称
				$('.main-content-top-tel,.info-tel').text(data.Telphone); //用户id
				$('.head-icon,.head-pic').attr("src",data.PhotoUrl); //头像
				$('.points,.integral').html(data.Score); //积分
				phonesmodiy=data.Telphone
				$('.myr_num').html('('+res.Result.TaskCount+')'); 
				$('.mya_num').html('('+res.Result.ActivityCount+')'); 

			}
		}
	})
}
getScrollNews(token, appId)

function getScrollNews(token, appId) {
	$.ajax({
		type: "post",
		data: {
			"Token": token,
			"AppId": appId
		},
		url: DOMAIN + "/api/Scroll/GetScrollNews",
		dataType: "json",
		success: function(res) {
			$('#scrollobj').html('');
			if(res.Code == 00) {
				var data = res.Result.SystemNewsList;
				//var data=[{"NewsId":1,"NewsName":"你好啊！"},{"NewsId":2,"NewsName":"你000000好啊！"}];
				var divs = '';
				for(var i = 0; i < data.length; i++) {
					divs += '<span class="scroll-r" ids="'+data[i].NewsId+'">' + data[i].NewsName + '</span>';
				}
				$('#scrollobj').append(divs).css("padding-left",window.screen.width+"px");

				setInterval("scroll(document.getElementById('scrollobj'),"+data.length+")", 20);
			} else {
				$('#scrollobj,.newsd').hide();
			}
		}
	})
}

function scroll(obj,n) {
	var tmp = (obj.scrollLeft) ++;
	if(obj.scrollLeft == tmp) {
		obj.innerHTML += "&nbsp;";
	}
	if(obj.scrollLeft >= obj.firstChild.offsetWidth*n+window.screen.width) {
		obj.scrollLeft = 0;
	}
}
unreadMessage(token);

function unreadMessage(token) {
	$.ajax({
		type: "get",
		url: DOMAIN + "/api/UserMessage/GetUserUnreadMessage?token=" + token + "",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			if(res.Code == 00) {
				$('.news_list').text(data.UnreadMessageCount);
				if(data.UnreadMessageCount>0){
					$('.news_list').parent().css("color","red");
					var show=true;
					setInterval(function(){
						if(show){
							show=false;
							$('.news_list').parent().css("visibility","hidden");
							return;
						}
						show=true;
						$('.news_list').parent().css("visibility","visible");
					},1000);
				}

			}
		}
	});
}
//点击滚动信息
$('#scrollobj').on('click', '.scroll-r', function (e) {
    module = $(this).attr('ids')
    window.location.href = 'notice_detail.html?appId=' + appId+'&NewsId='+module;
})
