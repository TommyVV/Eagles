var appId = getRequest('appId');
var token = localStorage.getItem("token")
var onurl=window.location.href
if(!localStorage.getItem('token')||localStorage.getItem('IsInternalUser')==0) {
	window.location.href = 'login.html?appId=' + appId + '&onurl='+encodeURI(onurl);
}
var mescroll;

$('#top-nav,#mobilenav').load('head.html')
$('#footer').load('./footer.html')

$('.dj-container').html('')
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
	exchangeRecord(token, appId, page);

}

function exchangeRecord(token, appId, page) {
	$.ajax({
		type: "post",
		data: {
			"Token": token,
			"AppId": appId,
			"PageSize": page.size,
			"PageIndex": page.num
		},
		url: "http://51service.xyz/Eagles/api/Order/GetOrderLs",
		dataType: "json",
		success: function(res) {
			//$('.dj-container').html('')
			var data = res.Result;
			if(res.Code == 00) {
				var exchangeRecord = '';
				if(res.Result.OrderList != '' || res.Result.OrderList != null) {
					for(var i = 0; i < data.OrderList.length; i++) {
						exchangeRecord += '<div class="record-item"><div class="orderNum">订单编号<span>' + data.OrderList[i].ProdId + '</span></div>' +
							'<img src="' + data.OrderList[i].SmallImageUrl + '" alt="">' +
							'<div class="record-content">' +
							'<div class="item-title">' + data.OrderList[i].ProdName + '</div>' +
							'<div class="point">' + data.OrderList[i].Score + '积分</div>' +
							'<div class="item-time"><span>兑换时间 : </span>' + data.OrderList[i].CreateTime + '</div>' +
							'</div></div>';
					}
					mescroll.endSuccess(data.OrderList.length);
					$('.dj-container').append(exchangeRecord) //文章列表
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