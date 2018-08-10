import React, { Component } from "react";
import {
  Button,
  Input,
  Form,
  message,
  Row,
  Col,
  Select,
  Avatar,
  Icon,
  DatePicker
} from "antd";
import Nav from "../Nav";
import { hashHistory } from "react-router";
import {
  getNextList,
  getInfoById,
  getList,
  createOrEdit
} from "../../services/menuService";
import "./style.less";

const Option = Select.Option;
const FormItem = Form.Item;
let uuid = 0;
class DynamicFieldSet extends React.Component {
  handleSubmit = e => {
    e.preventDefault();
    this.props.form.validateFields(async (err, values) => {
      if (!err) {
        try {
          console.log("Received values of form: ", values);
          const { nextMenuList, menu } = this.props;
          let hasParent = nextMenuList.findIndex(v => v.ParentId);
          if (hasParent > -1) {
            let params = {
              Info: nextMenuList,
              type: 0
            };
            let { Code } = await createOrEdit(params);
            if (Code === "00") {
              let tip = menu.Id ? "保存成功" : "创建成功";
              message.success(tip);
              hashHistory.replace("/menulist");
            } else {
              let tip = menu.Id ? "保存失败" : "创建失败";
              message.error(tip);
            }
          } else {
            message.error("请选择一级菜单");
          }
        } catch (e) {
          throw new Error(e);
        }
      } else {
        message.error("请检查字段是否正确");
      }
    });
  };
  remove = k => {
    const { form, menu, setNextMenuList } = this.props;
    const keys = form.getFieldValue("keys");
    const keys2 = keys.filter(key => key !== k); // 剩下的数据
    form.setFieldsValue({
      keys: keys2
    });
    setNextMenuList(keys2, menu);
  };

  add = () => {
    const { form, menu, setNextMenuList } = this.props;
    const keys = form.getFieldValue("keys");
    const nextKeys = keys.concat([
      {
        MenuId: "",
        MenuName: "",
        MenuLink: "",
        MenuLevel: "2",
        ParentId: menu.MenuId,
        OrgId: menu.OrgId,
        OrgName: menu.OrgName
      }
    ]);
    form.setFieldsValue({
      keys: nextKeys
    });
    setNextMenuList(nextKeys, menu);
  };
  changeInput(name, index, e) {
    const { List, setNextMenuList, nextMenuList, menu } = this.props;
    nextMenuList.map((obj, i) => {
      if (i == index) {
        obj[name] = e.target ? e.target.value : e;
      }
    });
    setNextMenuList(nextMenuList, menu);
  }

  render() {
    const { getFieldDecorator, getFieldValue } = this.props.form;
    const { isTwo, nextMenuList, menuList, menu } = this.props;
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

    getFieldDecorator("keys", { initialValue: nextMenuList });
    const keys = getFieldValue("keys");
    const formItems = keys.map((k, index) => {
      console.log("二级菜单：", k);
      return (
        <div
          key={`wrapper${index}`}
          style={{ borderBottom: "1px solid #d9d9d9", marginBottom: "16px" }}
        >
          <FormItem {...formItemLayout} label="所属一级菜单">
            <Select
              style={{ width: "80%" }}
              onChange={this.changeInput.bind(this, "ParentId", index)}
              defaultValue={menu.MenuId}
            >
              {menuList.map((o, i) => {
                return (
                  <Option key={i} value={o.MenuId}>
                    {o.MenuName}
                  </Option>
                );
              })}
            </Select>
          </FormItem>

          <FormItem
            {...formItemLayout}
            label="菜单名称"
            required={true}
            key={`name${index}`}
          >
            <Input
              placeholder="请输入二级菜单名称"
              style={{ width: "80%", marginRight: 16 }}
              onBlur={this.changeInput.bind(this, "MenuName", index)}
              defaultValue={k.MenuName}
            />
          </FormItem>
          <FormItem
            {...formItemLayout}
            label="菜单链接"
            required={false}
            key={`link${index}`}
          >
            <Input
              placeholder="请输入二级菜单链接"
              style={{ width: "80%", marginRight: 16 }}
              onBlur={this.changeInput.bind(this, "MenuLink", index)}
              defaultValue={k.MenuLink}
            />
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
      <Form onSubmit={this.handleSubmit}>
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
        <Row type="flex" justify="flexStart" className="edit" gutter={24}>
          <Col offset={4}>
            <Button
              htmlType="submit"
              className="btn btn--primary"
              type="primary"
            >
              {menu.MenuId ? "保存" : "新建"}
            </Button>
          </Col>
          <Col>
            <Button
              className="btn"
              onClick={() => hashHistory.replace("/menulist")}
            >
              取消
            </Button>
          </Col>
        </Row>
      </Form>
    );
  }
}

const DynamicMenuSet = Form.create()(DynamicFieldSet);
class MenuDetailTwo extends Component {
  constructor(props) {
    super(props);
    this.state = {
      nextMenuList: [], //二级菜单列表
      menuList: [], // 一级菜单列表
      menu: {} // 一级菜单详情
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    if (id) {
      this.getInfo(id);
    }else{
      this.getMenuList();
    }
  }
  // 根据id查询下级菜单列表
  getInfo = async MenuId => {
    try {
      await this.getMenuInfo({ MenuId });
      await this.getMenuList();
      const { List } = await getNextList({ MenuId });
      this.setState({ nextMenuList: List });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };
  // 获取一级菜单列表
  getMenuList = async () => {
    try {
      const { List } = await getList();
      this.setState({ menuList: List });
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  // 获取一级菜单详情
  getMenuInfo = async param => {
    try {
      const { Info } = await getInfoById(param);
      this.setState({ menu: Info });
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  setNextMenuList(nextMenuList, menu) {
    this.setState({
      nextMenuList,
      menu
    });
  }
  render() {
    const { nextMenuList, menu, menuList } = this.state;
    let { id } = this.props.params;
    return (
      <Nav>
        {/* isTwo 是否维护二级菜单 */}
        <DynamicMenuSet
          isTwo={true}
          nextMenuList={nextMenuList}
          menu={menu}
          menuList={menuList}
          setNextMenuList={this.setNextMenuList.bind(this)}
        />
      </Nav>
    );
  }
}

export default MenuDetailTwo;
