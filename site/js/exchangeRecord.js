$(document).ready(function () {
    $("#pc-header").load("./head.html")
    $('#pc-footer').load('./footer.html')
   
});
var token=localStorage.getItem("token")
exchangeRecord(token);
function exchangeRecord(token) {
	$.ajax({
		type: "post",
		data: {
			"Token": token,
  			"AppId": 10000000,
			 "PageSize": 0,
			 "PageIndex": 0
		},
		url: "http://51service.xyz/Eagles/api/Order/GetOrderLs",
		dataType: "json",
		success: function(res) {
			$('.dj-container').html('')
			var data = res.Result;
			if(res.Code == 00) {
				var exchangeRecord = ''; 
				for(var i = 0; i < data.OrderList.length; i++) {
					exchangeRecord+='<div class="record-item"><div class="orderNum">订单编号<span>'+data.ScoreList[i].ProdId+'</span></div>'+
            		'<img src="'+data.ScoreList[i].SmallImageUrl +'" alt="">'+
            		'<div class="record-content">'+
                '<div class="item-title">'+data.ScoreList[i].ProdName +'</div>'+
                '<div class="point">'+data.ScoreList[i].Score +'积分</div>'+
                '<div class="item-time"><span>兑换时间 : </span>'+data.ScoreList[i].CreateTime  +'</div>'+
		            '</div></div>';
				}
				$('.dj-container').append(exchangeRecord)//文章列表
			}
		}
	});
}