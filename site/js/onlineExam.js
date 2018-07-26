var testId = getRequest('testId');
var token = localStorage.getItem("token")
var appId = getRequest('appId')
var onurl=window.location.href
if(!localStorage.getItem('token')||localStorage.getItem('IsInternalUser')==0) {
	window.location.href = 'login.html?appId=' + appId + '&onurl='+encodeURI(onurl);
}
$('#top-nav,#mobilenav').load('./head.html')
$('#footer').load('footer.html')
$('#exam-button-jump').on('click', () => {
	window.location.href = 'onlineExamQuestion.html?testId=' + testId + '&appId=' + appId + ''
})
onlineexam(token, appId, testId)

function onlineexam(token, appId, testId) {
	$.ajax({
		type: "post",
		data: {
			"Token": token,
			"AppId": appId,
			"TestId": testId,
		},
		url: "http://51service.xyz/Eagles/api/TestPaper/GetTestPaper",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			if(res.Code == 00) {
				$('.exam-title').html(data.TestPaperTitle);
				$('.exam-img img').attr("src", data.HtmlContent)
				$('.zon_scores').text(data.FullScore); //总数
				$('.jg_scores').text(data.PassScore); //及格
				$('.st_nums').text(data.TestList.length); //试题数量
				$('.time_num').text(data.LimitedTime); //分数
			}
		}
	});
}