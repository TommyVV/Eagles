var Moduleid=getRequest('paramModuleid')//获取模块id
var ModuleType=getRequest('paramModuleType')//获取来源页面id
var token=localStorage.getItem("token")
/*var token="123"
var Moduleid=2//传地址截取的值
var ModuleType=0//传地址截取的值*/
partyLearning(Moduleid,token)//新闻列表
partyTitle(ModuleType,Moduleid)//来源页面的分类列表
function partyLearning(moduleId,token) {
	$.ajax({
		type: "post",
		data: {
			"ModuleId": moduleId,
			"NewsCount": 10,
			"Token": token,
			"AppId": 10000000
		},
		url: "http://51service.xyz/Eagles/api/News/GetModuleNews",
		dataType: "json",
		success: function(res) {
			$('.list-bottom').html('')
			var data = res.Result;
			if(res.Code == 00) {
				var learningList = ''; 
				for(var i = 0; i < data.NewsInfos.length; i++) {
					var externalUrl = data.NewsInfos[i].ExternalUrl
					if(data.NewsInfos[i].IsExternal == true) {
						externalUrl = data.NewsInfos[i].ExternalUrl
					} else {
						externalUrl = 'partyLearning_detail.html' //详情页
					}
					learningList+='<div class="media">'+
                  '<div class="media-left">'+
                    '<a href="'+externalUrl+'?newsId='+data.NewsInfos[i].NewsId+'"><img class="media-object" src="' + data.NewsInfos[i].ImageUrl + '" alt="' + data.NewsInfos[i].Title + '"></a>'+
                  '</div>'+
                  '<div class="media-body">'+
                    '<h4 class="media-heading">' + data.NewsInfos[i].Title + '</h4>'+
                    '<span class="list-time">' + data.NewsInfos[i].CreateTime + '</span>'+
                  '</div>'+
                '</div>'
				}
				$('.list-bottom').append(learningList)//新闻列表
			}
		}
	});
}
//分类列表的接口
function partyTitle(ModuleType,moduleId) {
	$.ajax({
		type: "post",
		data: {
			"ModuleType": ModuleType,
			"AppId": 10000000
		},
		url: "http://51service.xyz/Eagles/api/AppModule/GetModule",
		dataType: "json",
		success: function(res) {
			$('.dropdown-menu').html('');
			$('.list-header').html('');
			var data = res.Result;
			if(res.Code == 00) {
				var learningTitle = ''; 
				var partyheader='';
				for(var i = 0; i < data.Modules.length; i++) {
					if(data.Modules[i].ModuleId==moduleId){
						partyheader='<span class="lb_imgb"><img src="'+data.Modules[i].SmallImageUrl+'" /></span><span class="title">'+data.Modules[i].ModuleName+' ></span>'
					}
					learningTitle+='<li class="lb_lihide" ids="'+data.Modules[i].ModuleId+'">'+data.Modules[i].ModuleName+'</li>';
				}
				$('.dropdown-menu').append(learningTitle);
				$('.list-header').append(partyheader);
			}
		}
	});
}
//点击分类重新获取新分类的内容
$('.dropdown-menu').on('click', 'li', function(e) {
	var module=$(this).attr('ids')
	partyLearning(module,token)//新闻列表
	partyTitle(ModuleType,module)//来源页面的分类列表
})

