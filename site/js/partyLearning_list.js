$('#top-nav,#mobilenav').load('head.html')
$('#footer').load('footer.html');
var Moduleid = getRequest('paramModuleid') //获取模块id
var ModuleType = getRequest('paramModuleType') //获取来源页面id
var token = localStorage.getItem("token")
var appId = getRequest('appId')
var mescroll;
partyTitle(ModuleType, Moduleid, appId) //来源页面的分类列表
$('.list-bottom').html('')
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
        isBounce: false
    }
});

function downcallback() {
    partyLearningfunc({ num: 1, size: 10 });
}

function partyLearningfunc(page) {
    partyLearning(Moduleid, token, page, appId);
}

function partyLearning(moduleId, token, page, appId) {
    $.ajax({
        type: "post",
        data: {
            "ModuleId": moduleId,
            "NewsCount": 10,
            "PageSize": page.size,
            "PageIndex": page.num,
            "Token": token,
            "AppId": appId
        },
        url: DOMAIN + "/api/News/GetModuleNews",
        dataType: "json",
        success: function(res) {
            //$('.list-bottom').html('')
            var data = res.Result;
            if (res.Code == 00) {
                if (res.Result.NewsInfos != '' || res.Result.NewsInfos != null) {
                    var learningList = '';
                    if (page.num == 1) {
                        $('.list-bottom').html("");
                    }
                    $.each(res.Result.NewsInfos, function(i, news) {
                        var imgUrl = news.ImageUrl;
                        var imgEle = "";
                        if (imgUrl) {
                            imgEle = '<div class="media-left" style="position: relative;"><img class="media-object" src="' + imgUrl + '"></div>';
                        } else {
                            imgEle = ''
                        }
                        var isVideo = news.IsVideo;
                        var videoIcon = '';
                        if (isVideo == "1") {
                            videoIcon = '<span class="glyphicon glyphicon-film" style="margin-right:10px;"></span>';
                        }
                        learningList = '<div class="media"><div class="newsbody">' +
                            imgEle +
                            '<div class="media-body ">' +
                            '<div class="media-heading overflow-two-line">' + videoIcon + news.Title + '</div>' +
                            '<div class="list-time">' + news.CreateTime + '</div>' +
                            '</div></div></div>'
                        var te = $(learningList);
                        if (isVideo == "1") {
                            te.find(".media-left").append('<img class="is-media" src="icons/videoFlag.png">')
                        }
                        te.find(".newsbody").click(function() {
                            if (news.IsExternal == true) {
                                window.location = news.ExternalUrl
                            } else {
                                window.location = 'partyLearning_detail.html?NewsId=' + news.NewsId +
                                    '&appId=' + appId + '' //详情页
                            }
                        });
                        $('.list-bottom').append(te) //新闻列表
                    });
                    if (res.Result.NewsInfos.length < page.size) {
                        mescroll.endSuccess(100000, false, null);
                    } else {
                        mescroll.endSuccess(100000, true, null);
                    }
                } else {
                    mescroll.lockDownScroll(true);
                    mescroll.lockUpScroll(true);
                }
            } else if (res.Code == '10') {
                mescroll.endSuccess(100000, false, null);
            } else {
                mescroll.endErr();
            }
        }
    });
}
//分类列表的接口
function partyTitle(ModuleType, moduleId, appId) {
    $.ajax({
        type: "post",
        data: {
            "ModuleType": ModuleType,
            "AppId": appId
        },
        url: DOMAIN + "/api/AppModule/GetModule",
        dataType: "json",
        success: function(res) {
            $('.dropdown-menu').html('');
            $('.list-header').html('');
            var data = res.Result;
            if (res.Code == 00) {
                var learningTitle = '';
                var partyheader = '';
                for (var i = 0; i < data.Modules.length; i++) {
                    if (data.Modules[i].ModuleId == moduleId) {
                        partyheader = '<span class="lb_imgb"><img src="' + data.Modules[i].SmallImageUrl +
                            '" /></span><span class="title">' + data.Modules[i].ModuleName + ' ></span>'
                    }
                    learningTitle += '<li class="lb_lihide" ids="' + data.Modules[i].ModuleId + '">' + data
                        .Modules[i].ModuleName + '</li>';
                }
                $('.dropdown-menu').append(learningTitle);
                $('.list-header').append(partyheader);
            }
        }
    });
}
//点击分类重新获取新分类的内容
$('.dropdown-menu').on('click', 'li', function(e) {
    Moduleid = $(this).attr('ids')
    partyTitle(ModuleType, Moduleid, appId) //来源页面的分类列表
    $('.list-bottom').html('')
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
                module: Moduleid
            },
            isLock: false,
            callback: partyLearningfunc,
            isBounce: false
        }
    });

})