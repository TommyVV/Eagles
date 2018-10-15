
var appId = getRequest('appId') //所有跳转到结果页的页面拼上appId
$('#top-nav,#mobilenav').load('./head.html')
$('#footer').load('footer.html')
var code = getRequest('code');
var mode = getRequest('mode');
var tip = getRequest("tip");
//var goodsdel = getRequest("goodsdel");

if (code == '1') {
	$(".result-des").html(tip);
	$("img").attr('src', 'icons/correct@2x.png');
} else {
	$(".result-des").html(tip);
	$("img").attr('src', 'icons/mistake@2x.png');
}
function countDown(secs, surl) {
	var jumpTo = document.getElementById('result_psf');
	jumpTo.innerHTML = secs;
	if (--secs > 0) {
		setTimeout("countDown(" + secs + ",'" + surl + "')", 1000);
	}
	else {
		window.location.href = surl;
	}
}
console.log(mode)
if (mode == "文章" && mode != undefined) {
	countDown(5, 'myArticle.html?appId=' + appId + '');
} else if (mode == "商品1" && mode != undefined) {
	countDown(5, 'exchangeRecord.html?appId=' + appId + '');
} else if (mode == "商品0" && mode != undefined) {
	$('.df_don').hide()
} else if (mode == 3) {
	countDown(5, 'index.html?moduleType=0&appId=' + appId + '');
}
