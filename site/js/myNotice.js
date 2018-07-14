if(!localStorage.getItem('token')){
	window.location.href = "login.html"
}
$('#top-nav,#mobilenav').load('./head.html')
var mescroll;
var token=localStorage.getItem('token')
var appId=getRequest('appId');
/*var appId=10000000*/
$('.myNoticeBox').html('')
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
	
	noticeList(token,appId,page)
}

function noticeList(token,appId,page) {
	$.ajax({
		type: "post",
		data: {
		  "Token": token,
		  "AppId": appId,
		  "PageSize": page.size,
  		  "PageIndex": page.num
		},
		url: "http://51service.xyz/Eagles/api/User/GetUserNotice",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			if(res.Code == 00) {
				if(res.Result.NoticeList!=''||res.Result.NoticeList!=null){
					var noticeList = ''; //移动端标点
					var news='';
					
					for(var i = 0; i < data.NoticeList.length; i++) {
						if(data.NoticeList[i].IsRead==1){
						news=''
					}else if(data.NoticeList[i].IsRead=0){
						news='<span class="new">New</span>'
					}
						noticeList += '<div class="item"><div class="title">'+data.NoticeList[i].Title+'</div>'+
	                    '<div class="content overflow-two-line">'+data.NoticeList[i].Content +'</div>'+
	                    '<div class="time">'+data.NoticeList[i].CreateTime  +'</div>'+
	                    ''+news+''+
	                	'</div>';
					}
					mescroll.endSuccess(data.NoticeList.length)
					$('.myNoticeBox').append(noticeList);
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