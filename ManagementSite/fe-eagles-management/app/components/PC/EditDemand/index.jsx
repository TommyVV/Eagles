import React, { Component } from 'react';
import { Button, message } from 'antd';

class EditDemand extends Component {
  constructor(props) {
    super(props);
  }
  // 编辑项目需求
  handleEdit = () => {
    // let { selectedRowKeys } = this.props
    // if (selectedRowKeys.length > 1) {
    //   return message.error('不能同时编辑多个项目')
    // }
    // if (selectedRowKeys.length === 0) {
    //   return message.error('请选择需要编辑的项目')
    // }
    // this.props.edit(selectedRowKeys[0])
    this.props.edit()
  }
  render() {
   
    return (
      <Button onClick={this.handleEdit} type={this.props.false ? '' : 'primary'} className={this.props.false ? 'btn' : 'btn btn--primary'} >编辑</Button>
    );
  }
}

export default EditDemand;