import React, { Component } from 'react';
import { Button, message } from 'antd';

class DeleteDemand extends Component {
  constructor(props) {
    super(props);
  }
  // 删除需求
  handleDelete = () => {
    let { selectedRowKeys } = this.props
    if (selectedRowKeys.length === 0) {
      return message.error('请选择需要删除的项目')
    }
    let params = selectedRowKeys.join(',')
    this.props.delete(selectedRowKeys)
  }
  render() {
    return (
      <Button onClick={this.handleDelete} className='btn'>删除</Button>
    );
  }
}

export default DeleteDemand;