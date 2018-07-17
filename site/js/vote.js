$(document).ready(function() {
    var token = getCookie("token");
    var userId = getCookie("userId");
    var appId = getRequest("appId");
    var voteId = getRequest("testId");
    var activityId = getRequest("activityId");
    $("#top-nav").html('');
    $("#top-nav").load("head.html", () => {});
    var voteItem = {};
    var joinFlag = 0;
    var requestFlag = false;
    getIsJoinTest();
    //提交按钮
    $('.sub-btn').click(function() {
        if (requestFlag) {
            return;
        }
        var length = $('.select').length;
        if (length > 0) {
            var answerList = voteItem.AnswerList;
            var answers = [];
            var options = $('.s-flag');
            for (var i = 0; i < options.length; i++) {
                if ($(options[i]).hasClass('select')) {
                    answers.push({
                        "AnswerId": answerList[i].AnswerId,
                        "CustomizeAnswer": answerList[i].Answer
                    })
                }
            }
            testPaperAnswer(answers);
        }
    });
    //查询用户是否参加过该活动
    function getIsJoinTest() {
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/TestPaper/GetIsJoinTest",
            data: {
                "ActivityId": activityId,
                "TestId": voteId,
                "Token": token,
                "AppId": appId
            },
            success: function(data) {
                console.log("GetIsJoinTest---", data);
                if (data.Code == "00") {
                    var joinFlag = data.Result.IsJoin;
                    if (joinFlag == '1') {
                        $('.sub-btn').removeClass('hide');
                    } else {
                        bootoast({
                            message: '您已参加过该活动,不能重复参加',
                            type: 'warning',
                            position: 'toast-top-center',
                            timeout: 3
                        });
                    }
                    getTestPaper();
                } else {
                    bootoast({
                        message: '' + data.Message + '',
                        type: 'warning',
                        position: 'toast-top-center',
                        timeout: 3
                    });
                }
            }
        });
    }
    //查询投票活动
    function getTestPaper() {
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/TestPaper/GetTestPaper",
            data: {
                "TestId": voteId,
                "Token": token,
                "AppId": appId
            },
            success: function(data) {
                console.log("GetTestPaper---", data);
                if (data.Code == "00") {
                    $('.main_content').removeClass('hide');
                    voteInfoShow(data.Result);
                } else {
                    bootoast({
                        message: '' + data.Message + '',
                        type: 'warning',
                        position: 'toast-top-center',
                        timeout: 3
                    });
                }
            }
        });
    }
    //提交投票活动
    function testPaperAnswer(answers) {
        requestFlag = true;
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/TestPaper/TestPaperAnswer",
            data: {
                "TestId": voteId,
                "UseTime": 0,
                "TestList": [{
                    "QuestionId": voteItem.QuestionId,
                    "Answers": answers
                }],
                "Token": token,
                "AppId": appId
            },
            success: function(data) {
                console.log("TestPaperAnswer---", data);
                if (data.Code == "00") {
                    bootoast({
                        message: '问卷提交成功:',
                        type: 'success',
                        position: 'toast-top-center',
                        timeout: 3
                    });
                } else {
                    bootoast({
                        message: '问卷提交失败:' + data.Message + '',
                        type: 'warning',
                        position: 'toast-top-center',
                        timeout: 3
                    });
                }
            },
            complete: function() {
                requestFlag = false;
            }
        });
    }
    //页面展示投票信息
    function voteInfoShow(data) {
        var voteType = data.IsImageVote;
        var answerStr = ``;
        voteItem = data.TestList[0];
        var multiple = voteItem.Multiple;
        var multipleCount = voteItem.MultipleCount;
        //投票描述
        var headStr = `<h4 class="title">${data.TestPaperTitle}</h4>
        <div class="endTime">截止时间:2018-02-06</div>
        <div class="num">参与人数:${data.UserCount}</div>`;
        $('.header').html(headStr);
        var contentStr = `<p class="content">${data.HtmlContent}</p>`;
        $('.content-box').html(contentStr);
        var joinRatio = function(answerUserCount) {
            if (data.UserCount == 0) {
                return 0
            } else {
                return parseInt(answerUserCount * 100 / data.UserCount);
            }
        };
        //投票区
        var list = voteItem.AnswerList;
        if (voteType) {
            //是图片类型
            answerStr += `<div class="img-options">`
            list.forEach(element => {
                answerStr += `<div class="img-option" id="${element.AnswerId}">
                                <div class="option-name">
                                    <img class="s-flag" src="icons/sel_no@2x.png" alt="">&nbsp;${element.Answer}
                                </div>
                                <img class="a-img" src="${element.ImageUrl}" alt="" srcset="">
                                <div class="out-ratio">
                                    <div class="in-ratio" style="width:${joinRatio(element.UserCount)}%;"></div>
                                </div>
                                <div class="ratio-text">${joinRatio(element.UserCount)}%(${element.UserCount}人)</div>
                            </div>`
            });
            answerStr += `</div>`;
            var voteStr = `<div class="item">
                                <div class="item-title">
                                    ${voteItem.Question}
                                </div>
                                ${answerStr}
                            </div>`;
            $('.vote-box').html(voteStr);
            if (joinFlag == '1') {
                $('.img-option').click(function() {
                    var obj = $(this).find('.option-name').find('img');
                    console.log($(obj).attr('class'));
                    var selectFlag = $(obj).hasClass('select');
                    if (selectFlag == false) {
                        if (multiple == 1) {
                            //允许多选
                            var selectCount = $('.select').length;
                            if (selectCount == multipleCount) {
                                return;
                            } else {
                                $(obj).addClass('select');
                                $(obj).attr('src', 'icons/sel_yes@2x.png');
                            }
                        } else {
                            $(".select").removeClass('select');
                            $(obj).addClass('select');
                            $(obj).attr('src', 'icons/sel_yes@2x.png');
                        }
                    } else {
                        $(obj).removeClass('select');
                        $(obj).attr('src', 'icons/sel_no@2x.png');
                    }
                });
            }
        } else {
            //非图片类型
            answerStr += `<div class="options">`
            list.forEach(element => {
                answerStr += `<div class="option" id="${element.AnswerId}">
                                <div class="flag">
                                     <img class="s-flag" src="icons/sel_no@2x.png" alt="">
                                </div>
                                <div class="c-area">
                                    <div class="content">
                                        <div>${element.Answer}</div>
                                        <div class="num">${element.UserCount}人</div>
                                    </div>
                                    <div class="out-ratio">
                                        <div class="in-ratio" style="width:${joinRatio(element.UserCount)}%;">${joinRatio(element.UserCount)}%</div>
                                    </div>
                                </div>
                            </div>`
            });
            answerStr += `</div>`;
            var voteStr = `<div class="item">
                                <div class="item-title">
                                    ${voteItem.Question}
                                </div>
                                ${answerStr}
                            </div>`;
            $('.vote-box').html(voteStr);
            if (joinFlag == '1') {
                $('.option').click(function() {
                    var obj = $(this).find('.flag').find('img');
                    console.log($(obj).attr('class'));
                    var selectFlag = $(obj).hasClass('select');
                    if (selectFlag == false) {
                        if (multiple == 1) {
                            //允许多选
                            var selectCount = $('.select').length;
                            if (selectCount == multipleCount) {
                                return;
                            } else {
                                $(obj).addClass('select');
                                $(obj).attr('src', 'icons/sel_yes@2x.png');
                            }
                        } else {
                            $(".select").removeClass('select');
                            $(obj).addClass('select');
                            $(obj).attr('src', 'icons/sel_yes@2x.png');
                        }
                    } else {
                        $(obj).removeClass('select');
                        $(obj).attr('src', 'icons/sel_no@2x.png');
                    }
                });
            }
        }

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