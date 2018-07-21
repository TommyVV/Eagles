var token = localStorage.getItem("token")
var appId = getRequest('appId');
var productId = getRequest('productId') //获取产品
if (!localStorage.getItem('token')) {
    window.location.href = 'login.html?appId=' + appId + '';
}
$('#top-nav,#mobilenav').load('head.html')
$('#footer').load('./footer.html')
goodDetal(productId, appId)
$(".sub-btn,.overview-props-btn").click(function() {
    $.ajax({
        type: "post",
        data: {
            "ProductId": productId,
            //"ProductId": $('.overview-props-price span').text(),
            "Count": $('.num span').text(),
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
                window.location.href = "exchangeResult.html?code=1&tip=商品兑换成功&appId=" + appId;
            } else {
                window.location.href = "exchangeResult.html?code=0&tip="+res.Message+"&appId=" + appId;
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
                $('.ld_img,.goods-img').attr('src', data.ProductImgUrl);
                $('.overview-props-name,.goods-title').text(data.ProductName);
                $('.overview-props-count span,.num span').text(data.PeopleCount);
                $('.overview-props-price span').text(data.Price);
                $('.overview-kuc span').text(data.Inventory);
                $('.good_kcned span').text(data.Inventory);
                $('.good_priced span').text(data.Price);
                
                $('.points').html(data.ProductScore + '积分换购'); //移动端
                $('.overview-props-date span,.time span').text(data.ProductBeginTime);
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