var token = localStorage.getItem("token")
var appId = getRequest('appId');
var productId = getRequest('productId') //获取产品
var onurl=window.location.href
if(!localStorage.getItem('token')||localStorage.getItem('IsInternalUser')==0) {
	window.location.href = 'login.html?appId=' + appId + '&onurl='+encodeURI(onurl);
}
$('#top-nav,#mobilenav').load('head.html')
$('#footer').load('./footer.html')
goodDetal(productId, appId)
$(".sub-btn,.overview-props-btn").click(function() {
    $.ajax({
        type: "post",
        data: {
            "ProductId": productId,
            "Count": 1,
            "Address": "string",
            "Province": "string",
            "City": "string",
            "District": "string",
            "Token": token,
            "AppId": appId
        },
        url: "http://51service.xyz/Eagles/api/Score/AppScoreExchange",
        dataType: "json",
        success: function(res) {
            //$('.goods-area').html('')
            var data = res.Result;
            if (res.Code == 00) {
                window.location.href = "public_result.html?code=1&tip=商品兑换成功&appId=" + appId +'&mode=商品1';
            } else {
                window.location.href = "public_result.html?code=0&tip="+res.Message+"&appId=" + appId +'&mode=商品0';
            }
        }
    });

});

function goodDetal(productIds, appId) {
    $.ajax({
        type: "post",
        data: {
            "ProductId": productIds,
            "AppId": appId
        },
        url: "http://51service.xyz/Eagles/api/Product/GetProductDetail",
        dataType: "json",
        success: function(res) {
            //$('.goods-area').html('')
            var data = res.Result;
            if (res.Code == 00) {
				if(data.Inventory==0){
					$('.sub-btn,.overview-props-btn').attr("disabled","disabled");
					$('.sub-btn,.overview-props-btn').css("background","#ccc");
				}
                $('.ld_img,.goods-img').attr('src', data.ProductImgUrl);
                $('.overview-props-name,.goods-title').text(data.ProductName);
                $('.overview-props-count span,.num span').text(data.PeopleCount);
                $('.overview-props-price span').text(data.Price);
                $('.overview-kuc span').text(data.Inventory);
                $('.good_kcned span').text(data.Inventory);
                $('.good_priced span').text(data.Price);
                
                $('.points').html('价格:'+data.ProductScore+'积分' ); //移动端
                $('.overview-props-date span,.time span').text(data.ProductBeginTime+'~'+data.ProductEndTime );
                $('.detail p,.detail-content').html(data.ProductDescrption) //文章列表
            }
        }
    });
}

class CalculateScreen {
    constructor() {
        this.isMobile = /Android|webOS|iPhone|iPod|BlackBerry/i.test(navigator.userAgent);
        this.init();
    }
    init() {
        if (!this.isMobile) {
            $('.mobile').hide();
            $('.pc').show();
            /*$('#top-nav').load('head.html', () => {

            })
            $('#footer').load('footer.html', () => {

            })
            $('body').css('background-color', 'rgb(248,248,248)');*/
        } else {
            $('.mobile').show();
            $('.pc').hide();
            /* $('body').css('background-color', '#fff');*/
        }
    }
}
new CalculateScreen();

$(window).resize(function() {
    new CalculateScreen();
})