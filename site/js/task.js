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
    //列表内容
    taskList();
    function taskList() {
        var str = `<div class="task-item">
        <img src="https://ss0.baidu.com/73F1bjeh1BF3odCf/it/u=3094324923,617905685&fm=85&s=D23E3CC4D6C9912E31101C7903005050" alt="">
        <div class="task-content">
            <div class="task-title">三会一课
            </div>
            <div class="task-status line-color accept-status">已接受</div>
        </div>
    </div>
    <div class="task-item">
        <img src="https://ss0.baidu.com/73F1bjeh1BF3odCf/it/u=3094324923,617905685&fm=85&s=D23E3CC4D6C9912E31101C7903005050" alt="">
        <div class="task-content">
            <div class="task-title">高效落实三会一课
            </div>
            <div class="task-status init-status">已接受</div>
        </div>
    </div>`;
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

});