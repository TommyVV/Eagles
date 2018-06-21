import React, { Component } from 'react';
import Utils from '../../../utils/util';
import Dotdotdot from 'react-dotdotdot';
import './style.less';

const authICon = require('../../../../static/image/svg/auth_company.svg');

class AgencyItem extends Component {
  constructor(props, context) {
    super(props, context);
  }

  jump() {
    this.props.jump();
  }

  render() {
    const { agency } = this.props;
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
      <div className="opagency__item" onClick={this.jump.bind(this)}>
        <div className="item--img">
          <img src={agency.avatar} className='avatar' />
          {
            agency.checkstatus == '0' ? null : <img src={authICon} alt="" className='auth_icon' />
          }
        </div>
        <div className="item--info">
          <div className="info--name">
            {agency.checkstatus === 0 ? <span className='await'></span> : null}
            {agency.name || agency.abbreviation}
          </div>
          <div className="info--label">
            {labelStr}
          </div>
        </div>
      </div>
    );
  }
}

export default AgencyItem;
