$(document).ready(function () {
    //三会一课
    $("#t-cate").click(function () {
        cateStatus('t-cate');
    });
    //我的
    $('#m-cate').click(function () {
        cateStatus('m-cate');
    });
    //分类项目点击
    $(".list-item").click(function () {
        $(".drog-down-menu").toggleClass("hide");
        $($("#t-cate").find('span')).css('transform', 'rotate(360deg)');
        $($("#m-cate").find('span')).css('transform', 'rotate(360deg)');
    });
    //已接受
    $("#accept").click(function(){
        window.location.href = "taskStatus.html?status=1";
    });
    $("#accept1").click(function(){
        window.location.href = "taskStatus.html?status=2";
    });
    $("#complete").click(function(){
        window.location.href = "taskStatus.html?status=3";
    });
    $("#init").click(function(){
        window.location.href = "taskStatus.html?status=0";
    });
    //列表内容
    function taskList() {
        var str = ``;
        $(".task-list").html(str+str+str);
    }
    function cateStatus(id) {
        $(".drog-down-menu").toggleClass("hide");
        if ($(".drog-down-menu").hasClass('hide')) {
            $($("#" + id).find('span')).css('transform', 'rotate(360deg)');
        } else {
            $($("#" + id).find('span')).css('transform', 'rotate(180deg)');
        }
    }


    $("#select-result").on('click', (e) => {
        if (e.target.tagName === 'LI') {
            $('#select-result > span').html($(e.target).html().trim());
        }
        $('#person-name').toggle();
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
