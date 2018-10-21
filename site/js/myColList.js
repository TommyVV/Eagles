$(document).ready(function() {
    var token = getCookie('token');
    var appId = getRequest("appId");
    if (!token) {
        window.location.href = 'login.html?appId=' + appId + '';
    }
    $('#top-nav').html('');
    $('#top-nav').load('./head.html');
    var currentItemType = '0';
    var pageIndex = 1;
    var pageSize = 10;
    var mescroll;
    mescroll = new MeScroll("mescroll", {
        down: {
            auto: false,
            callback: downCallback,
        },
        up: {
            callback: getMeeting,
            isBounce: false
        }
    });

    function downCallback() {
        pageIndex = 1;
        $(".item").html('')
        getMeeting();
    }
    //查询会议
    function getMeeting() {
        $.ajax({
            type: 'POST',
            url: DOMAIN + '/api/User/GetUserCollectLog',
            data: {
                "CollectType":"1",
                "Token": token,
                "AppId": appId,
                "PageSize": pageSize,
                "PageIndex": pageIndex
            },
            success: function(data) {
                console.log('GetMeeting---', data);
                if (data.Code == "00") {
                    var list = data.Result.CollectsList;
                    $(".item").append(dealReturnList(list));
                    if (list.length < pageSize) {
                        mescroll.endSuccess(100000, false, null);
                    } else {
                        pageIndex = pageIndex + 1;
                        mescroll.endSuccess(100000, true, null);
                    }
                    //给每列数据绑定事件
                    $(".article").click(function() {
                        var newsId = $(this).attr('id');
                        window.location.href = newsId;
                    });
                } else if (data.Code == '10') {
                    mescroll.endSuccess(10, false, null);
                } else {
                    errorTip(data.Message);
                    mescroll.endErr();
                }
            },
            error: function() {
                errorTip('网络错误');
                mescroll.endErr();
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
    //处理返回数据
    function dealReturnList(list) {
        var content = '';
        list.forEach(element => {
            if(element.ImageUrl){
                content += `<div class="article" id="${element.NewsUrl}">
                <div class="left">
                    <img src="${element.ImageUrl}"
                        alt="">
                </div>
                <div class="right">
                    <div class="content overflow-two-line">
                        ${element.Title}
                    </div>
                    <div class="date">
                        ${element.CreateTime}
                    </div>
                </div>
            </div>`; 
            }else{
                content += `<div class="article" id="${element.NewsUrl}">
                <div class="right">
                    <div class="content overflow-two-line">
                        ${element.Title}
                    </div>
                    <div class="date">
                        发布人:${element.Author}
                    </div>
                    <div class="date">
                        收藏日期:${element.CreateTime}
                    </div>
                </div>
            </div>`;
            }
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