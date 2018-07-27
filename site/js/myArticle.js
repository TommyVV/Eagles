var NewsTypes = getRequest('NewsType');
var appId = getRequest('appId');
var token = localStorage.getItem("token")
var onurl=window.location.href
if(!localStorage.getItem('token')||localStorage.getItem('IsInternalUser')==0) {
	window.location.href = 'login.html?appId=' + appId + '&onurl='+encodeURI(onurl);
}
$('#top-nav,#mobilenav').load('./head.html')
$('#footer').load('./footer.html')
//var token="abc";
var mescroll;
var check_value;
var zlValue;
var bqValue;
var dyValue;
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

function downcallback() {}

function partyLearningfunc(page) {

	if(page.check_value) {
		if(check_value == zlValue) {
			myAricle(0, token, page, appId);
		} else if(check_value == bqValue) {
			myAricle(2, token, page, appId);
		} else if(check_value == dyValue) {
			myAricle(3, token, page, appId);
		} else {
			myAricle(1, token, page, appId);
		}
	} else {
		if(NewsTypes == undefined) {
			$('.select_txt').text("文章");
			myAricle(0, token, page, appId);
		} else if(NewsTypes == 0) {
			$('.select_txt').text("文章");
			myAricle(0, token, page, appId);
		} else if(NewsTypes == 1) {
			$('.select_txt').text("心得体会");
			myAricle(1, token, page, appId);
		} else if(NewsTypes == 2) {
			$('.select_txt').text("会议");
			myAricle(2, token, page, appId);
		} else if(NewsTypes == 3) {
			$('.select_txt').text("党员申请书");
			myAricle(3, token, page, appId);
		}
	}

}
$(".select_box").click(function(event) {
	event.stopPropagation();
	$(this).find(".option").toggle();
	$(this).parent().siblings().find(".option").hide();
});
$(document).click(function(event) {
	var eo = $(event.target);
	if($(".select_box").is(":visible") && eo.attr("class") != "option" && !eo.parent(".option").length)
		$('.option').hide();
});

function myAricle(NewsType, token, page, appId) {
	$.ajax({
		type: "post",
		data: {
			"NewsType": NewsType,
			"Token": token,
			"AppId": appId,
			"PageSize": page.size,
			"PageIndex": page.num
		},
		url: "http://51service.xyz/Eagles/api/User/GetUserArticle",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			if(res.Code == 00) {
				if(res.Result.NewsList != '' || res.Result.NewsList != null) {
					var myAricle = '';
					for(var i = 0; i < data.NewsList.length; i++) {
						myAricle += '<div class="article" type="'+data.NewsList[i].NewsType +'" conretn="' + data.NewsList[i].Title + '" comids="'+data.NewsList[i].NewsContent+'" dangyuan="'+data.NewsList[i].ToUser+'" ispubic="'+data.NewsList[i].IsPublic+'" imgsrec="'+data.NewsList[i].ImageUrl+'">' +
							'<div class="left"><img src="' + data.NewsList[i].ImageUrl + '" alt=""></div>' +
							'<div class="right">' +
							'<div class="content overflow-two-line">' + data.NewsList[i].Title + '</div>' +
							'<div class="date">' + data.NewsList[i].CreateTime + '</div>' +
							'</div>' +
							'</div>';

					}
					mescroll.endSuccess(data.NewsList.length);
					$('.item').append(myAricle) //文章列表
				} else {
					mescroll.lockDownScroll(true);
					mescroll.lockUpScroll(true);
				}

			} else {
				mescroll.lockDownScroll(true);
				mescroll.lockUpScroll(true);
				$('.mescroll-hardware').html('没有更多')
			}
		}
	});
}
$('.option').on('click', 'li', function(e) {
	check_value = $(this).text();
	zlValue = $('.option li:eq(1)').html();
	bqValue = $('.option li:eq(2)').html();
	dyValue = $('.option li:eq(3)').html();
	$(this).parent().siblings(".select_txt").text(check_value);
	$("#select_value").val(check_value);
	$('.item').html('')
	if(mescroll) {
		mescroll.destroy();
	}
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
				time: null,
				check_value: check_value
			},
			isLock: false,
			callback: partyLearningfunc,
			isBounce: false
		}
	});

});
$(".select_box").click(function () {
        if ($(this).find(".glyphicon").hasClass("glyphicon-menu-down")) {
            $(this).find(".glyphicon").removeClass("glyphicon-menu-down").addClass("glyphicon-menu-up");
            //$(".peop-list").removeClass("hide");
        } else {
            $(this).find(".glyphicon").removeClass("glyphicon-menu-up").addClass("glyphicon-menu-down");
            //$(".peop-list").addClass("hide");
        }
    });
	
//点击文章分类
$('.item').on('click', '.article', function (e) {
	var aryewsType=$(this).attr('type');
	var conretn=$(this).attr('conretn');
	var dangyuan=$(this).attr('dangyuan');
	var comids=$(this).attr('comids');
	var ispubic=$(this).attr('ispubic');
	var imgsrec=$(this).attr('imgsrec');
	
	window.location.href = 'publishArticle.html?appId=' + appId + '&aryewsType=' +
		aryewsType + '&conretn=' + conretn + '&dangyuan=' + dangyuan + '&comids='+comids+'&ispubic='+ispubic+'&imgsrec='+imgsrec;

})