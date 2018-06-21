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
  getProjectInfoById,
  getProjectList,
  deleteProject,
  updateProject
} from "../../../services/projectService";
import util from "../../../utils/util";
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
          <Col span={5} key={1}>
            <FormItem label="习题类型">
              {getFieldDecorator("exType")(
                <Select>
                  <Option value="0">全部</Option>
                  <Option value="1">在线考试习题</Option>
                  <Option value="2">新闻习题</Option>
                </Select>
              )}
            </FormItem>
          </Col>
          <Col span={5} key={2}>
            <FormItem label="标题">
              {getFieldDecorator(`title`)(<Input />)}
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
          <Col span={5} key={4}>
            <FormItem label="状态">
              {getFieldDecorator("state")(
                <Select>
                  <Option value="0">全部</Option>
                  <Option value="1">待审核</Option>
                  <Option value="2">审核通过</Option>
                  <Option value="3">审核不通过</Option>
                </Select>
              )}
            </FormItem>
          </Col>
        </Row>
        <Row>
          <Col span={23} style={{ textAlign: "right", paddingRight: "16px" }}>
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
      exType: Form.createFormField({
        value: "0"
      }),
      state: Form.createFormField({
        value: "0"
      })
    };
  }
})(SearchForm);

class Exercise extends React.Component {
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
        title: "问卷名称",
        dataIndex: "projectName",
        width: "20%"
      },
      {
        title: "习题类型",
        dataIndex: "creatorName",
        width: "20%"
      },
      {
        title: "状态",
        dataIndex: "state",
        width: "20%",
        render: text => (
          <span>{util.timeStampConvent(text, "yyyy-MM-dd hh:mm:ss")}</span>
        )
      },
      {
        title: "发布时间",
        dataIndex: "createTime",
        width: "20%",
        render: text => (
          <span>{util.timeStampConvent(text, "yyyy-MM-dd hh:mm:ss")}</span>
        )
      },
      {
        title: "操作",
        width: "20%",
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
              >
                删除
              </a>
            </div>
          );
        }
      }
    ];
    this.getListConfig = {
      requestPage: 1,
      pageSize: 6,
      keyword: ""
    };
  }
  componentWillMount() {
    // this.getCurrentList(this.getListConfig);
  }

  // 选择分享时触发的改变
  onSelectChange = selectedRowKeys => {
    this.setState({ selectedRowKeys });
  };

  // 加载当前页
  // getCurrentList = async params => {
  //   try {
  //     let { keyword, requestPage } = params;
  //     let config = { ...this.getListConfig, requestPage, keyword };
  //     let { totalSize, projectList } = await getProjectList(config);
  //     console.log("projectList - ", projectList);
  //     projectList.forEach(v => (v.key = v.projectId));
  //     this.setState({ projectList, current: requestPage });
  //     this.updatePageConfig(totalSize);
  //   } catch (e) {
  //     message.error("获取失败");
  //     throw new Error(e);
  //   }
  // };
  // 更新分页配置
  updatePageConfig(totalSize) {
    let pageConfig = {
      total: totalSize,
      pageSize: this.getListConfig.pageSize,
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
  handleDelete = async shareIds => {
    confirm({
      title: "是否确认删除?",
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { selectedRowKeys } = this.state;
          if (selectedRowKeys.length === 0) {
            return message.error("请选择需要删除的项目");
          }
          let { code } = await deleteProject({
            projectIdList: selectedRowKeys
          });
          if (code === 0) {
            message.success("删除成功");
            await this.getCurrentList({
              ...this.getListConfig,
              requestPage: this.state.current,
              keyword: this.state.keyword
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
    const { selectedRowKeys, pageConfig, projectList } = this.state;
    const rowSelection = {
      selectedRowKeys,
      onChange: this.onSelectChange
    };
    return (
      <Nav>
        <WrapperSearchForm />
        <Table
          dataSource={projectList}
          columns={this.columns}
          rowSelection={rowSelection}
          pagination={pageConfig}
          locale={{ emptyText: "暂无数据" }}
        />

        <Row
          type="flex"
          justify="center"
          gutter={24}
          className={projectList.length === 0 ? "init" : ""}
        >
          <Col>
            <Button type="primary" className="btn btn--primary">
              <a onClick={() => hashHistory.replace(`/exercise/create`)}>
                新增问卷
              </a>
            </Button>
          </Col>
          <Col>
            <Button onClick={this.handleDelete} className="btn">
              批量删除
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default Exercise;
