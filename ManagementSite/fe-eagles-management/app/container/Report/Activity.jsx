import React, { Component } from "react";
import Nav from "../Nav";
import { getActivity, getActivityPie } from "../../services/reportService";
import { message, Card, Row, Col } from "antd";
import ReactEcharts from 'echarts-for-react';
// const TreeNode = Tree.TreeNode;
const _ = require("lodash");
import "./style.less";

class ActivityReport extends Component {
  constructor(props) {
    super(props);
    this.state = {
      activity: {},
      activityPieData: {}
    };
  }
  componentDidMount() {
    this.getInfo();
  }
  // 根据id查询详情
  getInfo = async () => {
    try {
      let res = await getActivity({
        HistogramType: "2"
      });
      this.setState({
        activity: res
      });
      let pieData = await getActivityPie({
        HistogramType: "2"
      });
      this.setState({
        activityPieData: pieData
      });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  //活动
  getOptionActivity() {
    const { activity } = this.state;

    return {
      color: ['#003366', '#006699', '#4cabce', '#e5323e'],
      tooltip: {
        trigger: 'axis',
        axisPointer: {
          type: 'shadow'
        }
      },
      legend: {
        data: activity.legend ? activity.legend.data : []
      },
      calculable: true,
      xAxis: [
        {
          type: activity.xAxis ? activity.xAxis[0].type : "",
          axisTick: { show: false },
          data: activity.xAxis ? activity.xAxis[0].data : []
        }
      ],
      yAxis: [
        {
          type: activity.yAxis ? activity.yAxis[0].type : ""
        }
      ],
      series: activity.series ? activity.series : []
    };
  }
  // 活动饼图
  getOptionActivityPie() {
    const { activityPieData } = this.state;
    return {
      title: {
        text: '活动统计',
        // subtext: '纯属虚构',
        x: 'center'
      },
      tooltip: {
        trigger: 'item',
        formatter: "{a} <br/>{b} : {c} ({d}%)"
      },
      legend: {
        type: 'scroll',
        orient: 'vertical',
        right: 10,
        top: 20,
        bottom: 20,
        data: activityPieData.legendData,
        selected: activityPieData.selected
      },
      series: [
        {
          name: '入党时间',
          type: 'pie',
          radius: '55%',
          center: ['40%', '50%'],
          data: activityPieData.seriesData,
          itemStyle: {
            emphasis: {
              shadowBlur: 10,
              shadowOffsetX: 0,
              shadowColor: 'rgba(0, 0, 0, 0.5)'
            }
          }
        }
      ]
    };
  }
  render() {
    return (
      <Nav>
        <Row gutter={12}>
          <Col span={12}>
            <Card style={{ width: 800 }}>
              <ReactEcharts
                option={this.getOptionActivity()}
                notMerge={true}
                lazyUpdate={true}
                theme={"theme_name"}
              />
            </Card>
          </Col>
        </Row>
        <Row style={{ height: 10 }}></Row>
        <Row gutter={12}>
          <Col span={12}>
            <Card style={{ width: 800 }}>
              <ReactEcharts
                option={this.getOptionActivityPie()}
                notMerge={true}
                lazyUpdate={true}
                theme={"theme_name"}
              />
            </Card>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default ActivityReport;
