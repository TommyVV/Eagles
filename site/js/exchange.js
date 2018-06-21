$(document).ready(function () {
    $("#pc-header").load("./head.html")
    $('#pc-footer').load('./footer.html')
    var winWidth = document.body.clientWidth;
    if (winWidth <= 768) {
        var goodsItemWidth = $('.goods-item').width() + parseInt($('.goods-item').css('margin-left')) * 2;
        var marginLeft = (winWidth - parseInt(winWidth / goodsItemWidth) * goodsItemWidth) / 2;
        $('.goods-area').css('padding-left', marginLeft + 'px');
    }

    //去看看
    $(".item-btn").click(function () {
        window.location.href = 'goodsDetail.html';
    });

});