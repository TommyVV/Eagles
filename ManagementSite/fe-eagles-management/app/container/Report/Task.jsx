import React, { Component } from "react";
import Nav from "../Nav";
import { getActivity,getActivityPie } from "../../services/reportService";
import { message, Card, Row, Col } from "antd";
import ReactEcharts from 'echarts-for-react';
const _ = require("lodash");
import "./style.less";

class TaskReport extends Component {
  constructor(props) {
    super(props);
    this.state = {
      task: {},
      taskPieData: {}
    };
  }
  componentDidMount() {
    this.getInfo();
  }
  // 根据id查询详情
  getInfo = async () => {
    try {
      const res = await getActivity({
        HistogramType: "1"
      });
      this.setState({
        task: res
      });
      debugger
      let pieData = await getActivityPie({
        HistogramType: "1"
      });
      this.setState({
        taskPieData: pieData
      });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  //活动
  getOptionTask() {
    const { task } = this.state;

    return {
      color: ['#003366', '#006699', '#4cabce', '#e5323e'],
      tooltip: {
        trigger: 'axis',
        axisPointer: {
          type: 'shadow'
        }
      },
      legend: {
        data: task.legend ? task.legend.data : []
      },
      calculable: true,
      xAxis: [
        {
          type: task.xAxis ? task.xAxis[0].type : "",
          axisTick: { show: false },
          data: task.xAxis ? task.xAxis[0].data : []
        }
      ],
      yAxis: [
        {
          type: task.yAxis ? task.yAxis[0].type : ""
        }
      ],
      series: task.series ? task.series : []
    };
  }
  // task饼图
  getOptionTaskPie() {
    const { taskPieData } = this.state;
    return {
      title: {
        text: '任务统计',
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
        data: taskPieData.legendData,
        selected: taskPieData.selected
      },
      series: [
        {
          name: '入党时间',
          type: 'pie',
          radius: '55%',
          center: ['40%', '50%'],
          data: taskPieData.seriesData,
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
                option={this.getOptionTask()}
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
                option={this.getOptionTaskPie()}
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

export default TaskReport;
