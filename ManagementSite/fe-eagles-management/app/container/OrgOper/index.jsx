import React, { Component } from "react";
import {
  Table,
  Row,
  Col,
  Button,
  message,
  Modal,
  Form,
  Select,
} from "antd";
const FormItem = Form.Item;
const Option = Select.Option;
import { hashHistory } from "react-router";
import { getList, del } from "../../services/operatorService";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class OrgOperatorList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      operatorList: [] // 列表数组
    };
    this.columns = [
      {
        title: "组织编号",
        dataIndex: "OrgId"
      },
      {
        title: "组织名称",
        dataIndex: "OrgName"
      },
      {
        title: "操作员编号",
        dataIndex: "OperId"
      },
      {
        title: "操作员名称",
        dataIndex: "OperName"
      },
      // {
      //   title: "所属权限组",
      //   dataIndex: "permission"
      // },
      {
        title: "添加时间",
        dataIndex: "CreateTime",
        render: text => <span>{new Date(text).format("yyyy-MM-dd")}</span>
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(`/orgoperator/detail/${obj.OperId}`)
                }
              >
                编辑
              </a>
              <a
                onClick={() => this.handleDelete(obj.OperId)}
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
      Province: "",
      City: "",
      District: ""
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
      let request = {
        OrgBranch: true,
        ...PageNumber
      }
      let { List, TotalCount, Message } = await getList(request);
      console.log("List - ", List);
      List.forEach(v => {
        v.key = v.OperId;
      });
      this.setState({ operatorList: List, current: PageNumber, Message });
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
  handleDelete = async OperId => {
    confirm({
      title: "是否确认删除?",
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code, Message } = await del({
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
            message.error(Message);
          }
        } catch (e) {
          throw new Error(e);
        }
      }
    });
  };
  render() {
    const { Message, pageConfig, operatorList } = this.state;
    return (
      <Nav>
        <Table
          dataSource={operatorList}
          columns={this.columns}
          pagination={pageConfig}
          locale={{ emptyText: Message }}
          bordered
        />
        <Row
          type="flex"
          gutter={24}
        // className={projectList.length === 0 ? "init" : ""}
        >
          <Col>
            <Button
              className="btn btn--primary"
              type="primary"
              onClick={() => hashHistory.replace(`/orgoperator/detail`)}
            >
              新增
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default OrgOperatorList;
