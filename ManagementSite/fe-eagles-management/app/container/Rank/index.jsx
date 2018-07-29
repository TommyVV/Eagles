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
import { getRankList, getRankInfoById } from "../../services/scoreService";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class RankList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      rankList: [] // 列表数组
    };
    this.columns = [
      {
        title: "用户姓名",
        dataIndex: "UserName"
      },
      {
        title: "用户积分总数",
        dataIndex: "Score"
      },
      // {
      //   title: "用户使用积分",
      //   dataIndex: "UserIdentity"
      // },
      {
        title: "党员状态",
        dataIndex: "UserIdentity"
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              <a onClick={() => hashHistory.replace(`/rank/detail/${obj.UserId}/${obj.UserName}/${obj.Score}`)}>
                查看详细信息
              </a>
            </div>
          );
        }
      }
    ];

    this.getListConfig = {
      PageNumber: 1,
      PageSize: 10,
      UserName: ""
    };
  }
  componentWillMount() {
    this.getCurrentList(this.getListConfig);
  }

  // 加载当前页
  getCurrentList = async params => {
    const { PageNumber } = params;
    try {
      let { List, TotalCount } = await getRankList(params);
      console.log("List - ", List);
      List.forEach(v => {
        v.key = v.UserId;
      });
      this.setState({ rankList: List, current: PageNumber });
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
    const { pageConfig, rankList } = this.state;
    return (
      <Nav>
        <Table
          dataSource={rankList}
          columns={this.columns}
          pagination={pageConfig}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />
      </Nav>
    );
  }
}

export default RankList;
