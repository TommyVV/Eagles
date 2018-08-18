import React, { Component } from "react";
import { Button, Input, Form, message, Row, Col, Select } from "antd";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import { getInfoById } from "../../services/publicArticleService";
import { audit } from "../../services/auditService";
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
          const { obj, type } = this.props;
          const { AuditStatus, Reason, NewsId } = values;

          let params = {
            AuditStatus,
            Reason,
            Type: "6", // 文章
            AuditId: NewsId,
            AuditType: type
          };
          let { Code } = await audit(params);
          message.success("审核成功");
          hashHistory.replace(`/articlelist/${type}`);
        } catch (e) {
          message.error("审核失败");
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
          {getFieldDecorator("NewsId")(<Input />)}
        </FormItem>
        <FormItem {...formItemLayout} label="标题">
          {getFieldDecorator("NewsTitle")(<Input disabled />)}
        </FormItem>
        <FormItem {...formItemLayout} label="作者">
          {getFieldDecorator("Author")(<Input disabled />)}
        </FormItem>
        <FormItem {...formItemLayout} label="发布时间">
          {getFieldDecorator("CreateTime")(<Input disabled />)}
        </FormItem>
        <FormItem {...formItemLayout} label="文章描述">
          {getFieldDecorator("NewsDetail")(<TextArea rows={4} disabled />)}
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="审核结果"
          style={{ display: audit == "1" ? null : "none" }}
        >
          {getFieldDecorator("AuditStatus")(
            <Select>
              <Option value="0">同意</Option>
              <Option value="1">不通过</Option>
            </Select>
          )}
        </FormItem>
        <FormItem
          {...formItemLayout}
          label="审核结果描述"
          style={{ display: audit == "1" ? null : "none" }}
        >
          {getFieldDecorator("Reason")(<TextArea rows={4} />)}
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
      NewsId: Form.createFormField({
        value: obj.NewsId
      }),
      NewsTitle: Form.createFormField({
        value: obj.NewsTitle
      }),
      Author: Form.createFormField({
        value: obj.Author
      }),
      CreateTime: Form.createFormField({
        value: obj.CreateTime
          ? new Date(obj.CreateTime).format("yyyy-MM-dd")
          : ""
      }),
      NewsDetail: Form.createFormField({
        value: obj.NewsDetail
      }),
      AuditStatus: Form.createFormField({
        value: obj.AuditStatus ? obj.AuditStatus + "" : "0"
      }),
      Reason: Form.createFormField({
        value: obj.Reason
      })
    };
  }
})(Base);
class PublicArticleDetail extends Component {
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
  getInfo = async NewsId => {
    try {
      const { info } = await getInfoById({ NewsId });
      this.setState({ obj: info });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  render() {
    let { audit, type } = this.props.params;
    return (
      <Nav>
        <FormMap obj={this.state.obj} audit={audit} type={type} />
      </Nav>
    );
  }
}

export default PublicArticleDetail;
