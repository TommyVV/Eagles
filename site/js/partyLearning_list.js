$('#top-nav,#mobilenav').load('head.html')
$('#footer').load('footer.html');
var Moduleid = getRequest('paramModuleid') //获取模块id
var ModuleType = getRequest('paramModuleType') //获取来源页面id
var token = localStorage.getItem("token")
var appId = getRequest('appId')
var mescroll;
var module;
partyTitle(ModuleType, Moduleid, appId) //来源页面的分类列表
$('.list-bottom').html('')
mescroll = new MeScroll("mescroll", {
    down: {
        auto: false,
        isLock: true,
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

function downcallback() {}

function partyLearningfunc(page) {
    if (page.module) {
        partyLearning(page.module, token, page, appId);
    } else {
        partyLearning(Moduleid, token, page, appId);
    }
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
        success: function (res) {
            //$('.list-bottom').html('')
            var data = res.Result;
            if (res.Code == 00) {
                if (res.Result.NewsInfos != '' || res.Result.NewsInfos != null) {
                    var learningList = '';
                    for (var i = 0; i < data.NewsInfos.length; i++) {
                        var externalUrl = data.NewsInfos[i].ExternalUrl
                        if (data.NewsInfos[i].IsExternal == true) {
                            externalUrl = data.NewsInfos[i].ExternalUrl
                        } else {
                            externalUrl = 'partyLearning_detail.html?NewsId=' + data.NewsInfos[i].NewsId +
                                '&appId=' + appId + '' //详情页
                        }
                        var imgUrl = data.NewsInfos[i].ImageUrl;
                        var imgEle="";
                        if(imgUrl){
                            imgEle='<img class="media-object" src="' + imgUrl+ '">';
                        }else{
                            imgEle='<div class="media-object"></div>'
                        }
                        learningList += '<div class="media"><a href="' + externalUrl + '">' +
                            '<div class="media-left">' +
                            imgEle +
                            '</div>' +
                            '<div class="media-body">' +
                            '<h4 class="media-heading">' + data.NewsInfos[i].Title + '</h4>' +
                            '<span class="list-time">' + data.NewsInfos[i].CreateTime + '</span>' +
                            '</div>' +
                            '</a></div>'
                    }
                    mescroll.endSuccess(data.NewsInfos.length);
                    $('.list-bottom').append(learningList) //新闻列表
                } else {
                    mescroll.lockDownScroll(true);
                    mescroll.lockUpScroll(true);
                }

            } else {
                mescroll.lockDownScroll(true);
                mescroll.lockUpScroll(true);
                $('.mescroll-hardware').html('没有更多数据')
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
        success: function (res) {
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
$('.dropdown-menu').on('click', 'li', function (e) {
    module = $(this).attr('ids')
    partyTitle(ModuleType, module, appId) //来源页面的分类列表
    $('.list-bottom').html('')
    if (mescroll) {
        mescroll.destroy();
    }
    mescroll = new MeScroll("mescroll", {

        down: {
            auto: false,
            isLock: true,
            callback: downcallback
        },
        up: {
            page: {
                num: 0,
                size: 10,
                time: null,
                module: module
            },
            isLock: false,
            callback: partyLearningfunc,
            isBounce: false,
			htmlNodata: '<p class="upwarp-nodata">没有更多数据</p>'
        }
    });

})
