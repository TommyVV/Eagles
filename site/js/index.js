$('.navbar-right .glyphicon-search').on('click', function () {
	$('.navbar-header').hide();
	$('.search-modal').css('display','flex').show();
})
$('#search-cancle').on('click',function(){
	$('.navbar-header').show();
	$('.search-modal').hide();
})

$('.navbar-search .search-cancle').on('click', function () {
	$('.navbar-right .glyphicon-search').show();
	$('.navbar-search').hide();
})

//search
$('#search-input').on('keydown',(e) => {
	if(e.keyCode == 13){
		console.log('keydown');
	}
})
//跳转更多
$('.more').on('click',()=>{
	console.log('load more');
})

$("[data-toggle='popover']").popover();