var NewsTypes = getRequest('NewsType');
var appId = getRequest('appId');
var token = localStorage.getItem("token")
var onurl = window.location.href
if (!localStorage.getItem('token') || localStorage.getItem('IsInternalUser') == 0) {
    window.location.href = 'login.html?appId=' + appId + '&onurl=' + encodeURI(onurl);
}
$('#top-nav,#mobilenav').load('./head.html')
$('#footer').load('./footer.html')


//添加发布按钮
pulicContent('publishArticle.html?appId=' + appId + '');
//var token="abc";
var mescroll;
var check_value;
var zlValue;
var bqValue;
var dyValue;
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
        callback: partyLearningfunc,
        isBounce: false
    }
});

function downcallback() {
    partyLearningfunc({ num: 1, size: 10 });
}

function partyLearningfunc(page) {
    if (page.check_value) {
        if (check_value == zlValue) {
            myAricle(0, token, page, appId);
        } else if (check_value == bqValue) {
            myAricle(2, token, page, appId);
        } else if (check_value == dyValue) {
            myAricle(3, token, page, appId);
        } else {
            myAricle(1, token, page, appId);
        }
    } else {
        // if(NewsTypes == undefined) {
        // 	$('.select_txt').text("文章");
        // 	myAricle(0, token, page, appId);
        // } else if(NewsTypes == 0) {
        // 	$('.select_txt').text("文章");
        // 	myAricle(0, token, page, appId);
        // } else if(NewsTypes == 1) {
        // 	$('.select_txt').text("心得体会");
        // 	myAricle(1, token, page, appId);
        // } else if(NewsTypes == 2) {
        // 	$('.select_txt').text("会议");
        // 	myAricle(2, token, page, appId);
        // } else if(NewsTypes == 3) {
        // 	$('.select_txt').text("党员申请书");
        // 	myAricle(3, token, page, appId);
        // }
        $(".cl").removeClass("select");
        if (NewsTypes == undefined) {
            $($('.cl')[0]).addClass("select");
            myAricle(0, token, page, appId);
        } else if (NewsTypes == 0) {
            $($('.cl')[0]).addClass("select");
            myAricle(0, token, page, appId);
        } else if (NewsTypes == 1) {
            $($('.cl')[1]).addClass("select");
            myAricle(1, token, page, appId);
        } else if (NewsTypes == 2) {
            $($('.cl')[2]).addClass("select");
            myAricle(2, token, page, appId);
        } else if (NewsTypes == 3) {
            $($('.cl')[3]).addClass("select");
            myAricle(3, token, page, appId);
        }
    }

}
$(".select_box").click(function(event) {
    event.stopPropagation();
    $(this).find(".option").toggle();
    $(this).parent().siblings().find(".option").hide();
});
$(document).click(function(event) {
    var eo = $(event.target);
    if ($(".select_box").is(":visible") && eo.attr("class") != "option" && !eo.parent(".option").length)
        $('.option').hide();
});

function myAricle(NewsType, token, page, appId) {
    $.ajax({
        type: "post",
        data: {
            "NewsType": NewsType,
            "Token": token,
            "AppId": appId,
            "PageSize": page.size,
            "PageIndex": page.num
        },
        url: DOMAIN + "/api/User/GetUserArticle",
        dataType: "json",
        success: function(res) {
            var data = res.Result;
            if (res.Code == 00) {
                if (res.Result.NewsList != '' || res.Result.NewsList != null) {
                    if (page.num == 1) {
                        $('.item').html("");
                    }
                    var myAricle = '';
                    for (var i = 0; i < data.NewsList.length; i++) {
                        myAricle += '<div class="article" NewsId="' + data.NewsList[i].NewsId + '" type="' + data.NewsList[i].NewsType + '" >' +
                            '<div class="left"><img src="' + data.NewsList[i].ImageUrl + '" alt=""></div>' +
                            '<div class="right">' +
                            '<div class="content overflow-two-line">' + data.NewsList[i].Title + '</div>' +
                            '<div class="date">' + data.NewsList[i].CreateTime + '</div>' +
                            '</div>' +
                            '</div>';
                    }
                    if (data.NewsList.length < 10) {
                        mescroll.endSuccess(10, false, null);
                    } else {
                        mescroll.endSuccess(data.NewsList.length);
                    }
                    $('.item').append(myAricle) //文章列表
                } else {
                    mescroll.endSuccess(10, false, null);
                }

            } else if (res.Code == 10) {
                mescroll.endSuccess(10, false, null);
            } else {
                mescroll.endErr();
            }
        }
    });
}
$(".cl").click(function() {
    NewsTypes = $(this).index();
    $('.item').html('');
    if (mescroll) {
        mescroll.destroy();
    }
    mescroll = new MeScroll("mescroll", {
        down: {
            auto: false,
            callback: downcallback
        },
        up: {
            page: {
                num: 0,
                size: 10,
                time: null,
            },
            callback: partyLearningfunc,
            isBounce: false
        }
    });
});
$(".select_box").click(function() {
    if ($(this).find(".glyphicon").hasClass("glyphicon-menu-down")) {
        $(this).find(".glyphicon").removeClass("glyphicon-menu-down").addClass("glyphicon-menu-up");
        //$(".peop-list").removeClass("hide");
    } else {
        $(this).find(".glyphicon").removeClass("glyphicon-menu-up").addClass("glyphicon-menu-down");
        //$(".peop-list").addClass("hide");
    }
});

//点击文章分类
$('.item').on('click', '.article', function(e) {
    var aryewsType = $(this).attr('type');

    var NewsId = $(this).attr('NewsId');
    window.location.href = 'publishArticle.html?appId=' + appId + '&NewsId=' + NewsId + '&aryewsType=' + aryewsType

})