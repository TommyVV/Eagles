var appId = getRequest('appId');
var token = localStorage.getItem("token")
if(!localStorage.getItem('token')) {
	window.location.href = 'login.html?appId=' + appId + '';
}
$('#top-nav,#mobilenav').load('head.html')
$('#pc-footer').load('./footer.html')

myInfo(token, appId) //个人信息
$('.img_float').click(function() {
	console.log(8)
	$('.operate').show();
})

function myInfo(token, appId) {
	$.ajax({
		type: "post",
		data: {
			"Token": token,
			"AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/User/GetUserInfo",
		dataType: "json",
		success: function(res) {

			if(res.Code == 00) {
				if(res.Result != '' && res.Result != null) {
					var data = res.Result.ResultUserInfo;
					$('#ld_img,.head-pic').attr('src', data.PhotoUrl);
					$('.ld_name').text(data.Name);
					$('.ld_sex').text(data.Gender);
					$('.ld_h').text(data.Ethnic);
					$('.ld_datas').text(data.Birth);
					$('.ld_des').text(data.Origin);
					$('.ld_chengs').text(data.OriginAddress);
					$('.ld_czds').text(data.Address);
					$('.ld_phone').text(data.Telphone);
					$('.ld_sfzh').text(data.IdCard);
					$('.ld_gzd').text(data.Education);
					$('.ld_byz').text(data.School);
					$('.ld_bjy').text(data.Employer);
					$('.ld_dak').text(data.Department);
					$('.ld_dedata').text(data.PrepPartyDate);
					$('.ld_zgdata').text(data.FormalPartyDat);
					$('.ld_selib').text(data.PartyType);
					$('.ld_status').text(data.PartyMembershipDues);
					$('.ld_myzf').text(data.Branch);
					$('.ld_rzbdata').text(data.JoinBranchDate);
					$('.ld_zhizpeople').text(data.MyOrganization);
					$('.ld_peopledata').text(data.JoinOrganizationDate);
					$('.ld_pedata').text(data.MembershipStatus);
					$('.ld_nameinput').val(data.Name)
					$('.ld_sexinput').val(data.Gender)
					$('.ld_hinput').val(data.Ethnic)
					$('.ld_datasinput').val(data.Birth)
					$('.ld_desinput').val(data.Origin)
					$('.ld_chengsinput').val(data.OriginAddress)
					$('.ld_czdsinput').val(data.Address)
					$('.ld_phoneinput').val(data.Telphone)
					$('.ld_bjyinput').val(data.Employer)
					$('.ld_dakinput').val(data.Department)

				}
			}
		}
	})
}
$('.lg_left').click(function() {
	$(this).siblings().find('label').hide()
	$(this).siblings().find('input').show()
	$(this).siblings().find('input').focus();
})
$('#fileupload').fileupload({
	dataType: 'json',
	url: "http://51service.xyz/Eagles/api/Upload/UploadFile", //文件的后台接受地址
	//设置进度条
	progressall: function(e, data) {},
	//上传完成之后的操作，显示在img里面
	done: function(e, data) {

		$("#ld_img").attr("src", data.result.Result.FileUploadResults[0].FileUrl);
	}
});
$('.confirmBtn').on('click', function() {
	var RequestUserInfo = {};

	RequestUserInfo.PhotoUrl = $('#ld_img').attr('src');
	RequestUserInfo.Name = $('.ld_nameinput').val();
	RequestUserInfo.Gender = $('.ld_sexinput').val();
	RequestUserInfo.Ethnic = $('.ld_hinput').val()
	RequestUserInfo.Birth = $('.ld_datasinput').val()
	RequestUserInfo.Origin = $('.ld_desinput').val()
	RequestUserInfo.OriginAddress = $('.ld_chengsinput').val()
	RequestUserInfo.Address = $('.ld_czdsinput').val()
	RequestUserInfo.Telphone = $('.ld_phoneinput').val()
	RequestUserInfo.Employer = $('.ld_bjyinput').val()
	RequestUserInfo.Department = $('.ld_dakinput').val()
	$.ajax({
		type: "post",
		data: {
			"RequestUserInfo": JSON.stringify(RequestUserInfo),
			"Token": token,
			"AppId": appId
		},
		url: "http://51service.xyz/Eagles/api/User/GetUserInfo",
		dataType: "json",
		success: function(res) {

			if(res.Code == 00) {
				window.location.href = 'mine.html?appId='+appId
			}
		}
	})
})