import React, { Component } from "react";
import {
  Table,
  Row,
  Col,
  Button,
  message,
  Modal,
  Form,
  Input,
  Select,
  Cascader
} from "antd";
const FormItem = Form.Item;
const Option = Select.Option;
import { hashHistory } from "react-router";
import { getList } from "../../../services/exerciseService";

const confirm = Modal.confirm;

class ExList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      List: [], // 列表数组
      selectedRowKeys: [], // id数组
      current: 1, // 当前页
      pageConfig: {} // 当前页配置
    };
    this.columns = [
      {
        title: "题目",
        dataIndex: "Question"
      },
      {
        title: "是否多选",
        dataIndex: "Multiple",
        render: text => <span>{text == "0" ? "单选" : "多选"}</span>
      },
      {
        title: "是否投票",
        dataIndex: "IsVote",
        render: text => <span>{text ? "是" : "否"}</span>
      }
    ];

    this.getListConfig = {
      PageNumber: 1,
      PageSize: 5
    };
  }
  componentWillMount() {
    this.getCurrentList(this.getListConfig);
  }

  // 选择分享时触发的改变
  onSelectChange = selectedRowKeys => {
    this.setState({ selectedRowKeys });
  };

  // 加载当前页
  getCurrentList = async params => {
    const { PageNumber } = params;
    try {
      let { List, TotalCount } = await getList(params);
      console.log("List - ", List);
      List.forEach((v, i) => {
        v.key = JSON.stringify(v);
      });
      this.setState({ List, current: PageNumber });
      this.updatePageConfig(TotalCount);
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  // 更新分页配置
  updatePageConfig(totalSize) {
    let pageConfig = {
      total: totalSize,
      pageSize: this.getListConfig.PageSize,
      current: this.state.current,
      onChange: async (page, pagesize) => {
        this.getCurrentList({
          ...this.getListConfig,
          PageNumber: page
        });
      }
    };
    this.setState({ pageConfig });
  }
  render() {
    const { selectedRowKeys, pageConfig, List } = this.state;
    const formItemLayout = {
      labelCol: {
        xl: { span: 3 }
      },
      wrapperCol: {
        xl: { span: 10 }
      }
    };
    const rowSelection = {
      selectedRowKeys,
      onChange: this.onSelectChange
    };
    return (
      <Table
        columns={this.columns}
        dataSource={List}
        bordered
        pagination={pageConfig}
        rowSelection={rowSelection}
        style={{ width: "90%" }}
        width="700px"
      />
    );
  }
}

export default ExList;
