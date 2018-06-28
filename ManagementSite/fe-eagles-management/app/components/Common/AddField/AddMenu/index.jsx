import { Form, Input, Icon, Button, Select } from "antd";
import React, { Component } from "react";
const FormItem = Form.Item;
const Option = Select.Option;
import "./style.less";

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
    const { isTwo } = this.props;
    const formItemLayout = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 4 }
      },
      wrapperCol: {
        xs: { span: 8 },
        sm: { span: 8 }
      }
    };

    getFieldDecorator("keys", { initialValue: [] });
    const keys = getFieldValue("keys");
    const formItems = keys.map((k, index) => {
      return (
        <div
          key={`wrapper${k}`}
          style={{ borderBottom: "1px solid #d9d9d9", marginBottom: "16px" }}
        >
          {isTwo ? (
            <FormItem {...formItemLayout} label="所属一级菜单">
              {getFieldDecorator("exType")(
                <Select style={{ width: "80%" }}>
                  <Option value="0">一级菜单一</Option>
                  <Option value="1">一级菜单二</Option>
                  <Option value="2">一级菜单三</Option>
                </Select>
              )}
            </FormItem>
          ) : null}

          <FormItem
            {...formItemLayout}
            label="菜单名称"
            required={false}
            key={`name${k}`}
          >
            {getFieldDecorator(`names[${k}]`)(
              <Input
                placeholder="passenger name"
                style={{ width: "80%", marginRight: 16 }}
              />
            )}
          </FormItem>
          <FormItem
            {...formItemLayout}
            label="菜单链接"
            required={false}
            key={`link${k}`}
          >
            {getFieldDecorator(`names[${k}]`)(
              <Input
                placeholder="passenger name"
                style={{ width: "80%", marginRight: 16 }}
              />
            )}
            <Icon
              className="dynamic-delete-button"
              type="minus-circle-o"
              onClick={() => this.remove(k)}
            />
          </FormItem>
        </div>
      );
    });
    return (
      <Form>
        {formItems}
        <FormItem
          {...formItemLayout}
          label={isTwo ? "添加二级菜单" : "添加一级菜单"}
        >
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

const DynamicMenuSet = Form.create()(DynamicFieldSet);

export default DynamicMenuSet;
