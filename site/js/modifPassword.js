var appId = getRequest('appId');
var phonesmodiy = getRequest('phonesmodiy');
var token = localStorage.getItem("token")
$('#top-nav,#mobilenav').load('./head.html')
$("#footer").load("footer.html");
navbar(appId)

function navbar(appId) {
    $.ajax({
        type: "post",
        data: {
            "AppId": appId
        },
        url: DOMAIN + "/api/AppMenu/GetAppMenu",
        dataType: "json",
        success: function(res) {
            var data = res.Result;
            if (res.Code == 00) {
                $('#login_logo').attr("src", data.LogoUrl)
            }
        }
    });
}

    //保存
$('.btn-login').on('click', function(e) {
    e.preventDefault();
    let password = $('#inputPassword').val(); //新密码
    let account = $('#inputUser').val(); //旧密码
    if (!account) {
        bootoast({
            message: '请输入原密码',
            type: 'warning',
            position: 'toast-top-center',
            timeout: 1
        });
        return;
    } else if (!password) {
        bootoast({
            message: '请输入新密码',
            type: 'warning',
            position: 'toast-top-center',
            timeout: 1
        });
        return;
    }
    $.ajax({
        type: "post",
        data: {
		  "Phone": phonesmodiy,
		  "UserPwd":account,
		  "NewPwd": password,
		  "Token": token,
		  "AppId": appId
        },
        url: DOMAIN + "/api/User/EditUserPwd",
        dataType: "json",
        success: function(res) {
            var data = res.Result;
            if (res.Code == 00) {
                window.location.href = 'mine.html?appId=' + appId
            } else {
                bootoast({
                    message: '' + res.Message + '',
                    type: 'warning',
                    position: 'toast-top-center',
                    timeout: 2
                });
            }
        }
    })
})