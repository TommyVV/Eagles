import React, { Component } from 'react';
import './style.less';

class FansItem extends Component {
  constructor(props, context) {
    super(props, context);
  }

  jump() {
    this.props.jump()
  }

  render() {
    const { user, isConcern } = this.props;
    return (
      <div className="fans-item" onClick={this.jump.bind(this)}>
        <div className="item-info">
          <div className="fans-img-div">
            <img src={user.avatar} />
          </div>
          <div className="fans-info">
            <div className="name">
              {user.userName}
            </div>
            <div className="detail">
              {user.comment || "这个人还没有添加描述"}
            </div>
          </div>
        </div>
        {this.props.children}
      </div>
    );
  }
}

export default FansItem;
