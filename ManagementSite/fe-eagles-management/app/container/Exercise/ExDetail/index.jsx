import React, { Component } from "react";
import {
  Button,
  Input,
  Form,
  message,
  Row,
  Col,
  Select,
  InputNumber
} from "antd";
import { connect } from "react-redux";
import Nav from "../../Nav";
import { hashHistory } from "react-router";
import { typeMap } from "../../../constants/config/appconfig";
import { getInfoById, createOrEdit } from "../../../services/exerciseService";
import { saveInfo, clearInfo } from "../../../actions/exAction";
import WrappedDynamicFieldSet from "../../../components/Common/AddField/AddEx";

import "./style.less";

const FormItem = Form.Item;
const Option = Select.Option;

@connect(
  state => {
    return {
      user: state.userReducer,
      exReducer: state.exReducer
    };
  },
  { saveInfo }
)
class Base extends Component {
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
          const { Info } = this.props.info;
          const { OptionList } = Info;
          if (OptionList.length) {
            let list = [];
            let isEmptyOption = false;
            OptionList.map((o, i) => {
              if (!o.OptionName) {
                isEmptyOption = true;
                return;
              }
              list.push({
                OptionId: o.OptionId,
                OptionName: o.OptionName,
                QuestionId: Info.QuestionId,
                IsRight: o.IsRight,
                AnswerType: o.AnswerType,
                Img: o.Img
              });
            });
            if (isEmptyOption) {
              message.error("选项不能为空");
            }
            let params = {
              Info: {
                Question: values.Question,
                QuestionId: values.QuestionId,
                IsVote: values.IsVote == "1" ? true : false,
                Multiple: values.Multiple,
                MultipleCount: values.MultipleCount
              },
              Option: list
            };
            let { Code } = await createOrEdit(params);
            if (Code === "00") {
              let tip = Info.QuestionId ? "保存成功" : "创建成功";
              message.success(tip);
              hashHistory.replace("/exerciselist");
            } else {
              let tip = Info.QuestionId ? "保存失败" : "创建失败";
              message.error(tip);
            }
          } else {
            message.error("请添加选项");
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
  change = (attr, value) => {
    const { getFieldsValue } = this.props.form;
    const { Info } = this.props.info;
    let values = getFieldsValue();
    console.log(attr, value);
    if (attr == "IsVote") {
      value = value == "1" ? true : false;
      this.props.saveInfo({
        Info: {
          ...Info,
          ...values,
          [attr]: value
        }
      });
      return;
    }
    if (attr == "MultipleCount") {
      value = value;
    }
    this.props.saveInfo({
      Info: {
        ...Info,
        ...values,
        [attr]: value,
        IsVote: Info.IsVote
      }
    });
  };
  setOptionList(OptionList, obj) {
    const { getFieldsValue } = this.props.form;
    const { Info } = this.props.info;
    let values = getFieldsValue();
    this.props.saveInfo({
      Info: {
        ...Info,
        ...values,
        OptionList,
        ...obj
      }
    });
  }
  render() {
    const { getFieldDecorator } = this.props.form;
    const { Info } = this.props.info;
    console.log(Info);
    const obj = {
      IsVote: Info.IsVote,
      Multiple: Info.Multiple,
      MultipleCount: Info.MultipleCount
    };
    const formItemLayout = {
      labelCol: {
        xl: { span: 2 }
      },
      wrapperCol: {
        xl: { span: 6 }
      }
    };
    const formItemLayoutOption = {
      labelCol: {
        xl: { span: 2 }
      },
      wrapperCol: {
        xl: { span: 24 }
      }
    };
    return (
      <div className="create_pro_form">
        <Form onSubmit={this.handleSubmit}>
          <FormItem {...formItemLayout} label="" style={{ display: "none" }}>
            {getFieldDecorator("QuestionId")(<Input />)}
          </FormItem>
          <FormItem {...formItemLayout} label="习题名称">
            {getFieldDecorator("Question", {
              rules: [
                {
                  required: true,
                  message: "必填，请输入习题名称"
                }
              ]
            })(<Input placeholder="必填，请输入习题名称" />)}
          </FormItem>
          <FormItem {...formItemLayout} label="习题类型">
            {getFieldDecorator("Multiple")(
              <Select onChange={this.change.bind(this, "Multiple")}>
                <Option value="0">单选</Option>
                <Option value="1">多选</Option>
              </Select>
            )}
          </FormItem>
          <FormItem
            {...formItemLayout}
            label="多选数量"
            style={{ display: Info.Multiple == "1" ? "block" : "none" }}
          >
            {getFieldDecorator("MultipleCount")(
              <InputNumber
                min={2}
                placeholder="请输入多选数量"
                onChange={this.change.bind(this, "MultipleCount")}
                style={{ width: "100%" }}
              />
            )}
          </FormItem>
          <FormItem {...formItemLayout} label="是否投票">
            {getFieldDecorator("IsVote")(
              <Select onChange={this.change.bind(this, "IsVote")}>
                <Option value="0">否</Option>
                <Option value="1">是</Option>
              </Select>
            )}
          </FormItem>
          {/* 选项 */}
          <WrappedDynamicFieldSet
            obj={obj}
            OptionList={Info.OptionList}
            setOptionList={this.setOptionList.bind(this)}
          />
          <FormItem>
            <Row gutter={24} style={{ marginTop: "24px" }}>
              <Col span={2} offset={2}>
                <Button
                  htmlType="submit"
                  className="btn btn--primary"
                  type="primary"
                >
                  {Info.QuestionId ? "保存" : "新建"}
                </Button>
              </Col>
              <Col span={2} offset={1}>
                <Button
                  className="btn"
                  onClick={() => hashHistory.replace("/exerciselist")}
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
    const { Info } = props.info;
    console.log("项目详情数据回显  ", Info);
    return {
      QuestionId: Form.createFormField({
        value: Info.QuestionId
      }),
      Question: Form.createFormField({
        value: Info.Question
      }),
      Multiple: Form.createFormField({
        value: Info.Multiple ? Info.Multiple + "" : "0"
      }),
      MultipleCount: Form.createFormField({
        value: Info.MultipleCount
      }),
      IsVote: Form.createFormField({
        value: Info.IsVote ? "1" : "0"
      }),
      AnswerType: Form.createFormField({
        value: Info.AnswerType ? Info.AnswerType + "" : "0"
      }),
      OptionList: Form.createFormField({
        value: Info.OptionList ? Info.AnswerType : []
      })
    };
  }
})(Base);

@connect(
  state => {
    return {
      user: state.userReducer,
      exReducer: state.exReducer
    };
  },
  { clearInfo, saveInfo }
)
class QuestionDetail extends Component {
  constructor(props) {
    super(props);
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
  getInfo = async QuestionId => {
    try {
      const { Info } = await getInfoById({ QuestionId });
      console.log(Info);
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
        <FormMap info={this.props.exReducer} />
      </Nav>
    );
  }
}

export default QuestionDetail;
