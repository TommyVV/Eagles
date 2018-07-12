import React, { Component } from "react";
import { Button, Input, Form, message, Row, Col, Select } from "antd";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import { getInfoById, createOrEdit } from "../../services/operatorService";
// import { getAllArea } from "../../services/areaService";
import "./style.less";

const FormItem = Form.Item;
const Option = Select.Option;
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
          const { operator } = this.props;
          let params = {
            Info: {
              ...operator,
              ...values
            },
            OrgId: 10000000,
            BranchId: 10000000
          };
          let { Code } = await createOrEdit(params);
          if (Code === "00") {
            let tip = this.props.operator.OperId
              ? "保存操作员成功"
              : "创建操作员成功";
            message.success(tip);
            hashHistory.replace("/operatorlist");
          } else {
            let tip = this.props.operator.OperId
              ? "保存操作员失败"
              : "创建操作员失败";
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
    const { authList } = this.props;
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
        {/* <FormItem {...formItemLayout} label="操作员编号">
          {getFieldDecorator("OperId", {
            rules: [
              {
                required: true,
                message: "必填，请输入操作员编号"
              }
            ]
          })(<Input placeholder="必填，请输入操作员名称" />)}
        </FormItem> */}
        <FormItem {...formItemLayout} label="" style={{ display: "none" }}>
          {getFieldDecorator("OperId")(<Input />)}
        </FormItem>
        <FormItem {...formItemLayout} label="操作员名称">
          {getFieldDecorator("OperName", {
            rules: [
              {
                required: true,
                message: "必填，请输入操作员姓名"
              }
            ]
          })(<Input placeholder="必填，请输入操作员姓名" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="操作员密码">
          {getFieldDecorator("Password", {
            rules: [
              {
                required: true,
                message: "必填，请输入操作员密码"
              }
            ]
          })(<Input placeholder="必填，请输入操作员密码" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="所属权限组">
          {getFieldDecorator("AuthorityGroupId")(
            <Select>
              {authList.map((obj, index) => {
                // todo
                return (
                  <Option value={index} key={index}>
                    党组织一
                  </Option>
                );
              })}
              <Option value="0">党组织二</Option>
              <Option value="2">党组织二</Option>
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
                {!this.props.operator.OperId ? "新建" : "保存"}
              </Button>
            </Col>
            <Col span={2} offset={1}>
              <Button
                className="btn"
                onClick={() => hashHistory.replace("/operatorlist")}
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
    console.log("详情数据回显 - ", props);
    const operator = props.operator;
    return {
      OperId: Form.createFormField({
        value: operator.OperId
      }),
      OperName: Form.createFormField({
        value: operator.OperName
      }),
      AuthorityGroupId: Form.createFormField({
        value: operator.AuthorityGroupId ? operator.AuthorityGroupId + "" : "0"
      }),
      Password: Form.createFormField({
        value: operator.Password
      })
    };
  }
})(Base);
class OperatorDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      authList: [],
      operator: {}
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
  getInfo = async OperId => {
    try {
      const { Info } = await getInfoById({ OperId });
      this.setState({ operator: Info });
      // this.getAreaList(); // 改成拿权限组列表 todo
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  // 改成拿权限组列表
  // getAreaList = async () => {
  //   try {
  //     const { AreaInfos } = await getAllArea();
  //     this.setState({ AreaInfos });
  //   } catch (e) {
  //     message.error("获取失败");
  //     throw new Error(e);
  //   }
  // };
  render() {
    return (
      <Nav>
        <FormMap
          operator={this.state.operator}
          authList={this.state.authList}
        />
      </Nav>
    );
  }
}

export default OperatorDetail;
