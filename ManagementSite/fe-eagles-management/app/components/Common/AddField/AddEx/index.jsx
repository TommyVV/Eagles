import { Form, Input, Icon, Button, Checkbox  } from "antd";
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
    const formItemLayout = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 4 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 20 }
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
                placeholder="passenger name"
                style={{ width: "40%", marginRight: 8 }}
              />
               <Checkbox >是否正确答案</Checkbox>
               <Checkbox >是否允许自定义</Checkbox>
            </span>
          )}
          <Icon
            className="dynamic-delete-button"
            type="minus-circle-o"
            onClick={() => this.remove(k)}
          />
          <AddImage/>
        </FormItem>
      );
    });
    return (
      <Form>
        {formItems}
        <FormItem {...formItemLayout} label="添加选项">
          <Button
            type="dashed"
            onClick={this.add}
            style={{ width: "30%", textAlign: "center" }}
          >
            <Icon type="plus" /> 添加
          </Button>
        </FormItem>
      </Form>
    );
  }
}

const WrappedDynamicFieldSet = Form.create()(DynamicFieldSet);

export default WrappedDynamicFieldSet;
