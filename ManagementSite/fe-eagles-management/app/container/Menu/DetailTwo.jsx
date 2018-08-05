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
import { getNextList, getList, createOrEdit } from "../../services/menuService";
import "./style.less";

const FormItem = Form.Item;
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
    const { isTwo, List, menuList } = this.props;
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

    getFieldDecorator("keys", { initialValue: List });
    const keys = getFieldValue("keys");
    const formItems = keys.map((k, index) => {
      console.log(k, menuList);
      return (
        <div
          key={`wrapper${k}`}
          style={{ borderBottom: "1px solid #d9d9d9", marginBottom: "16px" }}
        >
          {isTwo ? (
            <FormItem {...formItemLayout} label="所属一级菜单">
              <Select style={{ width: "80%" }}>
                {menuList.map((o, i) => {
                  return (
                    <Option key={i} value={o.MenuId}>
                      {o.MenuName}
                    </Option>
                  );
                })}
              </Select>
            </FormItem>
          ) : null}

          <FormItem
            {...formItemLayout}
            label="菜单名称"
            required={true}
            key={`name${k}`}
          >
            {getFieldDecorator(`MenuName`)(
              <Input
                placeholder="请输入二级菜单名称"
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
            {getFieldDecorator(`MenuLink`)(
              <Input
                placeholder="请输入二级菜单链接"
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
class MenuDetailTwo extends Component {
  constructor(props) {
    super(props);
    this.state = {
      List: [], //二级菜单列表
      menuList: [] // 一级菜单列表
    };
  }

  componentWillMount() {
    let { id } = this.props.params;
    this.getInfo(id);
  }
  // 根据id查询下级菜单列表
  getInfo = async MenuId => {
    try {
      await this.getMenuList();
      const { List } = await getNextList({ MenuId });
      this.setState({ List });
    } catch (e) {
      message.error("获取详情失败");
      throw new Error(e);
    }
  };

  getMenuList = async () => {
    try {
      const { List } = await getList();
      this.setState({ menuList: List });
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  render() {
    const { List, menuList } = this.state;
    let { id } = this.props.params;
    return (
      <Nav>
        {/* isTwo 是否维护二级菜单 */}
        <DynamicMenuSet isTwo={true} List={List} menuList={menuList} />
        <Row type="flex" justify="flexStart" className="edit" gutter={24}>
          <Col offset={4}>
            <Button
              htmlType="submit"
              className="btn btn--primary"
              type="primary"
            >
              {id ? "保存" : "新建"}
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
      </Nav>
    );
  }
}

export default MenuDetailTwo;
