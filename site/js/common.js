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
                str +=
                    `<div class="file">
                    <img src="icons/downloadfolder@2x.png">
                    <a href="${element.AttachmentDownloadUrl}" target="_blank">${element.AttachName}</a>
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
if(localStorage.getItem('TokenExpTime')!=null&&localStorage.getItem('TokenExpTime')){
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
	if (dataObjData != "" && dataObjData != null) {
	} else {
	    localStorage.removeItem("token"); //清除token的值
	    localStorage.removeItem("TokenExpTime"); //清楚时间
	    //alert("获取的信息已经过期");
	}
}

