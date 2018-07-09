$('#pc-header').load('./head.html')
$('#pc-footer').load('./footer.html')
var token=localStorage.getItem("token")
myInfo(token)//个人信息
function myInfo(token){
	$.ajax({
        type: "post",
		data: {
			  "Token": token,
			  "AppId": 10000000
		},
		url: "http://51service.xyz/Eagles/api/User/GetUserInfo",
		dataType: "json",
        success:function(res){
        	var data=res.Result.UserInfo;
            if(res.Code == 00){
            		$('#ld_img,.head-pic').attr('src',data.PhotoUrl); 
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
            }
        }
	})
}