var token = localStorage.getItem('token');
var moduleType = getRequest('moduleType') //获取来源页面['0', '1', '2', '3'],
var appId = getRequest('appId');
$('#top-nav,#mobilenav').load('head.html');
$('#footer').load('footer.html');
if (moduleType == 0) {
    document.title = '首页';
} else if (moduleType == 1) {
    document.title = '党建门户';
} else if (moduleType == 2) {
    document.title = '党建工作';
} else if (moduleType == 3) {
    document.title = '党建学习';
} else if (moduleType == 3) {
    document.title = '生活社区';
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
            $('body').css('background-color', 'rgb(248,248,248)');
        } else {
            $('.mobile').show();
            $('.pc').hide();
            $('body').css('background-color', '#fff');
        }
    }
}
new CalculateScreen();
$(window).resize(function() {
        new CalculateScreen();
    })
    //接口模块
scrollimg(moduleType, token, appId); //banner
moduleListTitle(moduleType, appId) //名称分类方法
function scrollimg(moduleType, token, appId) {
    $.ajax({
        type: "post",
        data: {
            "PageType": moduleType,
            "Token": token,
            "AppId": appId
        },
        url: DOMAIN + "/api/Scroll/GetScrollImg",
        dataType: "json",
        success: function(res) {
            var data = res.Result;
            if (res.Code == 00) {
                var limobile = ''; //移动端标点
                var divs = ''; //移动端图片
                var pclicon = '';
                if (data.ImageList != null) {
                    if (data.ImageList.length == 1) {
                        $('.focus_s,.carousel-indicators').hide();
                    }
                    for (var i = 0; i < data.ImageList.length; i++) {
                        limobile += '<li data-target="#carousel-example-generic" data-slide-to="' + i + '"></li>';
                        divs += '<div class="item"><a href="' + data.ImageList[i].TargetUrl + '"><img src="' + data.ImageList[i].ImageUrl + '" alt=""></a><div class="carousel-caption"></div></div>';
                        pclicon += '<li><a href="' + data.ImageList[i].TargetUrl + '"><img src="' + data.ImageList[i].ImageUrl + '" /> </a></li>'
                    }
                    $('#carousel-example-generic .carousel-indicators').append(limobile);
                    $('#carousel-example-generic .carousel-inner').append(divs);
                    $('#focus_m ul').append(pclicon);

                    focusRun.init();
                    $('#carousel-example-generic .carousel-indicators li:eq(0)').addClass('active');
                    $('.item:eq(0)').addClass('active');
                } else {
                    $('.pc-wrap-banner,.carousel-box').hide()
                }

            } else {
                $('.pc-wrap-banner,.carousel-box').hide()
            }
        }
    });
}

/* 获取分类名称 */
function moduleListTitle(moduleType, appId) {
    $.ajax({
        type: "post",
        data: {
            "ModuleType": moduleType,
            "AppId": appId
        },
        url: DOMAIN + "/api/AppModule/GetModule",
        dataType: "json",
        success: function(res) {
            var moudlesIds = []; //新闻类别属性对象
            var data = res.Result;
            if (res.Code == 00) {
                var title = ''; //移动端
                var pctitieone = ''; //PC端的左边标题
                var pctitietwo = ''; //PC端右边标题
                for (var i = 0; i < data.Modules.length; i++) {
                    var urls = data.Modules[i].TargetUrl;
                    if (urls == '' || urls == null) {
                        urls = "partyLearning_list.html?paramModuleid=" + data.Modules[i].ModuleId + "&paramModuleType=" + moduleType + "&appId=" + appId + ""; //列表页
                    } else {
                        urls = data.Modules[i].TargetUrl;
                    }
                    var pageCount = data.Modules[i].IndexPageCount;
                    var moudle = {}; //存储新闻类别的属性对象
                    moudle.id = data.Modules[i].ModuleId; //存储新闻类别ID
                    moudle.count = pageCount;
                    moudle.class1 = 'id-' + data.Modules[i].ModuleId; //存储一个新闻类别classid
                    moudle.flag = '' //存储新闻类别是左边还是右边的标识符
                    moudle.SubCateType = data.Modules[i].SubCateType;
                    title += '<div class="list-box"><div class="list-header"><span class="la_s1"><img src="' + data.Modules[i].SmallImageUrl + '"></span><span class="title">' + data.Modules[i].ModuleName + '</span><a href="' + urls + '" class="more">更多 ></a></div><div class="list-body id-' + data.Modules[i].ModuleId + '"><div class="list-top row"></div><div class="list-bottom"></div></div></div>';
                    if (i % 2 != 0) { //右边的标题
                        pctitietwo += '<div class="right-item"><h4 class="item-title id-' + data.Modules[i].ModuleId + '"><span class="title"><img src="' + data.Modules[i].SmallImageUrl + '" alt="">' + data.Modules[i].ModuleName + '</span><a href="' + urls + '" class="more">更多&gt;&gt;</a></h4><div class="la_divj"></div></div>';
                        moudle.flag = 'right';
                    } else { //左边的标题
                        pctitieone += '<div class="left-item"><h4 class="item-title id-' + data.Modules[i].ModuleId + '"><span class="title"><img src="' + data.Modules[i].SmallImageUrl + '" alt="">' + data.Modules[i].ModuleName + '</span><a href="' + urls + '" class="more">更多&gt;&gt;</a></h4><div class="left-item-content"></div></div>';
                        moudle.flag = 'left';
                    }
                    //存储新闻类别ID等属性
                    moudlesIds.push(moudle);
                }
                $('.main_content').append(title);
                $('#la_leftg').append(pctitieone);
                $('#la_rig').append(pctitietwo);
                //获取分类下新闻列表   
                for (var i = 0; i < moudlesIds.length; i++) {
                    //获取分类下新闻列表
                    moduleListcontent(moudlesIds[i].id, moudlesIds[i].class1, moudlesIds[i].flag, token, appId, moudlesIds[i].SubCateType, moudlesIds[i].count)
                }
            }
        }
    });
}
//获取新闻列表
function moduleListcontent(moduleId, class1, flag, token, appId, subCateType, count) {
    $.ajax({
        type: "post",
        data: {
            "ModuleId": moduleId,
            "NewsCount": count,
            "Token": token,
            "AppId": appId,
            "IsHome": true,
        },
        url: DOMAIN + "/api/News/GetModuleNews",
        dataType: "json",
        success: function(res) {
            var data = res.Result;
            if (res.Code == 00) {
                var contentone = ''; //移动端内容第一部分内容
                var contenttwo = ''; //移动端内容第二部分内容
                var pccontenone = ''; //PC端左边 第一部分内容
                var pccontentwo = ''; //PC端左边 第二部分内容
                var pclistriht = ''; //PC端右边 内容
                pccontentwo += '<ul>';
                for (var i = 0; i < data.NewsInfos.length; i++) {
                    var externalUrl = data.NewsInfos[i].ExternalUrl
                    if (data.NewsInfos[i].IsExternal == true) {
                        externalUrl = data.NewsInfos[i].ExternalUrl
                    } else {
                        externalUrl = 'partyLearning_detail.html?NewsId=' + data.NewsInfos[i].NewsId + '&appId=' + appId + '' //详情页NewsId 
                    }
                    //移动端代码
                    if (subCateType != 0) {
                        contenttwo += '<div class="media">' +
                            '<div class="media-body media-text"><a href="' + externalUrl + '"><h4 class="media-heading overflow-two-line">' + data.NewsInfos[i].Title + '</h4><span class="list-time">' + data.NewsInfos[i].CreateTime + '</span>' +
                            '<span class="news-source">' + data.NewsInfos[i].Source + '</span>' +
                            '</a></div>' +
                            '</div>';
                    } else {
                        if (i < 2) {
                            contentone += '<div class="media col-xs-6">' +
                                '<div class="media-top"><a href="' + externalUrl + '"><img class="media-object" src="' + data.NewsInfos[i].ImageUrl + '" alt="' + data.NewsInfos[i].Title + '"></a>' +
                                '</div><div class="media-body"><a href="' + externalUrl + '"><h4 class="media-heading overflow-two-line">' + data.NewsInfos[i].Title + '</h4><span class="list-time">' + data.NewsInfos[i].CreateTime + '</span>' +
                                '<span class="news-source">' + data.NewsInfos[i].Source + '</span>' +
                                '</a></div>' +
                                '</div>';
                        } else if (i >= 2) {
                            contenttwo += '<div class="media">' +
                                '<div class="media-left"><a href="' + externalUrl + '"><img class="media-object" src="' + data.NewsInfos[i].ImageUrl + '" alt="' + data.NewsInfos[i].Title + '"></a>' +
                                '</div><div class="media-body"><a href="' + externalUrl + '"><h4 class="media-heading overflow-two-line">' + data.NewsInfos[i].Title + '</h4><span class="list-time">' + data.NewsInfos[i].CreateTime + '</span>' +
                                '<span class="news-source">' + data.NewsInfos[i].Source + '</span>' +
                                '</a></div>' +
                                '</div>';
                        }
                    }
                    //PC代码 右边列表页
                    if (flag == 'right') {
                        if (i == 0) {
                            pclistriht += '<a href="' + externalUrl + '"><div class="top">' + '<img src="' + data.NewsInfos[i].ImageUrl + '" alt="' + data.NewsInfos[i].Title + '" />' +
                                '<div class="top-overview"><div class="top-overview-title">' + data.NewsInfos[i].Title + '</div>' +
                                '<span class="top-overview-date">' + data.NewsInfos[i].CreateTime + '</span>' +
                                '</div></div></a>';
                        } else {
                            pclistriht += '<a href="' + externalUrl + '"><p class="article">' + data.NewsInfos[i].Title + '</p></a>';

                        }

                    } else if (flag == 'left') {
                        if (i < 1) { //PC代码 左边列表页 大图
                            pccontenone += '<a href="' + externalUrl + '"><div class="top">' +
                                '<img src="' + data.NewsInfos[i].ImageUrl + '" alt="' + data.NewsInfos[i].Title + '" />' +
                                '<div class="top-overview">' +
                                '<div class="top-overview-title overflow-two-line">' + data.NewsInfos[i].Title + '</div>' +
                                '<div class="top-overview-article">' + data.NewsInfos[i].Title + '</div>' +
                                '<span class="top-overview-date">' + data.NewsInfos[i].CreateTime + '</span>' +
                                '</div>' +
                                '</div></a>';
                        }
                        if (i >= 1) { //PC代码 左边列表页 小图列表
                            pccontentwo += '<li><a href="' + externalUrl + '"><img src="' + data.NewsInfos[i].ImageUrl + '" alt="" /></a>' +
                                '<div class="overview"><a href="' + externalUrl + '">' +
                                '<div class="overview-title overflow-two-line">' + data.NewsInfos[i].Title + '</div>' +
                                '<div></div><span class="overview-date">' + data.NewsInfos[i].CreateTime + '</span>' +
                                '</a></div></li>';
                        }
                    }
                }
                pccontentwo += '</ul>';
                //移动端内容添加
                $('.' + class1).children('div .list-top').append(contentone);
                $('.' + class1).children('div .list-bottom').append(contenttwo);
                //PC端内容添加
                $('.' + class1).siblings('div .left-item-content').append(pccontenone);
                $('.' + class1).siblings('div .left-item-content').append(pccontentwo);
                $('.' + class1).siblings('div .la_divj').append(pclistriht); //右边标题下内容添加
            }
        }
    });
}