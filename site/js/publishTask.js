$(document).ready(function() {
    var token = getCookie("token");
    var userId = getCookie("userId");
    var appId = getRequest("appId");
        if(!token) {
        window.location.href = 'login.html?appId=' + appId + '';
    }
    //指派人员
    var toUserId = '';
    var toUserName = '';
    //指派人员信息
    var toUsreInfo = '';
    //创建类型
    var createType = '';
    //加载导航
    $("#top-nav").html("");
    $("#top-nav").load("head.html", () => {});
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
        $("#imgUpload").hide();
    }
    //查询关系列表
    getUserRelationship();
    $('#btnTestSaveLarge').on('click', function() {
        if (toUsreInfo) {
            $("#name").html(toUserName);
            var arr = toUsreInfo.split("-");
            toUserId = arr[0];
            createType = arr[1];
            $(this).parents('.modal').modal('hide');
        } else {
            bootoast({
                message: "请选择指派人员",
                type: "info",
                position: "toast-top-center",
                timeout: 2
            });
        }
    });
    //是否公开
    $(".flag-area").click(function() {
        if ($(".pub-flag").hasClass("select")) {
            $(".pub-flag")
                .attr("src", "icons/sel_no@2x.png")
                .removeClass("select");
        } else {
            $(".pub-flag")
                .attr("src", "icons/sel_yes@2x.png")
                .addClass("select");
        }
    });
    //图片上传
    $("#imgupload").fileupload({
        url: UPLOAD,
        dataType: "json",
        done: function(e, data) {
            if (data.result.Code == "00") {
                var array = data.result.Result.FileUploadResults;
                console.log(data.result.Result.FileUploadResults);
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
                "Type": 0,
                "UserId": userId,
                "Token": token,
                "AppId": appId
            },
            success: function(data) {
                console.log('GetUserRelationship---', data);
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
        })
    }

    function dealRelationList(list) {
        var content = `<div class="subordinates">`;
        list.forEach(element => {
            content += `<div class="subordinate-item" id="${element.UserId}-${element.IsLeader==true?'1':'0'}">
                <span>${element.Name==null?element.UserId:element.Name}</span>
                <div class="right-dir">
                    <span class="glyphicon" aria-hidden="true"></span>
                </div>
            </div>`;
        });
        content += `</div>`;
        $(".modal-body").html(content);
        $(".subordinate-item").click(function() {
            $('.subordinates .glyphicon').removeClass('glyphicon-ok');
            $($(this).find('.glyphicon')).addClass('glyphicon-ok');
            toUsreInfo = $(this).attr("id");
            toUserName = $($(this).find('span')).text();
        });
    }

    //发布任务
    function createTask() {
        if (!validCheck()) {
            return;
        }
        var pubFlag = $(".pub-flag").hasClass("select");
        var data = {
            TaskName: $("#title").val(),
            TaskFromUser: userId,
            TaskToUserId: toUserId,
            TaskBeginDate: $("#start").val(),
            TaskEndDate: $("#end").val(),
            TaskContent: $("#content").val(),
            CanComment: 0,
            IsPublic: pubFlag == true ? 0 : 1,
            CreateType: createType,
            AttachList: fileArray,
            Token: token,
            AppId: appId
        };
        console.log(data, JSON.stringify(data));
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Task/CreateTask",
            data: data,
            success: function(data) {
                console.log("CreateActivity---", data);
                if (data.Code == "00") {
                    window.location.href = "exchangeResult.html?code=1&tip=任务创建成功&appId=" + appId;
                } else {
                    bootoast({
                        message: "活动创建失败:" + data.Message + "",
                        type: "warning",
                        position: "toast-top-center",
                        timeout: 3
                    });
                }
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
        var data = {
            ActivityType: 0,
            ActivityName: $("#title").val(),
            ActivityFromUser: userId,
            ActivityToUserId: toUserId,
            ActivityBeginDate: $("#start").val(),
            ActivityEndDate: $("#end").val(),
            ActivityContent: $("#content").val(),
            CanComment: 0,
            IsPublic: 0,
            ImageUrl: imgUrl,
            CreateType: createType,
            AttachList: fileArray,
            Token: token,
            AppId: appId
        };
        console.log(data, JSON.stringify(data));
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Activity/CreateActivity",
            data: data,
            success: function(data) {
                console.log("CreateActivity---", data);
                if (data.Code == "00") {
                    window.location.href = "exchangeResult.html?code=1&tip=活动创建成功&appId=" + appId;
                } else {
                    bootoast({
                        message: "任务创建失败" + data.Message + "",
                        type: "warning",
                        position: "toast-top-center",
                        timeout: 3
                    });
                }
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
                $("#footer").load("footer.html", () => {});
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

    $(window).resize(function() {
        new CalculateScreen();
    });
});