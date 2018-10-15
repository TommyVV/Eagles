var appId = getRequest('appId');
var NewsId = getRequest('aryewsType');
var token = getCookie("token");
var userId = getCookie("userId");
var detailObj = null;
$('#top-nav,#mobilenav').load('head.html')

$('#pc-footer').load('./footer.html')
myart(NewsId, appId) 
//点赞、收藏按钮
$(".like").click(function(){
	if($(this).hasClass("select")){
		//取消点赞
		editUserCollectLog(0,1);
	}else{
		editUserCollectLog(0,0);
	}
});
$(".fav").click(function(){
	if($(this).hasClass("select")){
		//取消收藏
		editUserCollectLog(1,1);
	}else{
		editUserCollectLog(1,0);
	}
});
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
				detailObj = result;
				$('.main_content .title').html(result.Title);
				$('.main_content .time').text(result.UserName);
				$('.main_content .content-box').html(result.HtmlContent);
				$('.main_content .cont-imgs img').attr("src",result.ImageUrl);
				$('.main_content .view-count').html(result.ViewCount);
				$(".add_content").css("display","block");
				getUserCollectLogExists(0);
				getUserCollectLogExists(1);
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
//用户点赞、收藏
function editUserCollectLog(cType,status){
	$.ajax({
		type: "POST",
		url: DOMAIN + "/api/User/EditUserCollectLog",
		data: {
			CollectType:cType,
			NewsId: NewsId,
			NewsType:detailObj.NewsType,
			Title:detailObj.Title,
			Author:detailObj.Author,
			ImageUrl:detailObj.ImageUrl,
			NewsUrl:window.location.href,
			Status:status,
			Token: token,
			AppId: appId
		},
		success: function (data) {
			if(data.Code==00){
				getUserCollectLogExists(cType);
			}
		},
		error: function () {
		}
	});

}
//查询用户是否点赞、收藏
function getUserCollectLogExists(type) {
	$.ajax({
		type: "POST",
		url: DOMAIN + "/api/User/GetUserCollectLogExists",
		data: {
			Type:type,
			NewsId: NewsId,
			Token: token,
			AppId: appId,
			PageSize: 0,
			PageIndex: 0
		},
		success: function (data) {
			if(data.Code==00){
				var flag = data.Result.UserCollectsLog;
				if(flag==true){
					if(type==0){
						$(".like").addClass("select");
						$(".like span").removeClass("glyphicon-heart-empty").addClass("glyphicon-heart");
					}else{
						$(".fav").addClass("select");
						$(".fav span").removeClass("glyphicon-star-empty").addClass("glyphicon-star");
					}
				}else{
					if(type==0){
						$(".like").remove("select");
						$(".like span").removeClass("glyphicon-heart").addClass("glyphicon-heart-empty");
					}else{
						$(".fav").remove("select");
						$(".fav span").removeClass("glyphicon-star").addClass("glyphicon-star-empty");
					}
				}
			}
		},
		error: function () {
		}
	});
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