if(!localStorage.getItem('token')){
	window.location.href = "login.html"
}
$('#top-nav,#mobilenav').load('./head.html')
var testId=getRequest('testId');
var token=localStorage.getItem("token")
var appId=getRequest('appId')
$('#exam-button-jump').on('click', () => {
    window.location.href = 'onlineExamQuestion.html?testId='+testId+'&appId='+appId+''
})
onlineexam(token,appId,testId)
function onlineexam(token,appId,testId) {
	$.ajax({
		type: "post",
		data: {
		  "Token": token,
		  "AppId": appId,
		  "TestId":testId,
		},
		url: "http://51service.xyz/Eagles/api/TestPaper/GetTestPaper",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			if(res.Code == 00) {
				$('.exam-title').html(data.TestPaperTitle);
				$('.exam-img img').attr("src",data.HtmlContent)
				$('.zon_scores').text(data.FullScore);//总数
				$('.jg_scores').text(data.PassScore );//及格
				$('.st_nums').text(data.TestList.length);//试题数量
				$('.time_num').text(data.LimitedTime );//分数
			}
		}
	});
}