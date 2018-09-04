function Comment(info) {
    this._init_(info);
}

Comment.prototype = {
    constructor: Comment,
    _init_: function(info) {
        this.commentType = info.commentType;
        this.id = info.id;
        this.userType = info.userType;
        this.appId = info.appId;
        this.token = info.token;
        this.userId = info.userId;
        this.flag = false;
        $(".comment-content").html(`<div class="title">评论
                <span class="num"></span>
                <div class="edit-comment">
                    <span class="glyphicon glyphicon-edit"></span>写评论
                </div>
            </div>
            <div class="comment-edit hide">
                <textarea id="comment" name="" placeholder="请输入评论内容" id="" cols="30" rows="10"></textarea>
                <div class="sub-comment">提交评论</div>
            </div>
            <div class="comment-area">
            </div>`).removeClass('hide');
        //写评论
        $(".edit-comment").click(function() {
            if(!info.token){
                window.location.href = 'login.html?appId=' + info.appId + '';
                return;
            }
            if ($('.comment-edit').hasClass('hide')) {
                $('.comment-edit').removeClass("hide");
            } else {
                $('.comment-edit').addClass("hide");
            }
        });
        var that = this;
        //提交评论
        $(".sub-comment").click(function() {
            var commentStr = $("#comment").val();
            if (commentStr) {
                that.flag = true;
                that.editUserComment(commentStr);
            }
        });
    },
    //查询评论
    getUserComment: function() {
        var parData = {
            CommentType: this.commentType,
            Id: this.id,
            UserId: this.userId,
            CommentStatus: this.userType,
            Token: this.token,
            AppId: this.appId
        };
        var that = this;
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/UserComment/GetUserComment",
            data: parData,
            success: function(data) {
                //console.log("GetUserComment---", data);
                if (data.Code == "00") {
                    $(".comment-content .num").html("("+data.Result.CommentList.length+")");
                    that.commentArea(data.Result.CommentList, that.userType);
                } else if (data.Code == '10') {
                    $(".comment-content").removeClass('hide');
                    $(".comment-area").html(`暂无评论数据`);
                } else {
                    alert(data.Code, data.Message);
                }
            }
        });
    },
    //提交评论
    editUserComment: function(str) {
        var parData = {
            CommentType: this.commentType,
            Id: this.id,
            CommentUserId: this.userId,
            Comment: str,
            Token: this.token,
            AppId: this.appId
        };
        var that = this;
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/UserComment/EditUserComment",
            data: parData,
            success: function(data) {
                console.log("EditUserComment---", data);
                if (data.Code == "00") {
                    $("#comment").val("");
                    $('.comment-edit').addClass("hide");
                    that.getUserComment();
                } else {
                    alert(data.Code, data.Message);
                }
            },
            complete: function() {
                that.flag = false;
            }
        });
    },
    //审核评论
    auditUserComment: function(id, status) {
        var that = this;
        if (that.flag == true) {
            return;
        }
        that.flag = true;
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/UserComment/AuditUserComment",
            data: {
                CommentId: id,
                ReviewStatus: status,
                Token: this.token,
                AppId: this.appId
            },
            success: function(data) {
                console.log("AuditUserComment---", data);
                if (data.Code == "00") {
                    that.getUserComment();
                } else {
                    alert(data.Code, data.Message);
                }
            },
            complete: function() {
                that.flag = false;
            }
        });
    },
    //评论区域展示
    commentArea: function(list, userType) {
        var commentStr = ``;
        var commentOper = function(status, id) {
            var statusStr = ``;
            if (status == "-1" && userType == 0) {
                statusStr = `<div class="item-status">
                <div class="c-nopass c-status" id="${id}">审核不通过</div>
                <div class="c-pass c-status" id="${id}">审核通过</div>
            </div>`;
            }
            return statusStr;
        };
        var commentStatus = function(status) {
            if (status == "1") {
                return `<span style="color:red;">(不通过)</span>`;
            }
            return ``;
        };
        list.forEach(element => {
            commentStr += `<div class="comment-item">
            <div class="item-head">
                <div class="item-name">${
                  element.CommentUserName
                }${commentStatus(element.CommentStatus)}</div>
                ${commentOper(element.CommentStatus, element.CommentId)}
            </div>
            <div class="item-content">${element.CommentContent}</div>
            <div class="item-time">${element.CommentTime}</div>
        </div>`;
        });
        $(".comment-area").html(commentStr);
        var that = this;
        if (userType == 0) {
            //审核不通过
            $(".c-nopass").click(function() {
                var id = $(this).attr("id");
                that.auditUserComment(id, 1);
            });
            //审核通过
            $(".c-pass").click(function() {
                var id = $(this).attr("id");
                that.auditUserComment(id, 0);
            });
        }
    }
};