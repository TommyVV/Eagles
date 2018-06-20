$(document).ready(function () {
    $(".sub-btn").click(function () {
        window.location.href = "exchangeResult.html?code=1";
    });
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