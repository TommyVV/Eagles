import React, { Component } from 'react';
import { Col, Avatar, Icon, Row } from "antd";
import { connect } from "react-redux";
import './style.less';

@connect(
  state => state.userReducer,
  null
)
class Member extends Component {

  static default = {
    memberList: []
  }

  constructor(props) {
    super(props);
  }


  render() {
    let { memberList } = this.props;
    return (
      <Row >
        {
          memberList.map(member => (
            <Col key={member.user_id} span={2} className='member-box'>
              <Avatar src={member.avatar} />
              {this.props.userId !== member.user_id ? <Icon type="close-circle" className="close-member" onClick={() => this.props.removeMember(member)} /> : null}
              <p className='member-name'>{member.name}</p>
            </Col>
          ))
        }
        <Col span={2} className='member-box'>
          <Icon type="plus-circle-o" className='add-member' onClick={this.props.show} />
        </Col>
      </Row>
    );
  }
}

export default Member;