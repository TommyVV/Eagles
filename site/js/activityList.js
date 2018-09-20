$(document).ready(function() {
    var token = getCookie('token');
    var appId = getRequest("appId");
    if(!token) {
        window.location.href = 'login.html?appId=' + appId + '';
    }
    $('#top-nav').html('');
    $('#top-nav').load('./head.html');
    //添加发布按钮
    pulicContent('publishTask.html?appId=' + appId + '&type=0');
    var currentItemType = '0';
    var pageIndex = 1;
    var pageSize = 10;
    var mescroll;
    mescroll = new MeScroll("mescroll", {
        down: {
            use: false
        },
        up: {
            callback: getActivityList,
            isBounce: false
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
                        mescroll.endSuccess(100000, false, null);
                    } else {
                        pageIndex = pageIndex + 1;
                        mescroll.endSuccess(100000, true, null);
                    }
                    //给每列数据绑定事件
                    $(".article").click(function() {
                        var par = $(this).attr('id');
                        var tmpArray = par.split('_');
                        console.log(tmpArray);
                        var id = tmpArray[0];
                        var type = tmpArray[1];
                        var testId = tmpArray[2];
                        var status = tmpArray[3];
                        if (type == '0') {
                            //报名活动
                            if(status==-9){
                                //需要再次编辑
                                window.location.href = "publishTask.html?appId=" + appId + "&type=0&updateId=" + id;
                            }else{
                                window.location.href = "activityDetail.html?appId=" + appId + "&activityId=" + id;
                            }
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
    function errorTip(str){
        bootoast({
            message: ''+str,
            type: 'warning',
            position: 'toast-top-center',
            timeout: 3
        });
    }
    //处理返回数据
    function dealReturnList(list) {
        var content = '';
        list.forEach(element => {
            var leftEl=``;
            if(element.ImageUrl){
                leftEl=`<div class="left">
                        <img src="${element.ImageUrl}"
                            alt="">
                    </div>`;
            }
            content += `<div class="article" id="${element.ActivityId}_${element.ActivityType}_${element.TestId}_${element.Status}">
                    ${leftEl}
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