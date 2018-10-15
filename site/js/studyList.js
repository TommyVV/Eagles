$(document).ready(function() {
    var token = getCookie('token');
    var appId = getRequest("appId");
    if (!token) {
        window.location.href = 'login.html?appId=' + appId + '';
    }
    $('#top-nav').html('');
    $('#top-nav').load('./head.html');
    var currentItemType = '0';
    var pageIndex = 1;
    var pageSize = 10;
    var mescroll;
    mescroll = new MeScroll("mescroll", {
        down: {
            auto: false,
            callback: downCallback
        },
        up: {
            callback: getUserStudy,
            isBounce: false
        }
    });
    getUserStudy('init');
    function downCallback() {
        pageIndex = 1;
        getUserStudy();
    }

    //头部按钮点击
    $(".activity-cate").click(function() {
        var id = $(this).attr('id');
        if (id != currentItemType) {
            currentItemType = id;
            $(".activity-cate").find("span").removeClass("select");
            $(this).find("span").addClass("select");
            $(".item").html('');
            pageIndex = 1;
            getUserStudy();
        }

    });

    //查询学习时间
    function getUserStudy(sType) {
        var studyType = sType=="init"?'1':currentItemType;
        $.ajax({
            type: 'POST',
            url: DOMAIN + '/api/Study/GetUserStudy',
            data: {
                "StudyType": studyType,
                "Token": token,
                "AppId": appId,
                "PageSize": pageSize,
                "PageIndex": pageIndex
            },
            success: function(data) {
                if (data.Code == "00") {
                    var list = data.Result.StudyList;
                    if (pageIndex == 1) {
                        $(".item").html("");
                    }
                    if (studyType == 0) {
                        $(".head-area #0").html('已学习(' + data.Result.StudyCount + ')');
                    } else {
                        $(".head-area #1").html('未学习(' + data.Result.StudyCount + ')');
                    }
                    if(studyType==currentItemType){
                        $(".item").append(dealReturnList(list));
                    }
                    if (list.length < pageSize) {
                        mescroll.endSuccess(100000, false, null);
                    } else {
                        pageIndex = pageIndex + 1;
                        mescroll.endSuccess(100000, true, null);
                    }
                    //给每列数据绑定事件
                    $(".article").click(function() {
                        var newsId = $(this).attr('id');
                        window.location.href = "partyLearning_detail.html?appId=" + appId + "&NewsId=" + newsId;
                    });
                } else if (data.Code == '10') {
                    mescroll.endSuccess(10, false, null);
                } else {
                    errorTip(data.Message);
                    mescroll.endErr();
                }
            },
            error: function() {
                errorTip('网络错误');
                mescroll.endErr();
            }
        })
    }

    function errorTip(str) {
        bootoast({
            message: '' + str,
            type: 'warning',
            position: 'toast-top-center',
            timeout: 3
        });
    }
    //处理返回数据
    function dealReturnList(list) {
        var content = '';
        list.forEach(element => {
            content += `<div class="article" id="${element.NewsId}">
                    <div class="left">
                        <img src="${element.ImageUrl}"
                            alt="">
                    </div>
                    <div class="right">
                        <div class="content overflow-two-line">${element.Title}</div>
                        <div class="date">
                            <div>${element.CreateTime.substr(0, 10)}</div>
                            <div>
                            <span class="glyphicon glyphicon-time" aria-hidden="true"></span><span>已学习:</span><span class="studyTime">${element.StudyTime}分钟</span>
                            </div>    
                        </div>
                    </div>
                </div>`;
        });
        return content;
    }
    class CalculateScreen {
        constructor() {
            this.isMobile = /Android|webOS|iPhone|iPod|BlackBerry/i.test(
                navigator.userAgent
            );
            this.init();
        }
        init() {
            if (!this.isMobile) {
                $(".mobile").hide();
                $(".pc").show();
                $("#footer").load("footer.html", () => {});
                $("body").css("background-color", "rgb(248,248,248)");
                $(".container").addClass('pc-wrap');
            } else {
                $(".mobile").show();
                $(".pc").hide();
                $("body").css("background-color", "#fff");
                $(".container").removeClass('pc-wrap');
            }
        }
    }
    new CalculateScreen();

    $(window).resize(function() {
        new CalculateScreen();
    });

});