$(document).ready(function() {
    var token = getCookie("token");
    var userId = getCookie("userId");
    var appId = getRequest("appId");
    var pubType = getRequest("type");
    var activityId = getRequest("activityId");
    if(!pubType&&!token) {
        window.location.href = 'login.html?appId=' + appId + '';
    }
    $('#top-nav').html('');
    $('#top-nav').load('./head.html');
    var userType = 1;
    //查询活动详情
    getActivityDetail();
    //查询活动详情
    function getActivityDetail() {
        $(".activity-content").addClass("hide");
        var requestUrl="";
        if(pubType){
           requestUrl="/api/Activity/GetPublicActivityDetail"; 
        }else{
           requestUrl="/api/Activity/GetActivityDetail";  
        }
        $.ajax({
            type: "POST",
            url: DOMAIN + requestUrl,
            data: {
                ActivityId: activityId,
                Token: token,
                AppId: appId
            },
            success: function(data) {
                console.log(requestUrl,"---", data);
                if (data.Code == "00") {
                    var result = data.Result;
                    $(".main_content").html(showContent(result));
                    $(".names").hide();
                    var status = result.ActivityStatus;
                    userActivityStatus(result);
                    //展开折叠报名人数
                    $(".open-names").click(function() {
                        console.log($(".name-flag").attr("class"));
                        if ($(".name-flag").hasClass("glyphicon-menu-left")) {
                            $(".name-flag")
                                .removeClass("glyphicon-menu-left")
                                .addClass("glyphicon-menu-down");
                            $(".names").show();
                        } else {
                            $(".name-flag")
                                .removeClass("glyphicon-menu-down")
                                .addClass("glyphicon-menu-left");
                            $(".names").hide();
                        }
                    });
                } else {
                    errorTip(data.Message);
                }
            },
            error:function(){
                errorTip('网络错误');
            }
        });
    }
    //判断当前用户的活动状态
    function userActivityStatus(result) {
        var status = result.ActivityStatus;
        var joinList = result.ActivityJoinPeopleList;
        var createType = result.CreateType;
        //发起人
        var initiateUserId = result.InitiateUserId;
        var acceptUserId = result.AcceptUserId;
        var auditUserId = result.AuditUserId;
        if (status == -1) {
            //下级创建的活动
            if (userId == auditUserId) {
                //如果当前是上级，审核
                $("#btn-area").html(`<div class="pass">通过</div>
                <div class="nopass">不通过</div>`);
                // Type  01-上级审核任务 02-下级接受任务 03-下级申请完成
                // ReviewType  审核状态 0-通过 1-不通过
                $(".pass").click(function() {
                    editActivityReview(1, 0);
                });
                $(".nopass").click(function() {
                    editActivityReview(1, 1);
                });
            }
        } else if (status == 0) {
            //下级创建
            var exist = false;
            if (joinList) {
                var resultArray = joinList.find(function(element) {
                    return element.UserId == userId;
                });
                if (resultArray) {
                    exist = true;
                }
            }
            if (!exist) {
                $("#btn-area").html(`<div class="sub-btn">我要报名</div>`);
                $(".sub-btn").click(function() {
                    editActivityJoin();
                });
            } else {
                //负责人
                if(userId==acceptUserId){
                    $("#btn-area").html(`<div class="sub-btn">我要反馈</div>`);
                    $(".sub-btn").click(function() {
                        window.location.href =
                            "feedBack.html?pageType=0&appId=" + appId + "&activityId=" + activityId;
                    });
                }
            }
        } else if (status == 1) {
            //申请完成
            if(userId==auditUserId){
                //查询反馈信息
                GetActivityFeedBack(1);
            }else if(userId==acceptUserId){
                GetActivityFeedBack();
            }
        } else if (status == 2) {
            //活动完成
            //1）查询反馈信息
            GetActivityFeedBack(2);
            //评论区域
            if(userId==auditUserId){
                userType = 0;
            }
            var comment = new Comment({
                commentType: "1",
                id: activityId,
                userType: userType,
                appId: appId,
                token: token,
                userId: userId
            });
            comment.getUserComment();
        }
    }
    //参与活动
    function editActivityJoin() {
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Activity/EditActivityJoin",
            data: {
                ActivityId: activityId,
                Token: token,
                AppId: appId
            },
            success: function(data) {
                console.log("EditActivityJoin---", data);
                if (data.Code == "00") {
                    getActivityDetail();
                } else {
                    errorTip(data.Message);
                }
            },
            error:function(){
                errorTip('网络错误');
            }
        });
    }
    //审核任务
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
                    getActivityDetail();
                } else {
                    errorTip(data.Message);
                }
            },
            error:function(){
                errorTip('网络错误');
            }
        });
    }
    //活动反馈查询
    function GetActivityFeedBack(status) {
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Activity/GetActivityFeedBack",
            data: {
                ActivityId: activityId,
                Token: token,
                AppId: appId
            },
            success: function(data) {
                console.log("GetActivityFeedBack---", data);
                if (data.Code == "00") {
                    showActivityResult(data.Result, status);
                }else if(data.Code=="10"){

                }else {
                    errorTip(data.Message);
                }
            },
            error:function(){
                errorTip('网络错误');
            }
        });
    }
    //活动完成
    function editActivityComplete(type) {
        var ele = $('.select');
        if(ele.length==0){
            errorTip('请选择公示方式');
            return;
        }
        var data = {
            ActivityId: activityId,
            CompleteStatus: type,
            IsPublic: $('.select').attr('id').split('-')[1],
            Token: token,
            AppId: appId
        };
        // console.log(data, JSON.stringify(data));
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/Activity/EditActivityComplete",
            data: data,
            success: function(data) {
                console.log("EditActivityComplete---", data);
                if (data.Code == "00") {
                    getActivityDetail();
                } else {
                    errorTip(data.Message);
                }
            },
            error:function(){
                errorTip('网络错误');
            }
        });
    }
    //活动结果展示
    function showActivityResult(result, status) {
        //status 1 申请完成 2 完成
        var list = result.FeedBackList;
        var str = `<div class="title">活动结果</div>`;
        list.forEach(function(element) {
            str += `<div class="content">
            <div class="name">${element.UserName}</div>
            <div class="txt">${element.UserFeedBack}</div>
            </div>
            <div class="attaches">
                ${attachmentList(element.AttachList,0)}
            </div>
            <div class="result-time">${element.FeedBackTime}</div>`;
        });
        if (status == 1) {
            str += `<div class="pub-area">
                    <div class="item" id="pub-1">
                        <img class="pub-flag" src="icons/sel_no@2x.png" alt="">不公示
                    </div>
                    <div class="item" id="pub-2">
                        <img class="pub-flag" src="icons/sel_no@2x.png" alt="">公示到支部
                    </div>
                    <div class="item" id="pub-3">
                        <img class="pub-flag" src="icons/sel_no@2x.png" alt="">公示到组织
                    </div>
                </div>
                <div class="activity-result">
                        <div class="pass">通过</div>
                        <div class="nopass">不通过</div>
                    </div>`;
            $(".activity-content").removeClass('hide').html(str);
            $(".pub-area .item").click(function() {
                $('.item').removeClass('select');
                var options = $('.item').find('img');
                $(options).attr('src', 'icons/sel_no@2x.png');
                $(this).addClass('select');
                $($(this).find('img')).attr('src', 'icons/sel_yes@2x.png');
            });
            $(".activity-content .pass").click(function() {
                editActivityComplete(0);
            });
            $(".nopass").click(function() {
                editActivityComplete(1);
            });
        } else {
            $(".activity-content").removeClass('hide').html(str);
        }

    }
    function errorTip(str){
        bootoast({
            message: ''+str,
            type: 'warning',
            position: 'toast-top-center',
            timeout: 3
        });
    }
    //活动详情展示
    function showContent(data) {
        var str = `<div class="header">
                    ${data.ActivityName}
                </div>
                <div class="content-box">
                    <p class="content">${data.ActivityContent}
                    </p>
                    <p class="content">
                        <img src="${
            data.ActivityImageUrl
            }" class="img-responsive" alt="">
                    </p>
                    ${attachmentList(data.AttachmentList,0)}
                    <div id="btn-area"></div>
                </div>
                <div class="sign-up-area" style="display:${
            data.ActivityStatus > -1 ? "block" : "none"
            }">
                    <div class="area-title">
                        <span>已报名人员(${
            data.ActivityJoinPeopleList
                ? data.ActivityJoinPeopleList.length
                : 0
            })</span>
                        <span class="open-names">展开
                            <span class="name-flag glyphicon glyphicon-menu-left"></span>
                        </span>
                    </div>
                    <div class="names">
                        ${nameList(data.ActivityJoinPeopleList)}
                    </div>
                </div>`;
        return str;
    }
    //名字列表
    function nameList(list) {
        var str = ``;
        if (list) {
            list.forEach(element => {
                str += `<div>${element.Name}</div>`;
            });
        }
        return str;
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