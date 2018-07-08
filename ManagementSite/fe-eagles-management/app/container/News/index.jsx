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
import { getNewsList, deleteNews } from "../../services/newsService";
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class SearchForm extends Component {
  handleSearch = e => {
    e.preventDefault();
    this.props.form.validateFields((err, values) => {
      console.log("Received values of form: ", values);
      let params = {
        ...this.getListConfig,
        ...values
      };
      this.getCurrentList(params);
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
          <Col span={5} key={1}>
            <FormItem label="作者">
              {getFieldDecorator("title")(
                <Select>
                  <Option value="0">小李</Option>
                  <Option value="1">小王</Option>
                  <Option value="2">张三</Option>
                </Select>
              )}
            </FormItem>
          </Col>
          <Col span={5} key={5}>
            <FormItem label="标题">
              {getFieldDecorator(`goods`)(<Input />)}
            </FormItem>
          </Col>
          <Col span={8} key={3}>
            <FormItem label="发布时间">
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
    // const project = props.project;
    return {
      title: Form.createFormField({
        value: "0"
      }),
      state: Form.createFormField({
        value: "0"
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
        dataIndex: "NewsName",
        width: "30%"
      },
      {
        title: "作者",
        dataIndex: "Author"
      },
      {
        title: "标题图片",
        dataIndex: "NewsImg",
        render: image => {
          return (
            <div>
              <img style={{ width: "80px", padding: "10px 0" }} src={image} />
            </div>
          );
        }
      },
      {
        title: "来源",
        dataIndex: "Source"
      },
      {
        title: "发布时间",
        dataIndex: "CreateTime",
        render: text => <span>{new Date(text).format("yyyy-MM-DD")}</span>
      },
      {
        title: "操作",
        dataIndex: "NewsId",
        render: NewsId => {
          return (
            <div>
              <a onClick={() => hashHistory.replace(`/news/detail/${NewsId}`)}>
                编辑
              </a>
              <a
                onClick={() => this.handleDelete(NewsId)}
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
      PageSize: 2,
      UserId: "",
      NewsName: "",
      StartTime: "2018-06-01T01:37:09.235Z",
      EndTime: "2018-07-03T01:37:09.235Z"
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
    const { PageNumber } = this.getListConfig;
    try {
      let { List, TotalCount } = await getNewsList(params);
      console.log("projectList - ", List);
      List.forEach(v => {
        v.key = v.NewsId;
      });
      this.setState({ newsList: List, current: PageNumber });
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
  // 下拉提示列表 关键字匹配
  fetchList = async value => {
    try {
      let keyword = encodeURI(value);
      let params = { ...this.getListConfig, keyword };
      let { projectList } = await getProjectList(params);
      return projectList.map(v => ({
        text: v.projectName,
        value: v.projectName
      }));
    } catch (e) {
      throw new Error(e);
    }
  };
  // 回车搜索列表  关键字匹配
  searchList = async value => {
    try {
      let keyword = encodeURI(value);
      let params = { ...this.getListConfig, keyword };
      let { projectList, totalSize } = await getProjectList(params);
      projectList.forEach(v => (v.key = v.projectId));
      this.setState({
        keyword,
        projectList,
        current: 1
      });
      this.updatePageConfig(totalSize);
    } catch (e) {
      message.error("获取失败");
      throw new Error(e);
    }
  };
  // 删除项目
  handleDelete = async NewsId => {
    console.log(NewsId);
    confirm({
      title: "是否确认删除?",
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code } = await deleteNews({
            NewsId
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
    const { selectedRowKeys, pageConfig, newsList } = this.state;
    const rowSelection = {
      selectedRowKeys,
      onChange: this.onSelectChange
    };
    return (
      <Nav>
        <WrapperSearchForm />
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
            <Button className="btn btn--primary">
              <a onClick={() => hashHistory.replace(`/partymember/detail`)}>
                新增
              </a>
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default NewsList;
