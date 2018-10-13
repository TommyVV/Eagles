const DOMAIN = 'http://www.51service.xyz/Eagles';
const UPLOAD = "http://www.51service.xyz/Eagles/api/Upload/UploadFile";
//上传图片
const PICSIZE = 1000000;
//附件尺寸
const FILESIZE = 10000000;
//字数限制 文章、心得体会、入党申请书、会议纪要字数多少可以在后台设置
const DEFAULT_FONE_COUNT = 100;
const ARTICLE_FONE_COUNT = 100;
const EXPERIENCE_FONE_COUNT = 100;
const MEETING_FONE_COUNT = 100;
const APPLICATION_FONE_COUNT = 100;
//入党申请书提交成功
function introduceTip(intro){
    return "入党申请书提交成功,系统将通知到您的介绍人："+intro+"同志";
}
// const DOMAIN = 'http://ruisuikj.com/Eagles';
// const UPLOAD = "http://ruisuikj.com/Eagles/api/Upload/UploadFile";

function getRequest(name) {
    var url = window.location.href; //获取当前页面的URL
    var result;
    result = url.match("(\\?|&)" + name + "=([^&$]*)");
    result = (result == null || result.length == 1) ? "" :
        decodeURIComponent(result[2]);
    return result;
}
/**
 * 设置cookie
 * @param {[type]} key   [键名]
 * @param {[type]} value [键值]
 * @param {[type]} days  [保存的时间（天）]
 */
function setCookie(key, value, days) {
    // 设置过期原则
    if (!value) {
        localStorage.removeItem(key)
    } else {
        var Days = days || 7; // 默认保留7天
        var exp = new Date();
        localStorage.setItem(key, JSON.stringify({
            value,
            expires: exp.getTime() + Days * 24 * 60 * 60 * 1000
        }))
    }
}
//发布按钮点击
function pulicContent(url) {
    var elementStr = `<div class="float-layer">
            <img src="icons/class_partyjob@3x.png?v=1" alt="" srcset="">
        </div>`;
    $("body").append(elementStr);
    $(".float-layer").click(function() {
        window.location.href = url;
    });
}

function getCookie(name) {
    return localStorage.getItem(name);
    // try {
    //     var o = JSON.parse(localStorage.getItem(name))
    //     if (!o || o.expires < Date.now()) {
    //         return null
    //     } else {
    //         return o.value
    //     }
    // } catch (e) {
    //     // 兼容其他localstorage
    //     console.log(e)
    //     return localStorage.getItem(name);
    // } finally {}
}
//附件列表
function attachmentList(list, type) {
    var str = ``;
    if (list) {
        list.forEach(element => {
            if (element.AttachmentDownloadUrl || element.AttachName) {
                var imgSrc = "icons/file/defaultFile.png";
                var imgUrl = element.AttachmentDownloadUrl;
                if (/\.(gif|jpg|jpeg|png|GIF|JPG|PNG)$/.test(imgUrl)) {
                    imgSrc = "icons/file/pic.png";
                } else if (/\.(pdf)$/.test(imgUrl)) {
                    imgSrc = "icons/file/pdf.png";
                } else if (/\.(mp4|rmvb|avi|ts)$/.test(imgUrl)) {
                    imgSrc = "icons/file/media.png";
                } else if (/\.(rar|zip)$/.test(imgUrl)) {
                    imgSrc = "icons/file/zip.png";
                } else if (/\.(xls|xlsx)$/.test(imgUrl)) {
                    imgSrc = "icons/file/excel.png";
                } else if (/\.(mp3)$/.test(imgUrl)) {
                    imgSrc = "icons/file/music.png";
                } else if (/\.(ppt|pptx)$/.test(imgUrl)) {
                    imgSrc = "icons/file/ppt.png";
                } else if (/\.(txt)$/.test(imgUrl)) {
                    imgSrc = "icons/file/txt.png";
                } else if (/\.(doc|docx)$/.test(imgUrl)) {
                    imgSrc = "icons/file/txt.png";
                }
                str +=
                    `<div class="file ${type==1?'file-oper':''}">
                    <img src="${imgSrc}">
                    <a href="${element.AttachmentDownloadUrl}" target="_blank">${element.AttachName}</a>
                    ${type==1?'<span class="glyphicon glyphicon-remove"></span>':''}
                </div>`;
            }
        });
    }
    return str;
}
//封装过期控制代码
function setLocalStorage(key, value) {
    var curTime = new Date().getTime();
    localStorage.setItem(key, JSON.stringify({
        data: value,
        time: curTime
    }));
}

function getLocalStorage(key, exp) {
    var data = localStorage.getItem(key);
    var dataObj = JSON.parse(data);
    if (new Date().getTime() - dataObj.time > exp) {
        console.log('信息已过期');
    } else {
        var dataObjDatatoJson = JSON.parse(dataObj.data)
        return dataObjDatatoJson;
    }
}
if (localStorage.getItem('TokenExpTime') != null && localStorage.getItem('TokenExpTime')) {
    var value = localStorage.getItem("token");
    setLocalStorage('information', JSON.stringify(value));
    var str = localStorage.getItem('TokenExpTime')
    str = str.replace(/-/g, '/'); // 将-替换成/，因为下面这个构造函数只支持/分隔的日期字符串
    var date1 = new Date(str); // 构造一个日期型数据，值为传入的字符串
    var date2 = new Date()
    var s1 = date1.getTime(),
        s2 = date2.getTime();
    var s = Math.round((s1 - s2))
    var dataObjData = getLocalStorage('information', s)
    if (dataObjData != "" && dataObjData != null) {} else {
        //alert("获取的token已经过期");
        localStorage.removeItem("token"); //清除token的值
        localStorage.removeItem("TokenExpTime"); //清楚时间

    }
}