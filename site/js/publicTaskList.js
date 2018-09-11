$(document).ready(function() {
    var token = getCookie("token");
    var userId = getCookie("userId");
    var appId = getRequest("appId");
    $("#top-nav").html('');
    $("#top-nav").load("head.html", () => {});

    var pageIndex = 1;
    var pageSize = 10;

    var mescroll;
    mescroll = new MeScroll("mescroll", {
        down: {
            use: false
        },
        up: {
            callback:getPublicTask,
            isBounce: false
        }
    });
    //查询活动
    function getPublicTask() {
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Task/GetPublicTask",
            data: {
                Token: token,
                AppId: appId,
                PageSize: pageSize,
                PageIndex: pageIndex
            },
            success: function(data) {
                console.log("Task---", data);
                if (data.Code == "00") {
                    var list = data.Result.TaskList;
                    taskList(list);
                    if (list.length < pageSize) {
                        mescroll.endSuccess(5, false, null);
                    } else {
                        pageIndex = pageIndex + 1;
                        mescroll.endSuccess(100000, true, null);
                    }

                } else if (data.Code == '10') {
                    mescroll.endSuccess(10, false, null);
                } else {
                    bootoast({
                        message: ''+data.Message,
                        type: 'warning',
                        position: 'toast-top-center',
                        timeout: 3
                    });
                    mescroll.endErr();
                }
            }
        });
    }
    //任务列表内容
    function taskList(list) {
        var str = ``;
        var pcStr = ``;
        list.forEach(element => {
            str += `<div class="task-item single-task" id="${element.TaskId}">
                        <div class="task-title">${element.TaskeName}</div>
                        <div class="task-content">
                            <div class="task-info">
                                <div>发布人:${element.TaskFromUserName}</div>
                                <div>截止时间:${element.TaskDate.substr(
                    0,
                    10
                )}</div>
                            </div>
                        </div>
                    </div>`;
            //todo ？？？ 
            pcStr += `<li class="single-task" id="${element.TaskId}">
            <h4 class="title">${element.TaskeName}</h4>
            <p>植树节是按照法律规定宣传保护树木，并动员群众参加以植树造林为活动内容的节日。按时间长短可分为植树日、植树周和植树月，共称为国际植树节。提倡通过这种活动，激发人们爱林造林的热情。中国的植树节由林学家韩安、凌道扬等倡议设立，最初确定在4月5日清明节。1928年为纪念孙中山逝世三周年将植树节改为3月12日。</p>
            <div class="props">
                <div class="props-left">
                    <div>
                        发布人：
                        <span class="detail">${element.TaskFromUserName}</span>
                    </div>
                    <div>
                        发布时间：
                        <span class="detail">${element.TaskDate.substr(
                    0,
                    10
                )}</span>
                    </div>
                </div>
            </div>
        </li>`;
        });
        $(".pc-list").html(pcStr);
        $(".task-list").html(str);
        $(".single-task").click(function() {
            window.location.href = "taskStatus.html?type=1&appId=" + appId + "&taskId=" + $(this).attr('id');
        });
        return str;
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
            } else {
                $(".mobile").show();
                $(".pc").hide();
                $("body").css("background-color", "#fff");
            }
        }
    }
    new CalculateScreen();

    $(window).resize(function() {
        new CalculateScreen();
    });
});