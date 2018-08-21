$(document).ready(function () {
    var token = getCookie("token");
    var userId = getCookie("userId");
    var appId = getRequest("appId");
    var updateId = getRequest("updateId");
    if (!token) {
        window.location.href = 'login.html?appId=' + appId + '';
    }
    var requestFlag = false;
    //指派人员
    var toUserId = '';
    var toUserName = '';
    //加载导航
    $("#top-nav").html("");
    $("#top-nav").load("head.html", () => { });
    //pageType 0 活动  1 任务
    var pageType = getRequest("type");
    var imgUrl = "";
    var fileArray = [];
    //初始化页面提示语
    var titlePlaceholder = pageType == "0" ? "活动标题" : "任务标题";
    $("#title").attr("placeholder", titlePlaceholder);
    var btnText = pageType == "0" ? "发布活动" : "发布任务";
    $('.sub-btn').html(btnText);
    var textareaPlaceholder = pageType == "0" ? "快让大家知道你的活动是什么吧~" : "快让大家知道你的任务是什么吧~";
    $('.plan-text').attr("placeholder", textareaPlaceholder);

    if (pageType == 1) {
        //任务
        $("#imgUpload").hide();
        //活动
        if(updateId){
            getTaskDetail(updateId);
        }else{
            //查询关系列表
            getUserRelationship();
        }
        
    }else{
        //活动
        if(updateId){
            getActivityDetail(updateId);
        }else{
            //查询关系列表
            getUserRelationship();
        }
    }

    //查询任务详情
    function getTaskDetail(id) {
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Task/GetTaskDetail",
            data: {
                TaskId: id,
                Token: token,
                AppId: appId
            },
            success: function (data) {
                console.log("GetTaskDetail---", data);
                if (data.Code == "00") {
                    var result = data.Result;
                    fillTaskData(result);
                    //查询关系列表
                    getUserRelationship();
                } else {
                    errorTip(data.Message);
                }
            },
            error: function () {
                errorTip("网络错误");
            },
            complete: function () {
                
            }
        });
    }
    function fillTaskData(result){
        $("#title").val(result.TaskName);
        toUserId = result.AcceptUserId; 
        $("#name").html(result.AcceptUserName);
        $("#start").val(dealDate(result.TaskBeginDate));
        $("#end").val(dealDate(result.TaskEndDate));
        $("#content").val(result.TaskContent);
        fileArray = delEmptyElement(result.AttachmentList);
        dealAttachment();
    }
    //查询活动详情
    function getActivityDetail(id) {
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Activity/GetActivityDetail",
            data: {
                ActivityId: id,
                Token: token,
                AppId: appId
            },
            success: function(data) {
                console.log("GetActivityDetail---", data);
                if (data.Code == "00") {
                    var result = data.Result;
                    fillActivityData(result);
                    //查询关系列表
                    getUserRelationship();
                } else {
                    errorTip(data.Message);
                }
            },
            error:function(){
                errorTip("网络错误");
            }
        });
    }
    function fillActivityData(result){
        $("#title").val(result.ActivityName);
        toUserId = result.AcceptUserId; 
        $("#name").html(result.AcceptUserName);
        imgUrl=result.ActivityImageUrl;
        $(".add").html(`<img src="${imgUrl}" class="upload-img">`);
        $("#start").val(dealDate(result.ActivityBeginTime));
        $("#end").val(dealDate(result.ActivityEndTime));
        $("#content").val(result.ActivityContent);
        fileArray = delEmptyElement(result.AttachmentList);
        dealAttachment();
    }
    //去掉数组中的空
    function delEmptyElement(arr){
        var resultArr = [];
        arr.forEach(function(el){
            if(el.AttachmentDownloadUrl){
                resultArr.push(el);
            }
        });
        return resultArr;
    }
    //错误提示
    function errorTip(str){
        bootoast({
            message: ""+str,
            type: "warning",
            position: "toast-top-center",
            timeout: 2
        });
    }
    function dealDate(date){
       try {
        if(date.length<10){
            return date;
        }else{
            return date.substr(0,10);
        } 
       } catch (error) {
           return "";
       }
        
    }
    // $('#btnTestSaveLarge').on('click', function () {
    //     if($(".subordinate-item").length==0){
    //         $(this).parents('.modal').modal('hide');
    //         return;
    //     }
    //     if (toUserId) {
    //         $("#name").html(toUserName);
    //         $(this).parents('.modal').modal('hide');
    //     } else {
    //         bootoast({
    //             message: "请选择负责人",
    //             type: "info",
    //             position: "toast-top-center",
    //             timeout: 2
    //         });
    //     }
    // });
    //图片上传
    $("#imgupload").fileupload({
        url: UPLOAD,
        dataType: "json",
        done: function (e, data) {
            if (data.result.Code == "00") {
                var array = data.result.Result.FileUploadResults;
                //console.log(data.result.Result.FileUploadResults);
                imgUrl = array[0].FileUrl;
                $(".add").html(`<img src="${imgUrl}" class="upload-img">`);
            } else {
                bootoast({
                    message: "上传失败",
                    type: "warning",
                    position: "toast-top-center",
                    timeout: 2
                });
            }
        }
    });
    //附件上传fileupload
    $("#fileupload").fileupload({
        url: UPLOAD,
        dataType: "json",
        //设置进度条
        progressall: function (e, data) {
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
        done: function (e, data) {
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
                dealAttachment();
            } else {
                console.log(data.result);
                bootoast({
                    message: '' + data.result.Message,
                    type: 'warning',
                    position: 'toast-top-center',
                    timeout: 3
                });
            }
        }
    });
    function dealAttachment(){
        $(".attaches").html(attachmentList(fileArray, 1));
        $(".glyphicon-remove").click(function () {
            var index = $('.glyphicon-remove').index(this);
            fileArray.splice(index, 1);
            $(this).parents('.file').remove();
            $(".upload-file").show();
            console.log('index---', index);
        })
    }
    $(".sub-btn").click(function () {
        if(requestFlag){
            return;
        }
        if (pageType == 0) {
            //活动
            createActivity();
        } else if (pageType == 1) {
            //任务
            createTask();
        }
    });
    //查询列表
    function getUserRelationship() {
        $.ajax({
            type: 'POST',
            url: DOMAIN + '/api/User/GetUserRelationship',
            data: {
                "Type": 1,
                "UserId": userId,
                "Token": token,
                "AppId": appId
            },
            success: function (data) {
                console.log('GetUserRelationship---', data);
                if (data.Code == "00") {
                    var list = data.Result.UserList;
                    dealRelationList(list);
                    if(list.length==0){
                        bootoast({
                            message: '当前用户未查询到可用负责人',
                            type: 'warning',
                            position: 'toast-top-center',
                            timeout: 3
                        });
                    }
                } else {
                    bootoast({
                        message: '' + data.Message,
                        type: 'warning',
                        position: 'toast-top-center',
                        timeout: 3
                    });
                }
            }
        })
    }

    function dealRelationList(list) {
        var content=``;
        if(list.length==0){
            content+=`<div>当前用户未查询到可用负责人</div>`;
            $(".modal-body").html(content);
            return;
        }
        content = `<div class="subordinates"><div class="subordinate-item subordinate-title">
        <div class="name">姓名</div>
        <div class="branch">支部名称</div><div class="right-dir"></div></div>`;
        list.forEach(element => {
            content += `<div class="subordinate-item" id="${element.UserId}">
                <div class="name">${element.Name}</div>
                <div class="branch">${element.BranchName}</div>
                <div class="right-dir">
                    <span class="glyphicon ${element.UserId==toUserId?'glyphicon-ok':''}" aria-hidden="true"></span>
                </div>
            </div>`;
        });
        content += `</div>`;
        $(".modal-body").html(content);
        $(".subordinate-item").click(function () {
            var index = $(".subordinate-item").index(this);
            if(index==0){
                return;
            }
            //console.log('index----',index);
            $('.subordinates .glyphicon').removeClass('glyphicon-ok');
            $($(this).find('.glyphicon')).addClass('glyphicon-ok');
            toUserId = $(this).attr("id");
            toUserName = $($(this).find('div')[0]).text();
            $("#name").html(toUserName);
            $('#myModal').modal('hide');
        });
    }

    //发布任务
    function createTask() {
        if (!validCheck()) {
            return;
        }
        requestFlag = true;
        var data = {
            TaskId:updateId,
            TaskName: $("#title").val(),
            TaskFromUser: userId,
            TaskToUserId: toUserId,
            TaskBeginDate: $("#start").val(),
            TaskEndDate: $("#end").val(),
            TaskContent: $("#content").val(),
            AttachList: fileArray,
            Token: token,
            AppId: appId
        };
        //console.log(data, JSON.stringify(data));
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Task/CreateTask",
            data: data,
            success: function (data) {
                console.log("CreateTask---", data);
                if (data.Code == "00") {
                    window.location.href = "exchangeResult.html?code=1&tip=任务创建成功&appId=" + appId + "&cb=task.html";
                } else {
                    bootoast({
                        message: "活动创建失败:" + data.Message + "",
                        type: "warning",
                        position: "toast-top-center",
                        timeout: 3
                    });
                }
            },
            complete:function(){
                requestFlag = false;
            }
        });
    }
    //发布活动
    function createActivity() {
        if (!validCheck()) {
            return;
        }
        if (!imgUrl) {
            bootoast({
                message: "请上传图片",
                type: "info",
                position: "toast-top-center",
                timeout: 2
            });
            return;
        }
        requestFlag = true;
        var data = {
            ActivityId:updateId,
            ActivityType: 0,
            ActivityName: $("#title").val(),
            ActivityFromUser: userId,
            ActivityToUserId: toUserId,
            ActivityBeginDate: $("#start").val(),
            ActivityEndDate: $("#end").val(),
            ActivityContent: $("#content").val(),
            CanComment: 0,
            ImageUrl: imgUrl,
            AttachList: fileArray,
            Token: token,
            AppId: appId
        };
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Activity/CreateActivity",
            data: data,
            success: function (data) {
                console.log("CreateActivity---", data);
                if (data.Code == "00") {
                    window.location.href = "exchangeResult.html?code=1&tip=活动创建成功&appId=" + appId + "&cb=activityList.html";
                } else {
                    bootoast({
                        message: "任务创建失败" + data.Message + "",
                        type: "warning",
                        position: "toast-top-center",
                        timeout: 3
                    });
                }
            },
            complete:function(){
                requestFlag = false;
            }
        });
    }

    function validCheck() {
        var title = $("#title").val();
        if (!title) {
            validTip("请填写标题");
            return false;
        }
        if (!toUserId) {
            validTip("请选择指派人员");
            return false;
        }
        var start = $("#start").val();
        if (!start) {
            validTip("请选择开始时间");
            return false;
        }
        var end = $("#end").val();
        if (!end) {
            validTip("请选择结束时间");
            return false;
        }
        var startDate = start.replace(/\-/g, "");
        var endDate = end.replace(/\-/g, "");
        var nowDate = $("#nowDate").val();
        if(startDate<nowDate){
            validTip("活动开始日期早于当前日期");
            return false;
        }
        if (startDate > endDate) {
            validTip("活动开始日期晚于结束日期");
            return false;
        }
        var content = $("#content").val();
        if (!content) {
            validTip("内容不能为空");
            return false;
        }
        return true;
    }

    function validTip(str) {
        bootoast({
            message: str,
            type: "info",
            position: "toast-top-center",
            timeout: 2
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
                $(".container").addClass("pc-wrap");
            } else {
                $(".mobile").show();
                $(".pc").hide();
                $("body").css("background-color", "#fff");
                $(".container").removeClass("pc-wrap");
            }
        }
    }
    new CalculateScreen();

    $(window).resize(function () {
        new CalculateScreen();
    });
});