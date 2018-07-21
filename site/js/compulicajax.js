$(document).ready(function () {
var appId = getRequest('appId');
console.log(appId)
	$.ajaxSetup({
	   contentType:"application/x-www-form-urlencoded;charset=utf-8",
	   complete:function(XMLHttpRequest,textStatus){
		  //通过XMLHttpRequest取得响应结果
		  var rese = XMLHttpRequest.responseText;
		  try{
			var jsonData = JSON.parse(rese);
			if(jsonData.Code == 11){
				//console.log(1)
			  //如果超时就处理 ，指定要跳转的页面(比如登陆页)
			  alert(jsonData.Message);
			  localStorage.clear();
			  window.location.href = 'login.html?appId=' + appId;
			  
			}else{
			  //正常情况就不统一处理了
			}
		  }catch(e){
		  }
		}
	});
	
});
