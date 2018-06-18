$(document).ready(function () {
    var status = getRequest('status');
    if(status=='0'){
        $(".status").html("未接受");
        $(".sub-btn").html("接受任务");
    }else if(status=="1"){
        $(".status").html("已接受");
        $(".sub-btn").html("制定计划");
    }else if(status=="2"){
        $(".status").html("已接受");
        $(".sub-btn").hide();
    }else if(status=="3"){
        $(".status").html("已完成");
        $(".sub-btn").hide();
    }else{
        $(".sub-btn").hide();
    }
    $(".opers").click(function(){
        window.location.href = "taskPlanEdit.html";
    });
});