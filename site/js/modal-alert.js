//type  0 普通提示框  1关系人员筛选 3
/**
 * 
 * @param {*} type 0普通提示框 1关系人员筛选 2 输入框(计划添加)
 * @param {*} info  type 0  info.content 提示内容 只显示一个确定按钮
 *                  type 1  查人员接口，填充入content  输出 personInfo{name,id}
 *                  type 2  计划编辑接口 textare 内容输出
 */
function Modal(type, info, cb) {
    this._init_(type, info, cb);
}
Modal.prototype = {
    _init_: function(type, info, cb) {
        this.type = type;
        this.title = info.title;
        this.content = "";
        this.cb = cb || '';
        this.selectItem = '';
        if (type == 0) {
            this.content = info.content;
            this.modalShow();
        } else if (type == 1) {
            this.content = '数据加载中...';
            this.getUserRelationship(info.data);
            this.modalShow();
        } else if (type == 2) {
            this.content = `<textarea id="step-content">${info.content}</textarea>`;
            this.modalShow();
        }
    },
    //查询列表
    getUserRelationship: function(data) {
        var that = this;
        $.ajax({
            type: "POST",
            url: DOMAIN + "/api/User/GetUserRelationship",
            data: data,
            success: function(data) {
                console.log("GetUserRelationship---", data);
                if (data.Code == "00") {
                    if (data.Result.UserList.length == 0) {
                        $('#modal .modal-body').html('暂无数据');
                    } else {
                        that.dealRelationList(data.Result.UserList);
                    }
                } else {
                    that.content = '查询出错';
                }
            },
            error: function() {
                that.content = '查询出错';
            }
        });
    },
    modalShow: function() {
        var btnArea = ``;
        if (this.type == 0) {
            btnArea = `<button type="button" class="btn btn-primary" data-dismiss="modal">确定</button>`;
        } else if (this.type == 1) {
            btnArea = `<button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
            <button type="button" class="btn btn-primary">确定</button>`;
        } else if (this.type == 2) {
            btnArea = `<button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
            <button type="button" class="btn btn-primary">保存</button>`;
        }
        var str = `<div class="modal fade" tabindex="-1" role="dialog" id="modal">
                    <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title">${this.title}</h4>
                        </div>
                        <div class="modal-body">
                        ${this.content}
                        </div>
                        <div class='tip'></div>
                        <div class="modal-footer">
                        ${btnArea}
                        </div>
                    </div><!-- /.modal-content -->
                    </div><!-- /.modal-dialog -->
                    </div><!-- /.modal -->`;
        $('body').append(str);
        //关闭
        $('.close,.btn-default').click(function() {
            $("#modal").remove();
        });
        //保存 确定
        var that = this;
        $('.btn-primary').click(function() {
            if (that.type == 1) {
                if (that.selectItem) {
                    that.cb(that.selectItem);
                    $("#modal").remove();
                    $(".modal-backdrop").remove();
                } else {
                    $('#modal .tip').html('计划内容不能为空');
                }
            } else if (that.type == 2) {
                var str = $("#step-content").val();
                if (str) {
                    that.cb(str);
                    $("#modal").remove();
                    $(".modal-backdrop").remove();
                } else {
                    $('#modal .tip').html('计划内容不能为空');
                }
            }
        });
    },
    dealRelationList: function(list) {
        var content = `<div class="subordinates">`;
        list.forEach((element, index) => {
            content += `<div class="subordinate-item" id="subordinate-${index}">
            <span>${element.Name == null ? element.UserId : element.Name}</span>
            <div class="right-dir">
                <span class="glyphicon" aria-hidden="true"></span>
            </div>
        </div>`;
        });
        content += `</div>`;
        $('#modal .modal-body').html(content);
        var that = this;
        $(".subordinate-item").click(function() {
            $('.subordinates .glyphicon').removeClass('glyphicon-ok');
            $($(this).find('.glyphicon')).addClass('glyphicon-ok');
            var id = $(this).attr("id");
            var arr = id.split("-");
            that.selectItem = list[arr[1]];
        });
    }
};