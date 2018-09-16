import React, { Component } from "react";
import {
  Table,
  message,
  Modal,
  Form,
  Select,
} from "antd";
import { hashHistory } from "react-router";
import {
  getListBranch,
  getListOrg
} from "../../services/publicActivityService";
import { auditStatus } from "../../constants/config/appconfig";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class PublicActivityList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      List: [] // 列表数组
    };
    this.columns = [
      {
        title: "标题",
        dataIndex: "ActivityName"
      },
      {
        title: "审核人",
        dataIndex: "AduitUserName"
      },
      {
        title: "负责人",
        dataIndex: "ResponsibleUserName"
      },
      {
        title: "申请时间",
        dataIndex: "CreateTime",
        render: text => <span>{new Date(text).format("yyyy-MM-dd")}</span>
      },
      {
        title: "参与人数",
        dataIndex: "UserCount"
      },
      {
        title: "审核状态",
        dataIndex: "Status",
        render: text => {
          return auditStatus.map(o => {
            return o.Status == text ? <span key="1">{o.text}</span> : null
          })
        }
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(
                    `/activitypublic/detail/${obj.ActivityId}/0/${
                    this.props.params.type
                    }`
                  )
                }
              >
                详情
              </a>
              <a
                onClick={() =>
                  hashHistory.replace(
                    `/activitypublic/detail/${obj.ActivityId}/1/${
                    this.props.params.type
                    }`
                  )
                }
                style={{ paddingLeft: "24px" }}
              >
                审核
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
    const { type } = this.props.params;
    this.getCurrentList(this.getListConfig, type);
  }

  // 加载当前页
  getCurrentList = async params => {
    const { PageNumber } = params;
    try {
      const { type } = this.props.params;
      let res = {};

      res = await getListBranch(params);

      console.log("res - ", res);
      if (res.Activitys) {
        res.Activitys.forEach(v => {
          v.key = v.ActivityId;
        });
        this.setState({ List: res.Activitys, current: PageNumber, Message: res.Message });
        this.updatePageConfig(res.TotalCount);
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
    const { pageConfig, List, Message } = this.state;
    return (
      <Nav>
        <Table
          dataSource={List}
          columns={this.columns}
          pagination={pageConfig}
          locale={{ emptyText: Message }}
          bordered
        />
      </Nav>
    );
  }
}

export default PublicActivityList;
