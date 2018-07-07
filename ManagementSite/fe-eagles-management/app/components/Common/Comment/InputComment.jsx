import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import './style.less';
import { collectArticle, delCollectArticle, releaseComment } from '../../../services/commentService';
import { saveShareMap } from "../../../actions/mobile/shareAction";
import { saveDemandMap } from "../../../actions/mobile/demandAction";
import { showSuccess, showError } from '../../../actions/mobile/globalAction';
import Utils from '../../../utils/util';

@connect(
  state => {
    return {
      appReducer: state.appReducer,
      shareReducer: state.mobileShareReducer,
      demandReducer: state.mobileDemandReducer
    }
  },
  { saveShareMap, saveDemandMap, showSuccess, showError }
)
class InputComment extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isFocus: false,
      value: '',
      isRequesting: false, // 是否正在发送请求
      isScrollToComment: false, // 是否滚动到评论区
    };
  }

  changeHanle(e) {
    let textContent = e.target.innerText;
    console.log(textContent)
    const { value } = this.state;
    this.state.value = textContent;
    // 第一次输入去渲染页面
    console.log(this.state.value)
    if (textContent && !value) {
      this.setState({ value: textContent })
    }
    // 清空输入框的时候，再去渲染页面
    console.log(textContent.trim().length)
    console.log(this.state.value.trim().length)
    console.log(!textContent.trim().length && !this.state.value.trim().length)

    if (!textContent.trim().length && !this.state.value.trim().length) {
      console.log(textContent)
      this.setState({
        value: '',
        isFocus: true,
      });
    }
  }
  // 滚动消息
  scrollMsg(e) {
    let { isScrollToComment } = this.state;
    if (isScrollToComment) {
      Utils.scrollToAnchor('root');
    } else {
      Utils.scrollToAnchor('comment-anchor');
    }
    this.state.isScrollToComment = !isScrollToComment;
    // this.props.scrollMsg(this.state.isScrollToComment);
  }
  // 拼接参数
  appendParam() {
    const { id, type } = this.props;
    let param = {};
    // 0:分享的评论；1：项目需求的评论
    if (type == 'share') {
      param.type = 0;
      param.typeId = id;
    } else if (type == 'demand') {
      param.type = 1;
      param.typeId = id;
    }
    return param;
  }
  // 收藏文章(发送请求)
  collectArticleFn = async (e, isCollection) => {
    if (this.state.isRequesting) {
      return;
    }
    const { id, type } = this.props;
    const { shareMap } = this.props.shareReducer;
    const { demandMap } = this.props.demandReducer;
    let map = type == 'share' ? shareMap : demandMap;
    let detail = map.get(id);
    let param = this.appendParam();
    if (isCollection) {
      try {
        this.state.isRequesting = true;
        await delCollectArticle(param);
        this.state.isRequesting = false;
        detail.isCollection = 0;
        this.props.showSuccess("取消收藏成功");
      } catch (e) {
        this.props.showError("取消收藏失败");
        console.log(e);
      }
    } else {
      try {
        this.state.isRequesting = true;
        await collectArticle(param);
        this.state.isRequesting = false;
        detail.isCollection = 1;
        this.props.showSuccess("收藏成功");
      } catch (e) {
        this.props.showError("收藏失败");
        console.log(e);
      }
    }
    map.set(id, detail);
    if (type == 'share') {
      this.props.saveShareMap(map);
    } else if (type == 'demand') {
      this.props.saveDemandMap(map);
    }
  }
  // 发布评论
  releaseCommentFn = async () => {
    const { value } = this.state;
    if (!value) {
      return;
    }
    if (this.state.isRequesting) {
      return;
    }
    let param = this.appendParam();
    param.content = encodeURI(value);
    const userInfo = this.props.appReducer.userInfo;
    param.userAvatar = userInfo.avatar;
    param.userId = userInfo.userId;
    param.userName = userInfo.userName;
    try {
      this.state.isRequesting = true;
      await releaseComment(param);
      this.state.isRequesting = false;
      this.setState({
        isFocus: false,
        value: ''
      });
      // 清空输入框
      var element_div = document.getElementById("input-comment");
      element_div.innerHTML = "";
      this.props.showSuccess("发送成功");
    } catch (e) {
      this.props.showError("发送失败");
      console.log(e);
    }
  }
  focusInput() {
    this.setState({ isFocus: true });
    this.props.setHeight(true);
  }
  focusInputOut() {
    this.setState({ isFocus: false });
    this.props.setHeight(false);
  }
  render() {
    const { isFocus, value } = this.state;
    const { isCollection } = this.props;
    return (
      <div className="input-comment-wrapper">
        <div className="qtui-mask"
          style={{ display: isFocus ? null : 'none' }}
          onClick={() => this.setState({ isFocus: false })}></div>
        {
          <div className="discuss-area">
            {
              value ? '' : (isFocus ?
                <span className="input-placeholder">优质留言将会被优先展示</span>
                :
                <span className="input-placeholder">
                  <i className="iconfont icon-write"></i>
                  写留言...
                </span>)
            }
            <div id="input-comment" className="discuss-input" contentEditable="true"
              onFocus={() => this.focusInput()}
              // onBlur={() => { this.focusInputOut() }}
              onInput={(e) => this.changeHanle(e)}
            >
            </div>
            {
              isFocus ?
                <div className="release-comment" onClick={() => this.releaseCommentFn()}>发送</div>
                :
                <div className="icon-wrapper">
                  <i className="scroll-msg iconfont icon-message" onClick={() => this.scrollMsg()} />
                  {
                    isCollection ?
                      <i className="msg-collect iconfont icon-collect del-collect" onClick={(e) => this.collectArticleFn(e, true)} /> :
                      <i className="msg-collect iconfont icon-colloct-empty add-collect" onClick={(e) => this.collectArticleFn(e, false)} />
                  }
                </div>}
          </div>
        }
      </div>
    );
  }
}

export default InputComment;