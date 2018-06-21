$(document).ready(function () {
    //积分换购
    $("#point-part").click(function () {
        window.location.href = 'exchange.html'
    });
    //兑换记录
    $("#record").click(function () {
        window.location.href = "exchangeRecord.html"
    });
    //我的任务
    $("#task").click(function () {
        window.location.href = "task.html"
    });




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
});




