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
import { getList, del,sendMsg } from "../../services/memberService";
import { getList as getBranchList } from "../../services/branchService";
import { auditStatus } from "../../constants/config/appconfig";
import { audit } from "../../services/auditService";

const confirm = Modal.confirm;
class SearchForm extends Component {
  constructor(props) {
    super(props);
    this.state = {
      branchList: []
    };
  }
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
  componentWillMount() {
    this.getCurrentList();
  }
  // 加载当前页
  getCurrentList = async () => {
    try {
      let { List } = await getBranchList({
        PageNumber: 1,
        PageSize: 10000
      });
      console.log("List - ", List);
      List.forEach((v, i) => {
        v.key = i;
      });
      this.setState({ branchList: List });
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };

  render() {
    const { getFieldDecorator } = this.props.form;
    const { branchList } = this.state;
    let userInfo = JSON.parse(localStorage.info);
    let branchId = userInfo.BranchId;
    console.log(branchId);
    return (
      <Form
        className="ant-advanced-search-form"
        layout="inline"
        onSubmit={this.handleSearch.bind(this)}
      >
        <Row gutter={24}>
          {branchId == 0 ? (
            <Col span={6} key={1}>
              <FormItem label="支部名称">
                {getFieldDecorator("BranchId")(
                  <Select>
                    <Option value="">全部</Option>
                    {branchList.map((o, i) => {
                      return (
                        <Option key={i} value={o.BranchId}>
                          {o.BranchName}
                        </Option>
                      );
                    })}
                  </Select>
                )}
              </FormItem>
            </Col>
          ) : null}
          <Col span={6} key={2}>
            <FormItem label="党员名称">
              {getFieldDecorator(`UserName`)(<Input />)}
            </FormItem>
          </Col>
          <Col span={6} key={3}>
            <FormItem label="审核状态">
              {getFieldDecorator("Status")(
                <Select>
                  <Option value="">全部</Option>
                  {auditStatus.map((o, i) => {
                    return (
                      <Option key={i} value={o.Status}>
                        {o.text}
                      </Option>
                    );
                  })}
                </Select>
              )}
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
    console.log(props);
    return {
      BranchId: Form.createFormField({
        value: props.obj.BranchId ? props.obj.BranchId : ""
      }),
      UserName: Form.createFormField({
        value: props.obj.UserName
      }),
      Status: Form.createFormField({
        value: props.obj.Status ? props.obj.Status : ""
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
        value: props.AuditStatus.value ? props.AuditStatus.value : "0"
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
                <Option value="1">不通过</Option>
              </Select>
            )}
          </FormItem>
        </Col>
      </Row>
      <Row gutter={24}>
        <Col span={20} key={2}>
          <FormItem {...formItemLayout} label="审核结果描述">
            {getFieldDecorator(`Reason`, {
              rules: [
                {
                  required: true,
                  message: "必填，请输入审核结果描述"
                }
              ]
            })(<TextArea rows={4} />)}
          </FormItem>
        </Col>
      </Row>
    </Form>
  );
});



// 
const SendMsgForm = Form.create({
  onFieldsChange(props, changedFields) {
    props.onChange(changedFields);
  },
  mapPropsToFields: props => {
    // const project = props.project;
    return {
      Title: Form.createFormField({
        ...props.Title,
        value: props.Title.value ? props.Title.value : ""
      }),
      NewsType: Form.createFormField({
        ...props.NewsType,
        value: props.NewsType.value ? props.NewsType.value : "50"
      }),
      TargetUrl: Form.createFormField({
        ...props.Title,
        value: props.TargetUrl.value ? props.TargetUrl.value : ""
      }),
      Content: Form.createFormField({
        ...props.Content,
        value: props.Content.value
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
          <FormItem {...formItemLayout} label="标题">
            {getFieldDecorator("Title", {
              rules: [
                {
                  required: true,
                  message: "必填，请输入标题"
                }
              ]
            })(<Input placeholder="必填，请输入标题" />)}
          </FormItem>
        </Col>
      </Row>
      <Row gutter={24}>
        <Col span={20} key={2}>
          <FormItem {...formItemLayout} label="类型">
           {getFieldDecorator("NewsType")(
              <Select>
                 <Option value="50">会议通知</Option>
              </Select>
            )}               
          </FormItem>
        </Col>
      </Row>
      <Row gutter={24}>
        <Col span={20} key={2}>
          <FormItem {...formItemLayout} label="通知详情链接">
            {getFieldDecorator("TargetUrl")(
              <Input placeholder="请输入跳转链接" />
            )}
          </FormItem>
        </Col>
      </Row>
      <Row gutter={24}>
        <Col span={20} key={2}>
          <FormItem {...formItemLayout} label="通知描述">
            {getFieldDecorator("Content")(
              <TextArea rows={4} placeholder="通知描述" />
            )}
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
      sendmsgvisible: false,
      obj: {},
      fields: {
        AuditStatus: "", //审核结果
        Reason: "" // 审核结果描述
      },
      SendMsgfields: {
        NewsType: "50", //审核结果
        Title: "", // 审核结果描述
        Content:"", // 审核结果描述
        TargetUrl:"", // 审核结果描述
      },
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
      // {
      //   title: "党员类型",
      //   dataIndex: "MemberType",
      //   render: text => <span>{text == "0" ? "党员" : "预备党员"}</span>
      // },
      {
        title: "审核状态",
        dataIndex: "Status",
        render: text => {
          return auditStatus.map(o => {
            return o.Status == text ? <span key="1">{o.text}</span> : null;
          });
        }
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
                style={{
                  paddingLeft: "24px",
                  display: this.state.authMap.get("User0003") ? null : "none"
                }}
              >
                设置数据权限
              </a>
              <a
                style={{
                  paddingLeft: "24px",
                  display:
                    this.state.authMap.get("Audit001") && obj.Status == "-1"
                      ? null
                      : "none"
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
      BranchId: "",
      Status: ""
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
      let { List, TotalCount, Message } = await getList(params);
      console.log("List - ", List);
      List.forEach(v => {
        v.key = v.UserId;
      });
      this.setState({ memberList: List, current: PageNumber, Message });
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
          let { Code, Message } = await del({ UserId });
          if (Code === "00") {
            message.success(`删除成功`);
            await this.getCurrentList({
              ...this.getListConfig,
              PageNumber: this.state.current
              // keyword: this.state.keyword
            });
            this.setState({ selectedRowKeys: [] });
          } else {
            message.error(Message);
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
      if (!Reason.value) {
        message.error("请输入审核结果描述");
        return;
      }
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


  SendMsghandleOk = async () => {
    try {
      const { selectedRowKeys, SendMsgfields } = this.state;
      const { NewsType, Title,Content,TargetUrl } = SendMsgfields;
      if (!Title.value) {
        message.error("请输入标题");
        return;
      }
      let params = {
        Title: Title.value,
        Content: Content.value,
        NewsType: NewsType, // 党员
        TargetUrl: TargetUrl.value,
        id:selectedRowKeys
      };
      console.log(params);
      let { Code } = await sendMsg(params);
      this.setState({
        sendmsgvisible: false,
        SendMsgfields: {
          Title: "",
          Content: "",
          TargetUrl:"",
          NewsType:"50"
        }
      });
      if (Code === "00") {
        message.success("发送成功");
        await this.getCurrentList({
          ...this.getListConfig,
          PageNumber: this.state.current
        });
      } else {
        message.error("发送失败");
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
  SendMsghandleFormChange = changedFields => {
    this.setState(({ SendMsgfields }) => ({
      SendMsgfields: { ...SendMsgfields, ...changedFields }
    }));
  };
  
  changeSearch = obj => {
    this.setState({
      obj
    });
  };
  render() {
    const {
      selectedRowKeys,
      pageConfig,
      memberList,
      Message,
      visible,
      fields,
      SendMsgfields,
      obj,
      sendmsgvisible
    } = this.state;
    const rowSelection = {
      selectedRowKeys,
      onChange: this.onSelectChange
    };
    return (
      <Nav>
        <WrapperSearchForm
          getCurrentList={this.getCurrentList.bind(this)}
          obj={obj}
          setObj={this.changeSearch.bind(this)}
        />
        <Table
          dataSource={memberList}
          columns={this.columns}
          rowSelection={rowSelection}
          pagination={pageConfig}
          locale={{ emptyText: Message }}
          bordered
        />
        <Modal
          title="审核党员"
          visible={visible}
          onOk={this.handleOk}
          onCancel={() => this.setState({ visible: false })}
        >
          <WrapperAuditForm  {...fields} onChange={this.handleFormChange} />
        </Modal>

        <Modal
          title="发送通知消息"
          visible={sendmsgvisible}
          onOk={this.SendMsghandleOk}
          onCancel={() => this.setState({ sendmsgvisible: false })}
        >
          <SendMsgForm {...SendMsgfields} onChange={this.SendMsghandleFormChange} />
        </Modal>

        <Row
          type="flex"
          // justify="center"
          gutter={24}
        >
          <Col>
            <Button
              className="btn btn--primary"
              type="primary"
              onClick={() => hashHistory.replace(`/partymember/detail`)}
            >
              新增
            </Button>
          </Col>
          <Col>
            <Button
              className="btn btn--primary"
              type="primary"
              onClick={() =>

                this.setState({ sendmsgvisible: true, id: this.state.selectedRowKeys })


                //hashHistory.hashHistory(`/partymember/SendMsg`)
              }
            >
              发送消息
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default PartyMemberList;
