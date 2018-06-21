import React, { Component } from 'react';
import Utils from '../../../../utils/util';
import Dotdotdot from 'react-dotdotdot';
import './style.less';

class AgencyItemY extends Component {
  constructor(props, context) {
    super(props, context);
  }

  jump() {
    this.props.jump();
  }

  //删除记录
  deleteHistory(e) {
    e.stopPropagation()
    this.props.handleDelete();
  }

  render() {
    const { agency, hasDelete, isSearch, keyword } = this.props; // isSearch 是否搜索页面进来的
    let labelStr = '';
    let list = [];
    if (agency.introduction && agency.introduction.length) {
      list = agency.introduction;
    } else if (agency.label || agency.label.length) {
      list = agency.label;
    }
    list.map((label, index) => {
      labelStr += label + ' ';
    });
    labelStr = labelStr.trim();
    return (
      <div className="org__item" onClick={this.jump.bind(this)}>
        <div className="org__item--img">
          <img src={agency.avatar} />
        </div>
        <div className="org__item--info">
          <div className="org__info--name" dangerouslySetInnerHTML={{ __html: Utils.lightKeyword(agency.name || agency.abbreviation, keyword) }}></div>
          {labelStr ? 
          <div className="label__str" dangerouslySetInnerHTML={{ __html: Utils.lightKeyword(labelStr, keyword) }}></div>
            : <div className="label__str">这个机构还没有添加描述</div>
        }
        </div>
        {this.props.children}
      </div>
    );
  }
}

export default AgencyItemY;
