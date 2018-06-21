import React, { Component } from 'react';
import { Button, message } from 'antd';
import link from "../../../config/link";

const share = link.share;

class DeleteShare extends Component {
  constructor(props) {
    super(props);
  }
  // 删除分享内容
  deleteShare = () => {
    let { selectedRowKeys } = this.props
    if (selectedRowKeys.length === 0) {
      return message.error('请选择需要删除的分享')
    }
    // 符合条件的时候获取分享内容详情
    this.props.delete(selectedRowKeys.join(';'))
  }
  render() {
    return (
      <Button onClick={this.deleteShare} className='btn'>删除</Button>
    );
  }
}

export default DeleteShare;