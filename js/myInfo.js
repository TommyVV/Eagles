$('#pc-header').load('./head.html')
$('#pc-footer').load('./footer.html')
myInfo()//个人信息
function myInfo(){
	$.ajax({
        type: "post",
		data: {
			  "Token": "string",
			  "AppId": 10000000
		},
		url: "http://51service.xyz/Eagles/api/User/GetUserInfo",
		dataType: "json",
        success:function(res){
        	var data=res.Result;
            if(res.Code == 00){
            	$('#ld_img,.head-pic').attr('src',data.PhotoUrl); 
				$('.ld_name').text(data.Name);
				$('.ld_sex').text(Gender);
				$('.ld_h').text(Ethnic);
				$('.ld_datas').text(Birth);
				$('.ld_des').text(Origin);
				$('.ld_chengs').text(OriginAddress);
				$('.ld_czds').text(Address);
				$('.ld_phone').text(Telphone);
				$('.ld_sfzh').text(IdCard);
				$('.ld_gzd').text(Education);
				$('.ld_byz').text(School);
				$('.ld_bjy').text(Employer);
				$('.ld_dak').text(Department);
				$('.ld_dedata').text(PrepPartyDate);
				$('.ld_zgdata').text(FormalPartyDat);
				$('.ld_selib').text(PartyType);
				$('.ld_status').text(PartyMembershipDues);
				$('.ld_myzf').text(Branch);
				$('.ld_rzbdata').text(JoinBranchDate);
				$('.ld_zhizpeople').text(MyOrganization);
				$('.ld_peopledata').text(JoinOrganizationDate);
				$('.ld_pedata').text(MembershipStatus);
            }
        }
	})
}