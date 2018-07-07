$('.navbar-right .glyphicon-search').on('click', function () {
	$('.navbar-search').show();
	$(this).hide();
})

$('.navbar-search .search-cancle').on('click', function () {
	$('.navbar-right .glyphicon-search').show();
	$('.navbar-search').hide();
})




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

$(window).resize(function () {
	new CalculateScreen();
})