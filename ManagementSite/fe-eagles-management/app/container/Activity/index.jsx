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
  DatePicker
} from "antd";
const FormItem = Form.Item;
const Option = Select.Option;
import { hashHistory } from "react-router";
import { getList, del } from "../../services/activityService";
import { audit } from "../../services/auditService";
import { auditStatus } from "../../constants/config/appconfig";
import moment from "moment";
import "moment/locale/zh-cn";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;
const TextArea = Input.TextArea;

class SearchForm extends Component {
  handleSearch = e => {
    e.preventDefault();
    const _this = this;
    this.props.form.validateFields((err, values) => {
      console.log("Received values of form: ", values);
      let params = {
        ..._this.props.getListConfig,
        ...values
      };
      const getCurrentList = this.props.getCurrentList;
      getCurrentList(params);
    });
  };

  handleReset = () => {
    this.props.form.resetFields();
    const { getFieldsValue } = this.props.form;
    let values = getFieldsValue();
    let params = {
      ...this.props.getListConfig,
      ...values
    };
    const getCurrentList = this.props.getCurrentList;
    getCurrentList(params);
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
          <Col span={5} key={1}>
            <FormItem label="标题">
              {getFieldDecorator(`ActivityName`)(<Input />)}
            </FormItem>
          </Col>
          <Col span={8} key={3}>
            <FormItem label="发布时间">
              <Col span={11}>
                <FormItem>
                  {getFieldDecorator("StartTime")(
                    <DatePicker placeholder="开始时间" />
                  )}
                </FormItem>
              </Col>
              <Col span={2}>
                <span
                  style={{
                    display: "inline-block",
                    width: "100%",
                    textAlign: "left"
                  }}
                >
                  -
                </span>
              </Col>
              <Col span={11}>
                <FormItem>
                  {getFieldDecorator("EndTime")(
                    <DatePicker placeholder="结束时间" />
                  )}
                </FormItem>
              </Col>
            </FormItem>
          </Col>
          <Col span={5} key={333}>
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
    const config = props.getListConfig;
    console.log(config);
    return {
      ActivityName: Form.createFormField({
        value: config.ActivityName
      }),
      StartTime: Form.createFormField({
        value: config.StartTime ? moment(config.StartTime, "YYYY-MM-dd") : null
      }),
      EndTime: Form.createFormField({
        value: config.EndTime ? moment(config.EndTime, "YYYY-MM-dd") : null
      }),
      Status: Form.createFormField({
        value: config.Status ? config.Status : ""
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
        value: props.AuditStatus.value ? props.AuditStatus.value + "" : "0"
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
class NewsList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedRowKeys: [], // 项目id数组
      newsList: [], // 新闻列表数组
      visible: false, // 弹出框
      keyword: "", // 关键字
      current: 1, // 当前页
      pageConfig: {}, // 当前页配置
      authMap: new Map(),
      fields: {
        AuditStatus: "", //审核结果
        Reason: "" // 审核结果描述
      }
    };
    this.columns = [
      {
        title: "标题",
        dataIndex: "ActivityTaskName"
      },
      {
        title: "类型",
        dataIndex: "ActivityTaskType",
        render: type => {
          switch (type) {
            case 0:
              return <span>{"报名"}</span>;
            case 1:
              return <span>{"投票"}</span>;
            case 2:
              return <span>{"调查问卷"}</span>;
            default:
              return <span>{"其他"}</span>;
          }
        }
      },
      {
        title: "标题图片",
        dataIndex: "ActivityTaskImg",
        render: image => {
          return (
            <div>
              <img style={{ width: "80px", padding: "10px 0" }} src={image} />
            </div>
          );
        }
      },
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
                  hashHistory.replace(`/activity/detail/${obj.ActivityTaskId}`)
                }
              >
                编辑
              </a>
              <a
                onClick={() => this.handleDelete(obj.ActivityTaskId)}
                style={{ paddingLeft: "24px" }}
              >
                删除
              </a>
              <a
                onClick={() =>
                  this.setState({
                    visible: true,
                    currentId: obj.ActivityTaskId
                  })
                }
                style={{
                  paddingLeft: "24px",
                  display:
                    this.state.authMap.get("Audit001") && obj.Status == "-1"
                      ? null
                      : "none"
                }}
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
      ActivityName: "",
      StartTime: "",
      EndTime: "",
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
    try {
      let { List, TotalCount, Message } = await getList(params);
      console.log("List - ", List);
      List.forEach((v, i) => {
        v.key = i;
      });
      this.getListConfig = params; // 保存搜索的数据
      this.setState({ newsList: List, current: params.PageNumber, Message });
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
  handleDelete = async ActivityId => {
    console.log(ActivityId);
    confirm({
      title: "是否确认删除?",
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code, Message } = await del({
            ActivityId
          });
          if (Code === "00") {
            message.success("删除成功");
            await this.getCurrentList({
              ...this.getListConfig,
              requestPage: this.state.current
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
  handleOk = async () => {
    try {
      const { currentId, fields } = this.state;
      const { AuditStatus, Reason } = fields;
      if (!Reason.value) {
        message.error("请输入审核结果描述");
        return;
      }
      let params = {
        AuditStatus: AuditStatus.value ? AuditStatus.value : "0",
        Reason: Reason.value,
        Type: "4", // 活动
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
    const {
      selectedRowKeys,
      pageConfig,
      newsList,
      Message,
      visible,
      fields
    } = this.state;
    const rowSelection = {
      selectedRowKeys,
      onChange: this.onSelectChange
    };
    return (
      <Nav>
        <WrapperSearchForm
          getCurrentList={this.getCurrentList}
          getListConfig={this.getListConfig}
        />
        <Table
          dataSource={newsList}
          columns={this.columns}
          rowSelection={rowSelection}
          pagination={pageConfig}
          locale={{ emptyText: Message }}
          bordered
        />
        <Modal
          title="审核活动"
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
        // className={projectList.length === 0 ? "init" : ""}
        >
          {/* <Col>
            <Button onClick={this.handleDelete} className="btn">
              批量删除
            </Button>
          </Col> */}
          <Col>
            <Button
              className="btn btn--primary"
              type="primary"
              onClick={() => hashHistory.replace(`/activity/detail`)}
            >
              新增
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default NewsList;
