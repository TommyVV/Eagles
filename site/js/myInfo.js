var appId = getRequest('appId');
var token = localStorage.getItem("token")
var onurl = window.location.href
if (!localStorage.getItem('token') || localStorage.getItem('IsInternalUser') == 0) {
    window.location.href = 'login.html?appId=' + appId + '&onurl=' + encodeURI(onurl);
}
$('#top-nav,#mobilenav').load('head.html')
$('#left-nav').load('leftNav.html?appId=' + appId)
$('#pc-footer').load('./footer.html')

myInfo(token, appId) //个人信息
$('.img_float').click(function() {
    console.log(8)
    $('.operate').show();
})

function myInfo(token, appId) {
    $.ajax({
        type: "post",
        data: {
            "Token": token,
            "AppId": appId
        },
        url: DOMAIN + "/api/User/GetUserInfo",
        dataType: "json",
        success: function(res) {

            if (res.Code == 00) {
                if (res.Result != '' && res.Result != null) {
                    var data = res.Result.ResultUserInfo;
                    $('#ld_img,.head-pic').attr('src', data.PhotoUrl);
                    $('.ld_name').text(data.Name);
                    $('#ld_nameinput').attr("aids", data.UserId);
                    $('.ld_sex').text(data.Gender);
                    $('.ld_marital').text(data.Marital);
                    $('.ld_h').text(data.Ethnic);
                    $('.ld_datas').text(data.Birth);
                    $('.ld_des').text(data.Origin);
                    $('.ld_chengs').text(data.OriginAddress);
                    $('.ld_czds').text(data.Address);
                    $('.ld_phone').text(data.Telphone);
                    $('.ld_sfzh').text(data.IdCard);
                    $('.ld_gzd').text(data.Education);
                    $('.ld_byz').text(data.School);
                    $('.ld_bjy').text(data.Employer);
                    $('.ld_dak').text(data.Department);
                    $('.ld_dedata').text(data.PrepPartyDate);
                    $('.ld_zgdata').text(data.FormalPartyDat);
                    $('.ld_selib').text(data.PartyType);
                    $('.ld_status').text(data.PartyMembershipDues);
                    $('.ld_myzf').text(data.Branch);
                    $('.ld_rzbdata').text(data.JoinBranchDate);
                    $('.ld_zhizpeople').text(data.MyOrganization);
                    $('.ld_peopledata').text(data.JoinOrganizationDate);
                    $('.ld_pedata').text(data.MembershipStatus);
                    $('.ld_nameinput').val(data.Name)
                    $('.ld_sexinput').val(data.Gender)
                    $('.ld_hinput').val(data.Ethnic)
                    $('.ld_datasinput').val(data.Birth)
                    $('.ld_desinput').val(data.Origin)
                    $('.ld_chengsinput').val(data.OriginAddress)
                    $('.ld_czdsinput').val(data.Address)
                    $('.ld_phoneinput').val(data.Telphone)
                    $('.ld_bjyinput').val(data.Employer)
                    $('.ld_dakinput').val(data.Department)

                }
            }
        }
    })
}
$('.lg_left').click(function() {
    $(this).siblings().find('label').hide()
    $(this).siblings().find('input').show()
    $(this).siblings().find('input').focus();
})
$('#fileupload').fileupload({
    dataType: 'json',
    url: DOMAIN + "/api/Upload/UploadFile", //文件的后台接受地址
    //设置进度条
    progressall: function(e, data) {},
    //上传完成之后的操作，显示在img里面
    done: function(e, data) {

        $("#ld_img").attr("src", data.result.Result.FileUploadResults[0].FileUrl);
    }
});
$('.confirmBtn').on('click', function() {
    var RequestUserInfo = {};
    RequestUserInfo.UserId = $('#ld_nameinput').attr("aids")
    RequestUserInfo.PhotoUrl = $('#ld_img').attr('src');
    RequestUserInfo.Name = $('.ld_nameinput').val();
    RequestUserInfo.Gender = $('.ld_sex').text();
    RequestUserInfo.Marital = $('.ld_marital').text();
    RequestUserInfo.Ethnic = $('.ld_hinput').val()
    RequestUserInfo.Birth = $('.ld_datasinput').val()
    RequestUserInfo.Origin = $('.ld_desinput').val()
    RequestUserInfo.OriginAddress = $('.ld_chengsinput').val()
    RequestUserInfo.Address = $('.ld_czdsinput').val()
        //RequestUserInfo.Telphone = $('.ld_phoneinput').val()
    RequestUserInfo.Employer = $('.ld_bjyinput').val()
    RequestUserInfo.Department = $('.ld_dakinput').val()
    $.ajax({
        type: "post",
        data: {
            "RequestUserInfo": RequestUserInfo,
            "Token": token,
            "AppId": appId
        },
        url: DOMAIN + "/api/User/EditUser",
        dataType: "json",
        success: function(res) {

            if (res.Code == 00) {
                window.location.href = 'mine.html?appId=' + appId
            } else {
                bootoast({
                    message: '' + res.Message,
                    type: 'warning',
                    position: 'toast-top-center',
                    timeout: 3
                });
            }
        }
    })
})
$('.p_sex .add_fj').on('click', function(e) {
    $('.showsex .p_greinfo').removeClass('glyphicon-menu-down').addClass('glyphicon-menu-right');;
    $('.ld_sex').text($(this).text())
    $('.p_sex').hide();
});
$('.showsex').on('click', function(e) {
    if ($(".p_sex").css("display") == "none") {
        $('.showsex .p_greinfo').removeClass('glyphicon-menu-right').addClass('glyphicon-menu-down');
        $(".p_sex").show();
    } else {
        $('.showsex .p_greinfo').removeClass('glyphicon-menu-down').addClass('glyphicon-menu-right');
        $(".p_sex").hide();
    }
});
$('.p_marital .add_fj').on('click', function(e) {
    $('.show-marital .p_greinfo').removeClass('glyphicon-menu-down').addClass('glyphicon-menu-right');;
    $('.ld_marital').text($(this).text())
    $('.p_marital').hide();
});
$('.show-marital').on('click', function(e) {
    if ($(".p_marital").css("display") == "none") {
        $('.show-marital .p_greinfo').removeClass('glyphicon-menu-right').addClass('glyphicon-menu-down');
        $(".p_marital").show();
    } else {
        $('.show-marital .p_greinfo').removeClass('glyphicon-menu-down').addClass('glyphicon-menu-right');
        $(".p_marital").hide();
    }
})