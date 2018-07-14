$(document).ready(function () {
    if (!localStorage.getItem('token')) {
        window.location.href = "login.html"
    }
    var code = getRequest('code');
    var tip = getRequest("tip");
    if (code == '1') {
        $(".result-des").html(tip);
        $(".result-bg").addClass("result-success");
        $(".glyphicon").addClass("glyphicon-ok icon");
    } else {
        $(".result-des").html(tip);
        $(".result-bg").addClass("result-fail");
        $(".glyphicon").addClass("glyphicon-remove icon");
    }
    class CalculateScreen {
        constructor() {
            this.isMobile = /Android|webOS|iPhone|iPod|BlackBerry/i.test(
                navigator.userAgent
            );
            this.init();
        }
        init() {
            if (!this.isMobile) {
                $(".mobile").hide();
                $(".pc").show();
                $("#top-nav").load("head.html", () => { });
                $("#footer").load("footer.html", () => { });
                $("body").css("background-color", "rgb(248,248,248)");
                $(".container").addClass('pc-wrap');
            } else {
                $(".mobile").show();
                $(".pc").hide();
                $("body").css("background-color", "#fff");
                $(".container").removeClass('pc-wrap');
            }
        }
    }
    new CalculateScreen();

    $(window).resize(function () {
        new CalculateScreen();
    });
});