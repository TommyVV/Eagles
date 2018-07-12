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
import Nav from "../Nav";
import "./style.less";

const confirm = Modal.confirm;

class IntergralList extends React.Component {
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
        title: "积分类型",
        dataIndex: "type",
        width: "25%"
      },
      {
        title: "增加积分",
        dataIndex: "add",
        width: "25%"
      },
      {
        title: "是否可用",
        dataIndex: "isAvailable",
        width: "25%",
        render: text => <span>{text}</span>
      },
      {
        title: "操作",
        width: "25%",
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
        type: "新闻",
        add: "3",
        isAvailable: "是",
      },
      {
        key: "2",
        type: "阅读",
        add: "4",
        isAvailable: "否",
      },
      {
        key: "3",
        type: "习题",
        add: "5",
        isAvailable: "是",
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
    const formItemLayout = {
      labelCol: {
        xl: { span: 3 }
      },
      wrapperCol: {
        xl: { span: 8 }
      }
    };
    return (
      <Nav>
        <Row gutter={24}>
          <Col span={12}>
            <Form>
              <FormItem {...formItemLayout} label="积分类型">
                <Select>
                  <Option value="0" selected>在线考试</Option>
                  <Option value="1">新闻习题</Option>
                  <Option value="2">随机试卷</Option>
                </Select>
              </FormItem>
            </Form>
          </Col>
        </Row>
        <Table
          dataSource={this.data}
          columns={this.columns}
          rowSelection={rowSelection}
          pagination={pageConfig}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />

        <Row
          type="flex"
          justify="center"
          gutter={24}
          className={projectList.length === 0 ? "init" : ""}
        >
          <Col>
            <Button onClick={this.handleDelete} className="btn">
              批量删除
            </Button>
          </Col>
          <Col>
            <Button className="btn btn--primary">
              <a onClick={() => hashHistory.replace(`/intergral/detail`)}>
                新增
              </a>
            </Button>
          </Col>
        </Row>
      </Nav>
    );
  }
}

export default IntergralList;