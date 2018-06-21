import React, { Component } from 'react';
import './style.less';
import Dotdotdot from 'react-dotdotdot';
import Util from '../../../../utils/util';

/**
 * 接受2个属性
 *    data : {} 每一行的数据
 *    jump : url 跳转页面函数
 */
class IoreItem extends Component {
  constructor(props, context) {
    super(props, context);
  }
  jump = (id) => {
    this.props.jump(id);
  }
  render() {
    const { share } = this.props;
    return (
      <div className="iore__item" onClick={() => this.jump(share.id)}>
        <div className="iore__item--author">
          <img src={share.avatar} alt="" className='iore__author--icon' />
          <span className='iore__author--agecny'>{share.identityName}</span>
          <span className='iore__author--dot'>·</span>
          <span className='iore__author--time'>{Util.getFormatTime(share.updateTime)}</span>
        </div>
        <div className="iore__item--content">
          <p className='iore__content--title'>{share.title}</p>
          <Dotdotdot clamp={2}>
            <div>
              {share.introduction}
            </div>
          </Dotdotdot>
        </div>
      </div>
    );
  }
}

export default IoreItem;
