var appId = getRequest('appId');
var NewsId = getRequest('aryewsType');
$('#top-nav,#mobilenav').load('head.html')

$('#pc-footer').load('./footer.html')

myart(NewsId, appId) 
addNewsViewCount();
var comment = new Comment({
	commentType: "3",
	id: NewsId,
	userType: "1",
	appId: appId,
	token: "",
	userId: ""
});
comment.getUserComment();
function myart(NewsId,appId) {
	$.ajax({
		type: "post",
		data: {
			"NewsId": NewsId,
			"Token": "",
			"AppId": appId
		},
		url: DOMAIN + "/api/News/GetPublicUserNewsDetail",
		dataType: "json",
		success: function(res) {
			console.log(res);
			if(res.Code == 00) {
				$('.main_content .title').html(res.Result.Title);
				$('.main_content .time').text(res.Result.UserName);
				$('.main_content .content-box').html(res.Result.HtmlContent);
				$('.main_content .cont-imgs img').attr("src",res.Result.ImageUrl);
				$('.main_content .view-count').html(res.Result.ViewCount);
				
			}
		}
	})
}
//更新阅读数量
function addNewsViewCount() {
	$.ajax({
		type: "POST",
		url: DOMAIN + "/api/News/AddNewsViewCount",
		data: {
			Type:"3",
			NewsId: NewsId,
			Token: "",
			AppId: appId
		},
		success: function (data) {
		},
		error: function () {
		}
	});
}