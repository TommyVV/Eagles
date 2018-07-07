$('.navbar-right .glyphicon-search').on('click',function(){
	$('.navbar-search').show();
	$(this).hide();
})

$('.navbar-search .search-cancle').on('click',function(){
	$('.navbar-right .glyphicon-search').show();
	$('.navbar-search').hide();
})