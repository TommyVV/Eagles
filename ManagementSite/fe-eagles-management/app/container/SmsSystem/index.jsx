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
import { getList, del } from "../../services/systemSmsService";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class SmsSystemList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      systemList: [] // 列表数组
    };
    this.columns = [
      {
        title: "短信提供商",
        dataIndex: "VendorName"
      },
      {
        title: "AppId",
        dataIndex: "AppId"
      },
      {
        title: "AppKey",
        dataIndex: "AppKey"
      },
      {
        title: "短信总数",
        dataIndex: "MaxCount"
      },
      {
        title: "已发数量",
        dataIndex: "SendCount"
      },
      {
        title: "状态",
        dataIndex: "Status",
        render: status => <span>{status == "0" ? "正常" : "禁用"}</span>
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(`/smssystem/detail/${obj.VendorId}`)
                }
              >
                编辑
              </a>
              <a
                onClick={() => this.handleDelete(obj.VendorId)}
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
  handleDelete = async VendorId => {
    confirm({
      title: "是否确认删除?",
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code } = await del({
            VendorId
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
            <Button className="btn btn--primary">
              <a onClick={() => hashHistory.replace(`/smssystem/detail`)}>
                新增
              </a>
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default SmsSystemList;
