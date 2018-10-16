import { Form, Input, Icon, Button, Checkbox, Row, Col, message } from "antd";
import React, { Component } from "react";
const FormItem = Form.Item;
import "./style.less";
import AddImage from "../AddImage";

let uuid = 0;
class DynamicFieldSet extends React.Component {
  remove = k => {
    const { form } = this.props;
    const keys = form.getFieldValue("keys");
    const keys2 = keys.filter(key => key !== k); // 剩下的数据
    form.setFieldsValue({
      keys: keys2
    });
    const { setOptionList, obj } = this.props;
    setOptionList(keys2, obj);
  };

  add = () => {
    const { form } = this.props;
    const keys = form.getFieldValue("keys");
    const nextKeys = keys.concat([
      {
        OptionId: "",
        OptionName: "",
        IsRight: "1",
        AnswerType: "50",
        Img: ""
      }
    ]);
    form.setFieldsValue({
      keys: nextKeys
    });
    const { setOptionList, obj } = this.props;
    setOptionList(nextKeys, obj);
  };
  changeInput(index, e) {
    const { OptionList, setOptionList, obj } = this.props;
    console.log(index, e.target.value);
    OptionList.map((obj, i) => {
      if (i == index) {
        obj.OptionName = e.target.value;
      }
    });
    setOptionList(OptionList, obj);
  }
  changeAnswer(index, e) {
    console.log(index, e.target.checked);
    const { OptionList, setOptionList, obj } = this.props;
    let IsRightCount = 0;
    let tipCount = 0;
    OptionList.map((k, i) => {
      if (k.IsRight == "0") {
        ++IsRightCount; // 正确答案个数
      }
    });
    OptionList.map((k, i) => {
      
      if (obj.Multiple == "0") {
        // 单选，只能选一个
        if (i == index) {
          k.IsRight = e.target.checked ? "0" : "1";
        } 
        else {
          k.IsRight =  "1";
        }
      } else {
        // 多选，根据多选数量判定
        
        //多选至少2个
        if(obj.MultipleCount==0){
          obj.MultipleCount=2;
        }
        
        if (e.target.checked && IsRightCount >= obj.MultipleCount) {
          if (!tipCount) {
            ++tipCount;
            message.error("请根据多选数量来选择");
            return;
          }
        } else {
          if (i == index) {
            k.IsRight = e.target.checked ? "0" : "1";
          }
        }
      }
    });
    setOptionList(OptionList, obj);
  }
  changeCustom(index, e) {
    console.log(index, e.target.checked);
    const { OptionList, setOptionList, obj } = this.props;
    OptionList.map((obj, i) => {
      if (i == index) {
        obj.AnswerType = e.target.checked ? "1" : "0";
      }
    });
    setOptionList(OptionList, obj);
  }
  changeImg(index, Img) {
    console.log(index, Img);
    const { OptionList, setOptionList, obj } = this.props;
    OptionList.map((obj, i) => {
      if (i == index) {
        obj.Img = Img;
      }
    });
    setOptionList(OptionList, obj);
  }
  render() {
    const { getFieldDecorator, getFieldValue } = this.props.form;
    const { obj, OptionList } = this.props;
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
        {
          keys.map((k, index) => {
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
                        display: obj.IsVote ? "none" : null
                      }}
                      onChange={this.changeAnswer.bind(this, index)}
                      checked={k.IsRight == "0" ? true : false}
                    >
                      是否正确答案
                    </Checkbox>
                    <Checkbox
                      style={{
                        display: obj.IsVote ? "none" : null
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
