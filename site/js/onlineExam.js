var testId = getRequest('testId');
var token = localStorage.getItem("token")
var appId = getRequest('appId')
var activityId = getRequest("activityId");
var counttime = 0; //试卷剩余时间
var limitedTime = 0; //试卷规定做题时间
var onurl=window.location.href
/* if(!localStorage.getItem('token')||localStorage.getItem('IsInternalUser')==0) {
	window.location.href = 'login.html?appId=' + appId + '&onurl='+encodeURI(onurl);
} */
$('#top-nav,#mobilenav').load('./head.html')
$('#footer').load('footer.html')
/* $('#exam-button-jump').on('click', () => {
	window.location.href = 'onlineExamQuestion.html?testId=' + testId + '&appId=' + appId + ''
}) */
onlineexam();
$('#exam-button-jump').on('click', function() {
	getIsJoinTest();
})
//查询用户是否参加过该活动
function getIsJoinTest() {
	$.ajax({
		type: "POST",
		url: "http://51service.xyz/Eagles/api/Activity/IsJoinActivity",
		data: {
			"ActivityId": activityId,
			"Token": token,
			"AppId": appId
		},
		success: function(data) {
			if (data.Code == "00") {
				$('.examconmids').removeClass('ishides')
				$('#h_examtitie').hide();
				daojishi(); //开始倒计时
			} else {
				bootoast({
					message: '' + data.Message + '',
					type: 'warning',
					position: 'toast-top-center',
					timeout: 2
				});
			}
		}
	});
}


function onlineexam() {
	$.ajax({
		type: "post",
		data: {
			"Token": token,
			"AppId": appId,
			"TestId": testId,
		},
		url: "http://51service.xyz/Eagles/api/TestPaper/GetTestPaper",
		dataType: "json",
		success: function(res) {
			var data = res.Result;
			if(res.Code == 00) {
				$('.exam-title').html(data.TestPaperTitle);
				$('.exam-img img').attr("src", data.HtmlContent)
				$('.zon_scores').text(data.FullScore); //总数
				$('.jg_scores').text(data.PassScore); //及格
				$('.st_nums').text(data.TestList.length); //试题数量
				$('.time_num').text(data.LimitedTime); //分数
				
				counttime = data.LimitedTime * 60; //试卷规定时间
				limitedTime = data.LimitedTime;
				var num = 0;
				if(data.TestList.length==1){
					$("#next").hide();
					$("#submit").show()
				}
				for(var i = 0; i < data.TestList.length; i++) {
					num += 1;
					var questionHtml = ''; //存储问题的Html
					var answerHtmlAll = ''; //存储答案的HTML

					questionHtml += '<div class="ques-content-q" id=question' + num + ' num="' + num + '"> <div class="ques-content-question" QuestionId=' + data.TestList[i].QuestionId + ' Multiple1=' + data.TestList[i].Multiple + ' MultipleCount="' + data.TestList[i].MultipleCount + '">' + num + '. ' + data.TestList[i].Question + '</div>';
					for(var j = 0; j < data.TestList[i].AnswerList.length; j++) { //遍历获取答案
						var answerHtml = '<div class="ques-content-options">'; //存储答案的Html
						var answerHtmlTemp = '';
						var answerModel = data.TestList[i].AnswerList[j]; //答案对象 
						var inputType = '';
						if(data.TestList[i].Multiple == '0') { //单选
							inputType = 'radio';
						} else if(data.TestList[i].Multiple == '1') { //多选
							inputType = 'checkbox';
						}
						if(data.TestList[i].AnswerList[j].AnswerType == '1') { //自定义文本框
							inputType = 'text';
							//答案的HTML
							answerHtmlTemp += '<div class="ques-content-options-list"><label><span class="ques-content-options-list-option"><span></span></span><span class="ques-content-options-list-explain">' + answerModel.Answer + ' </span><input type="' + inputType + '" name="ques' + num + '" AnswerId=' + answerModel.AnswerId + '></label></div>';
							answerHtml += answerHtmlTemp + '</div>'; // 答案
						} else {
							//答案的HTML
							answerHtmlTemp += '<div class="ques-content-options-list"><label><span class="ques-content-options-list-option"><input hidden type="' + inputType + '" name="ques' + num + '" AnswerId=' + answerModel.AnswerId + '><span></span></span><span class="ques-content-options-list-explain">' + answerModel.Answer + ' </span></label></div>';
							answerHtml += answerHtmlTemp + '</div>'; // 答案
						}

						answerHtmlAll += answerHtml;
					}

					questionHtml += answerHtmlAll + '</div>'; //答案追加到问题后面
					$('.ques-content').append(questionHtml);

					//答题卡追加数据
					var quesModalHtml = '<span class="ques-modal-span" onclick="clickQuesModal(this)" quesId="question' + num + '">' + num + '</span>';
					$('#ques-modal-wrap').append(quesModalHtml);
				}
				$('.ques-totalCount').append(num); //总题目数
				showQues($('#question1'));
			}
		}
	});
}
//点击查看答题卡
$('#num-modal').on('click', () => {
	$('.ques-modal-span').css('background', ''); //背景颜色先恢复为空
	$('#ques-modal').show(); //模态框展示
	$('#ques-modal-wrap').removeClass('list-hide').addClass('list-show'); //答题卡展示

	//遍历所有问题，获取每道题是否被选中的状态，选中则让答题卡题目背景颜色变红
	$('.ques-content-q').each(function(i, element) {
		var ques = $(element).find('.ques-content-question');
		var checkedInputs = [];

		checkedInputs = $(element).find('input[type=radio]:checked');
		if(checkedInputs.length <= 0) { //当前题目选中了答案 背景变红
			checkedInputs = $(element).find('input[type=checkbox]:checked');
		}
		//选中了答案
		if(checkedInputs.length > 0) {
			//清空
			var mo = $('.ques-modal-span[quesid=' + $(element).attr("id") + ']');
			mo.css('background', 'red');
		}

	})
})
$('.ques-modal').on('click', function() {
	$('#ques-modal').hide();
	$('#ques-modal-wrap').removeClass('list-show').addClass('list-hide');
})
//显示指定题目
function showQues(question) { //
	question.show().siblings().hide();
	$('.ques-newCount').html(question.attr('num')); //当前是第几题
}

//下一题 
//下一题如果不存在 则隐藏下一页的按钮，显示提交按钮
$('#next').on('click', function() {
	var showQuest = getShowQuestElement(); //获取当前显示的题目对象
	var quesContent = showQuest.find('.ques-content-question');
	var nextElement = showQuest.next('.ques-content-q'); //下一题
	if(nextElement.length > 0 && judgeMultiplecount()) { //下一题存在，并且通过了多选选择的校验
		//需要加多选判断
		showQuest.hide().next('.ques-content-q').show();
		$('.ques-newCount').html(nextElement.attr('num')); //当前是第几题
		$('#prev').show();
	}
	var nextNextEle = nextElement.next('.ques-content-q');
	if(nextNextEle.length <= 0) { //下下一题不存在，则隐藏下一题的按钮
		$('#next').hide();
		$('#submit').show();
	}
})

//判断当前题目 如果是多选题，选择的答案数量是不是超过了规定数量
//element = 题目对象
function judgeMultiplecount() {
	var rest = true; //是否通过校验
	var showQuest = getShowQuestElement();
	var quesContent = showQuest.children('.ques-content-question');
	if(quesContent.attr("multiple1") == '1') { //多选
		var checkedInputs = showQuest.find('input[type=checkbox]:checked');
		if(checkedInputs.length > quesContent.attr("multiplecount")) {
			rest = false;
			bootoast({
				message: '当前题目，您最多只能选择' + quesContent.attr("multiplecount") + '个选项',
				type: 'warning',
				position: 'toast-top-center',
				timeout: 2
			});
		}

	} else { //单选不处理

	}
	return rest;
}

//上一题
$('#prev').on('click', function() {
	$('#submit').hide();
	var j = 0;
	$('.ques-content-q').each(function(i, element) {
		if($(element).css("display") == 'block') {
			var prevElement = $(element).prev('.ques-content-q');
			if(j == 0 && prevElement.length > 0) {
				$(element).hide().prev('.ques-content-q').show();
				$('.ques-newCount').html(prevElement.attr('num')); //当前是第几题
				$('#next').show();
				$('#prev').hide();
				j++;
			}
		}
	})
})

//点击答题卡题目，跳转到对应的题目
function clickQuesModal(modal) {
	var quesId = $(modal).attr('quesId');
	showQues($(eval(quesId))); //题目显示	
	$('#ques-modal').hide();
}

//过去当前展示的题目对象
function getShowQuestElement() {
	var j = 0;
	var ele = 0; //当前显示的对象
	$('.ques-content-q').each(function(i, element) {
		if($(element).css("display") == 'block') {
			if(j == 0) {
				ele = $(element);
				return ele;
				j++;
			}
		}
	})
	return ele;
}

//页面提交
$('#submit').on('click', function() {
	submit(testId,token,appId);
})

//提交试卷
function submit(testId,token,appId) {

	var useTime = Math.ceil(limitedTime - (counttime / 60));
	if(judgeMultiplecount()) { //校验当前题如果是多选 选择的答案是否超过规定数量，返回TURE为正常，FALSE为超过了
		//获取数据，提交数据
		var dataJson = getTestPaperAnswerJson();
		var UseTime = 0;

		$.ajax({
			type: "post",
			data: {
				"TestId": testId, //试卷ID
				"UseTime": useTime, //试卷用时,用试卷规定时间减去剩余时间
				"TestList": dataJson,
				"Token": token,
				"AppId": appId
			},
			url: "http://51service.xyz/Eagles/api/TestPaper/TestPaperAnswer",
			dataType: "json",
			success: function(res) {
				if(res.Code == 00) {
					var datajlist=JSON.stringify(res.Result)
					window.location.href = 'examResult.html?appId='+appId+'&TestList='+datajlist
				} else {
					bootoast({
						message: '' + res.Message + '',
						type: 'warning',
						position: 'toast-top-center',
						timeout: 2
					});
				}
			}
		})
	}
}

//获取页答案Json数据
function getTestPaperAnswerJson() {
	var questionParent = $('.ques-content-q');
	var testList = []; //存储试题和答案ID

	for(var i = 0; i < questionParent.length; i++) {
		var questions = $(questionParent[i]).children('.ques-content-question'); //题目对象
		var questionid = $(questions).attr('questionid'); //问题ID
		var multiple = $(questions).attr('Multiple1'); //问题单选或多选，0单选 1多选

		var question = {}; //存储问题对象
		question.QuestionId = questionid; //问题ID
		question.Answers = []; //答案集合

		//获取答案中的自定义文本输入框框
		var textAnswer = $(questionParent[i]).find('input[type=text]');
		if(textAnswer) {
			if(textAnswer.val() == '') { //自定义文本框没有作答,弹出提示

			}
			var tempAnswers1 = {}; //存储临时的答案ID
			tempAnswers1.AnswerId = textAnswer.attr('answerid'); //文本框答案ID
			tempAnswers1.CustomizeAnswer = textAnswer.val(); //文本框内
			if(JSON.stringify(tempAnswers1) != "{}") //判断对象不为空时添加 避免添加空对象
				question.Answers.push(tempAnswers1);
		}
		if(multiple == 0) { //单选
			var tempAnswers2 = {}; //存储临时的答案ID
			var answer = $(questionParent[i]).find('input[type=radio]:checked');
			if(answer.length > 0) {
				tempAnswers2.AnswerId = answer.attr('answerid');
				if(JSON.stringify(tempAnswers2) != "{}") //判断对象不为空时添加 避免添加空对象
					question.Answers.push(tempAnswers2);
			} else { // 单选题没有选中答案

			}

		} else if(multiple == 1) { //多选
			var answerCheckboxs = $(questionParent[i]).find('input[type=checkbox]:checked'); //获取选中的多选元素

			if(answerCheckboxs.length) {
				for(var j = 0; j < answerCheckboxs.length; j++) { //多选
					var tempAnswers3 = {}; //存储临时的答案ID
					tempAnswers3.AnswerId = $(answerCheckboxs[j]).attr('answerid');

					if(JSON.stringify(tempAnswers3) != "{}")
						question.Answers.push(tempAnswers3);

				}
			} else { //多选题没有作答

			}
		}

		//答案对象
		testList.push(question); //问题对象

	}

	var testListJson = testList;
	console.info(testListJson);
	return testListJson;
}

function daojishi() {
	if(counttime >= 0) {
		var ms = counttime % 60; //余数 89%60==29秒
		var mis = Math.floor(counttime / 60); //分钟
		if(mis >= 60) {
			var hour = Math.floor(mis / 60);
			mis = Math.floor((counttime - hour * 60 * 60) / 60);
			$("#countdown").html(hour + "小时" + mis + "分" + ms + "秒数");
		} else if(mis >= 1) {
			$("#countdown").html(mis + "分" + ms + "秒数");
		} else {
			$("#countdown").html(ms + "秒数");
		}

		counttime--;
		vartt = window.setTimeout("daojishi()", 1000);
	} else { //倒计时结束，自动交卷
		window.clearTimeout(vartt);
		bootoast({
			message: '考试时间结束,请单击提交',
			type: 'warning',
			position: 'toast-top-center',
			timeout: 3
		});
		//window.confirm("考试时间结束,请单击提交");
		submit(testId,token,appId); //提交答案
	}

}