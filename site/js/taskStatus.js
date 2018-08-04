$(document).ready(function () {
    var token = getCookie("token");
    var userId = getCookie("userId");
    var appId = getRequest("appId");
    var taskId = getRequest("taskId");
    var score = 0;
    if (!token) {
        window.location.href = 'login.html?appId=' + appId + '';
    }
    //当前用户是上级还是下级（默认是下级）
    $("#top-nav").html('');
    $("#top-nav").load("head.html", () => { });
    // 0 是审核者 1 不是审核人
    var userType = 1;
    var requestFlag = false;
    //初始化页面
    getTaskDetail();
    $(".opers").click(function () {
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
            success: function (data) {
                console.log("GetTaskDetail---", data);
                if (data.Code == "00") {
                    dealTaskDetail(data.Result);
                } else {
                    requestFlag = false;
                    errorTip(data.Message);
                }
            },
            error: function () {
                errorTip("网络错误");
            },
            complete: function () {
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
            success: function (data) {
                console.log("GetTaskStep---", data);
                if (data.Code == "00") {
                    taskRecord(data.Result.StepList, status);
                } else {
                    errorTip('任务步骤:' + data.Message);
                }
            },
            error: function () {
                errorTip("网络错误");
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
            success: function (data) {
                console.log("EditTaskAccept---", data);
                if (data.Code == "00") {
                    getTaskDetail();
                } else {
                    requestFlag = false;
                    errorTip(data.Message);
                }
            },
            error: function () {
                requestFlag = false;
                errorTip("网络错误");
            }
        });
    }
    //页面内容显示
    function dealTaskDetail(data) {
        // <img src="${data.HeadImg}" alt="">
        // <div class="info-icon">
        // <span class="name">${data.InitiateUserName}</span>
        // <span class="time">${data.CreateTime.substr(0, 10)}</span>
        //  </div>
        var taskInfoStr = `<div class="row">
            <div class="info-item col-xs-12 col-sm-12 col-md-6">
            <div class="item-title">负责人：</div>
            <div class="item-c">${data.AcceptUserName}</div>
        </div>
        <div class="info-item col-xs-12 col-sm-12 col-md-6">
            <div class="item-title">审核人：</div>
            <div class="item-c">${data.AuditUserName}</div>
        </div>
        <div class="info-item col-xs-12 col-sm-12 col-md-6">
            <div class="item-title">任务标题：</div>
            <div class="item-c">${data.TaskName}</div>
        </div>
        <div class="info-item col-xs-12 col-sm-12 col-md-6">
            <div class="item-title">开始时间：</div>
            <div>${data.TaskBeginDate.substr(0, 10)}</div>
        </div>
        <div class="info-item col-xs-12 col-sm-12 col-md-6">
            <div class="item-title">截止时间：</div>
            <div>${data.TaskEndDate.substr(0, 10)}</div>
        </div>
        <div class="info-item col-xs-12 col-sm-12 col-md-6">
        <span class="item-title">当前状态：</span>
            <div class="status">${taskStatus(data.TaskStatus)}</div>
        </div>
    </div>`;
        score = data.Score || 0;
        $(".task-info").html(taskInfoStr);
        var taskDesc = `<div class="title">任务内容</div>
        <div class="content">${data.TaskContent}</div>
        <div class="add-area">
        ${attachmentList(data.AttachmentList, 0)}
        </div>
        <div id="btn-area"></div>`;
        $(".task-desc").html(taskDesc);
        //根据当前状态，判断显示按钮
        opersStatus(data);
    }

    function taskStatus(status) {
        if (status == -2) {
            return "待审核";
        } else if (status == -1) {
            return "未接受";
        } else if (status == -8) {
            return "完成未通过";
        } else if (status == -9) {
            return "创建未通过";
        } else if (status == 0) {
            return "任务启动";
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
        var auditUserId = data.AuditUserId;
        var createType = data.CreateType;
        if (status == -2 || status == -1) {
            //任务为开始，需要审核
            if (userId == auditUserId) {
                $("#btn-area").addClass('points-result').html(`<div class="pass">通过</div>
                <div class="nopass">不通过</div>`);
                $(".pass").click(function () {
                    if (!requestFlag) {
                        requestFlag = true;
                        editTaskAccept(1, "0");
                    }
                });
                $(".nopass").click(function () {
                    if (!requestFlag) {
                        requestFlag = true;
                        editTaskAccept(1, 1);
                    }
                });
            }
        } else if (status == 0) {
            //任务开始，接受者可制度计划
            if (userId == acceptUserId) {
                $("#btn-area").html(`<div class="sub-btn">制定计划</div>`);
                $(".sub-btn").click(function () {
                    window.location.href =
                        "taskPlan.html?appId=" + appId + "&taskId=" + taskId;
                });
            }
        } else if (status == 2) {
            //提交任务 审核者审核
            //查询任务步骤
            if(userId==auditUserId){
                userType = 0;
                getTaskStep(2);
            }else if(userId==acceptUserId){
                getTaskStep('');
            }
        } else if (status == 3) {
            //任务完成
            //查询任务步骤
            getTaskStep(3);
            if(userId==auditUserId){
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

        }
    }

    //活动记录(活动步骤)
    function taskRecord(list, status) {
        var step = ``;
        list.forEach((element, index) => {
            step += `<div class="oper-item">
                <div class="oper-text">${element.StepName}</div>
                <button type="button" class="opers" id="step-${index}" data-toggle="modal" data-target="#myModal">查看</button></div>`;
        });
        var str = ``;
        //待审核
        if (status == 2) {
            if (userType == 0) {
                //上级审核
                str = `<div class="title">计划步骤</div>
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
                    <div class="pub-area">
                        <div class="item flag-area" id="pub-1">
                            <img class="pub-flag" src="icons/sel_no@2x.png" alt="">不公示
                        </div>
                        <div class="item flag-area" id="pub-2">
                            <img class="pub-flag" src="icons/sel_no@2x.png" alt="">公示到支部
                        </div>
                        <div class="item flag-area" id="pub-3">
                            <img class="pub-flag" src="icons/sel_no@2x.png" alt="">公示到组织
                        </div>
                    </div>
                    <div class="points-result">
                        <div class="pass">通过</div>
                        <div class="nopass">不通过</div>
                    </div>
                </div>`;
                $(".task-record").removeClass('hide').html(str);
                //是否公开
                $(".pub-area .item").click(function () {
                    $('.item').removeClass('select');
                    var options = $('.item').find('img');
                    $(options).attr('src', 'icons/sel_no@2x.png');
                    $(this).addClass('select');
                    $($(this).find('img')).attr('src', 'icons/sel_yes@2x.png');
                });

                $(".points-stars .glyphicon").click(function () {
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
                $(".pass").click(function () {
                    if (!requestFlag) {
                        requestFlag = true;
                        editTaskComplete(0);
                    }
                });
                $(".nopass").click(function () {
                    if (!requestFlag) {
                        requestFlag = true;
                        editTaskComplete(1);
                    }
                });
            } else {
                //下级查看
                str = `<div class="title">计划步骤</div>${step}`;
                $(".task-record").removeClass('hide').html(str);
            }

        } else{
            var starStr = ``;
            for (var i = 0; i < 5; i++) {
                if (i < score) {
                    starStr += `<span class="glyphicon glyphicon-star s-star"></span>`;
                } else {
                    starStr += `<span class="glyphicon glyphicon-star n-star"></span>`;
                }
            }
            if(status==3){
                starStr = `<div class="points-area">
                <div class="points-tip">任务评分</div>
                <div class="points-stars">
                    ${starStr}
                </div>
            </div>`;
            }
            str = `<div class="title">计划步骤</div>
            ${step}`;
            $(".task-record").removeClass('hide').html(str);
        }
        //查看操作详情
        $(".opers").click(function () {
            var stepIndex = $(this).attr('id').split('-')[1];
            var stepItem = list[stepIndex];
            var stepDetail = `<div style="width:90%;margin-left:5%;">
                        <div class="step-title">步骤内容：</div>
                        <div class="step-content">${stepItem.StepName}</div>
                        <div class="step-title">反馈内容：</div>
                        <div class="step-content">${stepItem.Content == null ? "" : stepItem.Content}</div>
                        <div class="add-area">
                        ${attachmentList(stepItem.AttachList, 0)}
                        </div>
                        </div>`;
            $(".modal-body").html(stepDetail);
        });
    }
    //上级审核活动是否完成
    function editTaskComplete(status) {
        score = $('.s-star').length;
        var pubFlag = $('.pub-flag').hasClass('select');
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Task/EditTaskComplete",
            data: {
                TaskId: taskId,
                IsPublic: pubFlag == true ? 0 : 1,
                Score: score,
                CompleteStatus: status,
                Token: token,
                AppId: appId
            },
            success: function (data) {
                console.log("EditTaskComplete---", data);
                if (data.Code == "00") {
                    getTaskDetail();
                } else {
                    requestFlag = false;
                    errorTip(data.Message);
                }
            },
            error: function () {
                requestFlag = false;
                errorTip("网络错误");
            }
        });
    }
    function errorTip(str) {
        bootoast({
            message: '' + str,
            type: 'warning',
            position: 'toast-top-center',
            timeout: 3
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
                $("#footer").load("footer.html", () => { });
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

    $(window).resize(function () {
        new CalculateScreen();
    });
});