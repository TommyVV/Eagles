var appId = getRequest('appId');
var NewsId = getRequest('aryewsType');
var token = getCookie("token");
var userId = getCookie("userId");
$('#top-nav,#mobilenav').load('head.html')

$('#pc-footer').load('./footer.html')

myart(NewsId, appId) 
addNewsViewCount();
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
			//console.log(res);
			if(res.Code == 00) {
				var result = res.Result;
				$('.main_content .title').html(result.Title);
				$('.main_content .time').text(result.UserName);
				$('.main_content .content-box').html(result.HtmlContent);
				$('.main_content .cont-imgs img').attr("src",result.ImageUrl);
				$('.main_content .view-count').html(result.ViewCount);
				if(result.BookName){
					var authorStr = result.BookAuthor?("("+result.BookAuthor+")"):"";
					$(".book-info").html("书名："+result.BookName+authorStr);
				}
				var userType="1";
				if(result.UserId==userId){
					userType="0";
				}
				var comment = new Comment({
					commentType: "3",
					id: NewsId,
					userType: userType,
					appId: appId,
					token: token?token:"",
					userId: userId?userId:""
				});
				comment.getUserComment();
				
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