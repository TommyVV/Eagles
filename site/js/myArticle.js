var NewsTypes=getRequest('NewsType');
//var token=localStorage.getItem("token")
var token="abc";
var mescroll;
var check_value;
var zlValue;
var bqValue;
$('.item').html('')
mescroll = new MeScroll("mescroll", {
	down: {
		auto: false,
		isLock: true,
		callback: downcallback 
	},
	up: {
		page: {
            num: 0,
            size: 10,
            time: null
        },
        isLock: false,
		callback: partyLearningfunc,
		isBounce: false 
	}
 });
function downcallback()
{
}
function partyLearningfunc(page)
{
	
	if(page.check_value){
		if(check_value == zlValue) {
	    	myAricle(0,token,page);
	    }else if(check_value == bqValue) {
	    	myAricle(2,token,page);
	    }else {
	    	myAricle(1,token,page);
	    }
	}else{
		if(NewsTypes==undefined){
			$('.select_txt').text("文章");
			myAricle(0,token,page);
		}else if(NewsTypes==0){
			$('.select_txt').text("文章");
			myAricle(0,token,page);
		}else if(NewsTypes==1){
			$('.select_txt').text("心得体会");
			myAricle(1,token,page);
		}else if(NewsTypes==2){
			$('.select_txt').text("会议");
			myAricle(2,token,page);
		}
	}
	
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

function myAricle(NewsType,token,page) {
	$.ajax({
		type: "post",
		data: {
			"NewsType": NewsType,
			"Token": token,
			"AppId": 10000000,
			"PageSize": page.size,
  		  	"PageIndex": page.num
		},
		url: "http://51service.xyz/Eagles/api/User/GetUserArticle",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			if(res.Code == 00) {
				if(res.Result.NewsList!=''||res.Result.NewsList!=null){
					var myAricle = ''; 
					for(var i = 0; i < data.NewsList.length; i++) {
						myAricle+='<div class="article">'+
	                        '<div class="left"><img src="'+data.NewsList[i].ImageUrl +'" alt=""></div>'+
	                        '<div class="right">'+
	                            '<div class="content overflow-two-line">'+data.NewsList[i].Title +'</div>'+
	                            '<div class="date">'+data.NewsList[i].CreateTime  +'</div>'+
	                        '</div>'+
	                    '</div>';
	                    
					}
					mescroll.endSuccess(data.NewsList.length);
					$('.item').append(myAricle)//文章列表
				}else{
					mescroll.lockDownScroll( true );
                	mescroll.lockUpScroll( true );
				}
				
			}else{
				mescroll.lockDownScroll( true );
                mescroll.lockUpScroll( true );
				$('.mescroll-hardware').html('没有更多')
			}
		}
	});
}
$('.option').on('click', 'li', function(e) {
    check_value=$(this).text();
    zlValue = $('.option li:eq(1)').html();
    bqValue = $('.option li:eq(2)').html();
    $(this).parent().siblings(".select_txt").text(check_value);
    $("#select_value").val(check_value);
    $('.item').html('')
    if(mescroll){
		mescroll.destroy();
	}
     mescroll= new MeScroll("mescroll", {
		down: {
			auto: false,
			isLock: true,
			callback: downcallback 
		},
		up: {
			page: {
	            num: 0,
	            size: 10,
	            time: null,
	            check_value:check_value
	        },
	        isLock: false,
			callback: partyLearningfunc,
			isBounce: false 
		}
	});
    
});
