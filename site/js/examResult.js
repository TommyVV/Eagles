
var token=localStorage.getItem("token")
var appId=getRequest('appId')
var onurl=window.location.href
if(!localStorage.getItem('token')||localStorage.getItem('IsInternalUser')==0) {
	window.location.href = 'login.html?appId=' + appId + '&onurl='+encodeURI(onurl);
}
$('#top-nav,#mobilenav').load('./head.html')
$('#footer').load('footer.html')
var TestList=getRequest('TestList')
var examresult=JSON.parse(TestList)
var TestScore=examresult.TestScore;
var Score=examresult.Score;
var UseTime=examresult.UseTime;
$('#lv_fs').text(TestScore);
$('#lv_jfs').text(Score);
$('#lv_dts').text(UseTime );
var TestLists=examresult.TestList;
if(TestScore<60){
	$('#suces').attr("src","icons/mistake@2x.png")
}else{
	$('#suces').attr("src","icons/correct@2x.png")
}	
var examResult='';
for(var i = 0; i < TestLists.length; i++) {
	var fend='<span>'+TestLists[i].QuestionId +'</span>'
	if(TestLists[i].IsRight==true){
		fend='<span class="wrong">'+TestLists[i].QuestionId +'</span>'
	}else{
		fend='<span class="right">'+TestLists[i].QuestionId +'</span>'
	}
	examResult+=fend
}
$('.result-detail-list').append(examResult)
$('.numsf').text($('.right').length)
$('.numsgh').text($('.wrong').length)				
				
