const CONFIG = {
    api: '',
    domain: '',
    questionId: ''
}

class Formatter {
    constructor(msec = 0) {
        this.msec = msec;
        this.html = '';
    }
    format() {
        let sec = (this.msec / 1000) % 60 + '';
        let min = ((this.msec / 1000) - sec) / 60 + '';
        this.html = (min.length == 1 ? '0' + min : min) + '分钟' + (sec.length == 1 ? '0' + sec : sec) + '秒';
    }
}

class Countdown extends Formatter {
    constructor({ msec = 0 }) {
        super(msec);
        this.start();
    }
    start() {
        this.interval = setInterval(() => {
            this.msec -= 1000;
            if (this.msec < 0) {
                this.clear();
            } else {
                this.format();
                this.renderHtml();
            }
        }, 1000);
    }
    clear() {
        clearInterval(this.interval);
    }
    renderHtml() {
        $('#countdown').html(this.html);
    }
}



class Action {
    constructor() {
        this.questionId = '';
    }
    init() {
        console.log(this.questionId);
    }
    getQuestion() {

    }
    get() {
        $.ajax({
            method: 'GET',
            url: ''
        })
    }
    post() {
        $.ajax({
            method: 'POST'
        })
    }
    renderHtml() {

    }
}



class Modal {
    constructor() {
        this.init();
    }
}


let countdown = new Countdown({ msec: 9000 });
let action = new Action();


Object.defineProperty(action, 'questionId', {
    configurable: false,
    enumerable: false,
    get() {
        return CONFIG.questionId;
    },
    set(val) {
        CONFIG.questionId = val;
        action.getQuestion();
    }
})



$('#ques-content-button').on('click', (e) => {
    let id = e.target.id;
    switch (id) {
        case 'prev':
            action.questionId = '';
            break;
        case 'next':
            action.questionId = '';
            break;
        case 'submit':
            action.questionId = '';
            break;
        default:
            break;
    }
})


$('#num-modal').on('click', () => {
    action.questionId = 543;
    // return;
    $('#ques-modal').show();
    $('#ques-modal-wrap').removeClass('list-hide').addClass('list-show');
})
$('#ques-modal').on('click', (e) => {
    if (e.target.className === 'ques-modal') {
        $('#ques-modal').hide();
    }
    if (e.target.nodeName === 'SPAN') {

    }
})
$('#ques-modal').on('touchmove', (e) => {
    // e.preventDefault();
    e.stopPropagation()
    console.log(123)
})





