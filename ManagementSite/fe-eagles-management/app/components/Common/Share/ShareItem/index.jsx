import React, { Component } from 'react';
import Dotdotdot from 'react-dotdotdot';
import Utils from '../../../../utils/util';
import './style.less';
/**
 * 接受2个属性
 *    data : {} 每一行的数据
 *    jump : url 跳转页面函数
 */
class ShareItem extends Component {
  constructor(props, context) {
    super(props, context);
  }

  jump() {
    this.props.jump()
  }

  render() {
    const { data, op, keyword } = this.props
    return (
      <div className="share-item" onClick={this.jump.bind(this)}>
        <Dotdotdot clamp={1}>
          {
            op ?
              <div className="share-item__name">
                {data.checkStatus === 0 ? <span className='await'></span> : null}
                {data.title}
              </div>
              :
              <div className="share-item__name"
                dangerouslySetInnerHTML={{ __html: Utils.lightKeyword(data.title, keyword) }}>
              </div>
          }
        </Dotdotdot>
        <Dotdotdot clamp={2}>
          <div className="share-item__content"
            dangerouslySetInnerHTML={{ __html: Utils.lightKeyword(data.introduction, keyword) }}>
          </div>
        </Dotdotdot>
        {this.props.children}
      </div>
    );
  }
}
export default ShareItem;
