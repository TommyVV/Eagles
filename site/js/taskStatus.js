$(document).ready(function() {
    var token = getCookie("token");
    var userId = getCookie("userId");
    var appId = getRequest("appId");
    var taskId = getRequest("taskId");
    //当前用户是上级还是下级（默认是下级）
    $("#top-nav").html('');
    $("#top-nav").load("head.html", () => {});
    var userType = 1;
    var requestFlag = false;

    //初始化页面
    getTaskDetail();

    $(".opers").click(function() {
        window.location.href = "taskPlanEdit.html";
    });
    //查询任务详情
    function getTaskDetail() {
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Task/GetTaskDetail",
            data: {
                TaskId: taskId,
                Token: token,
                AppId: appId
            },
            success: function(data) {
                console.log("GetTaskDetail---", data);
                if (data.Code == "00") {
                    dealTaskDetail(data.Result);
                } else {
                    requestFlag = false;
                    alert(data.Code, data.Message);
                }
            },
            complete: function() {
                requestFlag = false;
            }
        });
    }
    //活动步骤查询
    function getTaskStep(status) {
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Task/GetTaskStep",
            data: {
                TaskId: taskId,
                Token: token,
                AppId: appId
            },
            success: function(data) {
                console.log("GetTaskStep---", data);
                if (data.Code == "00") {
                    taskRecord(data.Result.StepList, status);
                } else {
                    alert(data.Code, data.Message);
                }
            }
        });
    }
    //审核活动完成
    function editTaskAccept(type, reviewType) {
        //Type (integer, optional): 01-上级审核任务 02-下级接受任务 03-下级申请完成
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Task/EditTaskAccept",
            data: {
                Type: type,
                TaskId: taskId,
                ReviewType: reviewType,
                Token: token,
                AppId: appId
            },
            success: function(data) {
                console.log("EditTaskAccept---", data);
                if (data.Code == "00") {
                    getTaskDetail();
                } else {
                    requestFlag = false;
                    alert(data.Code, data.Message);
                }
            },
            error: function() {
                requestFlag = false;
            }
        });
    }
    //页面内容显示
    function dealTaskDetail(data) {
        // <img src="${data.HeadImg}" alt="">
        var taskInfoStr = `<div class="info-icon">
        <span class="name">${data.InitiateUserName}</span>
        <span class="time">${data.CreateTime.substr(0, 10)}</span>
    </div>
    <div class="row">
        <div class="info-item col-xs-12 col-sm-12 col-md-6">任务标题:
            <span>${data.TaskName}</span>
        </div>
        <div class="info-item col-xs-12 col-sm-12 col-md-6">开始时间:
            <span>${data.TaskBeginDate.substr(0, 10)}</span>
        </div>
        <div class="info-item col-xs-12 col-sm-12 col-md-6">截止时间:
            <span>${data.TaskEndDate.substr(0, 10)}</span>
        </div>
        <div class="info-item col-xs-12 col-sm-12 col-md-6">当前状态:
            <span class="status">${taskStatus(data.TaskStatus)}</span>
        </div>
    </div>`;
        $(".task-info").html(taskInfoStr);
        var taskDesc = `<div class="title">任务内容</div>
        <div class="content">${data.TaskContent}</div>
        <div class="add-area">
        ${attachmentList(data.AttachmentList)}
        </div>
        <div id="btn-area"></div>`;
        $(".task-desc").html(taskDesc);
        //根据当前状态，判断显示按钮
        opersStatus(data);
    }

    function taskStatus(status) {
        if (status == -2) {
            return "未接受";
        } else if (status == -1) {
            return "待上级批准";
        } else if (status == -8) {
            return "完成未通过";
        } else if (status == -9) {
            return "创建未通过";
        } else if (status == 0) {
            return "已接受";
        } else if (status == 2) {
            return "完成待审核";
        } else if (status == 3) {
            return "已完成";
        } else {
            return "";
        }
    }

    function opersStatus(data) {
        var status = data.TaskStatus;
        var initiateUserId = data.InitiateUserId;
        var acceptUserId = data.AcceptUserId;
        var createType = data.CreateType;
        if (status == -2) {
            //上级发起
            if (userId == acceptUserId) {
                $("#btn-area").html(`<div class="sub-btn">接受任务</div>`);
                $(".sub-btn").click(function() {
                    if (!requestFlag) {
                        requestFlag = true;
                        editTaskAccept("2", "0");
                    }
                });
            }
        } else if (status == -1) {
            if (acceptUserId == userId) {
                $("#btn-area").html(`<div class="pass">通过</div>
                <div class="nopass">不通过</div>`);
                $(".pass").click(function() {
                    if (!requestFlag) {
                        requestFlag = true;
                        editTaskAccept(1, "0");
                    }
                });
                $(".nopass").click(function() {
                    if (!requestFlag) {
                        requestFlag = true;
                        editTaskAccept(1, 1);
                    }
                });
            }
        } else if (status == -8) {} else if (status == -9) {} else if (status == 0) {
            if (
                (createType == "0" && userId == acceptUserId) ||
                (createType == "1" && userId == initiateUserId)
            ) {
                $("#btn-area").html(`<div class="sub-btn">制定计划</div>`);
                $(".sub-btn").click(function() {
                    window.location.href =
                        "taskPlan.html?appId=" + appId + "&taskId=" + taskId;
                });
            }
        } else if (status == 2) {
            //完成任务待审核 领导 审核
            if (
                (createType == "0" && userId == initiateUserId) ||
                (createType == "1" && userId == acceptUserId)
            ) {
                //查询任务步骤
                getTaskStep(2);
            }
        } else if (status == 3) {
            //查询任务步骤
            getTaskStep(3);
            //完成任务待审核 领导 审核
            if (
                (createType == "0" && userId == initiateUserId) ||
                (createType == "1" && userId == acceptUserId)
            ) {
                //上级登录
                userType = 0;
            }
            var comment = new Comment({
                commentType: '0',
                id: taskId,
                userType: userType,
                appId: appId,
                token: token,
                userId: userId
            });
            comment.getUserComment();
        } else {}
    }

    //活动记录(活动步骤)
    function taskRecord(list, status) {
        var step = ``;
        list.forEach(element => {
            step += `<div class="oper-item">
            <div class="oper-text">${element.StepName}</div>
        </div>`;
        });
        var str = ``;
        //待审核
        if (status == 2) {
            str = `<div class="title">操作记录</div>
            ${step}
            <div class="points-area">
                <div class="points-tip">请为任务评分</div>
                <div class="points-stars">
                    <span class="glyphicon glyphicon-star n-star"></span>
                    <span class="glyphicon glyphicon-star n-star"></span>
                    <span class="glyphicon glyphicon-star n-star"></span>
                    <span class="glyphicon glyphicon-star n-star"></span>
                    <span class="glyphicon glyphicon-star n-star"></span>
                </div>
                <div class="points-result">
                    <div class="pass">通过</div>
                    <div class="nopass">不通过</div>
                </div>
            </div>`;
            $(".task-record").removeClass('hide').html(str);
            $(".points-stars .glyphicon").click(function() {
                var list = $(".points-stars .glyphicon");
                var flag = true;
                for (var i = 0; i < list.length; i++) {
                    if (flag) {
                        $(list[i])
                            .removeClass("n-star")
                            .addClass("s-star");
                    } else {
                        $(list[i])
                            .removeClass("s-star")
                            .addClass("n-star");
                    }
                    if (list[i] == this) {
                        flag = false;
                    }
                }
            });
            $(".pass").click(function() {
                if (!requestFlag) {
                    requestFlag = true;
                    editTaskComplete(0);
                }
            });
            $(".nopass").click(function() {
                if (!requestFlag) {
                    requestFlag = true;
                    editTaskComplete(1);
                }
            });
        } else {
            str = `<div class="title">操作记录</div>
            ${step}
            <div class="points-area">
                <div class="points-tip">任务评分</div>
                <div class="points-stars">
                    <span class="glyphicon glyphicon-star n-star"></span>
                    <span class="glyphicon glyphicon-star n-star"></span>
                    <span class="glyphicon glyphicon-star n-star"></span>
                    <span class="glyphicon glyphicon-star n-star"></span>
                    <span class="glyphicon glyphicon-star n-star"></span>
                </div>
            </div>`;
            $(".task-record").html(str);
        }
    }
    //上级审核活动是否完成
    function editTaskComplete(status) {
        var score = $('.s-star').length;
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Task/EditTaskComplete",
            data: {
                TaskId: taskId,
                IsPublic: 0,
                Score: score,
                CompleteStatus: status,
                Token: token,
                AppId: appId
            },
            success: function(data) {
                console.log("EditTaskComplete---", data);
                if (data.Code == "00") {
                    getTaskDetail();
                } else {
                    requestFlag = false;
                    alert(data.Code, data.Message);
                }
            },
            error: function() {
                requestFlag = false;
            }
        });
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