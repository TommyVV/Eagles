import React, { Component } from 'react';
import { hashHistory } from 'react-router'; //引入路由函数
import Utils from '../../../utils/util';
import './style.less';


/**
 * 接受2个属性
 *    data : {} 每一行的数据
 */
class ShareIncomeItem extends Component {
  constructor(props, context) {
    super(props, context);
  }
  render() {
    const { data } = this.props;
    return (
      <div className="income-item">
        <div className="income-img-div">
          <img src={data.avatar} />
        </div>
        <div className="income-info">
          <div className="name">
            {data.userName}
          </div>
          <div className="time">
            {Utils.formatTime(data.createTime, 'yyyy-MM-dd hh:mm:ss')}
          </div>
        </div>
        <div className="income-div">
          ￥{data.dealPrice}
        </div>
      </div>
    );
  }
}

export default ShareIncomeItem;
