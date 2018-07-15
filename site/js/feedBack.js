$(document).ready(function () {
    var token = getCookie("token");
    var userId = getCookie("userId");
    var appId = getRequest("appId");
    var activityId = "";
    var stepId = "";
    var taskId = "";
    //pageType 1 活动 2 任务
    var pageType = getRequest("pageType");
    if (pageType == 1) {
        activityId = getRequest("activityId");
        $("#content").attr("placeholder", "快让大家知道你的活动进度吧~");
        $(".sub-btn").html("反馈并完成");
    } else if (pageType == 2) {
        stepId = getRequest("stepId");
        taskId = getRequest("taskId");
        $("#content").attr("placeholder", "快让大家知道你的任务进度吧~");
        $(".sub-btn").html("提交反馈");
    }

    //是否公开
    $('.flag-area').click(function(){
        if($('.pub-flag').hasClass('select')){

            $('.pub-flag').attr('src','icons/sel_no@2x.png').removeClass('select');
        }else{
            $('.pub-flag').attr('src','icons/sel_yes@2x.png').addClass('select');
        }
    });
    var fileArray = [];
    //附件上传fileupload
    $("#fileupload").fileupload({
        url: UPLOAD,
        dataType: "json",
        done: function (e, data) {
            if(data.result.Code == "00"){
                var array = data.result.Result.FileUploadResults;
                var object = {
                    AttachName: array[0].FileName,
                    AttachmentDownloadUrl: array[0].FileUrl
                };
                fileArray.push(object);
                $(".attaches").html(attachmentList(fileArray));
            }else{
                console.log(data.result);
            }
        }
    });
    $(".sub-btn").click(function () {
        var content = $("#content").val();
        if (!content) {
            $.alert('反馈内容不能为空');
            return;
        }
        if (pageType == 1) {
            //活动
            editActivityFeedBack();
        } else if (pageType == 2) {
            //任务
            editTaskFeedBack();
        }
    });
    var that=$;
    //任务反馈
    function editTaskFeedBack() {
        var pubFlag = $('.pub-flag').hasClass('select');
        var data = {
            "TaskId": taskId,
            "StepId": stepId,
            "Content": $("#content").val(),
            // "IsPublic": pubFlag==true?0:1,
            "AttachList": fileArray,
            "Token": token,
            "AppId": appId
        };
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Task/EditTaskFeedBack",
            data: data,
            success: function (data) {
                console.log("EditTaskFeedBack---", data);
                if (data.Code == "00") {
                    that.toast("提交成功", function () {
                        window.history.back();
                    });
                } else {
                    alert(data.Code, data.Message);
                }
            }
        });
    }
    //(活动)提交反馈
    function editActivityFeedBack() {
        var data = {
            ActivityId: activityId,
            Content: $("#content").val(),
            AttachList: fileArray,
            Token: token,
            AppId: appId
        };
        console.log(data, JSON.stringify(data));
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Activity/EditActivityFeedBack",
            data: data,
            success: function (data) {
                console.log("CreateActivity---", data);
                if (data.Code == "00") {
                    editActivityReview(2, 0);
                } else {
                    alert(data.Code, data.Message);
                }
            }
        });
    }
    //(活动)审核任务
    function editActivityReview(type, rType) {
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Activity/EditActivityReview",
            data: {
                Type: type,
                ActivityId: activityId,
                ReviewType: rType,
                Token: token,
                AppId: appId
            },
            success: function (data) {
                console.log("EditActivityReview---", data);
                if (data.Code == "00") {
                    window.location.href = "exchangeResult.html?code=1&tip=活动反馈成功";
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
                $("#top-nav").load("head.html", () => { });
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