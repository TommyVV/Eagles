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
  Table,
  InputNumber
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
import ExList from "../../../components/Common/ChooseEx/ExList";

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
    this.state = {
      isRandom: false,
      visible: false // 弹出框
    };
  }

  handleSubmit = e => {
    e.preventDefault();
    this.props.form.validateFields(async (err, values) => {
      if (!err) {
        try {
          console.log("submit form: ", values);
          const { Info } = this.props.info;
          let { SubjectList, HasLimitedTime } = Info;
          if (SubjectList.length) {
            if (Info.ExercisesType == "20" && SubjectList.length > 1) {
              message.error("投票类型的试卷，最多只能选择一道习题");
            } else {
              let idList = [];
              SubjectList.forEach(v => {
                idList.push(v.QuestionId);
              });
              delete Info["SubjectList"]; // 提交的时候，删除习题列表，只传数组
              let params = {
                Info: {
                  ...Info,
                  ...values,
                  HasLimitedTime
                },
                Subject: idList
              };
              let { Code,Message } = await createOrEditQuestion(params);
              if (Code === "00") {
                let tip = Info.ExercisesId ? "保存成功" : "创建成功";
                message.success(tip);
                hashHistory.replace("/questionlist");
              } else {
                // let tip = Info.ExercisesId ? "保存失败" : "创建失败";
                message.error(Message);
              }
            }
          } else {
            message.error("请选择习题");
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
    const { Info } = this.props.info;
    const { SubjectList } = Info;
    let values = getFieldsValue();
    this.props.saveInfo({
      Info: {
        ...values,
        IsScoreAward: value,
        SubjectList
      }
    });
  };
  changeType = value => {
    const { getFieldsValue } = this.props.form;
    const { Info } = this.props.info;
    const { SubjectList } = Info;
    let values = getFieldsValue();
    this.props.saveInfo({
      Info: {
        ...values,
        ExercisesType: value,
        SubjectList
      }
    });
  };
  change = value => {
    const { getFieldsValue } = this.props.form;
    let values = getFieldsValue();
    const { Info } = this.props.info;
    this.props.saveInfo({
      Info: {
        ...Info,
        ...values,
        HasLimitedTime: value == "1" ? true : false
      }
    });
  };
  changeRandom = value => {
    this.setState({
      isRandom: value == "1" ? true : false
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
        const { Info } = this.props.info;
        const { SubjectList } = Info;
        let values = getFieldsValue();
        const _this = this;
        // 删除本地的习题
        function delSubject(SubjectList) {
          // let newSubject = Subject.splice(
          //   Subject.findIndex(item => item.QuestionId === QuestionId),
          //   1
          // );
          let newSubject = SubjectList.filter(function (v) {
            return v.QuestionId != QuestionId;
          });
          _this.props.saveInfo({
            Info: {
              ...values,
              SubjectList: newSubject
            }
          });
        }
        delSubject(SubjectList);
        // if (!ExercisesId) {
        //   delSubject(Subject);
        //   message.success(`删除成功`);
        // } else {
        //   try {
        //     let { Code } = await delRelation({ ExercisesId, QuestionId });
        //     if (Code === "00") {
        //       delSubject(Subject);
        //       message.success(`删除成功`);
        //     } else {
        //       message.error(`删除失败`);
        //     }
        //   } catch (e) {
        //     throw new Error(e);
        //   }
        // }
      }
    });
  };
  // 随机生成习题
  randomFn = async ExercisesId => {
    try {
      const { getFieldsValue } = this.props.form;
      let values = getFieldsValue();
      if (values.randomCount <= 0) {
        message.error(`请输入随机生成习题数量`);
        return;
      }
      let { SubjectList } = await random({
        ExercisesId,
        RandomSubjectSum: values.randomCount
      });
      console.log("List - ", SubjectList);
      SubjectList.forEach((v, i) => {
        v.key = i;
      });
      this.props.saveInfo({
        Info: {
          ...values,
          SubjectList
        }
      });
      message.success(`成功生成${SubjectList.length}个习题`);
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  handleOk = async () => {
    const { Info } = this.props.info;
    let { SubjectList } = Info;
    const { getFieldsValue } = this.props.form;
    let values = getFieldsValue();
    const selected = this.exList.state.selectedRowKeys;
    let keys = [];
    selected.map(o => {
      keys.push(JSON.parse(o));
    });
    let newKeys = keys.concat(SubjectList);
    let hash = {};
    // let newArr=[];
    let newArr = newKeys.reduceRight((item, next) => {
      hash[next.QuestionId]
        ? ""
        : (hash[next.QuestionId] = true && item.push(next));
      return item;
    }, []);
    console.log(newArr);
    newArr.forEach((v, i) => {
      v.key = i;
    });
    this.props.saveInfo({
      Info: {
        ...values,
        SubjectList: newArr
      }
    });
    this.setState({
      visible: false
    });
    this.exList.setState({ selectedRowKeys: [] });
  };
  render() {
    const { getFieldDecorator } = this.props.form;
    const { Info } = this.props.info;
    const { ExercisesId } = Info;
    let { SubjectList } = Info;
    SubjectList &&
      SubjectList.forEach((v, i) => {
        v.key = i;
      });
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
        title: "习题编号",
        dataIndex: "QuestionId",
        key: "0"
      },
      {
        title: "习题名称",
        dataIndex: "Question",
        key: "1"
      },
      {
        key: "2",
        title: "类型",
        dataIndex: "Multiple",
        render: text => <span>{text == "0" ? "单选" : "多选"}</span>
      },
      {
        key: "3",
        title: "是否投票",
        dataIndex: "IsVote",
        render: text => <span>{text ? "是" : "否"}</span>
      },
      {
        key: "4",
        title: "操作",
        render: obj => (
          <span>
            <a
              onClick={() =>
                this.handleDelete(Info.ExercisesId, obj.QuestionId)
              }
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
              <Select onChange={this.changeType.bind(this)}>
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
          <FormItem {...formItemLayout} label="是否图片投票"
            style={{
              display: Info.ExercisesType == "20" ? "block" : "none"
            }}>
            {getFieldDecorator("IsImageVote")(
              <Select>
                <Option value="0">否</Option>
                <Option value="1">是</Option>
              </Select>
            )}
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
                  {getFieldDecorator(`PassAwardScore`)(
                    <InputNumber min={1} style={{ width: "100%" }} />
                  )}
                </FormItem>
              </Col>
              <Col span={6} key={2} style={{
                display: Info.ExercisesType == "20" ? "none" : null
              }}>
                <FormItem {...formItemLayoutChild} label="每题分值">
                  {getFieldDecorator(`SubjectScore`)(
                    <InputNumber min={1} style={{ width: "100%" }} />
                  )}
                </FormItem>
              </Col>
              <Col span={6} key={3} style={{
                display: Info.ExercisesType == "20" ? "none" : null
              }}>
                <FormItem {...formItemLayoutChild} label="及格分">
                  {getFieldDecorator(`PassScore`)(
                    <InputNumber min={1} style={{ width: "100%" }} />
                  )}
                </FormItem>
              </Col>
            </Row>
          </div>
          <FormItem {...formItemLayout} label="是否限制答题时间" style={{
            display: Info.ExercisesType == "5" ? "block" : "none"
          }}>
            {getFieldDecorator("HasLimitedTime")(
              <Select onChange={this.change.bind(this)}>
                <Option value="0">否</Option>
                <Option value="1">是</Option>
              </Select>
            )}
          </FormItem>
          <FormItem
            {...formItemLayout}
            label="限制答题时间"
            style={{
              display: Info.HasLimitedTime ? "block" : "none"
            }}
          >
            {getFieldDecorator(`LimitedTime`)(
              <InputNumber
                placeholder="请输入限制时间，单位：分钟"
                min={1}
                style={{ width: "100%" }}
              />
            )}
          </FormItem>
          <FormItem {...formItemLayout} label="是否随机生成习题">
            {getFieldDecorator("isRandom")(
              <Select onChange={this.changeRandom.bind(this)}>
                <Option value="0">否</Option>
                <Option value="1">是</Option>
              </Select>
            )}
          </FormItem>
          <FormItem
            {...formItemLayout}
            label="随机生成习题数量"
            style={{
              display: this.state.isRandom ? "block" : "none"
            }}
          >
            {getFieldDecorator(`randomCount`)(
              <InputNumber min={1} style={{ width: "100%" }} />
            )}
          </FormItem>
          <Table
            columns={columns}
            dataSource={SubjectList}
            bordered
            style={{ width: "90%" }}
            locale={{ emptyText: "未查询到符合条件的信息" }}
            title={() => (
              <Row gutter={24}>
                <Col span={4}>习题列表</Col>

                <Col span={2} offset={7}>
                  <Button
                    className="btn"
                    onClick={() => this.setState({ visible: true })}
                  >
                    新增
                  </Button>
                </Col>
                <Col
                  span={2}
                  offset={1}
                  style={{
                    display: this.state.isRandom ? "block" : "none"
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
                  onClick={() => hashHistory.replace("/questionlist")}
                >
                  取消
                </Button>
              </Col>
            </Row>
          </FormItem>
        </Form>
        <Modal
          title="选择习题"
          visible={this.state.visible}
          onOk={this.handleOk}
          onCancel={() => this.setState({ visible: false })}
          style={{ width: "100%", marginBottom: "0" }}
          width="700px"
        >
          <ExList
            ref={instance => (this.exList = instance)}
            SubjectList={SubjectList}
          />
        </Modal>
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
        value: Info.ExercisesType ? Info.ExercisesType + "" : "5"
      }),
      IsImageVote: Form.createFormField({
        value: Info.IsImageVote ? Info.IsImageVote + "" : "0"
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
        value: Info.HasLimitedTime ? "1" : "0"
      }),
      LimitedTime: Form.createFormField({
        value: Info.LimitedTime
      }),
      isRandom: Form.createFormField({
        value: "0"
      }),
      randomCount: Form.createFormField({
        value: Info.randomCount
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
  getInfo = async ExercisesId => {
    try {
      const { Info } = await getQuestionInfoById({ ExercisesId });
      const obj = {
        Info
      };
      this.props.saveInfo(obj);
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
