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
import { getList, del } from "../../../services/exerciseService";
import Nav from "../../Nav";
import "./style.less";

const confirm = Modal.confirm;

class ExList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      operatorList: [] // 列表数组
    };
    this.columns = [
      {
        title: "题目",
        dataIndex: "exTitle"
      },
      {
        title: "类型",
        dataIndex: "exType"
      },
      {
        title: "操作",
        dataIndex: "operate",
        render: () => (
          <span>
            <a href="javascript:;">编辑</a>
            <a href="javascript:;" style={{ paddingLeft: "24px" }}>
              删除
            </a>
          </span>
        )
      }
    ];

    this.getListConfig = {
      PageNumber: 1,
      PageSize: 10,
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
    const { PageNumber } = this.getListConfig;
    try {
      let { List } = await getList(params);
      console.log("List - ", List);
      List.forEach(v => {
        v.key = v.OperId;
      });
      this.setState({ operatorList: List, current: PageNumber });
      // this.updatePageConfig(totalSize);
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
          PageNumber: page,
        });
      }
    };
    this.setState({ pageConfig });
  }
  // 删除项目
  handleDelete = async OperId => {
    confirm({
      title: "是否确认删除?",
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code } = await del({
            OperId
          });
          if (Code === "00") {
            message.success("删除成功");
            await this.getCurrentList({
              ...this.getListConfig,
              PageNumber: this.state.current
              // keyword: this.state.keyword
            });
            this.setState({ selectedRowKeys: [] });
          } else {
            message.error("删除失败");
          }
        } catch (e) {
          throw new Error(e);
        }
      }
    });
  };
  render() {
    const { selectedRowKeys, pageConfig, operatorList } = this.state;
    const formItemLayout = {
      labelCol: {
        xl: { span: 3 }
      },
      wrapperCol: {
        xl: { span: 10 }
      }
    };
    return (
      <Nav>
        <Table
            columns={columns}
            dataSource={data}
            bordered
            style={{ width: "90%" }}
          />
        <Row
          type="flex"
          gutter={24}
          // className={projectList.length === 0 ? "init" : ""}
        >
          <Col>
            <Button className="btn btn--primary">
              <a onClick={() => hashHistory.replace(`/exercise/detail`)}>新增</a>
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default ExList;
