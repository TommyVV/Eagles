$(document).ready(function() {
    var token = getCookie("token");
    var userId = getCookie("userId");
    var appId = getRequest("appId");
    var taskId = getRequest("taskId");
    $("#top-nav").html('');
    $("#top-nav").load("head.html", () => {});
    var stepId = "";
    getTaskStep();
    //新增计划
    $(".plan-add").click(function() {
        stepId = "";
        var modal = new Modal(2, {
            title: '新增步骤',
            content: ''
        }, function(str) {
            console.log('text---', str);
            editTaskStep(str);
        });
    });
    var that = $;
    //申请完成
    $('.sub-btn').click(function() {
        editTaskComplete();
    });
    //活动步骤查询
    function getTaskStep() {
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
                    taskStepContent(data.Result.StepList);
                } else if (data.Code == "10") {
                    //数据为空
                    $(".plan-content").html(`<div class="plan-tip">赶快为任务指定计划吧！</div>`);
                } else {
                    alert(data.Code, data.Message);
                }
            }
        });
    }
    //活动步骤编辑
    function editTaskStep(name) {
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Task/EditTaskStep",
            data: {
                BranchId: 0,
                TaskId: taskId,
                StepId: stepId,
                StepName: name,
                Token: token,
                AppId: appId
            },
            success: function(data) {
                console.log("EditTaskStep---", data);
                if (data.Code == "00") {
                    getTaskStep();
                    $("#step-name").val("");
                    $(".alert").addClass("hide");
                } else {
                    alert(data.Code, data.Message);
                }
            }
        });
    }
    //活动步骤删除
    function removeTaskStep() {
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Task/RemoveTaskStep",
            data: {
                TaskId: taskId,
                StepId: stepId,
                Token: token,
                AppId: appId
            },
            success: function(data) {
                console.log("RemoveTaskStep---", data);
                if (data.Code == "00") {
                    getTaskStep();
                } else {
                    alert(data.Code, data.Message);
                }
            }
        });
    }
    //任务步骤显示
    function taskStepContent(list) {
        var str = ``;
        list.forEach(element => {
            str += `<div class="plan-item">
                    <div class="item-content content-${element.StepId}">${
                element.StepName
                }</div>
                    <div class="item-opers">
                        <div class="oper edit" id="edit-${element.StepId}">
                            <span class="glyphicon glyphicon-edit"></span>编辑
                        </div>
                        <div class="oper feedback" id="feedback-${
                element.StepId
                }">
                            <span class="glyphicon glyphicon-pencil"></span>反馈
                        </div>
                        <div class="oper del" id="del-${element.StepId}">
                            <span class="glyphicon glyphicon-trash"></span>删除
                        </div>
                    </div>
                </div>`;
        });
        $(".plan-content").html(str);
        $(".btn-area").removeClass("hide");
        //编辑按钮
        $(".edit").click(function() {
            stepId = $(this)
                .attr("id")
                .split("-")[1];
            $(".alert .weui-dialog__title").html("编辑步骤");
            $("#step-name").val($(".content-" + stepId).text());
            $(".alert").removeClass("hide");
        });
        //反馈按钮
        $(".feedback").click(function() {
            stepId = $(this)
                .attr("id")
                .split("-")[1];
            window.location.href =
                "feedBack.html?pageType=1&stepId=" +
                stepId +
                "&appId=" +
                appId +
                "&taskId=" +
                taskId;
        });
        //删除按钮
        $(".del").click(function() {
            stepId = $(this)
                .attr("id")
                .split("-")[1];
            removeTaskStep();
        });
    }
    //(下级申请)活动完成
    function editTaskComplete() {
        //Type (integer, optional): 01-上级审核任务 02-下级接受任务 03-下级申请完成
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Task/EditTaskAccept",
            data: {
                Type: '3',
                TaskId: taskId,
                ReviewType: '0',
                Token: token,
                AppId: appId
            },
            success: function(data) {
                console.log("(下级申请)活动完成---", data);
                if (data.Code == "00") {
                    that.toast("申请已提交", function() {});
                } else {
                    alert(data.Code, data.Message);
                }
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