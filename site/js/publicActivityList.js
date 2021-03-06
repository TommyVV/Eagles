$(document).ready(function() {
    var token = getCookie('token');
    var appId = getRequest("appId");
    $('#top-nav').html('');
    $('#top-nav').load('./head.html');
    var pageIndex = 1;
    var pageSize = 10;
    var mescroll;
    mescroll = new MeScroll("mescroll", {
        down: {
            auto: false,
            callback: downCallback
        },
        up: {
            callback: getPublicActivityList,
            isBounce: false
        }
    });

    function downCallback() {
        pageIndex = 1;
        getPublicActivityList();
    }
    //查询活动
    function getPublicActivityList() {
        $.ajax({
            type: 'POST',
            url: DOMAIN + '/api/Activity/GetPublicActivityList',
            data: {
                "ActivityType": 0,
                "Token": token,
                "AppId": appId,
                "PageSize": pageSize,
                "PageIndex": pageIndex
            },
            success: function(data) {
                console.log('GetActivityList---', data);
                if (data.Code == "00") {
                    var list = data.Result.ActivityList;
                    if (pageIndex == 1) {
                        $(".item").html("");
                    }
                    $(".item").append(dealReturnList(list));
                    if (list.length < pageSize) {
                        mescroll.endSuccess(100000, false, null);
                    } else {
                        pageIndex = pageIndex + 1;
                        mescroll.endSuccess(100000, true, null);
                    }
                    //给每列数据绑定事件
                    $(".article").click(function() {
                        var par = $(this).attr('id');
                        var tmpArray = par.split('-');
                        console.log(tmpArray);
                        var id = tmpArray[0];
                        var type = tmpArray[1];
                        var testId = tmpArray[2];
                        if (type == '0') {
                            //报名活动
                            window.location.href = "activityDetail.html?type=1&appId=" + appId + "&activityId=" + id;
                        } else if (type == '1') {
                            //投票活动
                            window.location.href = "vote.html?appId=" + appId + "&testId=" + testId + "&activityId=" + id;
                        } else if (type == '2') {
                            //问卷调查
                            window.location.href = "onlineExam.html?appId=" + appId + "&testId=" + testId + "&activityId=" + id;
                        }
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
            content += `<div class="article" id="${element.ActivityId}-${element.ActivityType}-${element.TestId}">
                    <div class="left">
                        <img src="${element.ImageUrl}"
                            alt="">
                    </div>
                    <div class="right">
                        <div class="content overflow-two-line">
                            ${element.ActivityName}
                        </div>
                        <div class="date">
                            ${element.ActivityDate.substr(0, 10)}
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