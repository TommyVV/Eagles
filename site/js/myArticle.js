var NewsTypes=getRequest('NewsType');
if(NewsTypes==undefined){
	$('.select_txt').text("文章");
	myAricle(0,token);
}else if(NewsTypes==0){
	$('.select_txt').text("文章");
	myAricle(0,token);
}else if(NewsTypes==1){
	$('.select_txt').text("心得体会");
	myAricle(1,token);
}else if(NewsTypes==2){
	$('.select_txt').text("会议");
	myAricle(2,token);
}
$(".select_box").click(function(event){   
    event.stopPropagation();
    $(this).find(".option").toggle();
    $(this).parent().siblings().find(".option").hide();
});
$(document).click(function(event){
    var eo=$(event.target);
    if($(".select_box").is(":visible") && eo.attr("class")!="option" && !eo.parent(".option").length)
    $('.option').hide();                                    
});
$(".option li").click(function(){
    var check_value=$(this).text();
    var zlValue = $('.option li:eq(1)').html();
    var bqValue = $('.option li:eq(2)').html();
    $(this).parent().siblings(".select_txt").text(check_value);
    $("#select_value").val(check_value);
    if(check_value == zlValue) {
    	myAricle(0,token);
    }else if(check_value == bqValue) {
    	myAricle(2,token);
    }else {
    	myAricle(1,token);
    }
});
var token="123";
//var NewsType='22';
var conmids=$('.lf_xin').text();
if(conmids=='文章'){
	myAricle(0,token);
}else if(conmids=='心得体会'){
	myAricle(1,token);
}else if(conmids=='会议'){
	myAricle(2,token);
}
myAricle(token);//文章列表
function myAricle(NewsType,token) {
	$.ajax({
		type: "post",
		data: {
			"NewsType": NewsType,
			"Token": token,
			"AppId": 10000000,
			"PageSize": 0,
			"PageIndex": 0
		},
		url: "http://51service.xyz/Eagles/api/User/GetUserArticle",
		dataType: "json",
		success: function(res) {
			$('.item').html('')
			var data = res.Result;
			if(res.Code == 00) {
				var myAricle = ''; 
				for(var i = 0; i < data.NewsList.length; i++) {
					myAricle+='<div class="article">'+
                        '<div class="left"><img src="'+data.NewsList.ImageUrl +'" alt=""></div>'+
                        '<div class="right">'+
                            '<div class="content overflow-two-line">'+data.NewsList[i].ShortDesc +'</div>'+
                            '<div class="date">'+data.NewsList[i].CreateTime  +'</div>'+
                        '</div>'+
                    '</div>';
				}
				$('.item').append(myAricle)//文章列表
			}
		}
	});
}