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
        $("#modalLargeLabel").html('新增步骤');
    });
    //确定计划
    $('#btnTestSaveLarge').on('click', function() {
        var stepContent = $("#step-content").val();
        if (stepContent) {
            editTaskStep(stepContent);
        } else {
            bootoast({
                message: "请填写任务步骤",
                type: "info",
                position: "toast-top-center",
                timeout: 2
            });
        }
    });
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
                    bootoast({
                        message: data.Message,
                        type: "info",
                        position: "toast-top-center",
                        timeout: 2
                    });
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
                    bootoast({
                        message: '提交成功',
                        type: "success",
                        position: "toast-top-center",
                        timeout: 2
                    });
                    getTaskStep();
                    $('.modal').modal('hide');
                } else {
                    bootoast({
                        message: data.Message,
                        type: "info",
                        position: "toast-top-center",
                        timeout: 2
                    });
                }
            },
            error:function(){

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
                    bootoast({
                        message: data.Message,
                        type: "info",
                        position: "toast-top-center",
                        timeout: 2
                    });
                }
            }
        });
    }
    //任务步骤显示
    function taskStepContent(list) {
        var str = ``;
        list.forEach((element,index) => {
            str += `<div class="plan-item">
                    <div class="item-content content-${element.StepId}">${
                element.StepName
                }</div>
                    <div class="item-opers">
                        <div class="oper edit" id="edit-${element.StepId}-${index}" data-toggle="modal" data-target="#myModal">
                            <span class="glyphicon glyphicon-edit"></span>编辑
                        </div>
                        <div class="oper feedback" id="feedback-${element.StepId}-${index}">
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
            var itemInfoArr = $(this)
                .attr("id")
                .split("-");
            stepId = itemInfoArr[1];
            $("#modalLargeLabel").html('编辑步骤');
            $("#step-content").val(list[itemInfoArr[2]].StepName);
        });
        //反馈按钮
        $(".feedback").click(function() {
            var itemInfoArr = $(this)
                .attr("id")
                .split("-");
            stepId = itemInfoArr[1];    
            localStorage.setItem('stepfeed',JSON.stringify(list[itemInfoArr[2]]));
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
                    window.location.href = "exchangeResult.html?code=1&tip=申请已提交成功&appId=" + appId;
                } else {
                    bootoast({
                        message: data.Message,
                        type: "info",
                        position: "toast-top-center",
                        timeout: 2
                    });
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