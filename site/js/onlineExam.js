$('#exam-button-jump').on('click', () => {
    window.location.href = './onlineExamQuestion.html'
})


let isMobile = /Android|webOS|iPhone|iPod|BlackBerry/i.test(navigator.userAgent);
console.log(isMobile)
if (isMobile) $('.exam').show();
