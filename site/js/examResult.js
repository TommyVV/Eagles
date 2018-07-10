
examResult(TestList);
function examResult(TestList) {
	$.ajax({
		type: "post",
		data: {
		  "TestId":23,
		  "UseTime": 0,
		  "TestList": TestList,
		  "Token": "abc",
		  "AppId": 10000000
		},
		url: "http://51service.xyz/Eagles/api/TestPaper/TestPaperAnswer",
		dataType: "json",
		success: function(res) {
			$('.result-detail-list').html('')
			var data = res.Result;
			if(res.Code == 00) {
				var examResult = ''; 
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