const DOMAIN = 'http://www.51service.xyz/Eagles/swagger/ui/index';

$('.btn-signup').on('click', function (e) {
    e.preventDefault();

    let password = $('#inputPassword').val();
    let account = $('#inputUser').val();
    let captcha = $('#inputCaptcha').val();
    if (!account) {
        alert('请输入手机号');
        return;
    } else if (!password) {
        alert('请输入密码');
        return;
    } else if (!captcha) {
        alert('请输入验证码');
        return;
    }

    window.location.href = "./index.html";
})
