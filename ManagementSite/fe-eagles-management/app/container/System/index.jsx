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
import { getList, del } from "../../services/systemService";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class SystemList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      systemList: [] // 列表数组
    };
    this.columns = [
      {
        title: "标题",
        dataIndex: "NewsName"
      },
      {
        title: "重复类型",
        dataIndex: "RepeatTime",
        render: obj => <span>{obj == "0" ? "每年" : "仅一次"}</span>
      },
      {
        title: "提醒时间",
        render: obj => (
          <span>
            {obj.RepeatTime == "0"
              ? new Date(obj.NoticeTime).format("MM-dd")
              : new Date(obj.NoticeTime).format("yyyy-MM-dd")}
          </span>
        )
      },
      {
        title: "内容",
        dataIndex: "HtmlDesc"
      },
      {
        title: "状态",
        dataIndex: "Status",
        render: obj => <span>{obj == "0" ? "正常" : "禁用"}</span>
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(`/system/detail/${obj.NewsId}`)
                }
              >
                编辑
              </a>
              <a
                onClick={() => this.handleDelete(obj.NewsId)}
                style={{ paddingLeft: "24px" }}
              >
                删除
              </a>
            </div>
          );
        }
      }
    ];

    this.getListConfig = {
      PageNumber: 1,
      PageSize: 10,
      Status: "",
      SystemMessageName: ""
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
      List.forEach(v => {
        v.key = v.NewsId;
      });
      this.setState({ systemList: List, current: PageNumber });
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
  handleDelete = async NewsId => {
    confirm({
      title: "是否确认删除?",
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code } = await del({
            NewsId
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
    const { selectedRowKeys, pageConfig, systemList } = this.state;
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
          dataSource={systemList}
          columns={this.columns}
          pagination={pageConfig}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />
        <Row type="flex" gutter={24}>
          <Col>
            <Button
              className="btn btn--primary"
              onClick={() => hashHistory.replace(`/system/detail`)}
            >
              新增
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default SystemList;
