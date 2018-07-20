var appId = getRequest('appId');
var token = localStorage.getItem("token")
//指派人员
var toUserId = '';
var toUserName = '';
//指派人员信息
var toUsreInfo = '';
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
	var pubFlag = $(".pub-flag").hasClass("select");
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
	if($('#selectpicker').val()==3){
		if(!toUserId) {
			bootoast({
				message: '请选择指派人员',
				type: 'warning',
				position: 'toast-top-center',
				timeout: 2
			});
			return;
		} 
	}
	$.ajax({
		type: "post",
		data: {
			"NewsTitle": title,
			"NewsType": type,
			"UserId":toUserId,
			"NewsContent": content,
			"IsPublic": pubFlag == true ? 1 : 0,
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
			}else{
				bootoast({
					message: "文章发布失败:" + res.Message + "",
					type: "warning",
					position: "toast-top-center",
					timeout: 2
				});
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
				if(res.Result.BranchUsers != '' && res.Result.BranchUsers != null&& res.Result.BranchUsers.length!= 0) {
					for(var i = 0; i < data.BranchUsers.length; i++) {
						options +='<div class="subordinate-item" id="' + data.BranchUsers[i].UserId + '">'+
							'<span>'+data.BranchUsers[i].UserName+'</span>'+
							'<div class="right-dir"><span class="glyphicon" aria-hidden="true"></span></div>'+
							'</div>'
					}
					$('.subordinates').append(options);
				}else{
					$('#modal .modal-body').html('暂无数据');
				}
			}
		}
	})
}
$('.modal-body').on('click', '.subordinate-item', function (e) {
		$('.subordinates .glyphicon').removeClass('glyphicon-ok');
		$($(this).find('.glyphicon')).addClass('glyphicon-ok');
		toUsreInfo = $(this).attr("id");
		toUserName = $($(this).find('span')).text();
		console.log(toUsreInfo,toUserName)
});
//是否公开
$(".flag-area").click(function() {
		if ($(".pub-flag").hasClass("select")) {
				$(".pub-flag")
						.attr("src", "icons/sel_no@2x.png")
						.removeClass("select");
		} else {
				$(".pub-flag")
						.attr("src", "icons/sel_yes@2x.png")
						.addClass("select");
		}
});
//指派人员
$('#btnTestSaveLarge').on('click', function() {
		if (toUsreInfo) {
			console.log(1)
				$("#name").html(toUserName);
				var arr = toUsreInfo.split("-");
				toUserId = arr[0];
				createType = arr[1];
				$(this).parents('.modal').modal('hide');
		} else {
				bootoast({
						message: "请选择指派人员",
						type: "info",
						position: "toast-top-center",
						timeout: 2
				});
		}
});