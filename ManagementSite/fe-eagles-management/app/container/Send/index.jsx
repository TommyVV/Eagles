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
const TextArea = Input.TextArea;
import { hashHistory } from "react-router";
import { getList, edit } from "../../services/sendService";
import Nav from "../Nav";
import "./style.less";

// 搜索的表单
class SearchForm extends Component {
  handleSearch = e => {
    e.preventDefault();
    this.props.form.validateFields((err, values) => {
      console.log("Received values of form: ", values);
      let params = {
        PageNumber: 1,
        PageSize: 10,
        ...values
      };
      const getCurrentList = this.props.getCurrentList;
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
    return (
      <Form
        className="ant-advanced-search-form"
        layout="inline"
        onSubmit={this.handleSearch}
      >
        <Row gutter={24}>
          <Col span={5} key={2}>
            <FormItem label="商品名称">
              {getFieldDecorator(`GoodsName`)(<Input />)}
            </FormItem>
          </Col>
          <Col span={8} key={3}>
            <FormItem label="下单日期">
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
                    textAlign: "center"
                  }}
                >
                  -
                </span>
              </Col>
              <Col span={10} offset={1}>
                <FormItem>
                  {getFieldDecorator("EndTime")(
                    <DatePicker placeholder="结束时间" />
                  )}
                </FormItem>
              </Col>
            </FormItem>
          </Col>
          <Col span={8} key={4} style={{ paddingTop: "3px" }}>
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
      GoodsName: Form.createFormField({
        value: props.obj.GoodsName
      }),
      StartTime: Form.createFormField({
        value: props.obj.StartTime
      }),
      EndTime: Form.createFormField({
        value: props.obj.EndTime
      })
    };
  }
})(SearchForm);

// 发货的表单
const WrapperSendForm = Form.create({
  onFieldsChange(props, changedFields) {
    props.onChange(changedFields);
  },
  mapPropsToFields: props => {
    console.log(props);
    return {
      ExpressId: Form.createFormField({
        value: props.ExpressId
      }),
      Address: Form.createFormField({
        value: props.Address
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
          <FormItem {...formItemLayout} label="快递单号">
            {getFieldDecorator(`ExpressId`)(<Input />)}
          </FormItem>
        </Col>
      </Row>
      <Row gutter={24}>
        <Col span={20} key={2}>
          <FormItem {...formItemLayout} label="快递信息备注">
            {getFieldDecorator(`Address`)(<TextArea rows={4} />)}
          </FormItem>
        </Col>
      </Row>
    </Form>
  );
});

class SendList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedRowKeys: [], // id数组
      sendList: [], // 列表数组
      keyword: "", // 关键字
      current: 10, // 当前页
      pageConfig: {}, // 当前页配置
      currentId: "", // 当前发货商品的id
      visible: false, // 弹出框
      fields: {
        ExpressId: "", //快递单号
        Address: "" // 快递地址
      },
      obj: {}
    };
    this.columns = [
      {
        title: "下单商品",
        dataIndex: "GoodsName"
      },
      {
        title: "下单数量",
        dataIndex: "Count"
      },
      {
        title: "下单日期",
        dataIndex: "PlaceTime",
        render: text => <span>{new Date(text).format("yyyy-MM-dd")}</span>
      },
      {
        title: "状态",
        dataIndex: "OrderStatus",
        render: text => <span>{text == "0" ? "可以发货" : "不能发货"}</span>
      },
      {
        title: "快递信息备注",
        dataIndex: "Address"
      },
      {
        title: "操作",
        render: obj => {
          return (
            <div>
              {obj.OrderStatus == "0" ? (
                <a
                  onClick={() =>
                    this.setState({
                      visible: true,
                      currentId: obj.OrderId,
                      fields: {
                        ExpressId: obj.ExpressId, //快递单号
                        Address: obj.Address // 快递地址
                      }
                    })
                  }
                >
                  发货
                </a>
              ) : (
                ""
              )}
            </div>
          );
        }
      }
    ];

    this.getListConfig = {
      PageNumber: 1,
      PageSize: 10,
      StartTime: "",
      EndTime: "",
      GoodsName: ""
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
    try {
      let { List, TotalCount } = await getList(params);
      console.log(List, TotalCount);
      List.forEach(v => (v.key = v.OrderId));
      let { PageNumber } = params;
      this.setState({ sendList: List, current: PageNumber });
      this.updatePageConfig(TotalCount);
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  // 更新分页配置
  updatePageConfig(TotalCount) {
    let pageConfig = {
      total: TotalCount,
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
  handleOk = async () => {
    try {
      const { currentId, fields } = this.state;
      let { Code } = await edit({
        OrderInfo: [
          {
            OrderId: currentId,
            ...fields
          }
        ]
      });
      if (Code === "00") {
        message.success("发货成功");
        this.setState({
          visible: false,
          fields: {
            ExpressId: "", //快递单号
            Address: "" // 快递地址
          }
        });
        await this.getCurrentList({
          ...this.getListConfig,
          PageNumber: this.state.current
        });
      } else {
        message.error("发货失败");
        this.setState({
          visible: false,
          fields: {
            ExpressId: "", //快递单号
            Address: "" // 快递地址
          }
        });
      }
    } catch (e) {
      throw new Error(e);
    }
  };
  handleFormChange = changedFields => {
    if (changedFields.ExpressId) {
      this.setState(({ fields }) => ({
        fields: {
          ...fields,
          ExpressId: changedFields.ExpressId.value
        }
      }));
    } else if (changedFields.Address) {
      this.setState(({ fields }) => ({
        fields: {
          ...fields,
          Address: changedFields.Address.value
        }
      }));
    }
  };
  changeSearch = obj => {
    this.setState({
      obj
    });
  };
  render() {
    const { visible, pageConfig, sendList, fields, obj } = this.state;
    return (
      <Nav>
        <WrapperSearchForm
          getCurrentList={this.getCurrentList.bind(this)}
          obj={obj}
          setObj={this.changeSearch.bind(this)}
        />
        <Table
          dataSource={sendList}
          columns={this.columns}
          pagination={pageConfig}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />
        <Modal
          title="商品发货"
          visible={visible}
          onOk={this.handleOk}
          onCancel={() => this.setState({ visible: false })}
        >
          <WrapperSendForm {...fields} onChange={this.handleFormChange} />
        </Modal>
      </Nav>
    );
  }
}

export default SendList;
