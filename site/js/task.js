$(document).ready(function() {
    var token = getCookie("token");
    var userId = getCookie("userId");
    var appId = getRequest("appId");
    if(!token) {
        window.location.href = 'login.html?appId=' + appId + '';
    }
    var taskType = 0;
    var checkUserId = userId;
    $("#top-nav").html('');
    $("#top-nav").load("head.html", () => {});

    //添加发布按钮
    pulicContent('publishTask.html?appId=' + appId + '&type=1');
    var pageIndex = 1;
    var pageSize = 10;
    var isPC = false;

    var mescroll;
    //类别的点击
    $("#t-cate").click(function() {
        //关闭我的部分
        $("#m-cate")
                .find(".glyphicon")
                .removeClass("glyphicon-menu-down")
                .addClass("glyphicon-menu-left");
            $(".peop-list").addClass("hide");
        if (
            $(this)
            .find(".glyphicon")
            .hasClass("glyphicon-menu-left")
        ) {
            $(this)
                .find(".glyphicon")
                .removeClass("glyphicon-menu-left")
                .addClass("glyphicon-menu-down");
            $(".task-type").removeClass("hide");
        } else {
            $(this)
                .find(".glyphicon")
                .removeClass("glyphicon-menu-down")
                .addClass("glyphicon-menu-left");
            $(".task-type").addClass("hide");
        }
        pageIndex = 1;
    });
    //任务类型 分类点击
    $(".task-type .list-item").click(function() {
        var id = $(this).attr("id");
        var text = $(this).html();
        $(".t-text").html(text);
        $(".task-type").addClass("hide");
        $("#t-cate")
            .find(".glyphicon")
            .removeClass("glyphicon-menu-down")
            .addClass("glyphicon-menu-left");
        taskType = id;
        $('.task-list').html('');
        getTaskList();
    });
    //人员下拉列表
    $("#m-cate").click(function() {
        //关闭类别筛选
        $("#t-cate")
                .find(".glyphicon")
                .removeClass("glyphicon-menu-down")
                .addClass("glyphicon-menu-left");
        $(".task-type").addClass("hide");
        if (
            $(this)
            .find(".glyphicon")
            .hasClass("glyphicon-menu-left")
        ) {
            $(this)
                .find(".glyphicon")
                .removeClass("glyphicon-menu-left")
                .addClass("glyphicon-menu-down");
            $(".peop-list").removeClass("hide");
        } else {
            $(this)
                .find(".glyphicon")
                .removeClass("glyphicon-menu-down")
                .addClass("glyphicon-menu-left");
            $(".peop-list").addClass("hide");
        }
    });
    getUserRelationship();
    //查询活动
    function getTaskList() {
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Task/GetTask",
            data: {
                UserId: checkUserId,
                TaskType: taskType,
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
                    // var tipInfo = `<div class="tip">暂时没有数据</div>`;
                    // $(".pc-list").html(tipInfo);
                    // $(".task-list").html(tipInfo);
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
    //查询下级人员
    function getUserRelationship() {
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/User/GetUserRelationship",
            data: {
                Type: 2,
                UserId: userId,
                Token: token,
                AppId: appId
            },
            success: function(data) {
                console.log("GetUserRelationship---", data);
                if (data.Code == "00") {
                    dealRelationList(data.Result.UserList);
                } else {
                    bootoast({
                        message: ''+data.Message,
                        type: 'warning',
                        position: 'toast-top-center',
                        timeout: 3
                    });
                }
            }
        });
    }
    //任务列表内容
    function taskList(list) {
        var str = ``;
        var pcStr = ``;
        var dealStatus = function(status) {
            var sta = ``;
            if (status == 0) {
                sta = `<div class="task-status line-color accept-status">已接受</div>`;
            } else if (status == 2) {
                sta = `<div class="task-status line-color accept-status">完成待审核</div>`;
            } else if (status == 3) {
                sta = `<div class="task-status line-color accept-status">已完成</div>`;
            } else if (status == -1) {
                sta = `<div class="task-status init-status">未接受</div>`;
            } else if (status == -2) {
                sta = `<div class="task-status init-status">待审核</div>`;
            }else if (status == -8) {
                sta = `<div class="task-status init-status">完成未通过</div>`;
            } else if (status == -9) {
                sta = `<div class="task-status init-status">创建未通过</div>`;
            }
            return sta;
        };
        var dealStatusPC = function(status) {
            var sta = ``;
            if (status == 0) {
                sta = `<span class="props-btn line-color">已接受</span>`;
            } else if (status == 2) {
                sta = `<span class="props-btn line-color">完成待审核</span>`;
            } else if (status == 3) {
                sta = `<span class="props-btn line-color">已完成</span>`;
            } else if (status == -1) {
                sta = `<div class="props-btn already">未接受</div>`;
            }else if (status == -2) {
                sta = `<span class="props-btn already">待审核</span>`;
            } else if (status == -8) {
                sta = `<span class="props-btn already">完成未通过</span>`;
            } else if (status == -9) {
                sta = `<span class="props-btn already">创建未通过</span>`;
            }
            return sta;
        };
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
                            ${dealStatus(element.TaskStatus)}
                        </div>
                    </div>`;
            pcStr += `<li class="single-task" id="${element.TaskId}">
            <h4 class="title">${element.TaskeName}</h4>
            <p>${element.TaskContent}</p>
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
                ${dealStatusPC(element.TaskStatus)}
            </div>
        </li>`;
        });
        $(".pc-list").append(pcStr);
        $(".task-list").append(str);
        $(".single-task").click(function() {
            window.location.href = "taskStatus.html?appId=" + appId + "&taskId=" + $(this).attr('id');
        });
        return str;
    }
    //当前人员及其下属列表
    function dealRelationList(list) {
        var str = `<div class="list-item" id="${userId}">我的</div>`;
        var pcStr = `<li id="${userId}">我的</li>`;
        list.forEach(element => {
            str += `<div class="list-item" id="${element.UserId}">${
                element.Name
                }</div>`;
            pcStr += `<li id="${element.UserId}">${element.Name}</li>`;
        });
        $(".peop-list .menu-list").html(str);
        $("#person-name").html(pcStr);
        $(".select-name").html('我的');
        $(".select-type").html('发起的');
        //切换人员 分类点击
        $(".peop-list .list-item").click(function() {
            var id = $(this).attr("id");
            var text = $(this).html();
            checkUserId = id;
            $(".m-text").html(text);
            $(".peop-list").addClass("hide");
            $("#m-cate")
                .find(".glyphicon")
                .removeClass("glyphicon-menu-down")
                .addClass("glyphicon-menu-left");
            $('.task-list').html('');
            getTaskList();
        });
        return str;
    }
    $("#select-result").on("click", e => {
        if (e.target.tagName === "LI") {
            $("#select-result > span").html(
                $(e.target)
                .html()
                .trim()
            );
            checkUserId = $(e.target).attr('id');
            $(".pc-list").html("");
            getTaskList();
        }
        $("#person-name").toggle();
    });
    $("#select-type").on("click", e => {
        if (e.target.tagName === "LI") {
            $("#select-type > span").html(
                $(e.target)
                .html()
                .trim()
            );
            taskType = $(e.target).attr('id');
            $(".pc-list").html("");
            getTaskList();
        }
        $("#type-name").toggle();

    });

    class CalculateScreen {
        constructor() {
            this.isMobile = /Android|webOS|iPhone|iPod|BlackBerry/i.test(
                navigator.userAgent
            );
            this.init();
        }
        init() {
            if (!this.isMobile) {
                if(isPC){
                    return;
                }
                isPC=true;
                mescroll = new MeScroll("mescrollPC", {
                    down: {
                        use: false
                    },
                    up: {
                        callback:getTaskList,
                        isBounce: false
                    }
                });
                $(".pc-list").html("");
                $(".mobile").hide();
                $(".pc").show();
                $("#footer").load("footer.html", () => {});
                $("body").css("background-color", "rgb(248,248,248)");
            } else {
                mescroll = new MeScroll("mescroll", {
                    down: {
                        use: false
                    },
                    up: {
                        callback:getTaskList,
                        isBounce: false
                    }
                });
                $(".task-list").html("");
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