
$(document).ready(function () {
    var token = getCookie('token');
    var userId = getCookie('userId');
    var appId = getRequest("appId");
    var toUserId = getRequest("toUserId");
    var createType = getRequest("createType")
    var imgUrl = "";
    var fileArray = [];
    $("#subord").click(function () {
        window.location.href = "subordinateList.html?appId="+appId;
    });
    //图片上传
    $('#imgupload').fileupload({
        url:UPLOAD,
        dataType: 'json',
        done: function(e, data) {
            if(data.result.Code=="00"){
                var array = data.result.Result.FileUploadResults;
                console.log(data.result.Result.FileUploadResults);
                imgUrl = array[0].FileUrl;
                $(".add").html(`<img src="${imgUrl}" class="upload-img">`);
            }else{
                alert("上传失败");
            }
        }
    });
    //附件上传fileupload
    $('#fileupload').fileupload({
        url:UPLOAD,        
        dataType: 'json',
        done: function(e, data) {
            var array = data.result.Result.FileUploadResults;
            var object = {
                "AttachmentType":array[0].FileType,
                "AttachmentDownloadUrl": array[0].FileUrl
              }
            fileArray.push(object);
            $(".attaches").html(attachmentList(fileArray));
        }
    });
    $(".sub-btn").click(function(){
        createActivity();
    });
    //发布活动
    function createActivity() {
        var data = {
            "ActivityType": 0,
            "ActivityName": $("#title").val(),
            "ActivityFromUser": userId,
            "ActivityToUserId": toUserId,
            "ActivityBeginDate": "2018-07-08",
            "ActivityEndDate": "2018-07-10",
            "ActivityContent": $("#content").val(),
            "CanComment": 0,
            "IsPublic": 0,
            "ImageUrl":imgUrl,
            "CreateType": createType,
            "AttachList":fileArray,
            "Token":token,
            "AppId": appId
          };
        console.log(data,JSON.stringify(data));
        $.ajax({
            type: 'POST',
            url: DOMAIN + '/api/Activity/CreateActivity',
            data: data,
            success: function (data) {
                console.log('CreateActivity---', data);
                if (data.Code == "00") {
                    window.location.href = 'exchangeResult.html?code=1&tip=活动创建成功';
                } else {
                    alert(data.Code, data.Message);
                }
            }
        })
    }
});