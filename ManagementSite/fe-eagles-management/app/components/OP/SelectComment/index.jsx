import React, { Component } from 'react';
import { Button, Flex, Icon } from 'antd-mobile';
import { reviewComment } from "../../../services/reviewService";
import { message } from "antd";
import './style.less';

class SelectComment extends Component {
  constructor(props) {
    super(props);
    this.state = {
      init: undefined,//初始化留言状态,内部初始化
      // show: false,//显示,
      status: undefined,//状态
    }
  }

  componentWillMount() {
    this.setState({
      status: this.props.status,
      init: !this.props.status
    })
  }

  // 显示留言
  handleShow = async () => {
    try {
      let { commentId } = this.props
      await reviewComment({ commentId, commentType: 1 })
      this.setState({
        init: false,
        // show: true,
        status: 'show'
      })
    } catch (e) {
      throw new Error(e)
    }
  }
  // 不显示留言
  handleHidden = async () => {
    try {
      let { commentId } = this.props
      await reviewComment({ commentId, commentType: 2 })
      this.setState({
        init: false,
        // show: false,
        status: 'hidden'
      })
    } catch (e) {
      throw new Error(e)
    }
  }
  // 留言操作反转
  handleReverse = async () => {
    try {
      let commentType = ''
      let { show, status } = this.state;
      if (status === 'show') {
        status = 'hidden';
        commentType = 2
      } else {
        commentType = 1
        status = 'show';
      }
      let { commentId } = this.props
      await reviewComment({ commentId, commentType })
      this.setState({
        status,
        // show: !show,
      })
    } catch (e) {
      throw new Error(e)
    }
  }
  render() {
    const { init, status } = this.state;
    let leftText;
    let rightText;
    // let leftText = show ? '已选为精选留言' : '已改为不显示';
    // let rightText = show ? '改为不显示' : '改为精选留言';
    switch (status) {
      case 'show':
        leftText = '已选为精选留言';
        rightText = '改为不显示';
        break;
      default:
        leftText = '已改为不显示';
        rightText = '改为精选留言';
        break;
    }
    return (
      <div className='op_show_hidden'>
        {
          init ?
            <div>
              <span className='base comment__btn--show' onClick={this.handleShow}>显示</span>
              <span className='base comment__btn--hidden' onClick={this.handleHidden}>不显示</span>
            </div>
            : null
        }
        {/* 已显示 */}
        {
          !init ?
            <Flex>
              <Flex.Item>
                <span className='comment__status'>
                  <Icon type='check-circle-o' /> {leftText}
                </span>
              </Flex.Item>
              <Flex.Item className='comment__status--modify'>
                <div>
                  <Button onClick={this.handleReverse}>{rightText}</Button>
                </div>
              </Flex.Item>
            </Flex>
            : null
        }
      </div>

    );
  }
}

export default SelectComment;