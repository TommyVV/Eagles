var appId = getRequest('appId');
var NewsId = getRequest('NewsId');
var token = localStorage.getItem("token")
var onurl=window.location.href
if(!localStorage.getItem('token')||localStorage.getItem('IsInternalUser')==0) {
	window.location.href = 'login.html?appId=' + appId + '&onurl='+encodeURI(onurl);
}
$('#top-nav,#mobilenav').load('head.html')

$('#pc-footer').load('./footer.html')

mynotice(NewsId,token, appId) 

function mynotice(NewsId,token, appId) {
	$.ajax({
		type: "post",
		data: {
			"NewsId": NewsId,
			"Token": token,
			"AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/Scroll/GetScrollNewsDetail",
		dataType: "json",
		success: function(res) {
			if(res.Code == 00) {
				$('.title').html(res.Result.Title);
				$('.time').text(res.Result.UserName);
				$('.content-box').html(res.Result.HtmlContent);
			}
		}
	})
}
