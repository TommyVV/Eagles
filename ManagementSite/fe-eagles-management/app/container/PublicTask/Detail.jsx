import React, { Component } from "react";
import {
  Button,
  Input,
  Form,
  message,
  Row,
  Col,
  Select,
  DatePicker
} from "antd";
import moment from "moment";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import { getInfoById, audit } from "../../services/publicTaskService";
import "./style.less";

const FormItem = Form.Item;
const Option = Select.Option;
const TextArea = Input.TextArea;
class Base extends Component {
  constructor(props) {
    super(props);
  }
  handleSubmit = e => {
    e.preventDefault();
    this.props.form.validateFields(async (err, values) => {
      if (!err) {
        try {
          console.log("Received values of form: ", values);
          const { obj } = this.props;
          const { RepeatTime, NoticeTime } = values;

          let params = {
            Info: {
              ...obj,
              ...values,
              NoticeTime:
                RepeatTime == "0"
                  ? moment(NoticeTime, "MM-dd").format()
                  : moment(NoticeTime, "yy-MM-dd").format()
            }
          };
          let { Code } = await createOrEdit(params);
          if (Code === "00") {
            let tip = obj.NewsId ? "保存成功" : "创建成功";
            message.success(tip);
            hashHistory.replace("/objlist");
          } else {
            let tip = obj.NewsId ? "保存失败" : "创建失败";
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
  render() {
    const { getFieldDecorator } = this.props.form;
    const { obj, audit } = this.props;
    const formItemLayout = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 4 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 6 }
      }
    };

    return (
      <Form onSubmit={this.handleSubmit}>
        <FormItem {...formItemLayout} label="" style={{ display: "none" }}>
          {getFieldDecorator("TaskId")(<Input />)}
        </FormItem>
        <FormItem {...formItemLayout} label="任务标题">
          {getFieldDecorator("TaskTitle")(<Input disabled />)}
        </FormItem>
        <FormItem {...formItemLayout} label="发起人">
          {getFieldDecorator("FromUser")(<Input disabled />)}
        </FormItem>
        <FormItem {...formItemLayout} label="负责人">
          {getFieldDecorator("ResponsibleUserName")(<Input disabled />)}
        </FormItem>
        <FormItem {...formItemLayout} label="发布时间">
          {getFieldDecorator("CreateTime")(<Input disabled />)}
        </FormItem>
        <FormItem {...formItemLayout} label="任务描述">
          {getFieldDecorator("TaskContent")(<TextArea rows={4} disabled />)}
        </FormItem>
        <FormItem {...formItemLayout} label="任务计划">
          {obj.Steps &&
            obj.Steps.map((o, i) => {
              return (
                <div key={i}>
                  <div>{o.StepName}</div>
                  <div>{o.StepContent}</div>
                </div>
              );
            })}
        </FormItem>
        <FormItem {...formItemLayout} label="附件">
          {obj.Attachments &&
            obj.Attachments.map((o, i) => {
              return (
                <div key={i}>
                  <a href={o.AttachmentUrl}>{o.AttachmentName}</a>
                </div>
              );
            })}
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="审核结果"
          style={{ display: audit == "1" ? null : "none" }}
        >
          {getFieldDecorator("Status")(
            <Select>
              <Option value="0">同意</Option>
              <Option value="1">拒绝</Option>
            </Select>
          )}
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="审核结果描述"
          style={{ display: audit == "1" ? null : "none" }}
        >
          {getFieldDecorator("HtmlDesc")(<TextArea rows={4} />)}
        </FormItem>
        <FormItem style={{ display: audit == "1" ? null : "none" }}>
          <Row gutter={24}>
            <Col span={2} offset={4}>
              <Button
                htmlType="submit"
                className="btn btn--primary"
                type="primary"
              >
                审核
              </Button>
            </Col>
          </Row>
        </FormItem>
      </Form>
    );
  }
}

const FormMap = Form.create({
  mapPropsToFields: props => {
    const { obj } = props;
    console.log("详情数据回显 - ", obj);
    return {
      TaskId: Form.createFormField({
        value: obj.TaskId
      }),
      TaskTitle: Form.createFormField({
        value: obj.TaskTitle
      }),
      FromUser: Form.createFormField({
        value: obj.FromUser
      }),
      ResponsibleUserName: Form.createFormField({
        value: obj.ResponsibleUserName
      }),
      CreateTime: Form.createFormField({
        value: obj.CreateTime
          ? new Date(obj.CreateTime).format("yyyy-MM-dd")
          : ""
      }),
      TaskContent: Form.createFormField({
        value: obj.TaskContent
      })
    };
  }
})(Base);
class PublicTaskDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      obj: {}
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿详情
    }
  }

  // 根据id查询详情
  getInfo = async TaskId => {
    try {
      const { info } = await getInfoById({ TaskId });
      this.setState({ obj: info });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  render() {
    let { audit } = this.props.params;
    return (
      <Nav>
        <FormMap obj={this.state.obj} audit={audit} />
      </Nav>
    );
  }
}

export default PublicTaskDetail;
