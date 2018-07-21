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
import { getList } from "../../services/orgSmsService";
import Nav from "../Nav";
import "./style.less";

class SmsOrgList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      systemList: [] // 列表数组
    };
    this.columns = [
      {
        title: "机构名称",
        dataIndex: "OrgName"
      },
      {
        title: "短信提供商",
        dataIndex: "VendorName"
      },
      {
        title: "可发送最大数量",
        dataIndex: "MaxCount"
      },
      {
        title: "已发送数量",
        dataIndex: "SendCount"
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(
                    `/smsorg/detail/${obj.OrgId}/${obj.VendorId}`
                  )
                }
              >
                编辑
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
      if (List && List.length) {
        List.forEach((v, i) => {
          v.key = i;
        });
        this.setState({ systemList: List, current: PageNumber });
        this.updatePageConfig(TotalCount);
      }
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
              onClick={() => hashHistory.replace(`/smsorg/detail`)}
            >
              新增
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default SmsOrgList;
