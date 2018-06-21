import React, { Component } from 'react';
import { hashHistory } from 'react-router'; //引入路由函数
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import QtImage from '../../Common/RenderImage/QtImage';
import { saveTab } from '../../../actions/mobile/globalAction';
import './style.less';

@connect(
  state => {
    return {
    }
  },
  { saveTab }
)
class MyInfo extends Component {
  constructor(props, context) {
    super(props, context);
  }

  jumpToConcern(userId) {
    this.props.saveTab(0);
    hashHistory.push(`/myconcern/${userId}`);
  }

  jumpToFans(userId) {
    this.props.saveTab(0);
    hashHistory.push(`/myfans/${userId}`)
  }

  //处理关注已关注
  handleConcern() {
    this.props.handleConcern();
  }


  render() {
    let { isMy, personInfo } = this.props;
    console.log(isMy);
    const srcs = [];
    srcs.push(personInfo.avatar);
    return (
      <div className="my-info">
        <div className="my-img">
          <QtImage src={personInfo.avatar} srcs={srcs} />
        </div>
        <div className="my-info-detail">
          <div className="name">
            {personInfo.userName}
          </div>
          <div className="my-follow-list">
            <span className="concern" onClick={this.jumpToConcern.bind(this, personInfo.userId)}>
              <span>{personInfo.focusCount}</span>关注
            </span>
            <span className="fans" onClick={this.jumpToFans.bind(this, personInfo.userId)}>
              <span>{personInfo.followersCount}</span>粉丝
            </span>
          </div>
        </div>
        {!isMy ? <div className="is-follow">
          <span className={personInfo.focus ? 'concerned' : 'concern'} onClick={this.handleConcern.bind(this)}>{personInfo.focus ? '已关注' : '关注'}</span>
        </div> : ''
        }
      </div>
    );
  }
}

export default MyInfo;
