var appId = getRequest('appId');
var token = localStorage.getItem("token")
//指派人员
var toUserId = '';
var toUserName = '';
//指派人员信息
var toUsreInfo = '';
var imgUrl = "";
var fileArray = [];
var inputval='';
var NewsId='';
var dangyuan='';
//$("input:radio[name='radio1']").attr("checked",2);
var onurl=window.location.href
if(!localStorage.getItem('token')||localStorage.getItem('IsInternalUser')==0) {
	window.location.href = 'login.html?appId=' + appId + '&onurl='+encodeURI(onurl);
}
let isMobile = /Android|webOS|iPhone|iPod|BlackBerry/i.test(navigator.userAgent);
$('#top-nav,#mobilenav').load('./head.html')
$('#footer').load('footer.html');
    //附件上传fileupload
$("#fileupload").fileupload({
	url: UPLOAD,
	dataType: "json",
	//设置进度条
	progressall: function (e, data) {
		var progress = parseInt((data.loaded / data.total) * 100);
		$(".upload-progress").removeClass("hide");
		console.log("progress", progress);
		$(".upload-progress .bar").css("width", progress + "%");
		if (progress == 100) {
			setTimeout(() => {
				$(".upload-progress").addClass("hide");
			}, 1000);
		}
	},
	done: function (e, data) {
		console.log("上传附件--", data);
		if (data.result.Code == "00") {
			var array = data.result.Result.FileUploadResults;
			var object = {
				AttachName: array[0].FileName,
				AttachmentDownloadUrl: array[0].FileUrl
			};
			fileArray.push(object);
			dealAttachment();
		} else {
			console.log(data.result);
			bootoast({
				message: '' + data.result.Message,
				type: 'warning',
				position: 'toast-top-center',
				timeout: 3
			});
		}
	}
});
function dealAttachment(){
	$(".attaches").html(attachmentList(fileArray, 1));
	$(".glyphicon-remove").click(function () {
		var index = $('.glyphicon-remove').index(this);
		fileArray.splice(index, 1);
		$(this).parents('.file').remove();
		$(".upload-file").show();
	});
	if (fileArray.length == 4) {
		$(".upload-file").hide();
	}
}
//文章发布
if(getRequest('aryewsType')!=undefined&&getRequest('aryewsType')!=''){
	$("#attList").hide();
	NewsId=getRequest('NewsId');
	$.ajax({
		type: "post",
		data: {
			"NewsId":NewsId,
			"Token": token,
			"AppId": appId
		},
		url: DOMAIN + "/api/User/GetUserArticleDetail",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			if(res.Code == 00) {
				$('#title').val(data.Title);
				$('.selectpicker').val(data.NewsType);//.attr("selected","true"); 
				$(".selectpicker").selectpicker('refresh');
				//imgsrec
				if(data.NewsType==3){
					$('.introducer').show();
				}
				if(data.NewsType==1){
					$(".book-control").show()
				}
				if(data.BookName){
					$('#bookId').val(data.BookName);
				}
				if(data.BookAuthor){
					$('#book-author').val(data.BookAuthor);
				}
				if(data.Introducer){
					$('#name').html(data.Introducer);
					toUserId=data.ToUserId;
				}
				$('.publish-content').val(data.HtmlContent)
				$("input:radio[value='"+data.IsPublic+"']").attr('checked','true');
				imgUrl = data.ImageUrl
				$(".add").html(`<img src="${imgUrl}" class="upload-img">`);
				if(data.Status!=-1){
					$(".publish-title,#selectpicker,.item-select,#imgupload,.demo--radio,.publish-content").attr("disabled",true);
					$('.publish-wrap-btn').hide()
				}
				dangyuan=data.UserName
			}
		}
	})
	
	
	
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

function save(publisType){
	if(publisType==1){
		inputval=1;	
	}else{
		inputval=$('input:radio[name="radio1"]:checked').val();
	}
	
	var title = $('#title').val(); //标题
	var type = $('#selectpicker').val(); //文章类型
	var content = $('.publish-content').val(); //文章内容	
	var bookName=$("#bookId").val();
	var bookAuthor=$("#book-author").val();
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
			"NewsContent": content,
			"ImageUrl": imgUrl,
			"IsPublic": inputval,
			"Token": token,
			"AppId": appId,
			"BookName":bookName,
			"BookAuthor":bookAuthor,
			"ToUser":toUserId,
			"AttachList":fileArray
		},
		url: DOMAIN + "/api/User/CreateArticle",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			if(res.Code == 00) {
				bootoast({
					message: "保存成功",
					type: "success",
					position: "toast-top-center",
					timeout: 2
				});
				window.location.href = 'public_result.html?code=1&tip=保存成功待审核&appId=' + appId +'&mode=文章';
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
}
$('#save').on('click', function(e) {	
	save(1);
});
$('#publish').on('click', function(e) {	
	save(0);
});
$("#selectpicker").change(function() {
	if($(this).val() == 3) {
		$('.introducer').show();
	} 
	else {
		$('.introducer').hide();
	}

	if($(this).val()==1){
		$(".book-control").show()
	}
	else{
		$(".book-control").hide()
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
		url: DOMAIN + "/api/User/GetBranchUser",
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
							''+
							'</div>'
					}
					$('.subordinates').append(options);
					
				}else{
					$('#modal .modal-body').html('暂无数据');
					//todo 统一话术
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
		$("#name").html(toUserName);
		var arr = toUsreInfo.split("-");
		toUserId = arr[0];
		createType = arr[1];
		$(this).parents('.modal').modal('hide');
});

//指派人员
$('#btnTestSaveLarge').on('click', function() {
		if (toUsreInfo) {
			
		} else {
				bootoast({
						message: "请选择指派党员",
						type: "info",
						position: "toast-top-center",
						timeout: 2
				});
		}
});