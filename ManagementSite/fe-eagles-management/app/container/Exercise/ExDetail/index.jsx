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
      user: state.userReducer
    };
  },
  { saveInfo }
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
  change = (attr, value) => {
    const { getFieldsValue } = this.props.form;
    let values = getFieldsValue();
    this.props.saveInfo({
      Info: {
        ...values,
        [attr]: value
      }
    });
  };
  render() {
    const { getFieldDecorator } = this.props.form;
    const { Info } = this.props.info;
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
          <FormItem {...formItemLayout} label="题目名称">
            {getFieldDecorator("Question", {
              rules: [
                {
                  required: true,
                  message: "必填，请输入题目名称"
                }
              ]
            })(<Input placeholder="必填，请输入题目名称" />)}
          </FormItem>
          <FormItem {...formItemLayout} label="题目类型">
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
              <Input placeholder="请输入多选数量" />
            )}
          </FormItem>
          <FormItem {...formItemLayout} label="是否投票">
            {getFieldDecorator("isRight")(
              <Select onChange={this.change.bind(this, "isRight")}>
                <Option value="0">否</Option>
                <Option value="1">是</Option>
              </Select>
            )}
          </FormItem>
          <WrappedDynamicFieldSet isVote={Info.isRight} />
          <FormItem>
            <Row type="flex" justify="center" className="edit" gutter={24}>
              <Col>
                <Button
                  htmlType="submit"
                  className="btn btnprimary"
                  type="primary"
                >
                  {Info.QuestionId ? "保存" : "新建"}
                </Button>
              </Col>
              <Col>
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
    console.log("项目详情数据回显  ", props);
    const { Info } = props.info;
    return {
      QuestionId: Form.createFormField({
        value: Info.QuestionId
      }),
      Multiple: Form.createFormField({
        value: Info.Multiple ? Info.Multiple + "" : "0"
      }),
      MultipleCount: Form.createFormField({
        value: Info.MultipleCount
      }),
      isRight: Form.createFormField({
        value: Info.isRight ? Info.isRight + "" : "0"
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
  { clearInfo }
)
class QuestionDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      projectDetails: {} //项目详情
    };
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
      const { Info } = await getInfoById();
      this.props.saveInfo(Info);
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
