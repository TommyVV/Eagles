var appId = getRequest('appId') //获取来源页面['0', '1', '2', '3']
var token=localStorage.getItem('token')
$(function() {
   // console.info(window.location.href);
	console.info(appId);
	navbar(appId)
	if(localStorage.getItem('token')&&localStorage.getItem("IsInternalUser")==1){
		
		unreadMessage(token);
	}
})

$('.navbar-header').on('click', '.main-topf', function(e) {
	if($('.operate').is(':hidden')) {
		$('.operate').show();
	} else {
		$('.operate').hide();
	}
})

//导航切换
function pcnavList(id) {
	var $obj = $("#pcJ_navlist"),
		$item = $("#pcJ_nav_" + id);
	$item.addClass("on").parent().removeClass("none").parent().addClass("selected");
	$obj.find("h4").hover(function() {
		$(this).addClass("hover");
	}, function() {
		$(this).removeClass("hover");
	});
	$obj.find("p").hover(function() {
		if($(this).hasClass("on")) { return; }
		$(this).addClass("hover");
	}, function() {
		if($(this).hasClass("on")) { return; }
		$(this).removeClass("hover");
	});
	$obj.find("h4").click(function() {		
		var $div = $(this).siblings(".pclist-item");
		if($(this).parent().hasClass("selected")) {
			$div.slideUp(600);
			$(this).parent().removeClass("selected");
		}
		if($div.is(":hidden")) {
			$("#pcJ_navlist li").find(".pclist-item").slideUp(600);
			$("#pcJ_navlist li").removeClass("selected");
			$(this).parent().addClass("selected");
			$div.slideDown(600);

		} else {
			$div.slideUp(600);
		}
	});
}

//导航切换
function navListm(id) {
	var $obj = $("#J_navlist"),
		$item = $("#J_nav_" + id);
	$item.addClass("on").parent().removeClass("none").parent().addClass("selected");
	$obj.find("h4").hover(function() {
		$(this).addClass("hover");
	}, function() {
		$(this).removeClass("hover");
	});
	$obj.find("p").hover(function() {
		if($(this).hasClass("on")) { return; }
		$(this).addClass("hover");
	}, function() {
		if($(this).hasClass("on")) { return; }
		$(this).removeClass("hover");
	});
	$obj.find("h4").click(function() {		
		var $div = $(this).siblings(".list-item");
		if($(this).parent().hasClass("selected")) {
			$div.slideUp(1);
			$(this).parent().removeClass("selected");
		}
		if($div.is(":hidden")) {
			$("#J_navlist li").find(".list-item").slideUp(1);
			$("#J_navlist li").removeClass("selected");
			$(this).parent().addClass("selected");
			$div.slideDown(1);

		} else {
			$div.slideUp(1);
		}
	});
}

function navbar(appId) {
	$.ajax({
		type: "post",
		data: {
			"AppId": appId
		},
		url: DOMAIN + "/api/AppMenu/GetAppMenu",
		dataType: "json",
		success: function(res) {
			
			var data = res.Result;
			//			$('#pcJ_navlist').html('');

			if(res.Code == 00) {
				var navdiv = '';
				
				var mobilenav = '';
				var mobiled = '';
				for(var i = 0; i < data.AppMenus.length; i++) {
					var aimgurl = '';
					var ptwo = '';
					if(!data.AppMenus[i].HasSubMenu) {
						aimgurl = '<a href="' + data.AppMenus[i].TargetUrl + '">' + data.AppMenus[i].MenuName + '</a>';
						ptwo = '';
					} else { //有二级菜单
						aimgurl = '' + data.AppMenus[i].MenuName + '<i class="glyphicon glyphicon-triangle-bottom" style="margin-left:8px;"></i>'
						for(var j = 0; j < data.AppMenus[i].SubMenus.length; j++) {
							ptwo += '<p><a href="' + data.AppMenus[i].SubMenus[j].TargetUrl + '" target="_self">' + data.AppMenus[i].SubMenus[j].MenuName + '</a></p>';
						}
					}
					navdiv += '<li><h4>' + aimgurl + '</h4><div class="pclist-item none">' + ptwo + '</div>';
					mobilenav += '<li><h4>' + aimgurl + '</h4><div class="list-item none">' + ptwo + '</div>';

				}

				$('#pcJ_navlist').append(navdiv);
				$('#J_navlist').append(mobilenav);

				$('#pc_logoimg,#name_logo').attr("src", data.LogoUrl);
				pcnavList(123);
				navListm(12);
			}
		}
	});
}
$('.sk_sps').on('click', function(e) {
	if(localStorage.getItem("IsInternalUser")==0){
		return false;
	}else{
		window.location.href = 'myNotice.html?appId=' + appId + '';
	}
	
})
/* $('.main-topf').on('click', function(e) {
	window.location.href = 'index.html?moduleType=0&appId=' + appId + '';
}) */


function unreadMessage(token) {
	$.ajax({
		type: "get",
		url: DOMAIN + "/api/UserMessage/GetUserUnreadMessage?token=" + token + "",
		dataType: "json",
		success: function(res) {
			$('.g_hds').html('')
			var data = res.Result;
			if(res.Code == 00) {
				$('.news_list,.g_hds').text(data.UnreadMessageCount);
				if(data.UnreadMessageCount==0){
					$('.g_hds').hide()
				}else{
					$('.g_hds').show()
					if(data.UnreadMessageCount>99){
						$('.g_hds').html(99)
					}
				}
				
			}
		}
	});
}

