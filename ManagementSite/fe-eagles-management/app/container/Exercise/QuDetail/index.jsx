import React, { Component } from "react";
import { connect } from "react-redux";
import {
  Button,
  Input,
  Form,
  message,
  Row,
  Col,
  Select,
  Checkbox,
  Table
} from "antd";
import Nav from "../../Nav";
import { hashHistory } from "react-router";
import { typeMap } from "../../../constants/config/question";
import { getQuestionInfoById } from "../../../services/questionService";
import { serverConfig } from "../../../constants/ServerConfigure";
import { saveInfo, clearInfo } from "../../../actions/questionAction";
// 引入编辑器以及编辑器样式
import BraftEditor from "braft-editor";
import "braft-editor/dist/braft.css";
import "./style.less";

const FormItem = Form.Item;
const Option = Select.Option;
@connect(
  state => {
    return {
      userReducer: state.userReducer,
      questionReducer: state.questionReducer
    };
  },
  { saveInfo, clearInfo }
)
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
    const { getFieldsValue } = this.props.form;
    let values = getFieldsValue();
    this.props.saveInfo({
      ...values,
      IsScoreAward: value == "1" ? true : false
    });
  };
  change = e => {
    const { getFieldsValue } = this.props.form;
    let values = getFieldsValue();
    this.props.saveInfo({
      ...values,
      HasLimitedTime: e.target.checked
    });
  };

  render() {
    const { getFieldDecorator } = this.props.form;
    const { question } = this.props;
    const { isShowInput } = this.state;
    const formItemLayout = {
      labelCol: {
        xl: { span: 2 }
      },
      wrapperCol: {
        xl: { span: 8 }
      }
    };
    const formItemLayoutContent = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 2 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 19 }
      }
    };
    const formItemLayoutChild = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 8 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 12 }
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
    // 编辑器属性
    const editorProps = {
      height: 300,
      contentFormat: "html",
      initialContent: question.Content,

      uploadImgShowBase64: false, // 是否使用base64
      uploadImgServer: serverConfig.API_SERVER + serverConfig.FILE.UPLOAD, // 图片上传到自己的服务器，自己需要自定义方法，todo
      onChange: Content => {
        setFieldsValue({ Content });
        console.log("新闻内容：", getFieldsValue());
      }
      // onRawChange: this.handleRawChange
    };
    return (
      <div className="create_pro_form">
        <Form onSubmit={this.handleSubmit}>
          <FormItem {...formItemLayout} label="" style={{ display: "none" }}>
            {getFieldDecorator("ExercisesId")(<Input />)}
          </FormItem>
          <FormItem {...formItemLayout} label="名称">
            {getFieldDecorator("ExercisesName", {
              rules: [
                {
                  required: true,
                  message: "必填，请输入试卷名称"
                }
              ]
            })(<Input placeholder="请输入试卷名称" />)}
          </FormItem>
          <FormItem {...formItemLayout} label="试卷类型">
            {getFieldDecorator("ExercisesType")(
              <Select>
                {typeMap.map((obj, index) => {
                  return (
                    <Option key={index} value={obj.ExercisesType}>
                      {obj.text}
                    </Option>
                  );
                })}
              </Select>
            )}
          </FormItem>
          <FormItem {...formItemLayout} label="来源">
            {getFieldDecorator("Source")(
              <Input placeholder="请输入试卷来源" />
            )}
          </FormItem>
          <FormItem {...formItemLayoutContent} label="内容">
            {getFieldDecorator("Content")(
              <div className="editor-wrap">
                <BraftEditor {...editorProps} />
              </div>
            )}
          </FormItem>
          <FormItem {...formItemLayout} label="是否积分奖励">
            {getFieldDecorator("IsScoreAward")(
              <Select onChange={this.rewardHandleChange.bind(this)}>
                <Option value="0">否</Option>
                <Option value="1">是</Option>
              </Select>
            )}
          </FormItem>
          <div
            style={{
              display: question.IsScoreAward ? "block" : "none",
              border: "1px solid #e8e8e8",
              borderRadius: "4px",
              padding: "24px",
              marginBottom: "24px",
              width: "90%"
            }}
          >
            <Row gutter={24}>
              <Col span={6} key={1}>
                <FormItem {...formItemLayoutChild} label="及格奖励积分">
                  {getFieldDecorator(`PassAwardScore`)(<Input />)}
                </FormItem>
              </Col>
              <Col span={6} key={2}>
                <FormItem {...formItemLayoutChild} label="每题分值">
                  {getFieldDecorator(`SubjectScore`)(<Input />)}
                </FormItem>
              </Col>
              <Col span={6} key={3}>
                <FormItem {...formItemLayoutChild} label="及格分">
                  {getFieldDecorator(`PassScore`)(<Input />)}
                </FormItem>
              </Col>
            </Row>
            <Row gutter={24}>
              <Col span={7} key={4} style={{ display: "block" }}>
                <Checkbox
                  onChange={this.change.bind(this)}
                  checked={question.HasLimitedTime}
                >
                  随机生成题目
                </Checkbox>
                <FormItem
                  {...formItemLayoutChild}
                  label="随机生成题目数量"
                  style={{ display: isShowInput ? "block" : "none" }}
                >
                  {getFieldDecorator(`exCount`)(<Input />)}
                </FormItem>
              </Col>
            </Row>
          </div>
          <Table
            columns={columns}
            dataSource={data}
            bordered
            style={{ width: "90%" }}
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
            <Row type="flex" className="edit" gutter={24}>
              <Col offset={2}>
                <Button
                  htmlType="submit"
                  className="btn btn--primary"
                  type="primary"
                >
                  {this.props.question.ExercisesId === "" ? "新建" : "保存"}
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
    const question = props.question;
    console.log("数据回显：", question);
    return {
      ExercisesId: Form.createFormField({
        value: question.ExercisesId
      }),
      ExercisesName: Form.createFormField({
        value: question.ExercisesName
      }),
      ExercisesType: Form.createFormField({
        value: question.ExercisesType + ""
      }),
      Source: Form.createFormField({
        value: question.Source
      }),
      Content: Form.createFormField({
        value: question.Content
      }),
      IsScoreAward: Form.createFormField({
        value: question.IsScoreAward ? "1" : "0"
      }),
      PassAwardScore: Form.createFormField({
        value: question.PassAwardScore
      }),
      SubjectScore: Form.createFormField({
        value: question.SubjectScore
      }),
      PassScore: Form.createFormField({
        value: question.PassScore
      }),
      HasLimitedTime: Form.createFormField({
        value: question.HasLimitedTime
      }),
      LimitedTime: Form.createFormField({
        value: question.LimitedTime
      })
    };
  }
})(QuestionForm);
@connect(
  state => {
    return {
      userReducer: state.userReducer,
      questionReducer: state.questionReducer
    };
  },
  { saveInfo, clearInfo }
)
export default class QuestionDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿详情
    } else {
      this.props.clearInfo();
    }
  }
  // 根据id查询详情
  getInfo = async id => {
    try {
      const { Info } = await getQuestionInfoById();
      this.props.saveInfo(Info);
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  render() {
    return (
      <Nav>
        <QuestionFormMap question={this.props.questionReducer} />
      </Nav>
    );
  }
}
