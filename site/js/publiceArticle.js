
var appId = getRequest('appId');
var token = localStorage.getItem("token")
if(!localStorage.getItem('token')) {
	window.location.href = 'login.html?appId=' + appId + '';
}
$('#top-nav,#mobilenav').load('./head.html')
$('#footer').load('./footer.html')
//var token="abc";
var mescroll;
$('.item').html('')
mescroll = new MeScroll("mescroll", {
	down: {
		auto: false,
		isLock: true,
		callback: downcallback
	},
	up: {
		page: {
			num: 0,
			size: 10,
			time: null
		},
		isLock: false,
		callback: partyLearningfunc,
		isBounce: false
	}
});

function downcallback() {}

function partyLearningfunc(page) {
myAricle(token, page, appId);
	
}
function myAricle(token, page, appId) {
	$.ajax({
		type: "post",
		data: {
			"Token": token,
			"AppId": appId,
			"PageSize": page.size,
			"PageIndex": page.num
		},
		url: "http://51service.xyz/Eagles/api/User/GetUserArticle",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			if(res.Code == 00) {
				if(res.Result.NewsList != '' || res.Result.NewsList != null) {
					var myAricle = '';
					for(var i = 0; i < data.NewsList.length; i++) {
						myAricle += '<div class="article" type="'+data.NewsList[i].NewsType +'" conretn="' + data.NewsList[i].Title + '" comids="'+data.NewsList[i].NewsContent+'" dangyuan="'+data.NewsList[i].ToUser+'" ispubic="'+data.NewsList[i].IsPublic+'">' +
							'<div class="left"><img src="' + data.NewsList[i].ImageUrl + '" alt=""></div>' +
							'<div class="right">' +
							'<div class="content overflow-two-line">' + data.NewsList[i].Title + '</div>' +
							'<div class="date">' + data.NewsList[i].CreateTime + '</div>' +
							'</div>' +
							'</div>';

					}
					mescroll.endSuccess(data.NewsList.length);
					$('.item').append(myAricle) //文章列表
				} else {
					mescroll.lockDownScroll(true);
					mescroll.lockUpScroll(true);
				}

			} else {
				mescroll.lockDownScroll(true);
				mescroll.lockUpScroll(true);
				$('.mescroll-hardware').html('没有更多')
			}
		}
	});
}
