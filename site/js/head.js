$('.navbar-header').on('click', '.collapsed', function(e) {
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
var appId = 10000000
navbar(appId)

function navbar(appId) {
	$.ajax({
		type: "post",
		data: {
			"AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/AppMenu/GetAppMenu",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			//			$('#pcJ_navlist').html('');

			if(res.Code == 00) {
				var navdiv = '';
				var ptwo = '';
				var mobilenav = '';
				var mobiled = '';
				for(var i = 0; i < data.AppMenus.length; i++) {
					var aimgurl = '';

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

