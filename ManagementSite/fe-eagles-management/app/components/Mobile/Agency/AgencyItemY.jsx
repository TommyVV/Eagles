import React, { Component } from 'react';
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
    const { agency} = this.props;
    const labelStr = agency.label.join(" ");
    return (
      <div className="agency-item-y" onClick={this.jump.bind(this)}>
        <div className="agency-img-div">
          <img src={agency.avatar} />
        </div>
        <div className="agency-info">
          <div className="name">
            {agency.abbreviation}
          </div>
          <div className="detail">
            <div className="laber-str">{labelStr ?  labelStr : '这个机构还没有添加描述'}</div>
            <div className="delete-btn" onClick={this.deleteHistory.bind(this)}>删除</div>
          </div>
        </div>
      </div>
    );
  }
}

export default AgencyItemY;
