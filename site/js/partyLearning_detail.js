$('#top-nav,#mobilenav').load('./head.html')
/* 
 1，涉及用户token的地方都需要验证
 2，提交答案时用户未登录，则跳转到登录页面，登录后 回跳回来自动提交答案，代码已写完 需要联调验证
 * 
 * */
var newsIds=getRequest('NewsId')//获取来源d的新闻id
var token = localStorage.getItem('token');
var appId = getRequest('appId');
//var appId = 10000000
addNewsViewCount(newsIds,token,appId)//更新新闻阅读量(页面一打开调用一下)
getNewsDetail(newsIds,token,appId); //加载页面详情
var TestId = localStorage.getItem('TestId'); //试卷ID
//进入页面默认调用一下，为了解决用户提交答案后未登录，登录后再跳转回来答案自动提交
submitTestPaperAnswer("1",TestId,0,token,appId);
//点击试卷提交 手动提交答案
$('#answer-submit').on('click', function() {
	submitTestPaperAnswer("0", TestId, 0,token,appId);
})
//提交试题的答案; 
//firstIn=1 为用户首次进入，自动提交答案 为了解决用户提交答案后未登录，登录后再跳转回来答案自动提交
function submitTestPaperAnswer(firstIn, TestId, UseTime,token,appId) {
	//存储答案数据
	var testlist = '';
	if(firstIn == '1') { //获取本地存储的答案信息 为了给用户提交时未登录，登录成功后再进来
		testlist = localStorage.getItem("testlist");
	} else {
		testlist = getTestPaperAnswerJson(); //获取页面答案的JSON数据
	}
	if(token && testlist && TestId) { //用户登录并且答案不为空则提交
		$.ajax({
			type: "post",
			data: {
				"TestId": TestId, //试卷ID
				"UseTime": UseTime, //试卷用时
				"TestList": testlist,
				"Token": token,
				"AppId": appId
			},
			url: "http://51service.xyz/Eagles/api/TestPaper/TestPaperAnswer",
			dataType: "json",
			success: function(res) {
				if(res.Code == 00) {	
					localStorage.removeItem("testlist");　 //提交成功后 删除本地存储的答案信息
					localStorage.removeItem("TestId");　 //删除本地存储的试卷ID
					bootoast({
					    message: '试卷提交成功',
						 type: 'success',
						 position:'toast-top-center',
						 timeout:2
					});
					//刷新当前页面,为了清空选中的答案
					window.location.reload();
				}

			}
		})
	} else if(!token && firstIn =='0') { //用户未登录
		localStorage.setItem("testlist", testlist); //先将答案写入到本地
		localStorage.setItem("TestId", TestId); //将试题的ID写入本地
		//跳转到登录页面
		window.location.href = "login.html";
	}
}

//获取新闻详情
function getNewsDetail(newsId,token,appId) {
	$.ajax({
		type: "post",
		data: {
			"NewsId": newsId,
			"Token": token,
			"AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/News/GetNewsDetail",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			var headerText = ''; //头部内容
			var contentBoxText = ''; //新闻正文
			var personBoxText = ''; //浏览次数
			var filesText = ''; //附件文件
			
			if(res.Code == 00) {
				TestId = data.TestId; //存储试卷ID
				//新闻内容
				contentBoxText = data.HtmlContent;
				//浏览次数
				personBoxText = '<span>' + data.ViewCount + '</span>'
				//添加附件
				for(var j = 0; j < data.Attach.length; j++) {
					filesText += ' <span class="glyphicon glyphicon-file" aria-hidden="true"></span>' + '<span><a href="' + data.Attach[j].AttachmentDownloadUrl + '" download="">' + data.Attach[j].AttachName + '</a></span>'
				}
				//头部内容
				$('.header .title').append(data.Title); //标题
				$('.header .time-box .time').append(data.CreateTime); //创建时间
				$('.header .source-box .source').append(data.Source); //来源
				$('.header .author-box .author').append(data.Author); //作者	
				$('.content-box .content').append(contentBoxText); //内容
				$('.person-box').append(personBoxText); //浏览人数
				$('.file').append(filesText); //附件
				if(data.CanStudy) { //获取学习时间
					getStudyTime(newsId, data.Module,token,appId); //学习时间
					//文章如果允许学习，并且用户已登录，每隔1分钟上报一次学习时间，学习时间增加1分钟 60000毫秒=1分钟
					if(token) {
						setInterval(function() {
							editStudyTime(data.NewsId, data.Module, 1,token,appId);
						}, 60000) 
					} else { //用户未登录 需要bootstrap的toast提示框，提示 "登录才可以累计学习时间"
						bootoast({
						    message: '登录才可以累计学习时间',
							 type: 'warning',
							 position:'toast-top-center',
							 timeout:2
						});
					}
				}
				//获取试卷信息
				if(data.TestId) {
					getNewsTest(data.TestId,token,appId)
				}
			}
		}
	})
}

//获取试卷信息
function getNewsTest(testId,token,appId) {
	$.ajax({
		type: "post",
		data: {
			"TestId": testId,
			"Token": token,
			"AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/TestPaper/GetTestPaper",
		dataType: "json",
		success: function(res) {
			if(res.Code == 00) {
				var data = res.Result.TestList;
				var question = ''; //问题标题
				var answer = ''; //问题的选项
				for(var i = 0; i < data.length; i++) {
					var answerLabel = ''; //选项文本
					var num = i + 1;
					//问题内容
					question += '<div class="question" QuestionId=' + data[i].QuestionId + ' Multiple1=' + data[i].Multiple + '><span>' + num + '</span><span>.</span><span>' + data[i].Question + '</span>';
					//问题的选项
					answerLabel += '<div class="answer">';
					//遍历获取选项
					for(var j = 0; j < data[i].AnswerList.length; j++) {
						if(data[i].AnswerList[j].AnswerType == 0) { //选项是默认的选择项
							if(data[i].Multiple == 1) { //多选
								answerLabel += '<label class="demo--label"><input class="demo--radio" type="checkbox" name="question-" ' + data[i].QuestionId + '" AnswerId=' + data[i].AnswerList[j].AnswerId + ' isRight=' + data[i].AnswerList[j].IsRight + '><span class="demo--radioInput"></span>' + data[i].AnswerList[j].Answer + ' </label>';
							} else { //单选
								answerLabel += '<label class="demo--label"><input class="demo--radio" type="radio"  AnswerId=' + data[i].AnswerList[j].AnswerId + ' isRight=' + data[i].AnswerList[j].IsRight + ' name="question-' + data[i].QuestionId + ' "><span class="demo--radioInput"></span>' + data[i].AnswerList[j].Answer + ' </label>';
							}
						} else if(data[i].AnswerList[j].AnswerType == 1) { //选项是文本框
							//写文本框的代码
							answerLabel += '<label class="demo--label">' + data[i].AnswerList[j].Answer + ' <input class="demo-text" style="width:100px;display:inline-block;" type="text"  AnswerId=' + data[i].AnswerList[j].AnswerId + ' isRight=' + data[i].AnswerList[j].IsRight + ' name="question-' + data[i].QuestionId + ' " /></label>';
						}
					}
					question += answerLabel + '</div></div>';
				}
				$('.footer .content').append(question); // 将问题和答案追加到页面中
			}
		}
	})
}

//获取用户的学习时间
function getStudyTime(NewsId, ModuleId,token,appId) {
	$.ajax({
		type: "post",
		data: {
			"NewsId": NewsId,
			"ModuleId": ModuleId,
			"Token": token,
			"AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/Study/GetStudyTime",
		dataType: "json",
		success: function(res) {
			$('.header .study-time').html('');
			var data = res.Result;
			if(res.Code == 00){
				if(data) {
					var studyTimeHtml = '<span class="glyphicon glyphicon-time" aria-hidden="true"></span><span>已学习:</span><span class="studyTime">' + data.StudyTime + '分钟</span>';
					$('.header .study-time').append(studyTimeHtml); //添加学习时间
				}
			}
		}
	})

}
//增加学习时间 外部需要写定时器，每分钟调取
function editStudyTime(NewsId, ModuleId, StudyTime,token,appId) {
	/*console.info('学习时间上报1分钟...');*/
	$.ajax({
		type: "post",
		data: {
			"StudyTime": StudyTime,
			"NewsId": NewsId,
			"ModuleId": ModuleId,
			"Token": token,
			"AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/Study/EditStudyTime",
		dataType: "json",
		success: function(res) {

		}
	})

}

//获取详情页答案Json数据
function getTestPaperAnswerJson() {
	var questions = $('.question');
	var testList = []; //存储试题和答案ID

	for(var i = 0; i < questions.length; i++) {
		var questionid = $(questions[i]).attr('questionid'); //问题ID
		var multiple = $(questions[i]).attr('Multiple1'); //问题单选或多选，0单选 1多选

		var question = {}; //存储问题对象
		question.QuestionId = questionid; //问题ID
		question.Answers = []; //答案集合

		//获取答案中的自定义文本输入框框
		var textAnswer = $(questions[i]).children('.answer').find('input[type=text]');
		if(textAnswer) {
			if(textAnswer.val()=='') { //自定义文本框没有作答,弹出提示
				bootoast({
				    message: '有题目的自定义文本框没有填写内容,请填写后再提交!',
					 type: 'warning',
					 position:'toast-top-center',
					 timeout:2
				});
				return;
			}
			var tempAnswers1 = {}; //存储临时的答案ID
			tempAnswers1.AnswerId = textAnswer.attr('answerid'); //文本框答案ID
			tempAnswers1.CustomizeAnswer = textAnswer.val(); //文本框内
			if(JSON.stringify(tempAnswers1) != "{}") //判断对象不为空时添加 避免添加空对象
				question.Answers.push(tempAnswers1);
		}
		if(multiple == 0) { //单选
			var tempAnswers2 = {}; //存储临时的答案ID
			var answer = $(questions[i]).children('.answer').find('input[type=radio]:checked');
			if(answer.length > 0) {
				tempAnswers2.AnswerId = answer.attr('answerid');
				if(JSON.stringify(tempAnswers2) != "{}") //判断对象不为空时添加 避免添加空对象
					question.Answers.push(tempAnswers2);
			} else { // 单选题没有选中答案
				bootoast({
				    message: '有题目题目没有作答,请全部完成后再提交!',
					 type: 'warning',
					 position:'toast-top-center',
					 timeout:2
				});
				return;
			}

		} else if(multiple == 1) { //多选
			var answerCheckboxs = $(questions[i]).children('.answer').find('input[type=checkbox]:checked'); //获取选中的多选元素

			if(answerCheckboxs.length) {
				for(var j = 0; j < answerCheckboxs.length; j++) { //多选
					var tempAnswers3 = {}; //存储临时的答案ID
					tempAnswers3.AnswerId = $(answerCheckboxs[j]).attr('answerid');

					if(JSON.stringify(tempAnswers3) != "{}")
						question.Answers.push(tempAnswers3);

				}
			}else{//多选题没有作答
				bootoast({
				    message: '有题目题目没有作答,请全部完成后再提交!',
					 type: 'warning',
					 position:'toast-top-center',
					 timeout:2
				});
				return;
			}
		}
		//答案对象
		testList.push(question); //问题对象
	}
	var testListJson = JSON.stringify(testList);
	console.info(testListJson);
	return testListJson;
}
//更新新闻阅读量
function addNewsViewCount(NewsId,token,appId) {
	$.ajax({
		type: "post",
		data: {
			"NewsId": NewsId,
			"Token": token,
			"AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/News/AddNewsViewCount",
		dataType: "json",
		success: function(res) {
			$('.header .study-time').html('');
			var data = res.Result;
			if(res.Code == 00){
				
			}
		}
	})

}
