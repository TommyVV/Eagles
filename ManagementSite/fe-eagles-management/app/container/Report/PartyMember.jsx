import React, { Component } from "react";
import Nav from "../Nav";
import { getPartyMember } from "../../services/reportService";
import { message, Card, Row, Col } from "antd";
import ReactEcharts from 'echarts-for-react';
import "./style.less";

class PartyMemberReport extends Component {
  constructor(props) {
    super(props);
    this.state = {
      sex: {},
      age: {},
      time: {},
    };
  }
  componentDidMount() {
    this.getInfo();
  }
  // 根据id查询详情
  getInfo = async () => {
    try {
      const sex = await getPartyMember({
        PieType: "1"
      });
      const age = await getPartyMember({
        PieType: "2"
      });
      const time = await getPartyMember({
        PieType: "3"
      });
      this.setState({
        sex, age, time
      });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  // 男女
  getOptionSex() {
    const { sex } = this.state;
    return {
      title: {
        text: '党员男女数量统计',
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
        data: sex.legendData,
        selected: sex.selected
      },
      series: [
        {
          name: '性别',
          type: 'pie',
          radius: '55%',
          center: ['40%', '50%'],
          data: sex.seriesData,
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
  // 年龄段
  getOptionAge() {
    const { age } = this.state;
    return {
      title: {
        text: '党员年龄段统计',
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
        data: age.legendData,

        selected: age.selected
      },
      series: [
        {
          name: '年龄段',
          type: 'pie',
          radius: '55%',
          center: ['40%', '50%'],
          data: age.seriesData,
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
  // 入党时间段
  getOptionTime() {
    const { time } = this.state;
    return {
      title: {
        text: '党员入党时间统计',
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
        data: time.legendData,

        selected: time.selected
      },
      series: [
        {
          name: '入党时间',
          type: 'pie',
          radius: '55%',
          center: ['40%', '50%'],
          data: time.seriesData,
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
        <Row gutter={16}>
          <Col span={8}>
            <Card style={{ width: 500 }}>
              <ReactEcharts
                option={this.getOptionSex()}
                notMerge={true}
                lazyUpdate={true}
                theme={"theme_name"}
              />
            </Card>
          </Col>
          <Col span={8}>
            <Card style={{ width: 500 }}>
              <ReactEcharts
                option={this.getOptionAge()}
                notMerge={true}
                lazyUpdate={true}
                theme={"theme_name"}
              />
            </Card>
          </Col>
          <Col span={8}>
            <Card style={{ width: 500 }}>
              <ReactEcharts
                option={this.getOptionTime()}
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

export default PartyMemberReport;
