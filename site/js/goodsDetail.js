$(document).ready(function () {
	if(!localStorage.getItem('token')){
		window.location.href = "login.html"
	}
	$('#pc-header,#mobilenav').load('./head.html')
    $(".sub-btn").click(function () {
        window.location.href = "exchangeResult.html?code=1";
    });
});
var token=localStorage.getItem("token")
var appId=getRequest('appId');
var productId=getRequest('productId')//获取产品
goodDetal(productId,appId)
function goodDetal(productIds,appId) {
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
			if(res.Code == 00) {
				$('.ld_img,.goods-img').attr('src',data.ProductImgUrl);
				$('.overview-props-name').text(data.ProductName);
				$('.overview-props-count span,.num span').text(data.PeopleCount);
				$('.overview-props-price span').text(data.ProductScore+'积分');
				$('.points').html(data.ProductScore+'积分换购');//移动端
				$('.overview-props-date span,.time span').text(data.ProductBeginTime);
				$('.detail p,.detail-content').html(data.ProductDescrption )//文章列表
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

$(window).resize(function () {
    new CalculateScreen();
})