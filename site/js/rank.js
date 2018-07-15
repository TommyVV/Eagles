if(!localStorage.getItem('token')) {
	window.location.href = "login.html"
}
$('#top-nav,#mobilenav').load('./head.html')
var token = localStorage.getItem("token");
var AppId = 10000000;
rank(token, AppId);

function rank(token, AppId) {
	$.ajax({
		type: "post",
		data: {
			"Token": token,
			"AppId": AppId
		},
		url: "http://51service.xyz/Eagles/api/Score/GetScoreRank",
		dataType: "json",
		success: function(res) {
			$('#t_bodys,#t_bodystwo').html('')
			var data = res.Result;
			if(res.Code == 00) {
				var rankdy = '';
				var rankzb = '';
				if(data.UserRank != '' && data.UserRank != null)
					for(var i = 0; i < data.UserRank.length; i++) {
						console.log(data.UserRank);
						var imgs = '';
						if(data.UserRank[i].Rank == 1) {
							imgs = '<img src="icons/gold@2x.png" alt="">';
						} else if(data.UserRank[i].Rank == 2) {
							imgs = '<img src="icons/silver@2x.png" alt="">';
						} else if(data.UserRank[i].Rank == 3) {
							imgs = '<img src="icons/copper@2x.png" alt="">';
						}
						if(i < 3) {
							console.log(data.UserRank[i])
							rankdy += '<tr class="list">' +
								'<td>' + imgs + '</td>' +
								'<td class="name">' + data.UserRank[i].Name + '</td>' +
								'<td>' + data.UserRank[i].BranchName + '</td>' +
								'<td>' + data.UserRank[i].Score + '</td>' +
								'</tr>';
						} else if(i >= 3) {
							rankdy += '<tr class="list">' +
								'<td>' + data.UserRank[i].Rank + '</td>' +
								'<td class="name">' + data.UserRank[i].Name + '</td>' +
								'<td>' + data.UserRank[i].BranchName + '</td>' +
								'<td>' + data.UserRank[i].Score + '</td>' +
								'</tr>';
						}

					}
			}
			if(data.BranchRank != '' && data.BranchRank != null)
				for(var i = 0; i < data.BranchRank.length; i++) {
					var imgs = '';
					if(data.BranchRank[i].Rank == 1) {
						imgs = '<img src="icons/gold@2x.png" alt="">';
					} else if(data.BranchRank[i].Rank == 2) {
						imgs = '<img src="icons/silver@2x.png" alt="">';
					} else if(data.BranchRank[i].Rank == 3) {
						imgs = '<img src="icons/copper@2x.png" alt="">';
					}

					if(i < 3) {
						rankzb += '<tr class="list">' +
							'<td>' + imgs + '</td>' +
							'<td class="name">' + data.BranchRank[i].Branch + '</td>' +
							'<td>' + data.BranchRank[i].UserCount + '</td>' +
							'<td>' + data.BranchRank[i].Score + '</td>' +
							'</tr>';
					} else if(i >= 3) {
						rankzb += '<tr class="list">' +
							'<td>' + data.BranchRank[i].Rank + '</td>' +
							'<td class="name">' + data.BranchRank[i].Branch + '</td>' +
							'<td>' + data.BranchRank[i].UserCount + '</td>' +
							'<td>' + data.BranchRank[i].Score + '</td>' +
							'</tr>';
					}

				}

			$('#t_bodys').append(rankdy); //文章列表
			$('#t_bodystwo').append(rankzb); //文章列表
		}

	})
}