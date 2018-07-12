$('.navbar-right .glyphicon-search').on('click', function() {
	$('.navbar-header').hide();
	$('.search-modal').css('display', 'flex').show();
})
$('#search-cancle').on('click', function() {
	$('.navbar-header').show();
	$('.search-modal').hide();
})

$('.navbar-search .search-cancle').on('click', function() {
	$('.navbar-right .glyphicon-search').show();
	$('.navbar-search').hide();
})
$("[data-toggle='popover']").popover();

class CalculateScreen {
	constructor() {
		this.isMobile = /Android|webOS|iPhone|iPod|BlackBerry/i.test(navigator.userAgent);
		this.init();
	}
	init() {
		if(!this.isMobile) {
			$('.mobile').hide();
			$('.pc').show();
			$('#top-nav').load('head.html', () => {

			})
			$('#left-nav').load('leftNav.html', () => {

			})
			$('#footer').load('footer.html', () => {

			})
			$('body').css('background-color', 'rgb(248,248,248)');
		} else {
			$('.mobile').show();
			$('.pc').hide();
			$('body').css('background-color', '#fff');

		}
	}
}
new CalculateScreen();

$(window).resize(function() {
	new CalculateScreen();
})
var token = localStorage.getItem('token');
//var pageType=getRequest('pageType')//获取来源页面id
//var moduleType=getRequest('moduleType')//获取来源页面['0', '1', '2', '3'],
//var appId=getRequest('appId')//获取来源页面id
var pageType=0;
var moduleType=0;
var appId=10000000
//接口模块
scrollimg(pageType,token,appId); //banner
moduleListTitle(moduleType,appId) //名称分类方法
function scrollimg(pageType,token,appId) {
	$.ajax({
		type: "post",
		data: {
			"PageType": pageType,
			"Token": token,
			"AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/Scroll/GetScrollImg",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			if(res.Code == 00) {
				var limobile = ''; //移动端标点
				var divs = ''; //移动端图片
				var pclicon = '';
				for(var i = 0; i < data.ImageList.length; i++) {
					limobile += '<li data-target="#carousel-example-generic" data-slide-to="' + i + '"></li>';
					divs += '<div class="item"><a href="' + data.ImageList[i].TargetUrl + '"><img src="' + data.ImageList[i].ImageUrl + '" alt=""></a><div class="carousel-caption"></div></div>';
					pclicon += '<li><a href="' + data.ImageList[i].TargetUrl + '"><img src="' + data.ImageList[i].ImageUrl + '" /> </a></li>'
				}
				$('#carousel-example-generic .carousel-indicators').append(limobile);
				$('#carousel-example-generic .carousel-inner').append(divs);
				$('#focus_m ul').append(pclicon);
				focusRun.init();
				$('#carousel-example-generic .carousel-indicators li:eq(0)').addClass('active');
				$('.item:eq(0)').addClass('active');
			}
		}
	});
}

/* 获取分类名称 */
function moduleListTitle(moduleType,appId) {
	$.ajax({
		type: "post",
		data: {
			"ModuleType": moduleType,
			"AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/AppModule/GetModule",
		dataType: "json",
		success: function(res) {
			var moudlesIds = []; //新闻类别属性对象
			var data = res.Result;
			if(res.Code == 00) {
				var title = ''; //移动端
				var pctitieone = ''; //PC端的左边标题
				var pctitietwo = ''; //PC端右边标题
				for(var i = 0; i < data.Modules.length; i++) {
					var urls = data.Modules[i].TargetUrl;
					if(urls == ''||urls == null) {
						urls="partyLearning_list.html?paramModuleid="+data.Modules[i].ModuleId+"&paramModuleType=0";//列表页
					} else {
						urls = data.Modules[i].TargetUrl;
					}
					var moudle = {}; //存储新闻类别的属性对象
					moudle.id = data.Modules[i].ModuleId; //存储新闻类别ID
					moudle.class1 = 'id-' + data.Modules[i].ModuleId; //存储一个新闻类别classid
					moudle.flag = '' //存储新闻类别是左边还是右边的标识符
					title += '<div class="list-box"><div class="list-header"><span class="la_s1"><img src="' + data.Modules[i].SmallImageUrl + '"></span><span class="title">' + data.Modules[i].ModuleName + '</span><a href="' + urls + '" class="more">更多 ></a></div><div class="list-body id-' + data.Modules[i].ModuleId + '"><div class="list-top row"></div><div class="list-bottom"></div></div></div>';
					if(i % 2 != 0) { //右边的标题
						pctitietwo += '<div class="right-item"><h4 class="item-title id-' + data.Modules[i].ModuleId + '"><span class="title"><img src="' + data.Modules[i].SmallImageUrl + '" alt="">' + data.Modules[i].ModuleName + '</span><a href="' + urls + '" class="more">更多&gt;&gt;</a></h4><div class="la_divj"></div></div>';
						moudle.flag = 'right';
					} else { //左边的标题
						pctitieone += '<div class="left-item"><h4 class="item-title id-' + data.Modules[i].ModuleId + '"><span class="title"><img src="' + data.Modules[i].SmallImageUrl + '" alt="">' + data.Modules[i].ModuleName + '</span><a href="' + urls + '" class="more">更多&gt;&gt;</a></h4><div class="left-item-content"></div></div>';
						moudle.flag = 'left';
					}
                    //存储新闻类别ID等属性
					moudlesIds.push(moudle);

				}
				$('.main_content').append(title);
				$('#la_leftg').append(pctitieone);
				$('#la_rig').append(pctitietwo);
               //获取分类下新闻列表   
				for(var i = 0; i < moudlesIds.length; i++) {
					//获取分类下新闻列表
					moduleListcontent(moudlesIds[i].id, moudlesIds[i].class1, moudlesIds[i].flag,token,appId)
				}
			}
		}
	});
}
//获取新闻列表
function moduleListcontent(moduleId, class1, flag,token,appId) {
	$.ajax({
		type: "post",
		data: {
			"ModuleId": moduleId,
			"NewsCount": 10,
			"Token": token,
			"AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/News/GetModuleNews",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			if(res.Code == 00) {
				var contentone = ''; //移动端内容第一部分内容
				var contenttwo = ''; //移动端内容第二部分内容
				var pccontenone = ''; //PC端左边 第一部分内容
				var pccontentwo = ''; //PC端左边 第二部分内容
				var pclistriht = ''; //PC端右边 内容
				pccontentwo += '<ul>';
				for(var i = 0; i < data.NewsInfos.length; i++) {
					var externalUrl = data.NewsInfos[i].ExternalUrl
					if(data.NewsInfos[i].IsExternal == true) {
						externalUrl = data.NewsInfos[i].ExternalUrl
					} else {
						externalUrl = 'partyLearning_detail.html' //详情页
					}
					//移动端代码
					if(i < 2) { 
						contentone += '<div class="media col-xs-6">' +
							'<div class="media-top"><a href="' + externalUrl + '"><img class="media-object" src="' + data.NewsInfos[i].ImageUrl + '" alt="' + data.NewsInfos[i].Title + '"></a>' +
							'</div><div class="media-body"><h4 class="media-heading overflow-two-line">' + data.NewsInfos[i].Title + '</h4><span class="list-time">' + data.NewsInfos[i].CreateTime + '</span>' +
							'</div>' +
							'</div>';
					} else if(i >= 2) {
						contenttwo += '<div class="media">' +
							'<div class="media-left"><a href="' + externalUrl + '"><img class="media-object" src="' + data.NewsInfos[i].ImageUrl + '" alt="' + data.NewsInfos[i].Title + '"></a>' +
							'</div><div class="media-body"><h4 class="media-heading overflow-two-line">' + data.NewsInfos[i].Title + '</h4><span class="list-time">' + data.NewsInfos[i].CreateTime + '</span>' +
							'</div>' +
							'</div>';
					}
					//PC代码 右边列表页
					if(flag == 'right') { 
						pclistriht += '<div class="top">' + '<img src="' + data.NewsInfos[i].ImageUrl + '" alt="' + data.NewsInfos[i].Title + '" />' +
							'<div class="top-overview"><div class="top-overview-title">' + data.NewsInfos[i].Title + '</div>' +
							'<span class="top-overview-date">' + data.NewsInfos[i].CreateTime + '</span>' +
							'</div></div>' +
							'<p class="article">' + data.NewsInfos[i].Title + '</p>';
					} else if(flag == 'left') {
						if(i < 1) { //PC代码 左边列表页 大图
							pccontenone += '<div class="top">' +
								'<img src="' + data.NewsInfos[i].ImageUrl + '" alt="' + data.NewsInfos[i].Title + '" />' +
								'<div class="top-overview">' +
								'<div class="top-overview-title overflow-two-line">' + data.NewsInfos[i].Title + '</div>' +
								'<div class="top-overview-article">' + data.NewsInfos[i].Title + '</div>' +
								'<span class="top-overview-date">' + data.NewsInfos[i].CreateTime + '</span>' +
								'</div>' +
								'</div>';
						}
						if(i >= 1) { //PC代码 左边列表页 小图列表
							pccontentwo += '<li><img src="' + data.NewsInfos[i].ImageUrl + '" alt="" />' +
								'<div class="overview">' +
								'<div class="overview-title overflow-two-line">' + data.NewsInfos[i].Title + '</div>' +
								'<div></div><span class="overview-date">' + data.NewsInfos[i].CreateTime + '</span>' +
								'</div></li>';
						}
					}
				}
				pccontentwo += '</ul>';
                //移动端内容添加
				$('.' + class1).children('div .list-top').append(contentone);
				$('.' + class1).children('div .list-bottom').append(contenttwo);
				//PC端内容添加
				$('.' + class1).siblings('div .left-item-content').append(pccontenone);
				$('.' + class1).siblings('div .left-item-content').append(pccontentwo);
				$('.' + class1).siblings('div .la_divj').append(pclistriht); //右边标题下内容添加
			}
		}
	});
}
$('.collapsed').click(function(){
	$('.operate').show();
})
//导航切换
function navList(id) {
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
			$('#J_navlist').html('');
			
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
				
				navList(12)
			}
		}
	});
}
