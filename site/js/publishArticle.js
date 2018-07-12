let isMobile = /Android|webOS|iPhone|iPod|BlackBerry/i.test(navigator.userAgent);
//文章发布
var token=localStorage.getItem("token")
//var appId=localStorage.getItem("token")
var token="abc"
var appId=10000000
$('.publish-btn').on('click', function (e) {
    e.preventDefault();
    var title = $('.publish-title').val();//标题
    var type = $('.publish-type').val();//文章类型
    var content = $('.publish-content').val();//文章内容
    if (!title) {
        alert('请填写文章标题');
        return;
    } else if (!type) {
        alert('请选择文章类型');
        return;
    } else if(!content){
    	alert('请填写文章内容');
        return;
    }
	$.ajax({
        type: "post",
		data: {
			  "NewsTitle": title,
			  "NewsType": type,
			  "NewsContent": content,
			  "Token": token,
			  "AppId": 10000000
		},
		url: "http://51service.xyz/Eagles/api/User/CreateArticle",
		dataType: "json",
        success:function(res){
        	var data=res.Result;
            if(res.Code == 00){
            	window.location.href='myArticle.html?NewsType='+$('.publish-type').val()+'';	
              //文章发布成功
            }
        }
	})
})
$(".publish-type").change(function(){
  if($(this).val()==3){
  	$('.assign').show();
  }else{
  	$('.assign').hide();
  }
});
branchUsers(token,appId);//党员支部信息展示
function branchUsers(token,appId){
	$.ajax({
        type: "post",
		data: {
			  "Token": token,
			  "AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/User/GetBranchUser",
		dataType: "json",
        success:function(res){
        	$('#lp_w').html('');
        	var data=res.Result;
            if(res.Code == 00){
            	var options='';
            	if(res.Result.BranchUsers!=''||res.Result.BranchUsers!=null){
            		for(var i = 0; i < data.BranchUsers.length; i++) {
            			options+='<option value="'+data.BranchUsers[i].UserId+'">'+data.BranchUsers[i].UserName+'</option>'
            		}
            		$('#lp_w').append(options)
            	}
            }
        }
	})
}

