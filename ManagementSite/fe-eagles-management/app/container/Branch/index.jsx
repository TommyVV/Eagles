import React, { Component } from "react";
import {
  Table,
  Row,
  Col,
  Button,
  message,
  Modal,
} from "antd";
import { hashHistory } from "react-router";
import { getList, del } from "../../services/branchService";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class BranchList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      branchIdList: [] // 列表数组
    };
    this.columns = [
      {
        title: "支部编号",
        dataIndex: "BranchId"
      },
      {
        title: "支部名称",
        dataIndex: "BranchName"
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(`/branch/detail/${obj.BranchId}`)
                }
              >
                编辑
              </a>
              <a
                onClick={() => this.handleDelete(obj.BranchId)}
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
      List.forEach(v => {
        v.key = v.BranchId;
      });
      this.setState({ branchList: List, current: PageNumber });
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
  handleDelete = async BranchId => {
    confirm({
      title: "是否确认删除?",
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code } = await del({
            BranchId
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
    const { selectedRowKeys, pageConfig, branchList } = this.state;
    return (
      <Nav>
        <Table
          dataSource={branchList}
          columns={this.columns}
          pagination={pageConfig}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />
        <Row
          type="flex"
          gutter={24}
          // className={projectList.length === 0 ? "init" : ""}
        >
          <Col>
            <Button className="btn btn--primary">
              <a onClick={() => hashHistory.replace(`/branch/detail`)}>新增</a>
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default BranchList;
