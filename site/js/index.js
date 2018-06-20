$('.navbar-right .glyphicon-search').on('click', function () {
	$('.navbar-header').hide();
	$('.search-modal').css('display', 'flex').show();
})
$('#search-cancle').on('click', function () {
	$('.navbar-header').show();
	$('.search-modal').hide();
})

$('.navbar-search .search-cancle').on('click', function () {
	$('.navbar-right .glyphicon-search').show();
	$('.navbar-search').hide();
})

//search
$('#search-input').on('keydown', (e) => {
	if (e.keyCode == 13) {
		console.log('keydown');
	}
})
//跳转更多
$('.more').on('click', () => {
	console.log('load more');
})

$("[data-toggle='popover']").popover();




class CalculateScreen {
	constructor() {
		this.isMobile = /Android|webOS|iPhone|iPod|BlackBerry/i.test(navigator.userAgent);
		this.init();
	}
	init() {
		if (!this.isMobile) {
			$('.mobile').hide();
			$('.pc').show();
			$('#top-nav').load('head.html', () => {

			})
			$('#left-nav').load('leftNav.html', () => {

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

$(window).resize(function () {
	new CalculateScreen();
})