var appId = getRequest('appId');
//积分换购
$("#point-part").click(function () {
    window.location.href = 'exchange.html?appId=' + appId + ''
});
//兑换记录
$("#record").click(function () {
    window.location.href = 'exchangeRecord.html?appId=' + appId + ''
});
//我的积分
$(".lw_muscore").click(function () {
    window.location.href = 'rank.html?appId=' + appId + ''
});
//我的信息
$(".fb_info").click(function () {
    window.location.href = 'myInfo.html?appId=' + appId + ''
});
//我的通知
$(".lw_news").click(function () {
    window.location.href = 'myNotice.html?appId=' + appId + ''
});
//我的学习
$(".fb_study").click(function () {
    window.location.href = 'studyList.html?appId=' + appId + ''
});
//我的会议
$(".fb_meeting").click(function () {
    window.location.href = 'meetingList.html?appId=' + appId + ''
});
//我的活动
$(".fb_profuct").click(function () {
    window.location.href = 'activityList.html?appId=' + appId + ''
});
//文章发布
$(".fb_wzby").click(function () {
    window.location.href = 'publishArticle.html?appId=' + appId + ''
});
//发布活动
$(".fb_fbgt").click(function () {
    window.location.href = 'publishTask.html?appId=' + appId + '&type=0'
});
//发布任务
$(".fb_wrw").click(function () {
    window.location.href = 'publishTask.html?appId=' + appId + '&type=1'
});
//我的任务
$(".fb_one").click(function () {
    window.location.href = 'task.html?appId=' + appId + ''
});
		//修改密码
		/* $(".md_password").click(function () {
				window.location.href = 'task.html?appId='+appId+''
		}); */





