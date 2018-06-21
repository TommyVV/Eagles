(function ($) {
    let queryStr = window.location.search.substring(1);
    if (!queryStr) return;
    let queryStrArr = queryStr.split('&');
    let query = {};
    let pages = { index: 0, partyLearning: 1, partyWork: 2, mine: 3 };
    queryStrArr.forEach((i) => {
        let queryArr = i.split('=');
        query[queryArr[0]] = queryArr[1];
    })
    $('#level-one > li').removeClass('active').eq(pages[query.page]).addClass('active');
})(jQuery)

