import React, { Component } from "react";
import {
  Button,
  Input,
  Form,
  message,
  Row,
  Col,
  Select,
  Avatar,
  Icon,
  DatePicker
} from "antd";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import "./style.less";
import DynamicMenuSet from "../../components/Common/AddField/AddMenu";

class MenuDetailTwo extends Component {
  constructor(props) {
    super(props);
    this.state = {
      projectDetails: {} //项目详情
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    console.log(id);
    // let author = {
    //   name: this.props.user.userName,
    //   user_id: this.props.user.userId,
    //   avatar: this.props.user.avatar,
    //   open_id: this.props.user.openId
    // };
    // if (projectId) {
    //   this.getInfo(projectId, author); //当前用户排在第一位
    // } else {
    //   let projectMembers = [author];
    //   this.props.saveProjectInfo({ projectMembers });
    // }
  }

  componentWillUnmount() {
    // this.props.clearProjectInfo();
  }
  // 根据id查询详情
  getInfo = async (projectId, author) => {
    try {
      let projectDetails = await getProjectInfoById({ projectId });
      console.log("projectDetails", projectDetails);
      this.setState({ projectDetails });
      let projectMembers = [author, ...projectDetails.membersData];
      let prevDemandAuthor = {
        open_id: projectDetails.basicData.open_id,
        create: true
      };
      this.props.saveProjectInfo({
        projectId,
        projectMembers,
        // prevDemandAuthor,
        open_id: projectDetails.basicData.open_id,
        projectName: projectDetails.basicData.projectName,
        requirementId: projectDetails.basicData.requirementId,
        requirementName: projectDetails.basicData.requirementName
      });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };

  render() {
    const { projectDetails } = this.state;
    return (
      <Nav>
        {/* isTwo 是否维护二级菜单 */}
        <DynamicMenuSet isTwo={true}/>
        <Row type="flex" justify="flexStart" className="edit" gutter={24}>
          <Col offset={4}>
            <Button
              htmlType="submit"
              className="btn btn--primary"
              type="primary"
            >
              {this.props.project === "" ? "新建" : "保存"}
            </Button>
          </Col>
          <Col>
            <Button
              className="btn"
              onClick={() => hashHistory.replace("/project")}
            >
              取消
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default MenuDetailTwo;
