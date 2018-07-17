var appId = getRequest('appId');
var token = localStorage.getItem("token")
var name = getRequest("name");
if(!localStorage.getItem('token')) {
	window.location.href = 'login.html?appId=' + appId + '';
}
let isMobile = /Android|webOS|iPhone|iPod|BlackBerry/i.test(navigator.userAgent);
$('#top-nav,#mobilenav').load('./head.html')
$('#footer').load('footer.html')
//文章发布
$("#name").html(name);
$('.publish-btn').on('click', function(e) {
	e.preventDefault();
	var title = $('.publish-title').val(); //标题
	var type = $('#selectpicker').val(); //文章类型
	var content = $('.publish-content').val(); //文章内容
	if(!title) {
		bootoast({
			message: '请填写文章标题',
			type: 'warning',
			position: 'toast-top-center',
			timeout: 2
		});
		return;
	} else if(!type) {
		bootoast({
			message: '请选择文章类型',
			type: 'warning',
			position: 'toast-top-center',
			timeout: 2
		});
		return;
	} else if(!content) {
		bootoast({
			message: '请填写文章内容',
			type: 'warning',
			position: 'toast-top-center',
			timeout: 2
		});
		return;
	}
	$.ajax({
		type: "post",
		data: {
			"NewsTitle": title,
			"NewsType": type,
			"NewsContent": content,
			"Token": token,
			"AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/User/CreateArticle",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			if(res.Code == 00) {
				window.location.href = 'myArticle.html?NewsType=' + $('#selectpicker').val() + '';
				//文章发布成功
			}
		}
	})
})
$("#selectpicker").change(function() {
	if($(this).val() == 3) {
		$('.assign').show();
	} else {
		$('.assign').hide();
	}
});

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
branchUsers(token, 10000000); //党员支部信息展示
function branchUsers(token, appId) {
	$.ajax({
		type: "post",
		data: {
			"Token": token,
			"AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/User/GetBranchUser",
		dataType: "json",
		success: function(res) {
			$('#mys_zpy').html('');
			var data = res.Result;
			if(res.Code == 00) {
				var options = '';
				if(res.Result.BranchUsers != '' && res.Result.BranchUsers != null) {
					for(var i = 0; i < data.BranchUsers.length; i++) {
						options += '<option value="' + data.BranchUsers[i].UserId + '">' + data.BranchUsers[i].UserName + '</option>'
					}
					$('#mys_zpy').append(options)
				}else{
					$('.relation-list').html('无指派人员')
				}
			}
		}
	})
}