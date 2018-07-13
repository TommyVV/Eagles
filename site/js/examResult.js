if(!localStorage.getItem('token')){
	window.location.href = "login.html"
}
$('#top-nav,#mobilenav').load('./head.html')
if(!localStorage.getItem('token')){
	window.location.href = "login.html"
}
var TestId=getRequest('TestId')
var TestList=getRequest('TestList')
var UseTime=getRequest('useTime')
var token=localStorage.getItem("token")
var appId=getRequest('appId')
examResult(TestId,TestList,token,appId);
function examResult(TestId,UseTime,TestList,token,appId) {
	$.ajax({
		type: "post",
		data: {
		  "TestId":TestId,
		  "UseTime":UseTime,
		  "TestList": TestList,
		  "Token": token,
		  "AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/TestPaper/TestPaperAnswer",
		dataType: "json",
		success: function(res) {
			$('.result-detail-list').html('')
			var data = res.Result;
			if(res.Code == 00) {
				var examResult = ''; 
				if(data.TestScore<60){
					$('#suces').attr("src","../icons/mistake@2x.png")
				}else{
					$('#suces').attr("src","../icons/correct@2x.png")
				}
				$('#lv_fs').text(data.TestScore);
				$('#lv_jfs').text(data.Score);
				$('#lv_dts').text(data.UseTime );
				for(var i = 0; i < data.TestList.length; i++) {
					var fend='<span>'+data.TestList[i].QuestionId +'</span>'
					if(data.TestList[i].IsRight==true){
						fend='<span class="wrong">'+data.TestList[i].QuestionId +'</span>'
					}else{
						fend='<span class="right">'+data.TestList[i].QuestionId +'</span>'
					}
					examResult+=fend
				}
				$('.result-detail-list').append(examResult)
				$('.numsf').text($('.right').length)
				$('.numsgh').text($('.wrong').length)
			}
		}
	});
}