$(document).ready(function() {
    var token = getCookie("token");
    var userId = getCookie("userId");
    var appId = getRequest("appId");
    var toUserId = getRequest("toUserId");
    var createType = getRequest("createType");
    var name = getRequest("name");
    //pageType 0 活动  1 任务
    var pageType = getRequest("type");
    var imgUrl = "";
    var fileArray = [];
    $("#name").html(name);
    var placeholder = pageType == "0" ? "活动标题" : "任务标题";
    $("#title").attr("placeholder", placeholder);
    if (pageType == 1) {
        $("#imgUpload").hide();
    }
    $("#subord").click(function() {
        $(".alert .weui-dialog__title").html("指派人员");
        $(".alert").removeClass("hide");
    });
    //弹框取消
    $(".alert .default").click(function() {
        $(".alert").addClass("hide");
    });
    //弹框确定
    $(".alert .primary").click(function() {
        $(".alert").addClass("hide");
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
                alert("上传失败");
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
                    $("#fileupload").hide();
                }
                $(".attaches").html(attachmentList(fileArray));
            } else {
                console.log(data.result);
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
    //发布任务
    function createTask() {
        var title = $("#title").val();
        if (!title) {
            $.alert("标题不能为空");
            return;
        }
        var start = $("#start").val();
        if (!start) {
            $.alert("开始时间不能为空");
            return;
        }
        var end = $("#end").val();
        if (!end) {
            $.alert("结束时间不能为空");
            return;
        }
        var content = $("#content").val();
        if (!content) {
            $.alert("活动内容不能为空");
            return;
        }
        var pubFlag = $(".pub-flag").hasClass("select");
        var data = {
            TaskName: title,
            TaskFromUser: userId,
            TaskToUserId: toUserId,
            TaskBeginDate: start,
            TaskEndDate: end,
            TaskContent: content,
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
                    window.location.href = "exchangeResult.html?code=1&tip=任务创建成功";
                } else {
                    alert(data.Code, data.Message);
                }
            }
        });
    }
    //发布活动
    function createActivity() {
        var title = $("#title").val();
        if (!title) {
            $.alert("标题不能为空");
            return;
        }
        var start = $("#start").val();
        if (!start) {
            $.alert("开始时间不能为空");
            return;
        }
        var end = $("#end").val();
        if (!end) {
            $.alert("结束时间不能为空");
            return;
        }
        var content = $("#content").val();
        if (!content) {
            $.alert("活动内容不能为空");
            return;
        }
        if (!imgUrl) {
            $.alert("请上传图片");
            return;
        }
        var data = {
            ActivityType: 0,
            ActivityName: title,
            ActivityFromUser: userId,
            ActivityToUserId: toUserId,
            ActivityBeginDate: start,
            ActivityEndDate: end,
            ActivityContent: content,
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
                    window.location.href = "exchangeResult.html?code=1&tip=活动创建成功";
                } else {
                    alert(data.Code, data.Message);
                }
            }
        });
    }
    getUserRelationship();
    //查询列表
    function getUserRelationship() {
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/User/GetUserRelationship",
            data: {
                Type: 0,
                UserId: userId,
                Token: token,
                AppId: appId
            },
            success: function(data) {
                console.log("GetUserRelationship---", data);
                if (data.Code == "00") {
                    $(".relation-list").html(dealRelationList(data.Result.UserList));
                    $(".subordinate-item").click(function() {
                        var id = $(this).attr("id");
                        var name = $($(this).find("span")).text();
                        var arr = id.split("-");
                    });
                } else {
                    alert(data.Code, data.Message);
                }
            }
        });
    }

    function dealRelationList(list) {
        var content = ``;
        list.forEach(element => {
            content += `<div class="subordinate-item" id="${element.UserId}-${
        element.IsLeader == true ? "1" : "0"
      }">
            <span>${element.Name == null ? element.UserId : element.Name}</span>
            <div class="right-dir">
                <span class="glyphicon glyphicon-menu-right" aria-hidden="true"></span>
            </div>
        </div>`;
        });
        return content;
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
                $("#top-nav").load("head.html", () => {});
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