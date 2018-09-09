import React, { Component } from "react";
import {
  Row,
  Col,
  message,
  Modal,
  Form,
  Select,
  Input
} from "antd";
const FormItem = Form.Item;
const Option = Select.Option;
const TextArea = Input.TextArea;
import { bitchAudit } from "../../../services/auditService";
import { hashHistory } from "react-router";

// 审核的表单
const WrapperAuditForm = Form.create({
  onFieldsChange(props, changedFields) {
    props.onChange(changedFields);
  },
  mapPropsToFields: props => {
    // const project = props.project;
    return {
      AuditStatus: Form.createFormField({
        ...props.AuditStatus,
        value: props.AuditStatus.value ? props.AuditStatus.value + "" : "0"
      }),
      Reason: Form.createFormField({
        ...props.Reason,
        value: props.Reason.value
      })
    };
  }
})(props => {
  const { getFieldDecorator } = props.form;
  const formItemLayout = {
    labelCol: {
      xs: { span: 24 },
      sm: { span: 7 }
    },
    wrapperCol: {
      xs: { span: 24 },
      sm: { span: 6 }
    }
  };
  return (
    <Form className="ant-advanced-search-form">
      <Row gutter={24}>
        <Col span={20} key={1}>
          <FormItem {...formItemLayout} label="审核结果">
            {getFieldDecorator("AuditStatus")(
              <Select>
                <Option value="0">通过</Option>
                <Option value="1">不通过</Option>
              </Select>
            )}
          </FormItem>
        </Col>
      </Row>
      <Row gutter={24}>
        <Col span={20} key={2}>
          <FormItem {...formItemLayout} label="审核结果描述">
            {getFieldDecorator(`Reason`, {
              rules: [
                {
                  required: true,
                  message: "必填，请输入审核结果描述"
                }
              ]
            })(<TextArea rows={4} />)}
          </FormItem>
        </Col>
      </Row>
    </Form>
  );
});
class Audit extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      fields: {
        AuditStatus: "", //审核结果
        Reason: "" // 审核结果描述
      },
    };
  }
  componentWillMount() {
  }
  handleOk = async () => {
    try {
      const { fields } = this.state;
      let { type, selectedRowKeys, getCurrentListFn } = this.props;
      const { AuditStatus, Reason } = fields;
      if (!Reason.value) {
        message.error("请输入审核结果描述");
        return;
      }
      let params = {
        AuditStatus: AuditStatus.value,
        Reason: Reason.value,
        Type: type, // 新闻
        AuditId: selectedRowKeys,
        AuditType: 0
      };
      let { Code } = await bitchAudit(params);
      this.setState({
        visible: false,
        fields: {
          AuditStatus: "",
          Reason: ""
        }
      });
      if (Code === "00") {
        message.success("审核成功");
        getCurrentListFn();
      } else {
        message.error("审核失败");
      }
    } catch (e) {
      throw new Error(e);
    }
  };
  handleFormChange = changedFields => {
    this.setState(({ fields }) => ({
      fields: { ...fields, ...changedFields }
    }));
  };
  render() {
    let {
      fields,
    } = this.state;
    let { visible, type } = this.props;
    let text = "";
    switch (type) {
      case 1:
        text = "审核新闻";
        break;
    }
    return (
      <Modal
        title={text}
        visible={visible}
        onOk={this.handleOk}
        onCancel={() => {
          const setVisible = this.props.setVisible;
          setVisible(false);
        }}
      >
        <WrapperAuditForm {...fields} onChange={this.handleFormChange} />
      </Modal>
    );
  }
}

export default Audit;
