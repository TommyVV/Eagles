import React, { Component } from 'react';
import {hashHistory} from 'react-router';
import { Flex } from 'antd-mobile';
import Emojify from 'react-qt-emoji';
import { connect } from 'react-redux';
import {saveTab} from '../../../../actions/mobile/globalAction';
import '../../../../../node_modules/react-qt-emoji/dist/styles/emoji.css';
import qt from '../../../../../static/lib/qingtui_jssdk-2.1';
import "./style.less";

@connect (
  state => {
    return {}
  },
  {saveTab}
)
class CommentItesm extends Component {
  constructor(props) {
    super(props);
  }

  //跳到个人详情
  // jumpDetail(userId) {
  //    this.props.saveTab(0);
  //   if (!this.props.op) {
  //      hashHistory.push(`/hisdetail/${userId}`);
  //   }
  // }

  //打开会话
  openChat(openId){
     console.log(openId);
     qt.openUserDetail({
      id: openId
     });
  }


  render() {
    const { obj, more } = this.props;
    return (
      <Flex direction='column' className={more ? 'comment__item comment__more__box' : 'comment__item'}>
        <Flex.Item className='comment__item--user'>
          <Flex>
            <Flex.Item className='comment__user--avatar'>
              <img src={obj.userAvatar} />
            </Flex.Item>
            <Flex.Item className='comment__item--details'>
              <span className='comment__item--name' onClick={this.openChat.bind(this, obj.openId)}>{obj.userName}</span>
              <Emojify>
                <div className='comment__item--content'>{decodeURI(obj.content)}</div>
              </Emojify>
              <div className='comment__item--time'>{new Date(obj.updateTime).format('MM-dd hh:mm')}</div>
              {this.props.children}
            </Flex.Item>
          </Flex>
        </Flex.Item>
      </Flex>
    );
  }
}

export default CommentItesm;