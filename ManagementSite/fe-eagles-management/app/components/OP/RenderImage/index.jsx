import React, { Component } from 'react';
import { Flex } from 'antd-mobile';
import './style.less';

class RenderImage extends Component {
  render() {
    return (
      <Flex
        wrap
        direction='column'
        className='images--list'
      >
        <Flex.Item>
          <img src="http://img01.sogoucdn.com/app/a/100520024/3c9da6be4f0247e01f07d403b7069e10" alt="图片介绍" />
        </Flex.Item>
        <Flex.Item>
          <img src="http://img03.sogoucdn.com/app/a/100520024/0e656a1ad0afea17b11bee05a15a1abb" alt="图片介绍" />
        </Flex.Item>
        <Flex.Item>
          <img src="http://img02.sogoucdn.com/app/a/100520024/949774c65ac8e125c5c8d3cbe2439315" alt="图片介绍" />
        </Flex.Item>
      </Flex>
    );
  }
}

export default RenderImage;