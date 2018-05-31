requirejs.config({
    // baseUrl: '',
    paths: {
        jquery: '../thirdparty/jquery-3.3.1.min',
        Bootstrop:'../thirdparty/bootstrap/js/bootstrap.min',
    },
    shim: {
        Bootstrop: {
            exports:"Bootstrop",
            deps:['jquery'],
        }
    }
});

require(['jquery', 'Bootstrop'], function ($) {
    console.log("Hello World!");
})