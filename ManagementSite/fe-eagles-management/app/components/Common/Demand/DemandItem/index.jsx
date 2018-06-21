import React, { Component } from 'react';
import { Form, Checkbox } from 'react-qtui';
import { Tag } from 'antd-mobile';
import Dotdotdot from 'react-dotdotdot';
import './style.less';
import Utils from '../../../../utils/util';

/**
 * 接受2个属性
 *    data : {} 每一行的数据
 *    jump : url 跳转页面函数
 */
class DemandItem extends Component {
  constructor(props, context) {
    super(props, context);
  }

  jump() {
    this.props.jump();
  }

  render() {
    const { demand, isIndex, keyword, op } = this.props; // isIndex 是否为首页渲染
    return (
      <div className="demand__item" onClick={this.jump.bind(this)}>
        {
          op
            ?
            <div>
              <div className="demand__item--name">
                {op ? demand.checkStatus === 0 ? <span className='await'></span> : null : null}
                {demand.title || demand.name}
              </div>
              <Dotdotdot clamp={2}>
                <div className="demand__item--content">
                  {demand.introduction || demand.description}
                </div>
              </Dotdotdot>
            </div>
            :
            <div>
              <div className="demand__item--name"
                dangerouslySetInnerHTML={{ __html: Utils.lightKeyword(isIndex ? demand.title : demand.name, keyword) }}>
              </div>
            <Dotdotdot clamp={2}>
              <div className="demand__item--content"
                dangerouslySetInnerHTML={{ __html: Utils.lightKeyword(isIndex ? demand.introduction : demand.description, keyword) }}>
              </div>
            </Dotdotdot>
            </div>
        }
        {this.props.children}
      </div>
    );
  }
}

DemandItem.defaultProps = {
  demand: {}
};

export default DemandItem;
