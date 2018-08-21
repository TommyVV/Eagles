import React, { Component } from "react";
import Nav from "../Nav";
import { getArticle } from "../../services/reportService";
import { getList } from "../../services/branchService";
import ReactEcharts from 'echarts-for-react';
import { message, Form, Select, Card, Row, Col } from "antd";
const FormItem = Form.Item;
const Option = Select.Option;
const _ = require("lodash");
import "./style.less";

class ArticleReport extends Component {
  constructor(props) {
    super(props);
    this.state = {
      article: {},
      branchList: []
    };
  }
  componentDidMount() {
    this.getCurrentList();
  }
  // 查询机构列表
  getCurrentList = async () => {
    try {
      let { List } = await getList({
        PageNumber: 1,
        PageSize: 1000000000,
      });
      if (List.length) {
        const { BranchId } = List[0];
        this.getInfo(BranchId);
      }
      this.setState({ branchList: List });
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  // 根据id查询详情
  getInfo = async (BranchId) => {
    try {
      const res = await getArticle({ BranchId });
      this.setState({
        article: res
      });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  changeSelect(value) {
    this.getInfo(value);
  }
  // 文章
  getOptionArticle() {
    const { article } = this.state;
    return {
      title: {
        text: '文章数据统计'
      },
      tooltip: {
        trigger: 'axis'
      },
      legend: {
        data: article.legend ? article.legend.data : []
      },
      grid: {
        left: '3%',
        right: '4%',
        bottom: '3%',
        containLabel: true
      },
      xAxis: {
        type:  article.xAxis ? article.xAxis[0].type : "",
        boundaryGap: false,
        data: article.xAxis ? article.xAxis[0].data : []
      },
      yAxis: {
        type: article.yAxis ? article.yAxis[0].type : ""
      },
      series: article.series ? article.series : []
    };
  }
  render() {
    const { branchList } = this.state;
    return (
      <Nav>
        <Row gutter={24}>
          <Col span={2} key={111} style={{ textAlign: "center" }}>
            <FormItem label="选择支部" />
          </Col>
          <Col span={5} key={22}>
            {branchList.length ? (<Select
              onChange={this.changeSelect.bind(this)}
              style={{ width: "100%" }}
              defaultValue={branchList[0].BranchId}
            >
              {branchList.map((o, i) => {
                return (
                  <Option key={i} value={o.BranchId} >
                    {o.BranchName}
                  </Option>
                );
              })}
            </Select>) : null}
          </Col>
        </Row>
        <Row gutter={16}>
          <Col span={8}>
            <Card style={{ width: 800 }}>
              <ReactEcharts
                option={this.getOptionArticle()}
              />
            </Card>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default ArticleReport;
