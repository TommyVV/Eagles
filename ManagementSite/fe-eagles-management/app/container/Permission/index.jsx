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
  Cascader
} from "antd";
const FormItem = Form.Item;
const Option = Select.Option;
import { hashHistory } from "react-router";
import { getList, del } from "../../services/authGroupService";
import { getOrgList } from "../../services/orgService";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;
class SearchForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      List: []
    };
  }
  componentWillMount() {
    this.getCurrentList();
  }
  // 加载当前页
  getCurrentList = async () => {
    try {
      let { List } = await getOrgList({
        PageNumber: 1,
        PageSize: 10000
      });
      console.log("List - ", List);
      List.forEach((v, i) => {
        v.key = i;
      });
      this.setState({ List });
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  handleSearch = e => {
    e.preventDefault();
    const view = this;
    this.props.form.validateFields((err, values) => {
      console.log("Received values of form: ", values);
      let params = {
        PageNumber: 1,
        PageSize: 10,
        ...values
      };
      const getCurrentList = view.props.getCurrentList;
      getCurrentList(params);
      const { setObj } = this.props;
      setObj(values);
    });
  };

  handleReset = () => {
    this.props.form.resetFields();
  };

  render() {
    const { getFieldDecorator } = this.props.form;
    const { List } = this.state;
    return (
      <Form
        className="ant-advanced-search-form"
        layout="inline"
        onSubmit={this.handleSearch.bind(this)}
      >
        <Row gutter={24}>
          <Col span={5} key={6}>
            <FormItem label="机构">
              {getFieldDecorator(`OrgId`)(
                <Select>
                  <Option value="0">全部</Option>
                  {List.map((o, i) => {
                    return (
                      <Option key={i} value={o.OrgId}>
                        {o.OrgName}
                      </Option>
                    );
                  })}
                </Select>
              )}
            </FormItem>
          </Col>
          <Col span={6} key={2}>
            <FormItem label="权限组名称">
              {getFieldDecorator(`AuthorityGroupName`)(<Input />)}
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
    console.log(this);
    console.log(props);
    return {
      AuthorityGroupName: Form.createFormField({
        value: props.obj.AuthorityGroupName
      }),
      OrgId: Form.createFormField({
        value: props.obj.OrgId ? props.obj.OrgId : "0"
      })
    };
  }
})(SearchForm);
class PermissionList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      operatorList: [], // 列表数组
      obj: {}
    };
    this.columns = [
      {
        title: "权限组名称",
        dataIndex: "AuthorityGroupName"
      },
      {
        title: "添加时间",
        dataIndex: "CreateTime",
        render: text => <span>{new Date(text).format("yyyy-MM-dd")}</span>
      },
      {
        title: "权限组所属机构",
        dataIndex: "OrgId"
      },
      {
        title: "状态"
        // dataIndex: "state"
      },

      {
        title: "操作",
        render: obj => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(
                    `/permission/detail/${obj.AuthorityGroupId}`
                  )
                }
              >
                编辑
              </a>
              <a
                onClick={() => this.handleDelete(obj.AuthorityGroupId)}
                style={{ paddingLeft: "24px" }}
              >
                删除
              </a>
            </div>
          );
        }
      }
    ];

    this.getListConfig = {
      PageNumber: 1,
      PageSize: 10
    };
  }
  componentWillMount() {
    this.getCurrentList(this.getListConfig);
  }

  // 选择分享时触发的改变
  onSelectChange = selectedRowKeys => {
    this.setState({ selectedRowKeys });
  };

  // 加载当前页
  getCurrentList = async params => {
    const { PageNumber } = params;
    try {
      let { List, TotalCount } = await getList(params);
      console.log("List - ", List);
      List.forEach((v, i) => {
        v.key = i;
      });
      this.setState({ List, current: PageNumber });
      this.updatePageConfig(TotalCount);
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  // 更新分页配置
  updatePageConfig(totalSize) {
    let pageConfig = {
      total: totalSize,
      pageSize: this.getListConfig.PageSize,
      current: this.state.current,
      onChange: async (page, pagesize) => {
        this.getCurrentList({
          ...this.getListConfig,
          PageNumber: page
        });
      }
    };
    this.setState({ pageConfig });
  }
  // 删除项目
  handleDelete = async AuthorityGroupId => {
    confirm({
      title: "是否确认删除?",
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code } = await del({
            AuthorityGroupId
          });
          if (Code === "00") {
            message.success("删除成功");
            await this.getCurrentList({
              ...this.getListConfig,
              PageNumber: this.state.current
              // keyword: this.state.keyword
            });
            this.setState({ selectedRowKeys: [] });
          } else {
            message.error("删除失败");
          }
        } catch (e) {
          throw new Error(e);
        }
      }
    });
  };
  changeSearch = obj => {
    this.setState({
      obj
    });
  };
  render() {
    const { selectedRowKeys, pageConfig, List, obj } = this.state;
    const formItemLayout = {
      labelCol: {
        xl: { span: 3 }
      },
      wrapperCol: {
        xl: { span: 10 }
      }
    };
    return (
      <Nav>
        <WrapperSearchForm
          getCurrentList={this.getCurrentList.bind(this)}
          obj={obj}
          setObj={this.changeSearch.bind(this)}
        />
        <Table
          dataSource={List}
          columns={this.columns}
          pagination={pageConfig}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />
        <Row type="flex" gutter={24}>
          <Col>
            <Button
              className="btn btn--primary"
              onClick={() => hashHistory.replace(`/permission/detail`)}
            >
              新增
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default PermissionList;
