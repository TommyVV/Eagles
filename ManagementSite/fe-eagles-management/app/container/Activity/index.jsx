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
import moment from "moment";
import "moment/locale/zh-cn";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

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
              {getFieldDecorator(`ActivityTaskName`)(<Input />)}
            </FormItem>
          </Col>
          {/* <Col span={8} key={3}>
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
                    textAlign: "center"
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
          </Col> */}
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
      ActivityTaskName: Form.createFormField({
        value: config.ActivityTaskName
      }),
      StartTime: Form.createFormField({
        value: config.StartTime ? moment(config.StartTime, "YYYY-MM-dd") : null
      }),
      EndTime: Form.createFormField({
        value: config.EndTime ? moment(config.EndTime, "YYYY-MM-dd") : null
      })
    };
  }
})(SearchForm);

class NewsList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      selectedRowKeys: [], // 项目id数组
      newsList: [], // 新闻列表数组
      keyword: "", // 关键字
      current: 1, // 当前页
      pageConfig: {} // 当前页配置
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
          return <span>{type == "0" ? "投票" : "调查问卷"}</span>;
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
      // {
      //   title: "发布时间",
      //   dataIndex: "CreateTime",
      //   render: text => <span>{new Date(text).format("yyyy-MM-dd")}</span>
      // },
      {
        title: "操作",
        dataIndex: "ActivityTaskId",
        render: ActivityTaskId => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(`/activity/detail/${ActivityTaskId}`)
                }
              >
                编辑
              </a>
              <a
                onClick={() => this.handleDelete(ActivityTaskId)}
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
      PageSize: 10,
      ActivityTaskName: ""
      // StarTime: "",
      // EndTime: ""
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
      console.log("List - ", List);
      List.forEach((v, i) => {
        v.key = i;
      });
      this.getListConfig = params; // 保存搜索的数据
      this.setState({ newsList: List, current: params.PageNumber });
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
          let { Code } = await del({
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
            message.error("删除失败");
          }
        } catch (e) {
          throw new Error(e);
        }
      }
    });
  };
  render() {
    const { selectedRowKeys, pageConfig, newsList } = this.state;
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
          locale={{ emptyText: "暂无数据" }}
          bordered
        />

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
