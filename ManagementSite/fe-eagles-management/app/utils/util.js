Date.prototype.format = function(fmt) {
  var o = {
    "M+": this.getMonth() + 1, //月份
    "d+": this.getDate(), //日
    "h+": this.getHours(), //小时
    "m+": this.getMinutes(), //分
    "s+": this.getSeconds(), //秒
    "q+": Math.floor((this.getMonth() + 3) / 3), //季度
    S: this.getMilliseconds() //毫秒
  };
  if (/(y+)/.test(fmt)) {
    fmt = fmt.replace(
      RegExp.$1,
      (this.getFullYear() + "").substr(4 - RegExp.$1.length)
    );
  }
  for (var k in o) {
    if (new RegExp("(" + k + ")").test(fmt)) {
      fmt = fmt.replace(
        RegExp.$1,
        RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length)
      );
    }
  }
  return fmt;
};

/*
 工具类
 */
const Utils = {
  // 获取url中所有的参数
  getParams(url) {
    var vars = {},
      hash,
      hashes,
      i;

    url = url || window.location.href;

    // 没有参数的情况
    if (url.indexOf("?") == -1) {
      return vars;
    }

    hashes = url.slice(url.indexOf("?") + 1).split("&");

    for (i = 0; i < hashes.length; i++) {
      if (!hashes[i] || hashes[i].indexOf("=") == -1) {
        continue;
      }
      hash = hashes[i].split("=");
      if (hash[1]) {
        vars[hash[0]] =
          hash[1].indexOf("#") != -1
            ? hash[1].slice(0, hash[1].indexOf("#"))
            : hash[1];
      }
    }
    return vars;
  },

  // 获取指定name的参数
  getParam(name, url) {
    return this.getParams(url)[name];
  },

  getCurrentParam(name) {
    return this.getParam(name, location.href);
  },
  /**
   * 获取OrgId和BranchId
   * @return {string} OrgId和BranchId
   */
  getOrgIdAndBranchId() {
    let Info = localStorage.info ? JSON.parse(localStorage.info) : {};
    let { OrgId, BranchId } = Info;
    return {
      OrgId,
      BranchId
    };
  },
  /**
   * 判断是否小于今天
   * @param {*} startTime 日期字符串
   */
  isLessThanCurrentDay(startTime) {
    let todayTimestamp = Date.parse(new Date(this.dateConvent(new Date()))); // 今天的时间戳
    let paramTimestamp = Date.parse(new Date(startTime)); // 传入参数的时间戳
    console.log(paramTimestamp < todayTimestamp);
    return paramTimestamp < todayTimestamp;
  },
  /**
   *  将日期转换为日期格式字符串
   * @param {*} date
   */
  dateConvent(date) {
    var year = date.getFullYear();
    var month = date.getMonth() + 1;
    month = month > 9 ? month : "0" + month;
    var day = date.getDate();
    day = day > 9 ? day : "0" + day;

    return year + "-" + month + "-" + day;
  },
  /**
   *  将日期时间戳转换为日期
   * @param {*} 时间戳
   */
  timeStampConvent(timeStamp, format = "yyyy-MM-dd") {
    // var time = new Date(parseInt(timeStamp)).toLocaleString();
    // return time.replace(/\//g, '-').slice(0, time.indexOf(' '));
    return new Date(parseInt(timeStamp)).format(format);
  },
  /**
   * 获取日期格式化规则
   * 1小时内，显示多少分钟前
   * 24小时内，显示多少小时前
   * 大于24小时，显示日期
   * @param {string} 日期时间戳
   * @returns {string}
   */
  getFormatTime(timestamp) {
    let dateStr = ``;
    const formatDate = Date.parse(new Date(timestamp));
    const today = new Date(); // 当前时间
    const minutes = Math.floor(
      parseInt(today - new Date(timestamp)) / 1000 / 60
    ); // 距离现在的间隔的分钟数
    const hours = Math.floor(minutes / 60); // 距离现在的间隔的小时数
    if (minutes < 60) {
      dateStr = `${minutes == 0 ? 1 : minutes}分钟前`;
    } else if (hours < 24) {
      dateStr = `${hours}小时前`;
    } else {
      dateStr = new Date(timestamp).format("yyyy-MM-dd");
    }
    return dateStr;
  },
  /**
   * 判断对象是否为空
   * @param {*} object
   */
  isEmptyObj(object) {
    for (let key in object) {
      return false;
    }
    return true;
  },

  checkQTClient(nextState, replace) {
    // const ClientInfo = qt.getClientInfo();
    // 是否为生产环境
    // const checkStatus =
    //   nextState.location.pathname !== '/ErrorPage' &&
    //   ClientInfo.type !== 'ios' &&
    //   ClientInfo.type !== 'android';
    // if (checkStatus) {
    //   replace('/pcindex');
    // }
    if (!navigator.userAgent.match(/(iPhone|iPod|Android|ios|iPad)/i)) {
      if (nextState.location.pathname.indexOf("op") < 0) {
        replace("/pcindex");
      }
    }
  },
  is_Mobile() {
    return navigator.userAgent.match(/(iPhone|iPod|Android|ios|iPad)/i);
  },
  is_iOS() {
    var ua = navigator.userAgent.toLowerCase();
    if (ua.match(/iPhone/i) == "iphone") {
      return true;
    } else {
      return false;
    }
  },
  /**
   * 往缓存里面写token和refresh_token
   * @param {*} token
   * @param {*} refresh_token
   */
  setLoaclStorage(token, refresh_token) {
    let old_localStorage = localStorage.info
      ? JSON.parse(localStorage.info)
      : {};
    let info = {
      ...old_localStorage,
      token,
      refresh_token
    };
    localStorage.info = JSON.stringify(info);
  },

  //时间戳转为日期格式
  formatTime(datetime = 0, pattern) {
    let chenck = Number(datetime);
    let time1;
    if (!isNaN(chenck)) {
      time1 = chenck;
    } else {
      time1 = new Date(datetime).getTime();
    }
    let date = new Date(time1);
    var o = {
      "M+": date.getMonth() + 1, //月份
      "d+": date.getDate(), //日
      "h+": date.getHours(), //小时
      "m+": date.getMinutes(), //分
      "s+": date.getSeconds(), //秒
      "q+": Math.floor((date.getMonth() + 3) / 3), //季度
      S: date.getMilliseconds(), //毫秒,
      W: (function(week) {
        var str;
        switch (week) {
          case 0:
          case 1:
          case 2:
          case 3:
          case 4:
          case 5:
          case 6:
            str = week;
            break;
          default:
            str = "";
        }
        return str;
      })(date.getDay())
    };
    if (/(y+)/.test(pattern))
      pattern = pattern.replace(
        RegExp.$1,
        (date.getFullYear() + "").substr(4 - RegExp.$1.length)
      );
    for (var k in o)
      if (new RegExp("(" + k + ")").test(pattern))
        pattern = pattern.replace(
          RegExp.$1,
          RegExp.$1.length === 1
            ? o[k]
            : ("00" + o[k]).substr(("" + o[k]).length)
        );
    return pattern;
  },
  // 滚动到指定的锚点
  scrollToAnchor(anchorName) {
    if (anchorName) {
      // 找到锚点
      let anchorElement = document.getElementsByClassName(anchorName)[0];
      // 如果对应id的锚点存在，就跳转到锚点
      if (anchorElement) {
        anchorElement.scrollIntoView({ block: "start", behavior: "smooth" });
      }
    }
  },
  // 高亮搜索结果
  lightKeyword(content, keyword) {
    keyword = keyword && keyword.trim();
    if (content && keyword) {
      const pattern = new RegExp(keyword, "gi");
      return content.replace(
        pattern,
        `<font style='color:#fe0000'>${keyword}</font>`
      );
    } else {
      return content;
    }
  },
  // 输入框过滤表情
  filterEmoji(str) {
    return str.replace(
      /[\uD83C|\uD83D|\uD83E][\uDC00-\uDFFF][\u200D|\uFE0F]|[\uD83C|\uD83D|\uD83E][\uDC00-\uDFFF]|[0-9|*|#]\uFE0F\u20E3|[0-9|#]\u20E3|[\u203C-\u3299]\uFE0F\u200D|[\u203C-\u3299]\uFE0F|[\u2122-\u2B55]|\u303D|[\A9|\AE]\u3030|\uA9|\uAE|\u3030/gi,
      ""
    );
  },
  partten: {
    phone: /^1[3-9]\d{9}$/
  }
};

export default Utils;
