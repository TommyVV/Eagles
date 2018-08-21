var appId = getRequest('appId');
var NewsId = getRequest('NewsId');
var token = localStorage.getItem("token")
var onurl=window.location.href
if(!localStorage.getItem('token')||localStorage.getItem('IsInternalUser')==0) {
	window.location.href = 'login.html?appId=' + appId + '&onurl='+encodeURI(onurl);
}
$('#top-nav,#mobilenav').load('head.html')

$('#pc-footer').load('./footer.html')

mynotice(NewsId,token, appId) //滚动信息详情

function mynotice(NewsId,token, appId) {
	$.ajax({
		type: "post",
		data: {
			"NewsId": NewsId,
			"Token": token,
			"AppId": appId
		},
		url: "http://ruisuikj.com/Eagles/api/Scroll/GetScrollNewsDetail",
		dataType: "json",
		success: function(res) {
			if(res.Code == 00) {
				$('.title').html(res.Result.NewsName);
				$('.time').text(res.Result.NoticeTime);
				$('.content-box').html(res.Result.NewsContent);
			}
		}
	})
}
