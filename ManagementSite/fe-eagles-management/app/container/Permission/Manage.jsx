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
import {
  getList,
  getPageList,
  createOrEdit
} from "../../services/authGroupService";
import Nav from "../Nav";
import "./style.less";
import { pageCodeMap } from "../../constants/config/appconfig";

const confirm = Modal.confirm;
class SearchForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      permissionList: [], // 列表数组
      pageList: [], // 列表数组
      checkedValues: []
    };
  }
  handleSubmit = e => {
    e.preventDefault();
    const view = this;
    this.props.form.validateFields(async (err, values) => {
      console.log("Received values of form: ", values);
      if (values.AuthorityGroupId) {
        const checked = view.state.checkedValues;
        let newKey = [];
        checked.map(v => {
          newKey.push({
            FunCode: v,
            EditTime: new Date(),
            CreateTime: ""
          });
        });
        const param = {
          GroupId: values.AuthorityGroupId,
          AuthorityInfo: newKey,
          OperId: 0
        };
        let { Code } = await createOrEdit(param);
        if (Code === "00") {
          message.success("保存成功");
        } else {
          message.error("保存失败");
        }
      } else {
        message.error("请选择权限组");
      }
    });
  };

  handleReset = () => {
    this.props.form.resetFields();
  };
  onChange(checkedValues) {
    console.log("checked = ", this, checkedValues);
    this.state.checkedValues = checkedValues;
  }
  changeGroup(value) {
    console.log("checked = ", value);
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
    const pageCodeArr = [...pageCodeMap];
    console.log(pageList);
    let newPageList = ["actv0001"];
    pageList.map((o, i) => {
      newPageList.push(o.FunCode);
    });
    return (
      <Form onSubmit={this.handleSubmit}>
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
          {pageList.length ? (
            <Checkbox.Group
              style={{ width: "100%" }}
              onChange={this.onChange.bind(this)}
              defaultValue={newPageList}
              key={1}
            >
              <Row>
                {pageCodeArr.map((o, i) => {
                  return (
                    <Col key={i} span={5} style={{ paddingBottom: "16px" }}>
                      <Checkbox
                        value={o[0]}
                        checked={
                          pageList.findIndex(v => v.FunCode == o[0]) > -1
                            ? true
                            : false
                        }
                      >
                        {o[1]}
                      </Checkbox>
                    </Col>
                  );
                })}
              </Row>
            </Checkbox.Group>
          ) : (
            <Checkbox.Group
              style={{ width: "100%" }}
              onChange={this.onChange.bind(this)}
              key={2}
            >
              <Row>
                {pageCodeArr.map((o, i) => {
                  return (
                    <Col key={i} span={5} style={{ paddingBottom: "16px" }}>
                      <Checkbox value={o[0]}>{o[1]}</Checkbox>
                    </Col>
                  );
                })}
              </Row>
            </Checkbox.Group>
          )}
        </FormItem>
        <Row type="flex" gutter={24}>
          <Col offset={4}>
            <Button
              htmlType="submit"
              className="btn btn--primary"
              type="primary"
            >
              保存
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
