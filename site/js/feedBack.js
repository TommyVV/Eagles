
$(document).ready(function () {
    var token = getCookie('token');
    var userId = getCookie('userId');
    var appId = getRequest("appId");
    var activityId = getRequest("activityId")
    if (!(token && userId && appId)) {
        alert("参数有误");
    }
    var fileArray = [];
    //附件上传fileupload
    $('#fileupload').fileupload({
        url: UPLOAD,
        dataType: 'json',
        done: function (e, data) {
            var array = data.result.Result.FileUploadResults;
            var object = {
                "AttachName": array[0].FileName,
                "AttachmentDownloadUrl": array[0].FileUrl
            }
            fileArray.push(object);
            $(".attaches").html(attachmentList(fileArray));
        }
    });
    $(".sub-btn").click(function () {
        editActivityReview(2, 0);
        //editActivityFeedBack();
    });
    //发布反馈
    function editActivityFeedBack() {
        var data = {
            "ActivityId": activityId,
            "Content": $("#content").val(),
            "AttachList": fileArray,
            "Token": token,
            "AppId": appId
        };
        console.log(data, JSON.stringify(data));
        $.ajax({
            type: 'POST',
            url: DOMAIN + '/api/Activity/EditActivityFeedBack',
            data: data,
            success: function (data) {
                console.log('CreateActivity---', data);
                if (data.Code == "00") {
                    editActivityReview(2, 0);
                } else {
                    alert(data.Code, data.Message);
                }
            }
        })
    }
    //审核任务
    function editActivityReview(type, rType) {
        $.ajax({
            type: 'POST',
            url: DOMAIN + '/api/Activity/EditActivityReview',
            data: {
                "Type": type,
                "ActivityId": activityId,
                "ReviewType": rType,
                "Token": token,
                "AppId": appId
            },
            success: function (data) {
                console.log('EditActivityReview---', data);
                if (data.Code == "00") {
                    window.location.href = 'exchangeResult.html?code=1&tip=活动反馈成功';
                } else {
                    alert(data.Code, data.Message);
                }
            }
        })
    }
});