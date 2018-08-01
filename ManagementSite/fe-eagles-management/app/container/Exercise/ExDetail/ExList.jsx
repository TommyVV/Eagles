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
      List: [], // 列表数组
      pageConfig: {} // 当前页配置
    };
    this.columns = [
      {
        title: "习题编号",
        dataIndex: "QuestionId"
      },
      {
        title: "习题名称",
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
      },
      {
        title: "操作",
        render: obj => (
          <span>
            <a
              onClick={() =>
                hashHistory.replace(`/exercise/detail/${obj.QuestionId}`)
              }
            >
              编辑
            </a>
            <a
              onClick={() => this.handleDelete(obj.QuestionId)}
              style={{ paddingLeft: "24px" }}
            >
              删除
            </a>
          </span>
        )
      }
    ];

    this.getListConfig = {
      PageNumber: 1,
      PageSize: 10
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
        v.key = i;
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
  // 删除项目
  handleDelete = async QuestionId => {
    confirm({
      title: "是否确认删除?",
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code } = await del({
            QuestionId
          });
          if (Code === "00") {
            message.success("删除成功");
            await this.getCurrentList({
              ...this.getListConfig,
              PageNumber: this.state.current
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
    const { List, pageConfig } = this.state;
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
          columns={this.columns}
          dataSource={List}
          bordered
          style={{ width: "90%" }}
          pagination={pageConfig}
        />
        <Row
          type="flex"
          gutter={24}
          // className={projectList.length === 0 ? "init" : ""}
        >
          <Col>
            <Button
              className="btn btn--primary"
              onClick={() => hashHistory.replace(`/exercise/detail`)}
            >
              新增
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default ExList;
