import React, { Component } from 'react';
import { Tag } from 'antd-mobile';
import './style.less';

const authICon = require('../../../../../static/image/svg/auth_company.svg');

class Header extends Component {
  constructor(props) {
    super(props);
  }

  render() {
    const { agency } = this.props;
    let tags = agency && agency.label;
    let tagArray = tags ? tags.split(";") : [];
    return (
      <div className='agency__container--bg'>
        <div className='agency__company'>
          <div className='agency__company--icon'>
            <img src={agency.avatar} alt="图标" />
            <span className='agency__company--auth'>
              <img src={authICon} alt="" /><span>认证企业</span>
              {this.props.children}
            </span>
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