$(document).ready(function() {
    var token = getCookie("token");
    var userId = getCookie("userId");
    var appId = getRequest("appId");
    var activityId = "";
    var stepId = "";
    var taskId = "";
    var fileArray = [];
    $("#top-nav").html('');
    $("#top-nav").load("head.html", () => {});
    var requestFlag = false;
    //pageType 0 活动 1 任务
    var pageType = getRequest("pageType");
    if (pageType == 0) {
        activityId = getRequest("activityId");
        $("#content").attr("placeholder", "快让大家知道你的活动进度吧~");
        $(".sub-btn").html("反馈并完成");
    } else if (pageType == 1) {
        stepId = getRequest("stepId");
        taskId = getRequest("taskId");
        $("#content").attr("placeholder", "快让大家知道你的任务进度吧~");
        $(".sub-btn").html("提交反馈");
        var stepfeed = JSON.parse(localStorage.getItem('stepfeed'));
        if(stepfeed){
            $("#content").val(stepfeed.Content);
            stepfeed.AttachList.forEach(element => {
                if(element.AttachName&&element.AttachmentDownloadUrl){
                    fileArray.push(element);
                }
            });
            fileArray = fileArray||[];
            if (fileArray.length == 4) {
                $(".upload-file").hide();
            }
            $(".attaches").html(attachmentList(fileArray,1));
            $(".glyphicon-remove").click(function(){
                var index = $('.glyphicon-remove').index(this);
                fileArray.splice(index,1);
                $(this).parents('.file').remove();
                $(".upload-file").show();
                console.log('index---',index);
            })
        }
    }
    //附件上传fileupload
    $("#fileupload").fileupload({
        url: UPLOAD,
        dataType: "json",
        //设置进度条
        progressall: function(e, data) {
            var progress = parseInt((data.loaded / data.total) * 100);
            $(".upload-progress").removeClass("hide");
            console.log("progress", progress);
            $(".upload-progress .bar").css("width", progress + "%");
            if (progress == 100) {
                setTimeout(() => {
                    $(".upload-progress").addClass("hide");
                }, 1000);
            }
        },
        done: function(e, data) {
            console.log("上传附件--", data);
            if (data.result.Code == "00") {
                var array = data.result.Result.FileUploadResults;
                var object = {
                    AttachName: array[0].FileName,
                    AttachmentDownloadUrl: array[0].FileUrl
                };
                fileArray.push(object);
                if (fileArray.length == 4) {
                    $(".upload-file").hide();
                }
                $(".attaches").html(attachmentList(fileArray,1));
                $(".glyphicon-remove").click(function(){
                    var index = $('.glyphicon-remove').index(this);
                    fileArray.splice(index,1);
                    $(this).parents('.file').remove();
                    $(".upload-file").show();
                    console.log('index---',index);
                })
            } else {
                console.log(data.result);
                bootoast({
                    message: ''+data.result.Message,
                    type: 'warning',
                    position: 'toast-top-center',
                    timeout: 3
                });
            }
        }
    });
    $(".sub-btn").click(function() {
        var content = $("#content").val();
        if (!content) {
            errorTip('反馈内容不能为空');
            return;
        }
        if (pageType == 0) {
            //活动
            editActivityFeedBack();
        } else if (pageType == 1) {
            //任务
            editTaskFeedBack();
        }
    });
    function errorTip(str){
        bootoast({
            message: ''+str,
            type: 'warning',
            position: 'toast-top-center',
            timeout: 3
        });
    }
    //任务反馈
    function editTaskFeedBack() {
        var pubFlag = $('.pub-flag').hasClass('select');
        if(requestFlag){
            return;
        }
        requestFlag=true;
        var data = {
            "TaskId": taskId,
            "StepId": stepId,
            "Content": $("#content").val(),
            // "IsPublic": pubFlag == true ? 0 : 1,
            "AttachList": fileArray,
            "Token": token,
            "AppId": appId
        };
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Task/EditTaskFeedBack",
            data: data,
            success: function(data) {
                console.log("EditTaskFeedBack---", data);
                if (data.Code == "00") {
                    $.toast("提交成功", function() {
                        window.history.back();
                    });
                } else {
                    errorTip(data.Message);
                }
            },
            error:function(){
                errorTip('网络错误');
            },
            complete:function(){
                requestFlag = false;
            }

        });
    }
    //(活动)提交反馈
    function editActivityFeedBack() {
        if(requestFlag){
            return;
        }
        requestFlag=true;
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
            success: function(data) {
                console.log("editActivityFeedBack---", data);
                if (data.Code == "00") {
                    editActivityReview(2, 0);
                } else {
                    requestFlag = false;
                    errorTip(data.Message);
                }
            },
            error:function(){
                errorTip('网络错误');
                requestFlag = false;
            }
        });
    }
    //(活动)审核
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
            success: function(data) {
                console.log("EditActivityReview---", data);
                if (data.Code == "00") {
                    window.location.href = "exchangeResult.html?code=1&tip=活动反馈成功&appId=" + appId;
                } else {
                    errorTip(data.Message);
                }
            },
            error:function(){
                errorTip('网络错误');
            },
            complete:function(){
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