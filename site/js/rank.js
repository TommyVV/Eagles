var token = localStorage.getItem("token");
var appId = getRequest('appId');
var onurl=window.location.href
if(!localStorage.getItem('token')||localStorage.getItem('IsInternalUser')==0) {
	window.location.href = 'login.html?appId=' + appId + '&onurl='+encodeURI(onurl);
}
$('#top-nav,#mobilenav').load('./head.html')
$('#footer').load('./footer.html')
rank(token, appId);

function rank(token, appId) {
	$.ajax({
		type: "post",
		data: {
			"Token": token,
			"AppId": appId
		},
		url: DOMAIN + "/api/Score/GetScoreRank",
		dataType: "json",
		success: function(res) {
			$('#t_bodys,#t_bodystwo').html('')
			var data = res.Result;
			if(res.Code == 00) {
				console.info('22');

				var rankdy = '';
				var rankzb = '';
				if(data.UserRank != '' && data.UserRank != null)
					for(var i = 0; i < data.UserRank.length; i++) {
						console.log(data.UserRank);
						var imgs = '';
						if(data.UserRank[i].No == 1) {
							imgs = '<img src="icons/gold@2x.png" alt="">';
						} else if(data.UserRank[i].No == 2) {
							imgs = '<img src="icons/silver@2x.png" alt="">';
						} else if(data.UserRank[i].No == 3) {
							imgs = '<img src="icons/copper@2x.png" alt="">';
						}
						if(i < 3) {
							console.log(data.UserRank[i])
							rankdy += '<tr class="list">' +
								'<td>' + imgs + '</td>' +
								'<td class="name">' + data.UserRank[i].UserName + '</td>' +
								'<td>' + data.UserRank[i].BranchName + '</td>' +
								'<td>' + data.UserRank[i].Score + '</td>' +
								'</tr>';
						} else if(i >= 3) {
							rankdy += '<tr class="list">' +
								'<td>' + data.UserRank[i].No + '</td>' +
								'<td class="name">' + data.UserRank[i].UserName + '</td>' +
								'<td>' + data.UserRank[i].BranchName + '</td>' +
								'<td>' + data.UserRank[i].Score + '</td>' +
								'</tr>';
						}

					}

				if(data.BranchRank != '' && data.BranchRank != null) {
					for(var i = 0; i < data.BranchRank.length; i++) {
						var imgs = '';
						if(data.BranchRank[i].No == 1) {
							imgs = '<img src="icons/gold@2x.png" alt="">';
						} else if(data.BranchRank[i].No == 2) {
							imgs = '<img src="icons/silver@2x.png" alt="">';
						} else if(data.BranchRank[i].No == 3) {
							imgs = '<img src="icons/copper@2x.png" alt="">';
						}

						if(i < 3) {
							rankzb += '<tr class="list">' +
								'<td>' + imgs + '</td>' +
								'<td>' + data.BranchRank[i].BranchName + '</td>' +
								'<td>' + data.BranchRank[i].Score + '</td>' +
								'</tr>';
						} else if(i >= 3) {
							rankzb += '<tr class="list">' +
								'<td>' + data.BranchRank[i].No + '</td>' +
								'<td>' + data.BranchRank[i].BranchName + '</td>' +
								'<td>' + data.BranchRank[i].Score + '</td>' +
								'</tr>';
						}

					}
				}

				$('#t_bodys').append(rankdy); //文章列表
				$('#t_bodystwo').append(rankzb); //文章列表
			}
		}

	})
}