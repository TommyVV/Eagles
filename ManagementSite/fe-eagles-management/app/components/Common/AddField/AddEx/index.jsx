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
  changeInput(index, e) {
    const { OptionList, setOptionList, IsVote } = this.props;
    console.log(index, e.target.value);
    OptionList.map((obj, i) => {
      if (i == index) {
        obj.OptionName = e.target.value;
      }
    });
    setOptionList(OptionList, IsVote);
  }
  changeAnswer(index, e) {
    console.log(index, e.target.checked);
    const { OptionList, setOptionList, IsVote } = this.props;
    OptionList.map((obj, i) => {
      if (i == index) {
        obj.IsRight = e.target.checked ? "0" : "1";
      }
    });
    setOptionList(OptionList, IsVote);
  }
  changeCustom(index, e) {
    console.log(index, e.target.checked);
    const { OptionList, setOptionList, IsVote } = this.props;
    OptionList.map((obj, i) => {
      if (i == index) {
        obj.AnswerType = e.target.checked ? "1" : "0";
      }
    });
    setOptionList(OptionList, IsVote);
  }
  changeImg(index, Img) {
    console.log(index, Img);
    const { OptionList, setOptionList, IsVote } = this.props;
    OptionList.map((obj, i) => {
      if (i == index) {
        obj.Img = Img;
      }
    });
    setOptionList(OptionList, IsVote);
  }
  render() {
    const { getFieldDecorator, getFieldValue } = this.props.form;
    const { IsVote, OptionList } = this.props;
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

    getFieldDecorator("keys", { initialValue: OptionList });
    const keys = getFieldValue("keys");
    return (
      <div>
        {keys.map((k, index) => {
          let itemIndex = index + 1;
          return (
            <FormItem
              {...formItemLayout}
              label={`选项${itemIndex}`}
              required={false}
              key={index}
            >
              {getFieldDecorator(`names[${itemIndex}]`)(
                <span>
                  <Input
                    placeholder="请输入选项"
                    style={{ width: "30%", marginRight: 8 }}
                    onBlur={this.changeInput.bind(this, index)}
                    defaultValue={k.OptionName}
                  />
                  <Checkbox
                    style={{
                      display: IsVote ? "none" : null
                    }}
                    onChange={this.changeAnswer.bind(this, index)}
                    checked={k.IsRight == "0" ? true : false}
                  >
                    是否正确答案
                  </Checkbox>
                  <Checkbox
                    style={{
                      display: IsVote ? "none" : null
                    }}
                    onChange={this.changeCustom.bind(this, index)}
                    checked={k.AnswerType == "1" ? true : false}
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
              <AddImage
                Img={k.Img}
                changeImg={this.changeImg.bind(this, index)}
              />
            </FormItem>
          );
        })}
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
