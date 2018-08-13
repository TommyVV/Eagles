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
  manageCreateOrEdit
} from "../../services/authGroupService";
import Nav from "../Nav";
import "./style.less";
import { pageCodeMap } from "../../constants/config/appconfig";

const confirm = Modal.confirm;
class SearchForm extends Component {

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
        let { Code } = await manageCreateOrEdit(param);
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
      this.setState({ pageList: [] });
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
      if (List.length) {
        this.changeGroup(List[0].AuthorityGroupId);
      }
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };

  render() {
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
    // const auth = JSON.parse(localStorage.info).authList;
    const pageCodeArr = [...pageCodeMap];
    console.log(pageList);
    let newPageList = [];
    pageList.map((o, i) => {
      newPageList.push(o.FunCode);
    });
    return (
      <Nav>
        <Form onSubmit={this.handleSubmit}>
          <FormItem {...formItemLayout} label="选择权限组">
            {permissionList.length ? <Select onChange={this.changeGroup.bind(this)} defaultValue={permissionList[0].AuthorityGroupId}>
              {permissionList.map((o, i) => {
                return (
                  <Option key={i} value={o.AuthorityGroupId}>
                    {o.AuthorityGroupName}
                  </Option>
                );
              })}
            </Select> : null}

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
      </Nav>
    );
  }
}

export default PermissionManage;
