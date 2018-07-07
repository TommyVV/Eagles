function getRequest(name) {
    var url = window.location.href;//获取当前页面的URL
    var result;
    result = url.match("(\\?|&)" + name + "=([^&$]*)");
    result = (result == null || result.length == 1) ? ""
            : decodeURIComponent(result[2]);
    return result;
}