import React, { Component } from "react";
import { connect } from "react-redux";
import {
  Button,
  Input,
  Form,
  message,
  Row,
  Col,
  Select,
  Upload,
  Icon
} from "antd";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import { getOrgList } from "../../services/orgService";
import { getInfoById, createOrEdit } from "../../services/menuService";
import { saveOrgInfo, clearInfo } from "../../actions/orgAction";
import "./style.less";
import { debug } from "util";

const FormItem = Form.Item;
const Option = Select.Option;
@connect(
  state => {
    return {
      user: state.userReducer,
      orgReducer: state.orgReducer
    };
  },
  { saveOrgInfo, clearInfo }
)
class Base extends Component {
  constructor(props) {
    super(props);
    this.state = {
      loading: false
    };
  }
  componentWillUnmount() {
    this.props.clearInfo();
  }
  handleSubmit = e => {
    e.preventDefault();
    this.props.form.validateFields(async (err, values) => {
      if (!err) {
        try {
          console.log("Received values of form: ", values);
          const { menu, orgList } = this.props;
          let OrgName = menu.OrgName;
          orgList.map((org, i) => {
            if (org.OrgId == values.OrgId) {
              OrgName = org.OrgName;
            }
          });
          let params = {
            Info: {
              ...menu,
              ...values,
              OrgName
            }
          };
          let { Code } = await createOrEdit(params);
          if (Code === "00") {
            let tip = this.props.menu.MenuId
              ? "保存一级菜单成功"
              : "创建一级菜单成功";
            message.success(tip);
            hashHistory.replace("/menulist");
          } else {
            let tip = this.props.menu.MenuId
              ? "保存一级菜单失败"
              : "创建一级菜单失败";
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
    const { orgList } = this.props;
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
          {getFieldDecorator("MenuId")(<Input />)}
        </FormItem>
        <FormItem {...formItemLayout} label="菜单名称">
          {getFieldDecorator("MenuName", {
            rules: [
              {
                required: true,
                message: "必填，请输入菜单名称"
              }
            ]
          })(<Input placeholder="必填，请输入菜单名称" />)}
        </FormItem>
        <FormItem {...formItemLayout} label="菜单链接">
          {getFieldDecorator("MenuLink", {
            rules: [
              {
                required: true,
                message: "必填，请输入菜单链接"
              }
            ]
          })(<Input placeholder="必填，请输入菜单链接" />)}
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
        <FormItem>
          <Row gutter={24}>
            <Col span={2} offset={4}>
              <Button
                htmlType="submit"
                className="btn btn--primary"
                type="primary"
              >
                {!this.props.menu.MenuId ? "新建" : "保存"}
              </Button>
            </Col>
            <Col span={2} offset={1}>
              <Button
                className="btn"
                onClick={() => hashHistory.replace("/menulist")}
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
    console.log("机构详情数据回显 - ", props);
    const menu = props.menu;
    return {
      OrgId: Form.createFormField({
        value: menu.OrgId ? menu.OrgId : ""
      }),
      MenuId: Form.createFormField({
        value: menu.MenuId
      }),
      MenuName: Form.createFormField({
        value: menu.MenuName
      }),
      MenuLink: Form.createFormField({
        value: menu.MenuLink
      })
    };
  }
})(Base);
@connect(
  state => {
    return {
      userReducer: state.userReducer,
      orgReducer: state.orgReducer
    };
  },
  { saveOrgInfo, clearInfo }
)
class MenuDetailOne extends Component {
  constructor(props) {
    super(props);
    this.state = {
      orgList: [],
      menu: {}
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id); //拿详情
    } else {
      this.getOrgList();
    }
  }

  componentWillUnmount() {
    this.props.clearInfo();
  }
  // 根据id查询详情
  getInfo = async MenuId => {
    try {
      const { Info } = await getInfoById({ MenuId });
      this.setState({ menu: Info });
      this.getOrgList();
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  getOrgList = async () => {
    try {
      const { List } = await getOrgList();
      this.setState({ orgList: List });
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  render() {
    return (
      <Nav>
        <FormMap menu={this.state.menu} orgList={this.state.orgList} />
      </Nav>
    );
  }
}

export default MenuDetailOne;
