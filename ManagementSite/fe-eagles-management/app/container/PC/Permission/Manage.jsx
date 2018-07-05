import React, { Component } from "react";
import {
  Table,
  Row,
  Col,
  Button,
  message,
  Modal,
  Form,
  Input,
  Select,
  Checkbox
} from "antd";
const FormItem = Form.Item;
const Option = Select.Option;
import { hashHistory } from "react-router";
import util from "../../../utils/util";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;
class SearchForm extends Component {
  handleSearch = e => {
    e.preventDefault();
    this.props.form.validateFields((err, values) => {
      console.log("Received values of form: ", values);
    });
  };

  handleReset = () => {
    this.props.form.resetFields();
  };

  render() {
    const { getFieldDecorator } = this.props.form;
    return (
      <Form
        className="ant-advanced-search-form"
        layout="inline"
        onSubmit={this.handleSearch}
      >
        <Row gutter={24}>
          <Col span={5} key={1}>
            <FormItem label="选择机构组">
              {getFieldDecorator("title")(
                <Select>
                  <Option value="0">党组织管理组</Option>
                  <Option value="1">党支部管理组</Option>
                </Select>
              )}
            </FormItem>
          </Col>
          <Col span={5} key={5}>
            <FormItem label="权限组">
              {getFieldDecorator("title")(
                <Select>
                  <Option value="0">正常</Option>
                  <Option value="1">失效</Option>
                </Select>
              )}
              {/* {getFieldDecorator(`goods`)(<Input />)} */}
            </FormItem>
          </Col>
          <Col
            span={6}
            style={{
              textAlign: "cnter",
              paddingLeft: "7px",
              paddingTop: "3px"
            }}
          >
            <Button type="primary" htmlType="submit">
              搜索
            </Button>
            <Button style={{ marginLeft: 8 }} onClick={this.handleReset}>
              清空
            </Button>
          </Col>
        </Row>
      </Form>
    );
  }
}

const WrapperSearchForm = Form.create({
  mapPropsToFields: props => {
    // const project = props.project;
    return {
      title: Form.createFormField({
        value: "0"
      }),
      state: Form.createFormField({
        value: "0"
      })
    };
  }
})(SearchForm);
class PermissionManage extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedRowKeys: [], // 项目id数组
      projectList: [], // 项目列表数组
      keyword: "", // 关键字
      current: 1, // 当前页
      pageConfig: {} // 当前页配置
    };
  }
  componentWillMount() {
    // this.getCurrentList(this.getListConfig);
  }

  render() {
    const { selectedRowKeys, pageConfig, projectList } = this.state;
    const rowSelection = {
      selectedRowKeys,
      onChange: this.onSelectChange
    };
    const formItemLayout = {
      labelCol: {
        xl: { span: 4 }
      },
      wrapperCol: {
        xl: { span: 10 }
      }
    };
    return (
      <Nav>
        <WrapperSearchForm />
        <Form>
          <FormItem {...formItemLayout} label="权限信息">
            <Checkbox.Group style={{ width: "100%" }}>
              <Row>
                <Col span={10} style={{ paddingBottom: "16px" }}>
                  <Checkbox value="1">党员信息维护</Checkbox>
                </Col>
                <Col span={10} style={{ paddingBottom: "16px" }}>
                  <Checkbox value="2">操作管理员</Checkbox>
                </Col>
                <Col span={10} style={{ paddingBottom: "16px" }}>
                  <Checkbox value="3">页面模块管理</Checkbox>
                </Col>
                <Col span={10} style={{ paddingBottom: "16px" }}>
                  <Checkbox value="4">党员信息维护</Checkbox>
                </Col>
              </Row>
              <Row>
                <Col span={10} style={{ paddingBottom: "16px" }}>
                  <Checkbox value="5">党员信息维护</Checkbox>
                </Col>
                <Col span={10} style={{ paddingBottom: "16px" }}>
                  <Checkbox value="6">操作管理员</Checkbox>
                </Col>
                <Col span={10} style={{ paddingBottom: "16px" }}>
                  <Checkbox value="7">页面模块管理</Checkbox>
                </Col>
                <Col span={10} style={{ paddingBottom: "16px" }}>
                  <Checkbox value="8">页面模块管理</Checkbox>
                </Col>
              </Row>
            </Checkbox.Group>
          </FormItem>
        </Form>
        <Row type="flex" gutter={24}>
          <Col offset={4}>
            <Button className="btn btn--primary">
              <a onClick={() => hashHistory.replace(`/org/detail`)}>更新</a>
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default PermissionManage;
