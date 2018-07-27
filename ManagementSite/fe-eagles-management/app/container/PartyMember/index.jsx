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
  Select
} from "antd";
const FormItem = Form.Item;
const TextArea = Input.TextArea;
const Option = Select.Option;
import { hashHistory } from "react-router";
import Nav from "../Nav";
import "./style.less";
import { getList, del } from "../../services/memberService";
import { audit } from "../../services/auditService";

const confirm = Modal.confirm;
class SearchForm extends Component {
  handleSearch = e => {
    e.preventDefault();
    const view = this;
    this.props.form.validateFields((err, values) => {
      console.log("Received values of form: ", values);
      console.log("Received values of form: ", values);
      let params = {
        ...this.props.pageConfig,
        ...values
      };
      const getCurrentList = view.props.getCurrentList;
      getCurrentList(params);
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
        onSubmit={this.handleSearch.bind(this)}
      >
        <Row gutter={24}>
          <Col span={6} key={1}>
            <FormItem label="支部名称">
              {getFieldDecorator("type")(
                <Select>
                  <Option value="0">全部</Option>
                  <Option value="1">支部</Option>
                  <Option value="2">小组</Option>
                </Select>
              )}
            </FormItem>
          </Col>
          <Col span={6} key={2}>
            <FormItem label="党员名称">
              {getFieldDecorator(`name`)(<Input />)}
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
    return {
      GoodsStatus: Form.createFormField({
        value: "0"
      })
    };
  }
})(SearchForm);

// 审核的表单
const WrapperAuditForm = Form.create({
  onFieldsChange(props, changedFields) {
    props.onChange(changedFields);
  },
  mapPropsToFields: props => {
    // const project = props.project;
    return {
      AuditStatus: Form.createFormField({
        ...props.AuditStatus,
        value: props.AuditStatus.value
      }),
      Reason: Form.createFormField({
        ...props.Reason,
        value: props.Reason.value
      })
    };
  }
})(props => {
  const { getFieldDecorator } = props.form;
  const formItemLayout = {
    labelCol: {
      xs: { span: 24 },
      sm: { span: 7 }
    },
    wrapperCol: {
      xs: { span: 24 },
      sm: { span: 6 }
    }
  };
  return (
    <Form className="ant-advanced-search-form">
      <Row gutter={24}>
        <Col span={20} key={1}>
          <FormItem {...formItemLayout} label="审核结果">
              {getFieldDecorator("AuditStatus")(
                <Select>
                  <Option value="0">通过</Option>
                  <Option value="1">拒绝</Option>
                </Select>
              )}
            </FormItem>
        </Col>
      </Row>
      <Row gutter={24}>
        <Col span={20} key={2}>
          <FormItem {...formItemLayout} label="审核结果描述">
            {getFieldDecorator(`Reason`)(<TextArea rows={4} />)}
          </FormItem>
        </Col>
      </Row>
    </Form>
  );
});

class PartyMemberList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedRowKeys: [], // 项目id数组
      memberList: [], // 项目列表数组
      current: 1, // 当前页
      pageConfig: {}, // 当前页配置
      authMap: new Map(),
      currentId: "", // 当前的id
      visible: false, // 弹出框
      fields: {
        AuditStatus: "", //审核结果
        Reason: "" // 审核结果描述
      }
    };
    this.columns = [
      {
        title: "党员名称",
        dataIndex: "UserName"
      },
      {
        title: "所属支部",
        dataIndex: "BranchName"
      },
      {
        title: "联系电话",
        dataIndex: "Phone"
      },
      {
        title: "党员类型",
        dataIndex: "MemberType",
        render: text => <span>{text}</span>
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(`/partymember/detail/${obj.UserId}`)
                }
              >
                编辑
              </a>
              <a
                onClick={() => this.handleDelete(obj.UserId)}
                style={{ paddingLeft: "24px" }}
              >
                删除
              </a>
              <a
                onClick={() =>
                  hashHistory.replace(
                    `/partymember/setnext/${obj.UserId}/${obj.UserName}`
                  )
                }
                style={{ paddingLeft: "24px" }}
              >
                设置数据权限
              </a>
              <a
                style={{
                  paddingLeft: "24px",
                  display: this.state.authMap.get("Audit001") ? null : "none"
                }}
                onClick={() =>
                  this.setState({ visible: true, currentId: obj.UserId })
                }
              >
                审核
              </a>
            </div>
          );
        }
      }
    ];

    this.getListConfig = {
      PageNumber: 1,
      PageSize: 10,
      UserName: "",
      BranchId: ""
    };
  }
  componentWillMount() {
    const auth = JSON.parse(localStorage.info).authList;
    if (auth) {
      const authMap = new Map();
      auth.map((a, i) => {
        authMap.set(a.FunCode, a.FunCode);
      });
      this.setState({
        authMap
      });
    }
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
      List.forEach(v => {
        v.key = v.UserId;
      });
      this.setState({ memberList: List, current: PageNumber });
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
      PageSize: this.getListConfig.PageSize,
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
  // 删除
  handleDelete = async UserId => {
    confirm({
      title: `是否确认删除?`,
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code } = await del({ UserId });
          if (Code === "00") {
            message.success(`删除成功`);
            await this.getCurrentList({
              ...this.getListConfig,
              PageNumber: this.state.current
              // keyword: this.state.keyword
            });
            this.setState({ selectedRowKeys: [] });
          } else {
            message.error(`删除失败`);
          }
        } catch (e) {
          throw new Error(e);
        }
      }
    });
  };
  // 批量操作
  handleEdit = async () => {
    try {
      let { selectedRowKeys } = this.state;
      console.log(selectedRowKeys);
      if (selectedRowKeys.length > 1) {
        return message.error("不能同时编辑多个项目");
      }
      if (selectedRowKeys.length === 0) {
        return message.error("请选择需要编辑的项目");
      }
      hashHistory.replace(`/project/create/${selectedRowKeys[0]}`);
    } catch (e) {
      throw new Error(e);
    }
  };
  handleOk = async () => {
    try {
      const { currentId, fields } = this.state;
      const { AuditStatus, Reason } = fields;
      let params = {
        AuditStatus: AuditStatus.value,
        Reason: Reason.value,
        Type: "2", // 党员
        AuditId: currentId,
        AuditType: 0
      };
      let { Code } = await audit(params);
      this.setState({
        visible: false,
        fields: {
          AuditStatus: "",
          Reason: ""
        }
      });
      if (Code === "00") {
        message.success("审核成功");
        await this.getCurrentList({
          ...this.getListConfig,
          PageNumber: this.state.current
        });
      } else {
        message.error("审核失败");
      }
    } catch (e) {
      throw new Error(e);
    }
  };
  handleFormChange = changedFields => {
    this.setState(({ fields }) => ({
      fields: { ...fields, ...changedFields }
    }));
  };
  render() {
    const { selectedRowKeys, pageConfig, memberList, visible,fields } = this.state;
    const rowSelection = {
      selectedRowKeys,
      onChange: this.onSelectChange
    };
    return (
      <Nav>
        <WrapperSearchForm
          pageConfig={pageConfig}
          getCurrentList={this.getCurrentList.bind(this)}
        />
        <Table
          dataSource={memberList}
          columns={this.columns}
          rowSelection={rowSelection}
          pagination={pageConfig}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />
        <Modal
          title="审核党员"
          visible={visible}
          onOk={this.handleOk}
          onCancel={() => this.setState({ visible: false })}
        >
          <WrapperAuditForm {...fields} onChange={this.handleFormChange} />
        </Modal>

        <Row
          type="flex"
          // justify="center"
          gutter={24}
        >
          <Col>
            <Button className="btn btn--primary">
              <a onClick={() => hashHistory.replace(`/goods/detail`)}>新增</a>
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default PartyMemberList;
