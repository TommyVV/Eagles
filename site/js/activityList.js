$(document).ready(function () {
    var token = getCookie('token');
    var appId = getRequest("appId");
    var currentItemType = '0';

    //查询所有活动
    getActivityList('0');
    //头部按钮点击
    $(".activity-cate").click(function () {
        var id = $(this).attr('id');
        if (id != currentItemType) {
            currentItemType=id;
            $(".activity-cate").find("span").removeClass("select");
            $(this).find("span").addClass("select");
            getActivityList(id);
        }

    });

    //查询活动
    function getActivityList(type) {
        $.ajax({
            type: 'POST',
            url: DOMAIN + '/api/Activity/GetActivityList',
            data: {
                "ActivityType": type,
                "ActivityPage": 0,
                "Token":token,
                "AppId": appId
              },
            success: function (data) {
                console.log('GetActivityList---', data);
                if (data.Code == "00") {
                    var list = data.Result.ActivityList;
                    $(".item").html(dealReturnList(list));
                    //给每列数据绑定事件
                    $(".article").click(function () {
                        var par = $(this).attr('id');
                        var tmpArray = par.split('_');
                        console.log(tmpArray);
                        var id = tmpArray[0];
                        var type = tmpArray[1];
                        if(type=='0'){
                            //报名活动
                            window.location.href = "activityDetail.html?appId="+appId+"&activityId=" + id;
                        }else if(type=='1'){
                            //投票活动
                        }else if(type=='2'){
                            //问卷调查
                        }
                    });
                } else {
                    alert(data.Code, data.Message);
                }
            }
        })
    }

});





//处理返回数据
function dealReturnList(list) {
    var content = '';
    list.forEach(element => {
        content += `<div class="article" id="${element.ActivityId}_${element.ActivityType}">
                    <div class="left">
                        <img src="${element.ImgUrl}"
                            alt="">
                    </div>
                    <div class="right">
                        <div class="content overflow-two-line">
                            ${element.ActivityName}
                        </div>
                        <div class="date">
                            ${element.ActivityDate.substr(0,10)}
                        </div>
                    </div>
                </div>`;
    });
    return content;
}