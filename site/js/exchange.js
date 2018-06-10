$(document).ready(function(){
    var winWidth = document.body.clientWidth;
    var goodsItemWidth = $('.goods-item').width()+parseInt($('.goods-item').css('margin-left'))*2;
    var marginLeft = (winWidth - parseInt(winWidth/goodsItemWidth)*goodsItemWidth)/2;
    $('.goods-area').css('padding-left',marginLeft+'px');
    //去看看
    $(".item-btn").click(function(){
        window.location.href = 'goodsDetail.html';
    });

});