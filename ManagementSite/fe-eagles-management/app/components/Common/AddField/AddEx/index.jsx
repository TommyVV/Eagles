import { Form, Input, Icon, Button, Checkbox, Row, Col } from "antd";
import React, { Component } from "react";
const FormItem = Form.Item;
import "./style.less";
import AddImage from "../AddImage";

let uuid = 0;
class DynamicFieldSet extends React.Component {
  remove = k => {
    const { form } = this.props;
    // can use data-binding to get
    const keys = form.getFieldValue("keys");

    // can use data-binding to set
    form.setFieldsValue({
      keys: keys.filter(key => key !== k)
    });
  };

  add = () => {
    const { form } = this.props;
    // can use data-binding to get
    const keys = form.getFieldValue("keys");
    const nextKeys = keys.concat(uuid);
    uuid++;
    // can use data-binding to set
    // important! notify form to detect changes
    form.setFieldsValue({
      keys: nextKeys
    });
  };

  render() {
    const { getFieldDecorator, getFieldValue } = this.props.form;
    const { isVote } = this.props;
    const formItemLayout = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 4 },
        xl: { span: 2 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 20 },
        xl: { span: 20 }
      }
    };

    getFieldDecorator("keys", { initialValue: [] });
    const keys = getFieldValue("keys");
    const formItems = keys.map((k, index) => {
      return (
        <FormItem
          {...formItemLayout}
          label={`选项${++index}`}
          required={false}
          key={k}
        >
          {getFieldDecorator(`names[${k}]`)(
            <span>
              <Input
                placeholder="请输入选项"
                style={{ width: "30%", marginRight: 8 }}
              />
              <Checkbox
                style={{
                  display: isVote == "0" ? null : "none"
                }}
              >
                是否正确答案
              </Checkbox>
              <Checkbox
                style={{
                  display: isVote == "0" ? null : "none"
                }}
              >
                是否允许自定义
              </Checkbox>
            </span>
          )}
          <Icon
            className="dynamic-delete-button"
            type="minus-circle-o"
            onClick={() => this.remove(k)}
          />
          <AddImage />
        </FormItem>
      );
    });
    return (
      <div>
        {formItems}
        <Row gutter={24}>
          <Col span={4} offset={2}>
            <Button
              type="dashed"
              onClick={this.add}
              style={{ textAlign: "center" }}
            >
              <Icon type="plus" /> 添加选项
            </Button>
          </Col>
        </Row>
      </div>
    );
  }
}

const WrappedDynamicFieldSet = Form.create()(DynamicFieldSet);

export default WrappedDynamicFieldSet;
