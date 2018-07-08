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
            <FormItem label="姓名">
              {getFieldDecorator(`user`)(<Input />)}
            </FormItem>
          </Col>
          <Col span={5} key={2}>
            <FormItem label="机构名称">
              {getFieldDecorator(`orgName`)(<Input />)}
            </FormItem>
          </Col>
          <Col span={6} style={{ textAlign: "left", paddingTop: "3px" }}>
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
      state: Form.createFormField({
        value: "0"
      })
    };
  }
})(SearchForm);

class RankList extends React.Component {
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
        title: "用户姓名",
        dataIndex: "name"
      },
      {
        title: "用户积分总数",
        dataIndex: "count"
      },
      {
        title: "用户使用积分",
        dataIndex: "used"
      },
      {
        title: "党员状态",
        dataIndex: "state"
      },
      {
        title: "操作",
        userId: "1",
        render: obj => {
          return (
            <div>
              <a
                onClick={() =>
                  hashHistory.replace(`/rankdetail/${obj.userId}`)
                }
              >
                查看详细信息
              </a>
            </div>
          );
        }
      }
    ];
    this.data = [
      {
        key: "1",
        name: "张三",
        count: "22222",
        used: "12",
        state: "预备党员",
        userId: "1"
      },
      {
        key: "2",
        name: "张三",
        count: "22222",
        used: "12",
        state: "党员",
        userId: "2"
      },
      {
        key: "3",
        name: "张三",
        count: "22222",
        used: "12",
        state: "预备党员",
        userId: "3"
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
          dataSource={this.data}
          columns={this.columns}
          rowSelection={rowSelection}
          pagination={pageConfig}
          locale={{ emptyText: "暂无数据" }}
          bordered
        />
      </Nav>
    );
  }
}

export default RankList;
