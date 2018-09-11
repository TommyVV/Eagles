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
import { getList as getBranchList } from "../../services/branchService";
import { auditStatus } from "../../constants/config/appconfig";
import moment from "moment";
import "moment/locale/zh-cn";
import Nav from "../Nav";
import "./style.less";
import Audit from "../../components/Common/Audit";

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

  componentWillMount() {
    this.getBranchListCurrentList();
  }

  getBranchListCurrentList = async () => {
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
    let userInfo = JSON.parse(localStorage.info);
    let tokenBranchId = userInfo.BranchId;
    const { branchList } = this.state;
    return (
      <Form
        className="ant-advanced-search-form"
        layout="inline"
        onSubmit={this.handleSearch.bind(this)}
      >
        <Row gutter={24}>
          {tokenBranchId == 0 ? (
            <Col span={6} key={1111}>
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
          <Col span={5} key={1}>
            <FormItem label="标题">
              {getFieldDecorator(`NewsName`)(<Input />)}
            </FormItem>
          </Col>

          <Col span={6} key={33}>
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
            span={5}
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
      NewsName: Form.createFormField({
        value: config.NewsName
      }),
      Status: Form.createFormField({
        value: config.Status ? config.Status : ""
      }),
      BranchId: Form.createFormField({
        value: config.BranchId
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
      pageConfig: {}, // 当前页配置
      authMap: new Map(),
      visible: false, // 弹出框
      branchList: []
    };
    this.columns = [
      {
        title: "标题",
        dataIndex: "NewsName",
        width: "30%"
      },
      {
        title: "栏目",
        dataIndex: "ModuleName"
      },
      {
        title: "支部名称",
        dataIndex: "BranchName"
      },
      {
        title: "类型",
        dataIndex: "NewsType",
        render: text => <span>{text == 0 ? "新闻" : "会议"}</span>
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
        render: text => <span>{new Date(text).format("yyyy-MM-dd")}</span>
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
                  hashHistory.replace(`/news/detail/${obj.NewsId}`)
                }
              >
                编辑
              </a>
              <a
                onClick={() => this.handleDelete(obj.NewsId)}
                style={{ paddingLeft: "24px" }}
              >
                删除
              </a>
              <a
                onClick={() => {
                  let selectedRowKeys = this.state;
                  selectedRowKeys = [];
                  selectedRowKeys.push(obj.NewsId);
                  this.setState({
                    visible: true,
                    selectedRowKeys: selectedRowKeys
                  });
                }}
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
      NewsName: "",
      NewsType: "0",
      Status: "",
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

  getCurrentListFn() {
    this.setState({ visible: false, selectedRowKeys: [] });
    this.getCurrentList(this.getListConfig);
  }
  // 选择分享时触发的改变
  onSelectChange = selectedRowKeys => {
    this.setState({ selectedRowKeys });
  };
  // 加载当前页
  getCurrentList = async params => {
    try {
      let { List, TotalCount } = await getNewsList(params);
      console.log("List - ", List);
      List.forEach(v => {
        v.key = v.NewsId;
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
  handleDelete = async NewsId => {
    console.log(NewsId);
    confirm({
      title: "是否确认删除?",
      okText: "确认",
      cancelText: "取消",
      onOk: async () => {
        try {
          let { Code, Message } = await deleteNews({
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
            message.error(Message);
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
  setVisible(isVisible) {
    this.setState({ visible: isVisible });
  }

  render() {
    let {
      selectedRowKeys,
      pageConfig,
      newsList,
      visible,
      authMap
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
          branchList={this.branchList}
        />
        <Table
          dataSource={newsList}
          columns={this.columns}
          rowSelection={rowSelection}
          pagination={pageConfig}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />
        <Audit
          visible={visible}
          setVisible={this.setVisible.bind(this)}
          type={1}
          selectedRowKeys={selectedRowKeys}
          getCurrentListFn={this.getCurrentListFn.bind(this)}
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
              onClick={() => hashHistory.replace(`/news/detail`)}
            >
              新增
            </Button>
          </Col>
          <Col>
            <Button
              className="btn btn--primary"
              type="primary"
              onClick={() => {
                let hasAudit = false;
                if (!selectedRowKeys.length) {
                  message.error("请选择待审核的新闻");
                  return;
                }
                selectedRowKeys.map(id => {
                  newsList.map(o => {
                    // 选中的是没有审核过的，并且有审核权限
                    if (
                      o.NewsId == id &&
                      authMap.get("Audit001") &&
                      o.Status != "-1" &&
                      !hasAudit
                    ) {
                      message.error("请选择待审核的新闻");
                      hasAudit = true;
                      return false;
                    }
                  });
                });
                if (!hasAudit) {
                  this.setState({ visible: true });
                }
              }}
            >
              批量审核
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default NewsList;
