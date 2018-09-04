import React, { Component } from "react";
import Nav from "../Nav";
import { getActivity } from "../../services/reportService";
import { message, Card, Row, Col } from "antd";
import ReactEcharts from 'echarts-for-react';
const _ = require("lodash");
import "./style.less";

class TaskReport extends Component {
  constructor(props) {
    super(props);
    this.state = {
      task: {}
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
      </Nav>
    );
  }
}

export default TaskReport;
