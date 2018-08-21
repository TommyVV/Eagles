$(document).ready(function() {
    var token = getCookie('token');
    var userId = getCookie('userId');
    var appId = getRequest("appId");
    var pageType = getRequest("type");
    getUserRelationship();
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
                    $(".list").html(dealRelationList(data.Result.UserList));
                    $(".subordinate-item").click(function() {
                        var id = $(this).attr("id");
                        var name = $($(this).find('span')).text();
                        var arr = id.split("-");
                        window.location.href = "publishTask.html?toUserId=" + arr[0] + "&createType=" + arr[1] + "&appId=" + appId + "&type=" + pageType+"&name="+name;
                    });
                } else {
                  
                }
            }
        })
    }

    function dealRelationList(list) {
        var content = ``;
        list.forEach(element => {
            content += `<div class="subordinate-item" id="${element.UserId}-${element.IsLeader==true?'1':'0'}">
            <span>${element.Name==null?element.UserId:element.Name}</span>
            <div class="right-dir">
                <span class="glyphicon glyphicon-menu-right" aria-hidden="true"></span>
            </div>
        </div>`;
    });
        return content;
    }
});