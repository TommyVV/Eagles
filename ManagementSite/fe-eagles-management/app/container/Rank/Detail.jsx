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
  DatePicker
} from "antd";
import { hashHistory } from "react-router";
import { getRankInfoById } from "../../services/scoreService";
import Nav from "../Nav";
import "./style.less";

class RankDetail extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      userName: "",
      score: "",
      scoreList: [],
      current: 1, // 当前页
      pageConfig: {} // 当前页配置
    };
    this.columns = [
      {
        title: "类型",
        dataIndex: "Comment"
      },
      {
        title: "积分使用情况",
        dataIndex: "Score"
      },
      {
        title: "时间",
        dataIndex: "CreateTime",
        render: text => <span>{new Date(text).format("yyyy-MM-dd")}</span>
      }
    ];
    this.getListConfig = {
      PageNumber: 1,
      PageSize: 10,
      UserId: ""
    };
  }
  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getCurrentList(this.getListConfig);
    }
  }

  // 加载当前页
  getCurrentList = async params => {
    const { PageNumber } = this.getListConfig;
    try {
      let { id, name, score } = this.props.params;
      let { Info } = await getRankInfoById({ ...params, UserId: id });
      console.log("List - ", Info);
      Info.forEach((v, i) => {
        v.key = i;
      });
      this.setState({
        userName: name,
        score,
        scoreList: Info,
        current: PageNumber
      });
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
          PageNumber: page
        });
      }
    };
    this.setState({ pageConfig });
  }
  render() {
    const { pageConfig, scoreList, userName, score } = this.state;
    return (
      <Nav>
        <h2 style={{ paddingLeft: "16px" }}>积分获取使用列表</h2>
        <Row gutter={24} style={{ padding: "16px" }}>
          <Col span={2}>用户：</Col>
          <Col span={3}>{userName}</Col>
          <Col span={2}>当前积分：</Col>
          <Col span={2}>{score}</Col>
        </Row>
        <Row gutter={24} style={{ padding: "16px" }}>
          <Col span={2}>积分明细：</Col>
        </Row>
        <Table
          dataSource={scoreList}
          columns={this.columns}
          pagination={pageConfig}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />
      </Nav>
    );
  }
}

export default RankDetail;
