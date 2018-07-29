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
import { getList, getPageList } from "../../services/authGroupService";
import Nav from "../Nav";
import "./style.less";
import { pageCodeMap } from "../../constants/config/appconfig";

const confirm = Modal.confirm;
class SearchForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      permissionList: [], // 列表数组
      pageList: [] // 列表数组
    };
  }
  handleSearch = e => {
    e.preventDefault();
    this.props.form.validateFields((err, values) => {
      console.log("Received values of form: ", values);
    });
  };

  handleReset = () => {
    this.props.form.resetFields();
  };
  onChange(checkedValues) {
    console.log("checked = ", checkedValues);
  }
  changeGroup(value) {
    this.getPageCode(value);
  }
  componentWillMount() {
    this.getCurrentList();
  }
  // 拿权限的页面
  getPageCode = async value => {
    try {
      let { List } = await getPageList({
        GroupId: value
      });
      console.log("List - ", List);
      this.setState({ pageList: List });
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  // 加载当前页
  getCurrentList = async () => {
    try {
      let { List } = await getList({
        PageNumber: 1,
        PageSize: 100
      });
      console.log("List - ", List);
      this.setState({ permissionList: List });
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };

  render() {
    const { getFieldDecorator } = this.props.form;
    const { permissionList, pageList } = this.state;
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
    const formItemLayoutCheck = {
      labelCol: {
        xs: { span: 24 },
        sm: { span: 4 }
      },
      wrapperCol: {
        xs: { span: 24 },
        sm: { span: 20 }
      }
    };
    return (
      <Form onSubmit={this.handleSearch}>
        <FormItem {...formItemLayout} label="选择权限组">
          {getFieldDecorator("AuthorityGroupId")(
            <Select onChange={this.changeGroup.bind(this)}>
              {permissionList.map((o, i) => {
                return (
                  <Option key={i} value={o.AuthorityGroupId}>
                    {o.AuthorityGroupName}
                  </Option>
                );
              })}
            </Select>
          )}
        </FormItem>
        <FormItem {...formItemLayoutCheck} label="权限信息">
          <Checkbox.Group style={{ width: "100%" }} onChange={this.onChange}>
            <Row>
              {pageList.map((o, i) => {
                return (
                  <Col key={i} span={5} style={{ paddingBottom: "16px" }}>
                    <Checkbox value={o.FunCode}>
                      {pageCodeMap.get(o.FunCode)}
                    </Checkbox>
                  </Col>
                );
              })}
            </Row>
          </Checkbox.Group>
        </FormItem>
        <Row type="flex" gutter={24}>
          <Col offset={4}>
            <Button className="btn btn--primary">
              <a onClick={() => hashHistory.replace(`/org/detail`)}>更新</a>
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
    return {};
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
        xl: { span: 2 }
      },
      wrapperCol: {
        xl: { span: 6 }
      }
    };
    return (
      <Nav>
        <WrapperSearchForm />
      </Nav>
    );
  }
}

export default PermissionManage;
