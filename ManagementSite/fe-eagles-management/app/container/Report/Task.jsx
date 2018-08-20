import React, { Component } from "react";
const Fragment = React.Fragment;
import Nav from "../Nav";
import { getTreeInfo } from "../../services/reportService";
import { message } from "antd";
// const TreeNode = Tree.TreeNode;
const _ = require("lodash");
import "./style.less";

class TaskReport extends Component {
  constructor(props) {
    super(props);
    this.state = {
      tree: {}
    };
  }
  componentDidMount() {
    this.getInfo();
  }
  // 根据id查询详情
  getInfo = async () => {
    try {
      const { legendData,seriesData,selected } = await getTreeInfo({
        PieType: "2"
      });
      console.log(legendData,seriesData,selected);
      this.setState({
        // tree: Info
      });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };

  render() {
    const { tree } = this.state;
    return (
      <Nav>
        
      </Nav>
    );
  }
}

export default TaskReport;
