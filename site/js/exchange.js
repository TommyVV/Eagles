$(document).ready(function () {
	if(!localStorage.getItem('token')){
		window.location.href = "login.html"
	}
	$('#top-nav,#mobilenav').load('./head.html')
    $('#pc-footer').load('./footer.html')
    var winWidth = document.body.clientWidth;
    if (winWidth <= 768) {
        var goodsItemWidth = $('.goods-item').width() + parseInt($('.goods-item').css('margin-left')) * 2;
        var marginLeft = (winWidth - parseInt(winWidth / goodsItemWidth) * goodsItemWidth) / 2;
        $('.goods-area').css('padding-left', marginLeft + 'px');
    }
});
var appId=getRequest('appId');
// $(".search-btn").click(function () {
//  exchange(appId);
// });
exchange(appId);//积分兑换接口
function exchange(appId) {
	$.ajax({
		type: "post",
		data: {
			"ProductName": '',
			"AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/Product/GetProduct",
		dataType: "json",
		success: function(res) {
			$('.goods-area').html('')
			var data = res.Result;
			if(res.Code == 00) {
				var exchange = ''; 
				for(var i = 0; i < data.ProductList.length; i++) {
					exchange+='<div class="goods-item"><img src="'+data.ProductList[i].ProductImageUrl+'" alt="">'+
            			'<div class="item-title">'+data.ProductList[i].ProductName+'</div>'+
            			'<div class="item-point">'+data.ProductList[i].ProductScore+'</div>'+
            			'<div class="item-btn" urls="'+data.ProductList[i].ProductId+'">去看看</div>'+
        '</div>';
				}
				$('.goods-area').append(exchange)
			}
		}
	});
}
//去看看
    $('.goods-area').on('click', '.item-btn', function(e) {
    	var pro=$(this).attr('urls');
        window.location.href = 'goodsDetail.html?productId='+pro+'&appId='+appId+'';
    });