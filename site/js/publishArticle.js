var appId = getRequest('appId');
var token = localStorage.getItem("token")
var name = getRequest("name");
if(!localStorage.getItem('token')) {
	window.location.href = 'login.html?appId=' + appId + '';
}
let isMobile = /Android|webOS|iPhone|iPod|BlackBerry/i.test(navigator.userAgent);
$('#top-nav,#mobilenav').load('./head.html')
//文章发布
$("#name").html(name);
$('.publish-btn').on('click', function(e) {
	e.preventDefault();
	var title = $('.publish-title').val(); //标题
	var type = $('.selectpicker').val(); //文章类型
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
				window.location.href = 'myArticle.html?NewsType=' + $('.selectpicker').val() + '';
				//文章发布成功
			}
		}
	})
})
$(".selectpicker").change(function() {
	if($(this).val() == 3) {
		$('.assign').show();
	} else {
		$('.assign').hide();
	}
});
 $("#subord").click(function() {
        window.location.href = "subordinateList.html?appId=" + appId + "&type=0";
    });
/*branchUsers(token, appId); //党员支部信息展示
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
			$('#lp_w').html('');
			var data = res.Result;
			if(res.Code == 00) {
				var options = '';
				if(res.Result.BranchUsers != '' || res.Result.BranchUsers != null) {
					for(var i = 0; i < data.BranchUsers.length; i++) {
						options += '<option value="' + data.BranchUsers[i].UserId + '">' + data.BranchUsers[i].UserName + '</option>'
					}
					$('#lp_w').append(options)
				}
			}
		}
	})
}*/