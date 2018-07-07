import React, { Component } from 'react';
import { Tag } from 'antd-mobile';
import QtImage from '../../RenderImage/QtImage';
import './style.less';

const authICon = require('../../../../../static/image/svg/auth_company.svg');

class Header extends Component {
  constructor(props) {
    super(props);
  }

  approvalMsg(status) {
    let msg = '';
    if (status == 0) {
      msg = <span className='agency__company--unAuth'>未提交认证</span>
    } else if (status == 1) {
      msg = <span className='agency__company--auth'>
        <img src={authICon} alt="" /><span>认证企业</span>
        {this.props.children}
      </span>
    } else if (status == 2) {
      msg = <span className='agency__company--unAuth'>等待认证</span>
    } else if (status == 3) {
      msg = <span className='agency__company--unAuth'>认证失败</span>
    }
    return msg;
  }

  render() {
    const { agency } = this.props;
    let tags = agency && agency.label;
    let tagArray = tags ? tags.split(";") : [];
    const srcs = [];
    srcs.push(agency.avatar);
    return (
      <div className='agency__container--bg'>
        <div className='agency__company'>
          <div className='agency__company--icon'>
            <QtImage src={agency.avatar} srcs={srcs} />
            {this.approvalMsg(agency.approvalStatus)}
          </div>
          <div className='agency__company--name'>
            <p>{agency.companyName}</p>
          </div>
          <div className='agency__company--tag'>
            {tagArray.map((tag, index) => {
              return <Tag disabled key={index}>{tag}</Tag>;
            })}
          </div>
        </div>
      </div>
    );
  }
}

export default Header;