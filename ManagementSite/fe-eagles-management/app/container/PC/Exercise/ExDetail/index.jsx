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
  Icon
} from "antd";
import { connect } from "react-redux";
import Nav from "../../Nav";
import { hashHistory } from "react-router";
import {
  saveProjectInfo,
  chooseMember,
  removeMemberFn,
  clearProjectInfo,
  removeDemandFn
} from "../../../../actions/PC/projectAction";
import "./style.less";
import WrappedDynamicFieldSet from "../../../../components/Common/AddField/AddEx";

const FormItem = Form.Item;
const Option = Select.Option;

@connect(
  state => {
    return {
      project: state.projectReducer,
      user: state.userReducer
    };
  },
  { chooseMember, removeMemberFn, removeDemandFn }
)
class Base extends Component {
  constructor(props) {
    super(props);
    this.state = {
      showDemandList: false, //项目需求列表
      showMemberList: false //项目需求列表
    };
  }

  handleSubmit = e => {
    e.preventDefault();
    this.props.form.validateFields(async (err, values) => {
      if (!err) {
        try {
          console.log("Received values of form: ", values);
          let { projectMembers } = this.props.project;
          let newProjectMembers = projectMembers.filter(
            v => v.user_id !== this.props.user.userId
          ); //删除本人
          let { projectName } = values;
          let { code } = await createProject({
            ...values,
            ...this.props.project,
            projectName,
            projectMembers: JSON.stringify(newProjectMembers)
          });
          if (code === 0) {
            let tip = this.props.project.projectId
              ? "保存项目成功"
              : "创建项目成功";
            message.success(tip);
            hashHistory.replace("/project");
          } else {
            let tip = this.props.project.projectId
              ? "保存项目失败"
              : "创建项目失败";
            message.error(tip);
          }
        } catch (e) {
          throw new Error(e);
        }
      } else {
        message.error("请检查字段是否正确");
      }
    });
  };

  showModal = attr => {
    this.setState({
      [attr]: true
    });
  };

  handleCancel = attr => {
    this.setState({
      [attr]: false
    });
  };

  render() {
    const { getFieldDecorator } = this.props.form;
    const {
      showDemandList,
      basicData,
      membersData,
      showMemberList
    } = this.state;
    console.log("members - ", this.props);
    const formItemLayout = {
      labelCol: {
        xl: { span: 2 }
      },
      wrapperCol: {
        xl: { span: 12 }
      }
    };
    const tailFormItemLayout = {
      wrapperCol: {
        xl: {
          span: 5,
          offset: 11
        }
      }
    };
    return (
      <div className="create_pro_form">
        <Form onSubmit={this.handleSubmit}>
          <FormItem {...formItemLayout} label="" style={{ display: "none" }}>
            {getFieldDecorator("projectId")(<Input />)}
          </FormItem>
          <FormItem {...formItemLayout} label="题目名称">
            {getFieldDecorator("projectName", {
              rules: [
                {
                  required: true,
                  message: "必填，20字以内!",
                  pattern: /^(?!.{21}|\s*$)/g
                }
              ]
            })(<Input placeholder="必填，20字以内" />)}
          </FormItem>
          <FormItem {...formItemLayout} label="类型">
            {getFieldDecorator("exType")(
              <Select>
                <Option value="0">单选</Option>
                <Option value="1">复选</Option>
              </Select>
            )}
          </FormItem>
          <FormItem {...formItemLayout} label="">
            {getFieldDecorator("projectName", {
              rules: [
                {
                  required: true,
                  message: "必填，20字以内!",
                  pattern: /^(?!.{21}|\s*$)/g
                }
              ]
            })(<WrappedDynamicFieldSet />)}
          </FormItem>
          <FormItem>
            <Row type="flex" justify="center" className="edit" gutter={24}>
              <Col>
                <Button
                  htmlType="submit"
                  className="btn btn--primary"
                  type="primary"
                >
                  {this.props.project.projectId === "" ? "新建" : "保存"}
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
          </FormItem>
        </Form>
      </div>
    );
  }
}

const FormMap = Form.create({
  mapPropsToFields: props => {
    console.log("项目详情数据回显 - ", props);
    const project = props.project;
    return {
      projectId: Form.createFormField({
        value: project.basicData ? project.basicData.id : ""
      }),
      exType: Form.createFormField({
        value: 0
      }),
      projectName: Form.createFormField({
        value: project.basicData ? project.basicData.projectName : ""
      })
    };
  }
})(Base);

@connect(
  state => {
    return {
      user: state.userReducer
    };
  },
  { saveProjectInfo, clearProjectInfo }
)
class QuestionDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      projectDetails: {} //项目详情
    };
  }

  componentWillMount() {
    // let { projectId } = this.props.params;
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
    this.props.clearProjectInfo();
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
        <FormMap project={projectDetails} />
      </Nav>
    );
  }
}

export default QuestionDetail;
