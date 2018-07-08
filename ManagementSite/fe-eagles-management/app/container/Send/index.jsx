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
import {
  getList,
  edit
} from "../../services/sendService";
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
          <Col span={5} key={2}>
            <FormItem label="商品名称">
              {getFieldDecorator(`user`)(<Input />)}
            </FormItem>
          </Col>
          <Col span={8} key={3}>
            <FormItem label="下单日期">
              <Col span={11}>
                <FormItem>
                  {getFieldDecorator("startTime")(
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
              <Col span={11}>
                <FormItem>
                  {getFieldDecorator("endTime")(
                    <DatePicker placeholder="结束时间" />
                  )}
                </FormItem>
              </Col>
            </FormItem>
          </Col>
          {/* <Col span={5} key={4}>
            <FormItem label="状态">
              {getFieldDecorator("state")(
                <Select>
                  <Option value="0">全部</Option>
                  <Option value="1">未发货</Option>
                  <Option value="2">已发货</Option>
                  <Option value="3">已签收</Option>
                </Select>
              )}
            </FormItem>
          </Col> */}
          {/* <Col span={5} key={5}>
            <FormItem label="下单商品">
              {getFieldDecorator(`goods`)(<Input />)}
            </FormItem>
          </Col> */}
          <Col span={8} key={4} style={{ paddingTop: "3px" }}>
            <Button type="primary" htmlType="submit">
              搜索
            </Button>
            <Button style={{ marginLeft: 8 }} onClick={this.handleReset}>
              清空
            </Button>
          </Col>
        </Row>
        {/* <Row>
          <Col span={23} style={{ textAlign: "right", paddingRight: "16px" }}>
            <Button type="primary" htmlType="submit">
              搜索
            </Button>
            <Button style={{ marginLeft: 8 }} onClick={this.handleReset}>
              清空
            </Button>
          </Col>
        </Row> */}
      </Form>
    );
  }
}

const WrapperSearchForm = Form.create({
  mapPropsToFields: props => {
    // const project = props.project;
    return {
      exType: Form.createFormField({
        value: "0"
      }),
      state: Form.createFormField({
        value: "0"
      })
    };
  }
})(SearchForm);

class SendList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedRowKeys: [], // 项目id数组
      projectList: [], // 项目列表数组
      keyword: "", // 关键字
      current: 1, // 当前页
      pageConfig: {} // 当前页配置
    };
    this.columns = [
      {
        title: "用户",
        dataIndex: "user"
      },
      {
        title: "地址信息",
        dataIndex: "address"
      },
      {
        title: "下单商品",
        dataIndex: "goods"
      },
      {
        title: "下单日期",
        dataIndex: "date"
      },
      {
        title: "状态",
        dataIndex: "state",
        render: text => <span>{text}</span>
      },
      {
        title: "快递信息备注",
        dataIndex: "remark",
        render: text => <span>{text}</span>
      },
      {
        title: "操作",
        render: (text, record) => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(`/project/detail/${record.projectId}`)
                }
              >
                编辑
              </a>
              <a
                onClick={() =>
                  hashHistory.replace(`/project/detail/${record.projectId}`)
                }
                style={{ paddingLeft: "24px" }}
              >
                删除
              </a>
            </div>
          );
        }
      }
    ];
    this.data = [
      {
        key: "1",
        user: "张三",
        address: "上海浦东新区",
        goods: "耐克足球鞋",
        date: "2018-5-12 10:16",
        state: "未发货",
        remark: "暂时没有备注"
      },
      {
        key: "2",
        user: "张三",
        address: "上海浦东新区",
        goods: "耐克足球鞋",
        date: "2018-5-12 10:16",
        state: "未发货",
        remark: "暂时没有备注"
      },
      {
        key: "3",
        user: "张三",
        address: "上海浦东新区",
        goods: "耐克足球鞋",
        date: "2018-5-12 10:16",
        state: "未发货",
        remark: "暂时没有备注"
      }
    ];
    this.getListConfig = {
      PageNumber: 1,
      PageSize: 10,
      StartTime: "",
      EndTime: "",
      OrgId: "",
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
      List.forEach(v => (v.key = v.ExercisesId));
      let { PageNumber } = params;
      this.setState({ questionList: List, current: PageNumber });
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
          requestPage: page,
          keyword: this.state.keyword
        });
      }
    };
    this.setState({ pageConfig });
  }
  // 编辑
  handleDelete = async id => {
    confirm({
      title: "是否确认删除?",
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code } = await deleteQuestion({
            ExercisesId: id
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
  // 编辑项目
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
  render() {
    const { selectedRowKeys, pageConfig, sendList } = this.state;
    const rowSelection = {
      selectedRowKeys,
      onChange: this.onSelectChange
    };
    return (
      <Nav>
        <WrapperSearchForm />
        <Table
          dataSource={sendList}
          columns={this.columns}
          rowSelection={rowSelection}
          pagination={pageConfig}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />

        <Row
          type="flex"
          // justify="center"
          gutter={24}
          // className={projectList.length === 0 ? "init" : ""}
        >
          {/* <Col >
            <Button onClick={this.handleDelete} className="btn">
              批量发货
            </Button>
          </Col> */}
        </Row>
      </Nav>
    );
  }
}

export default SendList;
