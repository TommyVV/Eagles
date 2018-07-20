$(document).ready(function() {
    var token = getCookie('token');
    var appId = getRequest("appId");
    $('#top-nav').html('');
    $('#top-nav').load('./head.html');
    var currentItemType = '0';
    var pageIndex = 1;
    var pageSize = 6;

    var mescroll;
    mescroll = new MeScroll("mescroll", {
        down: {
            use: false
        },
        up: {
            callback: getActivityList,
            isBounce: false,
            htmlNodata: '没有更多数据'
        }
    });

    //查询所有活动
    //getActivityList();
    //头部按钮点击
    $(".activity-cate").click(function() {
        var id = $(this).attr('id');
        if (id != currentItemType) {
            currentItemType = id;
            $(".activity-cate").find("span").removeClass("select");
            $(this).find("span").addClass("select");
            $(".item").html('');
            pageIndex = 1;
            getActivityList();
        }

    });

    //查询活动
    function getActivityList() {
        $.ajax({
            type: 'POST',
            url: DOMAIN + '/api/Activity/GetActivityList',
            data: {
                "ActivityType": 0,
                "ActivityPage": currentItemType,
                "Token": token,
                "AppId": appId,
                "PageSize": pageSize,
                "PageIndex": pageIndex
            },
            success: function(data) {
                console.log('GetActivityList---', data);
                if (data.Code == "00") {
                    var list = data.Result.ActivityList;
                    $(".item").append(dealReturnList(list));
                    if (list.length < pageSize) {
                        mescroll.endSuccess(5, false, null);
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
                            window.location.href = "activityDetail.html?appId=" + appId + "&activityId=" + id;
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
                    mescroll.endErr();
                }
            },
            error: function() {
                mescroll.endErr();
            }
        })
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