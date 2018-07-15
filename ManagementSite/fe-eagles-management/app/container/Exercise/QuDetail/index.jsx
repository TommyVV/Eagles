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
  Modal,
  Table
} from "antd";
import Nav from "../../Nav";
import { hashHistory } from "react-router";
import { typeMap } from "../../../constants/config/appconfig";
import {
  getQuestionInfoById,
  createOrEditQuestion
} from "../../../services/questionService";
import { delRelation, random } from "../../../services/exerciseService";
import { saveInfo, clearInfo } from "../../../actions/questionAction";
import "./style.less";

const FormItem = Form.Item;
const Option = Select.Option;
const TextArea = Input.TextArea;
const confirm = Modal.confirm;
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
    this.state = {};
  }

  handleSubmit = e => {
    e.preventDefault();
    this.props.form.validateFields(async (err, values) => {
      if (!err) {
        try {
          console.log("Received values of form: ", values);
          let { Subject } = this.props.info;
          let idList = [];
          Subject.forEach(v => {
            idList.push(v.QuestionId);
          });
          let params = {
            Info: {
              ...this.props.info.Info,
              ...values
            },
            Subject: idList
          };
          let { Code } = await createOrEditQuestion(params);
          if (Code === "00") {
            let tip = this.props.info.Info.ExercisesId
              ? "保存成功"
              : "创建成功";
            message.success(tip);
            hashHistory.replace("/questionlist");
          } else {
            let tip = this.props.info.Info.ExercisesId
              ? "保存失败"
              : "创建失败";
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
      Info: {
        ...values,
        IsScoreAward: value
      }
    });
  };
  change = value => {
    const { getFieldsValue } = this.props.form;
    let values = getFieldsValue();
    this.props.saveInfo({
      Info: {
        ...values,
        HasLimitedTime: value
      }
    });
  };
  // 删除
  handleDelete = async (ExercisesId, QuestionId) => {
    confirm({
      title: `是否确认删除?`,
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        const { getFieldsValue } = this.props.form;
        let { Subject } = this.props.info;
        let values = getFieldsValue();
        const _this = this;
        // 删除本地的习题
        function delSubject(Subject) {
          // let newSubject = Subject.splice(
          //   Subject.findIndex(item => item.QuestionId === QuestionId),
          //   1
          // );
          let newSubject = Subject.filter(function(v) {
            return v.QuestionId != QuestionId;
          });
          _this.props.saveInfo({
            Info: {
              ...values
            },
            Subject: newSubject
          });
        }
        if (!ExercisesId) {
          delSubject(Subject);
          message.success(`删除成功`);
        } else {
          try {
            let { Code } = await delRelation({ ExercisesId, QuestionId });
            if (Code === "00") {
              delSubject(Subject);
              message.success(`删除成功`);
            } else {
              message.error(`删除失败`);
            }
          } catch (e) {
            throw new Error(e);
          }
        }
      }
    });
  };
  // 随机生成习题
  randomFn = async ExercisesId => {
    try {
      const { getFieldsValue } = this.props.form;
      let values = getFieldsValue();
      if (!values.LimitedTime) {
        message.error(`请输入随机生成题目数量`);
        return;
      }
      let { SubjectList } = await random({
        ExercisesId,
        RandomSubjectSum: values.LimitedTime
      });
      console.log("List - ", SubjectList);
      SubjectList.forEach(v => {
        v.key = v.QuestionId;
      });
      this.props.saveInfo({
        Info: {
          ...values
        },
        Subject: SubjectList
      });
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  render() {
    const { getFieldDecorator } = this.props.form;
    const { Info, Subject } = this.props.info;
    console.log("渲染习题列表：", Subject);
    const formItemLayout = {
      labelCol: {
        xl: { span: 3 }
      },
      wrapperCol: {
        xl: { span: 6 }
      }
    };
    const formItemLayoutChild = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 10 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 12 }
      }
    };
    const columns = [
      {
        title: "题目",
        dataIndex: "Question"
      },
      {
        title: "类型",
        dataIndex: "Multiple",
        render: text => <span>{text == "0" ? "单选" : "多选"}</span>
      },
      {
        title: "操作",
        render: obj => (
          <span>
            <a
              onClick={() =>
                hashHistory.replace(`/exercise/detail/${obj.QuestionId}`)
              }
            >
              编辑
            </a>
            <a
              onClick={() =>
                this.handleDelete(Info.ExercisesId, obj.QuestionId)
              }
              style={{ paddingLeft: "24px" }}
            >
              删除
            </a>
          </span>
        )
      }
    ];
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
            {getFieldDecorator("source", {
              rules: [
                {
                  required: true,
                  message: "必填，请输入试卷来源"
                }
              ]
            })(<Input placeholder="请输入试卷来源" />)}
          </FormItem>
          <FormItem {...formItemLayout} label="内容">
            {getFieldDecorator("HtmlDescription")(
              <TextArea rows={4} placeholder="请输入试卷内容" />
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
              display: Info.IsScoreAward == "1" ? "block" : "none",
              border: "1px solid #e8e8e8",
              borderRadius: "4px",
              paddingTop: "24px",
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
          </div>
          <FormItem {...formItemLayout} label="是否随机生成题目">
            {getFieldDecorator("HasLimitedTime")(
              <Select onChange={this.change.bind(this)}>
                <Option value="0">否</Option>
                <Option value="1">是</Option>
              </Select>
            )}
          </FormItem>
          <FormItem
            {...formItemLayout}
            label="随机生成题目数量"
            style={{
              display: Info.HasLimitedTime == "1" ? "block" : "none"
            }}
          >
            {getFieldDecorator(`LimitedTime`)(<Input />)}
          </FormItem>
          <Table
            columns={columns}
            dataSource={Subject}
            bordered
            style={{ width: "90%" }}
            locale={{ emptyText: "暂无数据" }}
            title={() => (
              <Row gutter={24}>
                <Col span={4}>习题列表</Col>

                <Col span={2} offset={7}>
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
                  style={{
                    display: Info.HasLimitedTime == "1" ? "block" : "none"
                  }}
                >
                  <Button
                    className="btn btn--primary"
                    type="primary"
                    onClick={() => this.randomFn(Info.ExercisesId)}
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
                  {this.props.info.Info.ExercisesId ? "保存" : "新建"}
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
    const { Info } = props.info;
    console.log("数据回显：", Info);
    return {
      ExercisesId: Form.createFormField({
        value: Info.ExercisesId
      }),
      ExercisesName: Form.createFormField({
        value: Info.ExercisesName
      }),
      ExercisesType: Form.createFormField({
        value: Info.ExercisesType + ""
      }),
      source: Form.createFormField({
        value: Info.source
      }),
      HtmlDescription: Form.createFormField({
        value: Info.HtmlDescription
      }),
      IsScoreAward: Form.createFormField({
        value: Info.IsScoreAward == "1" ? "1" : "0"
      }),
      PassAwardScore: Form.createFormField({
        value: Info.PassAwardScore
      }),
      SubjectScore: Form.createFormField({
        value: Info.SubjectScore
      }),
      PassScore: Form.createFormField({
        value: Info.PassScore
      }),
      HasLimitedTime: Form.createFormField({
        value: Info.HasLimitedTime == "1" ? "1" : "0"
      }),
      LimitedTime: Form.createFormField({
        value: Info.LimitedTime
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
        <QuestionFormMap info={this.props.questionReducer} />
      </Nav>
    );
  }
}
