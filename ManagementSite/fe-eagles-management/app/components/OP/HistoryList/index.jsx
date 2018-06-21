import React, { Component } from 'react';
import { Flex } from 'antd-mobile';
import { Article } from 'react-qtui';
import util from "../../../utils/util";
import './style.less';

class HistoryList extends Component {

  static defaultProps = {
    handleHistoryList: []
  }

  constructor(props) {
    super(props);
  }

  checkStatus = (approvalStatus) => {
    let status = ''
    switch (approvalStatus) {
      case 0:
        status = undefined
        break;
      case 1:
        status = 'show'
        break;
      default:
        status = 'hidden'
        break;
    }
    return status;
  }

  render() {
    const { handleHistoryList } = this.props;
    return (
      <Article className='history_box'>
        {handleHistoryList.map(history => {
          if (history.status === 0) { // 未通过
            return (
              <Flex key={history.updateTime} className='history_item' className='history_box'>
                <Flex.Item className='history_icon reject'>
                  <i className='iconfont icon-refuse' />未通过
                </Flex.Item>
                <Flex.Item>由{history.handlePersonName}于{util.timeStampConvent(history.updateTime, 'yy-MM-dd hh:mm')}拒绝，拒绝原因：
                  <span dangerouslySetInnerHTML={{ __html: history.rejectReason && history.rejectReason.replace(/\n|\r\n/g,'<br />')}}></span>
                </Flex.Item>
              </Flex>
            )
          } else { // 通过
            return (
              <Flex key={history.updateTime} className='history_box'>
                <Flex.Item className='history_icon pass'>
                  <span>
                    <i className='iconfont icon-pass' />已通过
                  </span>
                </Flex.Item>
                <Flex.Item>由{history.handlePersonName}于{util.timeStampConvent(history.updateTime, 'yy-MM-dd hh:mm')}审核通过</Flex.Item>
              </Flex>
            )
          }
        })}
      </Article>
    );
  }
}

export default HistoryList;