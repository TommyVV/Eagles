import React, { Component } from 'react';
import { hashHistory } from 'react-router'; //引入路由函数
import './style.less';

/**
 * 接受2个属性
 *    data : {} 每一行的数据
 */
class MyShareHead extends Component {
  constructor(props, context) {
    super(props, context);
  }

  render() {
    const { income, type, typeId } = this.props;
    return (
      <div className="my-share-info">
        <div className="my-share-income">
          累计收入(元)
          </div>
        <div className="income-money">{income}</div>
        <div className="my-share-analyze">
          <div onClick={(e) => hashHistory.push(`/sharetotallist/${type}/${typeId}`)}>查看分析</div>
        </div>
      </div>
    );
  }
}

export default MyShareHead;
