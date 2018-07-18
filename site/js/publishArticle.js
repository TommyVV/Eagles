var appId = getRequest('appId');
var token = localStorage.getItem("token")
if(!localStorage.getItem('token')) {
	window.location.href = 'login.html?appId=' + appId + '';
}
let isMobile = /Android|webOS|iPhone|iPod|BlackBerry/i.test(navigator.userAgent);
$('#top-nav,#mobilenav').load('./head.html')
$('#footer').load('footer.html')
//文章发布

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

   
branchUsers(token, appId); //党员支部信息展示
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
			$('#modal-body').html('');
			var data = res.Result;
			if(res.Code == 00) {
				var options = '';
				if(res.Result.BranchUsers != '' && res.Result.BranchUsers != null) {
					for(var i = 0; i < data.BranchUsers.length; i++) {
						options += '<option value="' + data.BranchUsers[i].UserId + '">' + data.BranchUsers[i].UserName + '</option>'
					}
					
					$('#modal-body').append(options);
					$("#name").html($('#modal-body').find("option:selected").text());
					console.log($('#modal-body').html())
				}
			}
		}
	})
}
$("#modal-body").change(function() {
	$("#name").html($('#modal-body').find("option:selected").text());
});