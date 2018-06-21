import React, { Component } from 'react';
import { Form, Checkbox } from 'react-qtui';
import './style.less';

/**
 * 接受2个属性
 *    data : {} 每一行的数据
 *    jump : url 跳转页面函数
 */
class MyShareItem extends Component {
  constructor(props, context) {
    super(props, context);
  }
  jump = () => {
    this.props.jump()
  }
  render() {
    const { data } = this.props
    return (
      <div className="myshare-item" onClick={this.jump}>
        <div className="myshare-item__name">
          {/* 中冶赛迪产业园区一体化解决方案 */}
          {data.title}
        </div>
        <div className="myshare-item__info">
          <span className="myshare-item__info__price">
            售价：
            <span className="myshare-item__info__total--price">￥{data.price}</span>
          </span>
          <span className="myshare-item__info__total">
            累计：
            <span className="myshare-item__info__total--price">￥{data.income}</span>
          </span>

        </div>
      </div>
    );
  }
}

export default MyShareItem;
