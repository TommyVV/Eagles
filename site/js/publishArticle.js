var appId = getRequest('appId');
var token = localStorage.getItem("token")
//指派人员
var toUserId = '';
var toUserName = '';
//指派人员信息
var toUsreInfo = '';
var imgUrl = "";
var inputval='';
var NewsId=''
//$("input:radio[name='radio1']").attr("checked",2);
var onurl=window.location.href
if(!localStorage.getItem('token')||localStorage.getItem('IsInternalUser')==0) {
	window.location.href = 'login.html?appId=' + appId + '&onurl='+encodeURI(onurl);
}
let isMobile = /Android|webOS|iPhone|iPod|BlackBerry/i.test(navigator.userAgent);
$('#top-nav,#mobilenav').load('./head.html')
$('#footer').load('footer.html')
//文章发布
if(getRequest('aryewsType')!=undefined&&getRequest('aryewsType')!=''){
	NewsId=getRequest('NewsId');
	$('.publish-title').val(getRequest('conretn'));
	$('#selectpicker').val(getRequest('aryewsType')).attr('selected','selected'); 
	//imgsrec
	if(getRequest('aryewsType')==3){
		$('.assign').show();
	}
	$('.publish-content').val(getRequest('comids'))
	$("input:radio[value='"+getRequest('ispubic')+"']").attr('checked','true');
	imgUrl = getRequest('imgsrec');
	$(".add").html(`<img src="${imgUrl}" class="upload-img">`);
}

//图片上传
$("#imgupload").fileupload({
	url: UPLOAD,
	dataType: "json",
	done: function(e, data) {
		if (data.result.Code == "00") {
			var array = data.result.Result.FileUploadResults;
			console.log(data.result.Result.FileUploadResults);
			imgUrl = array[0].FileUrl;
			$(".add").html(`<img src="${imgUrl}" class="upload-img">`);
		} else {
			bootoast({
				message: "上传失败",
				type: "warning",
				position: "toast-top-center",
				timeout: 2
			});
		}
	}
});
$('.publish-btn').on('click', function(e) {
	
	e.preventDefault();
	inputval=$('input:radio[name="radio1"]:checked').val();
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
	}else if (!imgUrl) {
		bootoast({
			message: "请上传图片",
			type: "info",
			position: "toast-top-center",
			timeout: 2
		});
		return;
	}else if(!inputval){
        bootoast({
        	message: "请选择公开类型",
        	type: "info",
        	position: "toast-top-center",
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
			"NewsId":NewsId,
			"NewsTitle": title,
			"NewsType": type,
			"UserId":toUserId,
			"NewsContent": content,
			"ImageUrl": imgUrl,
			"IsPublic": inputval,
			"Token": token,
			"AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/User/CreateArticle",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			if(res.Code == 00) {
				bootoast({
					message: "文章发布成功，待审核",
					type: "success",
					position: "toast-top-center",
					timeout: 2
				});
				window.location.href = 'public_result.html?code=1&tip=文章发布成功待审核&appId=' + appId +'&mode=文章';
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
					if(getRequest('dangyuan')!=undefined){
						var idf=getRequest('dangyuan')
						$("#name").html($('#' + idf).find('span').text());
						toUserId=idf
					}
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
/* $(".flag-area").click(function() {
		if ($(".pub-flag").hasClass("select")) {
				$(".pub-flag")
						.attr("src", "icons/sel_no@2x.png")
						.removeClass("select");
		} else {
				$(".pub-flag")
						.attr("src", "icons/sel_yes@2x.png")
						.addClass("select");
		}
}); */
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