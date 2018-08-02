var appId = getRequest('appId');
var NewsId = getRequest('aryewsType');
$('#top-nav,#mobilenav').load('head.html')

$('#pc-footer').load('./footer.html')

myart(NewsId, appId) 

function myart(NewsId,appId) {
	$.ajax({
		type: "post",
		data: {
			"NewsId": NewsId,
			"Token": "",
			"AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/News/GetPublicUserNewsDetail",
		dataType: "json",
		success: function(res) {
			if(res.Code == 00) {
				$('.title').html(res.Result.Title);
				$('.time').text(res.Result.UserName);
				$('.content-box').html(res.Result.HtmlContent);
				$('.cont-imgs img').attr("src",res.Result.ImageUrl);
				
			}
		}
	})
}
