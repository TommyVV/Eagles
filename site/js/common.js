const DOMAIN = 'http://www.51service.xyz/Eagles';
const UPLOAD = "http://www.51service.xyz/Eagles/api/Upload/UploadFile";

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
function attachmentList(list) {
    var str = ``;
    if (list) {
        list.forEach(element => {
            if (element.AttachmentDownloadUrl || element.AttachName) {
                str += `<div class="file">
                    <span class="glyphicon glyphicon-paperclip"></span>
                    <a href="${element.AttachmentDownloadUrl}" target="_blank">${element.AttachName}</a>
                </div>`;
            }
        });
    }
    return str;
}