var appId = getRequest('appId');
var token = localStorage.getItem('token')
var onurl = window.location.href
if (!localStorage.getItem('token') || localStorage.getItem('IsInternalUser') == 0) {
    window.location.href = 'login.html?appId=' + appId + '&onurl=' + encodeURI(onurl);
}
$('#top-nav,#mobilenav').load('./head.html')
$('#footer').load('./footer.html')

var winWidth = document.body.clientWidth;
if (winWidth <= 768) {
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

function downcallback() {
    exchange(token, appId, { num: 1, size: 10 })
}

function partyLearningfunc(page) {

    exchange(token, appId, page)
}
//exchange(appId); //积分兑换接口
function exchange(token, appId, page) {
    $.ajax({
        type: "post",
        data: {
            "ProductName": '',
            "AppId": appId,
            "Token": token,
            "PageSize": page.size,
            "PageIndex": page.num
        },
        url: DOMAIN + "/api/Product/GetProduct",
        dataType: "json",
        success: function(res) {
            //$('.goods-area').html('')
            var data = res.Result;
            if (res.Code == 00) {
                var list = data.ProductList;
                if (list != '' && list != null) {
                    var exchange = '';
                    for (var i = 0; i < data.ProductList.length; i++) {
                        exchange += '<div class="goods-item"><img src="' + data.ProductList[i].ProductImageUrl + '" alt="">' +
                            '<div class="item-title">' + data.ProductList[i].ProductName + '</div>' +
                            '<div class="item-point">积分:' + data.ProductList[i].ProductScore + '</div>' +
                            '<div class="item-btn" urls="' + data.ProductList[i].ProductId + '">去看看</div>' +
                            '</div>';
                    }
                    if (list.length < page.size) {
                        mescroll.endSuccess(100000, false, null);
                    } else {
                        mescroll.endSuccess(100000, true, null);
                    }
                    if (page.num == 1) {
                        $('.goods-area').html("");
                    }
                    $('.goods-area').append(exchange)
                } else {
                    mescroll.lockDownScroll(true);
                    mescroll.lockUpScroll(true);
                }

            } else if (res.Code == 10) {
                mescroll.endSuccess(100000, false, null);
            } else {
                mescroll.endErr();
                $('.mescroll-hardware').html('亲~暂无产品可以兑换~')
            }
        }
    });
}
//去看看
$('.goods-area').on('click', '.item-btn', function(e) {
    var pro = $(this).attr('urls');
    window.location.href = 'goodsDetail.html?productId=' + pro + '&appId=' + appId + '';
});