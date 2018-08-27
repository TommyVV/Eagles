import React, { Component } from "react";
import { Button, Input, Form, message, Row, Col, Select } from "antd";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import { getInfoById, createOrEdit } from "../../services/operatorService";
import { getList } from "../../services/authGroupService";
import { getList as getBranchList } from "../../services/branchService";
import "./style.less";

const FormItem = Form.Item;
const Option = Select.Option;
class Base extends Component {
  constructor(props) {
    super(props);
    this.state = {
      branchList: []
    };
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
              ...values,
              IsBranch: false
            }
          };
          let { Code } = await createOrEdit(params);
          if (Code === "00") {
            let tip = this.props.operator.OperId ? "保存成功" : "创建成功";
            message.success(tip);
            hashHistory.replace("/orgoperatorlist");
          } else {
            let tip = this.props.operator.OperId ? "保存失败" : "创建失败";
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
  change(value) {
    const { setObj, operator } = this.props;
    setObj({
      ...operator,
      IsBranch: value == "0" ? false : true
    });
  }

  render() {
    const { getFieldDecorator } = this.props.form;
    const { authList, operator, branchList } = this.props;
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
        <FormItem {...formItemLayout} label="昵称">
          {getFieldDecorator("Nickname")(
            <Input placeholder="必填，请输入昵称" />
          )}
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
                return (
                  <Option value={obj.AuthorityGroupId} key={index}>
                    {obj.AuthorityGroupName}
                  </Option>
                );
              })}
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
                onClick={() => hashHistory.replace("/orgoperatorlist")}
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
      Nickname: Form.createFormField({
        value: operator.Nickname
      }),
      AuthorityGroupId: Form.createFormField({
        value: operator.AuthorityGroupId ? operator.AuthorityGroupId : ""
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
      operator: {},
      branchList: []
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿详情
    } else {
      this.setAuthList(); // 拿权限组列表
    }
  }
  setAuthList = async () => {
    await this.getAuthList();
    await this.getBranchList(); // 拿支部列表
    const operator = {
      AuthorityGroupId: this.state.authList[0] ? this.state.authList[0].AuthorityGroupId : "",     
    };
    this.setState({ operator });
  };

  // 根据id查询详情
  getInfo = async OperId => {
    try {
      await this.getAuthList(); // 拿权限组列表
      await this.getBranchList(); // 拿支部列表
      const { Info } = await getInfoById({ OperId });
      this.setState({ operator: Info });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  // 拿权限组列表
  getAuthList = async () => {
    try {
      const { List } = await getList({
        PageNumber: 1,
        PageSize: 1000000
      });
      this.setState({ authList: List });
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  // 拿支部列表
  getBranchList = async () => {
    try {
      const { List } = await getBranchList({
        PageNumber: 1,
        PageSize: 1000000
      });
      this.setState({ branchList: List });
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  changeObj(operator) {
    this.setState({
      operator
    });
  }
  render() {
    return (
      <Nav>
        <FormMap
          operator={this.state.operator}
          authList={this.state.authList}
          branchList={this.state.branchList}
          setObj={this.changeObj.bind(this)}
        />
      </Nav>
    );
  }
}

export default OperatorDetail;
