import React, { Component } from "react";
import { Button, Input, Form, message, Row, Col, Select } from "antd";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import { getInfoById, createOrEdit } from "../../services/authGroupService";
import { getOrgList } from "../../services/orgService";
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
          const { obj } = this.props;
          let params = {
            Info: {
              ...obj,
              ...values
            }
          };
          let { Code } = await createOrEdit(params);
          if (Code === "00") {
            let tip = obj.AuthorityGroupId ? "保存成功" : "创建成功";
            message.success(tip);
            hashHistory.replace("/permissionlist");
          } else {
            let tip = obj.AuthorityGroupId ? "保存失败" : "创建失败";
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
    const { orgList, obj } = this.props;
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
          {getFieldDecorator("AuthorityGroupId")(<Input />)}
        </FormItem>
        <FormItem {...formItemLayout} label="权限组名称">
          {getFieldDecorator("AuthorityGroupName", {
            rules: [
              {
                required: true,
                message: "必填，请输入权限组名称"
              }
            ]
          })(<Input placeholder="必填，请输入权限组名称" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="所属机构">
          {getFieldDecorator("OrgId")(
            <Select>
              {orgList.map((obj, index) => {
                return (
                  <Option value={obj.OrgId} key={index}>
                    {obj.OrgName}
                  </Option>
                );
              })}
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayout} label="状态">
          {getFieldDecorator("Status")(
            <Select>
              <Option value="0">正常</Option>
              <Option value="1">禁用</Option>
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
                {!obj.AuthorityGroupId ? "新建" : "保存"}
              </Button>
            </Col>
            <Col span={2} offset={1}>
              <Button
                className="btn"
                onClick={() => hashHistory.replace("/permissionlist")}
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
    const { obj } = props;
    console.log("详情数据回显 - ", obj);
    return {
      AuthorityGroupId: Form.createFormField({
        value: obj.AuthorityGroupId
      }),
      AuthorityGroupName: Form.createFormField({
        value: obj.AuthorityGroupName
      }),
      OrgId: Form.createFormField({
        value: obj.OrgId ? obj.OrgId : ""
      }),
      Status: Form.createFormField({ value: obj.Status ? obj.Status : "0" })
    };
  }
})(Base);
class OperatorDetail extends Component {
  constructor(props) {
    super(props);
    this.state = {
      orgList: [],
      obj: {}
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿详情
    } else {
      this.getOrgList(); // 拿权限组列表
    }
  }

  // 根据id查询详情
  getInfo = async AuthorityGroupId => {
    try {
      await this.getOrgList(); // 拿权限组列表
      const { Info } = await getInfoById({ AuthorityGroupId });
      this.setState({ obj: Info });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  // 拿机构列表
  getOrgList = async () => {
    try {
      const { List } = await getOrgList({
        PageNumber: 1,
        PageSize: 1000000
      });
      this.setState({ orgList: List });
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };

  render() {
    return (
      <Nav>
        <FormMap obj={this.state.obj} orgList={this.state.orgList} />
      </Nav>
    );
  }
}

export default OperatorDetail;
