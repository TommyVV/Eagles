var appId = getRequest('appId');
$('#top-nav,#mobilenav').load('./head.html')
$('#footer').load('./footer.html')
var mescroll;
$('.item').html('')
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
    partyLearningfunc({ num: 1, size: 10 })
}

function partyLearningfunc(page) {
    myAricle(page, appId);

}

function myAricle(page, appId) {
    $.ajax({
        type: "post",
        data: {
            "Token": "",
            "AppId": appId,
            "PageSize": page.size,
            "PageIndex": page.num
        },
        url: DOMAIN + "/api/News/GetPublicUserNews",
        dataType: "json",
        success: function(res) {
            var data = res.Result;
            if (res.Code == 00) {
                var list = res.Result.NewsList;
                if (list != '' || list != null) {
                    var myAricle = '';
                    for (var i = 0; i < list.length; i++) {
                        var leftEl = ``;
                        if (list[i].ImageUrl) {
                            leftEl = `<div class="left">
									<img src="${list[i].ImageUrl}"
										alt="">
								</div>`;
                        } else {
                            leftEl = `<div style="margin-right:0.36267rem;"></div>`;
                        }
                        myAricle += '<div class="article" type="' + list[i].NewsId + '">' +
                            leftEl +
                            '<div class="right">' +
                            '<div class="content overflow-two-line">' + list[i].Title + '</div>' +
                            '<div class="date">' + list[i].CreateTime + '</div>' +
                            '</div>' +
                            '</div>';

                    }
                    if (page.num == 1) {
                        $('.item').html("");
                    }
                    if (list.length < page.size) {
                        mescroll.endSuccess(100000, false, null);
                    } else {
                        pageIndex = pageIndex + 1;
                        mescroll.endSuccess(100000, true, null);
                    }
                    $('.item').append(myAricle) //文章列表
                } else if (res.Code == 10) {
                    mescroll.endSuccess(100000, false, null);
                } else {
                    mescroll.endErr();
                }

            } else {
                mescroll.lockDownScroll(true);
                mescroll.lockUpScroll(true);
                $('.mescroll-hardware').html('没有更多'); //todo？
            }
        }
    });
}
//点击文章分类
$('.item').on('click', '.article', function(e) {
    var aryewsType = $(this).attr('type');
    window.location.href = 'article_del.html?appId=' + appId + '&aryewsType=' + aryewsType;
})