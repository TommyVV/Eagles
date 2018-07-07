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
  Checkbox,
  Table
} from "antd";
const { TextArea } = Input;
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

const FormItem = Form.Item;
const Option = Select.Option;

class QuestionForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      isRewardWrapper: false,
      isShowInput: false
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
  rewardHandleChange = value => {
    this.setState({ isRewardWrapper: value == "1" ? true : false });
  };
  change = e => {
    this.setState({
      isShowInput: e.target.checked
    });
  };

  render() {
    const { getFieldDecorator } = this.props.form;
    const { isRewardWrapper, isShowInput } = this.state;
    const formItemLayout = {
      labelCol: {
        xl: { span: 2 }
      },
      wrapperCol: {
        xl: { span: 8 }
      }
    };
    const columns = [
      {
        title: "题目",
        dataIndex: "exTitle"
      },
      {
        title: "类型",
        dataIndex: "exType"
      },
      {
        title: "操作",
        dataIndex: "operate",
        render: () => (
          <span>
            <a href="javascript:;">删除</a>
            <a href="javascript:;" style={{ paddingLeft: "24px" }}>
              编辑
            </a>
          </span>
        )
      }
    ];

    const data = [
      {
        key: "1",
        exTitle: "问题a",
        exType: "单选"
      },
      {
        key: "2",
        exTitle: "问题b",
        exType: "复选"
      },
      {
        key: "3",
        exTitle: "问题c",
        exType: "单选"
      }
    ];
    return (
      <div className="create_pro_form">
        <Form onSubmit={this.handleSubmit}>
          <FormItem {...formItemLayout} label="" style={{ display: "none" }}>
            {getFieldDecorator("projectId")(<Input />)}
          </FormItem>
          <FormItem {...formItemLayout} label="名称">
            {getFieldDecorator("questionName", {
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
            {getFieldDecorator("quType")(
              <Select>
                <Option value="0">在线考试</Option>
                <Option value="1">新闻习题</Option>
                <Option value="2">随机试卷</Option>
              </Select>
            )}
          </FormItem>
          <FormItem {...formItemLayout} label="来源">
            {getFieldDecorator("from", {
              rules: [
                {
                  required: true,
                  message: "必填，20字以内!",
                  pattern: /^(?!.{21}|\s*$)/g
                }
              ]
            })(<Input placeholder="必填，20字以内" />)}
          </FormItem>
          <FormItem {...formItemLayout} label="内容">
            {getFieldDecorator("content", {
              rules: [
                {
                  required: true,
                  message: "必填，20字以内!",
                  pattern: /^(?!.{21}|\s*$)/g
                }
              ]
            })(<TextArea rows={4} />)}
          </FormItem>
          <FormItem {...formItemLayout} label="是否积分奖励">
            {getFieldDecorator("isReward")(
              <Select onChange={this.rewardHandleChange}>
                <Option value="0">否</Option>
                <Option value="1">是</Option>
              </Select>
            )}
          </FormItem>
          <Form
            layout="inline"
            style={{
              display: isRewardWrapper ? "block" : "none",
              border: "1px solid #d9d9d9",
              padding: "24px",
              marginBottom: "24px"
            }}
          >
            <Row gutter={24}>
              <Col span={7} key={1}>
                <FormItem label="及格奖励积分">
                  {getFieldDecorator(`rewardAll`)(<Input />)}
                </FormItem>
              </Col>
              <Col span={7} key={2}>
                <FormItem label="每题分值">
                  {getFieldDecorator(`reward`)(<Input />)}
                </FormItem>
              </Col>
              <Col span={7} key={3}>
                <FormItem label="及格分">
                  {getFieldDecorator(`passScore`)(<Input />)}
                </FormItem>
              </Col>
            </Row>
            <Row gutter={24} style={{ paddingTop: "24px" }}>
              <Col span={12} key={4} style={{ display: "block" }}>
                <Checkbox onChange={this.change}>随机生成题目</Checkbox>
                <FormItem
                  label="随机生成题目数量"
                  style={{ display: isShowInput ? "block" : "none" }}
                >
                  {getFieldDecorator(`exCount`)(<Input />)}
                </FormItem>
              </Col>
            </Row>
          </Form>
          <Table
            columns={columns}
            dataSource={data}
            bordered
            title={() => (
              <Row gutter={24}>
                <Col span={4}>题目列表</Col>

                <Col span={2} offset={9}>
                  <Button
                    className="btn"
                    onClick={() => hashHistory.replace("/exercise/detail")}
                  >
                    新增
                  </Button>
                </Col>
                <Col
                  span={2}
                  offset={1}
                  style={{ display: isShowInput ? "block" : "none" }}
                >
                  <Button
                    className="btn btn--primary"
                    type="primary"
                    onClick={() => hashHistory.replace("/project")}
                  >
                    生成
                  </Button>
                </Col>
              </Row>
            )}
          />
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

const QuestionFormMap = Form.create({
  mapPropsToFields: props => {
    const project = props.project;
    return {
      projectId: Form.createFormField({
        value: project.basicData ? project.basicData.id : ""
      }),
      quType: Form.createFormField({
        value: "0"
      }),
      isReward: Form.createFormField({
        value: "0"
      }),
      projectName: Form.createFormField({
        value: project.basicData ? project.basicData.projectName : ""
      })
    };
  }
})(QuestionForm);

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
        <QuestionFormMap project={projectDetails} />
      </Nav>
    );
  }
}

export default QuestionDetail;
