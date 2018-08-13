import React, { Component } from "react";
import { Button, Input, Form, message, Row, Col, Select } from "antd";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import { getInfoById, createOrEdit } from "../../services/branchService";
import "./style.less";
const TextArea = Input.TextArea;

const FormItem = Form.Item;
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
          const { branch } = this.props;
          let params = {
            Info: {
              ...branch,
              ...values
            }
          };
          let { Code } = await createOrEdit(params);
          if (Code === "00") {
            let tip = branch.BranchId
              ? "保存成功"
              : "创建成功";
            message.success(tip);
            hashHistory.replace("/branchlist");
          } else {
            let tip = branch.BranchId
              ? "保存失败"
              : "创建失败";
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
    const { branch } = this.props;
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
          {getFieldDecorator("BranchId")(<Input />)}
        </FormItem>
        <FormItem {...formItemLayout} label="支部名称">
          {getFieldDecorator("BranchName", {
            rules: [
              {
                required: true,
                message: "必填，请输入支部名称"
              }
            ]
          })(<Input placeholder="必填，请输入支部名称" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="支部描述">
          {getFieldDecorator("BranchDesc")(
            <TextArea placeholder="必填，请输入支部描述" rows={4} />
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="书记">
          {getFieldDecorator("Secretary")(<Input placeholder="请输入书记姓名" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="副书记">
          {getFieldDecorator("ViceSecretary")(<Input placeholder="请输入副书记姓名" />)}
        </FormItem>
        <FormItem>
          <Row gutter={24}>
            <Col span={2} offset={4}>
              <Button
                htmlType="submit"
                className="btn btn--primary"
                type="primary"
              >
                {!branch.BranchId ? "新建" : "保存"}
              </Button>
            </Col>
            <Col span={2} offset={1}>
              <Button
                className="btn"
                onClick={() => hashHistory.replace("/branchlist")}
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
    const { branch } = props;
    console.log("详情数据回显 - ", branch);
    return {
      BranchId: Form.createFormField({
        value: branch.BranchId
      }),
      BranchName: Form.createFormField({
        value: branch.BranchName
      }),
      Secretary: Form.createFormField({
        value: branch.Secretary
      }),
      ViceSecretary: Form.createFormField({
        value: branch.ViceSecretary
      }),
      BranchDesc: Form.createFormField({
        value: branch.BranchDesc
      })
    };
  }
})(Base);
class BranchDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      branch: {}
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿详情
    } else {
      // this.getAreaList(); // 改成拿权限组列表 todo
    }
  }

  // 根据id查询详情
  getInfo = async BranchId => {
    try {
      const { Info } = await getInfoById({ BranchId });
      this.setState({ branch: Info });
      // this.getAreaList(); // 改成拿权限组列表 todo
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  render() {
    return (
      <Nav>
        <FormMap branch={this.state.branch} />
      </Nav>
    );
  }
}

export default BranchDetail;
