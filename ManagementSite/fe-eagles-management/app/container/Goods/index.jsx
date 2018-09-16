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
const Option = Select.Option;
const TextArea = Input.TextArea;
import { hashHistory } from "react-router";
import Nav from "../Nav";
import "./style.less";
import { getList, createOrEdit, del } from "../../services/goodsService";
import { audit } from "../../services/auditService";
import { auditStatus } from "../../constants/config/appconfig";
const confirm = Modal.confirm;
class SearchForm extends Component {
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
      const { getCurrentList, setObj } = view.props;
      getCurrentList(params);
      setObj({ ...values });
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
          <Col span={5} key={5}>
            <FormItem label="商品名称">
              {getFieldDecorator(`GoodsName`)(<Input />)}
            </FormItem>
          </Col>
          <Col span={5} key={6}>
            <FormItem label="商品状态">
              {getFieldDecorator(`SaleStatus`)(
                <Select>
                  <Option value="-1">全部</Option>
                  <Option value="10">正常</Option>
                  <Option value="5">下架</Option>
                </Select>
              )}
            </FormItem>
          </Col>
          <Col span={6} key={3}>
            <FormItem label="审核状态">
              {getFieldDecorator("Status")(
                <Select >
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
    return {
      SaleStatus: Form.createFormField({
        value: props.obj.SaleStatus ? props.obj.SaleStatus : "-1"
      }),
      GoodsName: Form.createFormField({
        value: props.obj.GoodsName
      }),
      Status: Form.createFormField({
        value: props.obj.Status ? props.obj.Status : ""
      }),
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

class GoodsList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedRowKeys: [], // 项目id数组
      goodsList: [], // 项目列表数组
      current: 1, // 当前页
      pageConfig: {}, // 当前页配置
      authMap: new Map(),
      currentId: "", // 当前的id
      visible: false, // 弹出框
      fields: {
        AuditStatus: "", //审核结果
        Reason: "" // 审核结果描述
      },
      obj: {}
    };
    this.columns = [
      {
        title: "商品名称",
        dataIndex: "GoodsName",
      },
      {
        title: "库存",
        dataIndex: "Stock",
      },
      {
        title: "所需积分",
        dataIndex: "Score",
      },
      {
        title: "状态",
        dataIndex: "SaleStatus",
        render: text => <span>{text == "10" ? "正常" : "下架"}</span>,
      },
      {
        title: "审核状态",
        dataIndex: "Status",
        render: text => {
          return auditStatus.map(o => {
            return o.Status == text ? <span key="1">{o.text}</span> : null
          })
        }
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(`/goods/detail/${obj.GoodsId}`)
                }
              >
                编辑
              </a>
              <a
                onClick={() => this.handleDelete(obj.GoodsId)}
                style={{ paddingLeft: "24px" }}
              >
                删除
              </a>
              <a
                onClick={() => this.changeStatus(obj)}
                style={{ paddingLeft: "24px" }}
              >
                {obj.SaleStatus == "10" ? "下架" : "上架"}
              </a>
              <a
                onClick={() =>
                  this.setState({ visible: true, currentId: obj.GoodsId })
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
      GoodsName: "",
      SaleStatus: "-1",
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

  // 间接调用getCurrentList
  getCurrent(params) {
    this.getCurrentList(params);
  }
  // 加载当前页
  getCurrentList = async params => {
    const { PageNumber } = params;
    try {
      let { List, TotalCount, Message } = await getList(params);
      console.log("List - ", List);
      List.forEach(v => {
        v.key = v.GoodsId;
      });
      this.setState({ goodsList: List, current: PageNumber, Message });
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
  // 快速上下架
  changeStatus = async goods => {
    const tipTitle = goods.SaleStatus == "10" ? "下架" : "上架";
    confirm({
      title: `是否确认${tipTitle}“${goods.GoodsName}”?`,
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          const params = {
            Info: {
              ...goods,
              SaleStatus: goods.SaleStatus == "10" ? "5" : "10"
            }
          };
          let { Code } = await createOrEdit(params);
          if (Code === "00") {
            message.success(`${tipTitle}成功`);
            await this.getCurrentList({
              ...this.getListConfig,
              PageNumber: this.state.current
              // keyword: this.state.keyword
            });
            this.setState({ selectedRowKeys: [] });
          } else {
            message.error(`${tipTitle}失败`);
          }
        } catch (e) {
          throw new Error(e);
        }
      }
    });
  };

  // 删除
  handleDelete = async GoodsId => {
    confirm({
      title: `是否确认删除?`,
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code, Message } = await del({ GoodsId });
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
        Type: "3", // 商品
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
  changeSearch = obj => {
    this.setState({
      obj
    });
  };
  render() {
    const {
      selectedRowKeys,
      pageConfig,
      goodsList,
      Message,
      visible,
      fields,
      obj
    } = this.state;
    const rowSelection = {
      selectedRowKeys,
      onChange: this.onSelectChange
    };
    return (
      <Nav>
        <WrapperSearchForm
          getCurrentList={this.getCurrent.bind(this)}
          obj={obj}
          setObj={this.changeSearch.bind(this)}
        />
        <Table
          dataSource={goodsList}
          columns={this.columns}
          // rowSelection={rowSelection}
          pagination={pageConfig}
          locale={{ emptyText: Message }}
          bordered
        />
        <Modal
          title="审核商品"
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
          {/* <Col>
            <Button onClick={this.handleDelete} className="btn">
              批量下架
            </Button>
          </Col> */}
          <Col>
            <Button className="btn btn--primary" type="primary" onClick={() => hashHistory.replace(`/goods/detail`)}>
              新增
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default GoodsList;
