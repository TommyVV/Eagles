var token = localStorage.getItem('token')
var appId = getRequest('appId');
var onurl = window.location.href
if (!localStorage.getItem('token') || localStorage.getItem('IsInternalUser') == 0) {
    window.location.href = 'login.html?appId=' + appId + '&onurl=' + encodeURI(onurl);
}
$('#top-nav,#mobilenav').load('./head.html')
$('#footer').load('footer.html')
var mescroll;

/*var appId=10000000*/
$('.myNoticeBox').html('')
mescroll = new MeScroll("mescroll", {
    down: {
        auto: false,
        isLock: false,
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
        isBounce: false,
        htmlNodata: '<p class="upwarp-nodata">没有更多数据</p>'
    }
});

function downcallback() {
    partyLearningfunc({ num: 1, size: 10 });
}

function partyLearningfunc(page) {

    noticeList(token, appId, page)
}

function noticeList(token, appId, page) {
    $.ajax({
        type: "post",
        data: {
            "Token": token,
            "AppId": appId,
            "PageSize": page.size,
            "PageIndex": page.num
        },
        url: DOMAIN + "/api/User/GetUserNotice",
        dataType: "json",
        success: function(res) {
            var data = res.Result;
            if (res.Code == 00) {
                if (res.Result.NoticeList != '' || res.Result.NoticeList != null) {
                    var noticeList = ''; //移动端标点
                    var news = '';

                    for (var i = 0; i < data.NoticeList.length; i++) {
                        if (data.NoticeList[i].IsRead == 0) {
                            news = ''
                        } else if (data.NoticeList[i].IsRead = 1) {
                            news = '<span class="new">New</span>'
                        }

                        noticeList += '<div class="item"><a class="imgturls" href="' + data.NoticeList[i].TargetUrl + '" idse="' + data.NoticeList[i].NewsId + '"><div class="title">' + data.NoticeList[i].Title + '</div>' +
                            '<div class="content overflow-two-line">' + data.NoticeList[i].Content + '</div>' +
                            '<div class="time">' + data.NoticeList[i].CreateTime + '</div>' +
                            '' + news + '' +
                            '</a></div>';
                    }
                    if (page.num == 1) {
                        $('.myNoticeBox').html("");
                    }
                    if (data.NoticeList < page.size) {
                        mescroll.endSuccess(100000, false, null);
                    } else {
                        mescroll.endSuccess(100000, true, null);
                    }
                    $('.myNoticeBox').append(noticeList);
                } else {
                    mescroll.lockDownScroll(true);
                    mescroll.lockUpScroll(true);
                }

            } else if (res.Code == 10) {
                mescroll.endSuccess(100000, false, null);
            } else {
                mescroll.endErr();
                $('.mescroll-hardware').html('没有更多数据')
            }
        }
    });
}
//点击分类重新获取新分类的内容
$('.myNoticeBox').on('click', '.imgturls', function(e) {
    NewsId = $(this).attr('idse')
    $.ajax({
        type: "post",
        data: {
            "NewsId": NewsId,
            "Token": token,
            "AppId": appId
        },
        url: DOMAIN + "/api/User/EditUserNewsIsRead",
        dataType: "json",
        success: function(res) {
            var data = res.Result;
            if (res.Code == 00) {

            }
        }
    })
})