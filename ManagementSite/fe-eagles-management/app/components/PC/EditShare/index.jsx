import React, { Component } from 'react';
import { Button, message } from 'antd';
import { hashHistory } from 'react-router';
import './style.less';

class EditShare extends Component {
  constructor(props) {
    super(props);
  }
  // 编辑分享内容
  handleEdit = () => {
    let { selectedRowKeys } = this.props
    if (selectedRowKeys.length > 1) {
      return message.error('不能同时编辑多个分享')
    }
    if (selectedRowKeys.length === 0) {
      return message.error('请选择需要编辑的分享')
    }
    hashHistory.replace(`/sharepublished/${selectedRowKeys[0]}`)
  }
  render() {
    return (
      <Button onClick={this.handleEdit} type='primary' className='btn btn--primary'>编辑</Button>
    );
  }
}

export default EditShare;