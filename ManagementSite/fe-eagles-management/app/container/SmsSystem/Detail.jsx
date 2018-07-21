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
import { getInfoById, createOrEdit } from "../../services/systemSmsService";
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
          const { system } = this.props;
          let params = {
            SmsInfo: {
              ...system,
              ...values
            }
          };
          let { Code } = await createOrEdit(params);
          if (Code === "00") {
            let tip = system.VendorId ? "保存成功" : "创建成功";
            message.success(tip);
            hashHistory.replace("/smssystemlist");
          } else {
            let tip = system.VendorId ? "保存失败" : "创建失败";
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
  changDate(value) {
    console.log(value);
    // const { system } = this.props;
    // const { RepeatTime, NoticeTime } = values;
    // const system={

    // };
    // if (value == "0") {
    // }
  }
  render() {
    const { getFieldDecorator } = this.props.form;
    const { system } = this.props;
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
          {getFieldDecorator("VendorId")(<Input />)}
        </FormItem>
        <FormItem {...formItemLayout} label="短信提供商">
          {getFieldDecorator("VendorName")(
            <Input placeholder="请输入短信提供商" />
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="优先级">
          {getFieldDecorator("Priority")(<Input placeholder="请输入优先级" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="AppId">
          {getFieldDecorator("AppId")(<Input placeholder="请输入AppId" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="AppKey">
          {getFieldDecorator("AppKey")(<Input placeholder="请输入AppKey" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="SginKey">
          {getFieldDecorator("SginKey")(<Input placeholder="请输入SginKey" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="总数短信">
          {getFieldDecorator("MaxCount")(
            <Input placeholder="请输入总数短信" />
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="已发数量">
          {getFieldDecorator("SendCount")(
            <Input placeholder="请输入已发数量" />
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="接口地址">
          {getFieldDecorator("ServiceUrl")(
            <TextArea rows={4} placeholder="请输入接口地址" />
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="状态">
          {getFieldDecorator("Status")(
            <Select>
              <Option value="0">正常</Option>
              <Option value="1">禁止</Option>
            </Select>
          )}
        </FormItem>
        <FormItem>
          <Row gutter={24}>
            <Col span={2} offset={4}>
              <Button
                htmlType="submit"
                className="btn btn--primary"
                type="primary"
              >
                {!system.VendorId ? "新建" : "保存"}
              </Button>
            </Col>
            <Col span={2} offset={1}>
              <Button
                className="btn"
                onClick={() => hashHistory.replace("/smssystemlist")}
              >
                取消
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
    const { system } = props;
    console.log("详情数据回显 - ", system);
    return {
      VendorId: Form.createFormField({
        value: system.VendorId
      }),
      VendorName: Form.createFormField({
        value: system.VendorName
      }),
      Priority: Form.createFormField({
        value: system.Priority
      }),
      AppId: Form.createFormField({
        value: system.AppId
      }),
      AppKey: Form.createFormField({
        value: system.AppKey
      }),
      SginKey: Form.createFormField({
        value: system.SginKey
      }),
      MaxCount: Form.createFormField({
        value: system.MaxCount
      }),
      SendCount: Form.createFormField({
        value: system.SendCount
      }),
      ServiceUrl: Form.createFormField({
        value: system.ServiceUrl
      }),
      Status: Form.createFormField({
        value: system.Status ? system.Status + "" : "0"
      })
    };
  }
})(Base);
class SmsSystemDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      system: {}
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿详情
    }
  }

  // 根据id查询详情
  getInfo = async VendorId => {
    try {
      const { Info } = await getInfoById({ VendorId });
      this.setState({ system: Info });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  render() {
    return (
      <Nav>
        <FormMap system={this.state.system} />
      </Nav>
    );
  }
}

export default SmsSystemDetail;
