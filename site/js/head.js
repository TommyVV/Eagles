(function ($) {
    let queryStr = window.location.search.substring(1);
    if (!queryStr) return;
    let queryStrArr = queryStr.split('&');
    let query = {};
    let pages = { index: 0, partyLearning: 1, partyWork: 2, mine: 3 };
    queryStrArr.forEach((i) => {
        let queryArr = i.split('=');
        query[queryArr[0]] = queryArr[1];
    })
    $('#level-one > li').removeClass('active').eq(pages[query.page]).addClass('active');
    $('.navbar-right .glyphicon-search').on('click', function() {
	$('.navbar-header').hide();
	$('.search-modal').css('display', 'flex').show();
})
$('#search-cancle').on('click', function() {
	$('.navbar-header').show();
	$('.search-modal').hide();
})
$('.collapsed').click(function(){
	console.log(8)
	$('.operate').show();
})

$('.navbar-search .search-cancle').on('click', function() {
	$('.navbar-right .glyphicon-search').show();
	$('.navbar-search').hide();
})
/*$("[data-toggle='popover']").popover();*/
})(jQuery)

//导航切换
function pcnavList(id) {
    var $obj = $("#pcJ_navlist"), $item = $("#pcJ_nav_" + id);
    $item.addClass("on").parent().removeClass("none").parent().addClass("selected");
    $obj.find("h4").hover(function () {
        $(this).addClass("hover");
    }, function () {
        $(this).removeClass("hover");
    });
    $obj.find("p").hover(function () {
        if ($(this).hasClass("on")) { return; }
        $(this).addClass("hover");
    }, function () {
        if ($(this).hasClass("on")) { return; }
        $(this).removeClass("hover");
    });
    $obj.find("h4").click(function () {
        var $div = $(this).siblings(".pclist-item");
        if ($(this).parent().hasClass("selected")) {
            $div.slideUp(600);
            $(this).parent().removeClass("selected");
        }
        if ($div.is(":hidden")) {
            $("#pcJ_navlist li").find(".pclist-item").slideUp(600);
            $("#pcJ_navlist li").removeClass("selected");
            $(this).parent().addClass("selected");
            $div.slideDown(600);

        } else {
            $div.slideUp(600);
        }
    });
}
var appId=10000000
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
			$('#pcJ_navlist').html('');
			
			if(res.Code == 00) {
				var navdiv='';
				var ptwo='';
				
				for(var i=0;i<data.AppMenus.length;i++){
					var aimgurl='';
					
					if(!data.AppMenus[i].HasSubMenu){
						aimgurl='<a href="'+data.AppMenus[i].TargetUrl+'">'+data.AppMenus[i].MenuName+'</a>'
					}else{
						aimgurl=''+data.AppMenus[i].MenuName+'<i class="glyphicon glyphicon-triangle-bottom" style="margin-left:8px;"></i>'
					}
					navdiv+='<li><h4>'+aimgurl+'</h4><div class="pclist-item none"></div>'
					if(data.AppMenus[i].HasSubMenu){
						
						for(var j=0;j<data.AppMenus[i].SubMenus.length;j++){
							ptwo+='<p><a href="'+data.AppMenus[i].SubMenus[j].TargetUrl+'" target="_self">'+data.AppMenus[i].SubMenus[j].MenuName+'</a></p>'
						}
						
					}
				}
				$('#pcJ_navlist').append(navdiv);
				$('.pclist-item').append(ptwo);
				$('#pc_logoimg').attr("src",data.LogoUrl);
				
				pcnavList(123)
			}
		}
	});
}

//导航切换
function navListm(id) {
    var $obj = $("#J_navlist"), $item = $("#J_nav_" + id);
    $item.addClass("on").parent().removeClass("none").parent().addClass("selected");
    $obj.find("h4").hover(function () {
        $(this).addClass("hover");
    }, function () {
        $(this).removeClass("hover");
    });
    $obj.find("p").hover(function () {
        if ($(this).hasClass("on")) { return; }
        $(this).addClass("hover");
    }, function () {
        if ($(this).hasClass("on")) { return; }
        $(this).removeClass("hover");
    });
    $obj.find("h4").click(function () {
        var $div = $(this).siblings(".list-item");
        if ($(this).parent().hasClass("selected")) {
            $div.slideUp(600);
            $(this).parent().removeClass("selected");
        }
        if ($div.is(":hidden")) {
            $("#J_navlist li").find(".list-item").slideUp(600);
            $("#J_navlist li").removeClass("selected");
            $(this).parent().addClass("selected");
            $div.slideDown(600);

        } else {
            $div.slideUp(600);
        }
    });
}
navbarm(appId)
function navbarm(appId) {
	$.ajax({
		type: "post",
		data: {
			"AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/AppMenu/GetAppMenu",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			/*$('#J_navlist').html('');*/
			if(res.Code == 00) {
				var navdiv='';
				var ptwo='';
				for(var i=0;i<data.AppMenus.length;i++){
					var aimgurl='';
					
					if(!data.AppMenus[i].HasSubMenu){
						aimgurl='<a href="'+data.AppMenus[i].TargetUrl+'">'+data.AppMenus[i].MenuName+'</a>'
					}else{
						aimgurl=data.AppMenus[i].MenuName
					}
					navdiv+='<li><h4>'+aimgurl+'</h4><div class="list-item none"></div>'
					if(data.AppMenus[i].HasSubMenu){
						
						for(var j=0;j<data.AppMenus[i].SubMenus.length;j++){
							ptwo+='<p><a href="'+data.AppMenus[i].SubMenus[j].TargetUrl+'" target="_self">'+data.AppMenus[i].SubMenus[j].MenuName+'</a></p>'
						}
						
					}
				}
				$('#J_navlist').append(navdiv);
				$('.list-item').append(ptwo);
				$('#name_logo').attr("src",data.LogoUrl);
				
				navListm(12)
			}
		}
	});
}


