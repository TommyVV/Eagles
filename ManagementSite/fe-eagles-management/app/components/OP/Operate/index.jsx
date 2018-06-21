import React, { Component } from 'react';
import { Button, Flex } from 'antd-mobile';
import { reviewShare } from "../../../services/reviewService";
import './style.less'

class Operate extends Component {

  constructor(props) {
    super(props);
  }

  render() {
    return (
      <Flex className='op__area'>
        <Flex.Item>
          <Button onClick={this.props.jump} className='op__btn--refuse' >拒绝</Button>
        </Flex.Item>
        <Flex.Item>
          <Button className='op__btn--pass' onClick={this.props.handlePass}>通过</Button>
        </Flex.Item>
      </Flex>
    );
  }
}

export default Operate;