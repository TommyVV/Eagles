var appId = getRequest('appId');
var token=localStorage.getItem('token')
if(!localStorage.getItem('token')) {
	window.location.href = 'login.html?appId=' + appId + '';
}
$('#top-nav,#mobilenav').load('./head.html')
$('#pc-footer').load('./footer.html')

var winWidth = document.body.clientWidth;
if(winWidth <= 768) {
	var goodsItemWidth = $('.goods-item').width() + parseInt($('.goods-item').css('margin-left')) * 2;
	var marginLeft = (winWidth - parseInt(winWidth / goodsItemWidth) * goodsItemWidth) / 2;
	$('.goods-area').css('padding-left', marginLeft + 'px');
}

// $(".search-btn").click(function () {
//  exchange(appId);
// });
var mescroll;
$('.goods-area').html('')
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

	exchange(token, appId, page)
}
//exchange(appId); //积分兑换接口
function exchange(token,appId,page) {
	$.ajax({
		type: "post",
		data: {
			"ProductName": '',
			"AppId": appId,
			 "Token": token,
			 "PageSize": page.size,
			"PageIndex": page.num
		},
		url: "http://51service.xyz/Eagles/api/Product/GetProduct",
		dataType: "json",
		success: function(res) {
			//$('.goods-area').html('')
			var data = res.Result;
			if(res.Code == 00) {
				if(data.ProductList!=''&&data.ProductList!=null){
					var exchange = '';
					for(var i = 0; i < data.ProductList.length; i++) {
						exchange += '<div class="goods-item"><img src="' + data.ProductList[i].ProductImageUrl + '" alt="">' +
							'<div class="item-title">' + data.ProductList[i].ProductName + '</div>' +
							'<div class="item-point">积分:' + data.ProductList[i].ProductScore + '</div>' +
							'<div class="item-btn" urls="' + data.ProductList[i].ProductId + '">去看看</div>' +
							'</div>';
					}
					mescroll.endSuccess(data.ProductList.length)
					$('.goods-area').append(exchange)
				}else{
					mescroll.lockDownScroll(true);
					mescroll.lockUpScroll(true);
				}
				
			}else {
                alert(res.Code, res.Message);
                mescroll.lockDownScroll(true);
				mescroll.lockUpScroll(true);
				$('.mescroll-hardware').html('没有更多')
            }
		}
	});
}
//去看看
$('.goods-area').on('click', '.item-btn', function(e) {
	var pro = $(this).attr('urls');
	window.location.href = 'goodsDetail.html?productId=' + pro + '&appId=' + appId + '';
});