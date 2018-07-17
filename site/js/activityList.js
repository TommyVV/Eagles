$(document).ready(function() {
    var token = getCookie('token');
    var appId = getRequest("appId");
    $('#top-nav').html('');
    $('#top-nav').load('./head.html');
    var currentItemType = '0';

    //查询所有活动
    getActivityList('0');
    //头部按钮点击
    $(".activity-cate").click(function() {
        var id = $(this).attr('id');
        if (id != currentItemType) {
            currentItemType = id;
            $(".activity-cate").find("span").removeClass("select");
            $(this).find("span").addClass("select");
            $(".item").html('');
            getActivityList(id);
        }

    });

    //查询活动
    function getActivityList(type) {
        $.ajax({
            type: 'POST',
            url: DOMAIN + '/api/Activity/GetActivityList',
            data: {
                "ActivityType": type,
                "ActivityPage": 0,
                "Token": token,
                "AppId": appId
            },
            success: function(data) {
                console.log('GetActivityList---', data);
                if (data.Code == "00") {
                    var list = data.Result.ActivityList;
                    $(".item").html(dealReturnList(list));
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
                    $(".item").html('暂无数据');
                } else {
                    alert(data.Code, data.Message);
                }
            }
        })
    }
    //处理返回数据
    function dealReturnList(list) {
        var content = '';
        list.forEach(element => {
            content += `<div class="article" id="${element.ActivityId}-${element.ActivityType}-${element.TestId}">
                    <div class="left">
                        <img src="${element.ImgUrl}"
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